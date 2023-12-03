using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contract
{
    public interface IBus
    {
        string Name { get; }
        string Description { get; }

        Task<bool> Login(string username, string password);

        List<Order1> GetOrders();

        List<OrderDetail> GetOrdersDetailById(int orderid);

        List<Customer> GetCustomers();
        void saveChanges();
        List<Product> GetProducts();
        List<Category> GetCategories();
        IEnumerable<dynamic> GetProductsByFilter(string name, string sortType, int priceFrom = -1, int priceTo = -1, int currentPage = 1, int itemPerPage = 10);
        public List<dynamic> GetOrderByFilter(string dateFrom, string dateTo, int currentPage = 1, int itemPerPage = 10);
        public void DeleteOrder(int DelId);
        public void addOrder(Order1 addOrder);
        public void addOrderDetail(OrderDetail addOrderDetail);

        public Customer getCustomerByName(string name);
        public void addCustomer(Customer addCustomer);

        public bool addProduct(Product addProduct);
       public bool addCategory(Category category);
        public Product getProductById(int id);
        public void updateProduct(Product updateProduct);
        void DepensOn(IDataAccess data);
    }
}
