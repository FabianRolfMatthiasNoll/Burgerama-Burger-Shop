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
        public string MoodName { get; set; }

        public int CalculateDeliveryTime();

        public IMood SwitchToNextMood();

        public IMood SwitchToNextMoodTimeCycle();
    }
}
