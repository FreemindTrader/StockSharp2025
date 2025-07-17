// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Databinding.LegendPlacementToGridPositionConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;
using StockSharp.Xaml.Charting.ChartModifiers;

namespace StockSharp.Xaml.Charting.Common.Databinding
{
    public class LegendPlacementToGridPositionConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            LegendPlacement legendPlacement = (LegendPlacement) value;
            string str = "ROW";
            bool flag = (parameter as string ?? str).ToUpperInvariant().Contains(str);
            int num;
            switch ( legendPlacement )
            {
                case LegendPlacement.TopLeft:
                    num = flag ? 1 : 0;
                    break;
                case LegendPlacement.TopRight:
                    num = flag ? 1 : 4;
                    break;
                case LegendPlacement.BottomLeft:
                    num = flag ? 5 : 0;
                    break;
                case LegendPlacement.BottomRight:
                    num = flag ? 5 : 4;
                    break;
                case LegendPlacement.Top:
                    num = flag ? 1 : 2;
                    break;
                case LegendPlacement.Bottom:
                    num = flag ? 5 : 2;
                    break;
                case LegendPlacement.Left:
                    num = flag ? 3 : 0;
                    break;
                case LegendPlacement.Right:
                    num = flag ? 3 : 4;
                    break;
                default:
                    num = flag ? 3 : 2;
                    break;
            }
            return ( object ) num;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
