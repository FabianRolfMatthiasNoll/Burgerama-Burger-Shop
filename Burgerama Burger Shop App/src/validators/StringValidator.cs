using Burgerama_Burger_Shop_App.src.interfaces;

namespace Burgerama_Burger_Shop_App.src.validators
{
    public class StringValidator : IValidator
    {
        public StringValidator()
        {

        }

        public bool IsValid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }
    }
}
