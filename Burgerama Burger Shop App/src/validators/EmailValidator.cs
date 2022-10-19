using Burgerama_Burger_Shop_App.src.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    public class EmailValidator : IEmailValidator
    {
        public EmailValidator()
        {

        }

        public bool IsValid(string emailInput)
        {
            var emailCheck = new EmailAddressAttribute();
            string email = emailInput.ToLower();
            if (emailCheck.IsValid(email))
            {
                return true;
            }
            return false;
        }

        public bool IsEmailTaken(List<User> users, string emailInput)
        {
            string email = emailInput.ToLower();
            foreach (var user in users)
            {
                if (user.email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
