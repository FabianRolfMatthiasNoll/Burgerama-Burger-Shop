using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.products
{

    internal class Merchandise : Product
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

        public override void PrintSummaryInfo(int consoleRow)
        {
            Console.SetCursorPosition(0, consoleRow);
            Console.Write("(" + (consoleRow - 6) + ") ");
            Console.Write(name);
            Console.Write(" " + size);
            Console.SetCursorPosition(40, consoleRow);
            Console.WriteLine(price + "$");
        }
    }
}
