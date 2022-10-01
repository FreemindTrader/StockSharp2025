using Ecng.Collections;
using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StockSharp.Xaml.Converters
{
    public class ColoredIconConverter : IMultiValueConverter
    {
        private static readonly Dictionary<string, DrawingImage> _imagesDict = new Dictionary<string, DrawingImage>();
        private static readonly Dictionary<string, Pen> _pensDict = new Dictionary<string, Pen>();

        object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {            
            Brush brush = (Brush) values[0];
            Brush myBrush = values.Length > 1 ? ( Brush ) values[ 1 ] : null;

            string imageKey = (string) parameter;

            if ( brush == null || imageKey == null )
            {
                return null;
            }

            if ( myBrush == null )
            {
                myBrush = brush;
            }

            string str = myBrush.ToString();
            string key2 = imageKey + (object) brush + str;

            DrawingImage drawingImage;
            if ( ColoredIconConverter._imagesDict.TryGetValue( key2, out drawingImage ) )
            {
                return ( object ) drawingImage;
            }

            DrawingImage image = IconsExtension.GetImage( imageKey );

            if ( image == null )
            {
                ColoredIconConverter._imagesDict[ key2 ] = ( DrawingImage ) null;
                return ( object ) null;
            }

            drawingImage = image.Clone();
            XamlHelper.UpdateBrush( drawingImage, brush );
            XamlHelper.UpdatePen( drawingImage, ( Pen ) _pensDict.SafeAdd<string, Pen>(  str, s => new Pen( myBrush, 1.0 ) ) );
            ColoredIconConverter._imagesDict[ key2 ] = drawingImage;

            return ( object ) drawingImage;
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException( );
        }        
    }
}
