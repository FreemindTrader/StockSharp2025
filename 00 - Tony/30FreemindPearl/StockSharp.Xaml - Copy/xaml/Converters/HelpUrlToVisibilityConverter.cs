using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using Ecng.Common;

using StockSharp.BusinessEntities;

using StockSharp.Localization;

namespace StockSharp.Xaml
{
    internal sealed class HelpUrlToVisibilityConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) ( Visibility ) ( value != null ? 0 : 2 );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }        
    }    
}
