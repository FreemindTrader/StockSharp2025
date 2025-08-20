// fx.Xaml.Charting.Numerics.GenericMath.ArrayOperations
using System;
using System.Collections.Generic;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace StockSharp.Xaml.Charting;

internal static class ArrayOperations
{
    private interface IGenericArrayHelper<T>
    {
        T Maximum( T[ ] array, int startIndex, int count );

        void MinMax( T[ ] array, int startIndex, int count, out T min, out T max );

        bool IsSortedAscending( T[ ] array, int startIndex, int count );

        bool IsSortedAscending( IList<T> items, int startIndex, int count );

        bool IsEvenlySpaced( T[ ] array, int startIndex, int count, double epsilon, out double spacing );

        bool IsEvenlySpaced( IList<T> items, int startIndex, int count, double epsilon, out double spacing );
    }

    private class DecimalGenericArrayHelper : IGenericArrayHelper<decimal>
    {
        public unsafe decimal Maximum( decimal[ ] array, int startIndex, int count )
        {
            decimal num = GenericMathFactory.GetMath<decimal>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( decimal* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    decimal num3 = *(decimal*)((byte*)ptr + (long)i * 16L);
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                decimal num3 = *(decimal*)((byte*)ptr + (long)i * 16L);
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( decimal[ ] array, int startIndex, int count, out decimal min, out decimal max )
        {
            IMath<decimal> math = GenericMathFactory.GetMath<decimal>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( decimal* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    decimal num2 = *(decimal*)((byte*)ptr + (long)i * 16L);
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L );
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                decimal num2 = *(decimal*)((byte*)ptr + (long)i * 16L);
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( decimal[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( decimal* ptr = &array[ 0 ] )
            {
                decimal d = *(decimal*)((byte*)ptr + (long)i++ * 16L);
                for ( ; i < num ; i++ )
                {
                    decimal num3 = *(decimal*)((byte*)ptr + (long)i * 16L);
                    if ( num3 < d )
                    {
                        return false;
                    }
                    d = num3;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<decimal> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( decimal[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( decimal* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(*(decimal*)((byte*)ptr + (long)startIndex++ * 16L));
                double num5 = TxToDouble(*(decimal*)((byte*)ptr + (long)startIndex++ * 16L));
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( *( decimal* ) ( ( byte* ) ptr + ( long ) i * 16L ) );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<decimal> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( decimal xValue )
        {
            return ( double ) xValue;
        }
    }

    private class DoubleGenericArrayHelper : IGenericArrayHelper<double>
    {
        public unsafe double Maximum( double[ ] array, int startIndex, int count )
        {
            double num = GenericMathFactory.GetMath<double>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( double* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    double num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                double num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( double[ ] array, int startIndex, int count, out double min, out double max )
        {
            IMath<double> math = GenericMathFactory.GetMath<double>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( double* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    double num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                double num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( double[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( double* ptr = &array[ 0 ] )
            {
                double num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    double num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<double> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( double[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( double* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<double> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( double xValue )
        {
            return xValue;
        }
    }

    private class SingleGenericArrayHelper : IGenericArrayHelper<float>
    {
        public unsafe float Maximum( float[ ] array, int startIndex, int count )
        {
            float num = GenericMathFactory.GetMath<float>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( float* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    float num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                float num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( float[ ] array, int startIndex, int count, out float min, out float max )
        {
            IMath<float> math = GenericMathFactory.GetMath<float>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( float* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    float num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                float num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( float[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( float* ptr = &array[ 0 ] )
            {
                float num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    float num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<float> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( float[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( float* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<float> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( float xValue )
        {
            return ( double ) xValue;
        }
    }

    private class Int32GenericArrayHelper : IGenericArrayHelper<int>
    {
        public unsafe int Maximum( int[ ] array, int startIndex, int count )
        {
            int num = GenericMathFactory.GetMath<int>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( int* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    int num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                int num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( int[ ] array, int startIndex, int count, out int min, out int max )
        {
            IMath<int> math = GenericMathFactory.GetMath<int>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( int* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    int num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                int num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( int[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( int* ptr = &array[ 0 ] )
            {
                int num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    int num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<int> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( int[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( int* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<int> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( int xValue )
        {
            return ( double ) xValue;
        }
    }

    private class UInt32GenericArrayHelper : IGenericArrayHelper<uint>
    {
        public unsafe uint Maximum( uint[ ] array, int startIndex, int count )
        {
            uint num = GenericMathFactory.GetMath<uint>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( uint* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    uint num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                uint num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( uint[ ] array, int startIndex, int count, out uint min, out uint max )
        {
            IMath<uint> math = GenericMathFactory.GetMath<uint>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( uint* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    uint num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                uint num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( uint[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( uint* ptr = &array[ 0 ] )
            {
                uint num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    uint num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<uint> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( uint[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( uint* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<uint> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( uint xValue )
        {
            return ( double ) xValue;
        }
    }

    private class Int64GenericArrayHelper : IGenericArrayHelper<long>
    {
        public unsafe long Maximum( long[ ] array, int startIndex, int count )
        {
            long num = GenericMathFactory.GetMath<long>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( long* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    long num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                long num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( long[ ] array, int startIndex, int count, out long min, out long max )
        {
            IMath<long> math = GenericMathFactory.GetMath<long>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( long* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    long num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                long num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( long[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( long* ptr = &array[ 0 ] )
            {
                long num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    long num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<long> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( long[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( long* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<long> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( long xValue )
        {
            return ( double ) xValue;
        }
    }

    private class UInt64GenericArrayHelper : IGenericArrayHelper<ulong>
    {
        public unsafe ulong Maximum( ulong[ ] array, int startIndex, int count )
        {
            ulong num = GenericMathFactory.GetMath<ulong>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( ulong* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    ulong num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                ulong num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( ulong[ ] array, int startIndex, int count, out ulong min, out ulong max )
        {
            IMath<ulong> math = GenericMathFactory.GetMath<ulong>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( ulong* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    ulong num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                ulong num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( ulong[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( ulong* ptr = &array[ 0 ] )
            {
                ulong num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    ulong num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<ulong> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( ulong[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( ulong* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<ulong> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( ulong xValue )
        {
            return ( double ) xValue;
        }
    }

    private class Int16GenericArrayHelper : IGenericArrayHelper<short>
    {
        public unsafe short Maximum( short[ ] array, int startIndex, int count )
        {
            short num = GenericMathFactory.GetMath<short>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( short* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    short num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                short num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( short[ ] array, int startIndex, int count, out short min, out short max )
        {
            IMath<short> math = GenericMathFactory.GetMath<short>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( short* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    short num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                short num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( short[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( short* ptr = &array[ 0 ] )
            {
                short num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    short num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<short> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( short[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( short* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<short> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( short xValue )
        {
            return ( double ) xValue;
        }
    }

    private class UInt16GenericArrayHelper : IGenericArrayHelper<ushort>
    {
        public unsafe ushort Maximum( ushort[ ] array, int startIndex, int count )
        {
            ushort num = GenericMathFactory.GetMath<ushort>().MinValue;
            switch ( count )
            {
                case 0:
                    return num;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( ushort* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num2 = count - count % 16;
                                while ( i != num2 )
                                {
                                    ushort num3 = ptr[i];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                    num3 = ptr[ i ];
                                    num = ( ( num3 > num ) ? num3 : num );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                ushort num3 = ptr[i];
                                num = ( ( num3 > num ) ? num3 : num );
                            }
                            return num;
                        }
                    }
            }
        }

        public unsafe void MinMax( ushort[ ] array, int startIndex, int count, out ushort min, out ushort max )
        {
            IMath<ushort> math = GenericMathFactory.GetMath<ushort>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( ushort* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    ushort num2 = ptr[i];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                    num2 = ptr[ i ];
                                    max = ( ( num2 > max ) ? num2 : max );
                                    min = ( ( num2 < min ) ? num2 : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                ushort num2 = ptr[i];
                                max = ( ( num2 > max ) ? num2 : max );
                                min = ( ( num2 < min ) ? num2 : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( ushort[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( ushort* ptr = &array[ 0 ] )
            {
                ushort num3 = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    ushort num4 = ptr[i];
                    if ( num4 < num3 )
                    {
                        return false;
                    }
                    num3 = num4;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<ushort> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( ushort[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( ushort* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<ushort> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( ushort xValue )
        {
            return ( double ) ( int ) xValue;
        }
    }

    private class ByteGenericArrayHelper : IGenericArrayHelper<byte>
    {
        public unsafe byte Maximum( byte[ ] array, int startIndex, int count )
        {
            byte b = GenericMathFactory.GetMath<byte>().MinValue;
            switch ( count )
            {
                case 0:
                    return b;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( byte* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    byte b2 = ptr[i];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                byte b2 = ptr[i];
                                b = ( ( b2 > b ) ? b2 : b );
                            }
                            return b;
                        }
                    }
            }
        }

        public unsafe void MinMax( byte[ ] array, int startIndex, int count, out byte min, out byte max )
        {
            IMath<byte> math = GenericMathFactory.GetMath<byte>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( byte* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    byte b = ptr[i];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                byte b = ptr[i];
                                max = ( ( b > max ) ? b : max );
                                min = ( ( b < min ) ? b : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( byte[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( byte* ptr = &array[ 0 ] )
            {
                byte b = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    byte b2 = ptr[i];
                    if ( b2 < b )
                    {
                        return false;
                    }
                    b = b2;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<byte> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( byte[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( byte* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<byte> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( byte xValue )
        {
            return ( double ) ( int ) xValue;
        }
    }

    private class SByteGenericArrayHelper : IGenericArrayHelper<sbyte>
    {
        public unsafe sbyte Maximum( sbyte[ ] array, int startIndex, int count )
        {
            sbyte b = GenericMathFactory.GetMath<sbyte>().MinValue;
            switch ( count )
            {
                case 0:
                    return b;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( sbyte* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    sbyte b2 = ptr[i];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                    b2 = ptr[ i ];
                                    b = ( ( b2 > b ) ? b2 : b );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                sbyte b2 = ptr[i];
                                b = ( ( b2 > b ) ? b2 : b );
                            }
                            return b;
                        }
                    }
            }
        }

        public unsafe void MinMax( sbyte[ ] array, int startIndex, int count, out sbyte min, out sbyte max )
        {
            IMath<sbyte> math = GenericMathFactory.GetMath<sbyte>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( sbyte* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    sbyte b = ptr[i];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                    b = ptr[ i ];
                                    max = ( ( b > max ) ? b : max );
                                    min = ( ( b < min ) ? b : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                sbyte b = ptr[i];
                                max = ( ( b > max ) ? b : max );
                                min = ( ( b < min ) ? b : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( sbyte[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( sbyte* ptr = &array[ 0 ] )
            {
                sbyte b = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    sbyte b2 = ptr[i];
                    if ( b2 < b )
                    {
                        return false;
                    }
                    b = b2;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<sbyte> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( sbyte[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( sbyte* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<sbyte> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( sbyte xValue )
        {
            return ( double ) xValue;
        }
    }

    private class DateTimeGenericArrayHelper : IGenericArrayHelper<DateTime>
    {
        public unsafe DateTime Maximum( DateTime[ ] array, int startIndex, int count )
        {
            DateTime dateTime = GenericMathFactory.GetMath<DateTime>().MinValue;
            switch ( count )
            {
                case 0:
                    return dateTime;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( DateTime* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    DateTime dateTime2 = ptr[i];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                    dateTime2 = ptr[ i ];
                                    dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                DateTime dateTime2 = ptr[i];
                                dateTime = ( ( dateTime2 > dateTime ) ? dateTime2 : dateTime );
                            }
                            return dateTime;
                        }
                    }
            }
        }

        public unsafe void MinMax( DateTime[ ] array, int startIndex, int count, out DateTime min, out DateTime max )
        {
            IMath<DateTime> math = GenericMathFactory.GetMath<DateTime>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( DateTime* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    DateTime dateTime = ptr[i];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                    dateTime = ptr[ i ];
                                    max = ( ( dateTime > max ) ? dateTime : max );
                                    min = ( ( dateTime < min ) ? dateTime : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                DateTime dateTime = ptr[i];
                                max = ( ( dateTime > max ) ? dateTime : max );
                                min = ( ( dateTime < min ) ? dateTime : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( DateTime[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( DateTime* ptr = &array[ 0 ] )
            {
                DateTime t = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    DateTime dateTime = ptr[i];
                    if ( dateTime < t )
                    {
                        return false;
                    }
                    t = dateTime;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<DateTime> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( DateTime[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( DateTime* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<DateTime> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( DateTime xValue )
        {
            return ( double ) xValue.Ticks;
        }
    }

    private class TimeSpanGenericArrayHelper : IGenericArrayHelper<TimeSpan>
    {
        public unsafe TimeSpan Maximum( TimeSpan[ ] array, int startIndex, int count )
        {
            TimeSpan timeSpan = GenericMathFactory.GetMath<TimeSpan>().MinValue;
            switch ( count )
            {
                case 0:
                    return timeSpan;
                case 1:
                    return array[ 0 ];
                default:
                    {
                        int i = startIndex;
                        fixed ( TimeSpan* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    TimeSpan timeSpan2 = ptr[i];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                    timeSpan2 = ptr[ i ];
                                    timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                TimeSpan timeSpan2 = ptr[i];
                                timeSpan = ( ( timeSpan2 > timeSpan ) ? timeSpan2 : timeSpan );
                            }
                            return timeSpan;
                        }
                    }
            }
        }

        public unsafe void MinMax( TimeSpan[ ] array, int startIndex, int count, out TimeSpan min, out TimeSpan max )
        {
            IMath<TimeSpan> math = GenericMathFactory.GetMath<TimeSpan>();
            max = math.MinValue;
            min = math.MaxValue;
            switch ( count )
            {
                case 0:
                    break;
                case 1:
                    min = array[ 0 ];
                    max = array[ 0 ];
                    break;
                default:
                    {
                        int i = startIndex;
                        fixed ( TimeSpan* ptr = &array[ 0 ] )
                        {
                            if ( array.Length > 16 )
                            {
                                int num = count - count % 16;
                                while ( i != num )
                                {
                                    TimeSpan timeSpan = ptr[i];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                    timeSpan = ptr[ i ];
                                    max = ( ( timeSpan > max ) ? timeSpan : max );
                                    min = ( ( timeSpan < min ) ? timeSpan : min );
                                    i++;
                                }
                            }
                            for ( ; i != count ; i++ )
                            {
                                TimeSpan timeSpan = ptr[i];
                                max = ( ( timeSpan > max ) ? timeSpan : max );
                                min = ( ( timeSpan < min ) ? timeSpan : min );
                            }
                        }
                        break;
                    }
            }
        }

        public unsafe bool IsSortedAscending( TimeSpan[ ] array, int startIndex, int count )
        {
            int num = startIndex + count;
            int i = startIndex;
            fixed ( TimeSpan* ptr = &array[ 0 ] )
            {
                TimeSpan t = ptr[i++];
                for ( ; i < num ; i++ )
                {
                    TimeSpan timeSpan = ptr[i];
                    if ( timeSpan < t )
                    {
                        return false;
                    }
                    t = timeSpan;
                }
            }
            return true;
        }

        public bool IsSortedAscending( IList<TimeSpan> items, int startIndex, int count )
        {
            if ( count <= 1 )
            {
                return true;
            }
            return IsSortedAscending( items.ToUncheckedList(), startIndex, count );
        }

        public unsafe bool IsEvenlySpaced( TimeSpan[ ] array, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( array[ startIndex ] ) - TxToDouble( array[ startIndex + 1 ] ) );
                return true;
            }
            int num = startIndex + count;
            fixed ( TimeSpan* ptr = &array[ 0 ] )
            {
                double num3 = TxToDouble(ptr[startIndex++]);
                double num5 = TxToDouble(ptr[startIndex++]);
                double num6 = num5 - num3;
                num3 = num5;
                for ( int i = startIndex ; i != num ; i++ )
                {
                    num5 = TxToDouble( ptr[ i ] );
                    double num7 = num5 - num3;
                    if ( Math.Abs( num6 - num7 ) > epsilon )
                    {
                        spacing = Math.Abs( num6 );
                        return false;
                    }
                    num6 = num7;
                    num3 = num5;
                }
                spacing = Math.Abs( num6 );
            }
            return true;
        }

        public bool IsEvenlySpaced( IList<TimeSpan> items, int startIndex, int count, double epsilon, out double spacing )
        {
            if ( count <= 1 )
            {
                spacing = 1.0;
                return true;
            }
            if ( count == 2 )
            {
                spacing = Math.Abs( TxToDouble( items[ startIndex ] ) - TxToDouble( items[ startIndex + 1 ] ) );
                return true;
            }
            return IsEvenlySpaced( items.ToUncheckedList(), startIndex, count, epsilon, out spacing );
        }

        private static double TxToDouble( TimeSpan xValue )
        {
            return ( double ) xValue.Ticks;
        }
    }

    private static readonly IDictionary<Type, object> _genericArrayHelpers;

    static ArrayOperations()
    {
        _genericArrayHelpers = new Dictionary<Type, object>();
        _genericArrayHelpers.Add( typeof( decimal ), new DecimalGenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( double ), new DoubleGenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( float ), new SingleGenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( int ), new Int32GenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( uint ), new UInt32GenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( long ), new Int64GenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( ulong ), new UInt64GenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( short ), new Int16GenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( ushort ), new UInt16GenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( byte ), new ByteGenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( sbyte ), new SByteGenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( DateTime ), new DateTimeGenericArrayHelper() );
        _genericArrayHelpers.Add( typeof( TimeSpan ), new TimeSpanGenericArrayHelper() );
    }

    internal static T Maximum<T>( IEnumerable<T> enumerable )
    {
        T[] array = enumerable as T[];
        if ( array != null )
        {
            return Maximum( array, 0, array.Length );
        }
        IList<T> list = enumerable as IList<T>;
        if ( list != null )
        {
            return Maximum( list.ToUncheckedList(), 0, ( ( ICollection<T> ) list ).Count );
        }
        IMath<T> math = GenericMathFactory.GetMath<T>();
        T val = math.MinValue;
        foreach ( T item in enumerable )
        {
            val = math.Max( val, item );
        }
        return val;
    }

    internal static T Minimum<T>( IEnumerable<T> enumerable )
    {
        IMath<T> math = GenericMathFactory.GetMath<T>();
        IMath<T> math2 = math;
        return Minimum( enumerable, math2.Min );
    }

    internal static T MinGreaterThan<T>( IEnumerable<T> enumerable, T floor )
    {
        IMath<T> math = (IMath<T>)GenericMathFactory.GetMath<T>();
        return Minimum( enumerable, ( T a, T b ) => ( ( IMath<T> ) math ).MinGreaterThan( floor, a, b ) );
    }

    internal static T Minimum<T>( IEnumerable<T> enumerable, Func<T, T, T> minFunc )
    {
        T[] array = enumerable as T[];
        if ( array != null )
        {
            return Minimum( array, 0, array.Length, minFunc );
        }
        IList<T> list = enumerable as IList<T>;
        if ( list != null )
        {
            return Minimum( list.ToUncheckedList(), 0, ( ( ICollection<T> ) list ).Count, minFunc );
        }
        IMath<T> math = GenericMathFactory.GetMath<T>();
        T val = math.MaxValue;
        foreach ( T item in enumerable )
        {
            val = minFunc( val, item );
        }
        return val;
    }

    public static void MinMax<T>( IEnumerable<T> enumerable, out T min, out T max )
    {
        T[] array = enumerable as T[];
        if ( array != null )
        {
            MinMax( array, 0, array.Length, out min, out max );
        }
        else
        {
            IList<T> list = enumerable as IList<T>;
            if ( list != null )
            {
                MinMax( list.ToUncheckedList(), 0, ( ( ICollection<T> ) list ).Count, out min, out max );
            }
            else
            {
                IMath<T> math = GenericMathFactory.GetMath<T>();
                max = math.MinValue;
                min = math.MaxValue;
                foreach ( T item in enumerable )
                {
                    max = math.Max( max, item );
                    min = math.Min( min, item );
                }
            }
        }
    }

    public static bool IsSortedAscending<T>( IEnumerable<T> enumerable ) where T : IComparable
    {
        T[] array = enumerable as T[];
        if ( array != null )
        {
            return IsSortedAscending( array, 0, array.Length );
        }
        IList<T> list = enumerable as IList<T>;
        if ( list != null )
        {
            return IsSortedAscending( list.ToUncheckedList(), 0, ( ( ICollection<T> ) list ).Count );
        }
        IEnumerator<T> enumerator = enumerable.GetEnumerator();
        if ( !enumerator.MoveNext() )
        {
            return true;
        }
        T current = enumerator.Current;
        while ( enumerator.MoveNext() )
        {
            if ( enumerator.Current.CompareTo( current ) < 0 )
            {
                return false;
            }
            current = enumerator.Current;
        }
        return true;
    }

    public static bool IsEvenlySpaced<T>( IEnumerable<T> enumerable, double epsilon, out double spacing ) where T : IComparable
    {
        T[] array = enumerable as T[];
        if ( array != null )
        {
            return IsEvenlySpaced( array, 0, array.Length, epsilon, out spacing );
        }
        IList<T> list = enumerable as IList<T>;
        if ( list != null )
        {
            return IsEvenlySpaced( list.ToUncheckedList(), 0, ( ( ICollection<T> ) list ).Count, epsilon, out spacing );
        }
        IMath<T> math = GenericMathFactory.GetMath<T>();
        IEnumerator<T> enumerator = enumerable.GetEnumerator();
        if ( !enumerator.MoveNext() )
        {
            spacing = 1.0;
            return true;
        }
        double num = math.ToDouble(enumerator.Current);
        if ( !enumerator.MoveNext() )
        {
            spacing = 1.0;
            return true;
        }
        double num2 = math.ToDouble(enumerator.Current);
        double num3 = num2 - num;
        num = num2;
        while ( enumerator.MoveNext() )
        {
            num2 = math.ToDouble( enumerator.Current );
            double num4 = num2 - num;
            if ( Math.Abs( num3 - num4 ) > epsilon )
            {
                spacing = Math.Abs( num3 );
                return false;
            }
            num3 = num4;
            num = num2;
        }
        spacing = Math.Abs( num3 );
        return true;
    }

    internal static T Minimum<T>( T[ ] array, int startIndex, int count )
    {
        IMath<T> math = GenericMathFactory.GetMath<T>();
        IMath<T> math2 = math;
        return Minimum( array, startIndex, count, math2.Min );
    }

    internal static T Minimum<T>( T[ ] array, int startIndex, int count, Func<T, T, T> minFunc )
    {
        IMath<T> math = GenericMathFactory.GetMath<T>();
        T val = math.MaxValue;
        int i = startIndex;
        if ( array.Length > 16 )
        {
            int num = count - count % 16;
            while ( i != num )
            {
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
                val = minFunc( val, array[ i ] );
                i++;
            }
        }
        for ( ; i != count ; i++ )
        {
            val = minFunc( val, array[ i ] );
        }
        return val;
    }

    internal static T Maximum<T>( T[ ] array, int startIndex, int count )
    {
        return ( ( IGenericArrayHelper<T> ) _genericArrayHelpers[ typeof( T ) ] ).Maximum( array, startIndex, count );
    }

    internal static void MinMax<T>( T[ ] array, int startIndex, int count, out T min, out T max )
    {
        ( ( IGenericArrayHelper<T> ) _genericArrayHelpers[ typeof( T ) ] ).MinMax( array, startIndex, count, out min, out max );
    }

    internal static bool IsSortedAscending<T>( T[ ] array, int startIndex, int count )
    {
        return ( ( IGenericArrayHelper<T> ) _genericArrayHelpers[ typeof( T ) ] ).IsSortedAscending( array, startIndex, count );
    }

    internal static bool IsSortedAscending<T>( IList<T> items, int startIndex, int count )
    {
        return ( ( IGenericArrayHelper<T> ) _genericArrayHelpers[ typeof( T ) ] ).IsSortedAscending( items, startIndex, count );
    }

    internal static bool IsEvenlySpaced<T>( T[ ] array, int startIndex, int count, double epsilon, out double spacing )
    {
        return ( ( IGenericArrayHelper<T> ) _genericArrayHelpers[ typeof( T ) ] ).IsEvenlySpaced( array, startIndex, count, epsilon, out spacing );
    }

    internal static bool IsEvenlySpaced<T>( IList<T> items, int startIndex, int count, double epsilon, out double spacing )
    {
        return ( ( IGenericArrayHelper<T> ) _genericArrayHelpers[ typeof( T ) ] ).IsEvenlySpaced( items, startIndex, count, epsilon, out spacing );
    }
}
