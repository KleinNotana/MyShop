using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Entity;

namespace Contract
{
    public interface IDataAccess
    {
        string Name { get; }
        string Description { get; }

        public Task<bool> LoginAsync(string username, string password);
        public List<Product> GetProducts();
        public List<Category> GetCategory();

        public List<Order1> GetOrder();

        public List<OrderDetail> GetOrderDetail();

        public void deleteOrderDetail(OrderDetail delOrderDetail);
        public List<Customer> GetCustomer();

        public Product getProductById(int id);
        public void updateProduct(Product updateProduct);

        public Order1 getOrderById(int id);
        public void DeleteOrder(Order1 delOrder);
        public void addOrder(Order1 addOrder);
        public void addCustomer(Customer addCustomer);

        public void addOrderDetail(OrderDetail addOrderDetail);
        public void addProduct(Product addProduct);
        public void addCategory(Category category);
        public void saveChanges();

    }
}
