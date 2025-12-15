//using DevExpress.Data.Linq.Helpers;
//using DevExpress.Xpf.Bars;
//using Ecng.Common;
//using SciChart.Charting.Common.Extensions;
//using SciChart.Charting.Model.DataSeries;
//using SciChart.Charting.Visuals.RenderableSeries;
//using SciChart.Charting.Visuals;
//using SciChart.Core.Framework;
//using SciChart.Data.Model;
//using SciChart.Data.Numerics.PointResamplers;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading;
//using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
//using SciChart.Charting2D.Interop;

//#nullable disable
//namespace StockSharp.Xaml.Charting;

//internal class TransactionDataSeries :
//  BindableObject100,
//  IDataSeries<DateTime, double>,
//  ISuspendable,
//  IDataSeries
//{
//    private string _seriesName;
//    private readonly OhlcDataSeries<DateTime, double> _candlesSeries;
//    private readonly SciList<TransactionDataSegment> _transacationData = new SciList<TransactionDataSegment>();
//    private readonly TransactionDataSeries.TransactionList _yValues;
//    private List<ChartDrawData.sTrade> _transactionBuffer = new List<ChartDrawData.sTrade>();
//    private bool _flushingBufferedTransactions;

//    public TransactionDataSeries( OhlcDataSeries<DateTime, double> candles )
//    {
//        this._candlesSeries = candles ?? throw new ArgumentNullException( nameof( candles ) );
//        this._yValues = new TransactionDataSeries.TransactionList( this );
//        this._candlesSeries.DataSeriesChanged += new EventHandler<DataSeriesChangedEventArgs>( this.CandlesSeries_OnDataSeriesChanged );
//    }

//    private IList<DateTime> _dates => this._candlesSeries.XValues;

//    public DataSeriesType DataSeriesType
//    {
//        get => ( DataSeriesType ) 10;
//    }

//    public event EventHandler<DataSeriesChangedEventArgs> DataSeriesChanged;

//    public object SyncRoot { get; } = new object( );

//    public ISciChartSurface ParentSurface { get; set; }

//    public bool AcceptsUnsortedData
//    {
//        get => true;
//        set
//        {
//        }
//    }

//    public bool IsSecondary => true;

//    public string SeriesName
//    {
//        get => this._seriesName;
//        set
//        {
//            this._seriesName = value;
//            this.OnDataSeriesChanged( ( DataSeriesUpdate ) 1 );
//        }
//    }

//    public bool HasValues => this._dates.Count > 0;
//    public int Count => this._dates.Count;

//    public IComparable YMin => this.YRange.Min;

//    public IComparable YMax => this.YRange.Max;

//    IComparable IDataSeries.XMin
//    {
//        get => this.XRange.Min;
//    }

//    IComparable IDataSeries.XMax
//    {
//        get => this.XRange.Max;
//    }

//    public bool IsFifo => false;

//    public bool IsSorted => true;

//    internal ISciList<TransactionDataSegment> Data
//    {
//        get
//        {
//            return ( ISciList<TransactionDataSegment> ) this._transacationData;
//        }
//    }

//    public Type XType => typeof( DateTime );

//    public Type YType => typeof( double );

//    public IComparable LatestYValue => ( IComparable ) null;

//    private IList<DateTime> XValues => this._dates;

//    IList IDataSeries.XValues
//    {
//        get => ( IList ) this.XValues;
//    }

//    IList<DateTime> IDataSeries<DateTime, double>.XValues
//    {
//        get => this.XValues;
//    }

//    IList IDataSeries.YValues
//    {
//        get => ( IList ) this._yValues;
//    }

//    IList<double> IDataSeries<DateTime, double>.YValues
//    {
//        get => ( IList<double> ) this._yValues;
//    }

//    public void Clear( )
//    {
//        lock ( this.SyncRoot )
//        {
//            this._transacationData.Clear( );
//            this._transactionBuffer.Clear( );
//            this.EnsureData( );
//            this.OnDataSeriesChanged( ( DataSeriesUpdate ) 3 );
//        }
//    }

//    private void CandlesSeries_OnDataSeriesChanged( object sender, DataSeriesChangedEventArgs e )
//    {
//        if ( ( e.DataSeriesUpdate & DataSeriesUpdate.DataSeriesCleared ) == DataSeriesUpdate.DataSeriesCleared )
//            this.Clear( );
//        this.EnsureData( );
//        this.AddOrUpdateBufferedTransactions( );
//    }

