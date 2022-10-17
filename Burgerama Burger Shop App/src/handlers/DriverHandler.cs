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
            foreach(var driver in drivers)
            {
                foreach (var order in driver.orders)
                {
                    if (order.state != State.Closed)
                    {
                        driver.openOrders++;
                    }
                }
            }
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

        public int CheckLeastOpenOrders()
        {
            int index = 0;
            int orders = drivers[0].orders.Count;
            foreach (var driver in drivers)
            {
                if (driver.orders.Count < orders)
                {
                    index++;
                    orders = driver.orders.Count;
                }
            }
            return index;
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
