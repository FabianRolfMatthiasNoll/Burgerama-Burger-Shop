using Burgerama_Burger_Shop_App.products;

namespace Burgerama_Burger_Shop_App
{
    class Program
    {
        public static void Main()
        {
            //check if all necessary files exist
            CheckFileDependencies();

            //update drivers if driver_config.json has been changed
            Driver.UpdateDrivers();

            //check all product ids and throw an error if there are duplicate ones
            Product.CheckProductID();

            Console.SetWindowSize(140, 30);

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
                    ClearCurrentConsoleLine();
                    Console.Write("Please enter a number!:");
                    menuInput = Console.ReadLine();
                }

                if (menu < 1 || menu > 2)
                {
                    //If entered value isnt a integer an error occurs
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                    Console.Write("Please enter a valid Option: ");
                    menuInput = Console.ReadLine();
                }

                if (menu == 1)
                {
                    Registration.RegistrationMenu();
                } else if (menu == 2)
                {
                    Login.LoginMenu();
                }
            }
        }

        public static void CheckFileDependencies()
        {
            Console.WriteLine("Loading User Data...");
            if (!File.Exists("src/data/user_data.xml"))
            {
                Console.WriteLine("[Error] user data wasnt found!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Thread.Sleep(500);
            Console.WriteLine("User Data loaded successfully");
            Thread.Sleep(200);
            Console.WriteLine("Loading Product Data...");
            if (!File.Exists("src/data/product_data.json"))
            {
                Console.WriteLine("[Error] Product Data wasn`t found!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Thread.Sleep(500);
            Console.WriteLine("Product Data loaded successfully");
            Thread.Sleep(200);
            Console.WriteLine("Loading Driver Configurations...");
            if (!File.Exists("src/data/driver_config.json"))
            {
                Console.WriteLine("[Error] Driver Configuration wasnt found!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Thread.Sleep(500);
            Console.WriteLine("Driver Configuration loaded successfully");
            Thread.Sleep(200);
            Console.WriteLine("Loading Driver Data...");
            if (!File.Exists("src/data/driver_data.json"))
            {
                Console.WriteLine("[Error] Driver Data wasnt found!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Thread.Sleep(500);
            Console.WriteLine("Driver Data loaded successfully");
            Thread.Sleep(200);
            Console.WriteLine("All Files are Ready. Press any key to start the application");
            Console.ReadKey();
            Console.Clear();

        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}