using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App
{   
    enum Size
    {
        S = 0,
        M = 1,
        L = 2,
        XL = 3,
        XXL = 4
    }
    interface IDrink
    {
        void IsOnIce();
    }
    interface IMerchandise
    {
        void SetSize(int input);
    }
    internal class Product : IDrink, IMerchandise
    {
        public int id;
        public string category;
        public int categoryId;
        public string name;
        public double price;
        public int prepTime;
        public Size size;

        public Product(int prodId, string prodCategory, string prodName, double prodPrice, int prodPrepTime, int prodCategoryId)
        {
            id = prodId;
            category = prodCategory;
            categoryId = prodCategoryId;
            name = prodName;
            price = prodPrice;
            prepTime = prodPrepTime;
        }
        
        void IDrink.IsOnIce()
        {
            bool onIce;         //true = with Ice,,false = without Ice
        }

        void IMerchandise.SetSize(int input)
        {
            //ask robin what we can simplify here
            if(input == 0)
            {
                size = Size.S;
            } else if (input == 1)
            {
                size = Size.M;
            } else if (input == 2)
            {
                size = Size.L;
            } else if (input == 3)
            {
                size = Size.XL;
            } else if (input == 4)
            {
                size = Size.XXL;
            }
        }                
    }
}
