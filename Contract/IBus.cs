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

        Task<bool> Login(string username, string password);

        List<Order1> GetOrders();

        List<Customer> GetCustomers();
        void saveChanges();
        BindingList<Product> GetProducts();
        BindingList<dynamic> GetProductsDynamic();
        IEnumerable<dynamic> GetProductsByName(string name);
        IEnumerable<dynamic> GetProductsByFilter(string name, string sortType, int priceFrom = -1, int priceTo = -1, int currentPage = 1, int itemPerPage = 10);
        void DepensOn(IDataAccess data);
    }
}
