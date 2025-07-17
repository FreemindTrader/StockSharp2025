// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.Heatmap2DArrayDataSeries`3
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
namespace Ecng.Xaml.Charting
{
    [Obfuscation( ApplyToMembers = true, Exclude = true )]
    public sealed class Heatmap2DArrayDataSeries<TX, TY, TZ> : IDataSeries<TX, TY>, IDataSeries, ISuspendable, IHeatmap2DArrayDataSeriesInternal, IHeatmap2DArrayDataSeries where TX : IComparable where TY : IComparable where TZ : IComparable
    {
        private readonly object _syncRoot = new object();
        private readonly Func<int, TX> _xMapping;
        private readonly Func<int, TY> _yMapping;
        private readonly TZ[,] _array2D;
        private double[,] _cachedArray2D;
        private int[,] _cachedArgbColorArray2D;
        private DoubleToColorMappingSettings _cachedMappingSettings;
        private ISciChartSurface _parentSurface;

        public Heatmap2DArrayDataSeries( TZ[ , ] array2D, Func<int, TX> xMapping, Func<int, TY> yMapping )
        {
            if ( array2D == null )
                throw new ArgumentNullException();
            this.AcceptsUnsortedData = false;
            this._array2D = array2D;
            this._xMapping = xMapping;
            this._yMapping = yMapping;
        }

        event EventHandler<DataSeriesChangedEventArgs> IDataSeries.DataSeriesChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public object SyncRoot
        {
            get
            {
                return this._syncRoot;
            }
        }

        public bool AcceptsUnsortedData
        {
            get; set;
        }

        public bool IsEvenlySpaced
        {
            get
            {
                return true;
            }
        }

        ISciChartSurface IDataSeries.ParentSurface
        {
            get
            {
                return this._parentSurface;
            }
            set
            {
                this._parentSurface = value;
            }
        }

        public string SeriesName
        {
            get; set;
        }

        IRange IDataSeries.XRange
        {
            get
            {
                return ( IRange ) new DoubleRange( this._xMapping( 0 ).ToDouble(), this._xMapping( this.ArrayWidth - 1 ).ToDouble() );
            }
        }

        IComparable IDataSeries.YMin
        {
            get
            {
                return ( IComparable ) this._yMapping( 0 );
            }
        }

        IComparable IDataSeries.YMinPositive
        {
            get
            {
                return ( IComparable ) this._yMapping( 0 );
            }
        }

        IComparable IDataSeries.YMax
        {
            get
            {
                return ( IComparable ) this._yMapping( this.ArrayHeight - 1 );
            }
        }

        IComparable IDataSeries.XMin
        {
            get
            {
                return ( IComparable ) this._xMapping( 0 );
            }
        }

        IComparable IDataSeries.XMax
        {
            get
            {
                return ( IComparable ) this._xMapping( this.ArrayWidth - 1 );
            }
        }

        bool IDataSeries.IsFifo
        {
            get
            {
                return false;
            }
        }

        bool IDataSeries.HasValues
        {
            get
            {
                if ( this.ArrayHeight != 0 )
                    return ( uint ) this.ArrayWidth > 0U;
                return false;
            }
        }

        int IDataSeries.Count
        {
            get
            {
                return this.ArrayWidth;
            }
        }

        bool IDataSeries.IsSorted
        {
            get
            {
                return true;
            }
        }

        public Type XType
        {
            get
            {
                return typeof( TX );
            }
        }

        public Type YType
        {
            get
            {
                return typeof( TY );
            }
        }

        private int ArrayWidth
        {
            get
            {
                return this._array2D.GetLength( 1 );
            }
        }

        private int ArrayHeight
        {
            get
            {
                return this._array2D.GetLength( 0 );
            }
        }

        int IHeatmap2DArrayDataSeries.ArrayWidth
        {
            get
            {
                return this.ArrayWidth;
            }
        }

        int IHeatmap2DArrayDataSeries.ArrayHeight
        {
            get
            {
                return this.ArrayHeight;
            }
        }

        IList IDataSeries.XValues
        {
            get
            {
                return ( IList ) ( ( IDataSeries<TX, TY> ) this ).XValues;
            }
        }

