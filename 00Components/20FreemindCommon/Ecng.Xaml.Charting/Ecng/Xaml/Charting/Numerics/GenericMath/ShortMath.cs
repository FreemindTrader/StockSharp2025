// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.GenericMath.ShortMath
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting.Numerics.GenericMath
{
    internal sealed class ShortMath : IMath<short>
    {
        public short MinValue
        {
            get
            {
                return short.MinValue;
            }
        }

        public short MaxValue
        {
            get
            {
                return short.MaxValue;
            }
        }

        public short ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public short Max( short a, short b )
        {
            if ( ( int ) a <= ( int ) b )
                return b;
            return a;
        }

        public short Min( short a, short b )
        {
            if ( ( int ) a >= ( int ) b )
                return b;
            return a;
        }

        public short MinGreaterThan( short floor, short a, short b )
        {
            short num1 = this.Min(a, b);
            short num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( short value )
        {
            return false;
        }

        public short Subtract( short a, short b )
        {
            return ( short ) ( ( int ) a - ( int ) b );
        }

        public short Abs( short a )
        {
            return Math.Abs( a );
        }

        public double ToDouble( short value )
        {
            return ( double ) value;
        }

        public short Mult( short lhs, short rhs )
        {
            return ( short ) ( ( int ) lhs * ( int ) rhs );
        }

        public short Mult( short lhs, double rhs )
        {
            return ( short ) ( ( double ) lhs * rhs );
        }

        public short Add( short lhs, short rhs )
        {
            return ( short ) ( ( int ) lhs + ( int ) rhs );
        }

        public short Inc( ref short value )
        {
            return ++value;
        }

        public short Dec( ref short value )
        {
            return --value;
        }
    }
}
