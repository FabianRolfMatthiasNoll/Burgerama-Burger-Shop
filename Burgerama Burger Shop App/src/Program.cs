using Burgerama_Burger_Shop_App.products;
using Burgerama_Burger_Shop_App.src.handlers;
using Burgerama_Burger_Shop_App.src.userinterfaces;
using System.Runtime.CompilerServices;

namespace Burgerama_Burger_Shop_App
{
    class Program
    {
        public static void Main()
        {
            MainMenu mainMenu = new MainMenu();
            DriverHandler driverHandler = new DriverHandler();

            CheckFileDependencies();

            //update drivers if driver_config.json has been changed
            driverHandler.UpdateDrivers();

            Product.CheckProductID();

            mainMenu.menuUI();
        }

        public static void CheckFileDependencies()
        {
            FileHandler fileSystem = new FileHandler("src/data/");
            Console.WriteLine("Loading User Data...");
            if (!fileSystem.IsDataAvailable("user_data.xml"))
            {
                Console.WriteLine("[Error] user data wasnt found!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Thread.Sleep(500);
            Console.WriteLine("User Data loaded successfully");
            Thread.Sleep(200);
            Console.WriteLine("Loading Product Data...");
            if (!fileSystem.IsDataAvailable("product_data.json"))
            {
                Console.WriteLine("[Error] Product Data wasn`t found!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Thread.Sleep(500);
            Console.WriteLine("Product Data loaded successfully");
            Thread.Sleep(200);
            Console.WriteLine("Loading Driver Configurations...");
            if (!fileSystem.IsDataAvailable("driver_config.json"))
            {
                Console.WriteLine("[Error] Driver Configuration wasnt found!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Thread.Sleep(500);
            Console.WriteLine("Driver Configuration loaded successfully");
            Thread.Sleep(200);
            Console.WriteLine("Loading Driver Data...");
            if (!fileSystem.IsDataAvailable("driver_data.json"))
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