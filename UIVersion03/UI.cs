using System;
using System.Windows;
using System.Windows.Controls;
using Contract;

namespace UIVersion03
{
    public class UI : IUserInterface
    {
        private IBus _bus;
        public string Name => "UserInterface Version 02";

        public string Description => "UserInterface Version 02 - only use windows";

        public Window LoginWindow => new LoginWindow(_bus);

        public void DepensOn(IBus bus)
        {
            _bus = bus;
        }
    }
}
