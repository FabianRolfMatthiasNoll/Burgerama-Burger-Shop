using Burgerama_Burger_Shop_App.src.validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.validator_tests
{
    public class IntValidatorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void IsIntValidPass(int input)
        {
            //submethod isInputinBound is tested within here
            IntValidator intValidator = new IntValidator(1, 3);
            bool output;

            output = intValidator.IsInputValid(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(5)]
        public void IsIntValidFail(int input)
        {
            IntValidator intValidator = new IntValidator(1, 3);
            bool output;

            output = intValidator.IsInputValid(input);

            Assert.False(output);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public void IsStringValidPass(string input)
        {
            IntValidator intValidator = new IntValidator(1, 3);
            bool output;

            output = intValidator.IsInputValid(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("0")]
        [InlineData("5")]
        [InlineData("a")]
        [InlineData("hello")]
        public void IsStringValidFail(string input)
        {
            IntValidator intValidator = new IntValidator(1, 3);
            bool output;

            output = intValidator.IsInputValid(input);

            Assert.False(output);
        }

        [Theory]
        [InlineData("3")]
        [InlineData("-10")]
        [InlineData("0")]
        public void IsStringAnIntPass(string input)
        {
            IntValidator intValidator = new IntValidator(0, 0);
            bool output;

            output = intValidator.IsInputInt(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData("Hallo")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A1")]
        public void IsStringAnIntFail(string input)
        {
            IntValidator intValidator = new IntValidator(0, 0);
            bool output;

            output = intValidator.IsInputInt(input);

            Assert.False(output);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(3.7)]
        public void IsIntPositivPass(int input)
        {
            IntValidator intValidator = new IntValidator(0, 0);
            bool output;

            output = intValidator.IsIntPositiv(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(-3.4)]
        public void IsIntPositivFail(int input)
        {
            IntValidator intValidator = new IntValidator(0, 0);
            bool output;

            output = intValidator.IsIntPositiv(input);

            Assert.False(output);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("5")]
        public void IsStringPositivPass(string input)
        {
            IntValidator intValidator = new IntValidator(0, 0);
            bool output;

            output = intValidator.IsIntPositiv(input);

            Assert.True(output);
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("-5")]
        [InlineData("-3.4")]
        [InlineData("3.7")]
        public void IsStringPositivFail(string input)
        {
            IntValidator intValidator = new IntValidator(0, 0);
            bool output;

            output = intValidator.IsIntPositiv(input);

            Assert.False(output);
        }
    }
}
