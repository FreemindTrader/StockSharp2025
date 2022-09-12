using DevExpress.Xpf.Editors;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Messages;
using StockSharp.Xaml.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StockSharp.Xaml
{
    public class PositionChangeTypesComboBox : ComboBoxEdit
    {
        private static readonly IDictionary<PositionChangeTypes, string> _itemSource =  Ecng.Common.Enumerator.GetValues<PositionChangeTypes>().Where( p => !p.IsObsolete() ).ToDictionary( x => x, n => n.GetDisplayName() );
        private static readonly IEnumerable<PositionChangeTypes> _defaultTypes = (IEnumerable<PositionChangeTypes>) new PositionChangeTypes[]
        {
            PositionChangeTypes.BeginValue,
            PositionChangeTypes.CurrentValue,
            PositionChangeTypes.BlockedValue,
            PositionChangeTypes.Commission,
            PositionChangeTypes.AveragePrice
        };
        public static readonly DependencyProperty SelectedTypesProperty = DependencyProperty.Register(nameof (SelectedTypes), typeof (IEnumerable<PositionChangeTypes>), typeof (PositionChangeTypesComboBox), (PropertyMetadata) new UIPropertyMetadata((object) PositionChangeTypesComboBox.DefaultTypes));

        public PositionChangeTypesComboBox( )
        {            
            if ( XamlHelper.IsDesignMode( ( DependencyObject ) this ) )
                return;

            StyleSettings       = new CheckedComboBoxStyleSettings();
            SeparatorString     = string.Empty;
            ValueMember         = "Key";
            DisplayMember       = "Value";
            var txtBlockElement = new FrameworkElementFactory(typeof (TextBlock));

            txtBlockElement.SetBinding( TextBlock.TextProperty, new Binding()
            {
                Source    = this,
                Path      = new PropertyPath( "SelectedItems.Count"  ),
                Converter =  new SelectedItemsCountTextConverter(),
                Mode      = BindingMode.OneWay
            } );

            txtBlockElement.SetValue( FrameworkElement.MarginProperty, ( object ) new Thickness( 5.0, 0.0, 0.0, 0.0 ) );

            var template = new ControlTemplate();
            template.VisualTree     = txtBlockElement;
            template.Seal();
            IsTextEditable          = false;
            EditNonEditableTemplate = template;
            ItemsSource             = _itemSource;

            BindingOperations.SetBinding( ( DependencyObject ) this, ( DependencyProperty ) BaseEdit.EditValueProperty, ( BindingBase ) new Binding( nameof( SelectedTypes ) )
            {
                Source              = ( object ) this,
                Mode                = BindingMode.TwoWay,
                Converter           = ( IValueConverter ) new ComboBoxEditValueConverter<PositionChangeTypes>(),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
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
                Converter = new ComboBoxEditValueConverter<PositionChangeTypes>()
            } );
        }

        public IEnumerable<PositionChangeTypes> SelectedTypes
        {
            get
            {
                return ( IEnumerable<PositionChangeTypes> ) ( ( DependencyObject ) this ).GetValue( PositionChangeTypesComboBox.SelectedTypesProperty );
            }
            set
            {
                ( ( DependencyObject ) this ).SetValue( PositionChangeTypesComboBox.SelectedTypesProperty, ( object ) value.ToArray<PositionChangeTypes>() );
            }
        }

        public static IEnumerable<PositionChangeTypes> DefaultTypes
        {
            get
            {
                return PositionChangeTypesComboBox._defaultTypes;
            }
        }        
    }
}
