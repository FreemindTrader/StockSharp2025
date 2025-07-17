// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Databinding.AllTrueMultiConverter
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace fx.Xaml.Charting
{
    public class AllTrueMultiConverter : IMultiValueConverter
    {
        public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) values.OfType<bool>().Any<bool>( ( Func<bool, bool> ) ( val => !val ) );
        }

        public object[ ] ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
