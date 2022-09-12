using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Xaml;
using Ecng.Xaml.DevExp;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StockSharp.Xaml.PropertyGrid
{
    public class SecuritiesEditor : ButtonEdit
    {
        public const string ServiceName = "SecuritiesSelectWindowType";

        public SecuritiesEditor( )
        {
            AllowDefaultButton = new bool?( false );
            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory( typeof( TextBlock ) );
            frameworkElementFactory.SetBinding( TextBlock.TextProperty, new Binding( )
            {
                Source = ( this ),
                Path = new PropertyPath( "EditValue.Count", new object[ 0 ] ),
                Converter = new SelectedItemsCountTextConverter( ),
                Mode = BindingMode.OneWay
            } );

            frameworkElementFactory.SetValue( FrameworkElement.MarginProperty, new Thickness( 5.0, 0.0, 0.0, 0.0 ) );

            var template = new ControlTemplate( );
            template.VisualTree = frameworkElementFactory;
            
            template.Seal( );
            IsTextEditable = new bool?( false );
            EditNonEditableTemplate = template;
            ButtonInfo buttonInfo = new ButtonInfo( ) { GlyphKind = GlyphKind.Edit, Content = LocalizedStrings.XamlStr607 };
            buttonInfo.Click += OnButtonEditClick;
            Buttons.Add( buttonInfo );
            this.AddClearButton( ArrayHelper.Empty<Security>( ) );
        }

        private void OnButtonEditClick( object sender, RoutedEventArgs e )
        {
            IEnumerable<Security> editValue = ( IEnumerable<Security> )EditValue;
            Window instance;
            ISecuritiesSelectWindow securitiesSelectWindow = ( ISecuritiesSelectWindow )( instance = ConfigManager.GetService<Type>( "SecuritiesSelectWindowType" ).CreateInstance<Window>( ) );
            securitiesSelectWindow.SecurityProvider = ServicesRegistry.SecurityProvider;
            securitiesSelectWindow.SelectedSecurities = editValue.ToArray<Security>( );
            if ( !instance.ShowModal( ) )
            {
                return;
            }

            EditValue = securitiesSelectWindow.SelectedSecurities.ToArray<Security>( );
        }
    }
}
