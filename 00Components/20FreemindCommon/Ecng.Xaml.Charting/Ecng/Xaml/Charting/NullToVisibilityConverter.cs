// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.NullToVisibilityConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    public class NullToVisibilityConverter : IValueConverter
    {
        private const string InvertionFlag = "INVERSE";

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool flag = string.Equals(parameter as string, "INVERSE", StringComparison.InvariantCultureIgnoreCase);
            Visibility visibility1 = flag ? Visibility.Visible : Visibility.Collapsed;
            Visibility visibility2 = flag ? Visibility.Collapsed : Visibility.Visible;
            return ( object ) ( Visibility ) ( value == null ? ( int ) visibility1 : ( int ) visibility2 );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
