// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.ByteMath
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    internal sealed class ByteMath : IMath<byte>
    {
        public byte MinValue
        {
            get
            {
                return 0;
            }
        }

        public byte MaxValue
        {
            get
            {
                return byte.MaxValue;
            }
        }

        public byte ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public byte Max( byte a, byte b )
        {
            if ( ( int ) a <= ( int ) b )
                return b;
            return a;
        }

        public byte Min( byte a, byte b )
        {
            if ( ( int ) a >= ( int ) b )
                return b;
            return a;
        }

        public byte MinGreaterThan( byte floor, byte a, byte b )
        {
            byte num1 = this.Min(a, b);
            byte num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( byte value )
        {
            return false;
        }

        public byte Subtract( byte a, byte b )
        {
            return ( byte ) ( ( uint ) a - ( uint ) b );
        }

        public byte Abs( byte a )
        {
            return a;
        }

        public double ToDouble( byte value )
        {
            return ( double ) value;
        }

        public byte Mult( byte lhs, byte rhs )
        {
            return ( byte ) ( ( uint ) lhs * ( uint ) rhs );
        }

        public byte Mult( byte lhs, double rhs )
        {
            return ( byte ) ( ( double ) lhs * rhs );
        }

        public byte Add( byte lhs, byte rhs )
        {
            return ( byte ) ( ( uint ) lhs + ( uint ) rhs );
        }

        public byte Inc( ref byte value )
        {
            return ++value;
        }

        public byte Dec( ref byte value )
        {
            return --value;
        }
    }
}
