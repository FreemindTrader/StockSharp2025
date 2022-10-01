using DevExpress.Xpf.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace StockSharp.Xaml
{
    /// <summary>
    /// This class is used to register attached property <see cref="F:StockSharp.Xaml.Doc.UrlProperty" />.
    /// </summary>
    public static class Doc
    {
        /// <summary>Attached property for help button.</summary>
        public static readonly DependencyProperty UrlProperty = DependencyProperty.RegisterAttached( "Url", typeof( string ), typeof( Doc ), new PropertyMetadata( null, new PropertyChangedCallback( OnUrlPropertyChanged ) ) );

        private static void OnUrlPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ich = d as IHeaderItemsControlHost;
            if ( ich == null || ich.HeaderItems.Any<object>(  x => x is WindowHelpButton ) )
            {
                return;
            }
                
            ObservableCollection<object> headerItems = ich.HeaderItems;
            var btn = new WindowHelpButton();
            btn.DataContext = ich;

            headerItems.Add( btn );
        }

        /// <summary>
        /// Set attached property value for <see cref="F:StockSharp.Xaml.Doc.UrlProperty" />.
        /// </summary>
        public static void SetUrl( UIElement element, string value )
        {
            element.SetValue( Doc.UrlProperty, ( object )value );
        }

        /// <summary>
        /// Get attached property value for <see cref="F:StockSharp.Xaml.Doc.UrlProperty" />.
        /// </summary>
        public static string GetUrl( UIElement element )
        {
            return ( string )element.GetValue( Doc.UrlProperty );
        }        
    }
}
