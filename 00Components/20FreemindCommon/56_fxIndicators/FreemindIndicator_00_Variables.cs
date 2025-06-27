using System;
using fx.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using fx.Definitions;
using fx.Algorithm;
using fx.Bars;

using System.Collections.ObjectModel;
using fx.Common;

using System.Collections.Generic;
using System.Threading;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator /*, ILogSource, ILogReceiver*/
    {
        private Action< bool, HistoricBarsUpdateEventArg > _indicatorProcessFunc = null;

        


        public bool IsNonVisual
        {
            get => _isNonVisual;
            set
            {
                _isNonVisual = value;

                if ( _isNonVisual )
                {
                    _indicatorProcessFunc = NonVisualOnCalculate;
                }
                else
                {
                    _indicatorProcessFunc = VisualOnCalculate;
                }
            }
        }
        
        private bool _isNonVisual = false;

        private DictionarySlim< int, Tuple< int, double, double > > _extremumsValueDictionary;        
        private fx.Collections.OrderedDictionary< int, TASignal >                  _macdSignificantCross;
        
        private fx.Collections.OrderedDictionary<int, Tuple<MacdSignal, double >>  _macdExtremumDict;
        private fx.Collections.OrderedDictionary< int, TASignal >                  _stochasticsDictionary;
        private fx.Collections.OrderedDictionary< int, TASignal >                  _smoothedStochasticsDictionary;
        private fx.Collections.OrderedDictionary< int, TASignal >                  _overBoughtSoldDictionary;
        KeyValuePair< long, WavePointImportance >                   _price_TimeElapsed_oldestWave;
        KeyValuePair< long, WavePointImportance >                   _price_TimeElapsed_latestWave;

        KeyValuePair< long, WavePointImportance >                   _priorTime_CurrentPrice_oldestWave;
        KeyValuePair< long, WavePointImportance >                   _priorTime_CurrentPrice_latestWave;

        KeyValuePair< long, WavePointImportance >                   _priceRange_Time_oldestWave;
        KeyValuePair< long, WavePointImportance >                   _priceRange_Time_latestWave;

        KeyValuePair< long, WavePointImportance >                   _priorPrice_TrendTime_oldestWave;
        KeyValuePair< long, WavePointImportance >                   _priorPrice_TrendTime_latestWave;

        private DictionarySlim< int, TASignal >                     _macdDivergence;
        private DictionarySlim< int, TASignal >                     _comasDictionary;
        
        

        private FxBBstrengthBindingList                             _fxBBstrengthList = null;
        private FxTradingEventsTsoCollection                        _fxTradingEventsBindingList = null;
        private FxBarPercentageBindingList                          _fxBarPercentBindingList = null;

        private ThreadSafeDictionary< int, BBANDSStates >           _signalBollinger;
        private PooledList< int >                                   _crossBollinger;
        private FxTradingEventsMsgBindingList                       _msgBindingList;

        private PeriodXTaManager                                    _periodXTaManager                  = null;
        private TASignal                                            _lastCrossingDirection             = TASignal.NONE;
        private volatile bool                                       _noCalculationAllowed              = false;
        private DateTime                                            _lastCurrentBarUpdate;
        private bool                                                _performingElliottWaveReculation   = false;
        private bool                                                _doneFetchingFromDatabase          = false;
        private int                                                 _barCountBeforeCalculation         = 0;

        long                                                        _last3rdRedTime = -1;
        int                                                         _last3rdRedIndex = 0;
        int                                                         _lastRedIndex = -1;
        long                                                        _lastRedTime = -1;

        int                                                         _lastNenZZCountedBars = 0;

        
        bool                                                        _needToRebuildWaveImportance = false;

        private bool                                                _isNotAboveHourlyBars              = true;
        private int                                                 _lastMacdMaxIndex                  = 0;
        private int                                                 _lastMacdMinIndex                  = 0;

        private int                                                 _numberOfBarsRequired              = 0;
        private int                                                 _previousBBandCross                = 0;
        private int                                                 _currentBBandCross                 = 0;
        #region Macd
        private int                       _lastMacdCrossIndex                = 1;
        private DateTime                  _lastMacdCrossTime                 = DateTime.MinValue;
        private double                    _lastMacdCrossFastMacd             = -1;
        private double                    _lastMacdCrossSlowMacd             = -1;
        private int                       _lastMacdCrossUpIndex              = 1;
        private int                       _lastMacdCrossDownIndex            = 1;
        private TASignal                  _lastMacdSignal                    = TASignal.NONE;
        private MarketDirection           _lastMacdDirection                 = MarketDirection.Unknown;
        private MacdCrossEventArgs        _lastMacdCrossEvent;
        private bool                      _hasNewMacdCross                   = false;
        #endregion

        #region Oscillator
        private int                       _lastOscillatorCrossIndex          = 1;
        private DateTime                  _lastOscillatorCrossTime           = DateTime.MinValue;
        private double                    _lastOscillatorKValue              = -1;
        private double                    _lastOscillatorDValue              = -1;
        private MarketDirection           _lastOscillatorDirection           = MarketDirection.Unknown;
        private TASignal           _lastOscillatorSignal = TASignal.NONE;
        private bool                      _hasNewOscillatorCross             = false;
        private OscillatorCrossEventArgs  _lastOscillatorCrossEvent;
        private int                       _lastOscillatorExitOverBought      = 1;
        private int                       _lastOscillatorExitOverSold        = 1;

        #endregion


        #region EMA cross
        private int                       _lastEmaCrossIndex                 = 1;
        private DateTime                  _lastEmaCrossTime                  = DateTime.MinValue;
        private double                    _lastEma5Value                     = -1;
        private double                    _lastEma13Value                    = -1;
        private double                    _lastEma144Value                   = -1;
        private MarketDirection           _lastEmaDirection                  = MarketDirection.Unknown;
        private EmaCrossEventArgs         _lastEmaCrossEvent;
        private bool                      _hasNewEmaCross                    = false;

        #endregion


        #region SMA

        private int                       _lastSmaCrossIndex                 = 1;
        private DateTime                  _lastSmaCrossTime                  = DateTime.MinValue;
        private double                    _lastSma55Value                    = -1;
        private int                       _lastSmaBelowCount                 = 0;
        private int                       _lastSmaAboveCount                 = 0;
        private MarketDirection           _lastSmaCrossSignal                = MarketDirection.Unknown;
        private SmaCrossEventArgs         _lastSmaCrossEvent;
        private bool                      _hasNewSmaCross                    = false;
        #endregion


        #region CandleSticks Calculation
        
        double _realBodyHeightAverage = 0d;

        double _dataBarRangeAverage = 0d;

        double _topShadowAverage = 0d;

        double _bottomShadowAverage = 0d;

        #endregion


        private int _indexOfLastAroonCross = 1;
        private int                 _indexOfLastSarBreak               = 1;
        private int                 _indexOfSecondLastSarBreak         = 1;        

        

        

        

        

        private int                 _indexOfLastSmoothedMacd           = 1;

        

        private int                 _indexOfLastSmoothedOscillator     = 1;
        


        private int                 _indexOfLastComasTuringPoint       = 1;

        private int                 _indexOfLastBollingBandCross       = 0;

        private int                 _indexOfLastMaxValuesBtMacdPoints  = 1;
        

        private int                 _indexOfLastMacdNegDivergence      = 1;
        


        private int                 _indexOfLastMacdPosDivergence      = 1;
        
        

        private int                 _indexOfLastMaxValuesBtOscillator  = 1;
        



        private int                 _indexOfLastOscNegDivergence       = 1;
        
        
        private int                 _indexOfLastOscPosDivergence       = 1;



        private long                _lastDetectHiddenDivergenceInMajorUptrendCheckTime = 0;
        private long                _lastDetectHiddenDivergenceInMajorDowntrendCheckTime = 0;
        private int                 _lastCandleCheckIndex              = 0;
        private int                 _lastHiddenDivergenceIndex = 0;
        private int                 _currentComasSignificantPoint      = 0;
        private int                 _currentOscillatorCross            = 0;
        private int                 _exitOverBoughtIndex               = 0;
        private int                 _exitOverSoldIndex                 = 0;

        private HewManager          _hews;
        private MonoWaveManager     _monoWaveManager;

        #region Varaibles for Gann Swing

        private GannSwingVariables  _gannVariables0 = new GannSwingVariables();
        private GannSwingVariables  _gannVariables1 = new GannSwingVariables();
        private GannSwingVariables  _gannVariables2 = new GannSwingVariables();
        private GannSwingVariables  _gannVariables3 = new GannSwingVariables();
        private GannSwingVariables  _gannVariables4 = new GannSwingVariables();
        private GannSwingVariables  _gannVariables5 = new GannSwingVariables();

        private NeoSwingVariables   _neoVariables5 = new NeoSwingVariables();
        private NeoSwingVariables   _neoVariables8 = new NeoSwingVariables();
        private NeoSwingVariables   _neoVariables13 = new NeoSwingVariables();
        private NeoSwingVariables   _neoVariables21 = new NeoSwingVariables();
        private NeoSwingVariables   _neoVariables34 = new NeoSwingVariables();
        private NeoSwingVariables   _neoVariables55 = new NeoSwingVariables();
        private NeoSwingVariables   _neoHrs08 = new NeoSwingVariables();
        private NeoSwingVariables   _neoDaily = new NeoSwingVariables();
        private NeoSwingVariables   _neoWeekly = new NeoSwingVariables();
        private NeoSwingVariables   _neoMonthly = new NeoSwingVariables();

        private FastZigZagVariables _fastZZ5 = new FastZigZagVariables( );
        private FastZigZagVariables _fastZZ8 = new FastZigZagVariables( );
        private FastZigZagVariables _fastZZ13 = new FastZigZagVariables( );
        private FastZigZagVariables _fastZZ21 = new FastZigZagVariables( );
        private FastZigZagVariables _fastZZ34 = new FastZigZagVariables( );
        private FastZigZagVariables _fastZZ55 = new FastZigZagVariables( );
        private FastZigZagVariables _fastZZ89 = new FastZigZagVariables( );
        private FastZigZagVariables _fastZZ144 = new FastZigZagVariables( );






        #endregion



        private bool                _isZigZagInitalized               = true;
        private TimeSpan            _higherTimeFrame                  = TimeSpan.MinValue;
        //private IndicatorResults    _higherTimeFrameGannSwing;
        //private fxHistoricBarsRepo   _higerTimeFrameRepo;

        private bool                _isMovingUp                       = false;

        //private double[ ]           _higerTimeFrameZigZag;

        private int                 _extLabel                         = 0;

        //private DateTime          _timeExtremum4                    = DateTime.MinValue;
        //private DateTime          _timeExtremum5                    = DateTime.MinValue;
        //private DateTime          _timeExtremum6                    = DateTime.MinValue;

        private DateTime            _latestDatabaseReadTime           = DateTime.MinValue;

        //private double            _rateExtremum4                    = 0.0;
        //private double            _rateExtremum5                    = 0.0;
        //private double            _rateExtremum6                    = 0.0;

        //private int               _indexExtremum4                   = -1;
        //private int               _indexExtremum5                   = -1;
        //private int               _indexExtremum6                   = -1;
        private int                 _countedBars                      = 0;



        private int                 _zigzagLabel                      = 0; // = 0-before the first fracture ZZ. = 1-looking tags highs. = 2-looking for tags lows.

        private int                 _extDepth                         = 21;
        private int                 _extBackstep                      = 34;
        private bool                _noBackstep                       = false;
        private bool                _recoverFilter                    = true;

        private int                 _lastnenZigZagExtremumIndex       = -1;

        PivotPointsInfo             _dailyPivotPoints                 = null;
        PivotPointsInfo             _weeklyPivotPoints                = null;
        PivotPointsInfo             _monthlyPivotPoints               = null;

        
        



    }

}