using System;
using Burgerama_Burger_Shop_App.src.handlers;
using Burgerama_Burger_Shop_App.src.validators;
using System.Diagnostics.CodeAnalysis;
using Burgerama_Burger_Shop_App.src.interfaces;
using ConsoleTables;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    [ExcludeFromCodeCoverage]
    public class ManagmentUi
    {
        readonly ManagementHandler _managementHandler;
        readonly IIntValidator _intValidator;
        string _userInput;

        public ManagmentUi()
        {
            _managementHandler = new ManagementHandler("src/data/", "driver_data.json");
            _intValidator = new IntValidator(1,3);
        }

        public void ManagerMenu()
        {
            _managementHandler.LoadDriverData();
            MenuStart:
            Console.Clear();
            Console.SetWindowSize(180, 50);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                             Welcome Back Manager");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          Here are all existing Orders:             ");
            var tableDriver = _managementHandler.CreateDriverOverview();
            tableDriver.Write(Format.Alternative);
            var table = _managementHandler.CreateOrderOverview();
            table.Write(Format.Alternative);

            Console.WriteLine("\nManagment System Online");
            Console.WriteLine("1) Move Time forward by 15min");
            Console.WriteLine("2) Delete all Orders that are 'closed'");
            Console.WriteLine("3) Return to the main Menu");
            Console.Write("What do you want to do?: ");

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
                _managementHandler.FastForwardTime();
                _managementHandler.ReloadData();
                goto MenuStart;
            }
            else if (userSelection == 2)
            {
                _managementHandler.EraseClosedOrders();
                _managementHandler.ReloadData();
                goto MenuStart;
            }
            else if (userSelection == 3)
            {
                Console.Clear();
            }
        }
    }
}
