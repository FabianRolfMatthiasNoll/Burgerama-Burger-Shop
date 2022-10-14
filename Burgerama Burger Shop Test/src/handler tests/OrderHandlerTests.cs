using Burgerama_Burger_Shop_App;
using Burgerama_Burger_Shop_App.products;
using Burgerama_Burger_Shop_App.src;
using Burgerama_Burger_Shop_App.src.handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.handler_tests
{
    public class OrderHandlerTests
    {
        //Test Exceptions
        //Test FinishOrder = only consists of submethods that will be testet later

        [Fact]
        public void DoesProductDataLoadCorrectly()
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            List<Product> products = new List<Product>();

            orderHandler.LoadProductData();
            products = orderHandler.products;

            Assert.Equal(130, products[0].id);
            Assert.Equal("Food", products[0].category);
            Assert.Equal("Cheeseburger", products[0].name);
        }

        [Fact]
        public void SaveProductInOrder()
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            orderHandler.products.Add(new Product(1, "Food", "Burger", 3.99, 66, 1));

            orderHandler.AddProductToOrder(0);

            Assert.Equal("Food", orderHandler.order.boughtProducts[0].category);
            Assert.Equal("Burger", orderHandler.order.boughtProducts[0].name);
        }

        [Fact]
        public void IsProductADrinkPass()
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            orderHandler.products.Add(new Product(1, "Drink", "Sprite", 3.99, 66, 1));
            bool output;

            output = orderHandler.IsProductDrink(0);

            Assert.True(output);
        }

        [Theory]
        [InlineData("Drink","Red Bull")]
        [InlineData("Food", "Burger")]
        public void IsProductADrinkFail(string category, string name)
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            orderHandler.products.Add(new Product(1, category, name, 3.99, 66, 1));
            bool output;

            output = orderHandler.IsProductDrink(0);

            Assert.False(output);
        }

        [Fact]
        public void IsDrinkSetOnIceTrue()
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            orderHandler.products.Add(new Product(1, "Drink", "Sprite", 3.99, 66, 1));
            Drink product = new Drink(1, "Drink", "Cola", 5.99, 10, 2, false);

            orderHandler.SetDrinkOnIce("true", 0);
            product = (Drink)orderHandler.products[0];

            Assert.Equal(true, product.onIce);
        }

        [Fact]
        public void IsProductMerchandisePass()
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            orderHandler.products.Add(new Product(1, "Merchandise (Clothing)", "Hoodie", 3.99, 66, 1));
            bool output;

            output = orderHandler.IsProductMerchandise(0);

            Assert.True(output);
        }

        [Fact]
        public void IsProductMerchandiseFail()
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            orderHandler.products.Add(new Product(1, "Merchandise (One Size)", "Stickers", 3.99, 66, 1));
            bool output;

            output = orderHandler.IsProductMerchandise(0);

            Assert.False(output);
        }

        [Theory]
        [InlineData("s")]
        [InlineData("l")]
        [InlineData("xl")]
        [InlineData("M")]
        [InlineData("XXL")]
        [InlineData("L")]
        [InlineData("S")]
        public void IsStringValidSizePass(string input)
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            bool output;

            output = orderHandler.SizeValidator(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData("10")]
        [InlineData("-10")]
        [InlineData("@")]
        [InlineData("-")]
        [InlineData("B")]
        [InlineData("b")]
        public void IsStringValidSizeFail(string input)
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            bool output;

            output = orderHandler.SizeValidator(input);

            Assert.False(output);
        }

        [Theory]
        [InlineData("s")]
        [InlineData("XL")]
        [InlineData("m")]
        [InlineData("XXL")]
        public void IsSizeOfMerchandiseSetPass(string input)
        {
            OrderHandler orderHandler = new OrderHandler("src/test data/", "product_data_test.json", "driver_data_test.json", "driver_config_test.json");
            orderHandler.products.Add(new Product(1, "Merchandise (Clothing)", "Hoodie", 3.99, 66, 1));
            Merchandise product = new Merchandise(1, "Merchandise (Clothing)", "Hoodie", 5.99, 10, 2);

            orderHandler.SetSizeOfProduct(input, 0);
            product = (Merchandise)orderHandler.products[0];

            Assert.Equal(input.ToUpper(), product.size);
        }
    }
}
