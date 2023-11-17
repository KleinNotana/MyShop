using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IBus
    {
        string Name { get; }
        string Description { get; }

        Task<bool> Login(string username, string password);

        void DepensOn(IDataAccess data);
    }
}
