using Burgerama_Burger_Shop_App.src.interfaces;
using Burgerama_Burger_Shop_App.src.moods;

namespace Burgerama_Burger_Shop_App
{
    public class Driver
    {
        public string name;
        public int capacity;
        public int openOrders = 0;
        public IMood mood = new HappyMood(20,5,0);
        public List<Order> orders;
        public int workLoad;

        public Driver()
        {
            orders = new List<Order>();
        }

        public void CalculateWorkLoad()
        {
            workLoad = capacity - openOrders;
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
            return this.mood.CalculateDeliveryTime();
        }
    }
}
