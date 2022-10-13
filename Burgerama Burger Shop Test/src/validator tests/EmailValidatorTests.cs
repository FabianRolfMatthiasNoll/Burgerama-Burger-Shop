using Burgerama_Burger_Shop_App.src.validators;
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


    }
}
