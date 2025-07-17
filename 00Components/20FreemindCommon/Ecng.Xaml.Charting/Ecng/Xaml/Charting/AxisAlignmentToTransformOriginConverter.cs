// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.AxisAlignmentToTransformOriginConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting
{
    public class AxisAlignmentToTransformOriginConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            switch ( ( AxisAlignment ) value )
            {
                case AxisAlignment.Right:
                    return ( object ) new Point( 0.0, 0.0 );
                case AxisAlignment.Left:
                    return ( object ) new Point( 1.0, 0.0 );
                case AxisAlignment.Top:
                    return ( object ) new Point( 0.0, 1.0 );
                case AxisAlignment.Bottom:
                    return ( object ) new Point( 0.0, 0.0 );
                default:
                    return ( object ) new Point();
            }
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
