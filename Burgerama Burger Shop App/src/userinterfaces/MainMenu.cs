using Burgerama_Burger_Shop_App.src.validators;
using System.Diagnostics.CodeAnalysis;
using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    [ExcludeFromCodeCoverage]
    public class MainMenu
    {
        LoginUI loginUI;
        RegistrationUI registrationUI;
        IIntValidator intValidator;
        string userInput;

        public MainMenu()
        {
            loginUI = new LoginUI();
            registrationUI = new RegistrationUI();
            intValidator = new IntValidator(1,2);
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

            this.userInput = Console.ReadLine();

            while (!intValidator.IsValid(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid number!:");
                userInput = Console.ReadLine();
            }

            int.TryParse(this.userInput, out int userSelection);

            if (userSelection == 1)
            {
                registrationUI.RegistrationMenu();
                goto Return;
            }
            else if (userSelection == 2)
            {
                loginUI.LoginMenu();
                goto Return;
            }
        }
    }
}
