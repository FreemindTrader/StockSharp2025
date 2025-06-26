using Ecng.Collections;
using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockSharp.BusinessEntities;

using StockSharp.Algo.Candles;
using fx.TimePeriod;
using fx.Collections;
using System.Diagnostics;
using StockSharp.Algo;
using Ecng.Configuration;
using Ecng.Logging;

#pragma warning disable 168

namespace fx.Bars
{
    [Serializable]
    public sealed class fxHistoricBarsRepo : BaseLogReceiver, IHistoricBarsRepo, IDisposable
    {
        public event EventHandler< HistoricBarsUpdateEventArg > HistoricBarUpdateEvent;

        //bool _noNeedToShowUI = false;

        private SBarList                               _barList =null;
        
        private IHewManager                            _hews = null;
        private Security                               _security;
        private DateTime                               _lastConfirmedBarTime;
        private DateTime                               _lastActiveBarTime;        
        private DateTime                               _lastDataUpdate = DateTime.MinValue;
        private bool                                   _doneLoading = false;
        private TimeSpan?                              _period;
        private Stopwatch                              _stopWatch = new Stopwatch();
        private int                                    _fifoCapacity = -1;

        private IIndicatorManager _indicators = null;

        //private PooledList< SRlevel > _supportResistanceLevels;

        private object _supportResistanceLevelsLock = new object( );

        private PooledList< SRlevel > _dailyPivotLevels;

        private PooledList< SRlevel > _weeklyPivotLevels;

        private PooledList< SRlevel > _monthlyPivotLevels;

        public fxHistoricBarsRepo( Security security, TimeSpan period, IHewManager hew, IIndicatorManager mgr )
        {
            _security = security;

            _hews = hew;

            _indicators = mgr;
            _period = period;

            _indicators.Initialize( );

            Name = "fxHistoricBarsRepo";

            ServicesRegistry.LogManager.Sources.Add( this );
        }

        public double PipsAllowance()
        {
            var pips = ( double ) Security.PriceStep.Value * PipsError( Security );

            return pips;
        }

        public double PipsError( Security sec )
        {
            if ( sec.Type == StockSharp.Messages.SecurityTypes.Currency )
            {
                return 4;
            }
            else if ( sec.Type == StockSharp.Messages.SecurityTypes.Index )
            {
                return 10;
            }

            return 4;
        }


        public fxHistoricBarsRepo( Security security, TimeSpan period, IHewManager hew, IIndicatorManager mgr, int fifoCapacity )
        {
            _security     = security;
            _fifoCapacity = fifoCapacity;
            _indicators   = mgr;
            _period       = period;
            _hews          = hew;

            _indicators.Initialize();

            Name     = "fxHistoricBarsRepo";

            ServicesRegistry.LogManager.Sources.Add( this );
        }


        public bool DoneLoading
        {
            get
            {
                return _doneLoading;
            }
        }

        public Security Security
        {
            get
            {
                return _security;
            }
        }

        public event SupportResistanceLevelsUpdateDelege DailyPivotsUpdateEvent;

        public event SupportResistanceLevelsUpdateDelege WeeklyPivotsUpdateEvent;

        public event SupportResistanceLevelsUpdateDelege MonthlyPivotsUpdateEvent;

        public IList< SRlevel > DailyPivots
        {
            get
            {
                return _dailyPivotLevels;
            }

            set
            {
                _dailyPivotLevels = new PooledList< SRlevel >( value.Count );

                foreach( var level in value )
                {
                    _dailyPivotLevels.Add( level );
                }

                if( DailyPivotsUpdateEvent != null )
                {
                    DailyPivotsUpdateEvent( this, _dailyPivotLevels );
                }
            }
        }

        public IList< SRlevel > WeeklyPivots
        {
            get
            {
                return _weeklyPivotLevels;
            }

            set
            {
                _weeklyPivotLevels = new PooledList< SRlevel >( value.Count );

                foreach( var level in value )
                {
                    _weeklyPivotLevels.Add( level );
                }

                if( WeeklyPivotsUpdateEvent != null )
                {
                    WeeklyPivotsUpdateEvent( this, _weeklyPivotLevels );
                }
            }
        }

        public IList< SRlevel > MonthlyPivots
        {
            get
            {
                return _monthlyPivotLevels;
            }

            set
            {
                _monthlyPivotLevels = new PooledList< SRlevel >( value.Count );

                foreach( var level in value )
                {
                    _monthlyPivotLevels.Add( level );
                }

                if( MonthlyPivotsUpdateEvent != null )
                {
                    MonthlyPivotsUpdateEvent( this, _monthlyPivotLevels );
                }
            }
        }

        public void ClearMonthlyPivots( )
        {
            if ( _monthlyPivotLevels  != null )
            {
                _monthlyPivotLevels.Clear( );

                if ( MonthlyPivotsUpdateEvent != null )
                {
                    MonthlyPivotsUpdateEvent( this, _monthlyPivotLevels );
                }
            }
            
        }

        public void ClearWeeklyPivots( )
        {
            if ( _weeklyPivotLevels != null )
            {
                _weeklyPivotLevels.Clear( );

                if ( WeeklyPivotsUpdateEvent != null )
                {
                    WeeklyPivotsUpdateEvent( this, _weeklyPivotLevels );
                }
            }            
        }

        public void ClearDailyPivots( )
        {
            if ( _dailyPivotLevels != null )
            {
                _dailyPivotLevels.Clear( );

                if ( DailyPivotsUpdateEvent != null )
                {
                    DailyPivotsUpdateEvent( this, _dailyPivotLevels );
                }
            }
            
        }

        public IIndicatorManager Indicators
        {
            get
            {
                return _indicators;
            }
        }


        public DateTime GetNextPeriodTime( DateTime confirmedBarTime, TimeSpan period )
        {            
            if ( period == TimeSpan.FromDays( 30 ) )
            {
                var tz1              = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
                var newYorkTime      = TimeZoneInfo.ConvertTimeFromUtc( confirmedBarTime, tz1 );

                var firstOfNextMonth = new DateTime(newYorkTime.Year, newYorkTime.Month, 1).AddMonths(1);
                var lastOfThisMonth  = firstOfNextMonth.AddDays(-1);

                var newGMTTime       = TimeZoneInfo.ConvertTimeToUtc( lastOfThisMonth, tz1 );

                return newGMTTime;
            }
            else if ( period == TimeSpan.FromDays( 7 ) || period == TimeSpan.FromDays( 1 ) )
            {
                var tz1              = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
                var newYorkTime      = TimeZoneInfo.ConvertTimeFromUtc( confirmedBarTime, tz1 );
                var newNYTime        = newYorkTime + period;

                var newGMTTime       = TimeZoneInfo.ConvertTimeToUtc( newNYTime, tz1 );

                return newGMTTime;
            }
            

            return confirmedBarTime + period;
        }

