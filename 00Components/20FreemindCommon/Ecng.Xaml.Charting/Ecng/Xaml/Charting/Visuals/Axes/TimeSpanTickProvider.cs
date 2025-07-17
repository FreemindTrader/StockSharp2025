using System;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Utility;

namespace Ecng.Xaml.Charting.Visuals.Axes
{
    public class TimeSpanTickProvider : TimeSpanTickProviderBase
    {
        public TimeSpanTickProvider()
        {
        }

        protected override IComparable AddDelta( IComparable current, TimeSpan delta )
        {
            return current.ToTimeSpan() + delta;
        }

        protected override double GetTicks( IComparable value )
        {
            return ( double ) value.ToTimeSpan().Ticks;
        }

        protected override bool IsAdditionValid( IComparable current, TimeSpan delta )
        {
            return current.ToTimeSpan().IsAdditionValid( delta );
        }

        protected override bool IsDivisibleBy( IComparable current, TimeSpan delta )
        {
            return current.ToTimeSpan().IsDivisibleBy( delta );
        }

        protected override IComparable RoundUp( IComparable current, TimeSpan delta )
        {
            return new TimeSpan( ( long ) NumberUtil.RoundUp( this.GetTicks( current ), ( double ) delta.Ticks ) );


        }
    }
}
