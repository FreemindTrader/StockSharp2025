using DevExpress.Xpf.Core;
using StockSharp.Xaml;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public class ThemedImageConverter : IMultiValueConverter
    {

        private readonly DrawingImage _image;

        /// <summary>
        /// </summary>
        public ThemedImageConverter( DrawingImage image )
        {
            if ( image == null )
            {
                throw new ArgumentNullException( nameof( image ) );
            }

            this._image = image;
        }

        /// <summary>
        /// </summary>
        public ThemedImageConverter()
        {
        }

        object IMultiValueConverter.Convert( object[ ] value, Type targetType, object parameter, CultureInfo culture )
        {
            DependencyObject d = value[0] as DependencyObject;
            WpfSvgPalette actualPalette = value[2] as WpfSvgPalette;
            WpfSvgPalette svgPalette = ( value[1] as ThemeTreeWalker )?.InplaceResourceProvider.GetSvgPalette( ( object )d, ( ThemeTreeWalker )null );
            if ( this._image != null )
                return ( object )ThemedImageConverter.ReplaceBrush( this._image, actualPalette, svgPalette );
            DrawingImage drawingImage = value[3] as DrawingImage;
            if ( drawingImage != null )
                return ( object )ThemedImageConverter.ReplaceBrush( drawingImage, actualPalette, svgPalette );
            string key = value[3] as string;
            if ( key == null )
                return ( object )null;
            return ( object )ThemedIconsExtension.GetImage( key );
        }

        object[ ] IMultiValueConverter.ConvertBack(
          object _param1,
          Type[ ] _param2,
          object _param3,
          CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }

        private static Brush GetActualBrush( Brush origin, WpfSvgPalette actualPalette, WpfSvgPalette basePalette )
        {
            if ( !( origin is SolidColorBrush ) )
                return ( Brush )null;
            string str = ( ( object )origin ).ToString().Remove( 1, 2 );
            Brush brush = ( Brush )null;
            if ( ( actualPalette == null ? 0 : ( actualPalette.ReplaceBrush( str, ( string )null, str, out brush ) ? 1 : 0 ) ) != 0 || basePalette == null || ( !basePalette.ReplaceBrush( str, ( string )null, str, out brush ) || actualPalette == null ) )
                return brush;
            //actualPalette.OverridesThemeColors;
            return brush;
        }

        private static DrawingImage ReplaceBrush( DrawingImage image, WpfSvgPalette actualPalette, WpfSvgPalette basePalette )
        {
            Brush brush = image.GetBrush();
            if ( brush == null )
                return image;

            Brush actualBrush = ThemedImageConverter.GetActualBrush( brush, actualPalette, basePalette );
            if ( actualBrush != null && ( ( object )actualBrush ).ToString() != ( ( object )brush ).ToString() )
            {
                image = image.Clone();
                image.UpdateBrush( actualBrush );
            }
            return image;
        }
    }
}
