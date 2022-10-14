using Burgerama_Burger_Shop_App;
using Burgerama_Burger_Shop_App.src;
using Burgerama_Burger_Shop_App.src.handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.handler_tests
{
    public class RegistrationHandlerTests
    {
        [Fact]
        public void DoesDataLoadCorrectly()
        {
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");

            registrationHandler.LoadRegistrationData("user_data_test.xml", "german_cities_test.json");

            Assert.Equal("Berlin", registrationHandler.germanCities[0].city);
            Assert.Equal("fabian.noll@googlemail.com", registrationHandler.users[0].email);  
        }

        [Theory]
        [InlineData("Herman.Sigfried@ks.de")]
        [InlineData("Joachim.Löw@germany.tu")]
        [InlineData("p.hub@thehub.com")]
        public void EmailIsValidAndSetPass(string email)
        {
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");
            registrationHandler.LoadRegistrationData("user_data_test.xml", "german_cities_test.json");
            bool output;

            output = registrationHandler.SetEmailIfValid(email);
            email = email.ToLower();

            Assert.True(output);
            Assert.Equal(email, registrationHandler.user.email);
        }

        [Theory]
        [InlineData("HelloFresh")]
        [InlineData("fabian.noll@googlemail.com")]
        [InlineData(" ")]
        [InlineData("")]
        public void EmailIsValidAndSetFail(string email)
        {
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");
            registrationHandler.LoadRegistrationData("user_data_test.xml", "german_cities_test.json");

            bool output;

            output = registrationHandler.SetEmailIfValid(email);
            email = email.ToLower();

            Assert.False(output);
            Assert.NotEqual(email, registrationHandler.user.email);
        }

        [Theory]
        [InlineData("Neuhauserstraße 71")]
        [InlineData("Dr. Karl Storz Straße 34")]
        [InlineData("Rundes Eck 7")]
        public void SetStreetIfValidPass(string street)
        {
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");
            bool output;

            output = registrationHandler.SetStreetIfValid(street);

            Assert.True(output);
            Assert.Equal(street, registrationHandler.user.street);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("-100")]
        public void SetStreetIfValidFail(string street)
        {
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");
            bool output;

            output = registrationHandler.SetStreetIfValid(street);

            Assert.False(output);
            Assert.NotEqual(street, registrationHandler.user.street);
        }

        [Theory]
        [InlineData("78532")]
        [InlineData("39775")]
        public void SetZIPIfValidPass(string postal)
        {
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");
            bool output;

            output = registrationHandler.SetZIPIfValid(postal);

            Assert.True(output);
            Assert.Equal(postal, registrationHandler.user.postal);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("-100")]
        [InlineData("Rundes Eck 7")]
        public void SetZIPIfValidFail(string postal)
        {
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");
            bool output;

            output = registrationHandler.SetZIPIfValid(postal);

            Assert.False(output);
            Assert.NotEqual(postal, registrationHandler.user.postal);
        }

        [Fact]
        public void IsUserRegisteredCorrect()
        {
            FileHandler fileHandler = new FileHandler("src/test data/");
            RegistrationHandler registrationHandler = new RegistrationHandler("src/test data/");
            registrationHandler.user.email = "fernando.rodriguez@spain.to";
            registrationHandler.user.password = "encanto";
            registrationHandler.user.street = "teststreet 1";
            registrationHandler.user.postal = "00000";
            registrationHandler.user.city = "testamonia";
            List<User> users = new List<User>();

            registrationHandler.RegisterUser("user_data_test.xml");
            users = fileHandler.LoadUserData("user_data_test.xml");


            Assert.Equal(registrationHandler.user.email, users[7].email);
            Assert.Equal(registrationHandler.user.password, users[7].password);
            Assert.Equal(registrationHandler.user.street, users[7].street);
            Assert.Equal(registrationHandler.user.postal, users[7].postal);
            Assert.Equal(registrationHandler.user.city, users[7].city);
        }
    }
}
