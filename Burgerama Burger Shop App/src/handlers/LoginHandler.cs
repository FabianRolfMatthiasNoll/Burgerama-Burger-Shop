using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Burgerama_Burger_Shop_App.src.interfaces;
using Burgerama_Burger_Shop_App.src.userinterfaces;
using Burgerama_Burger_Shop_App.src.validators;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class LoginHandler
    {
        readonly IEmailValidator _emailValidator;
        readonly PasswordValidator _passwordValidator;
        readonly FileHandler _userData;
        List<User> _users;
        readonly string _fileName;
        public string email;
        public string password;

        public LoginHandler(string filePath, string fName)
        {
            _userData = new FileHandler(filePath);
            _emailValidator = new EmailValidator();
            _passwordValidator = new PasswordValidator();
            _users = new List<User>();
            _fileName = fName;
        }

        public void LoadUserData()
        {
            _users = _userData.LoadUserData(_fileName);
        }

        public bool SetEmail(string emailInput)
        {
            emailInput = emailInput.ToLower();

            if (emailInput == "manager" || emailInput == "admin")
            {
                this.email = emailInput;
                return true;
            }

            if (_emailValidator.IsValid(emailInput))
            {
                this.email = emailInput;
                return true;
            }
            return false;
        }

        [ExcludeFromCodeCoverage]
        public void SetPassword()
        {
            password = _passwordValidator.PasswordInput();
            password = _passwordValidator.HashString(password);
        }

        public bool IsUserManager()
        {
            if (email == "manager" && password == "39F968F400E6B06A5153F37683C348C94C948539B17636C0529A4E833ACE9C40")
            {
                return true;
            }
            return false;
        }

        public bool IsUserAdmin()
        {
            if (email == "admin" && password == "182906571A6237B82F137B3F2D83C003E564209E481304BF32F84B44A126C5B3")
            {
                return true;
            }
            return false;
        }

        public bool IsUserRegistered()
        {
            foreach (var user in _users)
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
            foreach (var login in _users)
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
