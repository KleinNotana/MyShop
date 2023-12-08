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
        BindingList<Category> GetCategories();
        IEnumerable<dynamic> GetProductsByFilter(string name, string sortType, int priceFrom = -1, int priceTo = -1, int currentPage = 1, int itemPerPage = 10, int categoryId = -1);
        public List<dynamic> GetOrderByFilter(string dateFrom, string dateTo, int currentPage = 1, int itemPerPage = 10);

        public Order1 getOrderById(int id);
        public void DeleteOrder(int DelId);
        public void addOrder(Order1 addOrder);
        public void addOrderDetail(OrderDetail addOrderDetail);
        public void deleteOrderDetail(OrderDetail deleteOrderDetail);



        public Customer getCustomerByName(string name);
        public void addCustomer(Customer addCustomer);

        public bool addProduct(Product addProduct);
        public bool addCategory(Category category);
        public Category getCategoryById(int id);
        public void updateCategory(Category updateCategory);
        public void deleteCategory(int id);
        public Product getProductById(int id);
        public void updateProduct(Product updateProduct);
        public void deleteProduct(int id);

        dynamic getDetailProduct(int id);
        void DepensOn(IDataAccess data);
    }
}
