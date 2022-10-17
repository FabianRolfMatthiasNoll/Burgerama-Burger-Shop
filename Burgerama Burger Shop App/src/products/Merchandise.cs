using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.products
{
    [ExcludeFromCodeCoverage]
    public class Merchandise : Product
    {
        public string size;

        public Merchandise(int prodId, string prodCategory, string prodName, double prodPrice, int prodPrepTime, int prodCategoryId) : base(prodId, prodCategory, prodName, prodPrice, prodPrepTime, prodCategoryId)
        {
            id = prodId;
            category = prodCategory;
            categoryId = prodCategoryId;
            name = prodName;
            price = prodPrice;
            prepTime = prodPrepTime;
        }

        public override void SetSize(string inputSize)
        {
            size = inputSize;
        }

        public override ConsoleTable PrintSummaryInfo(ConsoleTable table, int index)
        {
            table.AddRow(index, name, size, price + "$");
            return table;
        }
    }
}
