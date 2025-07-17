// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.AxisAlignmentToHorizontalAnchorPointConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting
{
    public class AxisAlignmentToHorizontalAnchorPointConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            AxisAlignment axisAlignment = (AxisAlignment) value;
            HorizontalAnchorPoint horizontalAnchorPoint = HorizontalAnchorPoint.Left;
            if ( ( uint ) ( axisAlignment - 2 ) <= 1U )
                horizontalAnchorPoint = HorizontalAnchorPoint.Center;
            return ( object ) horizontalAnchorPoint;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
