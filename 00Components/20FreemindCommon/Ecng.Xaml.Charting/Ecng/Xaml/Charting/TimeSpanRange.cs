// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.TimeSpanRange
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting
{
    public class TimeSpanRange : Range<TimeSpan>
    {
        private static readonly string FormattingString = "hh\\:mm\\:ss";

        public TimeSpanRange()
        {
            this.Min = TimeSpan.MaxValue;
            this.Max = TimeSpan.MaxValue;
        }

        public TimeSpanRange( TimeSpan min, TimeSpan max )
          : base( min, max )
        {
        }

        public override string ToString()
        {
            string format = "{0} {{Min={1}, Max={2}}}";
            Type type = this.GetType();
            TimeSpan timeSpan = this.Min;
            string str1 = timeSpan.ToString(TimeSpanRange.FormattingString);
            timeSpan = this.Max;
            string str2 = timeSpan.ToString(TimeSpanRange.FormattingString);
            return string.Format( format, ( object ) type, ( object ) str1, ( object ) str2 );
        }

        public override object Clone()
        {
            return ( object ) new TimeSpanRange( this.Min, this.Max );
        }

        public override TimeSpan Diff
        {
            get
            {
                return new TimeSpan( ( this.Max - this.Min ).Ticks );
            }
        }

        public override bool IsZero
        {
            get
            {
                TimeSpan timeSpan = this.Max - this.Min;
                long ticks1 = timeSpan.Ticks;
                timeSpan = TimeSpan.MinValue;
                long ticks2 = timeSpan.Ticks;
                return ticks1 <= ticks2;
            }
        }

        public override DoubleRange AsDoubleRange()
        {
            TimeSpan timeSpan = this.Min;
            double ticks1 = (double) timeSpan.Ticks;
            timeSpan = this.Max;
            double ticks2 = (double) timeSpan.Ticks;
            return new DoubleRange( ticks1, ticks2 );
        }

        public override IRange<TimeSpan> SetMinMax( double min, double max )
        {
            long num1 = (long) min.RoundOff();
            TimeSpan timeSpan1 = TimeSpan.MinValue;
            long ticks1 = timeSpan1.Ticks;
            timeSpan1 = TimeSpan.MaxValue;
            long ticks2 = timeSpan1.Ticks;
            long ticks3 = NumberUtil.Constrain(num1, ticks1, ticks2);
            long num2 = (long) max.RoundOff();
            TimeSpan timeSpan2 = TimeSpan.MinValue;
            long ticks4 = timeSpan2.Ticks;
            timeSpan2 = TimeSpan.MaxValue;
            long ticks5 = timeSpan2.Ticks;
            long ticks6 = NumberUtil.Constrain(num2, ticks4, ticks5);
            this.SetMinMaxInternal( new TimeSpan( ticks3 ), new TimeSpan( ticks6 ) );
            return ( IRange<TimeSpan> ) this;
        }

        public override IRange<TimeSpan> SetMinMax( double min, double max, IRange<TimeSpan> maxRange )
        {
            TimeSpan first1 = new TimeSpan((long) min.RoundOff());
            TimeSpan first2 = new TimeSpan((long) max.RoundOff());
            if ( maxRange != null )
            {
                first1 = ComparableUtil.Max<TimeSpan>( first1, maxRange.Min );
                first2 = ComparableUtil.Min<TimeSpan>( first2, maxRange.Max );
            }
            this.Min = first1;
            this.Max = first2;
            return ( IRange<TimeSpan> ) this;
        }

        public override IRange<TimeSpan> GrowBy( double minFraction, double maxFraction )
        {
            if ( !this.Min.IsDefined() || !this.Max.IsDefined() )
                return ( IRange<TimeSpan> ) this;
            long ticks1 = (this.Max - this.Min).Ticks;
            if ( ticks1 == 0L )
            {
                TimeSpan timeSpan = this.Max;
                double ticks2 = (double) timeSpan.Ticks;
                timeSpan = this.Max;
                double num1 = (double) timeSpan.Ticks * maxFraction;
                this.Max = new TimeSpan( ( long ) ( ticks2 + num1 ) );
                timeSpan = this.Min;
                double ticks3 = (double) timeSpan.Ticks;
                timeSpan = this.Min;
                double num2 = (double) timeSpan.Ticks * minFraction;
                this.Min = new TimeSpan( ( long ) ( ticks3 - num2 ) );
                return ( IRange<TimeSpan> ) this;
            }
            long ticks4 = (long) ((double) this.Max.Ticks + (double) ticks1 * maxFraction);
            long ticks5 = (long) ((double) this.Min.Ticks - (double) ticks1 * minFraction);
            long num3 = ticks4;
            TimeSpan timeSpan1 = TimeSpan.MaxValue;
            long ticks6 = timeSpan1.Ticks;
            if ( num3 <= ticks6 )
            {
                long num1 = ticks5;
                timeSpan1 = TimeSpan.MinValue;
                long ticks2 = timeSpan1.Ticks;
                if ( num1 >= ticks2 )
                {
                    this.Max = new TimeSpan( ticks4 );
                    this.Min = new TimeSpan( ticks5 );
                    return ( IRange<TimeSpan> ) this;
                }
            }
            return ( IRange<TimeSpan> ) this;
        }

        public override IRange<TimeSpan> ClipTo( IRange<TimeSpan> maximumRange )
        {
            TimeSpan max1 = this.Max;
            TimeSpan min1 = this.Min;
            TimeSpan max2 = this.Max > maximumRange.Max ? maximumRange.Max : this.Max;
            TimeSpan min2 = this.Min < maximumRange.Min ? maximumRange.Min : this.Min;
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
            return ( IRange<TimeSpan> ) this;
        }
    }
}
