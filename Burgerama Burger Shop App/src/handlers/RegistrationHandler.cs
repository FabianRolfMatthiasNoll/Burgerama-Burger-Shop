using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.validators;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class RegistrationHandler
    {
        private readonly IEmailValidator _emailValidator;
        private readonly IValidator _stringValidator;
        private readonly IIntValidator _intValidator;
        private readonly PasswordValidator _passwordValidator;
        private readonly FileHandler _userData;
        private readonly FileHandler _cityData;
        public List<City> germanCities;
        public List<User> users;
        public User user;

        public RegistrationHandler(string filePath)
        {
            user = new User();
            users = new List<User>();
            germanCities = new List<City>();
            _userData = new FileHandler(filePath);
            _cityData = new FileHandler(filePath);
            _emailValidator = new EmailValidator();
            _stringValidator = new StringValidator();
            _intValidator = new IntValidator(0,0);
            _passwordValidator = new PasswordValidator();
        }

        public void LoadRegistrationData(string userDataFile, string cityFile)
        {
            users = _userData.LoadUserData(userDataFile);
            germanCities = _cityData.ReadJson<City>(cityFile);
        }

        public bool SetEmailIfValid(string email)
        {
            if (_emailValidator.IsEmailTaken(users, email) || !_emailValidator.IsValid(email)) return false;
            email = email.ToLower();
            user.email = email;
            return true;
        }

        [ExcludeFromCodeCoverage]
        public bool GetPassword()
        {
            string password = _passwordValidator.PasswordInput();
            if (_stringValidator.IsValid(password))
            {
                return false;
            } else
            {
                user.password = _passwordValidator.HashString(password);
                return true;
            } 
        }

        public bool SetStreetIfValid(string street)
        {
            if (_stringValidator.IsValid(street) || _intValidator.IsInputInt(street)) return false;
            user.street = street;
            return true;
        }

        public bool SetZipIfValid(string postal)
        {
            if (_stringValidator.IsValid(postal) || !_intValidator.IsInputInt(postal) ||
                !_intValidator.IsIntPositiv(postal)) return false;
            user.postal = postal;
            return true;
        }

        public bool SetCityIfValid(string userCity)
        {
            foreach (var city in germanCities)
            {
                if (city.city == userCity)
                {
                    user.city = userCity;
                    return true;
                }
            }
            return false;
        }

        public void RegisterUser(string fileName)
        {
            _userData.WriteUserData(user,fileName);
        }
    }
}
