using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.interfaces
{
    interface IIntValidator : IValidator
    {
        public bool IsValid(int input);
        public bool IsInputInt();
        public bool IsInputInt(string input);
        public bool IsIntPositiv(string input);
        public bool IsIntPositiv(int input);
        public bool IsInputInBound();
    }
}
