using Burgerama_Burger_Shop_App.src.validators;
using System.Diagnostics.CodeAnalysis;
using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    [ExcludeFromCodeCoverage]
    public class MainMenu
    {
        readonly LoginUi _loginUi;
        readonly RegistrationUi _registrationUi;
        readonly IIntValidator _intValidator;
        string _userInput;

        public MainMenu()
        {
            _loginUi = new LoginUi();
            _registrationUi = new RegistrationUi();
            _intValidator = new IntValidator(1,2);
        }

        public void MenuUi()
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

            this._userInput = Console.ReadLine();

            while (!_intValidator.IsValid(_userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid number!:");
                _userInput = Console.ReadLine();
            }

            int.TryParse(this._userInput, out int userSelection);

            if (userSelection == 1)
            {
                _registrationUi.RegistrationMenu();
                goto Return;
            }
            else if (userSelection == 2)
            {
                _loginUi.LoginMenu();
                goto Return;
            }
        }
    }
}
