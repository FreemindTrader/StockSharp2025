// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.Int32Math
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting.Numerics.GenericMath
{
    internal sealed class Int32Math : IMath<int>
    {
        public int MaxValue
        {
            get
            {
                return int.MaxValue;
            }
        }

        public int MinValue
        {
            get
            {
                return int.MinValue;
            }
        }

        public int ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public int Max( int a, int b )
        {
            if ( a <= b )
                return b;
            return a;
        }

        public int Min( int a, int b )
        {
            if ( a >= b )
                return b;
            return a;
        }

        public int MinGreaterThan( int floor, int a, int b )
        {
            int num1 = this.Min(a, b);
            int num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( int value )
        {
            return false;
        }

        public int Subtract( int a, int b )
        {
            return a - b;
        }

        public int Abs( int a )
        {
            return Math.Abs( a );
        }

        public double ToDouble( int value )
        {
            return ( double ) value;
        }

        public int Mult( int lhs, int rhs )
        {
            return lhs * rhs;
        }

        public int Mult( int lhs, double rhs )
        {
            return ( int ) ( ( double ) lhs * rhs );
        }

        public int Add( int lhs, int rhs )
        {
            return lhs + rhs;
        }

        public int Inc( ref int value )
        {
            return ++value;
        }

        public int Dec( ref int value )
        {
            return --value;
        }
    }
}
