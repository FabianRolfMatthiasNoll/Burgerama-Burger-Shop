using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Burgerama_Burger_Shop_App
{
    internal class Managment
    {
        public static void ManagerMenu()
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                             Welcome Back Manager");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          Here are all existing Orders:             ");
            var table = new ConsoleTable("Email of User", "Address", "Driver", "Prep. Time", "Ship. Time", "Status");
            List<Driver> drivers = Driver.LoadCurrentDriverStates();
            foreach(var driver in drivers)
            {
                foreach(var order in driver.orders)
                {
                    table.AddRow(order.customer.email,order.customer.street + " " + order.customer.postal + " " + order.customer.city, driver.name, order.prepTime, order.shipTime, order.state);
                }
            }
            table.Write(Format.Alternative);

            Console.WriteLine("\nManagment System Online");
            Console.WriteLine("1) Move Time forward by 15min");
            Console.WriteLine("2) Delete all Orders that are 'closed'");
            Console.WriteLine("3) Return to the main Menu");
            Console.Write("What do you want to do?: ");

            int menu = Convert.ToInt32(Console.ReadLine());

            while (menu < 1 || menu > 3)
            {
                //If entered value isnt a integer an error occurs
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid Option: ");
                menu = Convert.ToInt32(Console.ReadLine());
            }
            if (menu == 1)
            {
                SpeedUpTime(drivers);
                ManagerMenu();
            }
            else if(menu == 2)
            {
                EraseClosedOrders(drivers);
                ManagerMenu();
            } else
            {
                Console.Clear();
                Program.Main();
            }
        }

        static void SpeedUpTime(List<Driver> drivers)
        {
            foreach (var driver in drivers)
            {
                foreach (var order in driver.orders)
                {
                    Order.DecreaseTotalTime(order);
                }
            }
            Driver.SaveCurrentDriverStates(drivers);
            Console.Clear();
        }

        static void EraseClosedOrders(List<Driver> drivers)
        {

        }
    }
}
