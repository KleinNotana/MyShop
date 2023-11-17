using Contract;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UIVersion01
{

    public class UIversion01 : IUserInterface
    {
        private IBus _bus;
        private Window _parentWindow;
        public string Name => "UserInterface V1";

        public string Description => "UI version01 - modern UI";

        public UserControl LoginScreen => new LoginScreen(_bus, _parentWindow);


        public void DepensOn(IBus bus, Window parent)
        {
            _bus = bus;
            _parentWindow = parent;

        }
    }
}
