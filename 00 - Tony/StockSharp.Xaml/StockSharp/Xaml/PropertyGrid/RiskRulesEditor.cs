using DevExpress.Xpf.Editors;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Risk;
using StockSharp.Xaml.Converters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StockSharp.Xaml.PropertyGrid
{
    public class RiskRulesEditor : ButtonEdit
    {
        public RiskRulesEditor( )
        {
            var frameworkElementFactory = new FrameworkElementFactory(typeof (TextBlock));
            frameworkElementFactory.SetBinding( TextBlock.TextProperty, ( BindingBase ) new Binding()
            {
                Source = this,
                Path = new PropertyPath( "EditValue.Count", new object[ 0 ] ),
                Converter = ( IValueConverter ) new SelectedItemsCountTextConverter(),
                Mode = BindingMode.OneWay
            } );
            frameworkElementFactory.SetValue( FrameworkElement.MarginProperty, new Thickness( 5.0, 0.0, 0.0, 0.0 ) );
            var template = new ControlTemplate();
            template.VisualTree = frameworkElementFactory;
            template.Seal();

            IsTextEditable = false;

            EditNonEditableTemplate = ( template );
            DefaultButtonClick += RiskRulesEditor_DefaultButtonClick;
        }

        

        private void RiskRulesEditor_DefaultButtonClick( object sender, RoutedEventArgs e )
        {
            var editValues = (IEnumerable<IRiskRule>) EditValue;

            var riskWindow = new RiskWindow();

            foreach ( var editValue in editValues )
            {
                riskWindow.Rules.Add( editValue.Clone() );
            }

            if ( !riskWindow.ShowModal( Application.Current.GetActiveOrMainWindow() ) )
            {
                return;
            }

            EditValue = ( ( ( IEnumerable<IRiskRule> ) riskWindow.Rules ).ToArray<IRiskRule>() );

        }        
    }
}
