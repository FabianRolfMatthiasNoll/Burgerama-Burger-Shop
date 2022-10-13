using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    public class EmailValidator
    {
        public EmailValidator()
        {

        }

        public bool IsEmailValid(string emailInput)
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
                //compares the string of user emails and the given email
                if (user.email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
