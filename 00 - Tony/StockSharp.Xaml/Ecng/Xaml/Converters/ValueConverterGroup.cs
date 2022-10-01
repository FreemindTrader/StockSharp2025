using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// </summary>
    public class ValueConverterGroup : List<IValueConverter>, IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return Enumerable.Aggregate<IValueConverter, object>( this,  value, (o, p ) => p.Convert( o, targetType, parameter, culture ) );
        }

        object IValueConverter.ConvertBack(
          object _param1,
          Type _param2,
          object _param3,
          CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }        
    }
}
