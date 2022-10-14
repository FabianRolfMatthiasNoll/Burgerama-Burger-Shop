using System;
using System.Collections.Generic;
using ConsoleTables;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.products;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class OrderHandler
    {
        FileHandler fileHandler;
        DriverHandler driverHandler;
        List<Product> unsortedProducts;
        public List<Product> products;
        public Order order;
        public string fileNameProduct;

        public OrderHandler(string filePath, string fileNameP, string fileNameDStates, string fileNameDConfig)
        {
            fileNameProduct = fileNameP;
            products = new List<Product>();
            order = new Order(filePath,  fileNameDStates);
            fileHandler = new FileHandler(filePath);
            driverHandler = new DriverHandler(filePath, fileNameDStates, fileNameDConfig);
            unsortedProducts = new List<Product>();
        }

        public void LoadProductData()
        {
            unsortedProducts = fileHandler.ReadJSON<Product>(fileNameProduct);
            products = unsortedProducts.OrderBy(o => o.categoryId).ToList();
        }

        public ConsoleTable CreateProductOverview()
        {
            var table = new ConsoleTable("ID", "Name", "Category", "Price");

            int index = 1;
            foreach (var product in products)
            {
                table.AddRow(index, product.name, product.category, product.price);
                index++;
            }

            table.AddRow(index, "Place Order", "", "");
            return table;
        }

        public ConsoleTable CreateSummaryOverview()
        {
            var table = new ConsoleTable("Pos.", "Name", "Variant", "Price");
            int index = 1;

            foreach (var product in order.boughtProducts)
            {
                table = product.PrintSummaryInfo(table, index);
                index++;
            }
            table.AddRow(index, "US Tax", "", Math.Round(order.totalSum * 0.0884, 2) + "$");
            return table;
        }

        public int GetProductCount()
        {
            LoadProductData();
            return unsortedProducts.Count;
        }

        public void AddProductToOrder(int index)
        {
            order.boughtProducts.Add(products[index]);
        }

        public void FinishOrder(User user)
        {
            order.FillInformationInOrder(user);
            driverHandler.AddOrderToDriver(order);
        }

        public bool IsProductDrink(int index)
        {
            if (products[index].category == "Drink" && !(products[index].name == "Red Bull"))
            {
                    return true;
            }
            return false;
        }

        public void SetDrinkOnIce(string input, int index)
        {
            if(input == "true")
            {
                Product drinkIce = new Drink(products[index].id,
                                             products[index].category,
                                             products[index].name,
                                             products[index].price,
                                             products[index].prepTime,
                                             products[index].categoryId,
                                             true);
                products[index] = drinkIce;
            } 
        }

        public bool IsProductMerchandise(int index)
        {
            if (products[index].category == "Merchandise (Clothing)")
            {
                return true;
            }
            return false;
        }

        public bool SizeValidator(string input)
        {
            input = input.ToUpper();
            return new[] { "S", "M", "L", "XL", "XXL" }.Contains(input);
        }

        public void SetSizeOfProduct(string input, int index)
        {
            input = input.ToUpper();
            Product merch = new Merchandise(products[index].id,
                                            products[index].category,
                                            products[index].name,
                                            products[index].price,
                                            products[index].prepTime,
                                            products[index].categoryId);
            merch.SetSize(input);
            products[index] = merch;
        }
    }
}
