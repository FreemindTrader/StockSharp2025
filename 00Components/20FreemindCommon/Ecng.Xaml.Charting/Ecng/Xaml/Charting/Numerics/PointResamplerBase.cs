// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.PointResamplerBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics.GenericMath;

namespace Ecng.Xaml.Charting.Numerics
{
    internal abstract class PointResamplerBase : IPointResampler
    {
        private const int _binsWidth = 400;
        private const int _binsHeight = 300;
        private static byte[] _bins;

        internal static bool RequiresReduction( ResamplingMode resamplingMode, IndexRange pointIndices, int viewportWidth )
        {
            int num1 = pointIndices.Max - pointIndices.Min + 1;
            int num2 = 4 * viewportWidth;
            return ( uint ) resamplingMode > 0U & num1 > num2;
        }

        public abstract IPointSeries Execute( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isFifo, bool isCategoryAxis, IList xColumn, IList yColumn, bool? dataIsSorted, bool? dataIsEvenlySpaced, bool? dataIsDisplayedAs2d, IRange visibleXRange );

        private static void GetXRange<TX>( IList<TX> xColumn, IndexRange pointRange, IRange visibleXRange, out double minXInclusive, out double maxXInclusive ) where TX : IComparable
        {
            minXInclusive = xColumn[ pointRange.Min ].ToDouble();
            maxXInclusive = xColumn[ pointRange.Max ].ToDouble();
            if ( visibleXRange == null || visibleXRange is IndexRange )
                return;
            minXInclusive = visibleXRange.Min.ToDouble();
            maxXInclusive = visibleXRange.Max.ToDouble();
        }

        private static bool GetMinMaxValuesForPixel<TX, TY>( IMath<TX> xMath, IMath<TY> yMath, UncheckedList<TX> xValues, UncheckedList<TY> yValues, int pointerToNextElement, int maxRemainingNumberOfPointsInPixel, double pixelEndXInclusive, out double minYInPixel, out double maxYInPixel, out int numberOfPointsInPixel ) where TX : IComparable where TY : IComparable
        {
            double d = yMath.ToDouble(yValues[pointerToNextElement]);
            double num = xMath.ToDouble(xValues[pointerToNextElement]);
            numberOfPointsInPixel = 0;
            minYInPixel = d;
            maxYInPixel = d;
            bool flag = false;
            for ( ; num <= pixelEndXInclusive ; num = xMath.ToDouble( xValues[ pointerToNextElement ] ) )
            {
                if ( numberOfPointsInPixel == 0 )
                    flag = double.IsNaN( d );
                else if ( flag != double.IsNaN( d ) )
                    return false;
                if ( d <= minYInPixel )
                    minYInPixel = d;
                if ( d >= maxYInPixel )
                    maxYInPixel = d;
                ++numberOfPointsInPixel;
                if ( numberOfPointsInPixel < maxRemainingNumberOfPointsInPixel )
                {
                    ++pointerToNextElement;
                    d = yMath.ToDouble( yValues[ pointerToNextElement ] );
                }
                else
                    break;
            }
            return true;
        }

        private static unsafe bool GetMinMaxValuesForPixel( double* xValues, double* yValues, int maxRemainingNumberOfPointsInPixel, double pixelEndXInclusive, out double minYInPixel, out double maxYInPixel, out int numberOfPointsInPixel )
        {
            double d = *yValues;
            double num = *xValues;
            numberOfPointsInPixel = 0;
            minYInPixel = d;
            maxYInPixel = d;
            bool flag = false;
            for ( ; num <= pixelEndXInclusive ; num = *xValues )
            {
                if ( numberOfPointsInPixel == 0 )
                    flag = double.IsNaN( d );
                else if ( flag != double.IsNaN( d ) )
                    return false;
                if ( d <= minYInPixel )
                    minYInPixel = d;
                if ( d >= maxYInPixel )
                    maxYInPixel = d;
                ++numberOfPointsInPixel;
                if ( numberOfPointsInPixel < maxRemainingNumberOfPointsInPixel )
                {
                    xValues += 8;
                    yValues += 8;
                    d = *yValues;
                }
                else
                    break;
            }
            return true;
        }

