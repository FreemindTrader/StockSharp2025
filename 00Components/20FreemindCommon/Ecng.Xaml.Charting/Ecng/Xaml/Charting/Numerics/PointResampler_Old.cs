// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.PointResampler_Old
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
namespace Ecng.Xaml.Charting
{
    internal class PointResampler_Old : IPointResampler_Old
    {
        private readonly int _resolution;
        private ResamplingMode _resamplingMode;

        internal PointResampler_Old( int resolution, ResamplingMode resamplingMode )
        {
            Guard.Assert( ( IComparable ) resolution, nameof( resolution ) ).IsGreaterThanOrEqualTo( ( IComparable ) 2 );
            this._resolution = resolution;
            this._resamplingMode = resamplingMode;
        }

        public int Resolution
        {
            get
            {
                return this._resolution;
            }
        }

        public ResamplingMode ResamplingMode
        {
            get
            {
                return this._resamplingMode;
            }
        }

        public bool RequiresReduction( IndexRange pointIndices, int viewportWidth )
        {
            return ( uint ) this._resamplingMode > 0U & pointIndices.Max - pointIndices.Min + 1 > this._resolution * viewportWidth;
        }

        public IPointResampler_Old WithMode( ResamplingMode newMode )
        {
            this._resamplingMode = newMode;
            return ( IPointResampler_Old ) this;
        }

        public IList ReducePoints( IList inputPoints, int viewportWidth )
        {
            return this.ReducePoints( inputPoints, new IndexRange( 0, inputPoints.Count - 1 ), viewportWidth );
        }

        public IList ReducePoints( IList inputPoints, IndexRange pointIndices, int viewportWidth )
        {
            if ( !this.RequiresReduction( pointIndices, viewportWidth ) )
            {
                int length = pointIndices.Max - pointIndices.Min + 1;
                if ( length == inputPoints.Count )
                    return inputPoints;
                double[] numArray = new double[length];
                int min = pointIndices.Min;
                for ( int index = 0 ; index < length ; ++index )
                {
                    numArray[ index ] = Convert.ToDouble( inputPoints[ min ] );
                    ++min;
                }
                return ( IList ) numArray;
            }
            if ( this._resamplingMode.ToString().StartsWith( ResamplingMode.MinMax.ToString() ) )
                return this.ResampledMinMax( inputPoints, pointIndices.Min, pointIndices.Max, viewportWidth );
            if ( this._resamplingMode == ResamplingMode.Min )
                return this.ResampledMin( inputPoints, pointIndices.Min, pointIndices.Max, viewportWidth );
            if ( this._resamplingMode == ResamplingMode.Max )
                return this.ResampledMax( inputPoints, pointIndices.Min, pointIndices.Max, viewportWidth );
            if ( this._resamplingMode == ResamplingMode.Mid )
                return this.ResampledMid( inputPoints, pointIndices.Min, pointIndices.Max, viewportWidth );
            throw new Exception( string.Format( "Resampling Mode {0} has not been handled", ( object ) this._resamplingMode ) );
        }

        private IList ResampledMinMax( IList inputPoints, int startIndex, int endIndex, int viewportWidth )
        {
            int num1 = endIndex - startIndex + 1;
            int num2 = this._resolution * viewportWidth;
            int num3 = num1 / num2;
            double[] numArray1 = new double[2 * num1 / num3];
            double val1_1 = double.MaxValue;
            double val1_2 = double.MinValue;
            int index1 = startIndex;
            int num4 = 0;
            int index2 = 0;
            int num5;
            for ( num5 = endIndex - num1 % num3 ; index1 <= num5 ; ++index1 )
            {
                double val2 = ((IComparable) inputPoints[index1]).ToDouble();
                val1_1 = Math.Min( val1_1, val2 );
                val1_2 = Math.Max( val1_2, val2 );
                if ( ++num4 >= num3 )
                {
                    double[] numArray2 = numArray1;
                    int index3 = index2;
                    int num6 = index3 + 1;
                    double num7 = val1_1;
                    numArray2[ index3 ] = num7;
                    double[] numArray3 = numArray1;
                    int index4 = num6;
                    index2 = index4 + 1;
                    double num8 = val1_2;
                    numArray3[ index4 ] = num8;
                    val1_1 = double.MaxValue;
                    val1_2 = double.MinValue;
                    num4 = 0;
                }
            }
            if ( endIndex != num5 && numArray1.Length > index2 )
            {
                for ( ; index1 <= endIndex ; ++index1 )
                {
                    val1_1 = Math.Min( val1_1, ( double ) inputPoints[ index1 ] );
                    val1_2 = Math.Max( val1_2, ( double ) inputPoints[ index1 ] );
                }
                numArray1[ index2 ] = val1_1;
                numArray1[ index2 ] = val1_2;
            }
            return ( IList ) numArray1;
        }

        private IList ResampledMax( IList inputPoints, int startIndex, int endIndex, int viewportWidth )
        {
            int num1 = endIndex - startIndex + 1;
            int num2 = this._resolution * viewportWidth;
            int num3 = num1 / num2;
            double[] numArray = new double[num1 / num3];
            double num4 = double.MinValue;
            int index = startIndex;
            int num5 = 0;
            int num6 = 0;
            for ( ; index <= endIndex ; ++index )
            {
                double num7 = ((IComparable) inputPoints[index]).ToDouble();
                if ( num7 > num4 )
                    num4 = num7;
                if ( ++num5 >= num3 )
                {
                    numArray[ num6++ ] = num4;
                    num4 = double.MinValue;
                    num5 = 0;
                }
            }
            return ( IList ) numArray;
        }

        private IList ResampledMin( IList inputPoints, int startIndex, int endIndex, int viewportWidth )
        {
            int num1 = endIndex - startIndex + 1;
            int num2 = this._resolution * viewportWidth;
            int num3 = num1 / num2;
            double[] numArray = new double[num1 / num3];
            double num4 = double.MaxValue;
            int index = startIndex;
            int num5 = 0;
            int num6 = 0;
            for ( ; index <= endIndex ; ++index )
            {
                double num7 = ((IComparable) inputPoints[index]).ToDouble();
                if ( num7 < num4 )
                    num4 = num7;
                if ( ++num5 >= num3 )
                {
                    numArray[ num6++ ] = num4;
                    num4 = double.MaxValue;
                    num5 = 0;
                }
            }
            return ( IList ) numArray;
        }

        private IList ResampledMid( IList inputPoints, int startIndex, int endIndex, int viewportWidth )
        {
            int num1 = endIndex - startIndex + 1;
            int num2 = this._resolution * viewportWidth;
            int num3 = num1 / num2;
            double[] numArray = new double[num1 / num3];
            double val1_1 = double.MaxValue;
            double val1_2 = double.MinValue;
            int index = startIndex;
            int num4 = 0;
            int num5 = 0;
            for ( ; index <= endIndex ; ++index )
            {
                double val2 = ((IComparable) inputPoints[index]).ToDouble();
                val1_1 = Math.Min( val1_1, val2 );
                val1_2 = Math.Max( val1_2, val2 );
                if ( ++num4 >= num3 )
                {
                    numArray[ num5++ ] = 0.5 * ( val1_2 + val1_1 );
                    val1_1 = double.MaxValue;
                    val1_2 = double.MinValue;
                    num4 = 0;
                }
            }
            return ( IList ) numArray;
        }
    }
}
