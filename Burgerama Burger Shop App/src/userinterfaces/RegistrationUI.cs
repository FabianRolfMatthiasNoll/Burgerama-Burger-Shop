using Burgerama_Burger_Shop_App.products;
using Burgerama_Burger_Shop_App.src.handlers;
using Burgerama_Burger_Shop_App.src.validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    [ExcludeFromCodeCoverage]
    public class RegistrationUI
    {
        RegistrationHandler registrationHandler;
        StringValidator stringValidator;

        public RegistrationUI()
        {
            registrationHandler = new RegistrationHandler("src/data/");
            stringValidator = new StringValidator();
        }
        public void RegistrationMenu()
        {
            registrationHandler.LoadRegistrationData("user_data.xml", "german_cities.json");

            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("");
            Console.WriteLine("For perfect customer satisfication we need a bit of information about you :)\n");

            string userInput;
            Console.Write("Please enter a email: ");
            userInput = Console.ReadLine();

            while (!registrationHandler.SetEmailIfValid(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Email is either taken or invalid. Please try again: ");
                userInput= Console.ReadLine();
            }

            Console.Write("Please choose a Password: ");
            while (!registrationHandler.GetPassword())
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Password cant be blank. Please choose another password: ");
            }
            

            Console.Write("Please enter your Street and Housenumber: ");
            userInput = Console.ReadLine();

            while (!registrationHandler.SetStreetIfValid(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Input can`t be empty! [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your Street and Housenumber: ");
                userInput = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();

            Console.Write("Please enter your ZIP-Code: ");
            userInput = Console.ReadLine();

            while (!registrationHandler.SetZIPIfValid(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Input can`t be empty! [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your ZIP-Code: ");
                userInput = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();

            Console.Write("Please enter your City: ");
            userInput = Console.ReadLine();

            while (!registrationHandler.SetCityIfValid(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your city is not in our delivery range. Please try again: ");
                userInput = Console.ReadLine();
            }

            Console.WriteLine("Thank you for registering with Burgerama Burger :)");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            registrationHandler.RegisterUser("user_data.xml");

        }
    }
}
