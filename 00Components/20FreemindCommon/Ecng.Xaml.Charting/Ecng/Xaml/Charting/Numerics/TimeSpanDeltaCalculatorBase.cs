// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.TimeSpanDeltaCalculatorBase
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
namespace fx.Xaml.Charting
{
    internal abstract class TimeSpanDeltaCalculatorBase : IDateDeltaCalculator, IDeltaCalculator
    {
        private readonly IList<TimeSpan> _dateDeltas = (IList<TimeSpan>) new TimeSpan[24]
    {
      TimeSpan.FromSeconds(1.0),
      TimeSpan.FromSeconds(2.0),
      TimeSpan.FromSeconds(5.0),
      TimeSpan.FromSeconds(10.0),
      TimeSpan.FromSeconds(30.0),
      TimeSpan.FromMinutes(1.0),
      TimeSpan.FromMinutes(2.0),
      TimeSpan.FromMinutes(5.0),
      TimeSpan.FromMinutes(10.0),
      TimeSpan.FromMinutes(30.0),
      TimeSpan.FromHours(1.0),
      TimeSpan.FromHours(2.0),
      TimeSpan.FromHours(4.0),
      TimeSpan.FromDays(1.0),
      TimeSpan.FromDays(2.0),
      TimeSpan.FromDays(7.0),
      TimeSpan.FromDays(14.0),
      TimeSpanExtensions.FromMonths(1),
      TimeSpanExtensions.FromMonths(3),
      TimeSpanExtensions.FromMonths(6),
      TimeSpanExtensions.FromYears(1),
      TimeSpanExtensions.FromYears(2),
      TimeSpanExtensions.FromYears(5),
      TimeSpanExtensions.FromYears(10)
    };
        private const uint DefaultTicksCount = 8;

        protected abstract long GetTicks( IComparable value );

        public TimeSpanDelta GetDeltaFromRange( IComparable min, IComparable max, int minorsPerMajor, uint maxTicks = 8 )
        {
            return this.GetDeltaFromTickRange( this.GetTicks( min ), this.GetTicks( max ), minorsPerMajor, maxTicks );
        }

        private TimeSpanDelta GetDeltaFromTickRange( long min, long max, int minorsPerMajor, uint maxTicks )
        {
            if ( min >= max )
                return new TimeSpanDelta( TimeSpan.Zero, TimeSpan.Zero );
            long num = max - min;
            TimeSpan desiredSpan = new TimeSpan(num);
            TimeSpan majorDelta = this._dateDeltas.FirstOrDefault<TimeSpan>((Func<TimeSpan, bool>) (x => new TimeSpan(x.Ticks * (long) maxTicks) > desiredSpan));
            return !majorDelta.Equals( TimeSpan.Zero ) ? this.GetDeltasForMajorDelta( majorDelta, num, minorsPerMajor, maxTicks ) : this.CalculateDeltas( 0L, num / TimeSpanExtensions.FromYears( 1 ).Ticks, minorsPerMajor, maxTicks, true );
        }

        private TimeSpanDelta CalculateDeltas( long min, long max, int minorsPerMajor, uint maxTicks, bool fromYears )
        {
            Tuple<long, long> tickSpacing = new NiceLongScale(min, max, minorsPerMajor, maxTicks).TickSpacing;
            return new TimeSpanDelta( fromYears ? TimeSpanExtensions.FromYears( ( int ) tickSpacing.Item1 ) : TimeSpan.FromTicks( tickSpacing.Item1 ), fromYears ? TimeSpanExtensions.FromYears( ( int ) tickSpacing.Item2 ) : TimeSpan.FromTicks( tickSpacing.Item2 ) );
        }

        private TimeSpanDelta GetDeltasForMajorDelta( TimeSpan majorDelta, long range, int minorsPerMajor, uint maxTicks )
        {
            int index = this._dateDeltas.IndexOf(majorDelta) - 2;
            return index < 0 ? this.CalculateDeltas( 0L, range, minorsPerMajor, maxTicks, false ) : new TimeSpanDelta( this._dateDeltas[ index ], majorDelta );
        }

        IAxisDelta IDeltaCalculator.GetDeltaFromRange( IComparable min, IComparable max, int minorsPerMajor, uint maxTicks )
        {
            return ( IAxisDelta ) this.GetDeltaFromRange( min, max, minorsPerMajor, maxTicks );
        }
    }
}
