// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.DoubleMath
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    internal sealed class DoubleMath : IMath<double>
    {
        public double MaxValue
        {
            get
            {
                return double.MaxValue;
            }
        }

        public double MinValue
        {
            get
            {
                return double.MinValue;
            }
        }

        public double ZeroValue
        {
            get
            {
                return 0.0;
            }
        }

        public double Max( double a, double b )
        {
            if ( a.IsNaN() || !b.IsNaN() && a <= b )
                return b;
            return a;
        }

        public double Min( double a, double b )
        {
            if ( a.IsNaN() || !b.IsNaN() && a >= b )
                return b;
            return a;
        }

        public double MinGreaterThan( double floor, double a, double b )
        {
            double num1 = this.Min(a, b);
            double num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( double value )
        {
            return value.IsNaN();
        }

        public double Subtract( double a, double b )
        {
            return a - b;
        }

        public double Abs( double a )
        {
            return Math.Abs( a );
        }

        public double ToDouble( double value )
        {
            return value;
        }

        public double Mult( double lhs, double rhs )
        {
            return lhs * rhs;
        }

        public double Add( double lhs, double rhs )
        {
            return lhs + rhs;
        }

        public double Inc( ref double value )
        {
            return ++value;
        }

        public double Dec( ref double value )
        {
            return --value;
        }
    }
}
