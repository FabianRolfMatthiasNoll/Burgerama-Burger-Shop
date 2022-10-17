using Burgerama_Burger_Shop_App.src.userinterfaces;
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
        public List<Driver> drivers;
        string fileNameStates;
        string fileNameConfig;

        public DriverHandler(string filePath, string fNameStates, string fNameConfig)
        {
            fileHandler = new FileHandler(filePath);
            drivers = new List<Driver>();
            fileNameStates = fNameStates;
            fileNameConfig = fNameConfig;
        }

        public void CountOpenOrders()
        {
            LoadDriverStates();
            foreach (var driver in drivers)
            {
                int openOrderCount = 0;

                foreach (var order in driver.orders)
                {
                    if (order.state != State.Closed)
                    {
                        openOrderCount++;
                    }
                }

                driver.openOrders = openOrderCount;
            }
            SaveCurrentDriverStates();
        }

        public void AddOrderToBestDriver(Order order)
        {
            LoadDriverStates();
            CountOpenOrders();
            drivers = drivers.OrderBy(o => o.openOrders).ToList();
            bool driverAdded = false;
            foreach(var driver in drivers)
            {
                if (driver.IsDriverFree())
                {
                    driver.orders.Add(order);
                    driverAdded = true;
                    break;
                }
            }

            if(driverAdded == false)
            {
                drivers[0].orders.Add(order);
            }
            SaveCurrentDriverStates();
        }

        public void UpdateDrivers()
        {
            List<Driver> driverStates = fileHandler.ReadJSON<Driver>(fileNameStates);
            drivers = fileHandler.ReadJSON<Driver>(fileNameConfig);

            foreach (var driver1 in driverStates)
            {
                foreach (var driver2 in drivers)
                {
                    if (driver1.name == driver2.name)
                    {
                        driver2.orders = driver1.orders;
                        driver2.openOrders = driver1.openOrders;
                        driver2.capacity = driver1.capacity;
                    }
                }
            }
            SaveCurrentDriverStates();
        }

        public void LoadDriverStates()
        {
            drivers = fileHandler.ReadJSON<Driver>(fileNameStates);
        }

        public void SaveCurrentDriverStates()
        {
            fileHandler.WriteJSON<Driver>(drivers, fileNameStates);
        }
    }
}
