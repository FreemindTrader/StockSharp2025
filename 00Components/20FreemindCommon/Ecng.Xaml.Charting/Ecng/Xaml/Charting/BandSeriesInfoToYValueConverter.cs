// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.BandSeriesInfoToYValueConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    public class BandSeriesInfoToYValueConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            BandSeriesInfo bandSeriesInfo = value as BandSeriesInfo;
            string str1 = bandSeriesInfo.FormattedYValue;
            string str2;
            string str3;
            if ( bandSeriesInfo.YValue.CompareTo( ( object ) bandSeriesInfo.Y1Value ) >= 0 )
            {
                str2 = bandSeriesInfo.FormattedYValue;
                str3 = bandSeriesInfo.FormattedY1Value;
            }
            else
            {
                str2 = bandSeriesInfo.FormattedY1Value;
                str3 = bandSeriesInfo.FormattedYValue;
            }
            if ( parameter != null )
            {
                string upperInvariant = parameter.ToString().ToUpperInvariant();
                if ( !( upperInvariant == "1" ) )
                {
                    if ( !( upperInvariant == "GREATER" ) )
                    {
                        if ( upperInvariant == "LOWER" )
                            str1 = str3;
                    }
                    else
                        str1 = str2;
                }
                else
                    str1 = bandSeriesInfo.FormattedY1Value;
            }
            return ( object ) str1;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