        IList IDataSeries.YValues
        {
            get
            {
                return ( IList ) ( ( IDataSeries<TX, TY> ) this ).YValues;
            }
        }

        public IComparable LatestYValue
        {
            get
            {
                return ( IComparable ) null;
            }
        }

        IList<TX> IDataSeries<TX, TY>.XValues
        {
            get
            {
                TX[] xArray = new TX[this.ArrayWidth];
                for ( int index = 0 ; index < xArray.Length ; ++index )
                    xArray[ index ] = this._xMapping( index );
                return ( IList<TX> ) xArray;
            }
        }

        IList<TY> IDataSeries<TX, TY>.YValues
        {
            get
            {
                TY[] yArray = new TY[this.ArrayHeight];
                for ( int index = 0 ; index < yArray.Length ; ++index )
                    yArray[ index ] = this._yMapping( index );
                return ( IList<TY> ) yArray;
            }
        }

        int[ , ] IHeatmap2DArrayDataSeriesInternal.GetArgbColorArray2D( DoubleToColorMappingSettings mappingSettings )
        {
            if ( this._cachedArgbColorArray2D == null || !mappingSettings.Equals( ( object ) this._cachedMappingSettings ) )
            {
                int arrayHeight = this.ArrayHeight;
                int arrayWidth = this.ArrayWidth;
                this._cachedArgbColorArray2D = new int[ arrayHeight, arrayWidth ];
                for ( int index1 = 0 ; index1 < arrayHeight ; ++index1 )
                {
                    for ( int index2 = 0 ; index2 < arrayWidth ; ++index2 )
                        this._cachedArgbColorArray2D[ index1, index2 ] = Heatmap2DArrayDataSeries<TX, TY, TZ>.DoubleToArgbColor( this._array2D[ index1, index2 ].ToDouble(), mappingSettings );
                }
                this._cachedMappingSettings = mappingSettings;
            }
            return this._cachedArgbColorArray2D;
        }

        private static int DoubleToArgbColor( double x, DoubleToColorMappingSettings mappingSettings )
        {
            x -= mappingSettings.Minimum;
            x *= mappingSettings.ScaleFactor;
            if ( mappingSettings.CachedMap == null )
            {
                mappingSettings.CachedMap = new int[ 1000 ];
                for ( int index1 = 0 ; index1 < mappingSettings.CachedMap.Length ; ++index1 )
                {
                    double num1 = (double) index1 / (double) mappingSettings.CachedMap.Length;
                    GradientStop[] gradientStops = mappingSettings.GradientStops;
                    int num2;
                    if ( gradientStops.Length > 1 )
                    {
                        GradientStop gradientStop1 = gradientStops[0];
                        for ( int index2 = 1 ; index2 < gradientStops.Length ; ++index2 )
                        {
                            GradientStop gradientStop2 = gradientStops[index2];
                            if ( num1 < gradientStop2.Offset )
                            {
                                double offset1 = gradientStop1.Offset;
                                Color color1 = gradientStop1.Color;
                                double offset2 = gradientStop2.Offset;
                                Color color2 = gradientStop2.Color;
                                num2 = Heatmap2DArrayDataSeries<TX, TY, TZ>.DoubleToArgbColor( ( num1 - offset1 ) / ( offset2 - offset1 ), ( int ) color1.A, ( int ) color1.R, ( int ) color1.G, ( int ) color1.B, ( int ) color2.A, ( int ) color2.R, ( int ) color2.G, ( int ) color2.B );
                                goto label_9;
                            }
                            else
                                gradientStop1 = gradientStop2;
                        }
                    }
                    Color color = mappingSettings.GradientStops[mappingSettings.GradientStops.Length - 1].Color;
                    num2 = -16777216 | ( int ) color.R << 16 | ( int ) color.G << 8 | ( int ) color.B;
                label_9:
                    mappingSettings.CachedMap[ index1 ] = num2;
                }
            }
            int index = (int) (x * (double) mappingSettings.CachedMap.Length);
            if ( index < 0 )
                index = 0;
            else if ( index >= mappingSettings.CachedMap.Length )
                index = mappingSettings.CachedMap.Length - 1;
            return mappingSettings.CachedMap[ index ];
        }

