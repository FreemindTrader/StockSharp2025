// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.TimeSpanTickProviderBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
namespace Ecng.Xaml.Charting
{
    public abstract class TimeSpanTickProviderBase : TickProvider<IComparable>
    {
        public override IComparable[ ] GetMajorTicks( IAxisParams axis )
        {
            return this.GetMajorTicks( axis.VisibleRange, ( IAxisDelta<TimeSpan> ) new TimeSpanDelta( axis.MinorDelta.ToTimeSpan(), axis.MajorDelta.ToTimeSpan() ) );
        }

        public override IComparable[ ] GetMinorTicks( IAxisParams axis )
        {
            return this.GetMinorTicks( axis.VisibleRange, ( IAxisDelta<TimeSpan> ) new TimeSpanDelta( axis.MinorDelta.ToTimeSpan(), axis.MajorDelta.ToTimeSpan() ) );
        }

        protected override double[ ] ConvertTicks( IComparable[ ] ticks )
        {
            return ( ( IEnumerable<IComparable> ) ticks ).Select<IComparable, double>( new Func<IComparable, double>( this.GetTicks ) ).ToArray<double>();
        }

        protected abstract double GetTicks( IComparable value );

        private IComparable[ ] GetMajorTicks( IRange tickRange, IAxisDelta<TimeSpan> tickDelta )
        {
            List<IComparable> comparableList = new List<IComparable>();
            if ( this.AssertRangesValid( tickRange, tickDelta.MajorDelta ) )
            {
                IComparable min = tickRange.Min;
                IComparable max = tickRange.Max;
                for ( IComparable current = this.RoundUp( min, tickDelta.MajorDelta ) ; current.CompareTo( ( object ) max ) <= 0 && this.IsAdditionValid( current, tickDelta.MajorDelta ) ; current = this.AddDelta( current, tickDelta.MajorDelta ) )
                {
                    if ( !comparableList.Contains( current ) )
                        comparableList.Add( current );
                }
            }
            return comparableList.ToArray();
        }

        private bool AssertRangesValid( IRange tickRange, TimeSpan tickDelta )
        {
            Guard.NotNull( ( object ) tickRange, nameof( tickRange ) );
            Guard.NotNull( ( object ) tickDelta, nameof( tickDelta ) );
            Guard.Assert( tickRange.Min, "tickRange.Min" ).IsLessThanOrEqualTo( tickRange.Max, "tickRange.Max" );
            if ( !tickDelta.IsZero() && !tickRange.IsZero && tickRange.Min.IsDefined() )
                return tickRange.Max.IsDefined();
            return false;
        }

        protected abstract IComparable RoundUp( IComparable current, TimeSpan delta );

        protected abstract bool IsAdditionValid( IComparable current, TimeSpan delta );

        protected abstract IComparable AddDelta( IComparable current, TimeSpan delta );

        protected abstract bool IsDivisibleBy( IComparable current, TimeSpan delta );

        private IComparable[ ] GetMinorTicks( IRange tickRange, IAxisDelta<TimeSpan> tickDelta )
        {
            List<IComparable> comparableList = new List<IComparable>();
            if ( this.AssertRangesValid( tickRange, tickDelta.MinorDelta ) )
            {
                IComparable min = tickRange.Min;
                IComparable max = tickRange.Max;
                IComparable current = min;
                if ( !this.IsDivisibleBy( current, tickDelta.MinorDelta ) )
                    current = this.RoundUp( current, tickDelta.MinorDelta );
                for ( ; current.CompareTo( ( object ) max ) < 0 && this.IsAdditionValid( current, tickDelta.MinorDelta ) ; current = this.AddDelta( current, tickDelta.MinorDelta ) )
                {
                    if ( !this.IsDivisibleBy( current, tickDelta.MajorDelta ) && current.CompareTo( ( object ) max ) != 0 && current.CompareTo( ( object ) min ) != 0 )
                        comparableList.Add( current );
                }
            }
            return comparableList.ToArray();
        }
    }
}