        protected static IPointSeries ReducePointsMinMaxUnevenlySpaced<TX, TY>( IList<TX> xColumn, IList<TY> yColumn, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, IRange visibleXRange ) where TX : IComparable where TY : IComparable
        {
            double minXInclusive;
            double maxXInclusive;
            PointResamplerBase.GetXRange<TX>( xColumn, pointRange, visibleXRange, out minXInclusive, out maxXInclusive );
            int max = pointRange.Max;
            int min = pointRange.Min;
            double num = (maxXInclusive - minXInclusive) / (double) viewportWidth;
            Point2DSeries reducedPoints = new Point2DSeries(viewportWidth * 2 + 1);
            UncheckedList<TY> uncheckedList1 = yColumn.ToUncheckedList<TY>(true);
            UncheckedList<TX> uncheckedList2 = xColumn.ToUncheckedList<TX>(true);
            bool flag = false;
            if ( uncheckedList2 != null && uncheckedList1 != null )
            {
                UncheckedList<double> xValues1 = uncheckedList2 as UncheckedList<double>;
                UncheckedList<double> yValues1 = uncheckedList1 as UncheckedList<double>;
                if ( xValues1 != null && yValues1 != null )
                {
                    PointResamplerBase.ReducePointUnevenImpl( xValues1, yValues1, reducedPoints, min, max, minXInclusive, maxXInclusive, viewportWidth, isCategoryAxis );
                    flag = true;
                }
                if ( !flag )
                {
                    UncheckedList<double> xValues2 = uncheckedList2 as UncheckedList<double>;
                    UncheckedList<float> yValues2 = uncheckedList1 as UncheckedList<float>;
                    if ( xValues2 != null && yValues2 != null )
                    {
                        PointResamplerBase.ReducePointUnevenImpl<double, float>( xValues2, yValues2, reducedPoints, min, max, minXInclusive, maxXInclusive, viewportWidth, isCategoryAxis );
                        flag = true;
                    }
                }
            }
            if ( !flag )
                PointResamplerBase.ReducePointUnevenImpl<TX, TY>( uncheckedList2, uncheckedList1, reducedPoints, min, max, minXInclusive, maxXInclusive, viewportWidth, isCategoryAxis );
            reducedPoints.Freeze();
            return ( IPointSeries ) reducedPoints;
        }

        private static unsafe void ReducePointUnevenImpl( UncheckedList<double> xValues, UncheckedList<double> yValues, Point2DSeries reducedPoints, int startIndexInclusive, int endIndexInclusive, double minXInclusive, double maxXInclusive, int viewportWidth, bool isCategoryAxis )
        {
            fixed ( double* numPtr1 = &xValues.Array[ startIndexInclusive ] )
            fixed ( double* numPtr2 = &yValues.Array[ startIndexInclusive ] )
            {
                IUltraList<double> xvalues = reducedPoints.XValues;
                IUltraList<double> yvalues = reducedPoints.YValues;
                double* xValues1 = numPtr1;
                double* yValues1 = numPtr2;
                int num1 = startIndexInclusive;
                bool flag = true;
                double num2 = (maxXInclusive - minXInclusive) / (double) viewportWidth;
                int num3 = 0;
                while ( num3 < viewportWidth )
                {
                    double minYInPixel;
                    double maxYInPixel;
                    int numberOfPointsInPixel;
                    if ( PointResamplerBase.GetMinMaxValuesForPixel( xValues1, yValues1, endIndexInclusive - num1 + 1, minXInclusive + num2 * ( double ) ( num3 + 1 ), out minYInPixel, out maxYInPixel, out numberOfPointsInPixel ) )
                        ++num3;
                    double num4 = isCategoryAxis ? (double) num1 : *xValues1;
                    if ( numberOfPointsInPixel != 0 )
                    {
                        if ( flag )
                        {
                            xvalues.Add( num4 );
                            yvalues.Add( *yValues1 );
                        }
                        xvalues.Add( num4 );
                        yvalues.Add( minYInPixel );
                        xvalues.Add( num4 );
                        yvalues.Add( maxYInPixel );
                    }
                    else if ( !flag )
                    {
                        xvalues.Add( *( double* ) ( ( IntPtr ) xValues1 - 8 ) );
                        yvalues.Add( *( double* ) ( ( IntPtr ) yValues1 - 8 ) );
                    }
                    num1 += numberOfPointsInPixel;
                    xValues1 += numberOfPointsInPixel;
                    yValues1 += numberOfPointsInPixel;
                    if ( num1 <= endIndexInclusive )
                        flag = numberOfPointsInPixel == 0;
                    else
                        break;
                }
                if ( num1 <= endIndexInclusive )
                {
                    double num4 = isCategoryAxis ? (double) num1 : *xValues1;
                    xvalues.Add( num4 );
                    yvalues.Add( *yValues1 );
                }
            }
        }

