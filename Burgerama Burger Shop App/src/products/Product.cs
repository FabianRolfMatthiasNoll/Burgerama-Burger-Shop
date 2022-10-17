using Burgerama_Burger_Shop_App.src.handlers;
using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App.products
{
    [ExcludeFromCodeCoverage]
    public class Product
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

        public static void CheckProductID()
        {
            FileHandler fileHandler = new FileHandler("src/data/");
            List<Product> productList = fileHandler.ReadJSON<Product>("product_data.json");

            foreach(var product1 in productList)
            {
                foreach(var product2 in productList)
                {
                    if(product1.id == product2.id && product1.name != product2.name)
                    {
                        Console.Clear();
                        Console.WriteLine("[ERROR] Product Initialization failed [CAUSE] Duplicate IDs found");
                        Console.WriteLine("[SOLUTION] fix multiple IDs and restart application");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }
        }

        public virtual void SetSize(string inputSize)
        {
            //not a empty method. needed to overwrite in merchandis.cs
        }
    }
}
