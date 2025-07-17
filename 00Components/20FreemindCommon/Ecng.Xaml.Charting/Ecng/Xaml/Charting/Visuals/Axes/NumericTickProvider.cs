// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.NumericTickProvider
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace Ecng.Xaml.Charting
{
    public class NumericTickProvider : TickProvider<double>
    {
        private const double MinDeltaValue = 1E-13;

        public override double[ ] GetMinorTicks( IAxisParams axis )
        {
            return this.GetMinorTicks( ( IRange<double> ) axis.VisibleRange.AsDoubleRange(), ( IAxisDelta<double> ) new DoubleAxisDelta( axis.MinorDelta.ToDouble(), axis.MajorDelta.ToDouble() ) );
        }

        public override double[ ] GetMajorTicks( IAxisParams axis )
        {
            return this.GetMajorTicks( ( IRange<double> ) axis.VisibleRange.AsDoubleRange(), ( IAxisDelta<double> ) new DoubleAxisDelta( axis.MinorDelta.ToDouble(), axis.MajorDelta.ToDouble() ) );
        }

        public double[ ] GetMajorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta )
        {
            if ( !this.IsParamsValid( tickRange, tickDelta ) )
                return new double[ 0 ];
            return this.CalculateMajorTicks( tickRange, tickDelta );
        }

        protected bool IsParamsValid( IRange<double> tickRange, IAxisDelta<double> tickDelta )
        {
            Guard.NotNull( ( object ) tickRange, nameof( tickRange ) );
            Guard.NotNull( ( object ) tickDelta, nameof( tickDelta ) );
            Guard.Assert( ( IComparable ) tickRange.Min, "tickRange.Min" ).IsLessThanOrEqualTo( ( IComparable ) tickRange.Max, "tickRange.Max" );
            if ( tickDelta.MinorDelta.IsRealNumber() && ( tickDelta.MinorDelta.CompareTo( 1E-13 ) >= 0 && tickRange.Min.IsRealNumber() ) )
                return tickRange.Max.IsRealNumber();
            return false;
        }

        public double[ ] GetMinorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta )
        {
            if ( !this.IsParamsValid( tickRange, tickDelta ) )
                return new double[ 0 ];
            return this.CalculateMinorTicks( tickRange, tickDelta );
        }

        public double[ ] GetMinorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta, double[ ] majorTicks )
        {
            if ( !this.IsParamsValid( tickRange, tickDelta ) )
                return new double[ 0 ];
            return this.CalculateMinorTicks( tickRange, tickDelta, majorTicks );
        }

        protected virtual double[ ] CalculateMajorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta )
        {
            return this.CalculateTicks( tickRange, tickDelta.MajorDelta, tickDelta.MajorDelta );
        }

        private double[ ] CalculateTicks( IRange<double> tickRange, double delta, double majorDelta )
        {
            List<double> doubleList = new List<double>();
            double min = tickRange.Min;
            double max = tickRange.Max;
            double num1 = min;
            bool flag = delta.CompareTo(majorDelta) == 0;
            if ( !NumberUtil.IsDivisibleBy( num1, delta ) )
                num1 = NumberUtil.RoundUp( num1, delta );
            double num2 = num1;
            int num3 = 0;
            for ( ; num1 <= max ; num1 = num2 + ( double ) ++num3 * delta )
            {
                if ( !( NumberUtil.IsDivisibleBy( num1, majorDelta ) ^ flag ) )
                    doubleList.Add( num1 );
            }
            return doubleList.ToArray();
        }

        protected virtual double[ ] CalculateMinorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta, double[ ] majorTicks )
        {
            return this.CalculateMinorTicks( tickRange, tickDelta );
        }

        protected virtual double[ ] CalculateMinorTicks( IRange<double> tickRange, IAxisDelta<double> tickDelta )
        {
            return this.CalculateTicks( tickRange, tickDelta.MinorDelta, tickDelta.MajorDelta );
        }
    }
}
