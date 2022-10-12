using Burgerama_Burger_Shop_App.src;
using Burgerama_Burger_Shop_App.src.handlers;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    internal class LoginUI
    {
        LoginHandler login;
        ManagmentUI managmentUI;
        OrderUI orderUI;

        public LoginUI()
        {
            login = new LoginHandler();
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

            Console.Write("Please enter your Email: ");
            login.GetEmail(Console.ReadLine());

            Program.ClearCurrentConsoleLine();
            Console.Write("Password:");
            login.GetPassword();

            if (login.IsUserManager())
            {
                managmentUI.ManagerMenu();
            }

            if (login.IsUserRegistered())
            {
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
