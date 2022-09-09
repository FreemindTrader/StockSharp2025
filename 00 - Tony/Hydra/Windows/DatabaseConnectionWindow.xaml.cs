using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Data;
using Ecng.Xaml.Database;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class DatabaseConnectionWindow : ThemedWindow, IComponentConnector
    {
        public DatabaseConnectionWindow()
        {
            InitializeComponent();
            ConnectionStrings.SelectedConnection = DatabaseHelper.Cache.Connections.FirstOrDefault();
        }

        public DatabaseConnectionPair Pair
        {
            get
            {
                return ( DatabaseConnectionPair )ConnectionStrings.SelectedItem;
            }
            set
            {
                ConnectionStrings.SelectedItem = value;
            }
        }

        private void ConnectionStrings_OnSelectionChanged( object sender, EditValueChangedEventArgs e )
        {
            //SettingsGrid.Pair = Pair ?? new DatabaseConnectionPair();
            TestBtn.IsEnabled = OkBtn.IsEnabled = Pair != null;
        }

        private void TestBtn_OnClick( object sender, RoutedEventArgs e )
        {
            //SettingsGrid.Pair.Verify( this, true );
        }        
    }
}
