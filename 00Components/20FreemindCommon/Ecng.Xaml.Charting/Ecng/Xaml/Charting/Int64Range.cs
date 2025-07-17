using System;

namespace StockSharp.Xaml.Charting
{
    public class Int64Range : Range<long>
    {
        public override long Diff
        {
            get
            {
                return base.Max - base.Min;
            }
        }

        public override bool IsZero
        {
            get
            {
                return this.Diff == ( long ) 0;
            }
        }

        public Int64Range()
        {
        }

        public Int64Range( long min, long max ) : base( min, max )
        {
        }

        public override DoubleRange AsDoubleRange()
        {
            return new DoubleRange( ( double ) base.Min, ( double ) base.Max );
        }

        public override IRange<long> ClipTo( IRange<long> maximumRange )
        {
            long num = Math.Min(base.Max, maximumRange.Max);
            return new Int64Range( Math.Max( base.Min, maximumRange.Min ), num );

        }

        public override object Clone()
        {
            return new Int64Range( base.Min, base.Max );
        }

        public override IRange<long> GrowBy( double minFraction, double maxFraction )
        {
            long max = base.Max - base.Min;
            if ( max != 0 )
            {
                long num = base.Max + (long)((int)((double)max * maxFraction));
                return new Int64Range( base.Min - ( long ) ( ( int ) ( ( double ) max * minFraction ) ), num );

            }
            base.Max = base.Max + ( long ) ( ( double ) base.Max * maxFraction );
            base.Min = base.Min - ( long ) ( ( double ) base.Min * minFraction );
            return this;
        }

        public override IRange<long> SetMinMax( double min, double max )
        {
            base.SetMinMaxInternal( ( long ) min, ( long ) max );
            return this;
        }

        public override IRange<long> SetMinMax( double min, double max, IRange<long> maxRange )
        {
            throw new NotImplementedException();
        }
    }
}