//    private void OnDataSeriesChanged( DataSeriesUpdate flags )
//    {
//        EventHandler<DataSeriesChangedEventArgs> dataSeriesChanged = this.DataSeriesChanged;
//        if ( dataSeriesChanged == null )
//            return;
//        dataSeriesChanged( ( object ) this, new DataSeriesChangedEventArgs( flags ) );
//    }

//    private void EnsureData( )
//    {
//        if ( !this._candlesSeries.IsSorted )
//            throw new NotSupportedException( "candle series is not sorted" );
//        lock ( this.SyncRoot )
//        {
//            if ( this._transacationData.Count == this._candlesSeries.Count )
//                return;
//            if ( this._transacationData.Count < this._candlesSeries.Count )
//            {
//                this._transacationData.AddRange( Enumerable.Range( 0, this._candlesSeries.Count - this._transacationData.Count ).Select<int, TransactionDataSegment>( TransactionDataSeries.SealClass01._function053 ?? ( TransactionDataSeries.SealClass01._function053 = new Func<int, TransactionDataSegment>( TransactionDataSeries.SealClass01._classInstance.Method03 ) ) ) );
//                return;
//            }
//        }
//        throw new InvalidOperationException( "changes were not synchronized" );
//    }

//    public void InvalidateParentSurface( RangeMode rangeMode )
//    {
//        this._candlesSeries.InvalidateParentSurface( rangeMode );
//    }

//    public IndexRange GetIndicesRange( IRange visibleRange )
//    {
//        return this._candlesSeries.GetIndicesRange( visibleRange );
//    }

//    IRange IDataSeries.GetWindowedYRange( IRange xRange )
//    {
//        IndexRange indicesRange = this.GetIndicesRange(xRange);
//        return !indicesRange.IsDefined ? ( IRange ) new DoubleRange( double.MinValue, double.MaxValue ) : this.GetWindowedYRange( indicesRange );
//    }
//    IRange IDataSeries.GetWindowedYRange( IndexRange indexRange, bool getPositiveRange )
//    {
//        return !indexRange.IsDefined ? ( IRange ) new DoubleRange( double.MinValue, double.MaxValue ) : this.GetWindowedYRange( indexRange );
//    }

//    IRange IDataSeries.GetWindowedYRange( IRange xRange, bool getPositiveRange )
//    {
//        return ( ( IDataSeries ) this ).GetWindowedYRange( xRange );
//    }

//    public IRange XRange
//    {
//        get
//        {
//            return !this._dates.Any<DateTime>( ) ? ( IRange ) new DoubleRange( double.MinValue, double.MaxValue ) : ( IRange ) new DateRange( this._dates[0], this._dates[this._dates.Count - 1] ).AsDoubleRange( );
//        }
//    }

//    public IRange YRange
//    {
//        get
//        {
//            if ( this._dates.Count == 0 )
//                return ( IRange ) new DoubleRange( double.MinValue, double.MaxValue );
//            double num1 = double.MaxValue;
//            double num2 = double.MinValue;
//            foreach ( TransactionDataSegment rvIvnoSs0Uo7ic5Jw in this._transacationData.Where<TransactionDataSegment>( TransactionDataSeries.SealClass01._function054 ?? ( TransactionDataSeries.SealClass01._function054 = new Func<TransactionDataSegment, bool>( TransactionDataSeries.SealClass01._classInstance.Method04 ) ) ) )
//            {
//                num1 = MathHelper.Min( num1, rvIvnoSs0Uo7ic5Jw.MaxValue( ) );
//                num2 = MathHelper.Max( num2, rvIvnoSs0Uo7ic5Jw.MinValue( ) );
//            }
//            return ( IRange ) new DoubleRange( num1, num2 );
//        }
//    }

//    public void AddOrUpdateTransaction( ChartDrawData.sTrade trans )
//    {
//        if ( ( trans != null ? ( trans.GetTransactionString( ).\u0023\u003DzCCMM80zDpO6N<char>( ) ? 1 : 0) : 1) != 0)
//      throw new ArgumentException( $"invalid transaction data: {trans}" );
//        lock ( this.SyncRoot )
//        {
//            lock ( this._candlesSeries.SyncRoot )
//            {
//                this._transactionBuffer.Add( trans );
//                this.AddOrUpdateBufferedTransactions( );
//            }
//        }
//    }

