using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.products
{
    internal class Drink : Product
    {
        public bool onIce;
        
        public Drink(int prodId, string prodCategory, string prodName, double prodPrice, int prodPrepTime, int prodCategoryId, bool prodOnIce) 
                  : base(prodId, prodCategory, prodName, prodPrice, prodPrepTime, prodCategoryId)
        {
            id = prodId;
            category = prodCategory;
            categoryId = prodCategoryId;
            name = prodName;
            price = prodPrice;
            prepTime = prodPrepTime;
            onIce = prodOnIce;
        }

        public override void PrintSummaryInfo(int consoleRow)
        {
            Console.SetCursorPosition(0, consoleRow);
            Console.Write("(" + (consoleRow - 6) + ") ");
            Console.Write(name);
            if(onIce = true)
            {
                Console.Write(" (on Ice)");
            }
            Console.SetCursorPosition(40, consoleRow);
            Console.WriteLine(price + "$");
        }
    }
}
