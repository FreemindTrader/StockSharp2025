using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using Ecng.Common;

using StockSharp.BusinessEntities;

using StockSharp.Localization;

namespace StockSharp.Xaml
{

    internal sealed class BoolToFontWeightConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool? boolValue = value as bool?;
            return ( object ) ( boolValue.GetValueOrDefault( ) & boolValue.HasValue ? FontWeights.Bold : FontWeights.Normal );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException( );
        }
    }
}
