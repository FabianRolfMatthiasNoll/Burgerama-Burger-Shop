using Burgerama_Burger_Shop_App.src.validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.validator_tests
{
    public class BoolValidatorTests
    {
        [Theory]
        [InlineData("true")]
        [InlineData("false")]
        [InlineData("False")]
        [InlineData("TRUE")]
        [InlineData("fAlsE")]
        public void IsStringAValidBoolPass(string input)
        {
            BoolValidator boolValidator = new BoolValidator();
            bool output;

            output = boolValidator.IsValid(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData("10")]
        [InlineData("-10")]
        [InlineData("Hello")]
        [InlineData("")]
        [InlineData(" ")]
        public void IsStringAValidBoolFail(string input)
        {
            BoolValidator boolValidator = new BoolValidator();
            bool output;

            output = boolValidator.IsValid(input);

            Assert.False(output);
        }
    }
}
