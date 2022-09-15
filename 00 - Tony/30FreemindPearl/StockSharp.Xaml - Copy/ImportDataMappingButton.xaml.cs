using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Import;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ImportDataMappingButton : Button
    {
        public static DependencyProperty DataTypeProperty  = DependencyProperty.Register(nameof (DataType), typeof (Type), typeof (ImportDataMappingButton), new PropertyMetadata(new PropertyChangedCallback( OnDataTypePropertyChanged )));
        public static DependencyProperty ValuesProperty    = DependencyProperty.Register(nameof (Values), typeof (ObservableCollection<FieldMappingValue>), typeof (ImportDataMappingButton), new PropertyMetadata( OnValuesPropertyChanged ));
        public static DependencyProperty FieldNameProperty = DependencyProperty.Register(nameof (FieldName), typeof (string), typeof (ImportDataMappingButton));
        public ImportDataMappingButton( )
        {
            InitializeComponent();
        }

        public Type DataType
        {
            get
            {
                return ( Type ) GetValue( DataTypeProperty );
            }
            set
            {
                SetValue( DataTypeProperty, value );
            }
        }

        public ObservableCollection<FieldMappingValue> Values
        {
            get
            {
                return ( ObservableCollection<FieldMappingValue> ) GetValue( ValuesProperty );
            }
            set
            {
                SetValue( ValuesProperty, ( object ) value );
            }
        }

        public string FieldName
        {
            get
            {
                return ( string ) GetValue( FieldNameProperty );
            }
            set
            {
                SetValue( FieldNameProperty, ( object ) value );
            }
        }

        private static void OnDataTypePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ImportDataMappingButton dataMappingButton;

            if ( ( dataMappingButton = d as ImportDataMappingButton ) == null )
                return;

            Type newValue = (Type) e.NewValue;

            dataMappingButton.IsEnabled = newValue != null && ( newValue.IsEnum || newValue == typeof( bool ) );
        }

        

        private static void OnValuesPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ImportDataMappingButton dataMappingButton;
            if ( ( dataMappingButton = d as ImportDataMappingButton ) == null )
                return;

            dataMappingButton.UpdateButtonUI();
        }

        protected override void OnClick( )
        {
            base.OnClick();
            var datamap = new ImportDataMappingWindow() { DataType = this.DataType };

            datamap.Values.AddRange( Values.Select( x => x.Clone() ) );


            datamap.Title = datamap.Title.Put( FieldName );

            if( !datamap.ShowModal( this ) )
                return;
            
            Values.Clear();
            Values.AddRange( datamap.Values );
            
            this.UpdateButtonUI();
        }


        private void UpdateButtonUI( )
        {
            var values = this.Values;

            string[] stringArray;

            if ( values == null )
            {
                stringArray = null;
            }
            else
            {
                stringArray = values.Select( x => string.Format( "{0} -> {1}", x.ValueFile, (( DataType != null ) || !this.DataType.IsEnum ) ? x.ValueStockSharp : x.ValueStockSharp.GetDisplayName() )  ).ToArray<string>();
                
            }

            if ( stringArray != null )
            {
                stringArray = new string[ 0 ];
            }
                
            
            this.Content = stringArray.IsEmpty( ) ? "..." : stringArray.Join( ", " );
            this.ToolTip = stringArray.IsEmpty( ) ? null : stringArray.Join( Environment.NewLine );
        }

        
    }
}
