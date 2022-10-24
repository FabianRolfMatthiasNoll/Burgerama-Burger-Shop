using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;
using Newtonsoft.Json;

namespace Burgerama_Burger_Shop_App.src.moods
{
    public class StressedMood : IMood
    {
        [JsonIgnore]
        public int DeliveryTime { get; set; }
        [JsonIgnore]
        public int Capacity { get; set; }
        [JsonIgnore]
        public int OpenOrders { get; set; }

        public string MoodName { get; set; }

        public StressedMood(int deliveryTime, int capacity, int openOrders)
        {
            DeliveryTime = deliveryTime;
            Capacity = capacity;
            OpenOrders = openOrders;
            MoodName = "Stressed";
        }

        public int CalculateDeliveryTime()
        {
            return this.DeliveryTime + 15;
        }

        public IMood SwitchToNextMood()
        {
            var mood = new StressedMood(this.DeliveryTime, this.Capacity, this.OpenOrders);
            return mood;
        }

        public IMood SwitchToNextMoodTimeCycle()
        {
            if (this.Capacity < this.OpenOrders)
            {
                var mood = new ExhaustedMood(this.DeliveryTime, this.Capacity, this.OpenOrders);
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
