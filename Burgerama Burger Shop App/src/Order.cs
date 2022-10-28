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
        public State state;
        public int prepTime;
        public int shipTime;
        public int totalTime;
        public double totalSum;
        public List<Product> boughtProducts;
        public User customer;

        readonly FileHandler _fileHandler;
        private readonly string _fileNameDriver;

        public Order(string filePath, string fileNameD)
        {
            _fileNameDriver = fileNameD;
            state = State.Preperation;
            _fileHandler = new FileHandler(filePath);
            boughtProducts = new List<Product>();

        }

        public void FillInformationInOrder(User user, Driver driver)
        {
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

                totalSum += product.price;
            }

            shipTime = driver.CalculateDeliveryTime();

            totalTime = shipTime + prepTime;
        }

        public void DecreaseTotalTime()
        {
            if (state == State.Preperation)
            {
                prepTime -= 15;
                if (prepTime <= 0)
                {
                    state = State.Delivery;
                    shipTime -= Math.Abs(prepTime);
                    prepTime = 0;
                }
            }
            else if (state == State.Delivery)
            {
                shipTime -= 15;
                if (shipTime <= 0)
                {
                    state = State.Closed;
                    shipTime = 0;
                }
            }
            totalTime -= 15;
            if(totalTime < 0)
            {
                totalTime = 0;
            }
        }
    }
}
