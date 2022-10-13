using Burgerama_Burger_Shop_App.products;
using Burgerama_Burger_Shop_App.src.handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    public class RegistrationUI
    {
        RegistrationHandler registrationHandler;

        public RegistrationUI()
        {
            registrationHandler = new RegistrationHandler();
        }
        public void RegistrationMenu()
        {
            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("");
            Console.WriteLine("For perfect customer satisfication we need a bit of information about you :)\n");

            registrationHandler.GetEmail();

            Console.Write("Please choose a Password: ");
            registrationHandler.GetPassword();

            Console.Write("Please enter your Street and Housenumber: ");
            registrationHandler.GetStreet(Console.ReadLine());

            Console.Write("Please enter your ZIP-Code: ");
            registrationHandler.GetZip(Console.ReadLine());

            Console.Write("Please enter your City: ");
            registrationHandler.GetCity(Console.ReadLine());

            Console.WriteLine("Thank you for registering with Burgerama Burger :)");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            registrationHandler.RegisterUser();

        }
    }
}
