// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.RangeExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using Ecng.Xaml.Charting.Utility;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class RangeExtensions
    {
        public static IRange GrowBy( this IRange range, double minFraction, double maxFraction, bool isLogarithmic, double logBase )
        {
            if ( !isLogarithmic )
            {
                return range.GrowBy( minFraction, maxFraction );
            }

            DoubleRange doubleRange1 = range.AsDoubleRange();
            double a1 = doubleRange1.Min <= 0.0 ? double.Epsilon : doubleRange1.Min;
            double a2 = doubleRange1.Max <= 0.0 ? double.Epsilon : doubleRange1.Max;
            double num1 = Math.Log(a1, logBase);
            double newBase = logBase;
            double num2 = Math.Log(a2, newBase);
            double num3 = num2 - num1;
            double num4 = num3 * minFraction;
            double num5 = num3 * maxFraction;
            double min = Math.Pow(logBase, num1 - num4);
            double max = Math.Pow(logBase, num2 + num5);
            if ( min > max )
            {
                NumberUtil.Swap( ref min, ref max );
            }

            DoubleRange doubleRange2 = new DoubleRange(min, max);
            return RangeFactory.NewWithMinMax( range, ( IComparable ) doubleRange2.Min, ( IComparable ) doubleRange2.Max );
        }

        public static IRange Union( this IEnumerable<IRange> ranges )
        {
            IRange range1 = (IRange) null;
            foreach ( IRange range2 in ranges )
            {
                range1 = range1 != null ? range1.Union( range2 ) : range2;
            }

            return range1;
        }
    }
}
