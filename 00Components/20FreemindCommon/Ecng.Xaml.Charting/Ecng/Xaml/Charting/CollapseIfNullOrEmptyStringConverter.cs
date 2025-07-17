// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.CollapseIfNullOrEmptyStringConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    public class CollapseIfNullOrEmptyStringConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool flag = value != null;
            string str = value as string;
            if ( str != null )
                flag &= !string.IsNullOrEmpty( str );
            return ( object ) ( Visibility ) ( flag ? 0 : 2 );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
