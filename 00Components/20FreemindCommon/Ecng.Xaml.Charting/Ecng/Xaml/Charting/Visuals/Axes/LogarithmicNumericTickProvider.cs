// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.LogarithmicNumericTickProvider
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace Ecng.Xaml.Charting
{
    public class LogarithmicNumericTickProvider : NumericTickProvider
    {
        public double LogarithmicBase
        {
            get; set;
        }

        protected override double[ ] CalculateMajorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta )
        {
            List<double> doubleList = new List<double>();
            double min = tickRange.Min;
            double max = tickRange.Max;
            double a = min;
            double majorDelta = tickDelta.MajorDelta;
            if ( !NumberUtil.IsPowerOf( a, this.LogarithmicBase, this.LogarithmicBase ) )
                a = NumberUtil.RoundDownPower( a, this.LogarithmicBase, this.LogarithmicBase );
            double num1 = Math.Round(Math.Log(a, this.LogarithmicBase), 10);
            if ( !NumberUtil.IsDivisibleBy( num1, majorDelta ) )
                num1 = NumberUtil.RoundUp( num1, majorDelta );
            double y = num1;
            double num2 = Math.Pow(this.LogarithmicBase, y);
            int num3 = 0;
            for ( ; num2 <= max ; num2 = Math.Pow( this.LogarithmicBase, y ) )
            {
                if ( NumberUtil.IsDivisibleBy( y, majorDelta ) )
                    doubleList.Add( num2 );
                y = num1 + ( double ) ++num3 * majorDelta;
            }
            return doubleList.ToArray();
        }

        protected override double[ ] CalculateMinorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta )
        {
            double[] majorTicks = this.CalculateMajorTicks(tickRange, tickDelta);
            return this.CalculateMinorTicks( tickRange, tickDelta, majorTicks );
        }

        protected override double[ ] CalculateMinorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta, double[ ] majorTicks )
        {
            List<double> doubleList = new List<double>();
            double minorDelta = tickDelta.MinorDelta;
            double majorDelta = tickDelta.MajorDelta;
            for ( int length = majorTicks.Length ; length >= 0 ; --length )
            {
                double num1 = Math.Pow(this.LogarithmicBase, majorDelta);
                double num2 = length < majorTicks.Length ? majorTicks[length] : majorTicks[length - 1] * num1;
                double num3 = num2 / num1;
                double num4 = num3 * minorDelta;
                double num5 = num3 + num4;
                while ( num5 < num2 )
                {
                    doubleList.Add( num5 );
                    num5 += num4;
                }
            }
            doubleList.Reverse();
            return doubleList.ToArray();
        }
    }
}
