// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.GenericMath.FloatMath
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    internal sealed class FloatMath : IMath<float>
    {
        public float MaxValue
        {
            get
            {
                return float.MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return float.MinValue;
            }
        }

        public float ZeroValue
        {
            get
            {
                return 0.0f;
            }
        }

        public float Max( float a, float b )
        {
            if ( this.IsNaN( a ) || !this.IsNaN( b ) && ( double ) a <= ( double ) b )
                return b;
            return a;
        }

        private bool IsDefined( float a )
        {
            if ( !float.IsInfinity( a ) )
                return !float.IsNaN( a );
            return false;
        }

        public float Min( float a, float b )
        {
            if ( this.IsNaN( a ) || !this.IsNaN( b ) && ( double ) a >= ( double ) b )
                return b;
            return a;
        }

        public float MinGreaterThan( float floor, float a, float b )
        {
            float num1 = this.Min(a, b);
            float num2 = this.Max(a, b);
            if ( num1.CompareTo( floor ) <= 0 )
                return num2;
            return num1;
        }

        public bool IsNaN( float value )
        {
            return ( double ) value != ( double ) value;
        }

        public float Subtract( float a, float b )
        {
            return a - b;
        }

        public float Abs( float a )
        {
            return Math.Abs( a );
        }

        public double ToDouble( float value )
        {
            return ( double ) value;
        }

        public float Mult( float lhs, float rhs )
        {
            return lhs * rhs;
        }

        public float Mult( float lhs, double rhs )
        {
            return lhs * ( float ) rhs;
        }

        public float Add( float lhs, float rhs )
        {
            return lhs + rhs;
        }

        public float Inc( ref float value )
        {
            return ++value;
        }

        public float Dec( ref float value )
        {
            return --value;
        }
    }
}
