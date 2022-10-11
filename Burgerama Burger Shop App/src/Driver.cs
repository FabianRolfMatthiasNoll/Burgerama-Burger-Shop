using Burgerama_Burger_Shop_App.products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App
{
    internal class Driver
    {
        public string name;
        public int capacity;

        public List<Order> orders;

        public Driver(string inName, int inCapacity)
        {
            name = inName;
            capacity = inCapacity;
            orders = new List<Order>();
        }
        public static bool IsDriverFree(Driver driver)
        {
            if(driver.capacity > driver.orders.Count)
            {
                return true;
            }
            return false;
        }

        public static int CheckLeastOpenOrders(List<Driver> drivers)
        {
            int index = 0;
            int orders = drivers[0].orders.Count;
            foreach(var driver in drivers)
            {
                if(driver.orders.Count < orders)
                {
                    index++;
                    orders = driver.orders.Count;
                }
            }
            return index;
        }

        public static void AddOrderToDriver(Order order)
        {
            List<Driver> drivers = Driver.LoadCurrentDriverStates();
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
            Driver.SaveCurrentDriverStates(drivers);
        }

        public static List<Driver> LoadCurrentDriverStates()
        {
            string json = File.ReadAllText("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\src\\data\\driver_data.json");

            var drivers = JsonConvert.DeserializeObject<List<Driver>>(json);

            return drivers; 
        }

        public static void SaveCurrentDriverStates(List<Driver> drivers)
        {
            string json = JsonConvert.SerializeObject(drivers, Formatting.Indented);
            File.WriteAllText(@"C:\Users\fanoll\Source\Repos\burgerama-burger-shop\Burgerama Burger Shop App\src\data\driver_data.json", json);
        }
    }
}
