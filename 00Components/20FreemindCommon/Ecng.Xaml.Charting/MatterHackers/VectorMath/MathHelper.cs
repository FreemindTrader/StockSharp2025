// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.MathHelper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.VectorMath
{
    internal static class MathHelper
    {
        public const double Pi = 3.14159274101257;
        public const double Tau = 6.28318548202515;
        public const double PiOver2 = 1.57079637050629;
        public const double PiOver3 = 1.04719758033752;
        public const double PiOver4 = 0.785398185253143;
        public const double PiOver6 = 0.523598790168762;
        public const double TwoPi = 6.28318548202515;
        public const double ThreePiOver2 = 4.71238911151886;
        public const double E = 2.71828174591064;
        public const double Log10E = 0.434294492006302;
        public const double Log2E = 1.44269502162933;

        public static long NextPowerOfTwo( long n )
        {
            if ( n < 0L )
            {
                throw new ArgumentOutOfRangeException( nameof( n ), "Must be positive." );
            }

            return ( long ) Math.Pow( 2.0, Math.Ceiling( Math.Log( ( double ) n, 2.0 ) ) );
        }

        public static int NextPowerOfTwo( int n )
        {
            if ( n < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( n ), "Must be positive." );
            }

            return ( int ) Math.Pow( 2.0, Math.Ceiling( Math.Log( ( double ) n, 2.0 ) ) );
        }

        public static float NextPowerOfTwo( float n )
        {
            if ( ( double ) n < 0.0 )
            {
                throw new ArgumentOutOfRangeException( nameof( n ), "Must be positive." );
            }

            return ( float ) Math.Pow( 2.0, Math.Ceiling( Math.Log( ( double ) n, 2.0 ) ) );
        }

        public static double NextPowerOfTwo( double n )
        {
            if ( n < 0.0 )
            {
                throw new ArgumentOutOfRangeException( nameof( n ), "Must be positive." );
            }

            return Math.Pow( 2.0, Math.Ceiling( Math.Log( n, 2.0 ) ) );
        }

        public static long Factorial( int n )
        {
            long num = 1;
            for ( ; n > 1 ; --n )
            {
                num *= ( long ) n;
            }

            return num;
        }

        public static long BinomialCoefficient( int n, int k )
        {
            return MathHelper.Factorial( n ) / ( MathHelper.Factorial( k ) * MathHelper.Factorial( n - k ) );
        }

        public static unsafe float InverseSqrtFast( float x )
        {
            float xhalf = 0.5f * x;
            int i = *(int*)&x;              // Read bits as integer.
            i = 0x5f375a86 - ( i >> 1 );      // Make an initial guess for Newton-Raphson approximation
            x = *( float* ) &i;                // Convert bits back to float
            x = x * ( 1.5f - xhalf * x * x ); // Perform left single Newton-Raphson step.
            return x;
        }

        public static double InverseSqrtFast( double x )
        {
            return ( double ) MathHelper.InverseSqrtFast( ( float ) x );
        }

        public static double Range0ToTau( double Value )
        {
            if ( Value < 0.0 )
            {
                Value += 6.28318548202515;
            }

            if ( Value >= 6.28318548202515 )
            {
                Value -= 6.28318548202515;
            }

            if ( Value < 0.0 || Value > 6.28318548202515 )
            {
                throw new Exception( "Value >= 0 && Value <= Tau" );
            }

            return Value;
        }

        public static double GetDeltaAngle( double StartAngle, double EndAngle )
        {
            if ( StartAngle != MathHelper.Range0ToTau( StartAngle ) )
            {
                throw new Exception( "StartAngle != Range0ToTau(StartAngle)" );
            }

            if ( EndAngle != MathHelper.Range0ToTau( EndAngle ) )
            {
                throw new Exception( "EndAngle != Range0ToTau(EndAngle)" );
            }

            double num = EndAngle - StartAngle;
            if ( num > 3.14159274101257 )
            {
                num -= 6.28318548202515;
            }

            if ( num < -3.14159274101257 )
            {
                num += 6.28318548202515;
            }

            return num;
        }

        public static double DegreesToRadians( double degrees )
        {
            return degrees * ( Math.PI / 180.0 );
        }

        public static double RadiansToDegrees( double radians )
        {
            return radians * ( 180.0 / Math.PI );
        }

        public static void Swap( ref double a, ref double b )
        {
            double num = a;
            a = b;
            b = num;
        }

        public static void Swap( ref float a, ref float b )
        {
            float num = a;
            a = b;
            b = num;
        }

        public static bool AlmostEqual( double a, double b, double differenceAllowed )
        {
            return a > b - differenceAllowed && a < b + differenceAllowed;
        }
    }
}
