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
    public class Level1FieldsComboBox : ComboBoxEdit
    {
        private static readonly IDictionary<Level1Fields, string> _allFields =  ( Ecng.Common.Enumerator.GetValues<Level1Fields>()).ToDictionary( t=>t, t=>t.GetDisplayName());
        private static readonly IEnumerable<Level1Fields> _defaultFields = _allFields.Keys.Except(new[]
            {
                Level1Fields.LastTrade,
                Level1Fields.LastTradeId,
                Level1Fields.LastTradeOrigin,
                Level1Fields.LastTradePrice,
                Level1Fields.LastTradeTime,
                Level1Fields.LastTradeUpDown,
                Level1Fields.LastTradeVolume,
                Level1Fields.BestBid,
                Level1Fields.BestBidPrice,
                Level1Fields.BestBidTime,
                Level1Fields.BestBidVolume,
                Level1Fields.BestAsk,
                Level1Fields.BestAskPrice,
                Level1Fields.BestAskTime,
                Level1Fields.BestAskVolume
            }).ToArray();

        public static readonly DependencyProperty SelectedFieldsProperty = DependencyProperty.Register(nameof (SelectedFields), typeof (IEnumerable<Level1Fields>), typeof (Level1FieldsComboBox), (PropertyMetadata) new UIPropertyMetadata((object) DefaultFields));

        public Level1FieldsComboBox( )
        {
            if ( this.IsDesignMode() )
            {
                return;
            }

            StyleSettings   = new CheckedComboBoxStyleSettings();
            SeparatorString = string.Empty;
            ValueMember     = "Key";
            DisplayMember   = "Value";
            var tb          = new FrameworkElementFactory(typeof (TextBlock));

            tb.SetBinding( TextBlock.TextProperty, new Binding()
            {
                Source =  this,
                Path = new PropertyPath( "SelectedItems.Count" ),
                Converter = ( IValueConverter ) new SelectedItemsCountTextConverter(),
                Mode = BindingMode.OneWay
            } );

            tb.SetValue( FrameworkElement.MarginProperty, new Thickness( 5.0, 0.0, 0.0, 0.0 ) );
            tb.SetValue( FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center );

            var template = new ControlTemplate();
            template.VisualTree = tb;
            
            template.Seal();
            IsTextEditable = false;
            EditNonEditableTemplate =  template;
            ItemsSource = _allFields;

            BindingOperations.SetBinding( this, EditValueProperty, new Binding( nameof( SelectedFields ) )
            {
                Source              = this,
                Mode                = BindingMode.TwoWay,
                Converter           = new ComboBoxEditValueConverter<Level1Fields>(),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            } );
        }

        protected override void OnLoadedInternal( )
        {
            base.OnLoadedInternal();

            if ( EditMode == EditMode.Standalone )
            {
                return;
            }

            BindingOperations.SetBinding( this, EditValueProperty, new Binding( "DataContext.Value" )
            {
                Source = this,
                Mode = BindingMode.TwoWay,
                Converter = new ComboBoxEditValueConverter<Level1Fields>()
            } );
        }

        public IEnumerable<Level1Fields> SelectedFields
        {
            get
            {
                return ( IEnumerable<Level1Fields> ) GetValue( SelectedFieldsProperty );
            }
            set
            {
                SetValue( SelectedFieldsProperty,  value.ToArray() );
            }
        }

        public static IEnumerable<Level1Fields> DefaultFields
        {
            get
            {
                return _defaultFields;
            }
        }        
    }
}
