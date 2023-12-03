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

        public void DeleteOrder(int DelId)
        {
            var orders = _data.GetOrder();
            var order = orders.Where(o => o.Id == DelId).FirstOrDefault();
            _data.DeleteOrder(order);

        }

        public void addOrder(Order1 addOrder)
        {
            _data.addOrder(addOrder);
        }

        public void DepensOn(IDataAccess data)
        {
            _data = data;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = _data.GetCustomer();
            return customers;
        }

        public List<dynamic> GetOrderByFilter(string dateFrom= "1/1/1900", string dateTo = "1/1/2100", int currentPage = 1, int itemPerPage = 10)
        {
            //throw new NotImplementedException();
            DateTime datefrom = DateTime.Parse(dateFrom);
            DateTime dateto = DateTime.Parse(dateTo);
            var orders = _data.GetOrder();
            var list = from o in orders join c in _data.GetCustomer() on o.CustomerId equals c.Id where o.OrderDate >= datefrom && o.OrderDate <= dateto select new { Id = o.Id, Name = c.CustomerName, Date = o.OrderDate };
            int count = list.Count();
            var result = from o in list
                         select new { Id = o.Id, Name = o.Name, Date = o.Date, Total = count };
            result = result.Skip((currentPage - 1) * itemPerPage).Take(itemPerPage);
            return result.ToList<dynamic>();

                       
        }

        public List<Order1> GetOrders()
        {
            List<Order1> orders = _data.GetOrder();
            return orders;
        }

        public List<OrderDetail> GetOrdersDetailById(int orderid)
        {
            //throw new NotImplementedException();
            List<OrderDetail> orderDetails = _data.GetOrderDetail();
            var result = orderDetails.Where(o => o.OrderId == orderid).ToList();
            return result;
        }

        public List<Product> GetProducts()
        {
            return new List<Product>(_data.GetProducts());
        }

        public IEnumerable<dynamic> GetProductsByFilter(string name, string sortType, int priceFrom = -1, int priceTo = -1, int currentPage = 1,int itemPerPage = 5)
        {
           var products = _data.GetProducts();
            var list = from p in products
                         where p.ProductName.ToLower().Contains(name.ToLower())
                         select new {Id = p.Id, ImgPath = p.ImgPath, Name = p.ProductName, Price = p.Price, Stock = 500, Sold = 1000 };

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
                     select new { Id = p.Id, ImgPath = p.ImgPath, Name = p.Name, Price = p.Price, Stock = 500, Sold = 1000, Total = count };
            result = result.Skip((currentPage - 1) * itemPerPage).Take(itemPerPage);
            
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

        public void addOrderDetail(OrderDetail addOrderDetail)
        {
            _data.addOrderDetail(addOrderDetail);
            //update stock
            var product = _data.getProductById(addOrderDetail.ProductId);
            product.Amount -= addOrderDetail.Amount;
            _data.saveChanges();
        }


        public bool addProduct(Product addProduct)
        {
            bool result = true;

            _data.addProduct(addProduct);

            return result;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>(_data.GetCategory());

            return categories;
        }

        public bool addCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Product getProductById(int id)
        {
            return _data.getProductById(id);
        }

        public void updateProduct(Product updateProduct)
        {
            _data.updateProduct(updateProduct);
        }

        public Customer getCustomerByName(string name)
        {
            var customers = _data.GetCustomer();
            var result = customers.Where(c => c.CustomerName.ToLower().Contains(name.ToLower())).FirstOrDefault();
            return result;
        }

        public void addCustomer(Customer addCustomer)
        {
            _data.addCustomer(addCustomer);
        }

        public Order1 getOrderById(int id)
        {
            return _data.getOrderById(id);
        }
    }

}
