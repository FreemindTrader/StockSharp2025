using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class DataSourcePanel : UserControl
    {
        private readonly IList<Tuple<string, object>> _itemSourceList;
        private Action _configureAction;


        public DataSourcePanel( )
        {
            this.InitializeComponent( );
            this.DataSourceListBoxEdit.ItemsSource = ( object )( this._itemSourceList = ( IList<Tuple<string, object>> )new ObservableCollection<Tuple<string, object>>( ) );
        }

        public IList<Tuple<string, object>> ItemsSource
        {
            get
            {
                return this._itemSourceList;
            }
        }

        public Tuple<string, object> SelectedItem
        {
            get
            {
                return ( Tuple<string, object> )this.DataSourceListBoxEdit.SelectedItem;
            }
            set
            {
                this.DataSourceListBoxEdit.SelectedItem = ( object )value;
            }
        }

        public Action Configure
        {
            get
            {
                return this._configureAction;
            }
            set
            {
                this._configureAction = value;
            }
        }

        public event Action SelectedItemChanged;

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            _configureAction?.Invoke( );
        }

        private void DataSourceListBoxEdit_SelectedIndexChanged( object sender, RoutedEventArgs e )
        {
            SelectedItemChanged?.Invoke( );
        }


    }
}
