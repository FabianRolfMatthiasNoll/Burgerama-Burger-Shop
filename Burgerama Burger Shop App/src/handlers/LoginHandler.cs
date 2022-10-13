using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Burgerama_Burger_Shop_App.src.userinterfaces;
using Burgerama_Burger_Shop_App.src.validators;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class LoginHandler
    {
        EmailValidator emailValidator;
        PasswordValidator passwordValidator;
        FileHandler userData;
        List<User> users;
        public string email;
        public string password;

        public LoginHandler()
        {
            userData = new FileHandler("src/data/");
            emailValidator = new EmailValidator();
            passwordValidator = new PasswordValidator();
            users = userData.LoadUserData();
        }

        public void GetEmail(string emailInput)
        {
            emailInput = emailInput.ToLower();
            while (!emailValidator.IsEmailValid(emailInput))
            {
                if (emailInput == "manager" || emailInput == "admin")
                {
                    break;
                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid email adress:");
                emailInput = Console.ReadLine();
                emailInput = emailInput.ToLower();
            }
            this.email = emailInput;
        }

        public void GetPassword()
        {
            password = passwordValidator.PasswordInput();
            password = passwordValidator.HashString(password);
        }

        public bool IsUserManager()
        {
            if (email == "manager" && password == "39F968F400E6B06A5153F37683C348C94C948539B17636C0529A4E833ACE9C40")
            {
                return true;
            }
            return false;
        }

        public bool IsUserRegistered()
        {
            foreach (var user in users)
            {
                //compares the string of user emails and the given email
                if (user.email == email && user.password == password)
                {
                    Console.Clear();
                    return true;
                }
            }
            return false;
        }

        public User ReturnUser()
        {
            foreach (var login in users)
            {
                if (string.Equals(login.email, email) && string.Equals(login.password, password))
                {
                    return login;
                }
            }
            return null;
        }
    }
}
