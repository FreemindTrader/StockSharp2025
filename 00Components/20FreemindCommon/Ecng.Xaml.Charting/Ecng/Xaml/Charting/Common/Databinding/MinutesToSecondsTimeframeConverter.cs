// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Databinding.MinutesToSecondsTimeframeConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting.Common.Databinding
{
    public class MinutesToSecondsTimeframeConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( !( value is int ) )
                return ( object ) -1;
            return ( object ) ( ( int ) value * 60 );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
