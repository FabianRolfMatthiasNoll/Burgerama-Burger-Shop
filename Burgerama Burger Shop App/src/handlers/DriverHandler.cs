using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class DriverHandler
    {
        FileHandler fileHandler;
        List<Driver> drivers;

        public DriverHandler()
        {
            fileHandler = new FileHandler("src/data/");
            drivers = new List<Driver>();
        }

        public void AddOrderToDriver(Order order)
        {
            UpdateDriverStates();
            bool driverAvailable = false;
            foreach (var driver in drivers)
            {
                if (Driver.IsDriverFree(driver))
                {
                    driver.orders.Add(order);
                    driverAvailable = true;
                    break;
                }
            }
            if (!driverAvailable)
            {
                int driverNum = Driver.CheckLeastOpenOrders(drivers);
                drivers[driverNum].orders.Add(order);
            }
            SaveCurrentDriverStates();
        }

        public void UpdateDrivers()
        {
            drivers = fileHandler.ReadJSON<Driver>("driver_data.json");
            List<Driver> driverStates = fileHandler.ReadJSON<Driver>("driver_config.json");

            foreach (var driver1 in driverStates)
            {
                foreach (var driver2 in drivers)
                {
                    if (driver1.name == driver2.name)
                    {
                        driver1.orders = driver2.orders;
                    }
                }
            }
            SaveCurrentDriverStates();
        }

        public void UpdateDriverStates()
        {
            drivers = fileHandler.ReadJSON<Driver>("driver_data.json");
        }

        public void SaveCurrentDriverStates()
        {
            fileHandler.WriteJSON<Driver>(drivers, "driver_data.json");
        }
    }
}