        private static int DoubleToArgbColor( double x, int a1, int r1, int g1, int b1, int a2, int r2, int g2, int b2 )
        {
            if ( x > 1.0 )
                x = 1.0;
            if ( x < 0.0 )
                x = 0.0;
            return a1 + ( int ) ( ( double ) ( a2 - a1 ) * x ) << 24 | r1 + ( int ) ( ( double ) ( r2 - r1 ) * x ) << 16 | g1 + ( int ) ( ( double ) ( g2 - g1 ) * x ) << 8 | b1 + ( int ) ( ( double ) ( b2 - b1 ) * x );
        }

        double[ , ] IHeatmap2DArrayDataSeries.GetArray2D()
        {
            if ( this._cachedArray2D == null )
            {
                int length1 = this._array2D.GetLength(0);
                int length2 = this._array2D.GetLength(1);
                this._cachedArray2D = new double[ length1, length2 ];
                for ( int index1 = 0 ; index1 < length1 ; ++index1 )
                {
                    for ( int index2 = 0 ; index2 < length2 ; ++index2 )
                        this._cachedArray2D[ index1, index2 ] = this._array2D[ index1, index2 ].ToDouble();
                }
            }
            return this._cachedArray2D;
        }

        [Obsolete( "IsAttached is obsolete because there is no DataSeriesSet now" )]
        bool IDataSeries.IsAttached
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IRange IDataSeries.YRange
        {
            get
            {
                return ( IRange ) new DoubleRange( this._yMapping( 0 ).ToDouble(), this._yMapping( this.ArrayHeight - 1 ).ToDouble() );
            }
        }

        DataSeriesType IDataSeries.DataSeriesType
        {
            get
            {
                return DataSeriesType.Heatmap;
            }
        }

        IComparable IDataSeries.XMinPositive
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int? IDataSeries.FifoCapacity
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int FindClosestLine( int start, int count, IComparable x, IComparable y, double xyScaleRatio, double maxXDistance )
        {
            throw new NotImplementedException();
        }

        public int FindClosestPoint( int start, int count, IComparable x, IComparable y, double xyScaleRatio, double maxXDistance )
        {
            throw new NotImplementedException();
        }

        void IDataSeries<TX, TY>.Append( TX x, params TY[ ] yValues )
        {
            throw new NotImplementedException();
        }

        void IDataSeries<TX, TY>.Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues )
        {
            throw new NotImplementedException();
        }

        void IDataSeries<TX, TY>.Remove( TX x )
        {
            throw new NotImplementedException();
        }

        void IDataSeries<TX, TY>.RemoveAt( int index )
        {
            throw new NotImplementedException();
        }

        void IDataSeries<TX, TY>.RemoveRange( int startIndex, int count )
        {
            throw new NotImplementedException();
        }

        void IDataSeries.Clear()
        {
            throw new NotImplementedException();
        }

        IDataSeries<TX, TY> IDataSeries<TX, TY>.Clone()
        {
            throw new NotImplementedException();
        }

        TY IDataSeries<TX, TY>.GetYMinAt( int index, TY existingYMin )
        {
            throw new NotImplementedException();
        }

        TY IDataSeries<TX, TY>.GetYMaxAt( int index, TY existingYMax )
        {
            throw new NotImplementedException();
        }

        IPointSeries IDataSeries.ToPointSeries( IList column, ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis )
        {
            throw new NotImplementedException();
        }

        int IDataSeries.FindIndex( IComparable x, SearchMode searchMode )
        {
            throw new NotImplementedException();
        }

        void IDataSeries.InvalidateParentSurface( RangeMode rangeMode )
        {
            throw new NotImplementedException();
        }

        public IDataSeries ToStackedDataSeriesComponent( IEnumerable<IDataSeries> previousDataSeriesInSameGroup )
        {
            throw new NotImplementedException();
        }

        public int FindClosestPoint( IComparable x, IComparable y, double xyScaleRatio, double maxXDistance )
        {
            throw new NotImplementedException();
        }

