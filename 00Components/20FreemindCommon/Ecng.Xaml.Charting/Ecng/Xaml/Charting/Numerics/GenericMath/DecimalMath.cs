// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.DecimalMath
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting.Numerics.GenericMath
{
    internal sealed class DecimalMath : IMath<Decimal>
    {
        public Decimal MinValue
        {
            get
            {
                return new Decimal( -1, -1, -1, true, ( byte ) 0 );
            }
        }

        public Decimal MaxValue
        {
            get
            {
                return new Decimal( -1, -1, -1, false, ( byte ) 0 );
            }
        }

        public Decimal ZeroValue
        {
            get
            {
                return Decimal.Zero;
            }
        }

        public Decimal Max( Decimal a, Decimal b )
        {
            if ( !( a > b ) )
                return b;
            return a;
        }

        public Decimal Min( Decimal a, Decimal b )
        {
            if ( !( a < b ) )
                return b;
            return a;
        }

        public Decimal MinGreaterThan( Decimal floor, Decimal a, Decimal b )
        {
            Decimal num1 = this.Min(a, b);
            Decimal num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( Decimal value )
        {
            return false;
        }

        public Decimal Subtract( Decimal a, Decimal b )
        {
            return a - b;
        }

        public Decimal Abs( Decimal a )
        {
            return Math.Abs( a );
        }

        public double ToDouble( Decimal value )
        {
            return ( double ) value;
        }

        public Decimal Mult( Decimal lhs, Decimal rhs )
        {
            return lhs * rhs;
        }

        public Decimal Mult( Decimal lhs, double rhs )
        {
            return lhs * ( Decimal ) rhs;
        }

        public Decimal Add( Decimal lhs, Decimal rhs )
        {
            return lhs + rhs;
        }

        public Decimal Inc( ref Decimal value )
        {
            return ++value;
        }

        public Decimal Dec( ref Decimal value )
        {
            return --value;
        }
    }
}
