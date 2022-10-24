using Burgerama_Burger_Shop_App.src.interfaces;
using Burgerama_Burger_Shop_App.src.moods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.mood_tests
{
    public class BoredMoodTests
    {
        [Theory]
        [InlineData(20, 22)]
        [InlineData(30, 33)]
        [InlineData(40, 44)]
        public void CalculateDeliveryTime(int initialDeliveryTime, int expectedDeliveryTime)
        {
            IMood mood = new BoredMood(initialDeliveryTime, 5, 0);

            var actualDeliveryTime = mood.CalculateDeliveryTime();

            Assert.Equal(expectedDeliveryTime, actualDeliveryTime);
        }

        [Fact]
        public void OrderComesIn_SwitchToMood()
        {
            IMood mood = new BoredMood(20, 5, 1);
            IMood expectedMood = new BalancedMood(20, 5, 1);

            var actualMood = mood.SwitchToNextMood();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void SwitchToNextMood_AfterTimeCycle()
        {
            IMood mood = new BoredMood(20,5,0);
            IMood expectedMood = new BoredMood(20,5,0);

            var actualMood = mood.SwitchToNextMoodTimeCycle();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }
    }
}
