using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting2D.Interop;
using SciChart.Core.Framework;
using SciChart.Core.Utility;
using SciChart.Data.Model;
using SciChart.Data.Numerics.GenericMath;
using SciChart.Data.Numerics.PointResamplers;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MathHelper = SciChart.Data.Numerics.GenericMath;


#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// This class is the base class for the derived 
/// 
///     1) point and figure chart
///     2) ClusterProfile chart
///     
/// TimeframeSegmentDataSeries is like the Candle of the TimeframeSegment
/// 
/// TimeframeSegmentPointSeries is like the line representation of the TimeframeSegment
/// 
/// </summary>
public class TimeframeSegmentDataSeries : BindableObject, IDataSeries<DateTime, double>, ISuspendable, IDataSeries, ITimeframe
{
    public const Decimal                           _MinPriceStep  = 0.000001M;
    public static readonly TimeSpan                _MaxTimeframe  = TimeSpan.FromDays(30.0);
    public static readonly IMath<DateTime>         _XMath         = GenericMathFactory.GetMath<DateTime>();    
    private readonly TimeframeDataSegment          _volumeProfile = new TimeframeDataSegment(new DateTime(), 0);
    private readonly SciList<TimeframeDataSegment> _segments      = new SciList<TimeframeDataSegment>();
    private readonly SciList<DateTime>             _segmentDates  = new SciList<DateTime>();
    private readonly YValueList                    _yValues;
    private readonly ChartCandleElement            _element;
    private Tuple<DateTime, DateTime, long>        _curPeriod;
    private string                                 _seriesName;


    public TimeframeSegmentDataSeries( TimeSpan? timeframe, ChartCandleElement element )
    {
        if ( timeframe.HasValue && ( timeframe.Value <= TimeSpan.Zero || timeframe.Value > _MaxTimeframe ) )
        {
            throw new ArgumentOutOfRangeException( nameof( timeframe ) );
        }

        _yValues  = new YValueList( this );
        Timeframe = timeframe;
        _element  = element ?? throw new ArgumentNullException( nameof( element ) );
    }

    private IReadOnlySciList<DateTime> SegmentDates
    {
        get => _segmentDates.AsReadOnly();
    }

    public DataSeriesType DataSeriesType
    {
        get => DataSeriesType.OneHundredPercentStackedXy;
    }

    public TimeSpan? Timeframe
    {
        get;
    }

    public event EventHandler<DataSeriesChangedEventArgs> DataSeriesChanged;

    public object SyncRoot
    {
        get;
    } = new object();

    public bool AcceptsUnsortedData
    {
        get => false;
        
        set
        {
        }
    }

    //
    // Summary:
    //     Gets or sets the parent SciChart.Charting.Visuals.ISciChartSurface which this
    //     SciChart.Charting.Model.DataSeries.IDataSeries instance is attached to.
    public ISciChartSurface ParentSurface
    {
        get;
        set;
    }

    public string SeriesName
    {
        get => _seriesName;
        
        set
        {
            _seriesName = value;
            
            DataSeriesChanged?.Invoke( this, new DataSeriesChangedEventArgs( DataSeriesUpdate.DataChanged ) );
        }
    }

    public bool HasValues => _segmentDates.Count > 0;

    //bool IDataSeries.IsSecondary
    //{
    //    get => false;
    //}

    public int Count => _segmentDates.Count;

    public IComparable YMin => YRange.Min;

    public IComparable YMax => YRange.Max;

    IComparable IDataSeries.XMin
    {
        get => XRange.Min;
    }

    IComparable IDataSeries.XMax
    {
        get => XRange.Max;
    }

    bool IDataSeries.IsFifo
    {
        get => false;
    }

    public bool IsSorted => true;

    public ISciList<TimeframeDataSegment> Segments
    {
        get
        {
            return ( ISciList<TimeframeDataSegment> ) _segments;
        }
    }

    protected IReadOnlySciList<TimeframeDataSegment> SegmentsReadOnly
    {
        get => _segments.AsReadOnly();
    }