//    private void AddOrUpdateBufferedTransactions( )
//    {
//        bool flag = false;
//        lock ( this.SyncRoot )
//        {
//            lock ( this._candlesSeries.SyncRoot )
//            {
//                if ( this._transactionBuffer.Count == 0 || this._flushingBufferedTransactions )
//                    return;
//                TimeSpan? timeframe = this._candlesSeries.Timeframe;
//                if ( !timeframe.HasValue )
//                    return;
//                TimeSpan valueOrDefault = timeframe.GetValueOrDefault();
//                try
//                {
//                    this._flushingBufferedTransactions = true;
//                    this._candlesSeries.\u0023\u003DzGXoUV7Q\u0024_bM\u0024();
//                    this.EnsureData( );
//                    List<ChartDrawData.sTrade> zU3TaXfsList = new List<ChartDrawData.sTrade>();
//                    foreach ( ChartDrawData.sTrade zU3TaXfs in this._transactionBuffer )
//                    {
//                        int index = this.XValues.\u0023\u003Dzk3Bi7DnEdNPh<DateTime>( zU3TaXfs.Time, ( IComparer<DateTime> ) null );
//                        if ( index < 0 )
//                            index = ~index - 1;
//                        if ( index >= 0 )
//                        {
//                            DateTime date = this._dates[index];
//                            if ( zU3TaXfs.Time >= date + valueOrDefault )
//                            {
//                                zU3TaXfsList.Add( zU3TaXfs );
//                            }
//                            else
//                            {
//                                SciList<TransactionDataSegment> data = this._transacationData;
//                                int num = index;
//                                if ( data[num] == null )
//                                    data[num] = new TransactionDataSegment( date );
//                                this._transacationData[index].AddOrUpdate( zU3TaXfs );
//                                flag = true;
//                            }
//                        }
//                    }
//                    this._transactionBuffer = zU3TaXfsList;
//                }
//                finally
//                {
//                    this._flushingBufferedTransactions = false;
//                }
//            }
//        }
//        if ( !flag )
//            return;
//        this.OnDataSeriesChanged( ( DataSeriesUpdate ) 1 );
//    }

//    public double GetYMinAt( int index, double existingYMin )
//    {
//        return Math.Min( this._transacationData[index].MaxValue( ), existingYMin );
//    }

//    public double GetYMaxAt( int index, double existingYMax )
//    {
//        return Math.Max( this._transacationData[index].MinValue( ), existingYMax );
//    }

//    public IRange GetWindowedYRange(
//      IndexRange xIndexRange )
//    {
//        this.EnsureData( );
//        IndexRange g8Oq2rGx6KyfAreq = (IndexRange) xIndexRange.Clone();
//        if ( g8Oq2rGx6KyfAreq.Min < 0 )
//            g8Oq2rGx6KyfAreq.Min = 0;
//        if ( g8Oq2rGx6KyfAreq.Max >= this._transacationData.Count )
//            g8Oq2rGx6KyfAreq.Max = this._transacationData.Count - 1;
//        double num1;
//        double num2;
//        TransactionDataSegment.MinMax( this._transacationData.Skip<TransactionDataSegment>( g8Oq2rGx6KyfAreq.Min ).Take<TransactionDataSegment>( g8Oq2rGx6KyfAreq.Max - g8Oq2rGx6KyfAreq.Min + 1 ), out num1, out num2 );
//        return ( IRange ) new DoubleRange( num1, num2 );
//    }

//    public IPointSeries ToPointSeries(
//      ResamplingMode resamplingMode,
//      IndexRange pointRange,
//      int viewportWidth,
//      bool isCategoryAxis,
//      bool? dataIsDisplayedAs2D,
//      IRange visibleXRange,
//      IPointResamplerFactory factory,
//      object pointSeriesArg = null )
//    {
//        return !pointRange.IsDefined ? ( IPointSeries ) null : ( IPointSeries ) new TransactionDataSegmentPointSeries( this._transacationData.ItemsArray, pointRange, visibleXRange );
//    }

