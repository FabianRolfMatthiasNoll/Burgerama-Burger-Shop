using Burgerama_Burger_Shop_App.src.handlers;
using Burgerama_Burger_Shop_App.src.validators;
using Burgerama_Burger_Shop_App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.validator_tests
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("Fabian.Noll@gmail.com")]
        [InlineData("storz@ks.de")]
        [InlineData("fn@j.de")]
        public void IsEmailValidPass(string input)
        {
            EmailValidator emailValidator = new EmailValidator();
            bool output;

            output = emailValidator.IsEmailValid(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData("Fabian.Noll@")]
        [InlineData("@ks.de")]
        [InlineData("BurgerSindScheiße")]
        [InlineData("")]
        [InlineData(" ")]
        public void IsEmailValidFail(string input)
        {
            EmailValidator emailValidator = new EmailValidator();
            bool output;

            output = emailValidator.IsEmailValid(input);

            Assert.False(output);
        }

        [Theory]
        [InlineData("Fabian.Noll@gmail.com")]
        [InlineData("Florian.Armbruster@gmx.de")]
        [InlineData("HeiNRich.SumMEr@gmx.de")]
        [InlineData("Dullenkopf.christian@siemens.de")]
        public void IsEmailTakenPass(string input)
        {
            FileHandler fileHandler = new FileHandler("src/test data/");
            EmailValidator emailValidator = new EmailValidator();
            List<User> userlist = fileHandler.LoadUserData("user_data_test.xml");
            bool output;

            output = emailValidator.IsEmailTaken(userlist, input);

            Assert.True(output);
        }

        [Theory]
        [InlineData("Fabian.Noll@goaglemail.com")]
        [InlineData("Nigerian.Prince@money.de")]
        [InlineData("")]
        [InlineData(" ")]
        public void IsEmailTakenFail(string input)
        {
            FileHandler fileHandler = new FileHandler("src/test data/");
            EmailValidator emailValidator = new EmailValidator();
            List<User> userlist = fileHandler.LoadUserData("user_data_test.xml");
            bool output;

            output = emailValidator.IsEmailTaken(userlist, input);

            Assert.False(output);
        }


    }
}
