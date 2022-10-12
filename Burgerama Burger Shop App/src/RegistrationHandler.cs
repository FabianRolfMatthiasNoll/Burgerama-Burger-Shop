using System;
using Burgerama_Burger_Shop_App.src;
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

namespace Burgerama_Burger_Shop_App.src
{
    internal class RegistrationHandler
    {
        EmailValidator emailValidator;
        EmptyStringValidator stringValidator;
        PasswordValidator passwordValidator;
        FileHandler userData;
        FileHandler cityData;
        User user;
        City city;

        public RegistrationHandler()
        {
            user = new User();
            city = new City();
            userData = new FileHandler("src/data/");
            cityData = new FileHandler("src/data/");
            emailValidator = new EmailValidator();
            stringValidator = new EmptyStringValidator();
            passwordValidator = new PasswordValidator();
        }

        public void GetEmail()
        {
            Console.Write("Please enter your Email: ");
            var emailCheck = new EmailAddressAttribute();
            string emailUnfiltered = Console.ReadLine();
            string email = emailUnfiltered.ToLower();

            while (IsEmailTaken(email) || !emailCheck.IsValid(email))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("The Email is either taken or not valid. Please try again: ");
                email = Console.ReadLine();
            }
            this.user.email = email;
        }

        bool IsEmailTaken(string email)
        {
            List<User> users = userData.LoadUserData();

            foreach (var user in users)
            {
                //compares the string of user emails and the given email
                if (user.email == email)
                {
                    return true;
                }
            }
            return false;
        }

        public void GetPassword()
        {
            string password = passwordValidator.PasswordInput();
            this.user.password = passwordValidator.HashString(password);
        } 

        public void GetStreet(string street)
        {
            while (string.IsNullOrEmpty(street))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Input can`t be empty! [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your Street and Housenumber: ");
                street = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();
            this.user.street = street;
        }

        public void GetZip(string postal)
        {
            while (string.IsNullOrEmpty(postal))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Input can`t be empty! [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your ZIP-Code: ");
                postal = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();
            this.user.postal = postal;
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
            this.user.city = userCity;
        }

        public void RegisterUser()
        {
            userData.WriteUserData(this.user);
        }
    }
}
