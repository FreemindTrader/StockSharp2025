// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.AxisAlignmentToAxisOrientationConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting
{
    public class AxisAlignmentToAxisOrientationConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            AxisAlignment axisAlignment = (AxisAlignment) value;
            bool flag = parameter is string;
            return ( object ) ( Orientation ) ( axisAlignment == AxisAlignment.Left || axisAlignment == AxisAlignment.Right ? ( flag ? 0 : 1 ) : ( flag ? 1 : 0 ) );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
