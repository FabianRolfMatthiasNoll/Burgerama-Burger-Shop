using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    public class IntValidator
    {
        public int minVal;
        public int maxVal;
        public int outputInt;
        public string inputString;
        

        public IntValidator(int minVal, int maxVal)
        {
            this.minVal = minVal;
            this.maxVal = maxVal;
        }

        public bool IsInputValid(string input)
        {
            inputString = input;
            if (this.IsInputInt() && this.IsInputInBound())
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool IsInputValid(int input)
        {
            outputInt = input;
            if (this.IsInputInBound())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInputInt()
        {
            return int.TryParse(this.inputString, out this.outputInt);
        }

        public bool IsInputInBound()
        {
            if (this.outputInt < this.minVal || this.outputInt > this.maxVal)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
