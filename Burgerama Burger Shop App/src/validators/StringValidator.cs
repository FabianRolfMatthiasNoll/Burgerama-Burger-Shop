using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    public class StringValidator
    {
        public StringValidator()
        {

        }

        public bool IsStringEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }
    }
}
