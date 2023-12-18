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
        //LOGIN
        public Task<bool> LoginAsync(string username, string password, string servername, string databasename);
        //PRODUCT
        public List<Product> GetProducts();
        public Product GetProductById(int id);
        public void UpdateProduct(Product updateProduct);
        public void AddProduct(Product addProduct);
        public void DeleteProduct(int id);
        //CATEGORY
        public List<Category> GetCategory();
        public void AddCategory(Category category);
        public Category GetCategoryById(int id);
        public void UpdateCategory(Category updateCategory);
        public void DeleteCategory(int id);
        //CUSTOMER
        public List<Customer> GetCustomer();
        public void AddCustomer(Customer addCustomer);
        //ORDER
        public List<Order1> GetOrder();
        public Order1 GetOrderById(int id);
        public void DeleteOrder(Order1 delOrder);
        public void AddOrder(Order1 addOrder);
        //ORDERDETAIL
        public List<OrderDetail> GetOrderDetail();
        public void DeleteOrderDetail(OrderDetail delOrderDetail);
        public void AddOrderDetail(OrderDetail addOrderDetail);
        
        //SAVE       
        public void SaveChanges();

    }
}
