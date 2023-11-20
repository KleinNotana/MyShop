using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public static class Database
    {
        
        static MyShopDbContext context;

        public static async Task<bool> canConnect(string connectionString)
        {
            context = new MyShopDbContext(connectionString);
            return await context.Database.CanConnectAsync();
        }

    }
}
