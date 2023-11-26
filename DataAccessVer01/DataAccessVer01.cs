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

        public async Task<bool> LoginAsync(string username, string password)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = ".\\SQLEXPRESS";
            builder.InitialCatalog = "MyShopDB";
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

        public void saveChanges()
        {
            Database DB = Database.Instance;
            DB.context.SaveChanges();
        }

        public void addOrder(Order1 addOrder)
        {
            Database DB = Database.Instance;
            DB.context.Order1s.Add(addOrder);
            DB.context.SaveChanges();
        }

        public void addOrderDetail(OrderDetail addOrderDetail)
        {
            Database DB = Database.Instance;
            DB.context.OrderDetails.Add(addOrderDetail);
            DB.context.SaveChanges();
        }
    }
}
