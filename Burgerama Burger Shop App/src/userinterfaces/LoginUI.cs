using Burgerama_Burger_Shop_App.src;
using Burgerama_Burger_Shop_App.src.handlers;
using System.Diagnostics.CodeAnalysis;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    [ExcludeFromCodeCoverage]
    public class LoginUi
    {
        readonly LoginHandler _login;
        readonly ManagmentUi _managmentUi;
        readonly OrderUi _orderUi;
        string _userInput;

        public LoginUi()
        {
            _login = new LoginHandler("src/data/", "user_data.xml");
            _managmentUi = new ManagmentUi();
            _orderUi = new OrderUi();
        }
        
        public void LoginMenu()
        {
            Return:
            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("");
            Console.WriteLine("Please input your Login Credentials\n");

            _login.LoadUserData();

            Console.Write("Please enter your Email: ");
            _userInput = Console.ReadLine();
            while (!_login.SetEmail(_userInput)) 
            { 
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid email adress:");
                _userInput = Console.ReadLine();
                _userInput = _userInput.ToLower();
            }

            Program.ClearCurrentConsoleLine();
            Console.Write("Password:");
            _login.SetPassword();

            if (_login.IsUserManager())
            {
                _managmentUi.ManagerMenu();
            }

            if (_login.IsUserRegistered())
            {
                Console.Clear();
                _orderUi.OrderMenu(_login.ReturnUser());
            } 
            else if (_login.IsUserManager())
            {

            } 
            else
            {
                Console.WriteLine("Your login credentials were incorrect");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                goto Return;
            }
        }
    }
}
