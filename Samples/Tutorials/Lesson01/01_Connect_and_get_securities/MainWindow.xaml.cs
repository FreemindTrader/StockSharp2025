using StockSharp.Algo;
using StockSharp.Algo.Testing;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
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

namespace _01_Connect_and_get_securities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Connector _connector = new Connector();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Setting_Click( object sender, RoutedEventArgs e )
        {
            _connector.Configure( this );
        }

        private void Connect_Click( object sender, RoutedEventArgs e )
        {
            SecurityPicker.SecurityProvider = _connector;
            _connector.Connect();
        }
    }
}