    //
    // Summary:
    //     Gets the Type of X-data points in this DataSeries. Used to check compatibility
    //     with Axis types.
    public Type XType => typeof( DateTime );

    //
    // Summary:
    //     Gets the Type of Y-data points in this DataSeries. Used to check compatibility
    //     with Axis types.
    public Type YType => typeof( double );

    public IComparable LatestYValue => ( IComparable ) null;

    private IList<DateTime> XValues => ( IList<DateTime> ) SegmentDates;

    IList IDataSeries.XValues
    {
        get => ( IList ) XValues;
    }

    IList<DateTime> IDataSeries<DateTime, double>.XValues
    {
        get => XValues;
    }

    IList IDataSeries.YValues
    {
        get => ( IList ) _yValues;
    }

    IList<double> IDataSeries<DateTime, double>.YValues
    {
        get => ( IList<double> ) _yValues;
    }

    public void Clear()
    {
        SciChartDebugLogger.Instance.WriteLine( "ERROR: Clear(): not supported", Array.Empty<object>() );
    }

    public void InvalidateParentSurface( RangeMode rangeMode )
    {
        if ( ParentSurface == null )
            return;

        switch ( rangeMode )
        {
            case RangeMode.None:
                ParentSurface.InvalidateElement();
                break;

            case RangeMode.ZoomToFit:
                ParentSurface.ZoomExtents();
                break;

            case RangeMode.ZoomToFitY:
                ParentSurface.ZoomExtentsY();
                break;
        }
    }

    public IndexRange GetIndicesRange( IRange visibleRange )
    {
        return GetIndicesRange( visibleRange, SearchMode.RoundDown, SearchMode.RoundUp );
    }

    private IndexRange GetIndicesRange( IRange range, SearchMode downSearchMode, SearchMode upSearchMode )
    {
        IndexRange indicesRange = new IndexRange(0, -1);
        if ( _segmentDates.Count == 0 )
            return indicesRange;
        if ( !( range.Clone() is IndexRange indexRange ) )
            indexRange = SearchDataIndexesOn( range, downSearchMode, upSearchMode );
        return NormalizeIndexRange( indexRange );
    }

    IRange IDataSeries.GetWindowedYRange( IRange xRange )
    {
        IndexRange range = SearchDataIndexesOn(xRange, (SearchMode)2, (SearchMode)3);
        return !range.IsDefined ? ( IRange ) new DoubleRange( double.MinValue, double.MaxValue ) : GetWindowedYRange( range );
    }

    IRange IDataSeries.GetWindowedYRange( IndexRange indexRange, bool getPositiveRange )
    {
        return !indexRange.IsDefined ? ( IRange ) new DoubleRange( double.MinValue, double.MaxValue ) : GetWindowedYRange( indexRange );
    }

    IRange IDataSeries.GetWindowedYRange( IRange xRange, bool getPositiveRange )
    {
        return ( ( IDataSeries ) this ).GetWindowedYRange( xRange );
    }

    //
    // Summary:
    //     Gets the total extents of the SciChart.Charting.Model.DataSeries.IDataSeries
    //     in the X direction.
    //
    // Remarks:
    //     Note: The performance implications of calling this is the DataSeries will perform
    //     a full recalculation on each get. It is recommended to get and cache if this
    //     property is needed more than once.
    //
    public IRange XRange
    {
        get
        {
            if ( !_segmentDates.Any<DateTime>() )
            {
                return ( IRange ) new DoubleRange( double.MinValue, double.MaxValue );
            }

            return ( IRange ) new DateRange( _segmentDates[0], _segmentDates[_segmentDates.Count - 1] ).AsDoubleRange();
        }
    }


    //
    // Summary:
    //     Gets the total extents of the SciChart.Charting.Model.DataSeries.IDataSeries
    //     in the Y direction.
    //
    // Remarks:
    //     Note: The performance implications of calling this is the DataSeries will perform
    //     a full recalculation on each get. It is recommended to get and cache if this
    //     property is needed more than once.
    //
    public IRange YRange
    {
        get
        {
            if ( _segments.Count == 0 )
                return ( IRange ) new DoubleRange( double.MinValue, double.MaxValue );

            double min = double.MaxValue;
            double max = double.MinValue;

            using ( var myEnum = _segments.GetEnumerator() )
            {
                while ( myEnum.MoveNext() )
                {
                    var current = myEnum.Current;

                    if ( current.LowPrice < min )
                        min = current.LowPrice;

                    if ( current.HighPrice > max )
                        max = current.HighPrice;
                }
            }
            return ( IRange ) new DoubleRange( min, max );
        }
    }

