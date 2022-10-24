using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.moods
{
    public class BalancedMood : IMood
    {
        public int CalculateDeliveryTime(int deliveryTime)
        {
            return deliveryTime;
        }

        public IMood SwitchToNextMood(int capacity, int openOrders)
        {
            throw new NotImplementedException();
        }

        public IMood SwitchToNextMoodTimeCycle(int capacity, int openOrders)
        {
            throw new NotImplementedException();
        }
    }
}
