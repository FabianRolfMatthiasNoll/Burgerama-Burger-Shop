using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.moods
{
    public class BoredMood : IMood
    {
        public int CalculateDeliveryTime(int deliveryTime)
        {
            return (int)(deliveryTime * 1.1);
        }

        public IMood SwitchToNextMood(int capacity, int openOrders)
        {
            var mood = new BalancedMood();
            return mood;
        }

        public IMood SwitchToNextMoodTimeCycle(int capacity, int openOrders)
        {
            var mood = new BoredMood();
            return mood;
        }
    }
}
