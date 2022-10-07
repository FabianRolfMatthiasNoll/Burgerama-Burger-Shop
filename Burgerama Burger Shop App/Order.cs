using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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

        public Order(int orderId)
        {
            id = orderId;
            state = State.Preperation;
        }

        public static Object FillInformationInOrder(Order order, User user, List<Product> shoppingCart)
        {
            order.customer = user;
            order.boughtProducts = shoppingCart;
            
            foreach(var product in order.boughtProducts)
            {
                //pass highest prepTime only
                if (order.prepTime < product.prepTime)
                {
                    order.prepTime = product.prepTime;
                }

                order.totalSum = order.totalSum + product.price;
            }

            //if a driver is free = 20min delivery - if not = 35min delivery
            order.shipTime = (DriverManagment.IsDriverFree()) ? 20 : 35;

            order.totalTime = order.shipTime + order.prepTime;

            return order;
        }
    }
}
