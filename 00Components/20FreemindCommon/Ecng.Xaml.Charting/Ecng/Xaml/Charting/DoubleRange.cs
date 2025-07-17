// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.DoubleRange
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting
{
    public class DoubleRange : Range<double>
    {
        public DoubleRange()
        {
        }

        public DoubleRange( double min, double max )
          : base( min, max )
        {
        }

        public static DoubleRange UndefinedRange
        {
            get
            {
                return new DoubleRange( double.NaN, double.NaN );
            }
        }

        public override object Clone()
        {
            return ( object ) new DoubleRange( this.Min, this.Max );
        }

        public override double Diff
        {
            get
            {
                return this.Max - this.Min;
            }
        }

        public override bool IsZero
        {
            get
            {
                return Math.Abs( this.Diff ) <= double.Epsilon;
            }
        }

        public override DoubleRange AsDoubleRange()
        {
            return this;
        }

        public override IRange<double> SetMinMax( double min, double max )
        {
            this.SetMinMaxInternal( min, max );
            return ( IRange<double> ) this;
        }

        public override IRange<double> SetMinMax( double min, double max, IRange<double> maxRange )
        {
            this.Min = Math.Max( min, maxRange.Min );
            this.Max = Math.Min( max, maxRange.Max );
            return ( IRange<double> ) this;
        }

        public override IRange<double> GrowBy( double minFraction, double maxFraction )
        {
            double diff = this.Diff;
            double num1 = this.Min - minFraction * (this.IsZero ? this.Min : diff);
            double num2 = this.Max + maxFraction * (this.IsZero ? this.Max : diff);
            if ( num1 > num2 )
                NumberUtil.Swap( ref num1, ref num2 );
            if ( Math.Abs( num1 - num2 ) <= double.Epsilon && Math.Abs( num1 ) <= double.Epsilon )
            {
                num1 = -1.0;
                num2 = 1.0;
            }
            this.Min = num1;
            this.Max = num2;
            return ( IRange<double> ) this;
        }

        public override IRange<double> ClipTo( IRange<double> maximumRange )
        {
            double max1 = this.Max;
            double min1 = this.Min;
            double max2 = Math.Min(this.Max, maximumRange.Max);
            double min2 = Math.Max(this.Min, maximumRange.Min);
            if ( min2 > maximumRange.Max )
                min2 = maximumRange.Min;
            if ( max2 < min1 )
                max2 = maximumRange.Max;
            if ( min2 > max2 )
            {
                min2 = maximumRange.Min;
                max2 = maximumRange.Max;
            }
            this.SetMinMaxInternal( min2, max2 );
            return ( IRange<double> ) this;
        }
    }
}
