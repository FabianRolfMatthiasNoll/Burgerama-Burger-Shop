using Burgerama_Burger_Shop_App.products;
using Burgerama_Burger_Shop_App.src.handlers;
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
using Burgerama_Burger_Shop_App.src.validators;

namespace Burgerama_Burger_Shop_App.src.userinterfaces
{
    public class OrderUI
    {
        IntValidator intValidator;
        OrderHandler orderHandler;
        BoolValidator boolValidator;
        public string userInput;

        public OrderUI()
        {
            orderHandler = new OrderHandler("src/data/", "product_data.json", "driver_data.json", "driver_config.json");
            intValidator = new IntValidator(1, orderHandler.GetProductCount() + 1);
            boolValidator = new BoolValidator();
        }

        public void OrderMenu(User user)
        {
            Console.WriteLine("Welcome " + user.email);
            Console.WriteLine("Please Have a look at out menu:");
            Console.WriteLine("-----------------------------------------");

            orderHandler.LoadProductData();

            int headerLength = 4;

            orderHandler.CreateProductOverview().Write(Format.MarkDown);
            //var table = orderHandler.CreateProductOverview();
            //table.Write(Format.MarkDown);

            Console.WriteLine("Please choose a product on from our menu");
            //Get console entry position for menu alignment
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            List<Product> shoppingCart = new List<Product>();

            while (true)
            {
                Program.ClearCurrentConsoleLine();
                Console.Write("Enter your Option: ");

                userInput = Console.ReadLine();

                while (!intValidator.IsInputValid(userInput))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please enter a valid number!:");
                    userInput = Console.ReadLine();
                }

                int.TryParse(this.userInput, out int userSelection);

                if (userSelection == orderHandler.GetProductCount() + 1)
                {
                    orderHandler.FinishOrder(user);
                    break;
                }

                if (orderHandler.IsProductDrink(userSelection - 1))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please choose 'true' or 'false' if you want your Drink on Ice: ");
                    userInput = Console.ReadLine();
                    while (!boolValidator.IsStringValidBool(userInput))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Program.ClearCurrentConsoleLine();
                        Console.Write("You can only choose 'true' or 'false'. Please try again: ");
                        userInput = Console.ReadLine();
                    }
                    orderHandler.SetDrinkOnIce(userInput, userSelection - 1);
                }

                if (orderHandler.IsProductMerchandise(userSelection - 1))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Program.ClearCurrentConsoleLine();
                    Console.Write("Please choose a following Size S,M,L,XL,XXL: ");
                    userInput = Console.ReadLine();
                    while (!orderHandler.SizeValidator(userInput))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Program.ClearCurrentConsoleLine();
                        Console.Write("You can only choose between: S,M,L,XL,XXL. Please try again: ");
                        userInput = Console.ReadLine();
                    }
                    orderHandler.SetSizeOfProduct(userInput, userSelection - 1);
                }

                orderHandler.AddProductToOrder(userSelection - 1);

                //setting the cursor behind the price
                Console.SetCursorPosition(60, userSelection + headerLength);
                Console.Write("(1)");
                Console.SetCursorPosition(x, y);
            }
            Console.Clear();

            ShowTotalOrder();
        }

        public void ShowTotalOrder()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("        Summary of your Order");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Delivering to:    " + orderHandler.order.customer.postal + " " + orderHandler.order.customer.city);
            Console.WriteLine("                  " + orderHandler.order.customer.street);
            Console.WriteLine("-----------------------------------------");

            var table = orderHandler.CreateSummaryOverview();
            table.Write(Format.Minimal);

            Console.WriteLine("\n-----------------------------------------");
            Console.WriteLine("Your total including tax is: " + Math.Round(orderHandler.order.totalSum * 1.0884, 2) + "$");
            Console.WriteLine("Your Delivery is estimated to take: " + orderHandler.order.totalTime + "min");
            Console.WriteLine("Thank you for ordering at Burgerama");
            Console.ReadKey();
        }   
    }
}
