using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

            FillProductData();  //Run only when first time initializing a new product_data.json

            //displaying our product catalog and return sorted product catalog as displayed
            List<Product> products = ShowProductData();

            Order order = (Order)OrderProduct(products, user);

            ShowTotalOrder(order);

            Console.WriteLine("Thank you for ordering at Burgerama");
            Console.ReadKey();
        }

        public static Object OrderProduct(List<Product> products, User user)
        {
            ArrayList orderList = ReadOrderList();

            //create a new order
            Order order = new Order(orderList.Count);

            List<Product> shoppingCart = new List<Product>();

            while (true)
            {
                //choose a product to add to the shopping cart
                Console.WriteLine("Please choose a product on from our menu");
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

                if (products[selection -1].category == "Merchandise (Clothing)")
                {
                    IMerchandise product = new Product(products[selection - 1].id,
                                                       products[selection - 1].category,
                                                       products[selection - 1].name,
                                                       products[selection - 1].price,
                                                       products[selection - 1].prepTime,
                                                       products[selection - 1].categoryId);
                    Console.WriteLine("You can choose: 0 - S / 1 - M / 2 - L / 3 - Xl / 4 - XXL");
                    Console.Write("Please select a size: ");
                    product.SetSize(Convert.ToInt32(Console.ReadLine()));
                    products[selection - 1] = (Product)product;
                }
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

        public static void FillProductData()
        {
            Product product = new Product(130, "Food", "Cheeseburger", 7.99, 5, 1);
            Product product2 = new Product(89, "Food", "Classic Burger", 9.90, 12, 1);
            IDrink product3 = new Product(223, "Drink", "Coca Cola", 2.99, 2, 2);
            IDrink product4 = new Product(33, "Drink", "Sprite", 2.99, 2, 2);
            Product product5 = new Product(23, "Food", "Burgerama Special", 16.99, 16, 1);
            Product product6 = new Product(666, "Food", "Red Devil", 12.99, 14, 1);
            Product product7 = new Product(43, "Drink", "Red Bull", 3.90, 12, 2);
            IMerchandise product8 = new Product(91, "Merchandise (Clothing)", "T-Shirt Burgerama", 12.89, 0, 3);
            Product product9 = new Product(4, "Merchandise (One Size)", "Snapback", 7.90, 0, 4);
            IMerchandise product10 = new Product(273, "Merchandise (Clothing)", "Hoodie", 29.80, 0, 3);
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
            File.WriteAllText(@"C:\Users\fanoll\Source\Repos\burgerama-burger-shop\Burgerama Burger Shop App\product_data.json", json);
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
            int i = 6; //i have to do this dynamicly at some point!!!
            int totalSum;
            foreach (var product in order.boughtProducts)
            {
                i++;
                Console.SetCursorPosition(0, i);
                Console.Write("(" + (i - 6) + ") ");
                Console.Write(product.name);
                Console.SetCursorPosition(25, i);
                Console.WriteLine(product.price + "$");
            }
            Console.WriteLine("\nYour total is: " + Math.Round(order.totalSum,2) + "$");
            Console.WriteLine("Your Delivery is estimated to take: " + order.totalTime + "min");
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
