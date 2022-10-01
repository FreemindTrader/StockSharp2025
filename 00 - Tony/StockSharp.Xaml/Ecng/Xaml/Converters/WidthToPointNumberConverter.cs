using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// </summary>
    public class WidthToPointNumberConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            int pointNo = 0;
            if ( value is double )
            {
                double width = ( double )value;
                
                if ( !double.IsNaN( width ) )
                {
                    pointNo = ( int )( width / 500.0 + 1.0 ) * 100;                    
                }
            }
            
            return pointNo;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
