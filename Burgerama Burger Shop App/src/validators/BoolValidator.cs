using Burgerama_Burger_Shop_App.src.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    public class BoolValidator : IValidator
    {
        IValidator stringValidator;

        public BoolValidator()
        {
            stringValidator = new StringValidator();
        }

        public bool IsValid(string input)
        {
            input = input.ToLower();
            if (stringValidator.IsValid(input))
            {
                return false;
            }

            if (input == "true" || input == "false")
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
