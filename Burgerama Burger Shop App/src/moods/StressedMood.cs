using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.moods
{
    public class StressedMood : IMood
    {
        public int CalculateDeliveryTime(int deliveryTime)
        {
            return deliveryTime + 15;
        }

        public IMood SwitchToNextMood(int capacity, int openOrders)
        {
            var mood = new StressedMood();
            return mood;
        }

        public IMood SwitchToNextMoodTimeCycle(int capacity, int openOrders)
        {
            if (capacity < openOrders)
            {
                var mood = new ExhaustedMood();
                return mood;
            }
            else
            {
                var mood = new BalancedMood();
                return mood;
            }
        }
    }
}
