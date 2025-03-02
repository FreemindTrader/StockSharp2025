using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class SelectDataTypesWindow : ThemedWindow, IComponentConnector
    {        
        public SelectDataTypesWindow()
        {
            InitializeComponent();
        }

        public IEnumerable<VisualDataType> DataTypes
        {
            get
            {
                return ( IEnumerable<VisualDataType> )DataTypesCtrl.ItemsSource;
            }
            set
            {
                DataTypesCtrl.ItemsSource = value;
            }
        }

        public IEnumerable<VisualDataType> SelectedDataTypes
        {
            get
            {
                return DataTypesCtrl.SelectedItems.Cast<VisualDataType>();
            }
        }

        private void DataTypesCtrl_OnSelectedIndexChanged( object sender, RoutedEventArgs e )
        {
            OkBtn.IsEnabled = SelectedDataTypes.Any();
        }        
    }
}
