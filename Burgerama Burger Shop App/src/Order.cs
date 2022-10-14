using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.products;
using Burgerama_Burger_Shop_App.src.handlers;

namespace Burgerama_Burger_Shop_App
{
    public enum State
    {
        Preperation = 0,
        Delivery = 1,
        Closed = 2
    }

    public class Order
    {
        public int id;
        public State state;
        public int prepTime;
        public int shipTime;
        public int totalTime;
        public double totalSum;
        public List<Product> boughtProducts;
        public User customer;

        FileHandler fileHandler;
        List<Driver> drivers;
        public string fileNameDriver;

        public Order(string filePath, string fileNameD)
        {
            fileNameDriver = fileNameD;
            state = State.Preperation;
            fileHandler = new FileHandler(filePath);
            boughtProducts = new List<Product>();
            drivers = new List<Driver>();

        }

        public void FillInformationInOrder(User user)
        {
            drivers = fileHandler.ReadJSON<Driver>(fileNameDriver);
            customer = user;
            
            foreach(var product in boughtProducts)
            {
                //pass highest prepTime only
                if (prepTime < product.prepTime)
                {
                    prepTime = product.prepTime;
                }

                if (prepTime == 0)
                {
                    state = State.Delivery;
                } else
                {
                    state = State.Preperation;
                }

                totalSum = totalSum + product.price;
            }

            bool driverAvailable = false;

            foreach (var driver in drivers)
            {
                if (Driver.IsDriverFree(driver))
                {
                    shipTime = 20;
                    driverAvailable = true;
                    break;
                }
            }
            if (!driverAvailable)
            {
                shipTime = 35;
            }

            totalTime = shipTime + prepTime;
        }

        public void DecreaseTotalTime()
        {
            if (state == State.Preperation)
            {
                prepTime = prepTime - 15;
                if (prepTime <= 0)
                {
                    state = State.Delivery;
                    shipTime = shipTime - Math.Abs(prepTime);
                    prepTime = 0;
                }
            }
            else if (state == State.Delivery)
            {
                shipTime = shipTime - 15;
                if (shipTime <= 0)
                {
                    state = State.Closed;
                    shipTime = 0;
                }
            }
        }
    }
}
