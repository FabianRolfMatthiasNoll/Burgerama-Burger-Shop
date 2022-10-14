using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    public class BoolValidator
    {
        StringValidator stringValidator;

        public BoolValidator()
        {
            stringValidator = new StringValidator();
        }

        public bool IsStringValidBool(string input)
        {
            input = input.ToLower();
            if (stringValidator.IsStringEmpty(input))
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
