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
        public List<Driver> drivers;
        string fileName;

        public ManagmentHandler(string filePath, string fName)
        {
            fileHandler = new FileHandler(filePath);
            drivers = new List<Driver>();
            fileName = fName;
        }

        public void LoadDriverData()
        {
            drivers = fileHandler.ReadJSON<Driver>(fileName);
        }

        public ConsoleTable CreateOrderOverview()
        {
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
            fileHandler.WriteJSON(drivers, fileName);
        }

        public void ReloadData()
        {
            drivers = fileHandler.ReadJSON<Driver>(fileName);
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
            fileHandler.WriteJSON(drivers, fileName);
        }
    }
}
