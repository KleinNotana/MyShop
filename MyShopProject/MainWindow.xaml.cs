using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Contract;

namespace MyShopProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        IBus _bus = null;
        IUserInterface _ui = null;
        IDataAccess _data = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo[] files = new DirectoryInfo(exeFolder).GetFiles("*.dll");

            foreach(var file in files)
            {
                var assembly = System.Reflection.Assembly.LoadFile(file.FullName);
                var types = assembly.GetTypes();
                foreach(var type in types)
                {
                    if (type.IsClass)
                    {
                        if(typeof(IBus).IsAssignableFrom(type))
                        {
                            _bus = (IBus)Activator.CreateInstance(type);
                        }
                        else if (typeof(IUserInterface).IsAssignableFrom(type))
                        {
                            _ui = (IUserInterface)Activator.CreateInstance(type);
                        }
                        else if (typeof(IDataAccess).IsAssignableFrom(type))
                        {
                            _data = (IDataAccess)Activator.CreateInstance(type);
                        }
                    }
                }   
            }


            if (_bus == null || _data == null || _ui == null)
            {
                MessageBox.Show("Error load dll files");
            }
            else
            {
                _bus.DepensOn(_data);
                _ui.DepensOn(_bus);

                Window window = _ui.LoginWindow;
                window.Show();
                this.Close();
            }
      
        }
    }
}
