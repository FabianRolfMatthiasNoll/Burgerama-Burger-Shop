using System;
using System.Collections.Generic;
using ConsoleTables;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama_Burger_Shop_App.products;
using System.Diagnostics.CodeAnalysis;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class OrderHandler
    {
        readonly FileHandler _fileHandler;
        private readonly DriverHandler _driverHandler;
        List<Product> _unsortedProducts;
        public List<Product> products;
        public Order order;
        public string fileNameProduct;

        public OrderHandler(string filePath, string fileNameP, string fileNameDStates, string fileNameDConfig)
        {
            fileNameProduct = fileNameP;
            products = new List<Product>();
            order = new Order(filePath,  fileNameDStates);
            _fileHandler = new FileHandler(filePath);
            _driverHandler = new DriverHandler(filePath, fileNameDStates, fileNameDConfig);
            _unsortedProducts = new List<Product>();
        }

        public void LoadProductData()
        {
            _unsortedProducts = _fileHandler.ReadJson<Product>(fileNameProduct);
            products = _unsortedProducts.OrderBy(o => o.categoryId).ToList();
        }

        [ExcludeFromCodeCoverage]
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

        [ExcludeFromCodeCoverage]
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

        [ExcludeFromCodeCoverage]
        public int GetProductCount()
        {
            LoadProductData();
            return _unsortedProducts.Count;
        }

        public void AddProductToOrder(int index)
        {
            order.boughtProducts.Add(products[index]);
        }

        [ExcludeFromCodeCoverage]
        public void FinishOrder(User user)
        {
            order.FillInformationInOrder(user);
            _driverHandler.AddOrderToBestDriver(order);
        }

        public bool IsProductDrink(int index)
        {
            if (products[index].category == "Drink" && products[index].name != "Red Bull")
            {
                    return true;
            }
            return false;
        }

        public void SetDrinkOnIce(string input, int index)
        {
            if(input == "true")
            {
                Product drinkIce = new Drink(products[index].id,
                                             products[index].category,
                                             products[index].name,
                                             products[index].price,
                                             products[index].prepTime,
                                             products[index].categoryId,
                                             true);
                products[index] = drinkIce;
            } 
        }

        public bool IsProductMerchandise(int index)
        {
            if (products[index].category == "Merchandise (Clothing)")
            {
                return true;
            }
            return false;
        }

        public bool SizeValidator(string input)
        {
            input = input.ToUpper();
            return new[] { "S", "M", "L", "XL", "XXL" }.Contains(input);
        }

        public void SetSizeOfProduct(string input, int index)
        {
            input = input.ToUpper();
            Product merch = new Merchandise(products[index].id,
                                            products[index].category,
                                            products[index].name,
                                            products[index].price,
                                            products[index].prepTime,
                                            products[index].categoryId);
            merch.SetSize(input);
            products[index] = merch;
        }
    }
}
