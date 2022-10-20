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
        readonly FileHandler _fileHandler;
        public List<Driver> drivers;
        readonly string _fileNameStates;
        readonly string _fileNameConfig;

        public DriverHandler(string filePath, string fNameStates, string fNameConfig)
        {
            _fileHandler = new FileHandler(filePath);
            drivers = new List<Driver>();
            _fileNameStates = fNameStates;
            _fileNameConfig = fNameConfig;
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
            List<Driver> driverStates = _fileHandler.ReadJson<Driver>(_fileNameStates);
            drivers = _fileHandler.ReadJson<Driver>(_fileNameConfig);

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
            drivers = _fileHandler.ReadJson<Driver>(_fileNameStates);
        }

        public void SaveCurrentDriverStates()
        {
            _fileHandler.WriteJson<Driver>(drivers, _fileNameStates);
        }
    }
}
