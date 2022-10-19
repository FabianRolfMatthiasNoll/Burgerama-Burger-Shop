using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.src.interfaces
{
    interface IEmailValidator : IValidator
    {
        public bool IsEmailTaken(List<User> users, string emailInput);
    }
}
