using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;
using Newtonsoft.Json;

namespace Burgerama_Burger_Shop_App.src.moods
{
    public class HappyMood : IMood
    {
        [JsonIgnore]
        public int DeliveryTime { get; set; }
        [JsonIgnore]
        public int Capacity { get; set; }
        [JsonIgnore]
        public int OpenOrders { get; set; }

        public string MoodName { get; set; }

        public HappyMood(int deliveryTime, int capacity, int openOrders)
        {
            DeliveryTime = deliveryTime;
            Capacity = capacity;
            OpenOrders = openOrders;
            MoodName = "Happy";
        }

        public int CalculateDeliveryTime()
        {
            return (int)(this.DeliveryTime * 0.8);
        }

        public IMood SwitchToNextMood()
        {
            var mood = new BalancedMood(this.DeliveryTime, this.Capacity, this.OpenOrders);
            return mood;
        }

        public IMood SwitchToNextMoodTimeCycle()
        {
            var mood = new BoredMood(this.DeliveryTime, this.Capacity, this.OpenOrders);
            return mood;
        }
    }
}
