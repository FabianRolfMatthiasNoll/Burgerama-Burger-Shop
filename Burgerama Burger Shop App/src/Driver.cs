using Burgerama_Burger_Shop_App.products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;
using Burgerama_Burger_Shop_App.src.moods;

namespace Burgerama_Burger_Shop_App
{
    public class Driver
    {
        public string name;
        public int capacity;
        public int openOrders = 0;
        public List<Order> orders;
        public IMood mood = new HappyMood();

        public Driver()
        {
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

        public void CountOpenOrders()
        {
            var countedOrders = 0;
            foreach (var order in orders)
            {
                if (order.state != State.Closed)
                {
                    countedOrders++;
                }
            }

            openOrders = countedOrders;
        }

        public int CalculateDeliveryTime()
        {
            //here will be the mood feature added
            if (capacity > openOrders)
            {
                return 20;
            }
            return 35;
        }
    }
}
