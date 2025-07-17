// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.Array2DSegment`2
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
namespace Ecng.Xaml.Charting
{
    internal class Array2DSegment<TX, TY> : I2DArraySegment, IPoint where TX : IComparable where TY : IComparable
    {
        private readonly int _xIndex;
        private readonly IHeatmap2DArrayDataSeriesInternal _dataSeries;
        private readonly Func<int, TX> _xMapping;
        private readonly Func<int, TY> _yMapping;
        private readonly int _arrayHeight;

        public Array2DSegment( IHeatmap2DArrayDataSeries dataSeries, Func<int, TX> xMapping, Func<int, TY> yMapping, int xIndex )
        {
            this._arrayHeight = dataSeries.ArrayHeight;
            this._xIndex = xIndex;
            this._xMapping = xMapping;
            this._yMapping = yMapping;
            this._dataSeries = ( IHeatmap2DArrayDataSeriesInternal ) dataSeries;
        }

        public double X
        {
            get
            {
                return this._xMapping( this._xIndex ).ToDouble();
            }
        }

        public double Y
        {
            get
            {
                return this._yMapping( this._arrayHeight ).ToDouble();
            }
        }

        public double XValueAtLeft
        {
            get
            {
                return this._xMapping( this._xIndex ).ToDouble();
            }
        }

        public double XValueAtRight
        {
            get
            {
                return this._xMapping( this._xIndex + 1 ).ToDouble();
            }
        }

        public double YValueAtBottom
        {
            get
            {
                return this._yMapping( 0 ).ToDouble();
            }
        }

        public double YValueAtTop
        {
            get
            {
                return this._yMapping( this._arrayHeight ).ToDouble();
            }
        }

        public IList<int> GetVerticalPixelsArgb( DoubleToColorMappingSettings mappingSettings )
        {
            return ( IList<int> ) new Array2DSegment<TX, TY>.VerticalPixels( this._arrayHeight, this._dataSeries.GetArgbColorArray2D( mappingSettings ), this._xIndex );
        }

        public IList<double> GetVerticalPixelValues()
        {
            return ( IList<double> ) new Array2DSegment<TX, TY>.VerticalPixelValues( this._arrayHeight, this._dataSeries.GetArray2D(), this._xIndex );
        }

        private class VerticalPixels : IList<int>, ICollection<int>, IEnumerable<int>, IEnumerable
        {
            private readonly int _count;
            private readonly int _xIndex;
            private readonly int[,] _argbColorArray2d;

            public VerticalPixels( int count, int[ , ] argbColorArray2d, int xIndex )
            {
                this._xIndex = xIndex;
                this._count = count;
                this._argbColorArray2d = argbColorArray2d;
            }

            public int IndexOf( int item )
            {
                throw new NotImplementedException();
            }

            public void Insert( int index, int item )
            {
                throw new NotImplementedException();
            }

            public void RemoveAt( int index )
            {
                throw new NotImplementedException();
            }

            public int this[ int index ]
            {
                get
                {
                    return this._argbColorArray2d[ index, this._xIndex ];
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public void Add( int item )
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains( int item )
            {
                throw new NotImplementedException();
            }

            public void CopyTo( int[ ] array, int arrayIndex )
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get
                {
                    return this._count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool Remove( int item )
            {
                throw new NotImplementedException();
            }

            public IEnumerator<int> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        private class VerticalPixelValues : IList<double>, ICollection<double>, IEnumerable<double>, IEnumerable
        {
            private readonly int _count;
            private readonly int _xIndex;
            private readonly double[,] _array2d;

            public VerticalPixelValues( int count, double[ , ] array2d, int xIndex )
            {
                this._xIndex = xIndex;
                this._count = count;
                this._array2d = array2d;
            }

            public int IndexOf( double item )
            {
                throw new NotImplementedException();
            }

            public void Insert( int index, double item )
            {
                throw new NotImplementedException();
            }

            public void RemoveAt( int index )
            {
                throw new NotImplementedException();
            }

            public double this[ int index ]
            {
                get
                {
                    return this._array2d[ index, this._xIndex ];
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public void Add( double item )
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains( double item )
            {
                throw new NotImplementedException();
            }

            public void CopyTo( double[ ] array, int arrayIndex )
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get
                {
                    return this._count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool Remove( double item )
            {
                throw new NotImplementedException();
            }

            public IEnumerator<double> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
