using Burgerama_Burger_Shop_App.src.interfaces;
using Burgerama_Burger_Shop_App.src.moods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.mood_tests
{
    public class BalancedMoodTests
    {
        [Theory]
        [InlineData(20, 20)]
        [InlineData(16, 16)]
        [InlineData(12, 12)]
        public void CalculateDeliveryTime(int initialDeliveryTime, int expectedDeliveryTime)
        {
            IMood mood = new BalancedMood(initialDeliveryTime,5,0);

            var actualDeliveryTime = mood.CalculateDeliveryTime();

            Assert.Equal(expectedDeliveryTime, actualDeliveryTime);
        }

        [Fact]
        public void OrderComesIn_OrderInCapacity_SwitchToMood()
        {
            IMood mood = new BalancedMood(20,5,2);
            IMood expectedMood = new BalancedMood(20, 5, 2);

            var actualMood = mood.SwitchToNextMood();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void OrderComesIn_OrderOverCapacity_SwitchToMood()
        {
            IMood mood = new BalancedMood(20, 5, 6);
            IMood expectedMood = new StressedMood(20, 5, 6);

            var actualMood = mood.SwitchToNextMood();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void SwitchToNextMood_AfterTimeCycle_OrdersInCapacity()
        {
            IMood mood = new BalancedMood(20,5,5);
            IMood expectedMood = new BalancedMood(20,5,5);

            var actualMood = mood.SwitchToNextMoodTimeCycle();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void SwitchToNextMood_AfterTimeCycle_NoOrders()
        {
            IMood mood = new BalancedMood(10,5,0);
            IMood expectedMood = new HappyMood(10,5,0);

            var actualMood = mood.SwitchToNextMoodTimeCycle();
            var actualType = actualMood.GetType();
            var expectedType = expectedMood.GetType();

            Assert.Equal(expectedType, actualType);
        }
    }
}
