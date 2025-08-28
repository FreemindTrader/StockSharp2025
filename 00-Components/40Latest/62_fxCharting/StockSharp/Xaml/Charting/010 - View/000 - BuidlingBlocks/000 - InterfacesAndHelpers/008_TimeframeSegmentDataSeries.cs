// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries.TimeframeSegmentDataSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll


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

public class TimeframeSegmentDataSeries : BindableObject100, IDataSeries<DateTime, double>, ISuspendable, IDataSeries, ITimeframe
{
    public const Decimal MinPriceStep = 0.000001M;
    public static readonly TimeSpan MaxTimeframe = TimeSpan.FromDays(30.0);
    public static readonly IMath<DateTime> XMath = GenericMathFactory.GetMath<DateTime>();
    private string                                         _seriesName;
    private readonly TimeframeDataSegment                  _volumeProfile = new TimeframeDataSegment(new DateTime(), 0);
    private readonly SciList<TimeframeDataSegment>       _segments      = new SciList<TimeframeDataSegment>();
    private readonly SciList<DateTime>                   _segmentDates  = new SciList<DateTime>();
    private readonly TimeframeSegmentDataSeries.YValueList _yValues;
    private readonly ChartCandleElement                    _element;
    private Tuple<DateTime, DateTime, long>                _curPeriod;


    private sealed class YValueList(TimeframeSegmentDataSeries ts ) : IList, IEnumerable, ICollection, IList<double>, ICollection<double>, IEnumerable<double>
    {
        private readonly TimeframeSegmentDataSeries _parentElement = ts ?? throw new ArgumentNullException("parent");

        public int Count => this._parentElement._segments.Count;

        public object SyncRoot
        {
            get => ( (ICollection)this._parentElement._segments ).SyncRoot;
        }

        public bool IsSynchronized
        {
            get => ( (ICollection)this._parentElement._segments ).IsSynchronized;
        }

        public bool IsReadOnly => true;

        public bool IsFixedSize => false;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        public IEnumerator<double> GetEnumerator()
        {
            return this._parentElement._segments.Select(s => s.Y).GetEnumerator();
        }

        bool IList.Contains(object value)
        {
            return value is double num && this.Contains(num);
        }

        public bool Contains(double value)
        {
            return this._parentElement._segments.Select(s => s.Y).Contains<double>(value);
        }

        int IList.IndexOf(object value)
        {
            return !( value is double num ) ? -1 : this.IndexOf(num);
        }

        public int IndexOf(double _param1)
        {
            int count = this._parentElement._segments.Count;
            for ( int index = 0; index < count; ++index )
            {
                if ( this._parentElement._segments[index].Y.DoubleEquals(_param1) )
                    return index;
            }
            return -1;
        }

        public double this[int index]
        {
            get => this._parentElement._segments[index].Y;
            set => throw new NotImplementedException();
        }

