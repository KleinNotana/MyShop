using System;
using Contract;

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

        public bool Login(string username, string password)
        {
            Result res = new Result();
            _data.LoginAsync(username, password, res);
            return res.result;
        }
    }
}
