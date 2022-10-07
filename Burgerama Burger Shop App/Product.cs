using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App
{
    internal class Product
    {
        public int id;
        public string category;
        public int categoryId;
        public string name;
        public double price;
        public int prepTime;

        public string size; //(S,M,L,XL,XXL)
        bool onIce;         //true = with Ice,,false = without Ice


        public Product(int prodId, string prodCategory, string prodName, double prodPrice, int prodPrepTime, int prodCategoryId)
        {
            id = prodId;
            category = prodCategory;
            categoryId = prodCategoryId;
            name = prodName;
            price = prodPrice;
            prepTime = prodPrepTime;
        }
    }
}
