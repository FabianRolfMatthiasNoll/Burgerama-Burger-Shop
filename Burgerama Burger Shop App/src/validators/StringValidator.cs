using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    internal class StringValidator
    {
        public StringValidator()
        {

        }

        public bool IsStringEmpty(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Input can`t be empty! [Enter]");
                Console.ReadKey();
                return true;
            }
            return false;
        }
    }
}
