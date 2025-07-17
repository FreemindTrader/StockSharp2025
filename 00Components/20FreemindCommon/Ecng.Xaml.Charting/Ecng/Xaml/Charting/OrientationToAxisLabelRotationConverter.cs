// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.OrientationToAxisLabelRotationConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml.Charting
{
    public class OrientationToAxisLabelRotationConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            Orientation orientation = (Orientation) value;
            return ( object ) new RotateTransform() { Angle = ( orientation == Orientation.Horizontal ? 0.0 : -90.0 ) };
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