    private IndexRange SearchDataIndexesOn( IRange inputRange, SearchMode downSearchMode, SearchMode upSearchMode )
    {
        var isIndexR = inputRange as IndexRange;

        if ( isIndexR != null )
        {
            return ( IndexRange ) isIndexR.Clone();
        }

        var output = new IndexRange(-1, -1);
        var isDoubleRange = inputRange as DoubleRange;

        var isDateRange = ( isDoubleRange == null ) ? ( inputRange as DateRange ) : new DateRange(new DateTime((long)isDoubleRange.Min), new DateTime((long)isDoubleRange.Max));

        if ( isDateRange == null )
        {
            SciChartDebugLogger.Instance.WriteLine( "ERROR: SearchDataIndexesOn: unable to convert range type={0}", inputRange.GetType().Name );

            return NormalizeIndexRange( output );
        }

        DateTime[] dt = _segmentDates.ToArray();

        output.Min = ( ( IList ) dt ).FindIndex( true, ( IComparable ) isDateRange.Min, downSearchMode );
        output.Max = ( ( IList ) dt ).FindIndex( true, ( IComparable ) isDateRange.Max, upSearchMode );

        return NormalizeIndexRange( output );
    }

    private IndexRange NormalizeIndexRange( IndexRange indexRange )
    {
        int count = _segmentDates.Count;

        if ( indexRange.IsDefined )
        {
            if ( indexRange.Min > count - 1 || indexRange.Max < 0 )
            {
                indexRange.Min = indexRange.Max = 0;
            }
            else
            {
                indexRange.Min = Math.Max( indexRange.Min, 0 );
                indexRange.Max = Math.Max( Math.Min( indexRange.Max, count - 1 ), indexRange.Min );
            }
        }

        if ( indexRange.Min.CompareTo( indexRange.Max ) > 0 )
        {
            indexRange.Min = 0;
        }

        SciChartDebugLogger.Instance.WriteLine( "GetIndicesRange(timeframesegment): Min={0}, Max={1}", indexRange.Min, indexRange.Max );

        return indexRange;
    }

    public static Tuple<DateTime, DateTime, long> GetTimeframePeriod( DateTime dt, TimeSpan period, Tuple<DateTime, DateTime, long> prevPeriod = null )
    {
        if ( ( period <= TimeSpan.Zero ) || ( period > _MaxTimeframe ) )
        {
            throw new ArgumentOutOfRangeException( nameof( period ) );
        }
            
        if ( prevPeriod != null && dt < prevPeriod.Item2 && dt >= prevPeriod.Item1 )
        {
            return prevPeriod;
        }
            
        long ticks = period.Ticks;
        long barCount;
        DateTime barBeginTime;
        DateTime barEndTime;

        if ( period < TimeSpan.FromDays( 1.0 ) )
        {
            // fraction of day that has elapsed since midnight
            barCount     = dt.TimeOfDay.Ticks / ticks;
            barBeginTime = dt.Date + TimeSpan.FromTicks( barCount * ticks );
            barEndTime   = barBeginTime + period;

            if ( barEndTime.Day != barBeginTime.Day )
                barEndTime = barEndTime.Date;
        }
        else if ( period == TimeSpan.FromDays( 1.0 ) )
        {
            barBeginTime = new DateTime( dt.Year, dt.Month, dt.Day );
            barEndTime   = barBeginTime + period;
            barCount     = ( long ) ( barBeginTime.DayOfYear - 1 );
        }
        else if ( period == TimeSpan.FromDays( 7.0 ) )
        {
            barBeginTime = new DateTime( dt.Year, dt.Month, dt.Day ) - TimeSpan.FromDays( ( double ) dt.DayOfWeek );
            barEndTime   = barBeginTime + period;
            barCount     = ( long ) barBeginTime.WeekNumber();
        }
        else
        {
            if ( period > _MaxTimeframe )
            {
                throw new NotSupportedException( $"Periods more than {_MaxTimeframe} days are not supported." );
            }
                
            var jan1st   = new DateTime(dt.Year, 1, 1);
            barCount     = ( long ) ( int ) ( ( jan1st - dt ).Ticks / ticks );
            barBeginTime = jan1st + TimeSpan.FromTicks( barCount * ticks );
            barEndTime   = barBeginTime + period;

            if ( barEndTime.Year != barBeginTime.Year )
            {
                barEndTime = new DateTime( barEndTime.Year, 1, 1 );
            }
                
        }
        return Tuple.Create<DateTime, DateTime, long>( barBeginTime, barEndTime, barCount );
    }

