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
            //to be deleted if it continues to be obsolete
            //ArrayList orderList = ReadOrderList();

            //variable for different menu positions based from the header
            int headerLength = 4;

            //create a new order
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

                int selection = Convert.ToInt32(Console.ReadLine());

                //check that the input value is a correct one
                while (selection < 1 || selection > (products.Count + 1))
                {
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please enter a valid Option: ");
                    selection = Convert.ToInt32(Console.ReadLine());
                    Program.ClearCurrentConsoleLine();
                }

                //option 13 = place order
                if (selection == products.Count + 1) {
                    order = (Order)Order.FillInformationInOrder(order, user, shoppingCart);
                    
                    break;
                }
                products[selection - 1] = CheckIfDrink(products[selection - 1]);

                products[selection - 1] = CheckIfMerchandise(products[selection - 1]);

                //add product to shopping cart - MUST HAPPEN AFTER PLACING ORDER!
                shoppingCart.Add(products[selection - 1]);

                //setting the cursor behind the price
                Console.SetCursorPosition(60, selection + headerLength);
                Console.Write("(1)");
                Console.SetCursorPosition(x, y);
            }
            Console.Clear();

            if(order.prepTime == 0)
            {
                order.state = State.Delivery;
            }
            Driver.AddOrderToDriver(order);

            //for the time this function is obsolete because orders get saved in the drivers 
            //orderList.Add(order);
            //WriteOrderList(orderList);
            return order;
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
            else
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

        static ArrayList ReadOrderList()
        {
            string json = File.ReadAllText("src/data/order_data.json");

            var orderList = JsonConvert.DeserializeObject<ArrayList>(json);
            return orderList;
        }

        static void WriteOrderList(ArrayList orderList)
        {
            //serializes the product data to json
            string json = JsonConvert.SerializeObject(orderList, Formatting.Indented);

            //write serialized json to file
            File.WriteAllText(@"src/data/order_data.json", json);
        }
    }
}
