using Burgerama_Burger_Shop_App;
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
            Driver driver = new Driver("Rosalin", 2);
            driver.orders.Add(null);
            bool output;

            output = driver.IsDriverFree();

            Assert.True(output);
        }

        [Fact]
        public void IsDriverFreeFail()
        {
            Driver driver = new Driver("Rosalin", 2);
            driver.orders.Add(null);
            driver.orders.Add(null);
            bool output;

            output = driver.IsDriverFree();

            Assert.False(output);
        }
    }
}
