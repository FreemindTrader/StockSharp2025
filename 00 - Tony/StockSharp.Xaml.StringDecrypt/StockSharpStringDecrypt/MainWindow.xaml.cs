using DevExpress.Xpf.Core;
using Ecng.Xaml;
using Ecng.Xaml.Database;
using MoreLinq;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace StockSharpStringDecrypt
{
    public partial class MainWindow : Window, IComponentConnector
    {        

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            IMessageAdapter messageAdapter = null;

            var some = new ConnectorInfo( messageAdapter );




            //var cb = new SubsetComboBox( );

            //var key = SubsetComboBox.SubsetComboBoxStyleKey;

            //var test = new IconUriBindingExtension( bind );

            //test.ProvideValue( null );
        }
    }
}


