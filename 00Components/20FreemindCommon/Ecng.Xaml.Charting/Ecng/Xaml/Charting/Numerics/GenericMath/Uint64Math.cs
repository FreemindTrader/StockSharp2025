// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.Uint64Math
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    internal sealed class Uint64Math : IMath<ulong>
    {
        public ulong MinValue
        {
            get
            {
                return 0;
            }
        }

        public ulong MaxValue
        {
            get
            {
                return ulong.MaxValue;
            }
        }

        public ulong ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public ulong Max( ulong a, ulong b )
        {
            if ( a <= b )
                return b;
            return a;
        }

        public ulong Min( ulong a, ulong b )
        {
            if ( a >= b )
                return b;
            return a;
        }

        public ulong MinGreaterThan( ulong floor, ulong a, ulong b )
        {
            ulong num1 = this.Min(a, b);
            ulong num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( ulong value )
        {
            return false;
        }

        public ulong Subtract( ulong a, ulong b )
        {
            return a - b;
        }

        public ulong Abs( ulong a )
        {
            return a;
        }

        public double ToDouble( ulong value )
        {
            return ( double ) value;
        }

        public ulong Mult( ulong lhs, ulong rhs )
        {
            return lhs * rhs;
        }

        public ulong Mult( ulong lhs, double rhs )
        {
            return ( ulong ) ( ( double ) lhs * rhs );
        }

        public ulong Add( ulong lhs, ulong rhs )
        {
            return lhs + rhs;
        }

        public ulong Inc( ref ulong value )
        {
            return ++value;
        }

        public ulong Dec( ref ulong value )
        {
            return --value;
        }
    }
}
