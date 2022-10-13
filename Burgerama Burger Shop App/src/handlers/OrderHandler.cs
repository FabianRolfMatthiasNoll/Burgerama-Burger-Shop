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
        List<Product> products;
        public Order order;

        public OrderHandler()
        {
            order = new Order();
            fileHandler = new FileHandler("src/data/");
            driverHandler = new DriverHandler();
            unsortedProducts = new List<Product>();
        }

        public void LoadProductData()
        {
            unsortedProducts = fileHandler.ReadJSON<Product>("product_data.json");
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

        public void AddProductToOrder(int selection)
        {
            products[selection] = CheckIfDrink(products[selection]);

            products[selection] = CheckIfMerchandise(products[selection]);

            order.boughtProducts.Add(products[selection]);
        }

        public void FinishOrder(User user)
        {
            order.FillInformationInOrder(user);
            driverHandler.AddOrderToDriver(order);
        }

        Product CheckIfDrink(Product product)
        {
            if (product.category == "Drink")
            {
                if (product.name == "Red Bull")
                {
                    return product;
                }
                product = IsDrinkOnIce(product);
            }
            return product;
        }

        Product IsDrinkOnIce(Product product)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Program.ClearCurrentConsoleLine();
            Console.Write("Please choose 'true' or 'false' if you want your Drink on Ice: ");
            string input = Console.ReadLine();
            string finput = input.ToLower();
            while (true)
            {
                if (finput == "true")
                {
                    Product drinkIce = new Drink(product.id,
                                              product.category,
                                              product.name,
                                              product.price,
                                              product.prepTime,
                                              product.categoryId,
                                              true);

                    Program.ClearCurrentConsoleLine();
                    return drinkIce;
                }
                else if (finput == "false")
                {
                    Product drink = new Drink(product.id,
                                              product.category,
                                              product.name,
                                              product.price,
                                              product.prepTime,
                                              product.categoryId,
                                              false);

                    Program.ClearCurrentConsoleLine();
                    return drink;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please choose 'true' or 'false' if you want your Drink on Ice: ");
                    input = Console.ReadLine();
                    finput = input.ToLower();
                }
            }
        }

        Product CheckIfMerchandise(Product product)
        {
            if (product.category == "Merchandise (Clothing)")
            {
                product = SetSize(product);
            }
            return product;
        }

        Product SetSize(Product product)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Program.ClearCurrentConsoleLine();
            Console.Write("Please choose a following Size S,M,L,XL,XXL: ");

            Product merch = new Merchandise(product.id,
                                                   product.category,
                                                   product.name,
                                                   product.price,
                                                   product.prepTime,
                                                   product.categoryId);

            while (true)
            {
                string input = Console.ReadLine();
                string finput = input.ToUpper();
                if (finput == "S")
                {
                    merch.SetSize("S");
                    return merch;
                }
                else if (finput == "M")
                {
                    merch.SetSize("M");
                    return merch;
                }
                else if (finput == "L")
                {
                    merch.SetSize("L");
                    return merch;
                }
                else if (finput == "XL")
                {
                    merch.SetSize("XL");
                    return merch;
                }
                else if (finput == "XXL")
                {
                    merch.SetSize("XXL");
                    return merch;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please input a legitimate Size [Press Any Key to continue]");
                    Console.ReadKey();
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please choose a following Size S,M,L,XL,XXL: ");
                }
            }
        }
    }
}
