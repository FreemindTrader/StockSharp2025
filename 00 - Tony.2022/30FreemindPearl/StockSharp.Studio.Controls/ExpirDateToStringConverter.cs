
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace StockSharp.Studio.Controls
{
    internal sealed class ExpirDateToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo _ )
        {
            if ( value is DateTime )
                return ( ( DateTime )value ).ToString( "D", Thread.CurrentThread.CurrentCulture );
            if ( value == null )
                return null;
            return value.ToString();
        }

        object IValueConverter.ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
