using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// Get value by index from the specified <see cref="T:System.Collections.IList" /> converter.
    /// </summary>
    public class IndexerConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            IList list = value as IList;
            if ( list == null )
                return ( object )DependencyProperty.UnsetValue;
            if ( !( parameter is int ) )
                return ( object )DependencyProperty.UnsetValue;
            int index = ( int )parameter;
            if ( index >= list.Count )
                return ( object )DependencyProperty.UnsetValue;
            return list[index];
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