        public int FindClosestLine( IComparable x, IComparable y, double xyScaleRatio, double maxXDistance, LineDrawMode drawNanAs )
        {
            throw new NotImplementedException();
        }

        public void OnBeginRenderPass()
        {
        }

        bool ISuspendable.IsSuspended
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IUpdateSuspender ISuspendable.SuspendUpdates()
        {
            throw new NotImplementedException();
        }

        void ISuspendable.ResumeUpdates( IUpdateSuspender suspender )
        {
            throw new NotImplementedException();
        }

        void ISuspendable.DecrementSuspend()
        {
            throw new NotImplementedException();
        }

        IndexRange IDataSeries.GetIndicesRange( IRange visibleRange )
        {
            return new IndexRange( 0, this.ArrayWidth - 1 );
        }

        IPointSeries IDataSeries.ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
        {
            return ( IPointSeries ) new Array2DPointSeries<TX, TY>( ( IHeatmap2DArrayDataSeries ) this, this._xMapping, this._yMapping );
        }

        IRange IDataSeries.GetWindowedYRange( IndexRange xIndexRange )
        {
            return this.GetYRange();
        }

        private IRange GetYRange()
        {
            return ( IRange ) new DoubleRange( this._yMapping( 0 ).ToDouble(), this._yMapping( this.ArrayHeight ).ToDouble() );
        }

        IRange IDataSeries.GetWindowedYRange( IndexRange xIndexRange, bool getPositiveRange )
        {
            return this.GetYRange();
        }

        IRange IDataSeries.GetWindowedYRange( IRange xRange, bool getPositiveRange )
        {
            return this.GetYRange();
        }

        IRange IDataSeries.GetWindowedYRange( IRange xRange )
        {
            return this.GetYRange();
        }

        HitTestInfo IDataSeries.ToHitTestInfo( int index )
        {
            return this.GetHitTestInfo( new int?( index ), new int?( index ) );
        }

        private HitTestInfo GetHitTestInfo( int? xIndex, int? yIndex )
        {
            lock ( this.SyncRoot )
            {
                bool flag = xIndex.HasValue && yIndex.HasValue;
                IComparable comparable = (IComparable) null;
                if ( flag )
                    comparable = ( IComparable ) this._array2D[ yIndex.Value, xIndex.Value ];
                HitTestInfo hitTestInfo = new HitTestInfo();
                hitTestInfo.DataSeriesName = this.SeriesName;
                hitTestInfo.DataSeriesType = DataSeriesType.Heatmap;
                hitTestInfo.ZValue = comparable;
                hitTestInfo.IsHit = flag;
                hitTestInfo.IsWithinDataBounds = flag;
                hitTestInfo.IsVerticalHit = flag;
                hitTestInfo = hitTestInfo;
                return hitTestInfo;
            }
        }

        public HitTestInfo ToHitTestInfo( double xValue, double yValue, bool interpolateXy = true )
        {
            int? index1 = this.GetIndex<TX>(this._xMapping, xValue, this.ArrayWidth);
            int? index2 = this.GetIndex<TY>(this._yMapping, yValue, this.ArrayHeight);
            HitTestInfo hitTestInfo = this.GetHitTestInfo(index1, index2);
            if ( interpolateXy )
            {
                hitTestInfo.XValue = ( IComparable ) xValue;
                hitTestInfo.YValue = ( IComparable ) yValue;
            }
            else if ( hitTestInfo.IsHit )
            {
                hitTestInfo.XValue = ( IComparable ) this._xMapping( index1.GetValueOrDefault( -1 ) );
                hitTestInfo.YValue = ( IComparable ) this._yMapping( index2.GetValueOrDefault( -1 ) );
            }
            return hitTestInfo;
        }

        private int? GetIndex<T>( Func<int, T> mapping, double value, int dimension ) where T : IComparable
        {
            for ( int index = 0 ; index < dimension ; ++index )
            {
                if ( mapping( index + 1 ).ToDouble() >= value && mapping( index ).ToDouble() < value )
                    return new int?( index );
            }
            return new int?();
        }
    }
}
