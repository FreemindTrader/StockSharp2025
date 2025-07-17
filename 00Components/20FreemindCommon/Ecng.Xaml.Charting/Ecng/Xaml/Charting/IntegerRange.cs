using System;

namespace Ecng.Xaml.Charting
{
    public class IntegerRange : Range<int>
    {
        public override int Diff
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
                return this.Diff == 0;
            }
        }

        public IntegerRange()
        {
        }

        public IntegerRange( int min, int max ) : base( min, max )
        {
        }

        public override DoubleRange AsDoubleRange()
        {
            return new DoubleRange( ( double ) base.Min, ( double ) base.Max );
        }

        public override IRange<int> ClipTo( IRange<int> maximumRange )
        {
            int max = base.Max;
            int min = base.Min;
            int num = (base.Max > maximumRange.Max ? maximumRange.Max : base.Max);
            int min1 = (base.Min < maximumRange.Min ? maximumRange.Min : base.Min);
            if ( min1 > maximumRange.Max )
            {
                min1 = maximumRange.Min;
            }
            if ( num < min )
            {
                num = maximumRange.Max;
            }
            if ( min1 > num )
            {
                min1 = maximumRange.Min;
                num = maximumRange.Max;
            }
            base.SetMinMaxInternal( min1, num );
            return this;
        }

        public override object Clone()
        {
            return new IntegerRange( base.Min, base.Max );
        }

        public override IRange<int> GrowBy( double minFraction, double maxFraction )
        {
            int max = base.Max - base.Min;
            if ( max != 0 )
            {
                int num = base.Max + (int)((double)max * maxFraction);
                return new IntegerRange( base.Min - ( int ) ( ( double ) max * minFraction ), num );

            }
            base.Max = base.Max + ( int ) ( ( double ) base.Max * maxFraction );
            base.Min = base.Min - ( int ) ( ( double ) base.Min * minFraction );
            return this;
        }

        public override IRange<int> SetMinMax( double min, double max )
        {
            base.SetMinMaxInternal( ( int ) min, ( int ) max );
            return this;
        }

        public override IRange<int> SetMinMax( double min, double max, IRange<int> maxRange )
        {
            throw new NotImplementedException();
        }
    }
}
