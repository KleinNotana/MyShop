using Contract;
using System;
using System.Windows.Controls;

namespace UIVersion01
{

    public class UIversion01 : IUserInterface
    {
        private IBus _bus;
        public string Name => "UserInterface V1";

        public string Description => "UI version01 - modern UI";

        public UserControl LoginScreen => new LoginScreen(_bus);


        public void DepensOn(IBus bus)
        {
            _bus = bus;
        }
    }
}