    public static double[ ] GeneratePrices( double min, double max, double step )
    {
        min        = min.NormalizePrice( step );
        max        = max.NormalizePrice( step );
        var prices = new double[1 + (int)Math.Round(( max - min ) / step)];

        for ( int i = 0; i < prices.Length; ++i )
        {
            prices[i] = ( min + ( double ) i * step ).NormalizePrice( step );
        }
            
        return prices;
    }

    public void Append( DateTime time, double price, CandlePriceLevel level )
    {
        bool succeed;

        lock ( SyncRoot )
        {
            if ( Timeframe.HasValue )
            {
                _curPeriod = GetTimeframePeriod( time, Timeframe.Value, _curPeriod );

                if ( _segments.Count > 0 )
                {
                    if ( _segments[_segments.Count - 1].Time > _curPeriod.Item1 )
                    {
                        throw new ArgumentOutOfRangeException( nameof( time ), "data must be ordered by time" );
                    }                        
                }
                succeed = AddOrUpdateSegment( _curPeriod.Item1, price, level, true );
            }
            else
                succeed = AddOrUpdateSegment( time, price, level, true );
        }
        if ( !succeed )
            return;
        
        DataSeriesChanged?.Invoke( this, new DataSeriesChangedEventArgs( ( DataSeriesUpdate ) 1 ) );
    }

    public void Update( DateTime time, double price, CandlePriceLevel level )
    {
        bool succeed;

        lock ( SyncRoot )
        {
            if ( Timeframe.HasValue )
            {
                _curPeriod = GetTimeframePeriod( time, Timeframe.Value, _curPeriod );

                if ( _segments.Count > 0 )
                {
                    if ( _segments[_segments.Count - 1].Time > _curPeriod.Item1 )
                    {
                        throw new ArgumentOutOfRangeException( nameof( time ), "data must be ordered by time" );
                    }                        
                }

                succeed = AddOrUpdateSegment( _curPeriod.Item1, price, level, false );
            }
            else
                succeed = AddOrUpdateSegment( time, price, level, false );
        }

        if ( !succeed )
            return;
        
        DataSeriesChanged?.Invoke( this, new DataSeriesChangedEventArgs( ( DataSeriesUpdate ) 1 ) );
    }

    private bool AddOrUpdateSegment( DateTime time, double price, CandlePriceLevel level, bool addVolume )
    {
        if ( level.TotalVolume < 0M )
        {
            throw new ArgumentOutOfRangeException( nameof( level ), level.TotalVolume, LocalizedStrings.InvalidValue );
        }

        if ( addVolume && level.TotalVolume == 0M )
            return false;

        TimeframeDataSegment tfds = null;

        if ( _segments.Count == 0 )
        {
            tfds = new TimeframeDataSegment( time, _segments.Count );
            _segments.Add( tfds );
            _segmentDates.Add( tfds.Time );
        }
        else
        {
            if ( _segments[_segments.Count - 1].Time == time )
            {
                tfds = _segments[_segments.Count - 1];
            }
            else
            {
                tfds = new TimeframeDataSegment( time, _segments.Count );
                _segments.Add( tfds );
                _segmentDates.Add( tfds.Time );
            }
        }

        if ( addVolume )
        {
            tfds.JoinCandlePriceLevel( price, level );
            _volumeProfile.JoinCandlePriceLevel( price, level );
        }
        else
        {
            tfds.SetCandlePriceLevel( price, level );
            _volumeProfile.SetCandlePriceLevel( price, level );
        }
        return true;
    }

