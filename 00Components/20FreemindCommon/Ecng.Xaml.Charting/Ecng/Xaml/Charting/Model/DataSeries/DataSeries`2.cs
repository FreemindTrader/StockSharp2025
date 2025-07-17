// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.DataSeries`2
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Numerics.GenericMath;
using StockSharp.Xaml.Charting.Numerics.PointResamplers;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public abstract class DataSeries<TX, TY> : BindableObject, IDataSeries<TX, TY>, IDataSeries, ISuspendable where TX : IComparable where TY : IComparable
    {
        public static readonly IMath<TY> YMath = GenericMathFactory.New<TY>();
        public static readonly IMath<TX> XMath = GenericMathFactory.New<TX>();
        protected ISeriesColumn<TX> _xColumn = (ISeriesColumn<TX>) new SeriesColumn<TX>();
        protected ISeriesColumn<TY> _yColumn = (ISeriesColumn<TY>) new SeriesColumn<TY>();
        private readonly object _syncRoot = new object();
        [Obsolete("XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true)]
        protected TY _yMin;
        [Obsolete("XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true)]
        protected TY _yMinPositive;
        [Obsolete("XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true)]
        protected TY _yMax;
        [Obsolete("XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true)]
        protected TX _xMin;
        [Obsolete("XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true)]
        protected TX _xMinPositive;
        [Obsolete("XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true)]
        protected TX _xMax;
        private int? _fifoCapacity;
        private string _seriesName;
        private StockSharp.Xaml.Charting.Common.WeakReference<ISciChartSurface> _parentSurface;

        public event EventHandler<DataSeriesChangedEventArgs> DataSeriesChanged;

        public object SyncRoot
        {
            get
            {
                return this._syncRoot;
            }
        }

        public IDataDistributionCalculator<TX> DataDistributionCalculator
        {
            get; set;
        }

        protected DataSeries()
        {
            this.Clear();
        }

        public ISciChartSurface ParentSurface
        {
            get
            {
                if ( this._parentSurface == null )
                {
                    return ( ISciChartSurface ) null;
                }

                return this._parentSurface.Target;
            }
            set
            {
                this._parentSurface = null;
                if ( value == null )
                {
                    return;
                }

                this._parentSurface = new StockSharp.Xaml.Charting.Common.WeakReference<ISciChartSurface>( value );
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

        public virtual IRange XRange
        {
            get
            {
                lock ( this.SyncRoot )
                {
                    TX min = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.MaxValue;
                    TX max = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.MinValue;
                    if ( this.DataDistributionCalculator.DataIsSortedAscending )
                    {
                        IList<TX> xColumn = (IList<TX>) this._xColumn;
                        if ( xColumn.Count > 0 )
                        {
                            min = xColumn[ 0 ];
                            max = xColumn[ xColumn.Count - 1 ];
                        }
                    }
                    else
                    {
                        ArrayOperations.MinMax<TX>( ( IEnumerable<TX> ) this._xColumn, out min, out max );
                    }

                    return RangeFactory.NewRange( ( IComparable ) min, ( IComparable ) max );
                }
            }
        }

        public virtual IRange YRange
        {
            get
            {
                lock ( this.SyncRoot )
                {
                    TY min;
                    TY max;
                    ArrayOperations.MinMax<TY>( ( IEnumerable<TY> ) this._yColumn, out min, out max );
                    return RangeFactory.NewRange( ( IComparable ) min, ( IComparable ) max );
                }
            }
        }

        public IComparable YMin
        {
            get
            {
                return this.YRange.Min;
            }
        }

        public IComparable LatestYValue
        {
            get
            {
                if ( this.YValues.Count != 0 )
                {
                    return ( IComparable ) this.YValues[ this.YValues.Count - 1 ];
                }

                return ( IComparable ) null;
            }
        }

        [Obsolete( "XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true )]
        public IComparable YMinPositive
        {
            get
            {
                return ( IComparable ) this._yMinPositive;
            }
        }

        public IComparable YMax
        {
            get
            {
                return this.YRange.Max;
            }
        }

        public IComparable XMin
        {
            get
            {
                return this.XRange.Min;
            }
        }

        [Obsolete( "XRange, YRange, XMin, YMin are all obsolete for performance reasons, Sorry!", true )]
        public IComparable XMinPositive
        {
            get
            {
                return ( IComparable ) this._xMinPositive;
            }
        }

        public IComparable XMax
        {
            get
            {
                return this.XRange.Max;
            }
        }

        public int Count
        {
            get
            {
                return ( ( ISeriesColumn ) this._yColumn ).Count;
            }
        }

        public bool AcceptsUnsortedData
        {
            get; set;
        }

        public string SeriesName
        {
            get
            {
                return this._seriesName;
            }
            set
            {
                this._seriesName = value;
                this.OnPropertyChanged( nameof( SeriesName ) );
                this.OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            }
        }

        [Obsolete( "IsAttached is obsolete because there is no DataSeries now" )]
        public bool IsAttached
        {
            get
            {
                return false;
            }
        }

        public abstract DataSeriesType DataSeriesType
        {
            get;
        }

        public TY this[ DataSeriesColumn column, int index ]
        {
            get
            {
                if ( ( uint ) column <= 1u )
                {
                    return ( ( IList<TY> ) _yColumn )[ index ];
                }
                throw new InvalidOperationException( $"The enumeration value {column} has not been catered" );
            }
        }

        public abstract bool HasValues
        {
            get;
        }

        public IList<TX> XValues
        {
            get
            {
                return ( IList<TX> ) this._xColumn;
            }
        }

        public IList<TY> YValues
        {
            get
            {
                return ( IList<TY> ) this._yColumn;
            }
        }

        IList IDataSeries.XValues
        {
            get
            {
                return ( IList ) this._xColumn;
            }
        }

        IList IDataSeries.YValues
        {
            get
            {
                return ( IList ) this._yColumn;
            }
        }

        public bool IsFifo
        {
            get
            {
                return this._fifoCapacity.HasValue;
            }
        }

        public bool IsSorted
        {
            get
            {
                return this.DataDistributionCalculator.DataIsSortedAscending;
            }
        }

        public int? FifoCapacity
        {
            get
            {
                return this._fifoCapacity;
            }
            set
            {
                this._fifoCapacity = value;
                this.Clear();
            }
        }

        public abstract void Append( TX x, params TY[ ] yValues );

        public abstract void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues );

        public void Remove( TX x )
        {
            int index = ((IList) this.XValues).FindIndex(this.IsSorted, (IComparable) x, SearchMode.Exact);
            if ( index == -1 )
            {
                return;
            }

            this.RemoveAt( index );
        }

        public abstract void RemoveAt( int index );

        public abstract void RemoveRange( int startIndex, int count );

        public void Clear()
        {
            lock ( this.SyncRoot )
            {
                this.ClearColumns();
                this.OnDataSeriesChanged( DataSeriesUpdate.DataChanged | DataSeriesUpdate.DataSeriesCleared );
                this.DataDistributionCalculator = DataDistributionCalculatorFactory.Create<TX>( this.IsFifo );
                this.DataDistributionCalculator.Clear();
            }
        }

        public abstract IDataSeries<TX, TY> Clone();

        public IndexRange GetIndicesRange( IRange range )
        {
            return this.GetIndicesRange( range, SearchMode.RoundDown, SearchMode.RoundUp );
        }

        private IndexRange GetIndicesRange( IRange range, SearchMode downSearchMode, SearchMode upSearchMode )
        {
            IndexRange indexRange = new IndexRange(0, -1);
            if ( ( ( ICollection ) this._xColumn ).Count > 0 )
            {
                indexRange = this.ToIndicesRange( range.Clone() as IndexRange ?? this.SearchDataIndexesOn( range, downSearchMode, upSearchMode ) );
            }

            return indexRange;
        }

        private IndexRange SearchDataIndexesOn( IRange range, SearchMode downSearchMode, SearchMode upSearchMode )
        {
            int count = ((ICollection)_xColumn).Count;
            IndexRange indexRange = new IndexRange(-1, -1);
            if ( !IsSorted )
            {
                indexRange.Min = 0;
                indexRange.Max = count - 1;
            }
            else
            {
                Type typeFromHandle = typeof(TX);
                IComparable comparable = range.Min;
                IComparable comparable2 = range.Max;
                if ( NumberUtil.IsIntegerType( typeFromHandle ) )
                {
                    comparable = Math.Floor( comparable.ToDouble() );
                    comparable2 = Math.Ceiling( comparable2.ToDouble() );
                }
                TX val = (TX)Convert.ChangeType(comparable, typeFromHandle, CultureInfo.InvariantCulture);
                TX val2 = (TX)Convert.ChangeType(comparable2, typeFromHandle, CultureInfo.InvariantCulture);
                TX val3 = ((IList<TX>)_xColumn)[0];
                if ( ( ( IList<TX> ) _xColumn )[ count - 1 ].CompareTo( val ) >= 0 && val3.CompareTo( val2 ) <= 0 )
                {
                    indexRange.Min = ( ( IList ) _xColumn ).FindIndex( true, ( IComparable ) ( object ) val, downSearchMode );
                    indexRange.Max = ( ( IList ) _xColumn ).FindIndex( true, ( IComparable ) ( object ) val2, upSearchMode );
                }
            }
            return indexRange;
        }

        private IndexRange ToIndicesRange( IndexRange indexRange )
        {
            int count = ((ICollection) this._xColumn).Count;
            if ( indexRange.IsDefined )
            {
                indexRange.Min = Math.Max( indexRange.Min, 0 );
                indexRange.Max = Math.Min( indexRange.Max, count - 1 );
            }
            if ( indexRange.Min.CompareTo( indexRange.Max ) > 0 )
            {
                indexRange.Min = 0;
            }

            UltrachartDebugLogger.Instance.WriteLine( "GetIndicesRange: Min={0}, Max={1}", ( object ) indexRange.Min, ( object ) indexRange.Max );
            return indexRange;
        }

        public abstract IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null );

        [Obsolete( "ToPointSeries overload has been deprecated, use ToPointSeries instead, and cast to correct type of point series", true )]
        public IPointSeries ToPointSeries( IList column, ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis )
        {
            throw new NotImplementedException();
        }

        public IRange GetWindowedYRange( IRange xRange )
        {
            return this.GetWindowedYRange( xRange, false );
        }

        public IRange GetWindowedYRange( IRange xRange, bool getPositiveRange )
        {
            if ( xRange == null )
            {
                throw new ArgumentNullException( nameof( xRange ) );
            }

            IndexRange indicesRange = this.GetIndicesRange(xRange, SearchMode.Nearest, SearchMode.Nearest);
            return indicesRange.IsDefined ? this.GetWindowedYRange( indicesRange, getPositiveRange ) : ( IRange ) this.NewRange( ( IComparable ) StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.MinValue, ( IComparable ) StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.MaxValue );
        }

        public IRange GetWindowedYRange( IndexRange xIndexRange )
        {
            return this.GetWindowedYRange( xIndexRange, false );
        }

        public IRange GetWindowedYRange( IndexRange xIndexRange, bool getPositiveRange )
        {
            lock ( this.SyncRoot )
            {
                TY existingYMax = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.MinValue;
                TY y = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.MaxValue;
                int num1 = Math.Max(xIndexRange.Min, 0);
                int num2 = Math.Min(xIndexRange.Max, this.Count - 1);
                for ( int index = num1 ; index <= num2 ; ++index )
                {
                    TY yminAt = this.GetYMinAt(index, y);
                    y = getPositiveRange ? this.GetPositiveMin<TY>( y, yminAt ) : yminAt;
                    existingYMax = this.GetYMaxAt( index, existingYMax );
                }
                return ( IRange ) this.NewRange( ( IComparable ) y, ( IComparable ) existingYMax );
            }
        }

        public int FindIndex( IComparable x, SearchMode searchMode = SearchMode.Exact )
        {
            Guard.NotNull( ( object ) x, nameof( x ) );
            if ( x.GetType() != typeof( TX ) )
            {
                if ( !( x.GetType() == typeof( double ) ) )
                {
                    throw new InvalidOperationException( string.Format( "The X-value type {0} does not match the DataSeries X-Type", ( object ) x.GetType() ) );
                }

                double rhs = (double) x;
                TX lhs = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ZeroValue;
                lhs = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Inc( ref lhs );
                x = ( IComparable ) StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Mult( lhs, rhs );
            }
            return this.XValues.FindIndex<TX>( this.IsSorted, x, searchMode );
        }

        public virtual HitTestInfo ToHitTestInfo( int index )
        {
            lock ( this.SyncRoot )
            {
                if ( index < 0 || index >= this.Count )
                {
                    return HitTestInfo.Empty;
                }

                TX xvalue = this.XValues[index];
                TY yvalue = this.YValues[index];
                return new HitTestInfo() { DataSeriesName = this.SeriesName, DataSeriesType = this.DataSeriesType, XValue = ( IComparable ) xvalue, YValue = ( IComparable ) yvalue, DataSeriesIndex = index };
            }
        }

        public void InvalidateParentSurface( RangeMode rangeMode )
        {
            if ( this.ParentSurface == null )
            {
                return;
            }

            switch ( rangeMode )
            {
                case RangeMode.None:
                    this.ParentSurface.InvalidateElement();
                    break;
                case RangeMode.ZoomToFit:
                    this.ParentSurface.ZoomExtents();
                    break;
                case RangeMode.ZoomToFitY:
                    this.ParentSurface.ZoomExtentsY();
                    break;
            }
        }

        protected T GetPositiveMin<T>( T positiveMin, T min ) where T : IComparable
        {
            if ( typeof( T ) == typeof( DateTime ) || typeof( T ) == typeof( TimeSpan ) )
            {
                return positiveMin;
            }

            IComparable comparable = typeof (T) == typeof (TX) ? (IComparable) StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ZeroValue : (IComparable) StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.ZeroValue;
            if ( min.CompareTo( ( object ) comparable ) <= 0 )
            {
                return positiveMin;
            }

            return min;
        }

        protected abstract void ClearColumns();

        public abstract TY GetYMinAt( int index, TY existingYMin );

        public abstract TY GetYMaxAt( int index, TY existingYMax );

        public TX GetXMinAt( int index, TX existingXMin )
        {
            TX xvalue = this.XValues[index];
            if ( !xvalue.IsDefined() )
            {
                return existingXMin;
            }

            return StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Min( existingXMin, xvalue );
        }

        internal TY ComputeMin( TY currentMin, TY inputMin, ISeriesColumn<TY> seriesColumn )
        {
            if ( this.IsFifo )
            {
                return seriesColumn.GetMinimum();
            }

            return StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.Min( currentMin, inputMin );
        }

        internal TY ComputeMax( TY currentMax, TY inputMax, ISeriesColumn<TY> seriesColumn )
        {
            if ( this.IsFifo )
            {
                return seriesColumn.GetMaximum();
            }

            return StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.Max( currentMax, inputMax );
        }

        internal TX ComputeMin( TX currentMin, TX inputMin, ISeriesColumn<TX> seriesColumn )
        {
            if ( this.IsFifo )
            {
                return seriesColumn.GetMinimum();
            }

            return StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Min( currentMin, inputMin );
        }

        internal TX ComputeMax( TX currentMax, TX inputMax, ISeriesColumn<TX> seriesColumn )
        {
            if ( this.IsFifo )
            {
                return seriesColumn.GetMaximum();
            }

            return StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Max( currentMax, inputMax );
        }

        internal void OnDataSeriesChanged( DataSeriesUpdate dataSeriesUpdate )
        {
            this.OnPropertyChanged( "LatestYValue" );
            // ISSUE: reference to a compiler-generated field
            EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
            if ( dataSeriesChanged == null )
            {
                return;
            }

            dataSeriesChanged( ( object ) this, new DataSeriesChangedEventArgs( dataSeriesUpdate ) );
        }

        private DoubleRange NewRange( IComparable min, IComparable max )
        {
            if ( typeof( TY ) == typeof( DateTime ) )
            {
                return new DoubleRange( ( double ) ( ( DateTime ) min ).Ticks, ( double ) ( ( DateTime ) max ).Ticks );
            }

            if ( !min.IsDefined() || !max.IsDefined() )
            {
                return new DoubleRange( double.MinValue, double.MaxValue );
            }

            return new DoubleRange( min.ToDouble(), max.ToDouble() );
        }

        protected void ThrowWhenAppendInvalid( int paramsCount )
        {
            throw new InvalidOperationException( string.Format( "Append(TX x, params TY[] yValues) in type {0} must receive only {1} list(s) of y values.", ( object ) this.GetType().Name, ( object ) paramsCount ) );
        }

        public bool IsSuspended
        {
            get
            {
                return UpdateSuspender.GetIsSuspended( ( ISuspendable ) this );
            }
        }

        public IUpdateSuspender SuspendUpdates()
        {
            ISciChartSurface parentSurface = this.ParentSurface;
            if ( parentSurface == null )
            {
                return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this );
            }

            System.Threading.Monitor.Enter( parentSurface.SyncRoot );
            return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this, parentSurface.SyncRoot );
        }

        public void ResumeUpdates( IUpdateSuspender suspender )
        {
            if ( suspender.ResumeTargetOnDispose )
            {
                this.OnDataSeriesChanged( DataSeriesUpdate.DataChanged | DataSeriesUpdate.DataSeriesCleared );
            }

            if ( suspender.Tag == null )
            {
                return;
            }

            System.Threading.Monitor.Exit( suspender.Tag );
        }

        public void DecrementSuspend()
        {
        }

        public virtual int FindClosestPoint( IComparable xValue, IComparable yValue, double xyScaleRatio, double hitTestRadius )
        {
            int num1 = -1;
            if ( xValue == null || yValue == null )
            {
                return num1;
            }

            lock ( this._syncRoot )
            {
                int count = this.XValues.Count;
                int num2 = 0;
                int num3 = count - 1;
                TX[] uncheckedList1 = this.XValues.ToUncheckedList<TX>();
                TY[] uncheckedList2 = this.YValues.ToUncheckedList<TY>();
                TX x1 = (TX) xValue;
                double num4 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ToDouble(x1);
                if ( this.IsSorted )
                {
                    if ( xyScaleRatio.CompareTo( 0.0 ) == 0 )
                    {
                        num1 = uncheckedList1.FindIndexInSortedData<TX>( count, x1, SearchMode.Nearest, StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath );
                    }
                    else
                    {
                        TX zeroValue = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ZeroValue;
                        TX lhs = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Inc(ref zeroValue);
                        if ( typeof( TX ) == typeof( DateTime ) )
                        {
                            double num5 = hitTestRadius;
                            DateTime maxValue = DateTime.MaxValue;
                            double ticks = (double) maxValue.Ticks;
                            if ( num5 >= ticks )
                            {
                                maxValue = DateTime.MaxValue;
                                hitTestRadius = ( double ) ( maxValue.Ticks - 1L );
                            }
                        }
                        TX x2 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Mult(lhs, hitTestRadius);
                        TX x3 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Subtract(x1, x2);
                        TX x4 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Add(x1, x2);
                        int indexInSortedData1 = uncheckedList1.FindIndexInSortedData<TX>(count, x3, SearchMode.RoundDown, StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath);
                        int indexInSortedData2 = uncheckedList1.FindIndexInSortedData<TX>(count, x4, SearchMode.RoundUp, StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath);
                        if ( indexInSortedData1 >= 0 )
                        {
                            num2 = indexInSortedData1;
                        }

                        if ( indexInSortedData2 >= 0 )
                        {
                            num3 = indexInSortedData2;
                        }

                        num3 -= num2;
                    }
                }
                if ( num1 == -1 )
                {
                    double num5 = double.MaxValue;
                    int num6 = num2 + num3 + 1;
                    for ( int index = num2 ; index < num6 ; ++index )
                    {
                        double num7 = Math.Abs(StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ToDouble(uncheckedList1[index]) - num4);
                        if ( xyScaleRatio.CompareTo( 0.0 ) != 0 )
                        {
                            double num8 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.ToDouble(StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.Subtract(uncheckedList2[index], (TY) yValue));
                            num7 += Math.Abs( num8 ) * xyScaleRatio;
                        }
                        if ( num7 < num5 )
                        {
                            num5 = num7;
                            num1 = index;
                        }
                    }
                }
                return num1;
            }
        }

        public virtual int FindClosestLine( IComparable x, IComparable y, double xyScaleRatio, double xRadius, LineDrawMode drawNanAs )
        {
            int num1 = this.Count;
            int num2 = -1;
            if ( num1 < 2 || x == null || y == null )
            {
                return num2;
            }

            lock ( this._syncRoot )
            {
                TX[] uncheckedList1 = this.XValues.ToUncheckedList<TX>();
                TY[] uncheckedList2 = this.YValues.ToUncheckedList<TY>();
                int index1 = 0;
                int count = this.XValues.Count;
                TX x1 = (TX) x;
                TY y1 = (TY) y;
                if ( this.IsSorted )
                {
                    TX x2 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ZeroValue;
                    x2 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Inc( ref x2 );
                    x2 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Mult( x2, xRadius );
                    int indexInSortedData1 = uncheckedList1.FindIndexInSortedData<TX>(count, StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Subtract(x1, x2), SearchMode.RoundDown, StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath);
                    int indexInSortedData2 = uncheckedList1.FindIndexInSortedData<TX>(count, StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.Add(x1, x2), SearchMode.RoundUp, StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath);
                    if ( drawNanAs == LineDrawMode.ClosedLines )
                    {
                        while ( indexInSortedData1 > 0 && StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.IsNaN( uncheckedList2[ indexInSortedData1 ] ) )
                        {
                            --indexInSortedData1;
                        }

                        while ( indexInSortedData2 < this.Count - 1 && StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.IsNaN( uncheckedList2[ indexInSortedData2 ] ) )
                        {
                            ++indexInSortedData2;
                        }

                        if ( indexInSortedData2 < this.Count - 1 )
                        {
                            ++indexInSortedData2;
                        }
                    }
                    num2 = index1 = indexInSortedData1;
                    num1 = indexInSortedData2 - indexInSortedData1 + 1;
                }
                double x3 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ToDouble(x1);
                double num3 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.ToDouble(y1);
                Point pt = new Point(x3, num3 * xyScaleRatio);
                double num4 = double.MaxValue;
                double x4 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ToDouble(uncheckedList1[index1]);
                double y2 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.ToDouble(uncheckedList2[index1]) * xyScaleRatio;
                double num5 = x3 - xRadius;
                double num6 = x3 + xRadius;
                int num7 = index1;
                int num8 = index1 + num1;
                for ( int index2 = index1 + 1 ; index2 < num8 ; ++index2 )
                {
                    TY y3 = uncheckedList2[index2];
                    if ( StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.IsNaN( y3 ) )
                    {
                        if ( drawNanAs == LineDrawMode.Gaps )
                        {
                            y2 = double.NaN;
                        }
                    }
                    else
                    {
                        double x2 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.XMath.ToDouble(uncheckedList1[index2]);
                        double y4 = StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.ToDouble(y3) * xyScaleRatio;
                        if ( ( x4 >= num5 || x2 >= num5 ) && ( x4 <= num6 || x2 <= num6 ) )
                        {
                            double num9 = PointUtil.DistanceFromLine(pt, new Point(x4, y2), new Point(x2, y4), true);
                            if ( num9 < num4 )
                            {
                                num2 = num7;
                                num4 = num9;
                            }
                        }
                        x4 = x2;
                        y2 = y4;
                        num7 = index2;
                    }
                }
                return num2;
            }
        }

        public virtual void OnBeginRenderPass()
        {
        }
    }
}
