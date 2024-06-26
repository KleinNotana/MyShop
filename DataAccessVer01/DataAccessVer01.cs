using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using Contract;
using Entity;
using Microsoft.Data.SqlClient;


namespace DataAccessVer01
{
    public class DataAccessVer01 : IDataAccess
    {
        public string Name => "DataAccessVer01";

        public string Description => "DataAccessVer01 - test";
        //LOGIN
        public async Task<bool> LoginAsync(string username, string password, string servername, string databasename)
        {
            var builder = new SqlConnectionStringBuilder();
            //builder.DataSource = ".\\SQLEXPRESS";
            builder.DataSource = servername;
            //builder.InitialCatalog = "MyShopDB";
            builder.InitialCatalog = databasename;
            //builder.IntegratedSecurity = true;
            builder.TrustServerCertificate = true;
            builder.UserID = username;
            builder.Password = password;

            string connectionString = builder.ConnectionString;
            Database DB = Database.Instance;
            //var connection = new SqlConnection(connectionString);
            DB.context = new MyShopDbContext(connectionString);
            //run in background thread

            bool canLogin = await DB.context.Database.CanConnectAsync();

            if (canLogin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public List<Product> GetProducts()
        {
            Database DB = Database.Instance;
            return DB.context.Products.ToList();
        }

        public List<Category> GetCategory()
        {
            Database DB = Database.Instance;
            return DB.context.Categories.ToList();

        }

        public List<Order1> GetOrder()
        {
            Database database = Database.Instance;
            return database.context.Order1s.ToList();
        }

        public void DeleteOrder(Order1 delOrder)
        {
            Database DB = Database.Instance;
            DB.context.Order1s.Remove(delOrder);
            DB.context.SaveChanges();
        }

        public List<OrderDetail> GetOrderDetail()
        {
            Database DB = Database.Instance;
            return DB.context.OrderDetails.ToList();
        }

        public List<Customer> GetCustomer()
        {
            Database DB = Database.Instance;
            return DB.context.Customers.ToList();
        }

        public void SaveChanges()
        {
            Database DB = Database.Instance;
            DB.context.SaveChanges();
        }

        public void AddOrder(Order1 addOrder)
        {
            Database DB = Database.Instance;
            DB.context.Order1s.Add(addOrder);
            DB.context.SaveChanges();
        }

        public void AddOrderDetail(OrderDetail addOrderDetail)
        {
            Database DB = Database.Instance;
            DB.context.OrderDetails.Add(addOrderDetail);
            DB.context.SaveChanges();
        }

        public void AddProduct(Product addProduct)
        {
            Database DB = Database.Instance;
            DB.context.Products.Add(addProduct);
            DB.context.SaveChanges();
        }

        public void AddCategory(Category category)
        {
            Database DB = Database.Instance;
            DB.context.Categories.Add(category);
            DB.context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            Database DB = Database.Instance;
            return DB.context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public void UpdateProduct(Product updateProduct)
        {
            Database database = Database.Instance;
            var product = database.context.Products.Where(p => p.Id == updateProduct.Id).FirstOrDefault();
            product.ProductName = updateProduct.ProductName;
            product.Price = updateProduct.Price;
            product.CategoryId = updateProduct.CategoryId;
            product.Description = updateProduct.Description;
            product.ImgPath = updateProduct.ImgPath;
            product.Amount = updateProduct.Amount;
            database.context.SaveChanges();
        }

        public void AddCustomer(Customer addCustomer)
        {
            Database DB = Database.Instance;
            DB.context.Customers.Add(addCustomer);
            DB.context.SaveChanges();
        }

        public Order1 GetOrderById(int id)
        {
            Database DB = Database.Instance;
            return DB.context.Order1s.Where(o => o.Id == id).FirstOrDefault();

        }

        public void DeleteOrderDetail(OrderDetail delOrderDetail)
        {
            Database DB = Database.Instance;
            DB.context.OrderDetails.Remove(delOrderDetail);
            DB.context.SaveChanges();
            
        }

        public void DeleteProduct(int id)
        {
            Database database = Database.Instance;
            var product = database.context.Products.Where(p => p.Id == id).FirstOrDefault();
            database.context.Products.Remove(product);
            database.context.SaveChanges();

        }

        public Category GetCategoryById(int id)
        {
            Database database = Database.Instance;
            return database.context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public void UpdateCategory(Category updateCategory)
        {
            Database database = Database.Instance;
            var category = database.context.Categories.Where(c => c.Id == updateCategory.Id).FirstOrDefault();
            category.Name = updateCategory.Name;
            database.context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            Database database = Database.Instance;
            var category = database.context.Categories.Where(c => c.Id == id).FirstOrDefault();
            database.context.Categories.Remove(category);
            database.context.SaveChanges();
        }
    }
}
