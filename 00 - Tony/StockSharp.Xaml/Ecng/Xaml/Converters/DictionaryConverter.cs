using Ecng.Common;
using Ecng.Reflection;
using StockSharp.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// Get value by key from the specified <see cref="T:System.Collections.IDictionary" /> converter.
    /// </summary>
    public class DictionaryConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            IDictionary dictionary = ( IDictionary )value;
            Type type = dictionary != null ? ReflectionHelper.GetGenericType( dictionary.GetType(), typeof( IDictionary<,> ) ) : ( Type )null;
            if ( type == ( Type )null )
                return ( object )DependencyProperty.UnsetValue;
            object key;
            try
            {
                key = Converter.To( parameter, type.GetGenericArguments()[0] );
            }
            catch ( Exception ex )
            {
                LoggingHelper.LogError( ex, ( string )null );
                return ( object )DependencyProperty.UnsetValue;
            }
            if ( dictionary.Contains( key ) )
                return dictionary[key];
            return ( object )DependencyProperty.UnsetValue;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
