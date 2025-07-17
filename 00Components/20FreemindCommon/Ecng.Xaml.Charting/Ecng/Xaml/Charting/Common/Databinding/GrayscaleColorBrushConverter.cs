// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Databinding.GrayscaleColorBrushConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml.Charting
{
    public class GrayscaleColorBrushConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            Color color = new Color();
            SolidColorBrush solidColorBrush = value as SolidColorBrush;
            if ( solidColorBrush != null )
                color = solidColorBrush.Color;
            else if ( value is Color )
                color = ( Color ) value;
            return ( object ) new SolidColorBrush( ( double ) color.R * 0.299 + ( double ) color.G * 0.587 + ( double ) color.B * 0.114 > 128.0 ? Colors.Black : Colors.White );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
