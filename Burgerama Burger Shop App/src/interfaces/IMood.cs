using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.interfaces
{
    public interface IMood
    {
        public int DeliveryTimeFactor { get; }

        public void SwitchToNextMood(int capacity, int openOrders);

        public void SwitchToNextMoodTimeCycle(int capacity, int openOrders);
    }
}
