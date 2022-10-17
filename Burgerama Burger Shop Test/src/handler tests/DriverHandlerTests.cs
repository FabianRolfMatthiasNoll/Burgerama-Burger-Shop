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
        [Fact]
        public void AddOrderToDriver()
        {
            DriverHandler driverHandler = new DriverHandler("src/test data/", "driver_data_test.json", "driver_config_test.json");
            Order order = new Order("src/test data/", "driver_data_test.json");
            driverHandler.LoadDriverStates();
            order = driverHandler.drivers[2].orders[2];

            driverHandler.AddOrderToBestDriver(order);
            driverHandler.LoadDriverStates();

            Assert.Equal(3, driverHandler.drivers[0].orders.Count);
        }

        [Fact]
        public void CountAllOpenOrders()
        {
            DriverHandler driverHandler = new DriverHandler("src/test data/", "driver_data_test.json", "driver_config_test.json");

            driverHandler.CountOpenOrders();

            Assert.Equal(1, driverHandler.drivers[0].openOrders);
            Assert.Equal(3, driverHandler.drivers[1].openOrders);
            Assert.Equal(3, driverHandler.drivers[2].openOrders);
        }

        [Fact]
        public void AddNewDriver()
        {
            DriverHandler driverHandler = new DriverHandler("src/test data/", "driver_data_test.json", "driver_config_test.json");

            driverHandler.UpdateDrivers();

            Assert.Equal("Lea Ivosevic", driverHandler.drivers[3].name);
        }


    }
}
