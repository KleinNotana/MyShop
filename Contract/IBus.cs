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

        List<Product> GetProducts();
        List<Order1> GetOrders();

        List<Customer> GetCustomers();
        void saveChanges();
        BindingList<Product> GetProducts();
        void DepensOn(IDataAccess data);
    }
}
