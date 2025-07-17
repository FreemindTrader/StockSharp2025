// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Utility.NumberUtil
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    public static class NumberUtil
    {
        private static readonly Decimal DecimalEpsilon = new Decimal(1, 0, 0, false, (byte) 28);
        private const double EPSILON = 1E-15;

        internal static byte NumDigitsInPositiveNumber( this int x )
        {
            if ( x < 1000000 )
            {
                if ( x < 10 )
                    return 1;
                if ( x < 100 )
                    return 2;
                if ( x < 1000 )
                    return 3;
                if ( x < 10000 )
                    return 4;
                return x < 100000 ? ( byte ) 5 : ( byte ) 6;
            }
            if ( x < 10000000 )
                return 7;
            if ( x < 100000000 )
                return 8;
            return x < 1000000000 ? ( byte ) 9 : ( byte ) 10;
        }

        internal static byte NumDigitsInPositiveNumber( this long x )
        {
            if ( x < 1000000L )
            {
                if ( x < 10L )
                    return 1;
                if ( x < 100L )
                    return 2;
                if ( x < 1000L )
                    return 3;
                if ( x < 10000L )
                    return 4;
                return x < 100000L ? ( byte ) 5 : ( byte ) 6;
            }
            if ( x < 10000000L )
                return 7;
            if ( x < 100000000L )
                return 8;
            if ( x < 1000000000L )
                return 9;
            if ( x < 10000000000L )
                return 10;
            if ( x < 100000000000L )
                return 11;
            if ( x < 1000000000000L )
                return 12;
            if ( x < 10000000000000L )
                return 13;
            return ( byte ) ( Math.Truncate( Math.Log10( ( double ) x ) ) + 1.0 );
        }

        public static bool DoubleEquals( this double value, double other )
        {
            return Math.Abs( value - other ) < 1E-15;
        }

        public static double Round( this double value, double nearest )
        {
            return Math.Round( value / nearest ) * nearest;
        }

        public static double NormalizePrice( this double price, double priceStep )
        {
            return Math.Round( price / priceStep ) * priceStep;
        }

        public static float Round( this float value, float nearest )
        {
            return ( float ) Math.Round( ( double ) value / ( double ) nearest ) * nearest;
        }

        public static double RoundUp( double value, double nearest )
        {
            return Math.Ceiling( value / nearest ) * nearest;
        }

        public static double RoundDown( double value, double nearest )
        {
            return Math.Floor( value / nearest ) * nearest;
        }

        internal static bool IsDivisibleBy( double value, double divisor )
        {
            value = Math.Round( value, 15 );
            if ( Math.Abs( divisor - 0.0 ) < 1E-15 )
                return false;
            double a = Math.Abs(value / divisor);
            double num = 1E-15 * a;
            return Math.Abs( a - Math.Round( a ) ) <= num;
        }

        internal static bool IsDivisibleBy( Decimal value, Decimal divisor )
        {
            if ( Math.Abs( divisor - Decimal.Zero ) < NumberUtil.DecimalEpsilon )
                return false;
            Decimal d = Math.Abs(value / divisor);
            Decimal num = NumberUtil.DecimalEpsilon * d;
            return Math.Abs( d - Math.Round( d ) ) <= num;
        }

        internal static Decimal RoundUp( Decimal value, Decimal nearest )
        {
            return Decimal.Ceiling( value / nearest ) * nearest;
        }

        public static void Swap( ref int value1, ref int value2 )
        {
            int num = value2;
            value2 = value1;
            value1 = num;
        }

        public static void Swap( ref long value1, ref long value2 )
        {
            long num = value2;
            value2 = value1;
            value1 = num;
        }

        public static void Swap( ref double value1, ref double value2 )
        {
            double num = value2;
            value2 = value1;
            value1 = num;
        }

        public static void Swap( ref float value1, ref float value2 )
        {
            float num = value2;
            value2 = value1;
            value1 = num;
        }

        internal static void SortedSwap( ref double xCoord1, ref double xCoord2, ref double yCoord1, ref double yCoord2 )
        {
            if ( xCoord1 <= xCoord2 )
                return;
            double num1 = xCoord1;
            xCoord1 = xCoord2;
            xCoord2 = num1;
            double num2 = yCoord1;
            yCoord1 = yCoord2;
            yCoord2 = num2;
        }

        public static int Constrain( int value, int lowerBound, int upperBound )
        {
            if ( value < lowerBound )
                return lowerBound;
            if ( value <= upperBound )
                return value;
            return upperBound;
        }

        public static long Constrain( long value, long lowerBound, long upperBound )
        {
            if ( value < lowerBound )
                return lowerBound;
            if ( value <= upperBound )
                return value;
            return upperBound;
        }

        public static double Constrain( double value, double lowerBound, double upperBound )
        {
            if ( value < lowerBound )
                return lowerBound;
            if ( value <= upperBound )
                return value;
            return upperBound;
        }

        public static bool IsPowerOf( double value, double power, double logBase )
        {
            return Math.Abs( NumberUtil.RoundUpPower( value, power, logBase ) - value ) <= double.Epsilon;
        }

        internal static double RoundUpPower( double value, double power, double logBase )
        {
            bool flag = Math.Sign(value) == -1;
            double a = Math.Round(Math.Log(Math.Abs(value), logBase) / Math.Log(Math.Abs(power), logBase), 5);
            double num1 = Math.Ceiling(a);
            if ( Math.Abs( num1 - a ) < double.Epsilon )
                return value;
            double y = flag ? num1 - 1.0 : num1;
            double num2 = Math.Pow(power, y);
            if ( !flag )
                return num2;
            return -num2;
        }

        internal static double RoundDownPower( double value, double power, double logBase )
        {
            bool flag = Math.Sign(value) == -1;
            double d = Math.Round(Math.Log(Math.Abs(value), logBase) / Math.Log(Math.Abs(power), logBase), 5);
            double num1 = Math.Floor(d);
            if ( Math.Abs( num1 - d ) < double.Epsilon )
                return value;
            double y = flag ? num1 - 1.0 : num1;
            double num2 = Math.Pow(power, y);
            if ( !flag )
                return num2;
            return -num2;
        }

        internal static bool IsIntegerType( Type type )
        {
            bool flag = false;
            if ( ( uint ) ( Type.GetTypeCode( type ) - 5 ) <= 7U )
                flag = true;
            return flag;
        }

        public static bool IsNaN( double value )
        {
            return value != value;
        }
    }
}
