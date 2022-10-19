using System;
using Burgerama_Burger_Shop_App.src.handlers;
using Burgerama_Burger_Shop_App.src.validators;
using System.Diagnostics.CodeAnalysis;
using Burgerama_Burger_Shop_App.src.interfaces;
using ConsoleTables;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    [ExcludeFromCodeCoverage]
    public class ManagmentUI
    {
        ManagmentHandler managmentHandler;
        IIntValidator intValidator;
        string userInput;

        public ManagmentUI()
        {
            managmentHandler = new ManagmentHandler("src/data/", "driver_data.json");
            intValidator = new IntValidator(1,3);
        }

        public void ManagerMenu()
        {
            managmentHandler.LoadDriverData();
            MenuStart:
            Console.Clear();
            Console.SetWindowSize(150, 50);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                             Welcome Back Manager");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          Here are all existing Orders:             ");
            
            var table = managmentHandler.CreateOrderOverview();
            table.Write(Format.Alternative);

            Console.WriteLine("\nManagment System Online");
            Console.WriteLine("1) Move Time forward by 15min");
            Console.WriteLine("2) Delete all Orders that are 'closed'");
            Console.WriteLine("3) Return to the main Menu");
            Console.Write("What do you want to do?: ");

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
                managmentHandler.FastForwardTime();
                managmentHandler.ReloadData();
                goto MenuStart;
            }
            else if (userSelection == 2)
            {
                managmentHandler.EraseClosedOrders();
                managmentHandler.ReloadData();
                goto MenuStart;
            }
            else if (userSelection == 3)
            {
                Console.Clear();
            }
        }
    }
}
