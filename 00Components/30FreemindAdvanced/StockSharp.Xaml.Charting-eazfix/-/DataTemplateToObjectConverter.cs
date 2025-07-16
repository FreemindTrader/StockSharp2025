using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    public class DataTemplateToObjectConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) ( ( ( FrameworkTemplate ) value ).LoadContent() as FrameworkElement );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
