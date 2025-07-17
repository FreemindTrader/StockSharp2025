// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ValueMultiplicatorConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    public class ValueMultiplicatorConverter : IValueConverter
    {
        private const string DefaultMultiplier = "1";

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            double num = (double) value;
            string s = (string) parameter ?? "1";
            double result;
            if ( double.TryParse( s, NumberStyles.Any, ( IFormatProvider ) culture, out result ) )
                return ( object ) ( num * result );
            if ( double.TryParse( s, NumberStyles.Any, ( IFormatProvider ) CultureInfo.InvariantCulture, out result ) )
                return ( object ) ( num * result );
            return ( object ) null;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