        public ref SBar AddSingleCandle( Candle candle )
        {
            DataBarUpdateType updateType = DataBarUpdateType.CurrentBarUpdate;            

            if( _lastActiveBarTime < _lastConfirmedBarTime )
            {
                _lastActiveBarTime = _lastConfirmedBarTime;
            }

            var newBarTime      = candle.OpenTime.UtcDateTime;
            var barPeriod       = (TimeSpan) candle.Arg;

            var currentBarIndex = _barList.Count - 1;
            var currentBarTime  = currentBarIndex > 0 ? _barList.LastBarTime : newBarTime;

            /* -------------------------------------------------------------------------------------------
             * 
             *  There are only two possibilities.
             *  1) We have a new Candle comes in which is next candle 
             *  2) We are updating current candle
             *  
             * -------------------------------------------------------------------------------------------
             */

            // The candle is still updating not finished yet and should have been added to _databars
            if ( newBarTime == _lastActiveBarTime )
            {
                ref SBar sameBar = ref _barList.UpdateLastCandle( candle );
            }
            else if ( newBarTime == GetNextPeriodTime( _lastConfirmedBarTime, barPeriod ) )
            {
                ref SBar newBar = ref _barList.AddCandle( candle );
                _lastActiveBarTime = candle.OpenTime.UtcDateTime;
            }
            else if( newBarTime > GetNextPeriodTime( _lastConfirmedBarTime, barPeriod ) )
            {
                // Here we have a bar which is not continus, this is common in 1 minute bar
                if( barPeriod > TimeSpan.FromMinutes( 1 ) && currentBarIndex  > 0 )
                {
                    string msg = string.Format( "[0] Missing some databars between {1} and {2}", barPeriod.ToShortForm( ), _lastConfirmedBarTime, candle.OpenTime.UtcDateTime );
                    this.LogWarning( msg );
                }

                ref SBar newBar = ref _barList.AddCandle( candle );
                _lastActiveBarTime = candle.OpenTime.UtcDateTime;
            }
            else
            {
                return ref SBar.EmptySBar;
            }
                
            if ( candle.State == StockSharp.Messages.CandleStates.Finished )
            {
                updateType = DataBarUpdateType.HistoryUpdate;

                // This current Null bar has become a Confirmed bar.
                _lastConfirmedBarTime = newBarTime;                                        
            }
            else
            {
                updateType = DataBarUpdateType.CurrentBarUpdate;
            }

            _lastDataUpdate = DateTime.Now;

            HistoricBarUpdateEvent?.Invoke( this, new HistoricBarsUpdateEventArg( updateType, _barList.LastBarIndex, _barList.LastBarIndex ) );


            return ref _barList[ _barList.Count - 1 ];                        
        }

        public ( uint, uint ) AddReplaceCandlesRange( Security security, List< Candle > candles, TimeSpan period, int waveScenarioNo, bool restoreWave = false )
        {
            if ( _barList == null )
            {
                ReloadAllCandles( security, candles, period, waveScenarioNo );
                return ( 0, _barList.LastBarIndex );
            }

            if( _lastActiveBarTime < _lastConfirmedBarTime )
            {
                _lastActiveBarTime = _lastConfirmedBarTime;
            }
            
            (uint begin, uint end ) range = _barList.AddReplaceCandles( candles );

            if ( restoreWave )
            {
                _hews.RestoreElliottWaves( _barList, period, waveScenarioNo );
            }
            
            if ( range == default ) return range;

            _lastConfirmedBarTime = _barList.LastBarTime;
            _lastActiveBarTime    = _barList.LastBarTime;            

            HistoricBarUpdateEvent?.Invoke( this, new HistoricBarsUpdateEventArg( DataBarUpdateType.HistoryUpdate, range.begin, range.end ) );

            return range;
        }

        public int ReloadAllCandles( Security security, List< Candle > candles, TimeSpan period, int waveScenarioNo )
        {            
            var updateType = DataBarUpdateType.Initial;            
            
            string command = string.Format( "Fetch {0} bar Infos from InfluxDB ", FinancialHelper.GetPeriodString( period ) );

            var symbolMgr  = ConfigManager.GetService<ISymbolsMgr>();

            if ( symbolMgr == null )
            {
                this.LogError( "SymbolMgr is not registered" );

                return 0;
            }

            _hews = symbolMgr.GetOrCreateAdvancedAnalysis( security ).HewManager;
            var undoArea = _hews.GetSelectedUndoRedoArea( period );

            undoArea.Start( command );

            _stopWatch.Restart();

            if ( candles.Count > 0 )
            {                
                int offerId = symbolMgr.GetOfferId( security );

                BuildElliottWaveDictionary( security.Code, offerId, period );

                var waveDates = _hews.GetWaveDatesList( period );
                                
                _barList = new SBarList( security, period, candles, waveDates );

                _barList.CopyCandlesSetSession( candles );

                _barList.AllocateWaves( waveDates );

                _hews.RestoreElliottWaves( _barList, period, waveScenarioNo );

                _lastConfirmedBarTime = _barList.LastBarTime;
            }                                  
            
            _stopWatch.Stop();

            string msg = string.Format( "{0} Datebars ----> ReloadAllCandles, takes {1} ms", period.ToShortForm(), _stopWatch.Elapsed.TotalMilliseconds );
            this.LogWarning( msg );

            undoArea.Commit( );

            _lastDataUpdate = DateTime.Now;
            
            HistoricBarUpdateEvent?.Invoke( this, new HistoricBarsUpdateEventArg( updateType, 0, _barList.LastBarIndex ) );

            return (int) _barList.LastBarIndex;
        }

