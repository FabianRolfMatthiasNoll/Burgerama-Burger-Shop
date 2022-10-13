using System;
using Burgerama_Burger_Shop_App.src.handlers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using Burgerama_Burger_Shop_App.src.validators;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    public class ManagmentUI
    {
        ManagmentHandler managmentHandler;
        IntValidator intValidator;

        public ManagmentUI()
        {
            managmentHandler = new ManagmentHandler();
            intValidator = new IntValidator();
        }

        public void ManagerMenu()
        {
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

            int menu = intValidator.IsInputValid(Console.ReadLine(), 1, 3);

            if (menu == 1)
            {
                managmentHandler.FastForwardTime();
                managmentHandler.ReloadData();
                goto MenuStart;
            }
            else if (menu == 2)
            {
                managmentHandler.EraseClosedOrders();
                managmentHandler.ReloadData();
                goto MenuStart;
            }
            else if (menu == 3)
            {
                Console.Clear();
            }
        }
    }
}
