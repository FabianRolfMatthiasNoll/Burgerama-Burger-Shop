using Burgerama_Burger_Shop_App.src.handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.handler_tests
{
    public class ManagmentHandlerTests
    {
        [Fact]
        public void MoveTimeForward()
        {
            ManagementHandler managementHandler = new ManagementHandler("src/test data/", "driver_data_test_time1.json");
            managementHandler.LoadDriverData();

            managementHandler.FastForwardTime();

            Assert.Equal(0, managementHandler.drivers[0].orders[0].prepTime);
            Assert.Equal(15, managementHandler.drivers[0].orders[0].shipTime);
            Assert.Equal(15, managementHandler.drivers[0].orders[0].totalTime);
            Assert.Equal(Burgerama_Burger_Shop_App.State.Delivery, managementHandler.drivers[0].orders[0].state);
        }

        [Fact]
        public void MoveTimeForwardTwice()
        {
            ManagementHandler managementHandler = new ManagementHandler("src/test data/", "driver_data_test_time2.json");
            managementHandler.LoadDriverData();

            managementHandler.FastForwardTime();

            Assert.Equal(0, managementHandler.drivers[0].orders[0].prepTime);
            Assert.Equal(0, managementHandler.drivers[0].orders[0].shipTime);
            Assert.Equal(0, managementHandler.drivers[0].orders[0].totalTime);
            Assert.Equal(Burgerama_Burger_Shop_App.State.Closed, managementHandler.drivers[0].orders[0].state);
        }

        [Fact]
        public void MoveTimeForwardThrice()
        {
            ManagementHandler managementHandler = new ManagementHandler("src/test data/", "driver_data_test_time3.json");
            managementHandler.LoadDriverData();

            managementHandler.FastForwardTime();

            Assert.Equal(0, managementHandler.drivers[0].orders[0].prepTime);
            Assert.Equal(0, managementHandler.drivers[0].orders[0].shipTime);
            Assert.Equal(0, managementHandler.drivers[0].orders[0].totalTime);
            Assert.Equal(Burgerama_Burger_Shop_App.State.Closed, managementHandler.drivers[0].orders[0].state);
        }

        [Fact]
        public void EraseAllClosedOrders()
        {
            ManagementHandler managementHandler = new ManagementHandler("src/test data/", "driver_data_test.json");
            managementHandler.LoadDriverData();

            int countOfClosedOrders = managementHandler.drivers[0].orders.Count;
            managementHandler.EraseClosedOrders();

            Assert.NotEqual(countOfClosedOrders, managementHandler.drivers[0].orders.Count);
            Assert.Equal(1, managementHandler.drivers[0].orders.Count);
        }
    }
}
