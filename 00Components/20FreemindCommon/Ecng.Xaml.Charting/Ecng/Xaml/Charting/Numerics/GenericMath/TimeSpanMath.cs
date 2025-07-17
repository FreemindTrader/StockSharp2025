// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.TimeSpanMath
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    internal sealed class TimeSpanMath : IMath<TimeSpan>
    {
        public TimeSpan MinValue
        {
            get
            {
                return TimeSpan.MinValue;
            }
        }

        public TimeSpan MaxValue
        {
            get
            {
                return TimeSpan.MaxValue;
            }
        }

        public TimeSpan ZeroValue
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        public TimeSpan Max( TimeSpan a, TimeSpan b )
        {
            if ( a.Ticks <= b.Ticks )
                return b;
            return a;
        }

        public TimeSpan Min( TimeSpan a, TimeSpan b )
        {
            if ( a.Ticks >= b.Ticks )
                return b;
            return a;
        }

        public TimeSpan MinGreaterThan( TimeSpan floor, TimeSpan a, TimeSpan b )
        {
            TimeSpan timeSpan1 = this.Min(a, b);
            TimeSpan timeSpan2 = this.Max(a, b);
            if ( timeSpan1.CompareTo( floor ) <= 0 )
                return timeSpan2;
            return timeSpan1;
        }

        public bool IsNaN( TimeSpan value )
        {
            return false;
        }

        public TimeSpan Subtract( TimeSpan a, TimeSpan b )
        {
            return a - b;
        }

        public TimeSpan Abs( TimeSpan a )
        {
            return a;
        }

        public double ToDouble( TimeSpan value )
        {
            return ( double ) value.Ticks;
        }

        public TimeSpan Mult( TimeSpan lhs, TimeSpan rhs )
        {
            return new TimeSpan( lhs.Ticks * rhs.Ticks );
        }

        public TimeSpan Mult( TimeSpan lhs, double rhs )
        {
            return new TimeSpan( ( long ) ( ( double ) lhs.Ticks * rhs ) );
        }

        public TimeSpan Add( TimeSpan lhs, TimeSpan rhs )
        {
            return new TimeSpan( lhs.Ticks + rhs.Ticks );
        }

        public TimeSpan Inc( ref TimeSpan value )
        {
            return new TimeSpan( value.Ticks + 1L );
        }

        public TimeSpan Dec( ref TimeSpan value )
        {
            return new TimeSpan( value.Ticks - 1L );
        }
    }
}
