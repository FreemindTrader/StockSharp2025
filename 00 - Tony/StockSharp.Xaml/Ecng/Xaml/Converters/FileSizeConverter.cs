using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>File size to human readable text converter.</summary>
    public class FileSizeConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            long? longSize = value as long?;
            if ( !longSize.HasValue )
            {
                int? intSize = value as int?;
                longSize = intSize.HasValue ? new long?( ( long )intSize.GetValueOrDefault() ) : new long?();
            }
            if ( !longSize.HasValue )
                return Binding.DoNothing;

            return ( object )IOHelper.ToHumanReadableFileSize( longSize.Value );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