        public int ReloadAllCandlesNoWaves( Security security, List<Candle> candles, TimeSpan period, int waveScenarioNo )
        {
            //_noNeedToShowUI = true;

            var updateType = DataBarUpdateType.Initial;

            string command = string.Format( "Fetch {0} bar Infos from InfluxDB ", FinancialHelper.GetPeriodString( period ) );

            var symbolMgr  = ConfigManager.GetService<ISymbolsMgr>();

            if ( symbolMgr == null )
            {
                this.LogError( "SymbolMgr is not registered" );

                return 0;
            }

            _hews = symbolMgr.GetOrCreateAdvancedAnalysis( security ).HewManager;
            var undoArea = _hews.GetSelectedUndoRedoArea( period );

            undoArea.Start( command );

            _stopWatch.Restart();

            if ( candles.Count > 0 )
            { 
                _barList = new SBarList( security, period, candles, null );

                _barList.CopyCandlesSetSession( candles );

                _lastConfirmedBarTime = _barList.LastBarTime;
            }

            _stopWatch.Stop();

            string msg = string.Format( "ReloadAllCandles, takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );
            this.LogWarning( msg );

            undoArea.Commit();

            _lastDataUpdate = DateTime.Now;

            HistoricBarUpdateEvent?.Invoke( this, new HistoricBarsUpdateEventArg( updateType, 0, _barList.LastBarIndex ) );

            return ( int ) _barList.LastBarIndex;
        }




        private int _databarCacheSize = 0;                

        private void UpdateDatabarsCachePreAdd( int i, ref SBar tmpBar )
        {
            if( i >= _databarCacheSize )
            {
                _databarCacheSize += _databarCacheSize;
            }            

                     
        }

        public void CreateDataBarCacheFriendlyStorage( Security security, TimeSpan period, int miniBarCount )
        {
            _databarCacheSize = FinancialHelper.CalculateStorageSize( period, miniBarCount );

            _barList = new SBarList( security, period, _databarCacheSize );
        }

        public void CreateDataBarCacheFriendlyStorage( Security security, TimeSpan period, DateTime beginDate, DateTime endDate )
        {
            var dateDiff      = endDate - beginDate;

            var totalMinutes  = dateDiff.TotalMinutes;

            _databarCacheSize = (int) ( totalMinutes / period.TotalMinutes + 3 );            
            
            _barList         = new SBarList( security, period, _databarCacheSize );
        }

        

        public bool IsOutsideBar( int selectedBarIndex )
        {
            if( selectedBarIndex < 1 )
            {
                return false;
            }

            if( ( _barList.High( selectedBarIndex  ) >= _barList[ selectedBarIndex - 1 ].High ) && ( _barList.Low( selectedBarIndex ) < _barList.Low( selectedBarIndex - 1 ) ) )
            {
                return true;
            }

            if( ( _barList.High( selectedBarIndex  ) > _barList[ selectedBarIndex - 1 ].High ) && ( _barList.Low( selectedBarIndex ) <= _barList.Low( selectedBarIndex - 1 ) ) )
            {
                return true;
            }

            return false;
        }

        public bool IsOutsideBar( long rawBarTime )
        {
            var selectedBarIndex = GetIndexByTime( rawBarTime );

            if( selectedBarIndex < 1 )
            {
                return false;
            }

            return ( IsOutsideBar( selectedBarIndex ) );
        }

        public bool IsOutsideBar( int firstBarIndex, int secondBarIndex )
        {
            if( ( firstBarIndex < 0 ) || ( secondBarIndex < 0 ) || ( firstBarIndex >= secondBarIndex ) )
            {
                return false;
            }

            if( ( _barList.High(  secondBarIndex  ) >= _barList.High(  firstBarIndex  ) ) && ( _barList.Low(  secondBarIndex  ) < _barList.Low(  firstBarIndex  ) ) )
            {
                return true;
            }

            if( ( _barList.High(  secondBarIndex  ) > _barList.High(  firstBarIndex  ) ) && ( _barList.Low(  secondBarIndex  ) <= _barList.Low(  firstBarIndex  ) ) )
            {
                return true;
            }

            return false;
        }

        

        

        
        

        

        public ref SBar Current
        {
            get
            {
                if ( _barList.Count > 0 )
                {
                    return ref _barList[ _barList.Count - 1 ];
                }
                else
                {
                    return ref SBar.EmptySBar;
                }                
            }
        }

        //public SBar LastStableBar
        //{
        //    get
        //    {
        //        return ( _bars.Count > 1 ) ? _bars.LastStableBar : null;
        //    }
        //}

        public int GetClosestLeftTurningBar( int index )
        {
            if( index < 0 && index >= TotalBarCount )
            {
                return -1;
            }

            for( int i = index; i > -1; i-- )
            {
                ref SBar bar = ref _barList[i];

                if( bar.IsWavePeak( ) || bar.IsWaveTrough( ) || bar.IsGannPeak( ) || bar.IsGannTrough( ) )
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetClosestRightTurningBar( int index )
        {
            if( index < 0 && index >= TotalBarCount )
            {
                return -1;
            }

            for( int i = index; i < TotalBarCount; i++ )
            {
                ref SBar bar = ref _barList[i];

                if( bar.IsWavePeak( ) || bar.IsWaveTrough( ) || bar.IsGannPeak( ) || bar.IsGannTrough( ) )
                {
                    return i;
                }
            }

            return -1;
        }

        //public IReadOnlyList< SBar > ImmutableBarsView
        //{
        //    get
        //    {
        //        return _bars.AsReadOnly();
        //    }
        //}

        public ref SBar NullBar
        {
            get
            {
                return ref _barList[ _barList.Count - 1 ];
            }
        }

        public DateTime? LastBarTime
        {
            get
            {
                if ( _barList != null && _barList.Count > 0 )
                {
                    return _barList[ _barList.Count - 1 ].BarTime;
                }

                return null;
            }
        }

        public TimeSpan? Period
        {
            get
            {
                return _period;
            }
        }

        public DateTime? FirstBarTime
        {
            get
            {
                if ( _barList != null && _barList.Count > 0 )
                {
                    return _barList[ 0 ].BarTime;
                }

                return null;                
            }
        }

        // This property is used to resemble Metatrader's language.
        public int Bars
        {
            get
            {
                return _barList.Count;
            }
        }

        public int TotalBarCount
        {
            get
            {
                if ( _barList == null ) return 0;
                return _barList.Count;
            }
        }

        private int _confirmedBarCount = 0;

        public int ConfirmedBarCount
        {
            get
            {
                return _confirmedBarCount;
            }
        }

        private DateTime LastDataUpdate
        {
            get
            {
                return _lastDataUpdate;
            }
        }

        public SBarList MainDataBars
        {
            get
            {
                return _barList;
            }

            
        }

        public bool CheckBars()
        {
            for ( int i = 0; i < _barList.Count; i++ )
            {
                if ( _barList[ i ].BarIndex != i )
                {
                    throw new InvalidProgramException( );
                }
            }

            return true;
        }

        public ref SBar GetBarByIndex( int index )
        {
            if( index < 0 || index >= _barList.Count )
            {
                return ref SBar.EmptySBar;
            }            

            return ref _barList[ index ];
        }

        public ref SBar GetBarByIndex( uint index )
        {
            if ( index < 0 || index >= _barList.Count )
            {
                return ref SBar.EmptySBar;
            }            

            return ref _barList[ index ];
        }

        public ref SBar GetBarByIndex( long index )
        {
            if( index < 0 || index >= _barList.Count )
            {
                return ref SBar.EmptySBar;
            }            

            return ref _barList[ (int) index ];
        }

        public ref SBar GetBarByTime( DateTime barTime )
        {                                    
            return ref _barList.GetBarByTime( barTime );
        }

        public ref SBar GetBarByTime( long rawBarTime )
        {
            return ref _barList.GetBarByTime( rawBarTime );
        }        

        public ref SBar this[ int index ]
        {
            get
            {
                return ref GetBarByIndex( index );
            }
        }

        public ref SBar this[ uint index ]
        {
            get
            {
                return ref GetBarByIndex( index );
            }
        }

        public int iLowest( int begin, int end )
        {
            if( ( begin < 0 ) || ( end >= _barList.Count ) )
            {
                return -1;
            }                       

            double lowest = double.MaxValue;
            int index = -1;

            for( int i = begin; i <= end; i++ )
            {
                if( _barList.Low(  i  ) < lowest )
                {
                    lowest = _barList.Low(  i  );
                    index = i;
                }
            }

            return index;
        }


        /// <summary>
        /// This is the scenario
        /// We want to find the lowest within a Range and advance one Bar by a time.
        /// 
        /// B1 B2 B3 B4 B5 B6 B7 B8 B9 B10 B11 B12 B13
        /// (         L                  )
        ///    (      L                     N )   - In this case, we only need to check the last N to see if it is lower than L
        ///        (  L                         N )
        ///           (                              ) - In this case, L is no longer in the range, we will have to loop thru all the data to find the lowest.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="localLowIndex"></param>
        /// <returns></returns>
        public int iLowest1MoreBar( int begin, int end, int localLowIndex)
        {
            if ( ( begin < 0 ) || ( end >= _barList.Count ) )
            {
                return -1;
            }

            if ( localLowIndex > -1 )
            {
                double lowest = _barList.Low(  localLowIndex  );
                int index = localLowIndex;

                if ( begin < localLowIndex && localLowIndex < end )
                {
                    if ( _barList.Low(  end  ) <= lowest )
                    {
                        return end;
                    }
                }
            }            

            return iLowest( begin, end );
        }

        
        

        public int iHighest( int begin, int end )
        {
            if ( ( begin < 0 ) || ( end >= _barList.Count ) )
            {
                return -1;
            }

            double highest = double.MinValue;
            int index = -1;

            for ( int i = begin; i <= end; i++ )
            {
                if ( _barList.High(  i  ) > highest )
                {
                    highest = _barList.High(  i  );
                    index = i;
                }
            }

            return index;
        }

        public int iHighest1MoreBar( int begin, int end, int localHighIndex )
        {
            if ( ( begin < 0 ) || ( end >= _barList.Count ) )
            {
                return -1;
            }

            if ( localHighIndex > -1 )
            {
                double highest = _barList.High(  localHighIndex  );
                int index = localHighIndex;

                if ( begin < localHighIndex && localHighIndex < end )
                {
                    if ( _barList.High(  end  ) >= highest )
                    {
                        return end;
                    }
                }
            }            

            return iHighest( begin, end );
        }

        public double GetHighest( long start, long end )
        {
            double highest = double.MinValue;

            for( long i = start; i <= end; i++ )
            {
                ref SBar bar = ref _barList[ (int) i ];

                if( bar != SBar.EmptySBar )
                {
                    double currentHigh = bar.High;

                    if( currentHigh > highest )
                    {
                        highest = currentHigh;
                    }
                }
            }

            return highest;
        }

        public double GetLowest( long start, long end )
        {
            double lowest = double.MaxValue;

            for( long i = start; i <= end; i++ )
            {
                double currentLow = _barList[ (int) i ].Low;

                if( currentLow < lowest )
                {
                    lowest = currentLow;
                }
            }

            return lowest;
        }

        public double[ ] GetValues( DataBarProperty valueEnum )
        {
            return GetValues( valueEnum, 0, int.MaxValue );
        }

        public double[ ] GetValues( DataBarProperty valueEnum, int startingIndex, int indexCount )
        {
            double[ ] result;

            int count = indexCount;

            if( count == 0 )
            {
                count = _barList.Count - startingIndex;
            }

            result = new double[ Math.Min( count, _barList.Count ) ];

            int endindex;

            if( ( startingIndex + count ) < _barList.Count )
            {
                endindex = startingIndex + count;
            }
            else
            {
                endindex = _barList.Count - 1;
            }

            Parallel.For( startingIndex, endindex,
            k =>
            {
                ref SBar bar = ref _barList[ k ];
                result[ k - startingIndex ] = bar.GetValue( valueEnum );
            } );

            return result;
        }

        public IList< double > GetRealBodyAsPips( )
        {
            var output = new PooledList< double >( );

            if( _barList.Count > 0 )
            {
                foreach ( var bar in _barList )
                {
                    output.Add( bar.RealBodyAsPip );
                }                
            }

            return output;
        }

        public IList< double > GetLowerShadowAsPips( )
        {
            var output = new PooledList< double >( );

            if ( _barList.Count > 0 )
            {
                foreach ( var bar in _barList )
                {
                    output.Add( bar.LowerShadowLengthAsPip );
                }
            }

            return output;            
        }

        public IList< double > GetUpperShadowAsPips( )
        {
            var output = new PooledList< double >( );

            if ( _barList.Count > 0 )
            {
                foreach ( var bar in _barList )
                {
                    output.Add( bar.UpperShadowLengthAsPip );
                }
            }

            return output;            
        }

        public IList< double > GetRangeAsPips( )
        {
            var output = new PooledList< double >( );

            if ( _barList.Count > 0 )
            {
                foreach ( var bar in _barList )
                {
                    output.Add( bar.CandleLengthAsPip );
                }
            }

            return output;            
        }

        public double GetDataBarSubset( DataBarProperty valueEnum, int startingIndex )
        {
            ref SBar bar = ref _barList[ startingIndex ];
            return ( ( double )bar.GetValue( valueEnum ) );
        }

        public IList< double > GetDataBarSubset( DataBarProperty valueEnum )
        {
            return GetDataBarSubset( valueEnum, 0, int.MaxValue );
        }

        //public long[ ] GetTimeForBars( int startingIndex, int indexCount )
        //{
        //    long[ ] result;

        //    int count = indexCount;
        //    int barCount = this.TotalBarCount;

        //    if( startingIndex < 0 )
        //    {
        //        startingIndex = 0;
        //    }

        //    if( count == 0 )
        //    {
        //        count = barCount - startingIndex;
        //    }

        //    if( ( count == barCount ) || ( ( startingIndex == 0 ) && ( count > barCount ) ) )
        //    {
        //        return _rawBarTimeArray;
        //    }
        //    else
        //    {
        //        result = new long[ Math.Min( count, barCount ) ];

        //        long endindex = startingIndex + count;

        //        if( endindex >= _bars.Count )
        //        {
        //            endindex = _bars.Count;
        //        }

        //        Array.Copy( _rawBarTimeArray, startingIndex, result, 0, endindex - startingIndex );
        //    }

        //    return result;
        //}

        public IList< double > GetDataBarSubset( DataBarProperty valueEnum, int startingIndex, int indexCount )
        {
            if( indexCount < 0 )
            {
                return null;
            }

            PooledList< double > result = new PooledList<double>();

            int count = indexCount;
            int barCount = TotalBarCount;

            if( startingIndex < 0 )
            {
                startingIndex = 0;
            }

            if( count == 0 )
            {
                count = barCount - startingIndex;
            }

            long endindex = startingIndex + count;

            if( endindex >= _barList.Count )
            {
                endindex = _barList.Count;
            }

            switch( valueEnum )
            {
                case DataBarProperty.Close:
                    {
                        for ( int i = startingIndex; i < ( int ) ( endindex - startingIndex ); i++ )
                        {
                            result.Add( _barList.Close( i ) );
                        }
                    }
                    
                    break;

                case DataBarProperty.Open:
                    {
                        for ( int i = startingIndex; i < ( int ) ( endindex - startingIndex ); i++   )
                        {
                            result.Add( _barList.Open(  i  ) );
                        }
                    }
                    break;

                case DataBarProperty.High:
                    {
                        for ( int i = startingIndex; i < ( int ) ( endindex - startingIndex ); i++ )
                        {
                            result.Add( _barList.High(  i  ) );
                        }
                    }
                    break;

                case DataBarProperty.Low:
                    {
                        for ( int i = startingIndex; i < ( int ) ( endindex - startingIndex ); i++ )
                        {
                            result.Add( _barList.Low(  i  ) );
                        }
                    }
                    break;

                case DataBarProperty.Volume:
                    {
                        for ( int i = startingIndex; i < ( int ) ( endindex - startingIndex ); i++ )
                        {
                            result.Add( _barList.Volume(  i  ) );
                        }
                    }
                    break;
                
                default:
                    throw new NotImplementedException( valueEnum.ToString( ) );
            }

            return result;
        }

        //public IList<long > GetDataBarTime( DataBarProperty valueEnum, int startingIndex, int indexCount )
        //{
        //    if ( indexCount < 0 )
        //    {
        //        return null;
        //    }

        //    ArraySegment< long > result;

        //    int count = indexCount;
        //    int barCount = this.TotalBarCount;

        //    if ( startingIndex < 0 )
        //    {
        //        startingIndex = 0;
        //    }

        //    if ( count == 0 )
        //    {
        //        count = barCount - startingIndex;
        //    }

        //    long endindex = startingIndex + count;

        //    if ( endindex >= _bars.Count )
        //    {
        //        endindex = _bars.Count;
        //    }

        //    result = new ArraySegment<long>( _rawBarTimeArray, startingIndex, ( int ) ( endindex - startingIndex ) );

        //    return result;
        //}

        //public PooledList< double > GetNonActiveBars( DataBarProperty valueEnum, int startingIndex )
        //{
        //    PooledList< double > result = new PooledList< double >( );

        //    int count = ( int )_bars.Count - startingIndex;

        //    for( int i = startingIndex; i < startingIndex + count && i < _bars.Count; i++ )
        //    {
        //        if( !_bars[ (int) i ].IsActiveHour )
        //        {
        //            result.Add( ( double )_bars[ (int) i ].GetValue( valueEnum ) );
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }

        //    return result;
        //}

        public int iBarShift( string symbol, int timeframe, DateTime time, bool exact )
        {
            return GetIndexByTime( time );
        }

        

        //        protected bool IsSessionStart(  )

        public bool IsTodaySession( int barIndex )
        {
            if ( _barList != null )
            {
                return _barList.IsTodaySession( barIndex );
            }
            

            return false;
        }

        public bool TodayPriceHasBrokenThisLevel( double price, bool upOrDown )
        {
            //long todayBeginningIndex = _cachedDailyBeginIndexSearches.Last( );

            //int totalBars = 0;

            //double[ ] result;
            //lock ( this )
            //{
            //    for ( long i = todayBeginningIndex; i < _bars.Count; i++ )
            //    {
            //        if ( upOrDown ) // true is above this price
            //        {
            //            if ( _bars[ (int) i ].Close > price )
            //            {
            //                totalBars++;

            //                if ( _bars[ (int) i ].Close > ( price + ( double ) 0.0005 ) ) return true;
            //            }
            //        }
            //        else
            //        {
            //            if ( _bars[ (int) i ].Close < price )
            //            {
            //                totalBars++;

            //                if ( _bars[ (int) i ].Close < ( price - ( double ) 0.0005 ) ) return true;
            //            }
            //        }
            //    }
            //}

            //if ( totalBars >= 2 ) return true;

            return false;
        }

        public int GetTimeBlockIndex( DateTime barTime )
        {
            int index = -1;

            if ( _barList != null )
            {
                index =  _barList.GetTimeBlockIndex( barTime );

                return GetIndexByTime( index );
            }

            return index;
        }

        public DateTime? GetTimeAtIndex( int index )
        {
            if( _barList.Count > 0 && index < _barList.Count )
            {
                ref SBar bar = ref _barList[ index ];
                return bar.BarTime;
            }

            return null;
        }

        public int GetIndexByTime( long time )
        {
            if( time > -1 )
            {
                return GetIndexByTime( time.FromLinuxTime( ) );
            }

            return -1;
        }

        public int GetIndexAtTimeRoundToMinute( DateTime time )
        {
            return GetIndexByTime( time );
        }

        public int GetIndexAtTimeRoundToMinute( long time )
        {
            if( time > -1 )
            {
                return GetIndexAtTimeRoundToMinute( time.FromLinuxTime( ) );
            }

            return -1;
        }

        /// <summary>
        /// Obtain the index of the dataSource bar, with the given time. Time gaps must be considered. Result is -1 to
        /// indicate not found. Since this baseMethod is called very extensively on drawing, it employes a caching
        /// mechanism.
        /// </summary>
        public int GetIndexByTime( DateTime time )
        {
            return _barList.GetIndexByTime( time.ToLinuxTime() );            
        }

        

        public ref SBar GetBarContainingTime( DateTime time, TimeSpan period )
        {
            if( period == TimeSpan.FromDays( 30 ) )
            {
                return ref GetMonthlyIndexContainingTime( time );
            }

            long min = 0;
            long max = _barList.Count - 1;

            while( min <= max )
            {
                long mid     = ( min + max ) / 2;
                
                ref SBar bar = ref _barList[ (int) mid ];
                var fromTime = bar.BarTime;
                var toTime   = fromTime + period;

                if( time >= fromTime && time < toTime )
                {
                    return ref bar;
                }
                else if( time < fromTime )
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }

            return ref SBar.EmptySBar;
        }

        public ref SBar GetMonthlyIndexContainingTime( DateTime time )
        {
            long barCount = _barList.Count - 1;

            for( int i = 0; i < barCount; i++ )
            {
                ref SBar curBar = ref _barList[ i ];
                var fromTime = curBar.BarTime;
                var toTime   = _barList[( i + 1 )].BarTime;

                if( time >= fromTime && time < toTime )
                {
                    return ref curBar;                    
                }
            }

            return ref SBar.EmptySBar;
        }

        //public void ApplyPatternsToBars( ReadOnlyCollection< int > candlePattern )
        //{
        //    lock( this  )
        //    {
        //        for ( int i = 0; i < candlePattern.Count; i++ )
        //        {
        //            if ( candlePattern[ i ] != 0 )
        //            {
        //                DataBar barI            = _bars[ i ];
        //                barI.CandleStickPattern = candlePattern[ i ];
        //                _bars[ i ]          = barI;
        //            }

        //        }
        //        
        //    }
        //}

        public void ApplyPatternsToBars( int startingIndex, int count, TACandle[ ] pattern )
        {
            for( int i = startingIndex; i < startingIndex + count; i++ )
            {
                if( pattern[ i - startingIndex ] > 0 )
                {
                    ref SBar bar = ref _barList[i];
                    bar.CandlePatterns = pattern[ i - startingIndex ];
                }
            }
        }

        public void ApplyPatternsToBars( CandleFormation pattern )
        {
            var barCount = _barList.Count;

            if( pattern == null )
            {
                return;
            }

            for( int i = pattern.BeginIndex; i <= pattern.EndIndex; i++ )
            {
                if( i > 0 && i < barCount )
                {
                    ref SBar bar = ref _barList[i];
                    bar.CandlePatterns = pattern.CandleType;
                }
            }
        }

        public void AddSignalsToDataBar( IEnumerable< KeyValuePair< int, TASignal > > signalCollection )
        {
            Parallel.ForEach( signalCollection, signal =>
            {   
                ref SBar safeDataBars = ref _barList[ signal.Key ];

                if( safeDataBars != SBar.EmptySBar )
                {
                    safeDataBars.AddSignal( signal.Value );
                }
            } );
        }

        public void AddSignalsToDataBar( IEnumerable< KeyValuePair< long, TASignal > > signalCollection )
        {
            Parallel.ForEach( signalCollection, signal =>
            {
                if( signal.Key == 1580449020000 )
                {
                }

                var index = GetIndexByTime( signal.Key );

                if ( index < 0 ) return;

                ref SBar safeDataBars = ref _barList[ index ];

                if( safeDataBars != SBar.EmptySBar )
                {
                    safeDataBars.AddSignal( signal.Value );
                    
                }
            } );
        }

        
        public void RemoveWavePTsFromAllBars( TASignal signal1, TASignal signal2 )
        {
            int count = TotalBarCount;

            for( int i = 0; i < count; i++ )
            {
                ref SBar safeDataBars = ref _barList[i];

                if( safeDataBars != SBar.EmptySBar && safeDataBars.TechnicalAnalysisSignal != TASignal.NONE )
                {
                    safeDataBars.RemoveSignals( signal1, signal2 );
                }
            }
        }

        public void RemoveWavePTsFromOneBar( int removedIndex )
        {
            ref SBar safeDataBars = ref _barList[ removedIndex ];

            if( safeDataBars != SBar.EmptySBar && safeDataBars.TechnicalAnalysisSignal != TASignal.NONE )
            {
                safeDataBars.RemoveSignals( TASignal.WAVE_TROUGH, TASignal.WAVE_PEAK );
            }
        }

        public void RemoveSignalsStartingFromIndex( int startIndex, TASignal signal1, TASignal signal2 )
        {
            int count = TotalBarCount;

            if( startIndex >= count )
            {
                return;
            }

            for( int i = startIndex; i < count; i++ )
            {
                if( i == 83704 )
                {
                }

                ref SBar safeDataBars = ref _barList[i];

                if( safeDataBars.TechnicalAnalysisSignal != TASignal.NONE )
                {
                    safeDataBars.RemoveSignals( signal1, signal2 );
                }
            }
        }

        public void RemoveSignalsFromList( PooledList< long > tobeRemoved, TASignal signal1, TASignal signal2 )
        {
            foreach( long barTime in tobeRemoved )
            {
                var index = GetIndexByTime( barTime );

                ref SBar safeDataBars = ref _barList[ index ];

                if( safeDataBars.TechnicalAnalysisSignal != TASignal.NONE )
                {
                    safeDataBars.RemoveSignals( signal1, signal2 );
                }
            }
        }

        public void RemoveSignals( TASignal toBeRemoved )
        {
            int count = TotalBarCount;

            for( int i = 0; i < count; i++ )
            {
                ref SBar safeDataBars = ref _barList[i];

                if( safeDataBars.TechnicalAnalysisSignal != TASignal.NONE )
                {
                    safeDataBars.RemoveSignals( toBeRemoved );
                }
            }
        }

        void BuildElliottWaveDictionary( string instrument, int offerId, TimeSpan period )
        {
            var command = string.Format( "BuildElliottWaveDictionary", FinancialHelper.GetPeriodString( period ) );                       

            var undoArea  = _hews.GetSelectedUndoRedoArea( period );

            undoArea.Start( command );           

            var startDate = DateTime.MinValue;
            var endDate   = DateTime.MinValue;

            ForexHelper.GetStartAndEndDateForDatabar( period, out startDate, out endDate );
            _hews.GetElliottWave( offerId, startDate.ToLinuxTime( ), endDate.ToLinuxTime( ), period );

            undoArea.Commit( );
        }

        public void GetExtremumsOfRange( int start, int end, ref float minimum, ref float maximum, bool includeBidAsk )
        {
            long barCount = _barList.Count;

            minimum       = float.MaxValue;
            maximum       = float.MinValue;

            for( int i = start; i < end && i < barCount; i++ )
            {
                if( _barList.Low(  i  ) > 0 )
                {
                    minimum = ( float )Math.Min( _barList.Low(  i  ), minimum );
                }

                if( _barList.High(  i  ) > 0 )
                {
                    maximum = ( float )Math.Max( _barList.High(  i  ), maximum );
                }
            }
        }

        public void ClearSignalAndPattern( )
        {
            long endindex = _barList.Count;

            Parallel.For( 0, endindex,
            k =>
            {
                ref SBar bar = ref _barList[ (int) k ];
                bar.ClearSignal( );
                bar.ClearPattern( );
            } );
        }

        public void RemoveWavesFromBar( int waveScenarioNo, long barTime )
        {
            var index = GetIndexByTime( barTime );

            if( index > 0 )
            {
                ref SBar  specificBar = ref _barList[ index ];

                specificBar.RemoveWavesFromDatabar( waveScenarioNo );
            }
        }

        public PooledList< long > RemoveWavesFromRange( int waveScenarioNo, IElliottWave waves )
        {
            var output = new PooledList< long >( );

            var beginningBar = waves.BeginTimeUTC;
            var endOfPeriod = beginningBar + FinancialHelper.GetTimeSpanFromString( waves.Period );

            var beginindex = GetIndexByTime( beginningBar );
            var endindex = GetIndexByTime( endOfPeriod );

            if( beginindex == -1 || endindex == -1 )
            {
                return output;
            }

            long barCount = _barList.Count;

            for( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                ref SBar bar = ref _barList[ (int) i ];
                if ( bar.RemoveMatchedWavesFromDatabar( waveScenarioNo, waves.GetWaveFromScenario( waveScenarioNo ) ) )
                {
                    output.Add( bar.LinuxTime );
                }
            }

            return output;
        }

        public PooledList< long > RemoveWavesFromRange( int waveScenarioNo, ref WaveInfo waves, DateTime beginTimeUTC, TimeSpan period )
        {
            var output      = new PooledList< long >( );

            var endOfPeriod = beginTimeUTC + period;

            var beginindex  = GetIndexByTime( beginTimeUTC );
            var endindex    = GetIndexByTime( endOfPeriod );

            if( beginindex == -1 || endindex == -1 )
            {
                return output;
            }

            long barCount = _barList.Count;

            for( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                ref SBar bar = ref _barList[ ( int ) i ];
                if ( bar.RemoveMatchedWavesFromDatabar( waveScenarioNo, waves.ElliottWave ) )
                {
                    output.Add( bar.LinuxTime );
                }
            }

            return output;
        }

        public PooledList< long > FindOwningBars( IElliottWave waves )
        {
            var output = new PooledList< long >( );

            var beginningBar = waves.BeginTimeUTC;
            var endOfPeriod = beginningBar + FinancialHelper.GetTimeSpanFromString( waves.Period );

            var beginindex = GetIndexByTime( beginningBar );
            var endindex = GetIndexByTime( endOfPeriod );

            if( beginindex == -1 || endindex == -1 )
            {
                return output;
            }

            long barCount = _barList.Count;

            var hew1st = waves.GetWaveFromScenario( 1 ).GetFirstWave( );

            var hew2nd = waves.GetWaveFromScenario( 1 ).GetSecondaryWave( );

            if( hew1st.HasValue )
            {
                for( long i = beginindex; i < endindex && i < barCount; i++ )
                {
                    var firstInfo = hew1st.Value;
                    ref SBar bar = ref _barList[ ( int ) i ];

                    if ( bar.MatchesWave( ref firstInfo ) )
                    {
                        output.Add( bar.LinuxTime );
                    }
                }
            }

            if( hew2nd.HasValue )
            {
                for( long i = beginindex; i < endindex && i < barCount; i++ )
                {
                    var secondInfo = hew2nd.Value;
                    ref SBar bar = ref _barList[ ( int ) i ];

                    if ( bar.MatchesWave( ref secondInfo ) )
                    {
                        output.Add( bar.LinuxTime );
                    }
                }
            }

            return output;
        }

        // FIX
        public ref SBar GetHighestBarOfTheRange( DateTime beginningBar, TimeSpan period )
        {
            DateTime endOfPeriod = beginningBar + period;

            return ref GetHighestBarOfTheRange( beginningBar, endOfPeriod );
        }




        // FIX
        public ref SBar GetHighestBarOfTheRangeInclusive( DateTime beginBarTime, DateTime endBarTime )
        {
            int beginindex = GetIndexAtTimeRoundToMinute( beginBarTime );
            int endindex   = GetIndexAtTimeRoundToMinute( endBarTime );

            if( beginindex == -1 || endindex == -1 )
            {
                return ref SBar.EmptySBar;
            }

            var maximum = _barList.High(  beginindex  );

            var barCount = _barList.Count;

            int hIndex = -1;

            for ( int i = beginindex; i <= endindex && i < barCount; i++ )
            {
                if( _barList.High(  i  ) > maximum )
                {
                    maximum = _barList.High(  i  );
                    hIndex = i;                    
                }
            }

            if ( hIndex > -1 )
            {
                return ref _barList[ hIndex ];
            }
            else
            {
                return ref SBar.EmptySBar;
            }            
        }


        // FIX
        public ref SBar GetHighestBarOfTheRange( DateTime beginBarTime, DateTime endBarTime )
        {
            var period        = endBarTime - beginBarTime;
            SBar beginBar = GetBarByTime( beginBarTime );
            SBar endBar   = GetBarByTime( endBarTime );

            if ( beginBar == SBar.EmptySBar || endBar == SBar.EmptySBar )
            {
                if ( beginBarTime > FirstBarTime && endBarTime <= LastBarTime )
                {
                    if ( beginBar == SBar.EmptySBar )
                    {
                        beginBar = GetPreviousBar( beginBarTime );

                        if ( beginBar == SBar.EmptySBar )
                        {
                            return ref SBar.EmptySBar;
                        }
                    }

                    if ( endBar == SBar.EmptySBar)
                    {
                        endBar = GetPreviousBar( endBarTime );

                        if ( endBar == SBar.EmptySBar )
                        {
                            return ref SBar.EmptySBar;
                        }                        
                    }
                }
                else
                {
                    return ref SBar.EmptySBar;
                }
            }

            double maximum = beginBar.High;

            long barCount  = _barList.Count;

            int hBarIndex = beginBar.Index;

            for ( int i = beginBar.Index; i < endBar.Index && i < barCount; i++ )
            {
                if ( _barList.High(  i  ) > maximum )
                {
                    maximum = _barList.High(  i  );
                    hBarIndex = i;
                }
            }

            if ( hBarIndex > -1 )
            {
                return ref _barList[ hBarIndex ];
            }
            else
            {
                return ref SBar.EmptySBar;
            }            
        }


        // FIX
        public ref SBar GetLowestBarOfTheRange( DateTime beginningBar, TimeSpan period )
        {
            DateTime endOfPeriod = beginningBar + period;

            return ref GetLowestBarOfTheRange( beginningBar, endOfPeriod );
        }

        // FIX
        public ref SBar GetLowestBarOfTheRange( DateTime beginBarTime, DateTime endBarTime )
        {
            var period   = endBarTime - beginBarTime;

            SBar beginBar =  GetBarByTime( beginBarTime );
            SBar endBar   =  GetBarByTime( endBarTime );

            if ( beginBar == SBar.EmptySBar || endBar == SBar.EmptySBar )
            {
                if ( beginBarTime > FirstBarTime && endBarTime <= LastBarTime )
                {
                    if ( beginBar == SBar.EmptySBar )
                    {
                        beginBar = GetPreviousBar( beginBarTime );

                        if ( beginBar == SBar.EmptySBar )
                        {
                            return ref SBar.EmptySBar;
                        }
                    }

                    if ( endBar == SBar.EmptySBar )
                    {
                        endBar =  GetPreviousBar( endBarTime );

                        if ( endBar == SBar.EmptySBar )
                        {
                            return ref SBar.EmptySBar;
                        }
                    }                    
                }
                else
                {
                    return ref SBar.EmptySBar;
                }
            }                      

            double minimum = beginBar.Low;

            long barCount = _barList.Count;

            int lBar = beginBar.Index;

            for ( int i = beginBar.Index; i <= endBar.Index && i < barCount; i++ )
            {
                if( _barList.Low(  i  ) < minimum )
                {
                    minimum = _barList.Low(  i  );
                    lBar = i;
                }
            }

            if ( lBar > -1 )
            {
                return ref _barList[ lBar ];
            }
            else
            {
                return ref SBar.EmptySBar;
            }                       
        }

        private ref SBar GetPreviousBar( DateTime beginBarTime  )
        {            
            for( long i = 0; i <= TotalBarCount; i++ )
            {
                ref SBar bar = ref _barList[ ( int ) i ];

                if ( bar.BarTime > beginBarTime )
                {                    
                    return ref bar;
                }
            }

            return ref SBar.EmptySBar;
        }

        private ref SBar GetNextBar( DateTime beginBarTime )
        {
            for( long i = 0; i <= TotalBarCount; i++ )
            {
                ref SBar bar = ref _barList[ ( int ) i ];

                if ( bar.BarTime > beginBarTime )
                {
                    return ref bar;
                }                
            }

            return ref SBar.EmptySBar;
        }

        public ref SBar GetLowestBarOfTheRangeInclusive( DateTime beginBarTime, DateTime endBarTime )
        {
            int beginindex = GetIndexAtTimeRoundToMinute( beginBarTime );
            int endindex   = GetIndexAtTimeRoundToMinute( endBarTime );

            if( beginindex == -1 || endindex == -1 )
            {
                return ref SBar.EmptySBar;
            }

            var minimum  = _barList.Low(  beginindex  );

            var barCount = _barList.Count;

            int lBar     = -1;

            for ( int i = beginindex; i <= endindex && i < barCount; i++ )
            {
                if( _barList.Low(  i  ) < minimum )
                {
                    minimum = _barList.Low(  i  );
                    lBar = i;                    
                }
            }


            if ( lBar > -1 )
            {
                return ref _barList[ lBar ];
            }
            else
            {
                return ref SBar.EmptySBar;
            }
        }

        public bool Is2ndBarOutsideBarTo1stBar( long firstBarTime, long secondBarTime )
        {
            var firstBarIndex = GetIndexByTime( firstBarTime );
            var secondBarIndex = GetIndexByTime( secondBarTime );

            return IsOutsideBar( firstBarIndex, secondBarIndex );
        }

        public bool Is2ndBarInsideBarTo1stBar( long firstBarTime, long secondBarTime )
        {
            int firstBarIndex = GetIndexByTime( firstBarTime );
            int secondBarIndex = GetIndexByTime( secondBarTime );

            return IsInsideBar( firstBarIndex, secondBarIndex );
        }

        public bool IsInsideBar( int firstBarIndex, int secondBarIndex )
        {
            if( ( firstBarIndex < 0 ) || ( secondBarIndex < 0 ) || ( firstBarIndex >= secondBarIndex ) )
            {
                return false;
            }

            if( ( _barList.High(  firstBarIndex  ) >= _barList.High(  secondBarIndex  ) ) && ( _barList.Low(  firstBarIndex  ) < _barList.Low(  secondBarIndex  ) ) )
            {
                return true;
            }

            if( ( _barList.High(  firstBarIndex  ) > _barList.High(  secondBarIndex  ) ) && ( _barList.Low(  firstBarIndex  ) <= _barList.Low(  secondBarIndex  ) ) )
            {
                return true;
            }

            return false;
        }

        public bool IsDownTrend( long previousTime, long currentBarTime )
        {

            ref SBar previousBar = ref GetBarByTime( previousTime );
            ref SBar currentBar  = ref GetBarByTime( currentBarTime );

            if ( previousBar == SBar.EmptySBar || currentBar == SBar.EmptySBar )
            {
                throw new ArgumentException( );
            }                

            if( Is2ndBarOutsideBarTo1stBar( previousTime, currentBarTime ) )
            {
                if( ( currentBar.Open <= previousBar.Close ) && ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }
            }
            else if( Is2ndBarInsideBarTo1stBar( previousTime, currentBarTime ) )
            {
                if( previousBar.HasElliottWave )
                {
                    var wavePosition = previousBar.MainElliottWave.GetWaveLabelPosition();

                    if( wavePosition == WaveLabelPosition.BOTTOM )
                    {
                        return false;
                    }
                    else if( wavePosition == WaveLabelPosition.TOP )
                    {
                        return true;
                    }
                }
                else
                {
                    throw new NotImplementedException( );
                }
            }
            else
            {
                if( ( currentBar.High <= previousBar.High ) && ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }

                if( ( currentBar.High < previousBar.High ) ) //&& ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }

                if( ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsDownTrend( int previousBarIndex, int currentBarIndex )
        {
            ref SBar previousBar = ref GetBarByIndex( previousBarIndex );
            ref SBar currentBar  = ref GetBarByIndex( currentBarIndex );

            if( Is2ndBarOutsideBarTo1stBar( previousBarIndex, currentBarIndex ) )
            {
                if( ( currentBar.Open <= previousBar.Close ) && ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }
            }
            else if( Is2ndBarInsideBarTo1stBar( previousBarIndex, currentBarIndex ) )
            {
                if( previousBar.HasElliottWave )
                {
                    var wavePosition = previousBar.MainElliottWave.GetWaveLabelPosition();

                    if ( wavePosition == WaveLabelPosition.BOTTOM )
                    {
                        return false;
                    }
                    else if( wavePosition == WaveLabelPosition.TOP )
                    {
                        return true;
                    }
                }
                else
                {
                    throw new NotImplementedException( );
                }
            }
            else
            {
                if( ( currentBar.High <= previousBar.High ) && ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }

                if( ( currentBar.High < previousBar.High ) ) //&& ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }

                if( ( currentBar.Low < previousBar.Low ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsUpTrend( long currentBarTime, long nextBarTime )
        {
            ref SBar nextBar = ref GetBarByTime( nextBarTime );
            ref SBar currentBar  = ref GetBarByTime( currentBarTime );

            if ( nextBar == SBar.EmptySBar || currentBar == SBar.EmptySBar )
            {
                throw new ArgumentException( );
            }
            
            if( Is2ndBarOutsideBarTo1stBar( currentBarTime, nextBarTime ) )
            {
                if( ( nextBar.Open >= currentBar.Close ) && ( nextBar.High > currentBar.High ) )
                {
                    return true;
                }
            }
            else if( Is2ndBarInsideBarTo1stBar( currentBarTime, nextBarTime ) )
            {
                if( currentBar.HasElliottWave )
                {
                    var wavePosition = currentBar.MainElliottWave.GetWaveLabelPosition();

                    if ( wavePosition == WaveLabelPosition.BOTTOM )
                    {
                        return true;
                    }
                    else if( wavePosition == WaveLabelPosition.TOP )
                    {
                        return false;
                    }
                }
                else
                {
                    throw new NotImplementedException( );
                }
            }
            else
            {
                if( ( currentBar.High < nextBar.High ) && ( currentBar.Low <= nextBar.Low ) )
                {
                    return true;
                }

                if( currentBar.Low < nextBar.Low )
                {
                    return true;
                }

                if( currentBar.High < nextBar.High )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsUpTrend( int currentBarIndex, int nextBarIndex )
        {
            ref SBar nextBar    = ref GetBarByIndex( nextBarIndex );
            ref SBar currentBar = ref GetBarByIndex( currentBarIndex );

            if( Is2ndBarOutsideBarTo1stBar( currentBarIndex, nextBarIndex ) )
            {
                if( ( nextBar.Open >= currentBar.Close ) && ( nextBar.High > currentBar.High ) )
                {
                    return true;
                }
            }
            else if( Is2ndBarInsideBarTo1stBar( currentBarIndex, nextBarIndex ) )
            {
                if( currentBar.HasElliottWave )
                {
                    var wavePosition = currentBar.MainElliottWave.GetWaveLabelPosition();

                    if ( wavePosition == WaveLabelPosition.BOTTOM )
                    {
                        return true;
                    }
                    else if( wavePosition == WaveLabelPosition.TOP )
                    {
                        return false;
                    }
                }
                else
                {
                    throw new NotImplementedException( );
                }
            }
            else
            {
                if( ( currentBar.High < nextBar.High ) && ( currentBar.Low <= nextBar.Low ) )
                {
                    return true;
                }

                if( currentBar.Low < nextBar.Low )
                {
                    return true;
                }

                if( currentBar.High < nextBar.High )
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetBarThatBreakWaveFourAndExtremumIndex( ref SBar wave4Bar, TrendDirection trend, out SBar wave5, ref SBar bBar )
        {
            wave5             = default;
            
            long barCount     = _barList.Count;

            double wave4Low   = wave4Bar.Low;
            double wave4High  = wave4Bar.High;

            double minimum    = wave4Low;
            double maximum    = wave4High;

            long minimumIndex = -1;
            long maximumIndex = -1;

            if( TrendDirection.Uptrend == trend )
            {
                for( int i = wave4Bar.Index; i < barCount; i++ )
                {
                    if( _barList.High(  i  ) > maximum )
                    {
                        maximum = _barList.High(  i  );
                        maximumIndex = i;
                    }

                    if( _barList.Low(  i  ) < wave4Low )
                    {
                        wave5 = ref _barList[ (int) maximumIndex ];

                        bBar = ref _barList[i];

                        return true;
                    }
                }
            }
            else if( TrendDirection.DownTrend == trend )
            {
                for( int i = wave4Bar.Index; i < barCount; i++ )
                {
                    if( _barList.Low(  i  ) < minimum )
                    {
                        minimum = _barList.Low(  i  );
                        minimumIndex = i;
                    }

                    if( _barList.High(  i  ) > wave4High )
                    {
                        wave5 = ref _barList[ ( int ) minimumIndex ];

                        bBar = ref _barList[i];

                        return true;
                    }
                }
            }

            return false;
        }

        public bool FindWaveInTheRange( IElliottWave waves )
        {
            var beginningBar = waves.BeginTimeUTC;
            var endOfPeriod = beginningBar + FinancialHelper.GetTimeSpanFromString( waves.Period );

            var beginindex = GetIndexByTime( beginningBar );
            var endindex = GetIndexByTime( endOfPeriod );

            if( beginindex == -1 || endindex == -1 )
            {
                return false;
            }

            long barCount = _barList.Count;

            for( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                var hewAll = waves.GetWaveFromScenario( 1 ).GetAllWaves( );

                foreach( var wave in hewAll )
                {
                    var refWave = wave;
                    ref SBar bar = ref _barList[ ( int ) i ];

                    if ( bar.MatchesWave( ref refWave ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public long GetHighestGannImportanceInTheRange( TimeSpan period, int waveImportance, DateTime beginTime, DateTime endTime, TASignal extremum )
        {
            var beginindex = GetIndexByTime( beginTime );
            var endindex = GetIndexByTime( endTime );

            if( beginindex == -1 )
            {
                beginindex = 0;
            }

            long barCount = _barList.Count;

            if( endindex == -1 )
            {
                endindex = ( int )barCount;
            }

            int maxWaveImportance = -1;
            long maxIndex = -1;

            for( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                ref SBar bar = ref _barList[ (int) i ];

                if( bar.BarTime >= beginTime && bar.BarTime <= endTime )
                {
                    if( bar.TechnicalAnalysisSignal != TASignal.NONE )
                    {
                        TASignal signal = bar.TechnicalAnalysisSignal;

                        if( signal.HasFlag( extremum ) )
                        {
                            var waveImp = _hews.GetGannImportance( period, bar.LinuxTime );

                            if( waveImp > maxWaveImportance )
                            {
                                maxWaveImportance = waveImp;
                                maxIndex = bar.BarIndex;
                            }
                        }
                    }
                }
            }

            return maxIndex;
        }

        public ref SBar GetHighestWaveImportanceInTheRange( IHewManager mgr, TimeSpan period, int waveImportance, DateTime beginTime, DateTime endTime, TASignal extremum )
        {
            if ( mgr == null ) return ref SBar.EmptySBar;
            
            _hews = mgr;
            
            var beginindex = GetIndexByTime( beginTime );
            var endindex   = GetIndexByTime( endTime );

            if( beginindex == -1 )
            {
                beginindex = 0;
            }

            long barCount = _barList.Count;

            if( endindex == -1 )
            {
                endindex = ( int )barCount;
            }

            int maxWaveImportance = 0;

            int hBar = -1;

            for( int i = beginindex; i < endindex && i < barCount; i++ )
            {
                ref SBar bar = ref _barList[i];

                if( bar.BarTime >= beginTime && bar.BarTime <= endTime )
                {
                    if( bar.TechnicalAnalysisSignal != TASignal.NONE )
                    {
                        TASignal signal = bar.TechnicalAnalysisSignal;

                        if( signal.HasFlag( extremum ) )
                        {
                           
                            var waveImp = _hews.GetWaveImportance( period, bar.LinuxTime );

                            if( waveImp > maxWaveImportance )
                            {
                                maxWaveImportance = waveImp;
                                hBar = i;                                
                            }
                        }
                    }
                }
            }

            if ( hBar > -1 )
            {
                return ref _barList[ hBar ];
            }
            else
            {
                return ref SBar.EmptySBar;
            }
        }

        // FIX
        public ref SBar GetBarWithWaveInRange( ref WaveInfo wave, DateTime beginTime, DateTime endTime )
        {
            var beginindex = GetIndexByTime( beginTime );
            var endindex   = GetIndexByTime( endTime );

            if( beginindex == -1 || endindex == -1 )
            {
                return ref SBar.EmptySBar;
            }

            long barCount = _barList.Count;            

            for( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                ref SBar bar = ref _barList[ (int) i ];
                
                if ( bar.MatchesWave( ref wave ) )
                {
                    return ref bar;                    
                }
            }

            return ref SBar.EmptySBar;
        }

        public bool FindWavesInTheRange( PooledList< WaveInfo > waves, DateTime beginTimeUTC, string period )
        {
            var beginningBar = beginTimeUTC;
            var endOfPeriod = beginningBar + FinancialHelper.GetTimeSpanFromString( period );

            var beginindex = GetIndexByTime( beginningBar );
            var endindex = GetIndexByTime( endOfPeriod );

            if( beginindex == -1 || endindex == -1 )
            {
                return false;
            }

            long barCount = _barList.Count;

            for( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                foreach( WaveInfo wave in waves )
                {
                    var refWave = wave; 
                    ref SBar bar = ref _barList[ ( int ) i ];

                    if ( bar.MatchesWave( ref refWave ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool DownloadDatabarsFromXtoY( DateTime startDate, DateTime endDate )
        {
            //if ( _tradeStationLiveDataClient != null && _sessionInfo.IsEmtpy == false && _period.HasValue )
            //{
            //    return _tradeStationLiveDataClient.DownloadDatabarsFromXtoY( _sessionInfo, _period.Value, startDate, endDate );
            //}

            return false;
        }

        public bool WasBrokenUpWard(ref SBar barB)
        {            
            int beginindex = barB.Index;
            var brokenLevel = barB.High;
            long barCount = _barList.Count;

            for (long i = beginindex; i < barCount; i++)
            {
                ref SBar bar = ref _barList[ ( int ) i ];
                if (bar.High > brokenLevel )
                {
                    return true;
                }                
            }

            return false;
        }

        public bool WasBrokenDownward(ref SBar barB)
        {
            int beginindex = barB.Index;
            var brokenLevel = barB.Low;
            long barCount = _barList.Count;

            for (long i = beginindex; i < barCount; i++)
            {
                ref SBar bar = ref _barList[ ( int ) i ];
                if (bar.Low < brokenLevel)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetIndexBreakDown( long beginindex, long endindex, double brokenLevel )
        {
            long barCount = _barList.Count;

            for ( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                ref SBar bar = ref _barList[ ( int ) i ];
                if ( bar.Low < brokenLevel )
                {
                    return (int) i;
                }
            }

            return -1;
        }

        public ( TimeSpan, ( long, long )  ) GetSelectedBarTimeRange()
        {
            long lowest = 0;
            long higest = 0;

            long barCount = _barList.Count;

            for ( long i = 0;  i < barCount; i++ )
            {
                ref SBar bar = ref _barList[ ( int ) i ];
                
                if ( bar.IsSelected )
                {
                    if ( lowest == 0 )
                    {
                        lowest = bar.LinuxTime;
                    }

                    if ( bar.LinuxTime > higest )
                    {
                        higest = bar.LinuxTime;
                    }
                }
            }

            return ( _barList.Period(), ( lowest, higest ) );
        }

        public int GetIndexBreakUp( long beginindex, long endindex, double brokenLevel )
        {
            long barCount = _barList.Count;

            for ( long i = beginindex; i < endindex && i < barCount; i++ )
            {
                ref SBar bar = ref _barList[ ( int ) i ];
                if ( bar.High > brokenLevel )
                {
                    return ( int ) i;
                }
            }

            return -1;
        }
        public void AddIndicator( IMyIndicator indicator, bool value )
        {
            Indicators.AddIndicator( indicator );

            indicator.FullCalculationDoneEvent += Indicator_FullCalculationDoneEvent;
        }

        private void Indicator_FullCalculationDoneEvent( object sender, HistoricBarsUpdateEventArg e )
        {
            _doneLoading = true;
        }
    }
}