//    public bool IsSuspended
//    {
//        get
//        {
//            return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y( ( ISuspendable ) this );
//        }
//    }

//    public IUpdateSuspender SuspendUpdates( )
//    {
//        ISciChartSurface parentSurface = this.ParentSurface;
//        if ( parentSurface == null )
//            return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this );
//        Monitor.Enter( parentSurface.SyncRoot );
//        return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this, parentSurface.SyncRoot );
//    }

//    public void ResumeUpdates(
//      IUpdateSuspender suspender )
//    {
//        if ( suspender.\u0023\u003DzuWdUDFWIQOsx( ))
//      this.OnDataSeriesChanged( ( DataSeriesUpdate ) 3 );
//        if ( suspender.\u0023\u003DzFG_qiXECs10o( ) == null)
//      return;
//        Monitor.Exit( suspender.\u0023\u003DzFG_qiXECs10o( ) );
//    }

//    public void DecrementSuspend( )
//    {
//    }

//    void IDataSeries<DateTime, double>.Append(
//      DateTime dt,


//      params double[ ] yValues )
//    {
//        throw new NotImplementedException( );
//    }

//    public void Append( IEnumerable<DateTime> dtList, params IEnumerable<double>[ ] yValues )
//    {
//        throw new NotImplementedException( );
//    }

//    [Obsolete( "IsAttached is obsolete because there is no DataSeriesSet now" )]
//    bool IDataSeries.IsAttached
//    {
//        get => throw new NotImplementedException( );
//    }

//    int? IDataSeries.FifoCapacity
//    {
//        get => throw new NotImplementedException( );
//        set => throw new NotImplementedException( );
//    }

//    void IDataSeries<DateTime, double>.Remove(
//      DateTime x )
//    {
//        throw new NotImplementedException( );
//    }

//    void IDataSeries<DateTime, double>.RemoveAt(
//      int index )
//    {
//        throw new NotImplementedException( );
//    }

//    void IDataSeries<DateTime, double>.RemoveRange(
//      int startIndex,
//      int count )
//    {
//        throw new NotImplementedException( );
//    }

//    IDataSeries<DateTime, double> IDataSeries<DateTime, double>.Clone( )
//    {
//        throw new NotImplementedException( );
//    }

//    int IDataSeries.FindIndex(
//      IComparable x,
//      SearchMode searchMode )
//    {
//        throw new NotImplementedException( );
//    }

//    int IDataSeries.FindClosestPoint(
//      IComparable x,
//      IComparable y,
//      double xyScaleRatio,
//      double maxXDistance )
//    {
//        throw new NotImplementedException( );
//    }

//    int IDataSeries.FindClosestLine(
//      IComparable x,
//      IComparable y,
//      double xyScaleRatio,
//      double maxXDistance,
//      LineDrawMode drawNanAs )
//    {
//        throw new NotImplementedException( );
//    }

//    public virtual void OnBeginRenderPass( )
//    {
//    }

//    IPointSeries IDataSeries.ToPointSeries(
//      IList column,
//      ResamplingMode resamplingMode,
//      IndexRange pointRange,
//      int viewportWidth,
//      bool isCategoryAxis )
//    {
//        throw new NotImplementedException( );
//    }

//    HitTestInfo IDataSeries.ToHitTestInfo(
//    int index )
//    {
//        return HitTestInfo.\u0023\u003Dzz_6Dy9M\u003D;
//    }

//    [Serializable]
//    private sealed class SealClass01
//    {
//        public static readonly TransactionDataSeries.SealClass01 _classInstance = new TransactionDataSeries.SealClass01();
//        public static Func<int, TransactionDataSegment> _function053;
//        public static Func<TransactionDataSegment, bool> _function054;

//        internal TransactionDataSegment Method03(
//          int _param1 )
//        {
//            return ( TransactionDataSegment ) null;
//        }

//        internal bool Method04(
//          TransactionDataSegment _param1 )
//        {
//            return _param1 != null;
//        }
//    }

//    private sealed class TransactionList :
//      IList,
//      IEnumerable,
//      ICollection,
//      IList<double>,
//      ICollection<double>,
//      IEnumerable<double>
//    {

//        private readonly TransactionDataSeries transactionDataSeries;

