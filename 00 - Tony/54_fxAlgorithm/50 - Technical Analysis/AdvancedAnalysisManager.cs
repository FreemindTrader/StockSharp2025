using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fx.Definitions;
using System.ComponentModel;
using DevExpress.Mvvm;

using fx.Common;
using System.Collections.ObjectModel;
using StockSharp.BusinessEntities;
using fx.Bars;
using fx.Collections;
using Ecng.Xaml;
using Ecng.ComponentModel;

#pragma warning disable 414

namespace fx.Algorithm
{
    /// <summary>
    /// Class contains general helper functionality for financial classes and operation.
    /// </summary>
    public sealed partial class AdvancedAnalysisManager : BindableBase, ITechnicalAnalysisSignalProvider, IAdvancedAnalysisManager
    {
        private readonly ThreadSafeDictionary< TimeSpan, WorkFlowStatus > _waveImportanceCalcStatus = new ThreadSafeDictionary< TimeSpan, WorkFlowStatus >( );

        private string _symbol;       

        public AdvancedAnalysisManager( string symbol )
        {
            _symbol = symbol;
        }

        



        #region properties

        //private readonly ThreadSafeDictionary<string, double> _averageDailyRange = new ThreadSafeDictionary<string, double>( 5 );

        #endregion

        private readonly ThreadSafeDictionary< string, AdvancedAnalysisResult >   _advancedAnalysisResultForSymbol      = new ThreadSafeDictionary< string, AdvancedAnalysisResult >( 5 );
        private readonly ThreadSafeDictionary< WaveModelKey, WavePredictionModel > _hewWaveModels      = new ThreadSafeDictionary< WaveModelKey, WavePredictionModel >( 5 );
        private readonly ThreadSafeDictionary< WaveModelKey, IFactal > _hewWave35      = new ThreadSafeDictionary< WaveModelKey, IFactal >( 5 );

        private readonly ThreadSafeDictionary< TimeSpan, MonoWaveManager >        _monowaveMgrs      = new ThreadSafeDictionary<TimeSpan, MonoWaveManager>( 3 );

        public MonoWaveManager CreateOrGetMonowaveManager( TimeSpan period )
        {
            MonoWaveManager result = null;

            if ( _monowaveMgrs.TryGetValue( period, out result ) )
            {
                return result;
            }

            var monowaveManager = new MonoWaveManager( );

            if ( _monowaveMgrs.TryAdd( period, monowaveManager ) )
            {
                return monowaveManager;
            }
            else
            {
                if ( _monowaveMgrs.TryGetValue( period, out result ) )
                {
                    return result;
                }
            }

            throw new KeyNotFoundException();
        }


        private IHewManager      _hews;
        

        private IPeriodXTaManager _1SecForSymbol ;
        private IPeriodXTaManager _1MinTaForSymbol ;
        private IPeriodXTaManager _4MinTaForSymbol ;
        private IPeriodXTaManager _5MinTaForSymbol ;
        private IPeriodXTaManager _15MinTaForSymbol ;
        private IPeriodXTaManager _30MinTaForSymbol ;
        private IPeriodXTaManager _01HourTaForSymbol ;  // 1 hours for 60 days = 60 * 24 = 1470
        private IPeriodXTaManager _02HourTaForSymbol ;  // 1 hours for 60 days = 60 * 24 = 1470
        private IPeriodXTaManager _03HourTaForSymbol ;  // 1 hours for 60 days = 60 * 24 = 1470
        private IPeriodXTaManager _04HourTaForSymbol ;  // 1 hours for 60 days = 60 * 24 = 1470
        private IPeriodXTaManager _06HourTaForSymbol ;  // 1 hours for 60 days = 60 * 24 = 1470
        private IPeriodXTaManager _08HourTaForSymbol ;  // 1 hours for 60 days = 60 * 24 = 1470
        private IPeriodXTaManager _dailyTaForSymbol ;  // 1 day for 3 years  = 360 * 3 = 1470        
        private IPeriodXTaManager _weeklyTaForSymbol ; // Weekly for 10 years = 52 * 10 = 520        
        private IPeriodXTaManager _monthlyTaForSymbol ;  // Monthly for 20 years = 12 * 20 = 240

        private SRlevelsTsoCollection _supportResistanceBindingList;
        private ObservableCollectionEx<SRlevel> _supportResistanceBindingListItemSource;
        
        private FxTradingEventsTsoCollection _tradingEvents;
        private ObservableCollectionEx< FxTradingEvents > _tradingEventsItemSource;

        private FxTradingEventsTsoCollection _backTestingTA;
        private ObservableCollectionEx< FxTradingEvents > _backTestingTAItemSource;

        private FxEconomicCalendarBindingList _economicCalender;

        private ObservableCollectionEx< FxNewsEvent > _economicCalenderItemSource;

        private FxBarPercentageBindingList _percentageBindingList;

        private ObservableCollectionEx< FxBarPercentage > _percentageBindingListItemSource;

        private FxBBstrengthBindingList _bbStrength;

        private Indicator _dailyPivots;
        private Indicator _weeklyPivots;
        private Indicator _monthlyPivots;

        

        private readonly ThreadSafeDictionary< string, FxTradingEventsMsgBindingList >             _tradingEventsMsgForSymbol            = new ThreadSafeDictionary< string, FxTradingEventsMsgBindingList >( 5 );
        
        private readonly ThreadSafeDictionary< string, FxCurrentWaveInfoBindingList >              _currentWaveInfoForSymbol             = new ThreadSafeDictionary< string, FxCurrentWaveInfoBindingList >( 5 );

        

        private MarketDirection _dailyTrend   = MarketDirection.Unknown;
        private MarketDirection _4hoursTrend  = MarketDirection.Unknown;
        private MarketDirection _2hoursTrend  = MarketDirection.Unknown;
        private MarketDirection _1hourTrend   = MarketDirection.Unknown;
        private MarketDirection _30MinsTrend  = MarketDirection.Unknown;
        private MarketDirection _15MinsTrend  = MarketDirection.Unknown;
        private MarketDirection _055MinsTrend = MarketDirection.Unknown;
        private MarketDirection _01MinsTrend  = MarketDirection.Unknown;

        private OscillatorEnum _dailyOBOS     = OscillatorEnum.Unknown;
        private OscillatorEnum _4hoursOBOS    = OscillatorEnum.Unknown;
        private OscillatorEnum _2hoursOBOS    = OscillatorEnum.Unknown;
        private OscillatorEnum _1hourOBOS     = OscillatorEnum.Unknown;
        private OscillatorEnum _30MinsOBOS    = OscillatorEnum.Unknown;
        private OscillatorEnum _15MinsOBOS    = OscillatorEnum.Unknown;
        private OscillatorEnum _055MinsOBOS   = OscillatorEnum.Unknown;
        private OscillatorEnum _01MinsOBOS    = OscillatorEnum.Unknown;

        private static Security _lastSymbol = null;

        
    }
}
