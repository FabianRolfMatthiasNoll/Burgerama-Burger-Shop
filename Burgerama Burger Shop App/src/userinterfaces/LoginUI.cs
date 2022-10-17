using Burgerama_Burger_Shop_App.src;
using Burgerama_Burger_Shop_App.src.handlers;
using System.Diagnostics.CodeAnalysis;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    [ExcludeFromCodeCoverage]
    public class LoginUI
    {
        LoginHandler login;
        ManagmentUI managmentUI;
        OrderUI orderUI;
        string userInput;

        public LoginUI()
        {
            login = new LoginHandler("src/data/", "user_data.xml");
            managmentUI = new ManagmentUI();
            orderUI = new OrderUI();
        }
        
        public void LoginMenu()
        {
            Return:
            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("");
            Console.WriteLine("Please input your Login Credentials\n");

            login.LoadUserData();

            Console.Write("Please enter your Email: ");
            userInput = Console.ReadLine();
            while (!login.SetEmail(userInput)) 
            { 
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid email adress:");
                userInput = Console.ReadLine();
                userInput = userInput.ToLower();
            }

            Program.ClearCurrentConsoleLine();
            Console.Write("Password:");
            login.SetPassword();

            if (login.IsUserManager())
            {
                managmentUI.ManagerMenu();
            }

            if (login.IsUserRegistered())
            {
                Console.Clear();
                orderUI.OrderMenu(login.ReturnUser());
            } 
            else if (login.IsUserManager())
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
