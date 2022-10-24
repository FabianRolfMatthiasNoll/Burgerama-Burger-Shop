using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.interfaces
{
    public interface IMood
    {
        public string Mood { get; set; }

        public void CalculateMood();

        public void CalculateMoodAfterTime();
    }
}
