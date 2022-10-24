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
            if (capacity > openOrders)
            {
                var mood = new BalancedMood();
                return mood;
            }
            else
            {
                var mood = new StressedMood();
                return mood;
            }
        }

        public IMood SwitchToNextMoodTimeCycle(int capacity, int openOrders)
        {
            if (openOrders == 0)
            {
                var mood = new HappyMood();
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
