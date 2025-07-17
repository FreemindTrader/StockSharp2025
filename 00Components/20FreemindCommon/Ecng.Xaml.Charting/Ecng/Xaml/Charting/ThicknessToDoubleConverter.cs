// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ThicknessToDoubleConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Charting
{
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            Thickness thickness = (Thickness) value;
            string str = (string) parameter;
            if ( str == "Top" )
                return ( object ) thickness.Top;
            if ( str == "Bottom" )
                return ( object ) thickness.Bottom;
            if ( str == "Left" )
                return ( object ) thickness.Left;
            if ( str == "Right" )
                return ( object ) thickness.Right;
            return ( object ) ( ( thickness.Left + thickness.Right + thickness.Top + thickness.Bottom ) / 4.0 );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