        private static void ReducePointUnevenImpl<TX, TY>( UncheckedList<TX> xValues, UncheckedList<TY> yValues, Point2DSeries reducedPoints, int startIndexInclusive, int endIndexInclusive, double minXInclusive, double maxXInclusive, int viewportWidth, bool isCategoryAxis ) where TX : IComparable where TY : IComparable
        {
            IMath<TX> xMath = GenericMathFactory.New<TX>();
            IMath<TY> yMath = GenericMathFactory.New<TY>();
            IUltraList<double> xvalues = reducedPoints.XValues;
            IUltraList<double> yvalues = reducedPoints.YValues;
            int pointerToNextElement = startIndexInclusive;
            bool flag = true;
            double num1 = (maxXInclusive - minXInclusive) / (double) viewportWidth;
            int num2 = 0;
            while ( num2 < viewportWidth )
            {
                double minYInPixel;
                double maxYInPixel;
                int numberOfPointsInPixel;
                if ( PointResamplerBase.GetMinMaxValuesForPixel<TX, TY>( xMath, yMath, xValues, yValues, pointerToNextElement, endIndexInclusive - pointerToNextElement + 1, minXInclusive + num1 * ( double ) ( num2 + 1 ), out minYInPixel, out maxYInPixel, out numberOfPointsInPixel ) )
                    ++num2;
                double num3 = isCategoryAxis ? (double) pointerToNextElement : xMath.ToDouble(xValues[pointerToNextElement]);
                if ( numberOfPointsInPixel != 0 )
                {
                    if ( flag )
                    {
                        xvalues.Add( num3 );
                        yvalues.Add( yMath.ToDouble( yValues[ pointerToNextElement ] ) );
                    }
                    xvalues.Add( num3 );
                    yvalues.Add( minYInPixel );
                    xvalues.Add( num3 );
                    yvalues.Add( maxYInPixel );
                }
                else if ( !flag )
                {
                    xvalues.Add( xMath.ToDouble( xValues[ pointerToNextElement - 1 ] ) );
                    yvalues.Add( yMath.ToDouble( yValues[ pointerToNextElement - 1 ] ) );
                }
                pointerToNextElement += numberOfPointsInPixel;
                if ( pointerToNextElement <= endIndexInclusive )
                    flag = numberOfPointsInPixel == 0;
                else
                    break;
            }
            if ( pointerToNextElement > endIndexInclusive )
                return;
            double num4 = isCategoryAxis ? (double) pointerToNextElement : xMath.ToDouble(xValues[pointerToNextElement]);
            xvalues.Add( num4 );
            yvalues.Add( yMath.ToDouble( yValues[ pointerToNextElement ] ) );
        }

        [DllImport( "msvcrt.dll", CallingConvention = CallingConvention.Cdecl )]
        private static extern void memset( IntPtr dst, int filler, int count );

        protected static IPointSeries ResampleInClusterMode<TX, TY>( IList<TX> xColumn, IList<TY> yColumn, IndexRange pointRange, int viewportWidth, bool isCategoryAxis ) where TX : IComparable where TY : IComparable
        {
            IMath<TX> math1 = GenericMathFactory.New<TX>();
            IMath<TY> math2 = GenericMathFactory.New<TY>();
            if ( PointResamplerBase._bins == null )
            {
                PointResamplerBase._bins = new byte[ 120000 ];
            }
            else
            {
                GCHandle gcHandle = GCHandle.Alloc((object) PointResamplerBase._bins, GCHandleType.Pinned);
                PointResamplerBase.memset( gcHandle.AddrOfPinnedObject(), 0, PointResamplerBase._bins.Length );
                gcHandle.Free();
            }
            TX min1;
            TX max1;
            ArrayOperations.MinMax<TX>( ( IEnumerable<TX> ) xColumn, out min1, out max1 );
            double num1 = !max1.Equals((object) min1) ? 399.0 / math1.Subtract(max1, min1).ToDouble() : 0.0;
            TY min2;
            TY max2;
            ArrayOperations.MinMax<TY>( ( IEnumerable<TY> ) yColumn, out min2, out max2 );
            double num2 = !max2.Equals((object) min2) ? 299.0 / math2.Subtract(max2, min2).ToDouble() : 0.0;
            Point2DSeries point2Dseries = new Point2DSeries(100);
            TX[] uncheckedList1 = xColumn.ToUncheckedList<TX>();
            TY[] uncheckedList2 = yColumn.ToUncheckedList<TY>();
            for ( int index1 = 0 ; index1 < xColumn.Count ; ++index1 )
            {
                TX x = uncheckedList1[index1];
                TY y = uncheckedList2[index1];
                int index2 = (int) (num1 * math1.Subtract(x, min1).ToDouble()) + (int) (num2 * math2.Subtract(y, min2).ToDouble()) * 400;
                if ( PointResamplerBase._bins[ index2 ] == ( byte ) 0 )
                {
                    PointResamplerBase._bins[ index2 ] = ( byte ) 1;
                    point2Dseries.Add( new Point2D( x.ToDouble(), y.ToDouble() ) );
                }
            }
            point2Dseries.Freeze();
            return ( IPointSeries ) point2Dseries;
        }
    }
}
