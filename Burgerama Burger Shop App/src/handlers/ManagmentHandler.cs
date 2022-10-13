using Burgerama_Burger_Shop_App.src.validators;
using System;
using ConsoleTables;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class ManagmentHandler
    {
        FileHandler fileHandler;
        List<Driver> drivers;

        public ManagmentHandler()
        {
            fileHandler = new FileHandler("src/data/");
            drivers = new List<Driver>();
        }

        public ConsoleTable CreateOrderOverview()
        {
            drivers = fileHandler.ReadJSON<Driver>("driver_data.json");
            var table = new ConsoleTable("Email of User", "Address", "Driver", "Prep. Time", "Ship. Time", "Status");
            foreach (var driver in drivers)
            {
                foreach (var order in driver.orders)
                {
                    table.AddRow(order.customer.email, order.customer.street
                         + " " + order.customer.postal
                         + " " + order.customer.city,
                                 driver.name,
                                 order.prepTime,
                                 order.shipTime,
                                 order.state);
                }
            }
            return table;
        }

        public void FastForwardTime()
        {
            foreach (var driver in drivers)
            {
                foreach (var order in driver.orders)
                {
                    order.DecreaseTotalTime();
                }
            }
            fileHandler.WriteJSON(drivers, "driver_data.json");
            Console.Clear();
        }

        public void ReloadData()
        {
            drivers = fileHandler.ReadJSON<Driver>("driver_data.json");
        }

        public void EraseClosedOrders()
        {
            foreach (var driver in drivers)
            {
            restart:
                foreach (var order in driver.orders)
                {
                    if (order.state == State.Closed)
                    {
                        driver.orders.Remove(order);
                        goto restart;
                    }
                }
            }
            fileHandler.WriteJSON(drivers, "driver_data.json");
            Console.Clear();
        }
    }
}
