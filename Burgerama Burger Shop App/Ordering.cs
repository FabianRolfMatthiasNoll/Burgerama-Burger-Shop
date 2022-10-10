using Burgerama_Burger_Shop_App.products;
using Newtonsoft.Json;
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
            Console.WriteLine("================================================================");

            Product.FillProductData();  //Run only when first time initializing a new product_data.json

            //displaying our product catalog and return sorted product catalog as displayed
            List<Product> products = ShowProductData();

            Order order = (Order)OrderProduct(products, user);

            ShowTotalOrder(order);

            Console.WriteLine("Thank you for ordering at Burgerama");
            Console.ReadKey();
        }

        public static Order OrderProduct(List<Product> products, User user)
        {
            ArrayList orderList = ReadOrderList();

            //create a new order
            Order order = new Order(orderList.Count);

            List<Product> shoppingCart = new List<Product>();

            while (true)
            {
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
                Console.SetCursorPosition(60, selection + 2);
                Console.Write("(1)");
                Console.SetCursorPosition(0, 16);
            }
            Console.Clear();
            
            orderList.Add(order);
            WriteOrderList(orderList);
            return order;
        }

        public static List<Product> ShowProductData()
        {
            string json = File.ReadAllText("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\product_data.json");

            var productList = JsonConvert.DeserializeObject<List<Product>>(json);

            List<Product> SortedProductList = productList.OrderBy(o => o.categoryId).ToList();

            int i = 2;
            foreach(var product in SortedProductList)
            {
                i++;
                Console.SetCursorPosition(0, i);
                Console.Write("(" + (i - 2) + ") ");
                Console.Write(product.name);
                Console.SetCursorPosition(25, i);
                Console.Write(product.category);
                Console.SetCursorPosition(53, i);
                Console.WriteLine(product.price + "$");
            }
            Console.SetCursorPosition(0, i + 1);
            Console.WriteLine("(" + (i - 1) + ") Place Order "); //i is only -1 because i didnt get incremented at the end of the foreach
            return SortedProductList;
        }
    
        public static void ShowTotalOrder(Order order)
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("             Summary of your Order");
            Console.WriteLine("===================================================");
            Console.WriteLine("Delivering to: " + order.customer.postal + " " + order.customer.city);
            Console.WriteLine("               " + order.customer.street);
            Console.WriteLine("===================================================");
            int consoleRow = 6; //i have to do this dynamicly at some point!!!
            int totalSum;
            foreach (var product in order.boughtProducts)
            {
                consoleRow++;
                product.PrintSummaryInfo(consoleRow);
            }
            Console.WriteLine("\nYour total is: " + Math.Round(order.totalSum,2) + "$");
            Console.WriteLine("Your Delivery is estimated to take: " + order.totalTime + "min");
        }

        static Product CheckIfDrink(Product product)
        {
            if (product.category == "Drink")
            {
                product = IsDrinkOnIce(product);
            }
            return product;
        }

        static Product IsDrinkOnIce(Product product)
        {
            Program.ClearCurrentConsoleLine();
            Console.Write("Please choose 'true' or 'false' if you want your Drink on Ice: ");
            if (Console.ReadLine() == "true")
            {
                Product drinkIce = new Drink(product.id,
                                          product.category,
                                          product.name,
                                          product.price,
                                          product.prepTime,
                                          product.categoryId,
                                          true);
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
                if (input == "S")
                {
                    merch.SetSize("S");
                    return merch;
                } else if (input == "M")
                {
                    merch.SetSize("M");
                    return merch;
                } else if (input == "L")
                {
                    merch.SetSize("L");
                    return merch;
                } else if (input == "XL")
                {
                    merch.SetSize("XL");
                    return merch;
                } else if (input == "XXL")
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
            string json = File.ReadAllText("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\order_data.json");

            var orderList = JsonConvert.DeserializeObject<ArrayList>(json);
            return orderList;
        }

        static void WriteOrderList(ArrayList orderList)
        {
            //serializes the product data to json
            string json = JsonConvert.SerializeObject(orderList, Formatting.Indented);

            //write serialized json to file
            File.WriteAllText(@"C:\Users\fanoll\Source\Repos\burgerama-burger-shop\Burgerama Burger Shop App\order_data.json", json);
        }
    }
}