    public double GetYMinAt( int index, double existingYMin )
    {
        return Math.Min( _segments[index].LowPrice, existingYMin );
    }

    public double GetYMaxAt( int index, double existingYMax )
    {
        return Math.Max( _segments[index].HighPrice, existingYMax );
    }

    public IRange GetWindowedYRange( IndexRange xIndexRange )
    {
        IndexRange iRange = (IndexRange)xIndexRange.Clone();
        
        if ( iRange.Min < 0 )
        {
            iRange.Min = 0;
        }
            
        if ( iRange.Max >= _segments.Count )
        {
            iRange.Max = _segments.Count - 1;
        }
            
        (double min, double max) tuple = TimeframeDataSegment.MinMax( _segments.Skip<TimeframeDataSegment>(iRange.Min).Take<TimeframeDataSegment>(iRange.Max - iRange.Min + 1));

        return ( IRange ) new DoubleRange( tuple.min, tuple.max );
    }

    public IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
    {
        return !pointRange.IsDefined ? null : ( IPointSeries ) new TimeframeSegmentPointSeries( Segments.ItemsArray, pointRange, visibleXRange, ( double ) ( _element.PriceStep ?? 0.000001M ) );
    }

    public CandlePriceLevel GetVolumeByPrice( double price, double priceStep )
    {
        return _volumeProfile.GetCPLFromPriceStep( price, priceStep );
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
        ISciChartSurface ps = ParentSurface;

        if ( ps == null )
        {
            return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this );
        }
            
        System.Threading.Monitor.Enter( ps.RootGrid );

