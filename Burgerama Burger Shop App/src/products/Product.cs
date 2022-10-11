using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.products
{
    internal class Product
    {
        public int id;
        public string category;
        public int categoryId;
        public string name;
        public double price;
        public int prepTime;

        public Product(int prodId, string prodCategory, string prodName, double prodPrice, int prodPrepTime, int prodCategoryId)
        {
            id = prodId;
            category = prodCategory;
            categoryId = prodCategoryId;
            name = prodName;
            price = prodPrice;
            prepTime = prodPrepTime;
        }

        public virtual ConsoleTable PrintSummaryInfo(ConsoleTable table, int index)
        {
            table.AddRow(index, name, "", price + "$");
            return table;
        }

        public static void FillProductData()
        {
            Product product = new Product(130, "Food", "Cheeseburger", 7.99, 5, 1);
            Product product2 = new Product(89, "Food", "Classic Burger", 9.90, 12, 1);
            Product product3 = new Product(223, "Drink", "Coca Cola", 2.99, 2, 2);
            Product product4 = new Product(33, "Drink", "Sprite", 2.99, 2, 2);
            Product product5 = new Product(23, "Food", "Burgerama Special", 16.99, 16, 1);
            Product product6 = new Product(666, "Food", "Red Devil", 12.99, 14, 1);
            Product product7 = new Product(43, "Drink", "Red Bull", 3.90, 12, 2);
            Product product8 = new Product(91, "Merchandise (Clothing)", "T-Shirt Burgerama", 12.89, 0, 3);
            Product product9 = new Product(4, "Merchandise (One Size)", "Snapback", 7.90, 0, 4);
            Product product10 = new Product(273, "Merchandise (Clothing)", "Hoodie", 29.80, 0, 3);
            Product product11 = new Product(54, "Food", "French Fries", 3.99, 5, 1);
            Product product12 = new Product(387, "Merchandise (One Size)", "Stickers", 0.99, 0, 4);

            ArrayList products = new ArrayList();

            /* TODO Make this a 3 liner instead every product singular
            for(int i = 0; i < 11; i++)
            {
                products.Add(product+ToString(i));
            }
            */

            products.Add(product);
            products.Add(product2);
            products.Add(product3);
            products.Add(product4);
            products.Add(product5);
            products.Add(product6);
            products.Add(product7);
            products.Add(product8);
            products.Add(product9);
            products.Add(product10);
            products.Add(product11);
            products.Add(product12);

            //serializes the product data to json
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            //write serialized json to file
            File.WriteAllText(@"C:\Users\fanoll\Source\Repos\burgerama-burger-shop\Burgerama Burger Shop App\src\data\product_data.json", json);
        }

        public virtual void SetSize(string inputSize)
        {

        }
    }
}
