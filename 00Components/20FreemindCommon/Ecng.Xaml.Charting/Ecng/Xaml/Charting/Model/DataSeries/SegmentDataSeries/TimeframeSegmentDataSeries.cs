// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.SegmentDataSeries.TimeframeSegmentDataSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace fx.Xaml.Charting
{
    [Obfuscation( ApplyToMembers = true, Exclude = true )]
    public class TimeframeSegmentDataSeries : BindableObject, IDataSeries<DateTime, double>, IDataSeries, ISuspendable
    {
        public static readonly IMath<DateTime> XMath = GenericMathFactory.New<DateTime>();
        private readonly Dictionary<double, long> _volumeByPrice = new Dictionary<double, long>();
        private readonly UltraList<TimeframeDataSegment> _segments = new UltraList<TimeframeDataSegment>();
        private readonly UltraList<DateTime> _segmentDates = new UltraList<DateTime>();
        public const double MinPriceStep = 1E-06;
        public const int TimeframeOneDay = 1440;
        public const int TimeframeOneWeek = 10080;
        public const int MaxTimeframe = 10080;
        private string _seriesName;
        private readonly TimeframeSegmentDataSeries.YValueList _yValues;
        private Tuple<DateTime, DateTime, int> _curPeriod;

        private IUltraReadOnlyList<DateTime> SegmentDates
        {
            get
            {
                return this._segmentDates.AsReadOnly();
            }
        }

        public DataSeriesType DataSeriesType
        {
            get
            {
                return DataSeriesType.TimeframeSegment;
            }
        }

        public int Timeframe
        {
            get;
        }

        public double PriceStep
        {
            get;
        }

        public event EventHandler<DataSeriesChangedEventArgs> DataSeriesChanged;

        public object SyncRoot { get; } = new object();

        public bool AcceptsUnsortedData
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public ISciChartSurface ParentSurface
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
                EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
                if ( dataSeriesChanged == null )
                {
                    return;
                }

                dataSeriesChanged( ( object ) this, new DataSeriesChangedEventArgs( DataSeriesUpdate.DataChanged ) );
            }
        }

        public bool HasValues
        {
            get
            {
                return this._segmentDates.Count > 0;
            }
        }

        public int Count
        {
            get
            {
                return this._segmentDates.Count;
            }
        }

        public IComparable YMin
        {
            get
            {
                return this.YRange.Min;
            }
        }

        public IComparable YMax
        {
            get
            {
                return this.YRange.Max;
            }
        }

        IComparable IDataSeries.XMin
        {
            get
            {
                return this.XRange.Min;
            }
        }

        IComparable IDataSeries.XMax
        {
            get
            {
                return this.XRange.Max;
            }
        }

        bool IDataSeries.IsFifo
        {
            get
            {
                return false;
            }
        }

        public bool IsSorted
        {
            get
            {
                return true;
            }
        }

        internal IUltraList<TimeframeDataSegment> Segments
        {
            get
            {
                return ( IUltraList<TimeframeDataSegment> ) this._segments;
            }
        }

        protected IUltraReadOnlyList<TimeframeDataSegment> SegmentsReadOnly
        {
            get
            {
                return this._segments.AsReadOnly();
            }
        }

        public Type XType
        {
            get
            {
                return typeof( DateTime );
            }
        }

        public Type YType
        {
            get
            {
                return typeof( double );
            }
        }

        public IComparable LatestYValue
        {
            get
            {
                return ( IComparable ) null;
            }
        }

        private IList<DateTime> XValues
        {
            get
            {
                return ( IList<DateTime> ) this.SegmentDates;
            }
        }

        IList IDataSeries.XValues
        {
            get
            {
                return ( IList ) this.XValues;
            }
        }

        IList<DateTime> IDataSeries<DateTime, double>.XValues
        {
            get
            {
                return this.XValues;
            }
        }

        IList IDataSeries.YValues
        {
            get
            {
                return ( IList ) this._yValues;
            }
        }

        IList<double> IDataSeries<DateTime, double>.YValues
        {
            get
            {
                return ( IList<double> ) this._yValues;
            }
        }

        public TimeframeSegmentDataSeries( int timeframe, double priceStep )
        {
            if ( timeframe < 1 || timeframe > 10080 )
            {
                throw new ArgumentOutOfRangeException( nameof( timeframe ) );
            }

            if ( priceStep <= 0.0 || priceStep.IsNaN() )
            {
                throw new ArgumentOutOfRangeException( nameof( priceStep ) );
            }

            this._yValues = new TimeframeSegmentDataSeries.YValueList( this );
            this.Timeframe = timeframe;
            this.PriceStep = priceStep.NormalizePrice( 1E-06 );
        }

        public void Clear()
        {
            UltrachartDebugLogger.Instance.WriteLine( "ERROR: TimeframeSegmentDataSeries.Clear(): not supported" );
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

        public IndexRange GetIndicesRange( IRange visibleRange )
        {
            return this.GetIndicesRange( visibleRange, SearchMode.RoundDown, SearchMode.RoundUp );
        }

        private IndexRange GetIndicesRange( IRange range, SearchMode downSearchMode, SearchMode upSearchMode )
        {
            IndexRange indexRange = new IndexRange(0, -1);
            if ( this._segmentDates.Count == 0 )
            {
                return indexRange;
            }

            return this.NormalizeIndexRange( range.Clone() as IndexRange ?? this.SearchDataIndexesOn( range, downSearchMode, upSearchMode ) );
        }

        IRange IDataSeries.GetWindowedYRange( IRange xRange )
        {
            IndexRange xIndexRange = this.SearchDataIndexesOn(xRange, SearchMode.RoundDown, SearchMode.RoundUp);
            if ( !xIndexRange.IsDefined )
            {
                return ( IRange ) new DoubleRange( double.MinValue, double.MaxValue );
            }

            return this.GetWindowedYRange( xIndexRange );
        }

        IRange IDataSeries.GetWindowedYRange( IndexRange indexRange, bool getPositiveRange )
        {
            if ( !indexRange.IsDefined )
            {
                return ( IRange ) new DoubleRange( double.MinValue, double.MaxValue );
            }

            return this.GetWindowedYRange( indexRange );
        }

        IRange IDataSeries.GetWindowedYRange( IRange xRange, bool getPositiveRange )
        {
            return ( ( IDataSeries ) this ).GetWindowedYRange( xRange );
        }

        public IRange XRange
        {
            get
            {
                if ( !this._segmentDates.Any<DateTime>() )
                {
                    return ( IRange ) new DoubleRange( double.MinValue, double.MaxValue );
                }

                return ( IRange ) new DateRange( this._segmentDates[ 0 ], this._segmentDates[ this._segmentDates.Count - 1 ] ).AsDoubleRange();
            }
        }

        public IRange YRange
        {
            get
            {
                if ( this._segments.Count == 0 )
                {
                    return ( IRange ) new DoubleRange( double.MinValue, double.MaxValue );
                }

                double min = double.MaxValue;
                double max = double.MinValue;
                foreach ( TimeframeDataSegment segment in this._segments )
                {
                    if ( segment.MinPrice < min )
                    {
                        min = segment.MinPrice;
                    }

                    if ( segment.MaxPrice > max )
                    {
                        max = segment.MaxPrice;
                    }
                }
                return ( IRange ) new DoubleRange( min, max );
            }
        }

        private IndexRange SearchDataIndexesOn( IRange range, SearchMode downSearchMode, SearchMode upSearchMode )
        {
            IndexRange indexRange = range as IndexRange;
            if ( indexRange != null )
            {
                return ( IndexRange ) indexRange.Clone();
            }
            IndexRange indexRange2 = new IndexRange(-1, -1);
            DoubleRange doubleRange = range as DoubleRange;
            DateRange dateRange = (doubleRange == null) ? (range as DateRange) : new DateRange(new DateTime((long)doubleRange.Min), new DateTime((long)doubleRange.Max));
            if ( dateRange == null )
            {
                UltrachartDebugLogger.Instance.WriteLine( "ERROR: SearchDataIndexesOn: unable to convert range type={0}", range.GetType().Name );
                return NormalizeIndexRange( indexRange2 );
            }
            DateTime[] list = _segmentDates.ToArray();
            indexRange2.Min = ( ( IList ) list ).FindIndex( true, ( IComparable ) dateRange.Min, downSearchMode );
            indexRange2.Max = ( ( IList ) list ).FindIndex( true, ( IComparable ) dateRange.Max, upSearchMode );
            return NormalizeIndexRange( indexRange2 );
        }

        private IndexRange NormalizeIndexRange( IndexRange indexRange )
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
                    indexRange.Min = Math.Max( indexRange.Min, 0 );
                    indexRange.Max = Math.Max( Math.Min( indexRange.Max, count - 1 ), indexRange.Min );
                }
            }
            if ( indexRange.Min.CompareTo( indexRange.Max ) > 0 )
            {
                indexRange.Min = 0;
            }

            UltrachartDebugLogger.Instance.WriteLine( "GetIndicesRange(timeframesegment): Min={0}, Max={1}", ( object ) indexRange.Min, ( object ) indexRange.Max );
            return indexRange;
        }

        public static Tuple<DateTime, DateTime, int> GetTimeframePeriod( DateTime dt, int periodMinutes, Tuple<DateTime, DateTime, int> prevPeriod = null )
        {
            if ( periodMinutes < 1 || periodMinutes > 10080 )
            {
                throw new ArgumentOutOfRangeException( nameof( periodMinutes ) );
            }

            if ( prevPeriod != null && dt < prevPeriod.Item2 && dt >= prevPeriod.Item1 )
            {
                return prevPeriod;
            }

            int num;
            DateTime dt1;
            DateTime dateTime1;
            if ( periodMinutes < 1440 )
            {
                num = ( int ) ( dt.TimeOfDay.TotalMinutes / ( double ) periodMinutes );
                dt1 = dt.Date + TimeSpan.FromMinutes( ( double ) ( num * periodMinutes ) );
                dateTime1 = dt1 + TimeSpan.FromMinutes( ( double ) periodMinutes );
                if ( dateTime1.Day != dt1.Day )
                {
                    dateTime1 = dateTime1.Date;
                }
            }
            else
            {
                switch ( periodMinutes )
                {
                    case 1440:
                        dt1 = new DateTime( dt.Year, dt.Month, dt.Day );
                        dateTime1 = dt1 + TimeSpan.FromDays( 1.0 );
                        num = dt1.DayOfYear - 1;
                        break;
                    case 10080:
                        dt1 = new DateTime( dt.Year, dt.Month, dt.Day ) - TimeSpan.FromDays( ( double ) dt.DayOfWeek );
                        dateTime1 = dt1 + TimeSpan.FromDays( 7.0 );
                        num = dt1.WeekNumber();
                        break;
                    default:
                        if ( periodMinutes > 10080 )
                        {
                            throw new NotSupportedException( string.Format( "Periods more than {0} days are not supported.", ( object ) 10080 ) );
                        }

                        DateTime dateTime2 = new DateTime(dt.Year, 1, 1);
                        num = ( int ) ( ( dateTime2 - dt ).TotalMinutes / ( double ) periodMinutes );
                        dt1 = dateTime2 + TimeSpan.FromMinutes( ( double ) ( num * periodMinutes ) );
                        dateTime1 = dt1 + TimeSpan.FromMinutes( ( double ) periodMinutes );
                        if ( dateTime1.Year != dt1.Year )
                        {
                            dateTime1 = new DateTime( dateTime1.Year, 1, 1 );
                            break;
                        }
                        break;
                }
            }
            return Tuple.Create<DateTime, DateTime, int>( dt1, dateTime1, num );
        }

        internal static double[ ] GeneratePrices( double min, double max, double step )
        {
            min = min.NormalizePrice( step );
            max = max.NormalizePrice( step );
            double[] numArray = new double[1 + (int) Math.Round((max - min) / step)];
            for ( int index = 0 ; index < numArray.Length ; ++index )
            {
                numArray[ index ] = ( min + ( double ) index * step ).NormalizePrice( step );
            }

            return numArray;
        }

        public void Append( DateTime time, double price, long volume )
        {
            bool flag;
            lock ( this.SyncRoot )
            {
                this._curPeriod = TimeframeSegmentDataSeries.GetTimeframePeriod( time, this.Timeframe, this._curPeriod );
                if ( this._segments.Count > 0 && this._segments[ this._segments.Count - 1 ].Time > this._curPeriod.Item1 )
                {
                    throw new ArgumentOutOfRangeException( nameof( time ), "data must be ordered by time" );
                }

                flag = this.AddOrUpdateSegment( this._curPeriod.Item1, price, volume, true );
            }
            if ( !flag )
            {
                return;
            }
            // ISSUE: reference to a compiler-generated field
            EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
            if ( dataSeriesChanged == null )
            {
                return;
            }

            dataSeriesChanged( ( object ) this, new DataSeriesChangedEventArgs( DataSeriesUpdate.DataChanged ) );
        }

        public void Update( DateTime time, double price, long volume )
        {
            bool flag;
            lock ( this.SyncRoot )
            {
                this._curPeriod = TimeframeSegmentDataSeries.GetTimeframePeriod( time, this.Timeframe, this._curPeriod );
                if ( this._segments.Count > 0 && this._segments[ this._segments.Count - 1 ].Time > this._curPeriod.Item1 )
                {
                    throw new ArgumentOutOfRangeException( nameof( time ), "data must be ordered by time" );
                }

                flag = this.AddOrUpdateSegment( this._curPeriod.Item1, price, volume, false );
            }
            if ( !flag )
            {
                return;
            }
            // ISSUE: reference to a compiler-generated field
            EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
            if ( dataSeriesChanged == null )
            {
                return;
            }

            dataSeriesChanged( ( object ) this, new DataSeriesChangedEventArgs( DataSeriesUpdate.DataChanged ) );
        }

        private bool AddOrUpdateSegment( DateTime periodStart, double price, long volume, bool sum )
        {
            if ( volume < 0L )
            {
                throw new ArgumentException( nameof( volume ) );
            }

            if ( sum && volume == 0L )
            {
                return false;
            }

            double priceStep = this.PriceStep;
            if ( priceStep <= 0.0 || priceStep.IsNaN() )
            {
                return false;
            }

            price = price.NormalizePrice( priceStep );
            TimeframeDataSegment timeframeDataSegment;
            if ( this._segments.Count == 0 || this._segments[ this._segments.Count - 1 ].Time != periodStart )
            {
                timeframeDataSegment = new TimeframeDataSegment( periodStart, priceStep, this._segments.Count );
                this._segments.Add( timeframeDataSegment );
                this._segmentDates.Add( timeframeDataSegment.Time );
            }
            else
            {
                timeframeDataSegment = this._segments[ this._segments.Count - 1 ];
            }

            long num;
            this._volumeByPrice.TryGetValue( price, out num );
            if ( sum )
            {
                timeframeDataSegment.AddPoint( price, volume );
                this._volumeByPrice[ price ] = num + volume;
                return true;
            }
            long valueByPrice = timeframeDataSegment.GetValueByPrice(price);
            if ( volume == valueByPrice )
            {
                return false;
            }

            timeframeDataSegment.UpdatePoint( price, volume );
            this._volumeByPrice[ price ] = num - valueByPrice + volume;
            return true;
        }

        public double GetYMinAt( int index, double existingYMin )
        {
            return Math.Min( this._segments[ index ].MinPrice, existingYMin );
        }

        public double GetYMaxAt( int index, double existingYMax )
        {
            return Math.Max( this._segments[ index ].MaxPrice, existingYMax );
        }

        public IRange GetWindowedYRange( IndexRange xIndexRange )
        {
            IndexRange indexRange = (IndexRange) xIndexRange.Clone();
            if ( indexRange.Min < 0 )
            {
                indexRange.Min = 0;
            }

            if ( indexRange.Max >= this._segments.Count )
            {
                indexRange.Max = this._segments.Count - 1;
            }

            double minPrice;
            double maxPrice;
            TimeframeDataSegment.MinMax( this._segments.Skip<TimeframeDataSegment>( indexRange.Min ).Take<TimeframeDataSegment>( indexRange.Max - indexRange.Min + 1 ), out minPrice, out maxPrice );
            return ( IRange ) new DoubleRange( minPrice, maxPrice );
        }

        public IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
        {
            if ( !pointRange.IsDefined )
            {
                return ( IPointSeries ) null;
            }

            return ( IPointSeries ) new TimeframeSegmentPointSeries( this.Segments.ItemsArray, pointRange, visibleXRange, this.PriceStep );
        }

        public long GetVolumeByPrice( double price, double priceStep = 0.0 )
        {
            if ( priceStep == 0.0 )
            {
                priceStep = this.PriceStep;
            }

            price = price.NormalizePrice( priceStep );
            long num;
            this._volumeByPrice.TryGetValue( price, out num );
            return num;
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
                // ISSUE: reference to a compiler-generated field
                EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
                if ( dataSeriesChanged != null )
                {
                    dataSeriesChanged( ( object ) this, new DataSeriesChangedEventArgs( DataSeriesUpdate.DataChanged | DataSeriesUpdate.DataSeriesCleared ) );
                }
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

        void IDataSeries<DateTime, double>.Append( DateTime dt, params double[ ] yValues )
        {
            throw new NotImplementedException();
        }

        public void Append( IEnumerable<DateTime> dtList, params IEnumerable<double>[ ] yValues )
        {
            throw new NotImplementedException();
        }

        [Obsolete( "IsAttached is obsolete because there is no DataSeriesSet now" )]
        bool IDataSeries.IsAttached
        {
            get
            {
                throw new NotImplementedException();
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

        void IDataSeries<DateTime, double>.Remove( DateTime x )
        {
            throw new NotImplementedException();
        }

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

        IPointSeries IDataSeries.ToPointSeries( IList column, ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis )
        {
            throw new NotImplementedException();
        }

        HitTestInfo IDataSeries.ToHitTestInfo( int index )
        {
            return HitTestInfo.Empty;
        }

        IComparable IDataSeries.YMinPositive
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private class YValueList : IList<double>, ICollection<double>, IEnumerable<double>, IEnumerable, IList, ICollection
        {
            private readonly TimeframeSegmentDataSeries _parent;

            public int Count
            {
                get
                {
                    return this._parent._segments.Count;
                }
            }

            public object SyncRoot
            {
                get
                {
                    return ( ( ICollection ) this._parent._segments ).SyncRoot;
                }
            }

            public bool IsSynchronized
            {
                get
                {
                    return ( ( ICollection ) this._parent._segments ).IsSynchronized;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public bool IsFixedSize
            {
                get
                {
                    return false;
                }
            }

            public YValueList( TimeframeSegmentDataSeries parent )
            {
                this._parent = parent;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ( IEnumerator ) this.GetEnumerator();
            }

            public IEnumerator<double> GetEnumerator()
            {
                return this._parent._segments.Select<TimeframeDataSegment, double>( ( Func<TimeframeDataSegment, double> ) ( s => s.Y ) ).GetEnumerator();
            }

            bool IList.Contains( object value )
            {
                if ( !( value is double ) )
                {
                    return false;
                }

                return this.Contains( ( double ) value );
            }

            public bool Contains( double value )
            {
                return this._parent._segments.Select<TimeframeDataSegment, double>( ( Func<TimeframeDataSegment, double> ) ( s => s.Y ) ).Contains<double>( value );
            }

            int IList.IndexOf( object value )
            {
                if ( !( value is double ) )
                {
                    return -1;
                }

                return this.IndexOf( ( double ) value );
            }

            public int IndexOf( double value )
            {
                int count = this._parent._segments.Count;
                for ( int index = 0 ; index < count ; ++index )
                {
                    if ( this._parent._segments[ index ].Y.DoubleEquals( value ) )
                    {
                        return index;
                    }
                }
                return -1;
            }

            public double this[ int index ]
            {
                get
                {
                    return this._parent._segments[ index ].Y;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            object IList.this[ int index ]
            {
                get
                {
                    return ( object ) this[ index ];
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
    }
}
