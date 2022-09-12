using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo.Import;
using StockSharp.Localization;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ImportDataMappingWindow : DXWindow
    {
        private readonly ObservableCollection< FieldMappingValue > _fieldMapping = new ObservableCollection< FieldMappingValue >( );

        private Type _dataType;

        public ImportDataMappingWindow( )
        {
            InitializeComponent();

            ValuesGrid.ItemsSource = _fieldMapping;
        }

        public Type DataType
        {
            get
            {
                return this._dataType;
            }
            set
            {
                this._dataType = value;

                IEnumerable<object> parameters;

                if ( !this.DataType.IsEnum )
                {
                    parameters = ( IEnumerable<object> ) new object[ 2 ]
                    {
                        true,
                        false
                    };
                }                    
                else
                {
                    parameters = Ecng.Common.Enumerator.GetValues( this.DataType );
                }
                    
                DataContext =  parameters;
            }
        }

        public ObservableCollection<FieldMappingValue> Values
        {
            get
            {
                return _fieldMapping;
            }
        }

        private void AddRow_Click( object sender, RoutedEventArgs e )
        {
            Values.Add( new FieldMappingValue( ) );
        }

        private void RemoveRow_Click( object sender, RoutedEventArgs e )
        {
            Values.RemoveRange( ValuesGrid.SelectedItems.Cast< FieldMappingValue >().ToArray() );
            
        }

        private void Ok_Click( object sender, RoutedEventArgs e )
        {
            var messageBoxBuilder = new MessageBoxBuilder().Owner((Window) this).Error();
            if ( Values.Any( x => x.ValueFile.IsEmpty() ) )
            {
                messageBoxBuilder.Text( LocalizedStrings.Str2832 ).Show();
            }
            else if ( Values.Any( x => x.ValueStockSharp == null ) )
            {
                messageBoxBuilder.Text( LocalizedStrings.Str2833 ).Show();
            }
            else
            {
                DialogResult = true;
            }
        }

        private void ValuesGrid_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            RemoveRow.IsEnabled = ValuesGrid.SelectedItems.Cast<FieldMappingValue>().ToArray().Any<FieldMappingValue>();
        }
    }
}
