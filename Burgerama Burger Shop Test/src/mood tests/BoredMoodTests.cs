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
            IMood mood = new BoredMood();

            var actualDeliveryTime = mood.CalculateDeliveryTime(initialDeliveryTime);

            Assert.Equal(expectedDeliveryTime, actualDeliveryTime);
        }

        [Fact]
        public void OrderComesIn_SwitchToMood()
        {
            IMood mood = new BoredMood();
            IMood expectedMood = new BalancedMood();
            int capacity = 5; int openOrders = 0;

            var actualMood = mood.SwitchToNextMood(capacity, openOrders);
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void SwitchToNextMood_AfterTimeCycle()
        {
            IMood mood = new BoredMood();
            IMood expectedMood = new BoredMood();

            int capacity = 5; int openOrders = 0;

            var actualMood = mood.SwitchToNextMoodTimeCycle(capacity, openOrders);
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }
    }
}
