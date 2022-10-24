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
        public int DeliveryTimeFactor { get; }
        public void SwitchToNextMood(int capacity, int openOrders)
        {
            throw new NotImplementedException();
        }

        public void SwitchToNextMoodTimeCycle(int capacity, int openOrders)
        {
            throw new NotImplementedException();
        }
    }
}
