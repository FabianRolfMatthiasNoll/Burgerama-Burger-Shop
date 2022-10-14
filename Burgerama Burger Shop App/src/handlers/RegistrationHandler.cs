using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.validators;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class RegistrationHandler
    {
        EmailValidator emailValidator;
        StringValidator stringValidator;
        IntValidator intValidator;
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
            if(!emailValidator.IsEmailTaken(users, email) && emailValidator.IsEmailValid(email))
            {
                email = email.ToLower();
                user.email = email;
                return true;
            }
            return false;
        }

        public void GetPassword()
        {
            string password = passwordValidator.PasswordInput();
            user.password = passwordValidator.HashString(password);
        }

        public bool SetStreetIfValid(string street)
        {
            if (!stringValidator.IsStringEmpty(street) && !intValidator.IsInputInt(street))
            {
                user.street = street;
                return true;
            }
            return false;
        }

        public bool SetZIPIfValid(string postal)
        {
            if (!stringValidator.IsStringEmpty(postal) && intValidator.IsInputInt(postal) && intValidator.IsIntPositiv(postal))
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
