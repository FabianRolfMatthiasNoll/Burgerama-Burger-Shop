using Burgerama_Burger_Shop_App.products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App
{
    public class Driver
    {
        public string name;
        public int capacity;
        public int openOrders = 0;
        public List<Order> orders;

        public Driver(string inName, int inCapacity)
        {
            name = inName;
            capacity = inCapacity;
            orders = new List<Order>();
        }

        public bool IsDriverFree()
        {
            if(capacity > orders.Count)
            {
                return true;
            }
            return false;
        }
    }
}
