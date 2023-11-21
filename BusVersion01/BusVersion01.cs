using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract;
using Entity;

namespace BusVersion01
{
    public class BusVersion01 : IBus
    {
        IDataAccess _data;
        public string Name => "BusVersion01";

        public string Description => "BusVersion01 - test";

        public void DepensOn(IDataAccess data)
        {
            _data = data;
        }

        public List<Product> GetProducts()
        {
            return _data.GetProducts();
        }

        public async Task<bool> Login(string username, string password)
        {
            bool result = await _data.LoginAsync(username, password);
            return result;
        }
    }
}
