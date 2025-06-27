using Ecng.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

internal sealed class FrameworkElementConverter : IValueConverter
{
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
        Type type = ( Type )value;
        if ( ( object )type == null )
            return null;
        return type.CreateInstance<FrameworkElement>( );
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
        throw new NotSupportedException( );
    }
}
