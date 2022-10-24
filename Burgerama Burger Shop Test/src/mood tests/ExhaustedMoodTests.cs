using Burgerama_Burger_Shop_App.src.interfaces;
using Burgerama_Burger_Shop_App.src.moods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.mood_tests
{
    public class ExhaustedMoodTests
    {
        [Theory]
        [InlineData(20, 45)]
        [InlineData(16, 41)]
        [InlineData(12, 37)]
        public void CalculateDeliveryTime(int initialDeliveryTime, int expectedDeliveryTime)
        {
            IMood mood = new ExhaustedMood();

            var actualDeliveryTime = mood.CalculateDeliveryTime(initialDeliveryTime);

            Assert.Equal(expectedDeliveryTime, actualDeliveryTime);
        }

        [Fact]
        public void OrderComesIn_OrderOverCapacity_SwitchToMood()
        {
            IMood mood = new ExhaustedMood();
            IMood expectedMood = new ExhaustedMood();
            int capacity = 5;
            int openOrders = 5;
            openOrders++;

            var actualMood = mood.SwitchToNextMood(capacity, openOrders);
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void SwitchToNextMood_AfterTimeCycle_OrdersInCapacity()
        {
            IMood mood = new ExhaustedMood();
            IMood expectedMood = new StressedMood();
            int capacity = 5;
            int openOrders = 5;

            var actualMood = mood.SwitchToNextMoodTimeCycle(capacity, openOrders);
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void SwitchToNextMood_AfterTimeCycle_OrdersStillOverCapacity()
        {
            IMood mood = new ExhaustedMood();
            IMood expectedMood = new StressedMood();
            int capacity = 5;
            int openOrders = 6;

            var actualMood = mood.SwitchToNextMoodTimeCycle(capacity, openOrders);
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }
    }
}
