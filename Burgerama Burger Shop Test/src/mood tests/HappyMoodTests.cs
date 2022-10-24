using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.src.interfaces;
using Burgerama_Burger_Shop_App.src.moods;

namespace Burgerama_Burger_Shop_Test.src.mood_tests
{
    public class HappyMoodTests
    {
        [Theory]
        [InlineData(25, 20)]
        [InlineData(20, 16)]
        [InlineData(15, 12)]
        public void CalculateDeliveryTime(int initialDeliveryTime, int expectedDeliveryTime)
        {
            IMood mood = new HappyMood(initialDeliveryTime, 5,0);

            var actualDeliveryTime = mood.CalculateDeliveryTime();

            Assert.Equal(expectedDeliveryTime, actualDeliveryTime);
        }

        [Fact]
        public void OrderComesIn_SwitchToMood()
        {
            IMood mood = new HappyMood(20, 5,1);
            IMood expectedMood = new BalancedMood(20,5,1);

            IMood actualMood = mood.SwitchToNextMood();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType,actualType);
        }

        [Fact]
        public void SwitchToNextMood_AfterTimeCycle()
        {
            IMood mood = new HappyMood(20, 5, 0);
            IMood expectedMood = new BoredMood(20,5,0);

            var actualMood = mood.SwitchToNextMoodTimeCycle();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }
    }
}
