using Burgerama_Burger_Shop_App;
using Burgerama_Burger_Shop_App.products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src
{
    [Collection("Non-Parallel Collection")]
    public class OrderTests
    {
        [Fact]
        public void CalculateOrderAttributesFoodAndAvailableDriver()
        {
            Order order = new Order("src/test data/", "driver_data_test.json");
            User user = new User();
            user.email = "TestEmail";
            order.boughtProducts.Add(new Product(1, "Food", "Burger", 4, 10, 1));
            order.boughtProducts.Add(new Product(1, "Food", "BurgerXL", 6, 15, 1));

            order.FillInformationInOrder(user);

            Assert.Equal(15, order.prepTime);
            Assert.Equal(State.Preperation, order.state);
            Assert.Equal(10, order.totalSum);
            Assert.Equal(20, order.shipTime);
            Assert.Equal(35, order.totalTime);
            Assert.Equal(user.email, order.customer.email);
        }
        
        [Fact]
        public void CalculateOrderAttributesFoodAndUnavailableDriver()
        {
            Order order = new Order("src/test data/", "driver_data_test_no_free.json");
            User user = new User();
            user.email = "TestEmail";
            order.boughtProducts.Add(new Product(1, "Food", "Burger", 4, 10, 1));
            order.boughtProducts.Add(new Product(1, "Food", "BurgerXL", 6, 15, 1));

            order.FillInformationInOrder(user);

            Assert.Equal(15, order.prepTime);
            Assert.Equal(State.Preperation, order.state);
            Assert.Equal(10, order.totalSum);
            Assert.Equal(35, order.shipTime);
            Assert.Equal(50, order.totalTime);
            Assert.Equal(user.email, order.customer.email);
        }

        [Fact]
        public void CalculateOrderAttributesMerchandiseAndAvailableDriver()
        {
            Order order = new Order("src/test data/", "driver_data_test.json");
            User user = new User();
            user.email = "TestEmail";
            order.boughtProducts.Add(new Product(1, "Merchandise", "Burger", 4, 0, 1));
            order.boughtProducts.Add(new Product(1, "Merchandise", "BurgerXL", 6, 0, 1));

            order.FillInformationInOrder(user);

            Assert.Equal(0, order.prepTime);
            Assert.Equal(State.Delivery, order.state);
            Assert.Equal(10, order.totalSum);
            Assert.Equal(20, order.shipTime);
            Assert.Equal(20, order.totalTime);
            Assert.Equal(user.email, order.customer.email);
        }

        [Fact]
        public void CalculateOrderAttributesMerchandiseAndUnavailableDriver()
        {
            Order order = new Order("src/test data/", "driver_data_test_no_free.json");
            User user = new User();
            user.email = "TestEmail";
            order.boughtProducts.Add(new Product(1, "Merchandise", "Burger", 4, 0, 1));
            order.boughtProducts.Add(new Product(1, "Merchandise", "BurgerXL", 6, 0, 1));

            order.FillInformationInOrder(user);

            Assert.Equal(0, order.prepTime);
            Assert.Equal(State.Delivery, order.state);
            Assert.Equal(10, order.totalSum);
            Assert.Equal(35, order.shipTime);
            Assert.Equal(35, order.totalTime);
            Assert.Equal(user.email, order.customer.email);
        }
        
        //decrese total time is tested in managmantHandler but still to be added
    }
}
