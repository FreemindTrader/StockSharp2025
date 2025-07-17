// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.DateTimeMath
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    internal sealed class DateTimeMath : IMath<DateTime>
    {
        public DateTime MinValue
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        public DateTime MaxValue
        {
            get
            {
                return DateTime.MaxValue;
            }
        }

        public DateTime ZeroValue
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        public DateTime Max( DateTime a, DateTime b )
        {
            if ( a.Ticks <= b.Ticks )
                return b;
            return a;
        }

        public DateTime Min( DateTime a, DateTime b )
        {
            if ( a.Ticks >= b.Ticks )
                return b;
            return a;
        }

        public DateTime MinGreaterThan( DateTime floor, DateTime a, DateTime b )
        {
            DateTime dateTime1 = this.Min(a, b);
            DateTime dateTime2 = this.Max(a, b);
            if ( dateTime1.CompareTo( floor ) <= 0 )
                return dateTime2;
            return dateTime1;
        }

        public bool IsNaN( DateTime value )
        {
            return false;
        }

        public DateTime Subtract( DateTime a, DateTime b )
        {
            return new DateTime( a.Ticks - b.Ticks );
        }

        public DateTime Abs( DateTime a )
        {
            return a;
        }

        public double ToDouble( DateTime value )
        {
            return ( double ) value.Ticks;
        }

        public DateTime Mult( DateTime lhs, DateTime rhs )
        {
            return new DateTime( lhs.Ticks * rhs.Ticks );
        }

        public DateTime Mult( DateTime lhs, double rhs )
        {
            return new DateTime( ( long ) ( ( double ) lhs.Ticks * rhs ) );
        }

        public DateTime Add( DateTime lhs, DateTime rhs )
        {
            return new DateTime( lhs.Ticks + rhs.Ticks );
        }

        public DateTime Inc( ref DateTime value )
        {
            return new DateTime( value.Ticks + 1L );
        }

        public DateTime Dec( ref DateTime value )
        {
            return new DateTime( value.Ticks - 1L );
        }
    }
}
