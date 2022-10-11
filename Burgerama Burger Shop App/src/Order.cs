using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.products;

namespace Burgerama_Burger_Shop_App
{
    enum State
    {
        Preperation = 0,
        Delivery = 1,
        Closed = 2
    }

    internal class Order
    {
        public int id;
        public State state;
        public int prepTime;
        public int shipTime;
        public int totalTime;
        public double totalSum;
        public List<Product> boughtProducts;
        public User customer;

        public Order()
        {
            state = State.Preperation;
        }

        public static Object FillInformationInOrder(Order order, User user, List<Product> shoppingCart)
        {

            List<Driver> drivers = Driver.LoadCurrentDriverStates();

            order.customer = user;
            order.boughtProducts = shoppingCart;
            
            foreach(var product in order.boughtProducts)
            {
                //pass highest prepTime only
                if (order.prepTime < product.prepTime)
                {
                    order.prepTime = product.prepTime;
                }

                if (order.prepTime == 0)
                {
                    order.state = State.Delivery;
                } else
                {
                    order.state = State.Preperation;
                }

                order.totalSum = order.totalSum + product.price;
            }

            bool driverAvailable = false;

            foreach (var driver in drivers)
            {
                if (Driver.IsDriverFree(driver))
                {
                    order.shipTime = 20;
                    driverAvailable = true;
                    break;
                }
            }
            if (!driverAvailable)
            {
                order.shipTime = 35;
            }

            order.totalTime = order.shipTime + order.prepTime;

            return order;
        }

        public static void DecreaseTotalTime(Order order)
        {
            if (order.state == State.Preperation)
            {
                order.prepTime = order.prepTime - 15;
                if (order.prepTime <= 0)
                {
                    order.state = State.Delivery;
                    order.shipTime = order.shipTime - Math.Abs(order.prepTime);
                    order.prepTime = 0;
                }
            }
            else if (order.state == State.Delivery)
            {
                order.shipTime = order.shipTime - 15;
                if (order.shipTime <= 0)
                {
                    order.state = State.Closed;
                    order.shipTime = 0;
                }
            }
        }
    }
}
