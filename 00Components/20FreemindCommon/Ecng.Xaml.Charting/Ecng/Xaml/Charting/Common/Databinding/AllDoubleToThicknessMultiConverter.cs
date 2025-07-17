// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Databinding.AllDoubleToThicknessMultiConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting.Common.Databinding
{
    public class AllDoubleToThicknessMultiConverter : IMultiValueConverter
    {
        public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) new Thickness( values.OfType<double>().Sum() );
        }

        public object[ ] ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
