using System;
using Burgerama_Burger_Shop_App.src.handlers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.validators;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    public class MainMenu
    {
        LoginUI loginUI;
        RegistrationUI registrationUI;
        IntValidator intValidator;

        public MainMenu()
        {
            loginUI = new LoginUI();
            registrationUI = new RegistrationUI();
            intValidator = new IntValidator();
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
            int menu = intValidator.IsInputValid(Console.ReadLine(), 1, 2);

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
