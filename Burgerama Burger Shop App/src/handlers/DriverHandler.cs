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
        string fileNameStates;
        string fileNameConfig;

        public DriverHandler(string filePath, string fNameStates, string fNameConfig)
        {
            fileHandler = new FileHandler(filePath);
            drivers = new List<Driver>();
            fileNameStates = fNameStates;
            fileNameConfig = fNameConfig;
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
            drivers = fileHandler.ReadJSON<Driver>(fileNameStates);
            List<Driver> driverStates = fileHandler.ReadJSON<Driver>(fileNameConfig);

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
            drivers = fileHandler.ReadJSON<Driver>(fileNameStates);
        }

        public void SaveCurrentDriverStates()
        {
            fileHandler.WriteJSON<Driver>(drivers, fileNameStates);
        }
    }
}
