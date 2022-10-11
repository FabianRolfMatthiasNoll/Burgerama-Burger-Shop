using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

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

        public override ConsoleTable PrintSummaryInfo(ConsoleTable table, int index)
        {
            if (onIce = true)
            {
                table.AddRow(index, name, "On Ice", price + "$");
            } else
            {
                table.AddRow(index, name, "", price + "$");
            }
            return table;
        }
    }
}
