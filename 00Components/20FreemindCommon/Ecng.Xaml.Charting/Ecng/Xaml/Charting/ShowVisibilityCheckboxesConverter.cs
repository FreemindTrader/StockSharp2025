// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ShowVisibilityCheckboxesConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting
{
    public class ShowVisibilityCheckboxesConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            Border element = value as Border;
            if ( element == null )
                return ( object ) Visibility.Collapsed;
            UltrachartLegend ultrachartLegend = element.GetVisualAncestors().FirstOrDefault<DependencyObject>((Func<DependencyObject, bool>) (x => x is UltrachartLegend)) as UltrachartLegend;
            return ( object ) ( Visibility ) ( ultrachartLegend != null ? ( ultrachartLegend.ShowVisibilityCheckboxes ? 0 : 2 ) : 2 );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
