﻿using System.ComponentModel.DataAnnotations;
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

        public LoginHandler(string filePath, string fileName)
        {
            userData = new FileHandler(filePath);
            emailValidator = new EmailValidator();
            passwordValidator = new PasswordValidator();
            users = userData.LoadUserData(fileName);
        }

        public bool SetEmail(string emailInput)
        {
            emailInput = emailInput.ToLower();

            if (emailInput == "manager" || emailInput == "admin")
            {
                this.email = emailInput;
                return true;
            }

            if (emailValidator.IsEmailValid(emailInput))
            {
                this.email = emailInput;
                return true;
            }
            return false;
        }

        public void SetPassword()
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
                if (user.email == email && user.password == password)
                {
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
                    User user = new User();
                    user = login;
                    return user;
                }
            }
            return null;
        }
    }
}
