// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.AxisAlignmentToVerticalAlignmentConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace Ecng.Xaml.Charting
{
    public class AxisAlignmentToVerticalAlignmentConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            AxisAlignment axisAlignment = (AxisAlignment) value;
            VerticalAlignment verticalAlignment = VerticalAlignment.Top;
            switch ( axisAlignment )
            {
                case AxisAlignment.Right:
                case AxisAlignment.Left:
                    verticalAlignment = VerticalAlignment.Center;
                    break;
                case AxisAlignment.Top:
                    verticalAlignment = VerticalAlignment.Top;
                    break;
                case AxisAlignment.Bottom:
                    verticalAlignment = VerticalAlignment.Bottom;
                    break;
            }
            return ( object ) verticalAlignment;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
