using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessVer01
{
    public  class Database
    {
        //singleton 
        private static Database _instance;
        public MyShopDbContext context;
        private Database()
        {
            
        }
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }
    }
}
