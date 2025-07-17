// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.GenericMath.UShortMath
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    internal sealed class UShortMath : IMath<ushort>
    {
        public ushort MinValue
        {
            get
            {
                return 0;
            }
        }

        public ushort MaxValue
        {
            get
            {
                return ushort.MaxValue;
            }
        }

        public ushort ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public ushort Max( ushort a, ushort b )
        {
            if ( ( int ) a <= ( int ) b )
                return b;
            return a;
        }

        public ushort Min( ushort a, ushort b )
        {
            if ( ( int ) a >= ( int ) b )
                return b;
            return a;
        }

        public ushort MinGreaterThan( ushort floor, ushort a, ushort b )
        {
            ushort num1 = this.Min(a, b);
            ushort num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( ushort value )
        {
            return false;
        }

        public ushort Subtract( ushort a, ushort b )
        {
            return ( ushort ) ( ( uint ) a - ( uint ) b );
        }

        public ushort Abs( ushort a )
        {
            return a;
        }

        public double ToDouble( ushort value )
        {
            return ( double ) value;
        }

        public ushort Mult( ushort lhs, ushort rhs )
        {
            return ( ushort ) ( ( uint ) lhs * ( uint ) rhs );
        }

        public ushort Mult( ushort lhs, double rhs )
        {
            return ( ushort ) ( ( double ) lhs * rhs );
        }

        public ushort Add( ushort lhs, ushort rhs )
        {
            return ( ushort ) ( ( uint ) lhs + ( uint ) rhs );
        }

        public ushort Inc( ref ushort value )
        {
            return ++value;
        }

        public ushort Dec( ref ushort value )
        {
            return --value;
        }
    }
}
