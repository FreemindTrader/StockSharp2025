using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using fx.Collections;
using DevExpress.Mvvm;
using Ecng.Collections;

using fx.Definitions;
using Ecng.Xaml;

namespace fx.Algorithm
{
    /// <summary>
    /// <image url="$(SolutionDir)\..\..\30 - CommonImages\OpenPositionPendingOrderInfo.png"/>
    /// </summary>
    public class FxTradingEventsTsoCollection : ThreadSafeObservableCollection< FxTradingEvents >
    {
        string _symbol;

        public FxTradingEventsTsoCollection( AdvancedAnalysisManager parent, IListEx< FxTradingEvents > items, string symbol ) : base( items )
        {
            _parent = parent;
            _symbol = symbol;
        }

        MarketDirection _mainTrend;
        //double _todayRange = -1;
        //double _averageDailyRange = -1;
        //BarSpeed _barSpeeding;

        private bool _doneIndicatorCalculation = false;

        private ThreadSafeDictionary< TimeSpan, FxTradingEvents > _periodToItem = new ThreadSafeDictionary< TimeSpan, FxTradingEvents >( );

        FxTradingEvents _dailyEvent = null;
        FxTradingEvents _8hoursEvent = null;
        FxTradingEvents _6hoursEvent = null;
        FxTradingEvents _4hoursEvent = null;
        FxTradingEvents _3hoursEvent = null;
        FxTradingEvents _2hoursEvent = null;
        FxTradingEvents _1hourEvent = null;
        FxTradingEvents _30MinsEvent = null;
        FxTradingEvents _15MinsEvent = null;
        FxTradingEvents _5MinsEvent = null;
        FxTradingEvents _4MinsEvent = null;
        FxTradingEvents _1MinsEvent = null;
        FxTradingEvents _1SecEvent = null;

        public MarketDirection MainTrend
        {
            get
            {
                return _mainTrend;
            }
            set
            {
                _mainTrend = value;
            }
        }

        ThreadSafeDictionary< TimeSpan, bool > _indicatorResultsReceived = new ThreadSafeDictionary< TimeSpan, bool >( 8 );

        public void ResetIndicatorList( )
        {
            _doneIndicatorCalculation = false;
        }

        private AdvancedAnalysisManager _parent;

        //public void InitializeEvent( TimeSpan timeFrame )
        //{
        //    InternalInitializeEvent( timeFrame );
        //}

        public void LinkEventByTime( TimeSpan inTime, FxTradingEvents inEvent )
        {
            if( inTime == TimeSpan.FromSeconds( 1 ) )
            {
                _1SecEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromMinutes( 1 ) )
            {
                _1MinsEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromMinutes( 4 ) )
            {
                _4MinsEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromMinutes( 5 ) )
            {
                _5MinsEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromMinutes( 15 ) )
            {
                _15MinsEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromMinutes( 30 ) )
            {
                _30MinsEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromHours( 1 ) )
            {
                _1hourEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromHours( 2 ) )
            {
                _2hoursEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromHours( 3 ) )
            {
                _3hoursEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromHours( 4 ) )
            {
                _4hoursEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromHours( 6 ) )
            {
                _6hoursEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromHours( 8 ) )
            {
                _8hoursEvent = inEvent;
            }
            else if( inTime == TimeSpan.FromDays( 1 ) )
            {
                _dailyEvent = inEvent;
            }
        }

        public int GetNumberOfBarsDiffToShow( TimeSpan period )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return 86400;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 1440;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return 360;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 288;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 96;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 48;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return 24;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                return 12;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                return 8;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                return 6;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                return 4;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                return 3;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return 7;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                return 4;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                return 4;
            }

