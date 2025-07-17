// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.AxisAlignmentToVeticalAnchorPointConverter
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;
namespace fx.Xaml.Charting
{
    public class AxisAlignmentToVeticalAnchorPointConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            AxisAlignment axisAlignment = (AxisAlignment) value;
            VerticalAnchorPoint verticalAnchorPoint = VerticalAnchorPoint.Center;
            if ( ( uint ) ( axisAlignment - 2 ) <= 1U )
                verticalAnchorPoint = VerticalAnchorPoint.Top;
            return ( object ) verticalAnchorPoint;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
