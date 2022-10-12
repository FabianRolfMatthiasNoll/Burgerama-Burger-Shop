using Burgerama_Burger_Shop_App.products;
using ConsoleTables;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_App
{
    internal class Ordering
    {

        public static void OrderMenu(User user)
        {
            Console.WriteLine("Welcome " + user.email);
            Console.WriteLine("Please Have a look at out menu:");
            Console.WriteLine("-----------------------------------------");

            //Product.FillProductData();  //Run only when first time initializing a new product_data.json

            //displaying our product catalog and return sorted product catalog as displayed
            List<Product> products = ShowProductData();

            Order order = (Order)OrderProduct(products, user);

            ShowTotalOrder(order);

            Console.WriteLine("Thank you for ordering at Burgerama");
            Console.ReadKey();
        }

        public static Order OrderProduct(List<Product> products, User user)
        {
            //variable for different menu positions based from the header
            int headerLength = 4;

            Order order = new Order();
            List<Product> shoppingCart = new List<Product>();

            while (true)
            {
                //Get console entry position for menu alignment
                int x = Console.CursorLeft;
                int y = Console.CursorTop;

                //choose a product to add to the shopping cart
                Console.WriteLine("Please choose a product on from our menu");
                Program.ClearCurrentConsoleLine();
                Console.Write("Enter your Option: ");

                var selectInput = Console.ReadLine();

                int selection = CheckValidInput(selectInput, products.Count);

                if (selection == products.Count + 1)
                {
                    order = (Order)Order.FillInformationInOrder(order, user, shoppingCart);

                    break;
                }

                products[selection - 1] = CheckIfDrink(products[selection - 1]);

                products[selection - 1] = CheckIfMerchandise(products[selection - 1]);

                shoppingCart.Add(products[selection - 1]);

                //setting the cursor behind the price
                Console.SetCursorPosition(60, selection + headerLength);
                Console.Write("(1)");
                Console.SetCursorPosition(x, y);
            }
            Console.Clear();
            Driver.AddOrderToDriver(order);
            return order;
        }

        public static int CheckValidInput(string selectInput, int products)
        {
            int selection;
            while (!int.TryParse(selectInput, out selection))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a number!:");
                selectInput = Console.ReadLine();
            }

            if (selection < 1 || selection > (products + 1))
            {
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid Option: ");
                selection = Convert.ToInt32(Console.ReadLine());
                Program.ClearCurrentConsoleLine();
            }
            return selection;
        }

        public static List<Product> ShowProductData()
        {
            string json = File.ReadAllText("src/data/product_data.json");

            var productList = JsonConvert.DeserializeObject<List<Product>>(json);

            List<Product> SortedProductList = productList.OrderBy(o => o.categoryId).ToList();

            var table = new ConsoleTable("ID","Name","Category","Price");
            
            int index = 1;
            foreach (var product in SortedProductList)
            {
                table.AddRow(index, product.name, product.category, product.price);
                index++;
            }

            table.AddRow(index, "Place Order","","");
            table.Write(Format.MarkDown);
            
            return SortedProductList;
        }
    
        public static void ShowTotalOrder(Order order)
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("        Summary of your Order");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Delivering to:    " + order.customer.postal + " " + order.customer.city);
            Console.WriteLine("                  " + order.customer.street);
            Console.WriteLine("-----------------------------------------");

            var table = new ConsoleTable("Pos.", "Name", "Variant", "Price");
            int index = 1;

            foreach (var product in order.boughtProducts)
            {
                table = product.PrintSummaryInfo(table, index);
                index++;
            }
            table.AddRow(index, "US Tax", "", Math.Round((order.totalSum * 0.0884), 2) + "$");

            table.Write(Format.Minimal);
            Console.WriteLine("\n-----------------------------------------");
            Console.WriteLine("Your total including tax is: " + Math.Round((order.totalSum * 1.0884),2) + "$");
            Console.WriteLine("Your Delivery is estimated to take: " + order.totalTime + "min");
            Console.ReadKey();
            Console.Clear();
            Program.Main();
        }

        static Product CheckIfDrink(Product product)
        {
            if (product.category == "Drink")
            {
                if(product.name == "Red Bull")
                {
                    return product;
                }
                product = IsDrinkOnIce(product);
            }
            return product;
        }

        static Product IsDrinkOnIce(Product product)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Program.ClearCurrentConsoleLine();
            Console.Write("Please choose 'true' or 'false' if you want your Drink on Ice: ");
            string input = Console.ReadLine();
            string finput = input.ToLower();
            while(true)
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

        static Product CheckIfMerchandise(Product product)
        {
            if (product.category == "Merchandise (Clothing)")
            {
                product = SetSize(product);
            }
            return product;
        }

        static Product SetSize(Product product)
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
                } else if (finput == "M")
                {
                    merch.SetSize("M");
                    return merch;
                } else if (finput == "L")
                {
                    merch.SetSize("L");
                    return merch;
                } else if (finput == "XL")
                {
                    merch.SetSize("XL");
                    return merch;
                } else if (finput == "XXL")
                {
                    merch.SetSize("XXL");
                    return merch;
                } else
                {
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
