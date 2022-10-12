using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.validators
{
    internal class IntValidator
    {
        public IntValidator()
        {

        }

        public int IsInputValid(string menuInput, int minVal, int maxVal)
        {
            int menuSelect = 0;
            while (true)
            {
                while (!int.TryParse(menuInput, out menuSelect))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please enter a number!:");
                    menuInput = Console.ReadLine();
                }
                if (menuSelect < minVal || menuSelect > maxVal)
                {
                    //If entered value isnt a integer an error occurs
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please enter a valid Option: ");
                    menuInput = Console.ReadLine();
                }
                else
                {
                    return menuSelect;
                }
            }
        }


    }
}