        object IList.this[int index]
        {
            get
            {
                return (object)this[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public void CopyTo(double[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void Add(double value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, double value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(double value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        int IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }
    }


    public TimeframeSegmentDataSeries(TimeSpan? timeframe, ChartCandleElement element)
    {
        if ( timeframe.HasValue &&
            ( !( timeframe.Value > TimeSpan.Zero ) || !( timeframe.Value <= TimeframeSegmentDataSeries.MaxTimeframe ) ) )
            throw new ArgumentOutOfRangeException(nameof(timeframe));
        this._yValues = new TimeframeSegmentDataSeries.YValueList(this);
        this.Timeframe = timeframe;
        this._element = element ?? throw new ArgumentNullException(nameof(element));
    }

    private IUltraReadOnlyList<DateTime> SegmentDates
    {
        get => this._segmentDates.AsReadOnly();
    }

    public DataSeriesType DataSeriesType
    {
        get => (DataSeriesType)9;
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

    public ISciChartSurface ParentSurface
    {
        get;
        set;
    }

    public string SeriesName
    {
        get => this._seriesName;
        set
        {
            this._seriesName = value;
            EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
            if ( dataSeriesChanged == null )
                return;
            dataSeriesChanged((object)this, new DataSeriesChangedEventArgs(DataSeriesUpdate.DataChanged));
        }
    }

    public bool HasValues => this._segmentDates.Count > 0;

    //bool IDataSeries.IsSecondary
    //{
    //    get => false;
    //}

    public int Count => this._segmentDates.Count;

    public IComparable YMin => this.YRange.Min;

    public IComparable YMax => this.YRange.Max;

    IComparable IDataSeries.XMin
    {
        get => this.XRange.Min;
    }

    IComparable IDataSeries.XMax
    {
        get => this.XRange.Max;
    }

    bool IDataSeries.IsFifo
    {
        get => false;
    }

    public bool IsSorted => true;

    public IUltraList<TimeframeDataSegment> Segments
    {
        get
        {
            return (IUltraList<TimeframeDataSegment>)this._segments;
        }
    }

    protected IUltraReadOnlyList<TimeframeDataSegment> SegmentsReadOnly
    {
        get => this._segments.AsReadOnly();
    }

    public Type XType => typeof(DateTime);

    public Type YType => typeof(double);

    public IComparable LatestYValue => (IComparable)null;

    private IList<DateTime> XValues => (IList<DateTime>)this.SegmentDates;

    IList IDataSeries.XValues
    {
        get => (IList)this.XValues;
    }

    IList<DateTime> IDataSeries<DateTime, double>.XValues
    {
        get => this.XValues;
    }

    IList IDataSeries.YValues
    {
        get => (IList)this._yValues;
    }

    IList<double> IDataSeries<DateTime, double>.YValues
    {
        get => (IList<double>)this._yValues;
    }

    public void Clear()
    {
        SciChartDebugLogger.Instance
            .WriteLine("ERROR: TimeframeSegmentDataSeries.Clear(): not supported", Array.Empty<object>());
    }

    public void InvalidateParentSurface(RangeMode rangeMode)
    {
        if ( this.ParentSurface == null )
            return;

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

    public IndexRange GetIndicesRange(IRange visibleRange)
    {
        return this.GetIndicesRange(visibleRange, SearchMode.RoundDown, SearchMode.RoundUp);
    }

    private IndexRange GetIndicesRange(IRange range, SearchMode downSearchMode, SearchMode upSearchMode)
    {
        IndexRange indicesRange = new IndexRange(0, -1);
        if ( this._segmentDates.Count == 0 )
            return indicesRange;
        if ( !( range.Clone() is IndexRange indexRange ) )
            indexRange = this.SearchDataIndexesOn(range, downSearchMode, upSearchMode);
        return this.NormalizeIndexRange(indexRange);
    }

    IRange IDataSeries.GetWindowedYRange(IRange xRange)
    {
        IndexRange range = this.SearchDataIndexesOn(xRange, (SearchMode)2, (SearchMode)3);
        return !range.IsDefined ? (IRange)new DoubleRange(double.MinValue, double.MaxValue) : this.GetWindowedYRange(range);
    }

    IRange IDataSeries.GetWindowedYRange(IndexRange indexRange, bool getPositiveRange)
    {
        return !indexRange.IsDefined ? (IRange)new DoubleRange(double.MinValue, double.MaxValue) : this.GetWindowedYRange(indexRange);
    }

    IRange IDataSeries.GetWindowedYRange(IRange xRange, bool getPositiveRange)
    {
        return ( (IDataSeries)this ).GetWindowedYRange(xRange);
    }

    public IRange XRange
    {
        get
        {
            if ( !this._segmentDates.Any<DateTime>() )
            {
                return (IRange)new DoubleRange(double.MinValue, double.MaxValue);
            }

            return (IRange)new DateRange(this._segmentDates[0], this._segmentDates[this._segmentDates.Count - 1]).AsDoubleRange();
        }
    }

    public IRange YRange
    {
        get
        {
            if ( this._segments.Count == 0 )
                return (IRange)new DoubleRange(double.MinValue, double.MaxValue);
            
            double min = double.MaxValue;
            double max = double.MinValue;

            using ( var myEnum = this._segments.GetEnumerator() )
            {
                while ( myEnum.MoveNext() )
                {
                    var current = myEnum.Current;

                    if ( current.MinPrice < min )
                        min = current.MinPrice;

                    if ( current.MaxPrice > max )
                        max = current.MaxPrice;
                }
            }
            return (IRange)new DoubleRange(min, max);
        }
    }

    private IndexRange SearchDataIndexesOn(IRange range, SearchMode downSearchMode, SearchMode upSearchMode)
    {
        IndexRange indexRange = range as IndexRange;
        if ( indexRange != null )
        {
            return (IndexRange)indexRange.Clone();
        }
        IndexRange indexRange2 = new IndexRange(-1, -1);
        DoubleRange doubleRange = range as DoubleRange;
        DateRange dateRange = ( doubleRange == null ) ? ( range as DateRange ) : new DateRange(new DateTime((long)doubleRange.Min), new DateTime((long)doubleRange.Max));
        
        if ( dateRange == null )
        {
            SciChartDebugLogger.Instance.WriteLine("ERROR: SearchDataIndexesOn: unable to convert range type={0}", range.GetType().Name);

            return NormalizeIndexRange(indexRange2);
        }
        DateTime[] list = _segmentDates.ToArray();
        indexRange2.Min = ( (IList)list ).FindIndex(true, (IComparable)dateRange.Min, downSearchMode);
        indexRange2.Max = ( (IList)list ).FindIndex(true, (IComparable)dateRange.Max, upSearchMode);
        return NormalizeIndexRange(indexRange2);
    }

    private IndexRange NormalizeIndexRange(IndexRange indexRange)
    {
        int count = this._segmentDates.Count;
        
        if ( indexRange.IsDefined )
        {
            if ( indexRange.Min > count - 1 || indexRange.Max < 0 )
            {
                indexRange.Min = indexRange.Max = 0;
            }
            else
            {
                indexRange.Min = Math.Max(indexRange.Min, 0);
                indexRange.Max = Math.Max(Math.Min(indexRange.Max, count - 1), indexRange.Min);
            }
        }

        if ( indexRange.Min.CompareTo(indexRange.Max) > 0 )
        {
            indexRange.Min = 0;
        }

        SciChartDebugLogger.Instance.WriteLine( "GetIndicesRange(timeframesegment): Min={0}, Max={1}", indexRange.Min, indexRange.Max);

        return indexRange;
    }

    public static Tuple<DateTime, DateTime, long> GetTimeframePeriod(DateTime dt, TimeSpan period, Tuple<DateTime, DateTime, long> prevPeriod = null)
    {
        if ( !( period > TimeSpan.Zero ) || !( period <= TimeframeSegmentDataSeries.MaxTimeframe ) )
            throw new ArgumentOutOfRangeException(nameof(period));
        if ( prevPeriod != null && dt < prevPeriod.Item2 && dt >= prevPeriod.Item1 )
            return prevPeriod;
        long ticks = period.Ticks;
        long num;
        DateTime dateTime1;
        DateTime dateTime2;
        if ( period < TimeSpan.FromDays(1.0) )
        {
            num = dt.TimeOfDay.Ticks / ticks;
            dateTime1 = dt.Date + TimeSpan.FromTicks(num * ticks);
            dateTime2 = dateTime1 + period;
            if ( dateTime2.Day != dateTime1.Day )
                dateTime2 = dateTime2.Date;
        }
        else if ( period == TimeSpan.FromDays(1.0) )
        {
            dateTime1 = new DateTime(dt.Year, dt.Month, dt.Day);
            dateTime2 = dateTime1 + period;
            num = (long)( dateTime1.DayOfYear - 1 );
        }
        else if ( period == TimeSpan.FromDays(7.0) )
        {
            dateTime1 = new DateTime(dt.Year, dt.Month, dt.Day) - TimeSpan.FromDays((double)dt.DayOfWeek);
            dateTime2 = dateTime1 + period;
            num = (long)dateTime1.WeekNumber();
        }
        else
        {
            if ( period > TimeframeSegmentDataSeries.MaxTimeframe )
                throw new NotSupportedException(
                    $"Periods more than {TimeframeSegmentDataSeries.MaxTimeframe} days are not supported.");
            DateTime dateTime3 = new DateTime(dt.Year, 1, 1);
            num = (long)(int)( ( dateTime3 - dt ).Ticks / ticks );
            dateTime1 = dateTime3 + TimeSpan.FromTicks(num * ticks);
            dateTime2 = dateTime1 + period;
            if ( dateTime2.Year != dateTime1.Year )
                dateTime2 = new DateTime(dateTime2.Year, 1, 1);
        }
        return Tuple.Create<DateTime, DateTime, long>(dateTime1, dateTime2, num);
    }

    public static double[] GeneratePrices(double min, double max, double step)
    {
        min = min.NormalizePrice(step);
        max = max.NormalizePrice(step);
        double[] prices = new double[1 + (int)Math.Round(( max - min ) / step)];
        for ( int index = 0; index < prices.Length; ++index )
            prices[index] = ( min + (double)index * step ).NormalizePrice(step);
        return prices;
    }

    public void Append(DateTime time, double price, CandlePriceLevel level)
    {
        bool flag;
        lock ( this.SyncRoot )
        {
            if ( this.Timeframe.HasValue )
            {
                this._curPeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(
                    time,
                    this.Timeframe.Value,
                    this._curPeriod);
                if ( this._segments.Count > 0 )
                {
                    if ( _segments[_segments.Count - 1].Time > this._curPeriod.Item1 )
                        throw new ArgumentOutOfRangeException(nameof(time), "data must be ordered by time");
                }
                flag = this.AddOrUpdateSegment(this._curPeriod.Item1, price, level, true);
            }
            else
                flag = this.AddOrUpdateSegment(time, price, level, true);
        }
        if ( !flag )
            return;
        EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
        if ( dataSeriesChanged == null )
            return;
        dataSeriesChanged((object)this, new DataSeriesChangedEventArgs((DataSeriesUpdate)1));
    }

    public void Update(DateTime time, double price, CandlePriceLevel level)
    {
        bool flag;
        lock ( this.SyncRoot )
        {
            if ( this.Timeframe.HasValue )
            {
                this._curPeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(
                    time,
                    this.Timeframe.Value,
                    this._curPeriod);

                if ( this._segments.Count > 0 )
                {
                    if ( _segments[_segments.Count - 1].Time > this._curPeriod.Item1 )
                        throw new ArgumentOutOfRangeException(nameof(time), "data must be ordered by time");
                }
                flag = this.AddOrUpdateSegment(this._curPeriod.Item1, price, level, false);
            }
            else
                flag = this.AddOrUpdateSegment(time, price, level, false);
        }
        if ( !flag )
            return;
        EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
        if ( dataSeriesChanged == null )
            return;
        dataSeriesChanged((object)this, new DataSeriesChangedEventArgs((DataSeriesUpdate)1));
    }

    private bool AddOrUpdateSegment(DateTime time, double price, CandlePriceLevel level, bool sum)
    {
        if ( level.TotalVolume < 0M )
        {
            throw new ArgumentOutOfRangeException(nameof(level), level.TotalVolume, LocalizedStrings.InvalidValue);
        }

        if ( sum && level.TotalVolume == 0M )
            return false;
        TimeframeDataSegment tfds;
        if ( this._segments.Count != 0 )
        {
            if ( !( _segments[_segments.Count - 1].Time != time ) )
            {
                tfds = _segments[_segments.Count - 1];
                goto label_8;
            }
        }
        tfds = new TimeframeDataSegment(time, this._segments.Count);
        this._segments.Add(tfds);
        this._segmentDates.Add(tfds.Time);
label_8:
        if ( sum )
        {
            tfds.AddOrUpdatePriceLevel(price, level);
            this._volumeProfile.AddOrUpdatePriceLevel(price, level);
        }
        else
        {
            tfds.SetPriceLevel(price, level);
            this._volumeProfile.SetPriceLevel(price, level);
        }
        return true;
    }

    public double GetYMinAt(int index, double existingYMin)
    {
        return Math.Min(this._segments[index].MinPrice, existingYMin);
    }

    public double GetYMaxAt(int index, double existingYMax)
    {
        return Math.Max(this._segments[index].MaxPrice, existingYMax);
    }

    public IRange GetWindowedYRange(IndexRange xIndexRange)
    {
        IndexRange g8Oq2rGx6KyfAreq = (IndexRange)xIndexRange.Clone();
        if ( g8Oq2rGx6KyfAreq.Min < 0 )
            g8Oq2rGx6KyfAreq.Min = 0;
        if ( g8Oq2rGx6KyfAreq.Max >= this._segments.Count )
            g8Oq2rGx6KyfAreq.Max = this._segments.Count - 1;
        (double, double) tuple = TimeframeDataSegment.MinMax(
            this._segments
                .Skip<TimeframeDataSegment>(g8Oq2rGx6KyfAreq.Min)
                .Take<TimeframeDataSegment>(g8Oq2rGx6KyfAreq.Max - g8Oq2rGx6KyfAreq.Min + 1));
        return (IRange)new DoubleRange(tuple.Item1, tuple.Item2);
    }

    public IPointSeries ToPointSeries(ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null)
    {
        return !pointRange.IsDefined ? (IPointSeries)null : (IPointSeries)new TimeframeSegmentPointSeries
        (this.Segments.ItemsArray, pointRange, visibleXRange, (double)( this._element.PriceStep ?? 0.000001M ));
    }

    public CandlePriceLevel GetVolumeByPrice(double price, double priceStep)
    {
        return this._volumeProfile.GetCandlePriceLevel(price, priceStep);
    }

    public bool IsSuspended
    {
        get
        {
            return UpdateSuspender.GetIsSuspended((ISuspendable)this);
        }
    }

    public IUpdateSuspender SuspendUpdates()
    {
        ISciChartSurface parentSurface = this.ParentSurface;
        if ( parentSurface == null )
            return (IUpdateSuspender)new UpdateSuspender((ISuspendable)this);

        System.Threading.Monitor.Enter(parentSurface.RootGrid);

        return (IUpdateSuspender)new UpdateSuspender((ISuspendable)this, parentSurface.RootGrid);
    }

    public void ResumeUpdates(IUpdateSuspender suspender)
    {
        if ( suspender.ResumeTargetOnDispose )
        {
            EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
            if ( dataSeriesChanged != null )
                dataSeriesChanged((object)this, new DataSeriesChangedEventArgs((DataSeriesUpdate)3));
        }
        if ( suspender.Tag == null )
            return;
        System.Threading.Monitor.Exit(suspender.Tag);
    }

    public void DecrementSuspend()
    {
    }

    void IDataSeries<DateTime, double>.Append(DateTime dt, params double[] yValues)
    {
        throw new NotImplementedException();
    }

    public void Append(IEnumerable<DateTime> dtList, params IEnumerable<double>[] yValues)
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

    void IDataSeries<DateTime, double>.RemoveAt( int index)
    {
        throw new NotImplementedException();
    }

    void IDataSeries<DateTime, double>.RemoveRange( int startIndex, int count)

    {
        throw new NotImplementedException();
    }

    IDataSeries<DateTime, double> IDataSeries<DateTime, double>.Clone()
    {
        throw new NotImplementedException();
    }

    int IDataSeries.FindIndex( IComparable x, SearchMode searchMode)
    {
        throw new NotImplementedException();
    }

    int IDataSeries.FindClosestPoint(IComparable x, IComparable y, double xyScaleRatio, double maxXDistance)

    {
        throw new NotImplementedException();
    }

    int IDataSeries.FindClosestLine(IComparable x, IComparable y, double xyScaleRatio, double maxXDistance, LineDrawMode drawNanAs)
    {
        throw new NotImplementedException();
    }

    public virtual void OnBeginRenderPass()
    {
    }

    IPointSeries ToPointSeries(ResamplingParams resamplingParams, ResamplingMode resamplingMode, IPointResamplerFactory factory, IPointSeries lastPointSeries)

    {
        throw new NotImplementedException();
    }

    HitTestInfo IDataSeries.ToHitTestInfo( int index)
    {
        return HitTestInfo.Empty;
    }

    public void Clear(bool releaseMemory = false)
    {
        throw new NotImplementedException();
    }

    IndexRange IDataSeries.GetIndicesRange(IRange visibleRange, SearchMode downSearchMode, SearchMode upSearchMode)
    {
        return GetIndicesRange(visibleRange, downSearchMode, upSearchMode);
    }

    public IPointSeries ToPointSeries(ResamplingParams resamplingParams, SciChart.Data.Numerics.ResamplingMode resamplingMode, IPointResamplerFactory factory, IPointSeries lastPointSeries)
    {
        throw new NotImplementedException();
    }

    public IRange GetXRange(bool getPositiveRange)
    {
        throw new NotImplementedException();
    }

    public void InvalidateParentSurface(RangeMode rangeMode, bool hasDataChanged = true)
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

    #endregion
}
