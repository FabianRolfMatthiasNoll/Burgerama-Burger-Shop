using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Security.Principal;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Burgerama_Burger_Shop_App
{
    class Program
    {
        public static void Main()
        {

            Console.SetWindowSize(140, 30);

            //Registration.RegistrationMenu();

            //Login.LoginMenu();

            //Ordering.OrderMenu("Test@email.com");

            //End of Debugging Bypass

            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("Welcome to Burgerama Burger Shop");
            Console.WriteLine("Please log in to take your order or register a new account with us :)");
            Console.WriteLine("(1) Register at Burgerama");
            Console.WriteLine("(2) Login at Burgerama");
            Console.WriteLine("");
            Console.Write("Please select an Option: ");
            int menu = Convert.ToInt32(Console.ReadLine());

            while(menu < 1 || menu > 2)
            {
                //If entered value isnt a integer an error occurs
                ClearCurrentConsoleLine();
                Console.Write("Please enter a valid Option: ");
                menu = Convert.ToInt32(Console.ReadLine());
            }
            if(menu == 1)
            {
                Registration.RegistrationMenu();
            } else
            {
                Login.LoginMenu();
            }
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}