using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;
using Newtonsoft.Json;

namespace Burgerama_Burger_Shop_App.src.moods
{
    public class BalancedMood : IMood
    {
        [JsonIgnore]
        public int DeliveryTime { get; set; }
        [JsonIgnore]
        public int Capacity { get; set; }
        [JsonIgnore]
        public int OpenOrders { get; set; }

        public string MoodName { get; set; }

        public BalancedMood(int deliveryTime, int capacity, int openOrders)
        {
            DeliveryTime = deliveryTime;
            Capacity = capacity;
            OpenOrders = openOrders;
            MoodName = "Balanced";
        }

        public int CalculateDeliveryTime()
        {
            return this.DeliveryTime;
        }

        public IMood SwitchToNextMood()
        {
            if (this.Capacity > this.OpenOrders)
            {
                var mood = new BalancedMood(this.DeliveryTime,this.Capacity,this.OpenOrders);
                return mood;
            }
            else
            {
                var mood = new StressedMood(this.DeliveryTime, this.Capacity, this.OpenOrders);
                return mood;
            }
        }

        public IMood SwitchToNextMoodTimeCycle()
        {
            if (this.OpenOrders == 0)
            {
                var mood = new HappyMood(this.DeliveryTime, this.Capacity, this.OpenOrders);
                return mood;
            }
            else
            {
                var mood = new BalancedMood(this.DeliveryTime, this.Capacity, this.OpenOrders);
                return mood;
            }
        }
    }
}
