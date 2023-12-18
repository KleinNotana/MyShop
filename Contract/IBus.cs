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
        public void DepensOn(IDataAccess data);

        //LOGIN

        public Task<bool> Login(string username, string password, string servername, string databasename);

        public void SaveChanges();

        //PRODUCT
        public List<Product> GetProducts();
        public BindingList<dynamic> GetProductsByFilter(string name, string sortType, int priceFrom = -1, int priceTo = -1, int currentPage = 1, int itemPerPage = 10, int categoryId = -1);
        public BindingList<dynamic> GetDiscountProductsByFilter(string name, string sortType, int priceFrom = -1, int priceTo = -1, int currentPage = 1, int itemPerPage = 10, int categoryId = -1);
        public bool AddProduct(Product addProduct);
        public Product GetProductById(int id);
        public void UpdateProduct(Product updateProduct);
        public void DeleteProduct(int id);
        public dynamic GetDetailProduct(int id);
        public bool UpdateDiscountProduct(int id, int discount, DateTime expDate);
        public void RemoveDiscountProduct(int id);

        //CATEGORY
        public BindingList<Category> GetCategories();
        public bool AddCategory(Category category);
        public int GetCategoryID(string name);
        public Category GetCategoryById(int id);
        public void UpdateCategory(Category updateCategory);
        public void DeleteCategory(int id);

        //CUSTOMER
        public List<Customer> GetCustomers();
        public Customer GetCustomerByName(string name);
        public Customer GetCustomerByPhone(string Phone);
        public void AddCustomer(Customer addCustomer);

        //ORDER

        public List<Order1> GetOrders();
        public List<dynamic> GetOrderByFilter(string dateFrom, string dateTo, int currentPage = 1, int itemPerPage = 10);
        public Order1 GetOrderById(int id);
        public void DeleteOrder(int DelId);
        public void AddOrder(Order1 addOrder);

        //ORDERDETAIL

        public List<OrderDetail> GetOrdersDetailById(int orderid);
        public void AddOrderDetail(OrderDetail addOrderDetail);
        public void DeleteOrderDetail(OrderDetail deleteOrderDetail);
        public double GetTotalPrice(int OrderId);
        
        //REPORT

        public List<dynamic> GetMonthlyReport(string dateFrom, string dateTo, int mode);
        public List<dynamic> GetDailyReport(string dateFrom, string dateTo, int mode);
        public List<dynamic> GetWeeklyReport(string dateFrom, string dateTo, int mode);
        public List<dynamic> GetYearlyReport(string dateFrom, string dateTo, int mode);
        public List<dynamic> GetTop5productbyday(string date);
        public List<dynamic> GetTop5productbyweek(string datefrom, int week);
        public List<dynamic> GetTop5productbymonth( string date);
        public List<dynamic> GetTop5productbyyear( string date);
        public BindingList<dynamic> GetOutOfStockProducts();
        public dynamic GetTotalSales();
        public dynamic GetSellingProductAmount();
        public dynamic GetSoldProductAmount();
        public dynamic GetTotalCustomers();
        public List<dynamic> GetTopSaleProducts();
        public List<dynamic> GetCurrentDailySales();
        public int GetWeek(DateTime startDay, DateTime endDay);



        public bool ImportData(string filePath);


    }
}
