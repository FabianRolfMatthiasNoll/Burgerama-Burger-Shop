using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.interfaces
{
    public interface IMood
    {
        public int CalculateDeliveryTime(int deliveryTime);

        public IMood SwitchToNextMood(int capacity, int openOrders);

        public IMood SwitchToNextMoodTimeCycle(int capacity, int openOrders);
    }
}
