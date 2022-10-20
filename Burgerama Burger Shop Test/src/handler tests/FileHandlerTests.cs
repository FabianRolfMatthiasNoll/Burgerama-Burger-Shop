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
    [Collection("Non-Parallel Collection")]
    public class FileHandlerTests
    {
        [Fact]
        public void DoesUserDataLoadCorrectly()
        {
            FileHandler fileHandler = new FileHandler("src/test data/");
            List<User> users = new List<User>();
            string email = "fabian.noll@googlemail.com";
            string password = "70E3418C7891EE54CB53731E1A5D7BBA092FC2C9CC7996B2045E66FACC2C9641";

            users = fileHandler.LoadUserData("user_data_test.xml");

            Assert.Equal(email, users[0].email);
            Assert.Equal(password, users[0].password);
        }

        [Fact]
        public void IsUserDataWrittenCorrect()
        {
            FileHandler fileHandler = new FileHandler("src/test data/");
            List<User> users = new List<User>();
            User user = new User();
            user.email = "fernando.rodriguez@spain.to";
            user.password = "encanto";
            user.street = "teststreet 1";
            user.postal = "00000";
            user.city = "testamonia";

            fileHandler.WriteUserData(user, "user_data_test.xml");
            users = fileHandler.LoadUserData("user_data_test.xml");

            Assert.Equal(user.email, users[7].email);
            Assert.Equal(user.password, users[7].password);
            Assert.Equal(user.street, users[7].street);
            Assert.Equal(user.postal, users[7].postal);
            Assert.Equal(user.city, users[7].city);
        }

        [Theory]
        [InlineData("driver_config_test.json")]
        [InlineData("driver_data_test.json")]
        [InlineData("german_cities_test.json")]
        [InlineData("product_data_test.json")]
        [InlineData("user_data_test.xml")]
        public void DataAvailablePass(string fileName)
        {
            FileHandler fileHandler = new FileHandler("src/test data/");

            var output = fileHandler.IsDataAvailable(fileName);

            Assert.True(output);
        }

        [Theory]
        [InlineData("random_file_that_doesnt_exist")]
        public void DataAvailableFail(string fileName)
        {
            FileHandler fileHandler = new FileHandler("src/test data/");

            var output = fileHandler.IsDataAvailable(fileName);

            Assert.False(output);
        }

        [Fact]
        public void DoesJsonLoadCorrectly()
        {
            FileHandler fileHandler = new FileHandler("src/test data/");

            var products = fileHandler.ReadJson<Product>("product_data_test.json");

            Assert.Equal("Cheeseburger", products[0].name);
        }

        [Fact]
        public void DoesJsonWriteCorrectly()
        {
            FileHandler fileHandler = new FileHandler("src/test data/");
            List<Product> products = new List<Product>();
            List<Product> newProducts = new List<Product>();
            products = fileHandler.ReadJson<Product>("product_data_test.json");

            fileHandler.WriteJson<Product>(products, "product_write_test.json");

            newProducts = fileHandler.ReadJson<Product>("product_write_test.json");
            Assert.Equal("Cheeseburger", newProducts[0].name);
        }
    }
}
