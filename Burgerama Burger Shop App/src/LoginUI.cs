using Burgerama_Burger_Shop_App.src;

namespace Burgerama_Burger_Shop_App
{
    internal class LoginUI
    {
        LoginHandler login;

        public LoginUI()
        {
            login = new LoginHandler();
        }

        public void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("");
            Console.WriteLine("Please input your Login Credentials\n");

            string email = LoginHandler.GetEmail();

            Program.ClearCurrentConsoleLine();
            Console.Write("Password:");
            string password = LoginHandler.HashString(LoginHandler.GetPassword());

            LoginHandler.IsUserManager(email, password);

            if (login.CheckLoginCredentials(email, password))
            {
                User LoggedUser = (User)login.ReturnUser(email, password);
                //instance open not static
                //graphic umlet software
                //inpuvalidation classes
                Ordering.OrderMenu(LoggedUser);
            }

            Console.WriteLine("Your login credentials were incorrect");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            LoginMenu();
        }
    }
}
