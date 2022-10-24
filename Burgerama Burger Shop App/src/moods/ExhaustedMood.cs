using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.moods
{
    public class ExhaustedMood : IMood
    {
        public int CalculateDeliveryTime(int deliveryTime)
        {
            return deliveryTime + 25;
        }

        public IMood SwitchToNextMood(int capacity, int openOrders)
        {
            var mood = new ExhaustedMood();
            return mood;
        }

        public IMood SwitchToNextMoodTimeCycle(int capacity, int openOrders)
        {
            var mood = new StressedMood();
            return mood;
        }
    }
}
