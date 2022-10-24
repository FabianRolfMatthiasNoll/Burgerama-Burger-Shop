using Burgerama_Burger_Shop_App.src.validators;
using System;
using ConsoleTables;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class ManagementHandler
    {
        private readonly FileHandler _fileHandler;
        public List<Driver> drivers;
        private readonly string _fileName;

        public ManagementHandler(string filePath, string fName)
        {
            _fileHandler = new FileHandler(filePath);
            drivers = new List<Driver>();
            _fileName = fName;
        }

        public void LoadDriverData()
        {
            drivers = _fileHandler.ReadDriverList(_fileName);
        }

        [ExcludeFromCodeCoverage]
        public ConsoleTable CreateDriverOverview()
        {
            var table = new ConsoleTable("Driver Name", "Mood", "Capacity", "Open Orders");
            foreach (var driver in drivers)
            {
                table.AddRow(driver.name, driver.mood.MoodName, driver.capacity, driver.openOrders);
            }
            return table;
        }

        [ExcludeFromCodeCoverage]
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
                driver.mood = driver.mood.SwitchToNextMoodTimeCycle();
            }
            _fileHandler.WriteJson(drivers, _fileName);
        }

        [ExcludeFromCodeCoverage]
        public void ReloadData()
        {
            drivers = _fileHandler.ReadDriverList(_fileName);
            foreach (var driver in drivers)
            {
                driver.CountOpenOrders();
            }
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
            _fileHandler.WriteJson(drivers, _fileName);
        }
    }
}
