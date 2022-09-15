using DevExpress.Xpf.Editors;
using Ecng.ComponentModel;
using StockSharp.Messages;
using StockSharp.Xaml.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml
{
    public class SecurityTypesComboBox : ComboBoxEdit
    {
        public static readonly DependencyProperty SelectedTypesProperty = DependencyProperty.Register(nameof (SelectedTypes), typeof (IEnumerable<SecurityTypes>), typeof (SecurityTypesComboBox));
        private static readonly IDictionary<SecurityTypes, string> _itemSource = Ecng.Common.Enumerator.GetValues<SecurityTypes>().ToDictionary( x => x, y =>  y.GetDisplayName() );

        public SecurityTypesComboBox( )
        {
            
            StyleSettings = ( ( BaseEditStyleSettings ) new CheckedComboBoxStyleSettings() );
            ValueMember   = "Key";
            DisplayMember = "Value";
            ItemsSource   = _itemSource ;
            SelectedTypes = Enumerable.Empty<SecurityTypes>();

            this.SetBinding( EditValueProperty, new Binding( nameof( SelectedTypes ) )
            {
                Source    = this,
                Mode      = BindingMode.TwoWay,
                Converter = new ComboBoxEditValueConverter<SecurityTypes>()
            } );
        }

        protected override void OnLoadedInternal( )
        {
            base.OnLoadedInternal();

            if ( EditMode == EditMode.Standalone )
                return;

            this.SetBinding( EditValueProperty, new Binding( "DataContext.Value" )
            {
                Source    = this,
                Mode      = BindingMode.TwoWay,
                Converter = new ComboBoxEditValueConverter<SecurityTypes>()
            } );
        }

        public IEnumerable<SecurityTypes> SelectedTypes
        {
            get
            {
                return ( IEnumerable<SecurityTypes> ) GetValue( SelectedTypesProperty );
            }
            set
            {
                SetValue( SelectedTypesProperty, value.ToArray() );
            }
        }       
    }
}
