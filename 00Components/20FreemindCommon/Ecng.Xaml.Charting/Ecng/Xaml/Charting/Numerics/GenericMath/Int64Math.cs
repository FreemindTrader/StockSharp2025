// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.GenericMath.Int64Math
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    internal sealed class Int64Math : IMath<long>
    {
        public long MaxValue
        {
            get
            {
                return long.MaxValue;
            }
        }

        public long MinValue
        {
            get
            {
                return long.MinValue;
            }
        }

        public long ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public long Max( long a, long b )
        {
            if ( a <= b )
                return b;
            return a;
        }

        public long Min( long a, long b )
        {
            if ( a >= b )
                return b;
            return a;
        }

        public long MinGreaterThan( long floor, long a, long b )
        {
            long num1 = this.Min(a, b);
            long num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( long value )
        {
            return false;
        }

        public long Subtract( long a, long b )
        {
            return a - b;
        }

        public long Abs( long a )
        {
            return Math.Abs( a );
        }

        public double ToDouble( long value )
        {
            return ( double ) value;
        }

        public long Mult( long lhs, long rhs )
        {
            return lhs * rhs;
        }

        public long Mult( long lhs, double rhs )
        {
            return ( long ) ( ( double ) lhs * rhs );
        }

        public long Add( long lhs, long rhs )
        {
            return lhs + rhs;
        }

        public long Inc( ref long value )
        {
            return ++value;
        }

        public long Dec( ref long value )
        {
            return --value;
        }
    }
}
