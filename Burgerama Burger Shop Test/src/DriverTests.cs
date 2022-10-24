using Burgerama_Burger_Shop_App;
using Burgerama_Burger_Shop_App.src.handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src
{
    public class DriverTests
    {

        [Fact]
        public void IsDriverFreePass()
        {
            Driver driver = new Driver()
            {
                name = "Rosalin",
                capacity = 2
            };
            driver.orders.Add(null);

            var output = driver.IsDriverFree();

            Assert.True(output);
        }

        [Fact]
        public void IsDriverFreeFail()
        {
            Driver driver = new Driver()
            {
                name = "Rosalin",
                capacity = 2
            };
            driver.orders.Add(null);
            driver.orders.Add(null);

            var output = driver.IsDriverFree();

            Assert.False(output);
        }

        [Theory]
        [InlineData("Happy")]
        [InlineData("Bored")]
        [InlineData("Balanced")]
        [InlineData("Stressed")]
        [InlineData("Exhausted")]
        public void SetAndGetDriverMood(string mood)
        {
            Driver driver = new Driver()
            {
                name = "Rosalin",
                capacity = 2
            };

            driver.Mood = mood;

            Assert.Equal(mood, driver.Mood);
        }

        //tests to be added
        //Happy => 20% faster Delivery
        //Bored => 10% slower Delivery
        //Balanced => normal Delivery
        //Stressed => 35min Delivery
        //Exhausted => 45min Delivery
        [Theory]
        [InlineData("Happy", 16)]
        [InlineData("Bored", 22)]
        [InlineData("Balanced", 20)]
        [InlineData("Stressed", 35)]
        [InlineData("Exhausted", 45)]
        public void SetMood_CalculateTime(string mood, int expectedDeliveryTime)
        {
            var driver = new Driver();
            driver.Mood = mood;

            var actualDeliveryTime = driver.CalculateDeliveryTime();

            Assert.Equal(expectedDeliveryTime, actualDeliveryTime);
        }

        //test addordertodriver inclusive moodtest etc.
        // Happy to Balanced
        //Bored to Balanced
        //Balanced to stressed

        [Theory]
        [InlineData("Happy", "Balanced", 1, 2)]
        [InlineData("Bored", "Balanced", 1, 2)]
        [InlineData("Balanced", "Stressed", 2, 1)]
        public void SetMood_AddOrder_LookAtMood(string initialMood, string expectedMood, int addedOrders, int capacity)
        {
            var driver = new Driver()
            {
                capacity = capacity,
                openOrders = addedOrders,
                Mood = initialMood
            };

            driver.CalculateMood();

            Assert.Equal(expectedMood, driver.Mood);
        }

        //tests to be added functionality
        //time forward no Orders Happy => Bored
        //time forward nor Orders Balanced => Happy
        //time forward orders > capacity Stressed => Exhausted
        //time forward noReqs Exhausted => Stressed
        //time forward orders < capacity Stressed => Balanced
        [Theory]
        [InlineData("Happy", "Bored", 0, 2)]
        [InlineData("Balanced", "Happy", 0, 2)]
        [InlineData("Stressed", "Exhausted", 3, 2)]
        [InlineData("Exhausted", "Stressed", 3, 2)]
        [InlineData("Exhausted", "Stressed", 2, 2)]
        [InlineData("Stressed", "Balanced", 0, 2)]
        [InlineData("Stressed", "Balanced", 1, 2)]
        [InlineData("Stressed", "Balanced", 2, 2)]
        public void MoveTime_CalculateMood_LookAtMood(string initialMood, string expectedMood, int addedOrders, int capacity)
        {
            var driver = new Driver()
            {
                capacity = capacity,
                openOrders = addedOrders,
                Mood = initialMood
            };

            driver.CalculateMoodAfterTime();

            Assert.Equal(expectedMood, driver.Mood);
        }
    }
}
