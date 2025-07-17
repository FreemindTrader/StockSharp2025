// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.SbyteMath
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    internal sealed class SbyteMath : IMath<sbyte>
    {
        public sbyte MinValue
        {
            get
            {
                return sbyte.MinValue;
            }
        }

        public sbyte MaxValue
        {
            get
            {
                return sbyte.MaxValue;
            }
        }

        public sbyte ZeroValue
        {
            get
            {
                return 0;
            }
        }

        public sbyte Max( sbyte a, sbyte b )
        {
            if ( ( int ) a <= ( int ) b )
                return b;
            return a;
        }

        public sbyte Min( sbyte a, sbyte b )
        {
            if ( ( int ) a >= ( int ) b )
                return b;
            return a;
        }

        public sbyte MinGreaterThan( sbyte floor, sbyte a, sbyte b )
        {
            sbyte num1 = this.Min(a, b);
            sbyte num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( sbyte value )
        {
            return false;
        }

        public sbyte Subtract( sbyte a, sbyte b )
        {
            return ( sbyte ) ( ( int ) a - ( int ) b );
        }

        public sbyte Abs( sbyte a )
        {
            return Math.Abs( a );
        }

        public double ToDouble( sbyte value )
        {
            return ( double ) value;
        }

        public sbyte Mult( sbyte lhs, sbyte rhs )
        {
            return ( sbyte ) ( ( int ) lhs * ( int ) rhs );
        }

        public sbyte Mult( sbyte lhs, double rhs )
        {
            return ( sbyte ) ( ( double ) lhs * rhs );
        }

        public sbyte Add( sbyte lhs, sbyte rhs )
        {
            return ( sbyte ) ( ( int ) lhs + ( int ) rhs );
        }

        public sbyte Inc( ref sbyte value )
        {
            return ++value;
        }

        public sbyte Dec( ref sbyte value )
        {
            return --value;
        }
    }
}