//        public TransactionList( TransactionDataSeries _param1 )
//        {
//            this.transactionDataSeries = _param1;
//        }

//        public int Count => this.transactionDataSeries._transacationData.Count;

//        public object SyncRoot => ( ( ICollection ) this.transactionDataSeries._transacationData ).SyncRoot;

//        public bool IsSynchronized
//        {
//            get => ( ( ICollection ) this.transactionDataSeries._transacationData ).IsSynchronized;
//        }

//        public bool IsReadOnly => true;

//        public bool IsFixedSize => false;

//        IEnumerator IEnumerable.GetEnumerator( )
//        {
//            return ( IEnumerator ) this.GetEnumerator( );
//        }

//        public IEnumerator<double> GetEnumerator( )
//        {
//            return this.transactionDataSeries.Data.Select<TransactionDataSegment, double>( TransactionDataSeries.TransactionList.SealClass01._function055 ?? ( TransactionDataSeries.TransactionList.SealClass01._function055 = new Func<TransactionDataSegment, double>( TransactionDataSeries.TransactionList.SealClass01._classInstance.Method01 ) ) ).GetEnumerator( );
//        }

//        bool IList.Contains( object _param1 )
//        {
//            return _param1 is double num && this.Contains( num );
//        }

//        public bool Contains( double _param1 )
//        {
//            return this.transactionDataSeries._transacationData.Select<TransactionDataSegment, double>( TransactionDataSeries.TransactionList.SealClass01._function056 ?? ( TransactionDataSeries.TransactionList.SealClass01._function056 = new Func<TransactionDataSegment, double>( TransactionDataSeries.TransactionList.SealClass01._classInstance.Method02 ) ) ).Contains<double>( _param1 );
//        }

//        int IList.IndexOf( object _param1 )
//        {
//            return _param1 is double num ? this.IndexOf( num ) : -1;
//        }

//        public int IndexOf( double _param1 )
//        {
//            int count = this.transactionDataSeries._transacationData.Count;
//            for ( int index = 0; index < count; ++index )
//            {
//                if ( this.transactionDataSeries._transacationData[index].Y.DoubleEquals( _param1 ) )
//                    return index;
//            }
//            return -1;
//        }

//        public double this[int _param1]
//        {
//            get => this.transactionDataSeries._transacationData[_param1].Y;
//            set => throw new NotImplementedException( );
//        }

//        object? IList.this[int index]
//        {
//            get => this[index];
//            set
//            {
//                throw new NotImplementedException( );
//            }
//        }



//        void ICollection<double>.CopyTo(
//          double[ ] _param1,
//          int _param2 )
//        {
//            throw new NotImplementedException( );
//        }

//        void ICollection.CopyTo( Array _param1, int _param2 )
//        {
//            throw new NotImplementedException( );
//        }

//        void ICollection<double>.Add(
//          double _param1 )
//        {
//            throw new NotImplementedException( );
//        }

//        void IList<double>.Insert(
//          int _param1,
//          double _param2 )
//        {
//            throw new NotImplementedException( );
//        }

//        bool ICollection<double>.Contains(
//          double _param1 )
//        {
//            throw new NotImplementedException( );
//        }

//        int IList.Add( object _param1 )
//        {
//            throw new NotImplementedException( );
//        }

//        void IList.Insert( int _param1, object _param2 )
//        {
//            throw new NotImplementedException( );
//        }

//        void IList.Remove( object _param1 )
//        {
//            throw new NotImplementedException( );
//        }

//        public void Clear( ) => throw new NotImplementedException( );

//        public void RemoveAt( int _param1 ) => throw new NotImplementedException( );

//        public bool Remove( double item )
//        {
//            throw new NotImplementedException( );
//        }

//        [Serializable]
//        private sealed class SealClass01
//        {
//            public static readonly TransactionDataSeries.TransactionList.SealClass01 _classInstance = new TransactionDataSeries.TransactionList.SealClass01();
//            public static Func<TransactionDataSegment, double> _function055;
//            public static Func<TransactionDataSegment, double> _function056;

//            internal double Method01(
//              TransactionDataSegment _param1 )
//            {
//                return _param1.Y;
//            }

//            internal double Method02(
//              TransactionDataSegment _param1 )
//            {
//                return _param1.Y;
//            }
//        }
//    }
//}
