using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Burgerama_Burger_Shop_App.src.interfaces
{
    public interface IMood
    {
        [JsonIgnore]
        public int DeliveryTime { get; set; }
        [JsonIgnore]
        public int Capacity { get; set; }
        [JsonIgnore]
        public int OpenOrders { get; set; }

        public string MoodName { get; set; }

        public int CalculateDeliveryTime();

        public IMood SwitchToNextMood();

        public IMood SwitchToNextMoodTimeCycle();
    }
}
