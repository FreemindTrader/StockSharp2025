// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.AxisAlignmentToVisibilityConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting
{
    public class AxisAlignmentToVisibilityConverter : IValueConverter
    {
        public bool IsLeftPointer
        {
            get; set;
        }

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            AxisAlignment axisAlignment = (AxisAlignment) value;
            return ( object ) ( Visibility ) ( !this.IsLeftPointer ? ( axisAlignment == AxisAlignment.Left || axisAlignment == AxisAlignment.Top ? 0 : 2 ) : ( axisAlignment == AxisAlignment.Left || axisAlignment == AxisAlignment.Top ? 2 : 0 ) );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
