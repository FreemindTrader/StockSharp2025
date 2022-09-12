using DevExpress.Xpf.Editors;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Commissions;
using StockSharp.Xaml.Converters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StockSharp.Xaml.PropertyGrid
{
    public class CommissionRulesEditor : ButtonEdit
    {
        public CommissionRulesEditor( )
        {            
            var frameworkElementFactory = new FrameworkElementFactory(typeof (TextBlock));
            frameworkElementFactory.SetBinding( TextBlock.TextProperty, ( BindingBase ) new Binding()
            {
                Source    =  this,
                Path      = new PropertyPath( "EditValue.Count", new object[ 0 ] ),
                Converter = ( IValueConverter ) new SelectedItemsCountTextConverter(),
                Mode      = BindingMode.OneWay
            } );
            frameworkElementFactory.SetValue( FrameworkElement.MarginProperty,  new Thickness( 5.0, 0.0, 0.0, 0.0 ) );
            var template = new ControlTemplate();
            template.VisualTree = frameworkElementFactory;            
            template.Seal();

            IsTextEditable = false;

            EditNonEditableTemplate = ( template );
            DefaultButtonClick += CommissionRulesEditor_DefaultButtonClick;
        }

        private void CommissionRulesEditor_DefaultButtonClick( object sender, RoutedEventArgs e )
        {
            var editValues = (IEnumerable<CommissionRule>) ((BaseEdit) this).EditValue;

            var commissionWindow = new CommissionWindow();

            foreach ( var editValue in editValues )
            {
                commissionWindow.Rules.Add( editValue.Clone( ) );
            }
            
            if ( ! commissionWindow.ShowModal( Application.Current.GetActiveOrMainWindow(  ) ) )
            {
                return;
            } 
            
            EditValue = (  ( ( IEnumerable ) commissionWindow.Rules ).OfType<CommissionRule>().ToArray() );
        }
    }
}
