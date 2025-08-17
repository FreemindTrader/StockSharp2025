using fx.Collections;
using fx.Common;
using fx.Definitions;
using StockSharp.Algo.Candles;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Bars
{
    public class HistoricBarsUpdateEventArg : EventArgs
    {
        public HistoricBarsUpdateEventArg( DataBarUpdateType type, uint begin, uint end )
        {
            UpdateType = type;
            BeginIndex = begin;
            EndIndex   = end;
        }

        public HistoricBarsUpdateEventArg( DataBarUpdateType type, int begin, int end )
        {
            UpdateType = type;
            BeginIndex = (uint) begin;
            EndIndex = (uint) end;
        }

        public HistoricBarsUpdateEventArg( DataBarUpdateType type, long begin, long end )
        {
            UpdateType = type;
            BeginIndex = ( uint ) begin;
            EndIndex = ( uint ) end;
        }

        public uint UpdatedBarsCount
        {
            get
            {
                return EndIndex - BeginIndex + 1;
            }
        }

        public DataBarUpdateType UpdateType { get; set; }

        public uint BeginIndex { get; set; }
        public uint EndIndex { get; set; }
    }

    public delegate void HistoricBarsUpdateDelegate( fxHistoricBarsRepo provider, DataBarUpdateType updateType, uint updatedBarsCount );
    public delegate void SupportResistanceLevelsUpdateDelege( fxHistoricBarsRepo provider, IList< SRlevel > newLevels );

    public interface IHistoricBarsRepo
    {
        

        ref SBar Current
        {
            get;
        }

        IIndicatorManager Indicators
        {
            get;
        }

        Security Security
        {
            get;
        }

        int ReloadAllCandles( Security security, List<TimeFrameCandleMessage> candles, TimeSpan period, int waveScenarioNo );

        ref SBar AddSingleCandle( TimeFrameCandleMessage candle );

        (uint, uint) AddReplaceCandlesRange(Security security, List<TimeFrameCandleMessage> candles, TimeSpan period, int waveScenarioNo, bool restoreWave = false );

        int TotalBarCount
        {
            get;
        }

        DateTime? FirstBarTime
        {
            get;
        }

        /// <summary>
        /// Date and time of the last bar of this provider.
        /// </summary>
        DateTime? LastBarTime
        {
            get;
        }

        TimeSpan? Period
        {
            get;
        }

        bool DoneLoading
        {
            get;
        }

        ref SBar this[ int index ]
        {
            get;
        }

        bool IsDownTrend( long previousTime, long currentBarTime );

        bool IsDownTrend( int previousBarIndex, int currentBarIndex );

        bool IsUpTrend( long currentBarTime, long nextBarTime );

        bool IsUpTrend( int currentBarIndex, int nextBarIndex );


        
        bool WasBrokenUpWard( ref SBar barB );
        
        bool FindWaveInTheRange( IElliottWave dbHewHTF );
        bool FindWavesInTheRange( PooledList< WaveInfo > wave, DateTime beginTimeUTC, string period );

        int GetIndexByTime( DateTime time );

        int GetIndexByTime( long time );

        ref SBar GetBarByIndex( int index );
        ref SBar GetBarByIndex( long index );
        ref SBar GetBarByTime( DateTime barTime );
        ref SBar GetBarByTime( long barTime );
        ref SBar GetHighestWaveImportanceInTheRange( IHewManager mgr, TimeSpan period, int waveImportance, DateTime beginTime, DateTime endTime, TASignal extremum );
        ref SBar GetBarWithWaveInRange( ref WaveInfo wave, DateTime beginTime, DateTime endTime );

        ref SBar GetHighestBarOfTheRange( DateTime beginningBar, TimeSpan period );

        ref SBar GetHighestBarOfTheRange( DateTime beginBarTime, DateTime endBarTime );

        ref SBar GetLowestBarOfTheRange( DateTime beginningBar, TimeSpan period );

        ref SBar GetLowestBarOfTheRange( DateTime beginBarTime, DateTime endBarTime );

        ref SBar GetLowestBarOfTheRangeInclusive( DateTime beginBarTime, DateTime endBarTime );

        ref SBar GetHighestBarOfTheRangeInclusive( DateTime beginBarTime, DateTime endBarTime );

        ref SBar GetBarContainingTime( DateTime time, TimeSpan period );

        int GetIndexAtTimeRoundToMinute( DateTime time );

        int GetIndexAtTimeRoundToMinute( long time );

        PooledList< long > FindOwningBars( IElliottWave waves );

        

        PooledList< long > RemoveWavesFromRange( int waveScenarioNo,  IElliottWave waves );

        PooledList< long > RemoveWavesFromRange( int waveScenarioNo, ref WaveInfo waves, DateTime beginTimeUTC, TimeSpan period );

        

        
        bool WasBrokenDownward(ref SBar barB);

        

        IList< double > GetDataBarSubset( DataBarProperty valueEnum );

        IList< double > GetDataBarSubset( DataBarProperty valueEnum, int startingIndex, int indexCount );

        
        double GetDataBarSubset( DataBarProperty valueEnum, int startingIndex );

        

        bool IsOutsideBar( int selectedBarIndex );

        bool IsOutsideBar( long rawBarTime );

        bool IsOutsideBar( int firstBar, int secondBar );

        DateTime? GetTimeAtIndex( int index );

        IList< double > GetRealBodyAsPips( );

        IList< double > GetUpperShadowAsPips( );

        IList< double > GetLowerShadowAsPips( );

        IList< double > GetRangeAsPips( );

        void ApplyPatternsToBars( int startingIndex, int count, TACandle[ ] pattern );

        void ApplyPatternsToBars( CandleFormation pattern );

        void AddSignalsToDataBar( IEnumerable< KeyValuePair< int, TASignal > > readOnlyCollection );

        void AddSignalsToDataBar( IEnumerable< KeyValuePair< long, TASignal > > readOnlyCollection );

        
        
        

        int Bars
        {
            get;
        }

        int iBarShift( string symbol, int timeframe, DateTime time, bool exact );
        
        int iHighest( int beginIndex, int endIndex );

        int iHighest1MoreBar( int beginIndex, int endIndex, int localLowIndex );

        int iLowest( int beginIndex, int endIndex );

        int iLowest1MoreBar( int beginIndex, int endIndex, int localLowIndex );

        void RemoveWavePTsFromAllBars( TASignal signal1, TASignal signal2 );

        void RemoveWavePTsFromOneBar( int removedIndex );

        void RemoveSignalsStartingFromIndex( int startIndex, TASignal signal1, TASignal signal2 );

        void RemoveSignalsFromList( PooledList< long > removed, TASignal signal1, TASignal signal2 );

        void RemoveSignals( TASignal signal );

        

        long GetHighestGannImportanceInTheRange( TimeSpan period, int waveImportance, DateTime beginTime, DateTime endTime, TASignal extremum );

        

        event SupportResistanceLevelsUpdateDelege DailyPivotsUpdateEvent;

        event SupportResistanceLevelsUpdateDelege WeeklyPivotsUpdateEvent;

        event SupportResistanceLevelsUpdateDelege MonthlyPivotsUpdateEvent;

        event EventHandler< HistoricBarsUpdateEventArg > HistoricBarUpdateEvent;

        bool GetBarThatBreakWaveFourAndExtremumIndex( ref SBar wave4Bar, TrendDirection trend, out SBar wave5, ref SBar bBar );

        bool DownloadDatabarsFromXtoY( DateTime startDate, DateTime endDate );

        void ClearMonthlyPivots( );

        void ClearWeeklyPivots( );

        void ClearDailyPivots( );

        IList< SRlevel > DailyPivots
        {
            get;
            set;
        }

        IList< SRlevel > WeeklyPivots
        {
            get;
            set;
        }

        IList< SRlevel > MonthlyPivots
        {
            get;
            set;
        }

        ref SBar NullBar
        {
            get;
        }

        int GetIndexBreakDown( long lowerBound, long upperBound, double breakLevel );
        int GetIndexBreakUp( long lowerBound, long upperBound, double breakLevel );

        void CreateDataBarCacheFriendlyStorage( SecurityId security, TimeSpan period, DateTime beginDate, DateTime endDate );
    }
}
