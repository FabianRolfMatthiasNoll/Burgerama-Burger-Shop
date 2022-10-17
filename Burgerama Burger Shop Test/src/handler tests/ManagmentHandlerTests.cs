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
            ManagmentHandler managmentHandler = new ManagmentHandler("src/test data/", "driver_data_test_time1.json");
            managmentHandler.LoadDriverData();

            managmentHandler.FastForwardTime();

            Assert.Equal(0, managmentHandler.drivers[0].orders[0].prepTime);
            Assert.Equal(15, managmentHandler.drivers[0].orders[0].shipTime);
            Assert.Equal(15, managmentHandler.drivers[0].orders[0].totalTime);
            Assert.Equal(Burgerama_Burger_Shop_App.State.Delivery, managmentHandler.drivers[0].orders[0].state);
        }

        [Fact]
        public void MoveTimeForwardTwice()
        {
            ManagmentHandler managmentHandler = new ManagmentHandler("src/test data/", "driver_data_test_time2.json");
            managmentHandler.LoadDriverData();

            managmentHandler.FastForwardTime();

            Assert.Equal(0, managmentHandler.drivers[0].orders[0].prepTime);
            Assert.Equal(0, managmentHandler.drivers[0].orders[0].shipTime);
            Assert.Equal(0, managmentHandler.drivers[0].orders[0].totalTime);
            Assert.Equal(Burgerama_Burger_Shop_App.State.Closed, managmentHandler.drivers[0].orders[0].state);
        }

        [Fact]
        public void MoveTimeForwardThrice()
        {
            ManagmentHandler managmentHandler = new ManagmentHandler("src/test data/", "driver_data_test_time3.json");
            managmentHandler.LoadDriverData();

            managmentHandler.FastForwardTime();

            Assert.Equal(0, managmentHandler.drivers[0].orders[0].prepTime);
            Assert.Equal(0, managmentHandler.drivers[0].orders[0].shipTime);
            Assert.Equal(0, managmentHandler.drivers[0].orders[0].totalTime);
            Assert.Equal(Burgerama_Burger_Shop_App.State.Closed, managmentHandler.drivers[0].orders[0].state);
        }

        [Fact]
        public void EraseAllClosedOrders()
        {
            ManagmentHandler managmentHandler = new ManagmentHandler("src/test data/", "driver_data_test.json");
            managmentHandler.LoadDriverData();

            int countOfClosedOrders = managmentHandler.drivers[0].orders.Count;
            managmentHandler.EraseClosedOrders();

            Assert.NotEqual(countOfClosedOrders, managmentHandler.drivers[0].orders.Count);
            Assert.Equal(1, managmentHandler.drivers[0].orders.Count);
        }
    }
}
