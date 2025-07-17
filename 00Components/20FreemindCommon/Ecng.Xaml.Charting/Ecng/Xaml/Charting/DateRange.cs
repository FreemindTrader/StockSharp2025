// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.DateRange
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    public class DateRange : Range<DateTime>
    {
        private static readonly string FormattingString = "dd MMM yyyy HH:mm:ss";

        public DateRange()
        {
            this.Min = DateTime.MaxValue;
            this.Max = DateTime.MaxValue;
        }

        public DateRange( DateTime min, DateTime max )
          : base( min, max )
        {
        }

        public override string ToString()
        {
            string format = "{0} {{Min={1}, Max={2}}}";
            Type type = this.GetType();
            DateTime dateTime = this.Min;
            string str1 = dateTime.ToString(DateRange.FormattingString);
            dateTime = this.Max;
            string str2 = dateTime.ToString(DateRange.FormattingString);
            return string.Format( format, ( object ) type, ( object ) str1, ( object ) str2 );
        }

        public override object Clone()
        {
            return ( object ) new DateRange( this.Min, this.Max );
        }

        public override DateTime Diff
        {
            get
            {
                if ( !( this.Min > this.Max ) )
                    return new DateTime( ( this.Max - this.Min ).Ticks );
                return DateTime.MinValue;
            }
        }

        public override bool IsZero
        {
            get
            {
                return ( this.Max - this.Min ).Ticks <= DateTime.MinValue.Ticks;
            }
        }

        public override DoubleRange AsDoubleRange()
        {
            DateTime dateTime = this.Min;
            double ticks1 = (double) dateTime.Ticks;
            dateTime = this.Max;
            double ticks2 = (double) dateTime.Ticks;
            return new DoubleRange( ticks1, ticks2 );
        }

        public override IRange<DateTime> SetMinMax( double min, double max )
        {
            long num1 = (long) min.RoundOff();
            DateTime dateTime1 = DateTime.MinValue;
            long ticks1 = dateTime1.Ticks;
            dateTime1 = DateTime.MaxValue;
            long ticks2 = dateTime1.Ticks;
            long ticks3 = NumberUtil.Constrain(num1, ticks1, ticks2);
            long num2 = (long) max.RoundOff();
            DateTime dateTime2 = DateTime.MinValue;
            long ticks4 = dateTime2.Ticks;
            dateTime2 = DateTime.MaxValue;
            long ticks5 = dateTime2.Ticks;
            long ticks6 = NumberUtil.Constrain(num2, ticks4, ticks5);
            this.SetMinMaxInternal( new DateTime( ticks3 ), new DateTime( ticks6 ) );
            return ( IRange<DateTime> ) this;
        }

        public override IRange<DateTime> SetMinMax( double min, double max, IRange<DateTime> maxRange )
        {
            DateTime first1 = new DateTime((long) min.RoundOff());
            DateTime first2 = new DateTime((long) max.RoundOff());
            if ( maxRange != null )
            {
                first1 = ComparableUtil.Max<DateTime>( first1, maxRange.Min );
                first2 = ComparableUtil.Min<DateTime>( first2, maxRange.Max );
            }
            this.Min = first1;
            this.Max = first2;
            return ( IRange<DateTime> ) this;
        }

        public override IRange<DateTime> GrowBy( double minFraction, double maxFraction )
        {
            if ( !this.Min.IsDefined() || !this.Max.IsDefined() )
                return ( IRange<DateTime> ) this;
            long ticks1 = (this.Max - this.Min).Ticks;
            bool isZero = this.IsZero;
            DateTime dateTime = this.Max;
            double ticks2 = (double) dateTime.Ticks;
            double num1 = maxFraction;
            long num2;
            if ( !isZero )
            {
                num2 = ticks1;
            }
            else
            {
                dateTime = this.Max;
                num2 = dateTime.Ticks;
            }
            double num3 = (double) num2;
            double num4 = num1 * num3;
            long ticks3 = (long) (ticks2 + num4);
            dateTime = this.Min;
            double ticks4 = (double) dateTime.Ticks;
            double num5 = minFraction;
            long num6;
            if ( !isZero )
            {
                num6 = ticks1;
            }
            else
            {
                dateTime = this.Min;
                num6 = dateTime.Ticks;
            }
            double num7 = (double) num6;
            double num8 = num5 * num7;
            long ticks5 = (long) (ticks4 - num8);
            long num9 = ticks3;
            dateTime = DateTime.MaxValue;
            long ticks6 = dateTime.Ticks;
            if ( num9 <= ticks6 )
            {
                long num10 = ticks5;
                dateTime = DateTime.MinValue;
                long ticks7 = dateTime.Ticks;
                if ( num10 >= ticks7 )
                {
                    if ( ticks3 < ticks5 )
                        NumberUtil.Swap( ref ticks3, ref ticks5 );
                    this.Max = new DateTime( ticks3 );
                    this.Min = new DateTime( ticks5 );
                    return ( IRange<DateTime> ) this;
                }
            }
            return ( IRange<DateTime> ) this;
        }

        public override IRange<DateTime> ClipTo( IRange<DateTime> maximumRange )
        {
            DateTime max1 = this.Max;
            DateTime min1 = this.Min;
            DateTime max2 = this.Max > maximumRange.Max ? maximumRange.Max : this.Max;
            DateTime min2 = this.Min < maximumRange.Min ? maximumRange.Min : this.Min;
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
            return ( IRange<DateTime> ) this;
        }
    }
}