        return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this, ps.RootGrid );
    }

    public void ResumeUpdates( IUpdateSuspender suspender )
    {
        if ( suspender.ResumeTargetOnDispose )
        {            
            DataSeriesChanged?.Invoke( this, new DataSeriesChangedEventArgs( ( DataSeriesUpdate ) 3 ) );
        }

        if ( suspender.Tag == null )
            return;

        System.Threading.Monitor.Exit( suspender.Tag );
    }

    public void DecrementSuspend()
    {
    }

    void IDataSeries<DateTime, double>.Append( DateTime dt, params double[ ] yValues )
    {
        throw new NotImplementedException();
    }

    public void Append( IEnumerable<DateTime> dtList, params IEnumerable<double>[ ] yValues )
    {
        throw new NotImplementedException();
    }

    //[Obsolete("IsAttached is obsolete because there is no DataSeriesSet now")]
    //bool IDataSeries.IsAttached

    //{
    //    get => throw new NotImplementedException();
    //}

    int? IDataSeries.FifoCapacity

    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }


    //void IDataSeries<DateTime, double>.Remove( DateTime x)

    //{
    //    throw new NotImplementedException();
    //}

    void IDataSeries<DateTime, double>.RemoveAt( int index )
    {
        throw new NotImplementedException();
    }

    void IDataSeries<DateTime, double>.RemoveRange( int startIndex, int count )

    {
        throw new NotImplementedException();
    }

    IDataSeries<DateTime, double> IDataSeries<DateTime, double>.Clone()
    {
        throw new NotImplementedException();
    }

    int IDataSeries.FindIndex( IComparable x, SearchMode searchMode )
    {
        throw new NotImplementedException();
    }

    int IDataSeries.FindClosestPoint( IComparable x, IComparable y, double xyScaleRatio, double maxXDistance )

    {
        throw new NotImplementedException();
    }

    int IDataSeries.FindClosestLine( IComparable x, IComparable y, double xyScaleRatio, double maxXDistance, LineDrawMode drawNanAs )
    {
        throw new NotImplementedException();
    }

    public virtual void OnBeginRenderPass()
    {
    }

    IPointSeries ToPointSeries( ResamplingParams resamplingParams, ResamplingMode resamplingMode, IPointResamplerFactory factory, IPointSeries lastPointSeries )

    {
        throw new NotImplementedException();
    }

    HitTestInfo IDataSeries.ToHitTestInfo( int index )
    {
        return HitTestInfo.Empty;
    }

    public void Clear( bool releaseMemory = false )
    {
        throw new NotImplementedException();
    }

    IndexRange IDataSeries.GetIndicesRange( IRange visibleRange, SearchMode downSearchMode, SearchMode upSearchMode )
    {
        return GetIndicesRange( visibleRange, downSearchMode, upSearchMode );
    }

    public IPointSeries ToPointSeries( ResamplingParams resamplingParams, SciChart.Data.Numerics.ResamplingMode resamplingMode, IPointResamplerFactory factory, IPointSeries lastPointSeries )
    {
        throw new NotImplementedException();
    }

    public IRange GetXRange( bool getPositiveRange )
    {
        throw new NotImplementedException();
    }

    public void InvalidateParentSurface( RangeMode rangeMode, bool hasDataChanged = true )
    {
        throw new NotImplementedException();
    }

    #region new Interface
    public IDataDistributionCalculator<DateTime, double> DataDistributionCalculator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public object Tag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IList<IPointMetadata> Metadata => throw new NotImplementedException();

    public bool HasMetadata => throw new NotImplementedException();

    public double MinXSpacing => throw new NotImplementedException();

    public int ChangeCount => throw new NotImplementedException();

    public bool DataIsSortedAscending => throw new NotImplementedException();

    public bool DataIsEvenlySpaced => throw new NotImplementedException();

    public bool DataContainsNaN => throw new NotImplementedException();

    TimeSpan? ITimeframe.Timeframe { get => Timeframe; set => throw new NotImplementedException(); }

    private sealed class YValueList( TimeframeSegmentDataSeries ts ) : IList, IEnumerable, ICollection, IList<double>, ICollection<double>, IEnumerable<double>
    {
        private readonly TimeframeSegmentDataSeries _parentElement = ts ?? throw new ArgumentNullException("parent");

        public int Count => _parentElement._segments.Count;

        public object SyncRoot
        {
            get => ( ( ICollection ) _parentElement._segments ).SyncRoot;
        }

        public bool IsSynchronized
        {
            get => ( ( ICollection ) _parentElement._segments ).IsSynchronized;
        }

        public bool IsReadOnly => true;

        public bool IsFixedSize => false;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( IEnumerator ) GetEnumerator();
        }

        public IEnumerator<double> GetEnumerator()
        {
            return _parentElement._segments.Select( s => s.Y ).GetEnumerator();
        }

        bool IList.Contains( object value )
        {
            return value is double num && Contains( num );
        }

        public bool Contains( double value )
        {
            return _parentElement._segments.Select( s => s.Y ).Contains<double>( value );
        }

        int IList.IndexOf( object value )
        {
            return !( value is double num ) ? -1 : IndexOf( num );
        }

        public int IndexOf( double _param1 )
        {
            int count = _parentElement._segments.Count;
            for ( int index = 0; index < count; ++index )
            {
                if ( _parentElement._segments[index].Y.DoubleEquals( _param1 ) )
                    return index;
            }
            return -1;
        }

        public double this[int index]
        {
            get => _parentElement._segments[index].Y;
            set => throw new NotImplementedException();
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public void CopyTo( double[ ] array, int arrayIndex )
        {
            throw new NotImplementedException();
        }

        public void CopyTo( Array array, int index )
        {
            throw new NotImplementedException();
        }

        public void Add( double value )
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Insert( int index, double value )
        {
            throw new NotImplementedException();
        }

        public bool Remove( double value )
        {
            throw new NotImplementedException();
        }

        public void RemoveAt( int index )
        {
            throw new NotImplementedException();
        }

        int IList.Add( object value )
        {
            throw new NotImplementedException();
        }

        void IList.Insert( int index, object value )
        {
            throw new NotImplementedException();
        }

        void IList.Remove( object value )
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
