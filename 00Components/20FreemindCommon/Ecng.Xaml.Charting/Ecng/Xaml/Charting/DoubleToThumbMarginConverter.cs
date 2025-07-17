// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.DoubleToThumbMarginConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    public class DoubleToThumbMarginConverter : IValueConverter
    {
        private const string IsVertical = "VERTICAL";

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            double num1 = (double) value;
            bool flag = string.Equals(parameter as string, "VERTICAL", StringComparison.InvariantCultureIgnoreCase);
            double num2 = num1 / 2.0;
            return ( object ) ( flag ? new Thickness( 0.0, -num2, 0.0, -num2 ) : new Thickness( -num2, 0.0, -num2, 0.0 ) );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
