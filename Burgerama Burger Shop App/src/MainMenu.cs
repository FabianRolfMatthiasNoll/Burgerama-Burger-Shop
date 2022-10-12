using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src
{
    internal class MainMenu
    {
        LoginUI loginUI;
        RegistrationUI registrationUI;

        public MainMenu()
        {
            loginUI = new LoginUI();
            registrationUI = new RegistrationUI();
        }

        public void menuUI()
        {
            Return:
            Console.SetWindowSize(140, 30);
            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("Welcome to Burgerama Burger Shop");
            Console.WriteLine("Please log in to take your order or register a new account with us :)");
            Console.WriteLine("(1) Register at Burgerama");
            Console.WriteLine("(2) Login at Burgerama");
            Console.WriteLine("");
            Console.Write("Please select an Option: ");
            var menuInput = Console.ReadLine();
            int menu;
            while (true)
            {
                while (!int.TryParse(menuInput, out menu))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please enter a number!:");
                    menuInput = Console.ReadLine();
                }

                if (menu < 1 || menu > 2)
                {
                    //If entered value isnt a integer an error occurs
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please enter a valid Option: ");
                    menuInput = Console.ReadLine();
                }

                if (menu == 1)
                {
                    registrationUI.RegistrationMenu();
                    goto Return;
                }
                else if (menu == 2)
                {
                    loginUI.LoginMenu();
                    goto Return;
                }
            }
        }

    }
}
