using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Contract;
using Entity;

namespace BusVersion01
{
    public class BusVersion01 : IBus
    {
        IDataAccess _data;
        public string Name => "BusVersion01";

        public string Description => "BusVersion01 - test";

        public void DepensOn(IDataAccess data)
        {
            _data = data;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = _data.GetCustomer();
            return customers;
        }

        public List<Order1> GetOrders()
        {
            List<Order1> orders = _data.GetOrder();
            return orders;
        }

        public BindingList<Product> GetProducts()
        {
            return new BindingList<Product>(_data.GetProducts());
        }

        public IEnumerable<dynamic> GetProductsByFilter(string name, string sortType, int priceFrom = -1, int priceTo = -1, int currentPage = 1,int itemPerPage = 5)
        {
           var products = _data.GetProducts();
            var list = from p in products
                         where p.ProductName.ToLower().Contains(name.ToLower())
                         select new { ImgPath = p.ImgPath, Name = p.ProductName, Price = p.Price, Stock = 500, Sold = 1000 };

            if (priceFrom != -1 && priceTo != -1)
            {
                list = list.Where(p => p.Price >= priceFrom && p.Price <= priceTo);
            }

            if (sortType == "Price")
            {
                list = list.OrderBy(p => p.Price);
            }
            else if (sortType == "Name")
            {
                list = list.OrderBy(p => p.Name);
            }

            int count = list.Count();
            var result = from p in list
                     select new { ImgPath = p.ImgPath, Name = p.Name, Price = p.Price, Stock = 500, Sold = 1000, Total = count };
            result = result.Skip((currentPage - 1) * itemPerPage).Take(itemPerPage);
            
            return result;
        }

        public IEnumerable<dynamic> GetProductsByName(string name)
        {
            var products = _data.GetProducts();
            var result = from p in products
                         where p.ProductName.ToLower().Contains(name.ToLower())
                         select new { ImgPath = p.ImgPath, Name = p.ProductName, Price = p.Price, Stock = 500, Sold = 1000 };



            return result;
        }

        public BindingList<dynamic> GetProductsDynamic()
        {
            var products = _data.GetProducts();
            var result = new BindingList<dynamic>();

            foreach (var product in products)
            {
                result.Add(new
                {
                    ImgPath = product.ImgPath,
                    Name = product.ProductName,
                    Price = product.Price,
                    Stock = 500,
                    Sold = 1000
                });
            }

            return result;
        }

        public async Task<bool> Login(string username, string password)
        {
            bool result = await _data.LoginAsync(username, password);
            return result;
        }

        public void saveChanges()
        {
            _data.saveChanges();
        }
    }

}
