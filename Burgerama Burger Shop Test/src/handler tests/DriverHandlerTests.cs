using Burgerama_Burger_Shop_App;
using Burgerama_Burger_Shop_App.src.handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.handler_tests
{
    public class DriverHandlerTests
    {
        [Theory]
        [InlineData("driver_data_test_free.json", "Lara Croft")]
        [InlineData("driver_data_test_no_free.json", "Nishal Bondeli")]
        public void GiveFreeDrivers_ReturnMostOpenOne(string testFile, string name)
        {
            var driverHandler = new DriverHandler("src/test data/driver data/", testFile,
                "driver_config_test.json");

            var driver = driverHandler.ReturnOpenDriver();

            Assert.Equal(name, driver.name);
        }

        [Fact]
        public void AddNewDriver()
        {
            DriverHandler driverHandler = new DriverHandler("src/test data/", "driver_data_test2.json", "driver_config_test.json");

            driverHandler.UpdateDrivers();

            Assert.Equal("Lea Ivosevic", driverHandler.drivers[3].name);
        }
    }
}
