using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<Customer> GetCustomer();

        public void saveChanges();

    }
}
