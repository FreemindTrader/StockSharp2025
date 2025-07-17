// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.DataTemplateToObjectConverter
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace fx.Xaml.Charting
{
    public class DataTemplateToObjectConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) ( ( ( FrameworkTemplate ) value ).LoadContent() as FrameworkElement );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
