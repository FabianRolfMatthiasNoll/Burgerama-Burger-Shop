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
        IEmailValidator emailValidator;
        IValidator stringValidator;
        IIntValidator intValidator;
        PasswordValidator passwordValidator;
        FileHandler userData;
        FileHandler cityData;
        public List<City> germanCities;
        public List<User> users;
        public User user;

        public RegistrationHandler(string filePath)
        {
            user = new User();
            users = new List<User>();
            germanCities = new List<City>();
            userData = new FileHandler(filePath);
            cityData = new FileHandler(filePath);
            emailValidator = new EmailValidator();
            stringValidator = new StringValidator();
            intValidator = new IntValidator(0,0);
            passwordValidator = new PasswordValidator();
        }

        public void LoadRegistrationData(string userDataFile, string cityFile)
        {
            users = userData.LoadUserData(userDataFile);
            germanCities = cityData.ReadJSON<City>(cityFile);
        }

        public bool SetEmailIfValid(string email)
        {
            if(!emailValidator.IsEmailTaken(users, email) && emailValidator.IsValid(email))
            {
                email = email.ToLower();
                user.email = email;
                return true;
            }
            return false;
        }

        [ExcludeFromCodeCoverage]
        public bool GetPassword()
        {
            string password = passwordValidator.PasswordInput();
            if (stringValidator.IsValid(password))
            {
                return false;
            } else
            {
                user.password = passwordValidator.HashString(password);
                return true;
            } 
        }

        public bool SetStreetIfValid(string street)
        {
            if (!stringValidator.IsValid(street) && !intValidator.IsInputInt(street))
            {
                user.street = street;
                return true;
            }
            return false;
        }

        public bool SetZIPIfValid(string postal)
        {
            if (!stringValidator.IsValid(postal) && intValidator.IsInputInt(postal) && intValidator.IsIntPositiv(postal))
            {
                user.postal = postal;
                return true;
            }
            return false;
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
            userData.WriteUserData(user,fileName);
        }
    }
}
