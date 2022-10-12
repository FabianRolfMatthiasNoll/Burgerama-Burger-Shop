using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    internal class DriverHandler
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
            string jsonDriver = File.ReadAllText("src/data/driver_config.json");
            string jsonData = File.ReadAllText("src/data/driver_data.json");

            var driverStates = JsonConvert.DeserializeObject<List<Driver>>(jsonData);

            List<Driver> drivers = JsonConvert.DeserializeObject<List<Driver>>(jsonDriver);

            foreach (var driver1 in drivers)
            {
                foreach (var driver2 in driverStates)
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
            string json = JsonConvert.SerializeObject(drivers, Formatting.Indented);
            File.WriteAllText(@"src/data/driver_data.json", json);
        }
    }
}
