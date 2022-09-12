using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System.Collections.Generic;
using System.Windows;

namespace StockSharp.Xaml
{
    internal sealed class SimpleButtonEx : SimpleButton
    {
        public static readonly DependencyProperty ColumnsDependencyProperty = DependencyProperty.Register(nameof (Columns), typeof (List<ExtendedColumnChooserColumn>), typeof (SimpleButtonEx), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty ValueDependencyProperty = DependencyProperty.Register(nameof (Value), typeof (bool), typeof (SimpleButtonEx), new PropertyMetadata((object) false));

        public SimpleButtonEx( )
        {
            this.Click += new RoutedEventHandler( this.Class404_Click );
        }

        public List<ExtendedColumnChooserColumn> Columns
        {
            get
            {
                return ( List<ExtendedColumnChooserColumn> ) this.GetValue( SimpleButtonEx.ColumnsDependencyProperty );
            }
            set
            {
                this.SetValue( SimpleButtonEx.ColumnsDependencyProperty, ( object ) value );
            }
        }

        public bool Value
        {
            get
            {
                return ( bool ) this.GetValue( SimpleButtonEx.ValueDependencyProperty );
            }
            set
            {
                this.SetValue( SimpleButtonEx.ValueDependencyProperty, ( object ) value );
            }
        }

        private void Class404_Click( object sender, RoutedEventArgs e )
        {
            if ( this.Columns == null )
            {
                return;
            }

            foreach ( ExtendedColumnChooserColumn column in this.Columns )
            {
                column.IsVisible = this.Value;
            }
        }
    }
}
