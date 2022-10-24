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

        public Driver ReturnOpenDriver()
        {
            LoadDriverStates();
            drivers = drivers.OrderBy(o => o.openOrders).ToList();
            foreach (var driver in drivers)
            {
                if (driver.IsDriverFree())
                {
                    return driver;
                }
            }
            return drivers[0];
        }

        public void AddOrderToDriver(Order order, Driver openDriver)
        {
            for (int i = 0; i < drivers.Count; i++)
            {
                if (drivers[i].name == openDriver.name)
                {
                    drivers[i] = openDriver;
                    //Add Functionality for new Mood Setting driver.CalculateMood
                    break;
                }
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
            foreach (var driver in drivers)
            {
                driver.CountOpenOrders();
            }
        }

        public void SaveCurrentDriverStates()
        {
            _fileHandler.WriteJson<Driver>(drivers, _fileNameStates);
        }
    }
}
