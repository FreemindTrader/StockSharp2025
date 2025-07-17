// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.GenericMath.Uint32Math
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Numerics.GenericMath
{
    internal sealed class Uint32Math : IMath<uint>
    {
        public uint MinValue
        {
            get
            {
                return 0;
            }
        }

        public uint MaxValue
        {
            get
            {
                return uint.MaxValue;
            }
        }

        public uint ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public uint Max( uint a, uint b )
        {
            if ( a <= b )
                return b;
            return a;
        }

        public uint Min( uint a, uint b )
        {
            if ( a >= b )
                return b;
            return a;
        }

        public uint MinGreaterThan( uint floor, uint a, uint b )
        {
            uint num1 = this.Min(a, b);
            uint num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( uint value )
        {
            return false;
        }

        public uint Subtract( uint a, uint b )
        {
            return a - b;
        }

        public uint Abs( uint a )
        {
            return a;
        }

        public double ToDouble( uint value )
        {
            return ( double ) value;
        }

        public uint Mult( uint lhs, uint rhs )
        {
            return lhs * rhs;
        }

        public uint Mult( uint lhs, double rhs )
        {
            return ( uint ) ( ( double ) lhs * rhs );
        }

        public uint Add( uint lhs, uint rhs )
        {
            return lhs + rhs;
        }

        public uint Inc( ref uint value )
        {
            return ++value;
        }

        public uint Dec( ref uint value )
        {
            return --value;
        }
    }
}
