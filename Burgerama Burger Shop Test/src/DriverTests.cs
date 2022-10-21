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
    }
}
