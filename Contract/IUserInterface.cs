using System;
using System.Windows.Controls;

namespace Contract
{
    public interface IUserInterface
    {
        string Name { get; }
        string Description { get; }

        UserControl LoginScreen { get; }

        void DepensOn(IBus bus);

    }
}
