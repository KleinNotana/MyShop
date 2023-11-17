using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IDataAccess
    {
        string Name { get; }
        string Description { get; }

        public Task<bool> LoginAsync(string username, string password);

    }
}
