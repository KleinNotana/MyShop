using System;
using System.Threading.Tasks;
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
            //var connection = new SqlConnection(connectionString);
           
            //run in background thread

            bool canLogin = await Database.canConnect(connectionString);

            if (canLogin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