            return 0;
        }

        public void InternalInitializeEvent( TimeSpan timeFrame, List< TACandleViewModel > candles, List< TADivergenceViewModel > divergence )
        {
            var tradingEvent = new FxTradingEvents( timeFrame );

            tradingEvent.CandlePatternList = candles;
            tradingEvent.TADivergenceList = divergence;

            LinkEventByTime( timeFrame, tradingEvent );

            _indicatorResultsReceived.Add( timeFrame, true );

            if( _periodToItem.ContainsKey( timeFrame ) )
            {
                _periodToItem[ timeFrame ] = tradingEvent;
            }
            else
            {
                _periodToItem.Add( timeFrame, tradingEvent );
            }

            Add( tradingEvent );
        }

        public void AddEmaCrossSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            InternalAddMovingAvgCrossSignal( timeFrame, ithBar, signal, barCount );
        }

        public void AddAroonCrossSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            InternalAddAroonCrossSignal( timeFrame, ithBar, signal, barCount );
        }

        public void AddMacdCrossSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            InternalAddMacdCrossSignal( timeFrame, ithBar, signal, barCount );
        }

        public void AddStochValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            InternalAddStochValue( timeFrame, ithBar, signal, barCount );
        }

        private void InternalAddStochValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.StochValue = signal;
            tradingEvent.MacdCrossSignalBar = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            RecalculateOBOS( );
        }

        private void RecalculateOBOS( )
        {
            double stoch = 0;
            double rsi   = 0;
            double mfi   = 0;
            int maxObOs  = 0;

            if( _dailyEvent != null )
            {
                stoch   += ( double )_dailyEvent.StochValue   / 100 * 1440;
                rsi     += ( double )_dailyEvent.HewRsiValue  / 100 * 1440;
                mfi     += ( double )_dailyEvent.MfiValue     / 100 * 1440;
                maxObOs += 1440;
            }

            if( _8hoursEvent != null )
            {
                stoch   += ( double )_8hoursEvent.StochValue  / 100 * 480;
                rsi     += ( double )_8hoursEvent.HewRsiValue / 100 * 480;
                mfi     += ( double )_8hoursEvent.MfiValue    / 100 * 480;
                maxObOs += 480;
            }

            if( _6hoursEvent != null )
            {
                stoch   += ( double )_6hoursEvent.StochValue  / 100 * 360;
                rsi     += ( double )_6hoursEvent.HewRsiValue / 100 * 360;
                mfi     += ( double )_6hoursEvent.MfiValue    / 100 * 360;
                maxObOs += 360;
            }

            if( _4hoursEvent != null )
            {
                stoch   += ( double )_4hoursEvent.StochValue  / 100 * 240;
                rsi     += ( double )_4hoursEvent.HewRsiValue / 100 * 240;
                mfi     += ( double )_4hoursEvent.MfiValue    / 100 * 240;
                maxObOs += 240;
            }

            if( _3hoursEvent != null )
            {
                stoch   += ( double )_3hoursEvent.StochValue  / 100 * 180;
                rsi     += ( double )_3hoursEvent.HewRsiValue / 100 * 180;
                mfi     += ( double )_3hoursEvent.MfiValue    / 100 * 180;
                maxObOs += 180;
            }

            if( _2hoursEvent != null )
            {
                stoch   += ( double )_2hoursEvent.StochValue  / 100 * 120;
                rsi     += ( double )_2hoursEvent.HewRsiValue / 100 * 120;
                mfi     += ( double )_2hoursEvent.MfiValue    / 100 * 120;
                maxObOs += 120;
            }

            if( _1hourEvent != null )
            {
                stoch   += ( double )_1hourEvent.StochValue   / 100 * 60;
                rsi     += ( double )_1hourEvent.HewRsiValue  / 100 * 60;
                mfi     += ( double )_1hourEvent.MfiValue     / 100 * 60;
                maxObOs += 60;
            }

            if( _30MinsEvent != null )
            {
                stoch   += ( double )_30MinsEvent.StochValue  / 100 * 30;
                rsi     += ( double )_30MinsEvent.HewRsiValue / 100 * 30;
                mfi     += ( double )_30MinsEvent.MfiValue    / 100 * 30;
                maxObOs += 30;
            }

            if( _15MinsEvent != null )
            {
                stoch   += ( double )_15MinsEvent.StochValue  / 100 * 15;
                rsi     += ( double )_15MinsEvent.HewRsiValue / 100 * 15;
                mfi     += ( double )_15MinsEvent.MfiValue    / 100 * 15;
                maxObOs += 15;
            }

            if( _5MinsEvent != null )
            {
                stoch   += ( double )_5MinsEvent.StochValue   / 100 * 5;
                rsi     += ( double )_5MinsEvent.HewRsiValue  / 100 * 5;
                mfi     += ( double )_5MinsEvent.MfiValue     / 100 * 5;
                maxObOs += 5;
            }

            var taResult = _parent.GetAnalysisResult( _symbol );

            taResult.MaximumOBOS = maxObOs;
            taResult.TotalOBOSvalue = ( ( stoch + rsi + mfi ) / 3 ) / taResult.MaximumOBOS * 100;
        }

        private void RecalculateTrendProbability( TimeSpan timeFrame )
        {
            /*
             * ------------------------------------------------------------------------------------
             *
             *   MarketDirection _aroonXSignal;
             *   MarketDirection _macdCrossSignal;
             *   MarketDirection _sma50xSignal;
             *   MarketDirection _psarDirection;
             *   MarketDirection _maXoverSignal;
             *
             * ------------------------------------------------------------------------------------
             */

            var t = _periodToItem[ timeFrame ];

            var macdBuy      = t.MacdCrossSignal == ( int )MarketDirection.Bullish;
            var sma50Buy     = t.Sma50xSignal    == ( int )MarketDirection.Bullish;
            var emaBuy       = t.MaXoverSignal   == ( int )MarketDirection.Bullish;
            var emaTrendBuy  = t.EmaTrend        == ( int )MarketDirection.Bullish;
            var aroonBuy     = t.AroonXSignal    == ( int )MarketDirection.Bullish;
            var psarBuy      = t.PsarSignal      == ( int )MarketDirection.Bullish;

            var macdSell     = t.MacdCrossSignal == ( int )MarketDirection.Bearish;
            var sma50Sell    = t.Sma50xSignal    == ( int )MarketDirection.Bearish;
            var emaSell      = t.MaXoverSignal   == ( int )MarketDirection.Bearish;
            var emaTrendSell = t.EmaTrend        == ( int )MarketDirection.Bearish;
            var aroonSell    = t.AroonXSignal    == ( int )MarketDirection.Bearish;
            var psarSell     = t.PsarSignal      == ( int )MarketDirection.Bearish;

            if( macdBuy && sma50Buy && emaBuy && aroonBuy && psarBuy && emaTrendBuy )
            {
                _mainTrend = MarketDirection.Bullish;
            }

            if( macdSell && sma50Sell && emaSell && aroonSell && psarSell && emaTrendSell )
            {
                _mainTrend = MarketDirection.Bearish;
            }

            _parent.RecalculateTrendProbability( timeFrame );
        }

        public void AddMfiValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            InternalAddMfiValue( timeFrame, ithBar, signal, barCount );
        }

        public void AddCciValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            InternalAddCciValue( timeFrame, ithBar, signal, barCount );
        }

        private void InternalAddCciValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            var tradingEvent                = _periodToItem[ timeFrame ];
            tradingEvent.CciTrueValue       = signal;

            int absValue                    = Math.Abs( signal );
            tradingEvent.CciValue           = absValue;
            tradingEvent.MacdCrossSignalBar = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;
        }

        public void AddEmaValues( TimeSpan timeFrame, int ithBar, double currentEma, double previousEma, int barCount )
        {
            InternalAddEmaValues( timeFrame, ithBar, currentEma, previousEma, barCount );
        }

        public void AddCandleValue( TimeSpan timeFrame, int ithBar, CandleFormation candle, int barCount )
        {
            InternalAddCandle( timeFrame, ithBar, candle, barCount );
        }

        public void AddSma50Value( TimeSpan timeFrame, int ithBar, double signal, int barCount )
        {
            InternalAddSma50Value( timeFrame, ithBar, signal, barCount );
        }

        public void AddHewRsiValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            InternalAddHewRsiValue( timeFrame, ithBar, signal, barCount );
        }

        private void InternalAddMfiValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.MfiValue = signal;
            tradingEvent.MacdCrossSignalBar = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            RecalculateOBOS( );
        }

        private void InternalAddHewRsiValue( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.HewRsiValue = signal;
            tradingEvent.MacdCrossSignalBar = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            RecalculateOBOS( );
        }

        public void AddSma55CrossSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal )
        {
            InternalAddMovingAvgSignal( timeFrame, ithBar, signal );
        }

        public void AddPsarSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            InternalAddPsarSignal( timeFrame, ithBar, signal, barCount );
        }

        public void AddDivergence( TimeSpan timeFrame, int ithBar, TADivergence signal, int barCount )
        {
            InternalAddDivergence( timeFrame, ithBar, signal, barCount );
        }

        public void AddExtremum( TimeSpan timeFrame, int ithBar, int barCount )
        {
            InternalAddExtremum( timeFrame, ithBar, barCount );
        }

        public int FindIndexByPeriod( TimeSpan myPeriod )
        {
            if( Items.Count > 0 )
            {
                var index = IndexOf( this.Where( X => X.Period == myPeriod ).FirstOrDefault( ) );

                return index;
            }

            return -1;
        }

        private void InternalAddExtremum( TimeSpan timeFrame, int ithBar, int barCount )
        {
            int index = FindIndexByPeriod( timeFrame );

            if( index >= 0 )
            {
                var tradingEvent = Items[ index ];

                tradingEvent.BarsFromLastExtremum = barCount - ithBar;
                tradingEvent.SpecialNumber = FinancialHelper.GetSpecialNumber( tradingEvent.BarsFromLastExtremum );
            }
            else
            {
                var tradingEvent = new FxTradingEvents( timeFrame );

                tradingEvent.BarsFromLastExtremum = barCount - ithBar;
                tradingEvent.SpecialNumber = FinancialHelper.GetSpecialNumber( tradingEvent.BarsFromLastExtremum );

                Add( tradingEvent );
            }

            CheckIfAllIndicatorResultsReceived( timeFrame );
        }

        public void CheckIfAllIndicatorResultsReceived( TimeSpan timeFrame )
        {
            if( !_doneIndicatorCalculation )
            {
                if( _indicatorResultsReceived.ContainsKey( timeFrame ) )
                {
                    _indicatorResultsReceived.Remove( timeFrame );

                    float eventCount = Items.Count;

                    if( eventCount > 0 )
                    {
                        int percentage = 50 + ( int )( ( eventCount - _indicatorResultsReceived.Count ) / eventCount * 50 );

                        Messenger.Default.Send( new WorkDoneMessage( percentage ) );
                    }
                }

                if( _indicatorResultsReceived.Count == 0 )
                {
                    _doneIndicatorCalculation = true;

                    Messenger.Default.Send( new IndicatorResultsReceivedMessage( ) );
                }
            }
        }

        private int GetImageComboNumber( TADivergence div )
        {
            if( div == TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM || div == TADivergence.POSITIVE_DIVERGENCE_LOWER_LOW )
            {
                return 910;
            }
            else if( div == TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM || div == TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW )
            {
                return 913;
            }

            else if( div == TADivergence.NEGATIVE_DIVERGENCE_DOUBLE_TOP || div == TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH )
            {
                return 911;
            }
            else if( div == TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_DOUBLE_TOP || div == TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH )
            {
                return 912;
            }

            else if( div == TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW || div == TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP )
            {
                return 906;
            }
            else if( div == TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW || div == TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP )
            {
                return 908;
            }

            else if( div == TADivergence.HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH || div == TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM )
            {
                return 907;
            }
            else if( div == TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH || div == TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM )
            {
                return 909;
            }

            return -1;
        }

        public void AddStochasticSignal( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            InternalAddStochasticSignal( timeFrame, ithBar, signal, barCount );
        }

        public void AddStatus( TimeSpan timeFrame, string message )
        {
            InternalAddStatus( timeFrame, message );
        }

        private void InternalAddStochasticSignal( TimeSpan timeFrame, int ithBar, int signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.LastStochSignal    = signal;
            tradingEvent.MacdCrossSignalBar = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;
        }

        private void InternalAddStatus( TimeSpan timeFrame, string message )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.LastMessage = message;
        }

        #region Trending Calculation
        private void InternalAddMovingAvgCrossSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.MaXoverSignal      = ( int )signal;
            tradingEvent.MaXoverSignalBar   = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddAroonCrossSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.AroonXSignal       = ( int )signal;
            tradingEvent.AroonXSignalBar    = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddMacdCrossSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.MacdCrossSignal    = ( int )signal;
            tradingEvent.MacdCrossSignalBar = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddEmaValues( TimeSpan timeFrame, int ithBar, double currentEma, double previousEma, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.EmaCurrent = currentEma;
            tradingEvent.EmaPrevious = previousEma;

            if( currentEma > previousEma )
            {
                tradingEvent.EmaTrend = ( int )MarketDirection.Bullish;
            }
            else if( currentEma < previousEma )
            {
                tradingEvent.EmaTrend = ( int )MarketDirection.Bearish;
            }

            tradingEvent.EmaTrendBar = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddSma50Value( TimeSpan timeFrame, int ithBar, double smaValue, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.Sma50Value         = smaValue;
            tradingEvent.Sma50ValueBar      = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddMovingAvgSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.Sma50xSignal = ( int ) signal;
            tradingEvent.Sma50xSignalBar = ithBar;

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddCandle( TimeSpan timeFrame, int ithBar, CandleFormation candle, int barCount )
        {
            if( candle == null )
            {
                return;
            }

            var tradingEvent = _periodToItem[ timeFrame ];

            if ( ithBar > 4780 )
            {

            }

            if ( GetNumberOfBarsDiffToShow( timeFrame ) >= ( barCount - ithBar ) )
            {
                tradingEvent.CandleFormation    = candle;
                tradingEvent.CandleFormationBar = ithBar;
                tradingEvent.BarsFromLastSignal = barCount - ithBar;
            }
            
            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddPsarSignal( TimeSpan timeFrame, int ithBar, MarketDirection signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.PsarSignal         = ( int )signal;
            tradingEvent.PsarSignalBar      = ithBar;
            tradingEvent.BarsFromLastSignal = barCount - ithBar;

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }

        private void InternalAddDivergence( TimeSpan timeFrame, int ithBar, TADivergence signal, int barCount )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            if( ithBar > tradingEvent.LastDivergenceBar )
            {
                if ( GetNumberOfBarsDiffToShow( timeFrame ) >= ( barCount - ithBar ) )
                {
                    tradingEvent.LastDivergenceSignal = signal;
                    tradingEvent.LastDivergence       = GetImageComboNumber( signal );
                    tradingEvent.LastDivergenceBar    = ithBar;
                    tradingEvent.BarsFromLastSignal   = barCount - ithBar;
                }                
            }
            else if( ithBar == tradingEvent.LastDivergenceBar )
            {
                var signalValue = GetImageComboNumber( signal );

                if( signalValue > tradingEvent.LastDivergence )
                {
                    if ( GetNumberOfBarsDiffToShow( timeFrame ) >= ( barCount - ithBar ) )
                    {
                        tradingEvent.LastDivergenceSignal = signal;
                        tradingEvent.LastDivergence       = signalValue;
                        tradingEvent.LastDivergenceBar    = ithBar;
                        tradingEvent.BarsFromLastSignal   = barCount - ithBar;
                    }                    
                }
            }

            CheckIfAllIndicatorResultsReceived( timeFrame );

            RecalculateTrendProbability( timeFrame );
        }
        #endregion
    }
}

