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
    internal class RegistrationHandler
    {
        EmailValidator emailValidator;
        StringValidator stringValidator;
        PasswordValidator passwordValidator;
        FileHandler userData;
        FileHandler cityData;
        User user;

        public RegistrationHandler()
        {
            user = new User();
            userData = new FileHandler("src/data/");
            cityData = new FileHandler("src/data/");
            emailValidator = new EmailValidator();
            stringValidator = new StringValidator();
            passwordValidator = new PasswordValidator();
        }

        public void GetEmail()
        {
            List<User> users = userData.LoadUserData();

            Console.Write("Please enter your Email: ");
            string email = Console.ReadLine();
            email = email.ToLower();

            while (emailValidator.IsEmailTaken(users, email) || !emailValidator.IsEmailValid(email))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("The Email is either taken or not valid. Please try again: ");
                email = Console.ReadLine();
                email = email.ToLower();
            }

            user.email = email;
        }

        public void GetPassword()
        {
            string password = passwordValidator.PasswordInput();
            user.password = passwordValidator.HashString(password);
        }

        public void GetStreet(string street)
        {
            while (stringValidator.IsStringEmpty(street))
            {
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your Street and Housenumber: ");
                street = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();
            user.street = street;
        }

        public void GetZip(string postal)
        {
            while (stringValidator.IsStringEmpty(postal))
            {
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your ZIP-Code: ");
                postal = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();
            user.postal = postal;
        }

        public void GetCity(string userCity)
        {
            List<City> germanCities = cityData.LoadJSON<City>("german_cities.json");
            bool cityIsInvalid = true;
            while (cityIsInvalid)
            {
                foreach (var city in germanCities)
                {
                    if (city.city == userCity)
                    {
                        cityIsInvalid = false;
                        break;
                    }
                }
                if (cityIsInvalid == false)
                {
                    break;
                }
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your city is not in our delivery range. [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your City: ");
                userCity = Console.ReadLine();
            }
            user.city = userCity;
        }

        public void RegisterUser()
        {
            userData.WriteUserData(user);
        }
    }
}
