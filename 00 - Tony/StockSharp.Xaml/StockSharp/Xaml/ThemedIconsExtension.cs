using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Xaml
{
    /// <summary>Xaml extension to use themed SVG icons.</summary>
    public class ThemedIconsExtension : SvgImageSourceExtension
    {
        
        private string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.ThemedIconsExtension" />.
        /// </summary>
        public ThemedIconsExtension()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.ThemedIconsExtension" />.
        /// </summary>
        public ThemedIconsExtension( string key )
        {

            this.Key = key;
        }

        private static Uri GetIconUri( string icon )
        {
            return new Uri( "pack://application:,,,/StockSharp.Xaml;component/IconsSvg/" + icon + ".svg" );
        }

        /// <summary>Icon key.</summary>
        public string Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
                this.Uri = ThemedIconsExtension.GetIconUri( this._key );
            }
        }

        /// <summary>Get icon image source.</summary>
        /// <param name="key">Icon key.</param>
        /// <returns>Image.</returns>
        public static ImageSource GetImage( string key )
        {
            return WpfSvgRenderer.CreateImageSource( ThemedIconsExtension.GetIconUri( key ), ( Uri )null, new Size?(), ( WpfSvgPalette )null, ( string )null, new bool?(), true, true );
        }
    }
}
