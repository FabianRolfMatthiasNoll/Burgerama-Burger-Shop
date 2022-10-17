using Burgerama_Burger_Shop_App.src.validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.validator_tests
{
    public class PasswordValidatorTests
    {
        [Fact]
        public void StringToHash()
        {
            PasswordValidator passwordValidator = new PasswordValidator();
            string password = "KST";
            string hash = "69CA6781A5A3FD9E6C06381EFCA699F13E947678B04E7EC976B5D52290AB8F33";
            string output;

            output = passwordValidator.HashString(password);

            Assert.Equal(hash, output);
        }

        [Fact]
        public void StringToHashFail()
        {
            PasswordValidator passwordValidator = new PasswordValidator();
            string password = "";
            string output;

            output = passwordValidator.HashString(password);

            Assert.Equal("", output);
        }
    }
}
