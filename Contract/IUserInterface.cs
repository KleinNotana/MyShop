using System;
using System.Windows;
using System.Windows.Controls;

namespace Contract
{
    public interface IUserInterface
    {
        string Name { get; }
        string Description { get; }

        Window LoginWindow { get; }

        void DepensOn(IBus bus);

    }
}
