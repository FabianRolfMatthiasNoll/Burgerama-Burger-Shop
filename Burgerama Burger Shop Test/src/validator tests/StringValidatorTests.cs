using Burgerama_Burger_Shop_App.src.validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.validator_tests
{
    public class StringValidatorTests
    {
        [Theory]
        [InlineData("Hello")]
        [InlineData("Neuhausestraße 71")]
        public void Test_Empty_String_Pass(string input)
        {
            //ARRANGE
            StringValidator stringValidator = new StringValidator();
            bool output;

            //ACT
            output = stringValidator.IsValid(input);

            //ASSERT
            Assert.Equal(false, output);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        public void Test_Empty_String_Fail(string input)
        {
            //ARRANGE
            StringValidator stringValidator = new StringValidator();
            bool output;

            //ACT
            output = stringValidator.IsValid(input);

            //ASSERT
            Assert.Equal(true, output);
        }
    }
}
