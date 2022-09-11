using System;
using System.Collections.Generic;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using StockSharp.BusinessEntities;
using fx.Bars;
using System.Collections.ObjectModel;
using Ecng.Xaml;
using Ecng.ComponentModel;

namespace fx.Algorithm
{
    /// <summary>
    /// Class contains general helper functionality for financial classes and operation.
    /// </summary>
    public sealed partial class AdvancedAnalysisManager
    {        

        public IHewManager HewManager
        {
            get
            {
                if ( _hews == null )
                {
                    _hews = new HewManager( );
                }

                return _hews;
            }            
        }

        


        public FxTradingEventsTsoCollection InitializeBacktestingTA( )
        {
            _backTestingTAItemSource = new ObservableCollectionEx< FxTradingEvents >( );            

            _backTestingTA = new FxTradingEventsTsoCollection( this, _backTestingTAItemSource, _symbol );

            var candles = new List< TACandleViewModel >( );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEveningStar ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDoji ) );
            candles.Add( new TACandleViewModel( TACandle.CdlHammer ) );
            candles.Add( new TACandleViewModel( TACandle.CdlMorningStar ) );
            candles.Add( new TACandleViewModel( TACandle.CdlPiercing ) );
            candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear ) );
            candles.Add( new TACandleViewModel( TACandle.CdlShootingStar ) );

            var divergence = new List< TADivergenceViewModel >( );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );

            _backTestingTA.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromHours( 2 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromHours( 3 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromHours( 4 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromHours( 6 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromHours( 8 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromDays( 1 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromDays( 7 ), candles, divergence );
            _backTestingTA.InternalInitializeEvent( TimeSpan.FromDays( 30 ), candles, divergence );

            return _backTestingTA;
        }

        public FxTradingEventsTsoCollection InitializeBacktestingTA( string symbol, PooledList<TimeSpan> selectedTF )
        {                        
            _backTestingTAItemSource = new ObservableCollectionEx< FxTradingEvents >( );

            _backTestingTA = new FxTradingEventsTsoCollection( this, _backTestingTAItemSource, _symbol );

            List< TACandleViewModel > candles = new List< TACandleViewModel >( );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEveningStar ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDoji ) );
            candles.Add( new TACandleViewModel( TACandle.CdlHammer ) );
            candles.Add( new TACandleViewModel( TACandle.CdlMorningStar ) );
            candles.Add( new TACandleViewModel( TACandle.CdlPiercing ) );
            candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear ) );
            candles.Add( new TACandleViewModel( TACandle.CdlShootingStar ) );

            List< TADivergenceViewModel > divergence = new List< TADivergenceViewModel >( );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );

            foreach ( var tf in selectedTF )
            {
                _backTestingTA.InternalInitializeEvent( tf, candles, divergence );
            }

            return _backTestingTA;
        }

        public FxTradingEventsTsoCollection InitializeTradingEventsBindingList( )
        {            
            var itemsSource = new ObservableCollectionEx< FxTradingEvents >( );            

            _tradingEvents = new FxTradingEventsTsoCollection( this, itemsSource, _symbol );

            var candles = new List< TACandleViewModel >( );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear  ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull  ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEveningStar    ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDoji           ) );
            candles.Add( new TACandleViewModel( TACandle.CdlHammer         ) );
            candles.Add( new TACandleViewModel( TACandle.CdlMorningStar    ) );
            candles.Add( new TACandleViewModel( TACandle.CdlPiercing       ) );
            candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear    ) );
            candles.Add( new TACandleViewModel( TACandle.CdlShootingStar   ) );

            var divergence = new List< TADivergenceViewModel >( );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW          ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH        ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW   ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH                  ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW             ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM         ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM                ) );

            _tradingEvents.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 2 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 3 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 4 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 6 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 8 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromDays( 1 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromDays( 7 ), candles, divergence );
            _tradingEvents.InternalInitializeEvent( TimeSpan.FromDays( 30 ), candles, divergence );

            return _tradingEvents;
        }

        

        public void ResetTradingEventsBindingList( string symbol )
        {            
            if( _tradingEvents != null )
            {
                _tradingEvents.ResetIndicatorList( );
            }
        }

        public FxBBstrengthBindingList InitializeBBstrengthBindingList( string symbol )
        {            
            var itemsSource = new ObservableCollectionEx< FxBBstrength >( );
            _bbStrength = new FxBBstrengthBindingList( itemsSource );

            _bbStrength.InternalInitializeEvent( TimeSpan.FromDays( 7 ), 9 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromDays( 1 ), 8 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 4 ), 7 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 2 ), 6 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ), 5 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), 4 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), 3 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), 2 );
            _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), 1 );

            return _bbStrength;
        }

        public FxBBstrengthBindingList BBstrengthBindingList
        {
            get
            {
                if ( _bbStrength  == null )
                {
                    var itemsSource = new ObservableCollectionEx< FxBBstrength >( );
                    var _bbStrength = new FxBBstrengthBindingList( itemsSource );

                    _bbStrength.InternalInitializeEvent( TimeSpan.FromDays( 7 ), 14 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromDays( 1 ), 13 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 8 ), 12 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 6 ), 11 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 4 ), 10 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 3 ), 9 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 2 ), 8 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromHours( 1 ), 7 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), 6 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), 5 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), 4 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ), 3 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), 2 );
                    _bbStrength.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ), 1 );
                }                

                return _bbStrength;
            }            
        }

        public FxTradingEventsMsgBindingList InitializeTradingEventsMsgBindingList( string symbol )
        {
            FxTradingEventsMsgBindingList tradingEventsBindingList = null;

            if( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
            {
                return tradingEventsBindingList;
            }

            var itemsSource = new ObservableCollectionEx< FxTradingEventsMsg >( );
            FxTradingEventsMsgBindingList eventsBindingList = new FxTradingEventsMsgBindingList( itemsSource );

            if( _tradingEventsMsgForSymbol.TryAdd( symbol, eventsBindingList ) )
            {
                var supportedTF = SupportedTimeSpan;

                foreach( var tf in supportedTF )
                {
                    eventsBindingList.InternalInitializeEvent( tf );
                }

                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 30 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 8 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 6 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 3 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ) );
                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ) );

                return eventsBindingList;
            }
            else
            {
                if( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
                {
                    return tradingEventsBindingList;
                }
            }

            throw new KeyNotFoundException( );
        }


        public SRlevelsTsoCollection SupportResistanceBindingList
        {
            get
            {
                if ( _supportResistanceBindingList == null )
                {
                    var itemsSource = new ObservableCollectionEx< SRlevel >( );

                    _supportResistanceBindingListItemSource = itemsSource;

                    _supportResistanceBindingList = new SRlevelsTsoCollection( itemsSource );
                }

                return _supportResistanceBindingList;
            }
        }

        public ObservableCollectionEx<FxBarPercentage> BarPercentageItemSource
        {
            get
            {
                return _percentageBindingListItemSource;
            }
            
        }


        public FxBarPercentageBindingList BarPercentageBindingList
        {
            get
            {
                _percentageBindingListItemSource = new ObservableCollectionEx<FxBarPercentage>( );

                _percentageBindingList = new FxBarPercentageBindingList( _percentageBindingListItemSource );

                _percentageBindingList.InternalInitializeEvent( 1 );
                _percentageBindingList.InternalInitializeEvent( 2 );
                _percentageBindingList.InternalInitializeEvent( 3 );
                _percentageBindingList.InternalInitializeEvent( 4 );
                _percentageBindingList.InternalInitializeEvent( 5 );

                return _percentageBindingList;
            }            
        }

        public WaveTargetsTsoCollection WaveTargetBindingList
        {
            get
            {
                if ( _waveTargetBindingList == null )
                {
                    var itemsSource = new ObservableCollectionEx< FibLevelInfo >( );

                    _waveTargetsItemSource = itemsSource;

                    _waveTargetBindingList = new WaveTargetsTsoCollection( itemsSource );
                }

                return _waveTargetBindingList;
            }
        }

        private WaveTargetsTsoCollection _waveTargetBindingList;
        private ObservableCollectionEx< FibLevelInfo > _waveTargetsItemSource ;
        public ObservableCollectionEx<FibLevelInfo> WaveTargetItemSource
        {
            get
            {
                return _waveTargetsItemSource;
            }
        }

        public ObservableCollectionEx< FxTradingEvents > TradingEventsItemSource
        {
            get
            {
                return _tradingEventsItemSource;
            }
        }

        public ObservableCollectionEx< SRlevel > SRLevelsItemSource
        {            
            get
            {
                return _supportResistanceBindingListItemSource;
            }
        }

        public AdvancedAnalysisResult GetAnalysisResult( string instrument )
        {
            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

            AdvancedAnalysisResult analysisResult = null;

            if( _advancedAnalysisResultForSymbol.TryGetValue( symbol, out analysisResult ) )
            {
                return analysisResult;
            }

            analysisResult = new AdvancedAnalysisResult( this );

            if( _advancedAnalysisResultForSymbol.TryAdd( symbol, analysisResult ) )
            {
                return analysisResult;
            }
            else
            {
                if( _advancedAnalysisResultForSymbol.ContainsKey( symbol ) )
                {
                    return _advancedAnalysisResultForSymbol[ symbol ];
                }
            }

            throw new KeyNotFoundException( );
        }

        public bool GetOrCreateWaveModel( WaveModelKey key, fxHistoricBarsRepo bars, HewManager hew, out WavePredictionModel waveModel )
        {
            waveModel = null;

            if ( _hewWaveModels.TryGetValue( key, out waveModel ) )
            {
                return true;
            }

            waveModel = new WavePredictionModel( key, bars, hew );

            if ( _hewWaveModels.TryAdd( key, waveModel ) )
            {
                return true;
            }
            else
            {
                if ( _hewWaveModels.ContainsKey( key ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetOrCreate5Waves( WaveModelKey key, fxHistoricBarsRepo bars, HewManager hew, out C5Waves c5waves )
        {
            IFactal factal = null;
            c5waves = null;

            if ( _hewWave35.TryGetValue( key, out factal ) )
            {
                c5waves = ( C5Waves ) factal;
                return true;
            }

            c5waves = new C5Waves( bars, hew, key );

            if ( _hewWave35.TryAdd( key, c5waves ) )
            {
                return true;
            }
            else
            {
                if ( _hewWave35.ContainsKey( key ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetOrCreate3Waves( WaveModelKey key, fxHistoricBarsRepo bars, HewManager hew, out C3Waves c3waves )
        {
            IFactal factal = null;
            c3waves = null;

            if ( _hewWave35.TryGetValue( key, out factal ) )
            {
                c3waves = ( C3Waves ) factal;
                return true;
            }

            c3waves = new C3Waves( bars, hew, key );

            if ( _hewWave35.TryAdd( key, c3waves ) )
            {
                return true;
            }
            else
            {
                if ( _hewWave35.ContainsKey( key ) )
                {
                    return true;
                }
            }

            return false;
        }



        public FxTradingEventsTsoCollection InitializeTechnicalAnalysis( PooledList<TimeSpan> selectedTF )
        {
            _tradingEventsItemSource = new ObservableCollectionEx<FxTradingEvents>( );

            _tradingEvents = new FxTradingEventsTsoCollection( this, _tradingEventsItemSource, _symbol );

            List< TACandleViewModel > candles = new List< TACandleViewModel >( );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
            candles.Add( new TACandleViewModel( TACandle.CdlEveningStar ) );
            candles.Add( new TACandleViewModel( TACandle.CdlDoji ) );
            candles.Add( new TACandleViewModel( TACandle.CdlHammer ) );
            candles.Add( new TACandleViewModel( TACandle.CdlMorningStar ) );
            candles.Add( new TACandleViewModel( TACandle.CdlPiercing ) );
            candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear ) );
            candles.Add( new TACandleViewModel( TACandle.CdlShootingStar ) );

            List< TADivergenceViewModel > divergence = new List< TADivergenceViewModel >( );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
            divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );

            foreach ( var tf in selectedTF )
            {
                _tradingEvents.InternalInitializeEvent( tf, candles, divergence );
            }

            return _tradingEvents;
        }

        public FxTradingEventsTsoCollection TradingEventsBindingList
        {
            get
            {
                if ( _tradingEvents == null )
                {
                    _tradingEventsItemSource = new ObservableCollectionEx<FxTradingEvents>( );

                    _tradingEvents = new FxTradingEventsTsoCollection( this, _tradingEventsItemSource, _symbol );

                    List< TACandleViewModel > candles = new List< TACandleViewModel >( );
                    candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlEveningStar ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlDoji ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlHammer ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlMorningStar ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlPiercing ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear ) );
                    candles.Add( new TACandleViewModel( TACandle.CdlShootingStar ) );

                    List< TADivergenceViewModel > divergence = new List< TADivergenceViewModel >( );
                    divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW ) );
                    divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
                    divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
                    divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
                    divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
                    divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
                    divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
                    divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );

                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 2 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 3 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 4 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 6 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromHours( 8 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromDays( 1 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromDays( 7 ), candles, divergence );
                    _tradingEvents.InternalInitializeEvent( TimeSpan.FromDays( 30 ), candles, divergence );
                }
                
                return _tradingEvents;
            }
            
        }

        public FxTradingEventsMsgBindingList GetTradingEventsMsgBindingList( Security security )
        {
            return GetTradingEventsMsgBindingList( security.Code );
        }

        public FxTradingEventsMsgBindingList GetTradingEventsMsgBindingList( string symbol )
        {
            FxTradingEventsMsgBindingList tradingEventsBindingList = null;

            if( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
            {
                return tradingEventsBindingList;
            }

            var itemsSource = new ObservableCollectionEx< FxTradingEventsMsg >( );
            FxTradingEventsMsgBindingList eventsBindingList = new FxTradingEventsMsgBindingList( itemsSource );

            if( _tradingEventsMsgForSymbol.TryAdd( symbol, eventsBindingList ) )
            {
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 30 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 8 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 6 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 3 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ) );
                eventsBindingList.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ) );

                return eventsBindingList;
            }
            else
            {
                if( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
                {
                    return tradingEventsBindingList;
                }
            }

            throw new KeyNotFoundException( );
        }

        public Indicator DailyPivotPoint
        {
            get
            {
                return _dailyPivots;
            }

            set
            {
                _dailyPivots = value;
            }
        }

        public Indicator WeeklyPivotPoint
        {
            get
            {
                return _weeklyPivots;
            }

            set
            {
                _weeklyPivots = value;
            }
        }

        public Indicator MonthlyPivotPoint
        {
            get
            {
                return _monthlyPivots;
            }

            set
            {
                _monthlyPivots = value;
            }
        }
               

        public IPeriodXTaManager Period01SecTa
        {
            get
            {
                if ( _1SecForSymbol == null )
                {
                    _1SecForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromSeconds( 1 ) );
                }

                return _1SecForSymbol;

            }
        }

        public IPeriodXTaManager Period01MinTa
        {
            get
            {
                if ( _1MinTaForSymbol == null )
                {
                    _1MinTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromMinutes( 1 ) );
                }

                return _1MinTaForSymbol;

            }
        }


        public IPeriodXTaManager Period04MinTa
        {
            get
            {
                if ( _4MinTaForSymbol == null )
                {
                    _4MinTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromMinutes( 4 ) );
                }

                return _4MinTaForSymbol;

            }
        }

        public IPeriodXTaManager Period05MinTa
        {
            get
            {
                if ( _5MinTaForSymbol == null )
                {
                    _5MinTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromMinutes( 5 ) );
                }

                return _5MinTaForSymbol;

            }
        }

        public IPeriodXTaManager Period15MinTa
        {
            get
            {
                if ( _15MinTaForSymbol == null )
                {
                    _15MinTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromMinutes( 15 ) );
                }

                return _15MinTaForSymbol;

            }
        }

        public IPeriodXTaManager Period30MinTa
        {
            get
            {
                if ( _30MinTaForSymbol == null )
                {
                    _30MinTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromMinutes( 30 ) );
                }

                return _30MinTaForSymbol;

            }
        }

        public IPeriodXTaManager Period01HourTa
        {
            get
            {
                if ( _01HourTaForSymbol == null )
                {
                    _01HourTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromHours( 1 ) );
                }

                return _01HourTaForSymbol;

            }
        }

        public IPeriodXTaManager Period02HourTa
        {
            get
            {
                if ( _02HourTaForSymbol == null )
                {
                    _02HourTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromHours( 2 ) );
                }

                return _02HourTaForSymbol;

            }
        }

        public IPeriodXTaManager Period03HourTa
        {
            get
            {
                if ( _03HourTaForSymbol == null )
                {
                    _03HourTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromHours( 3 ) );
                }

                return _03HourTaForSymbol;

            }
        }

        public IPeriodXTaManager Period04HourTa
        {
            get
            {
                if ( _04HourTaForSymbol == null )
                {
                    _04HourTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromHours( 4 ) );
                }

                return _04HourTaForSymbol;

            }
        }


        public IPeriodXTaManager Period06HourTa
        {
            get
            {
                if ( _06HourTaForSymbol == null )
                {
                    _06HourTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromHours( 6 ) );
                }

                return _06HourTaForSymbol;

            }
        }

        public IPeriodXTaManager Period08HourTa
        {
            get
            {
                if ( _08HourTaForSymbol == null )
                {
                    _08HourTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromHours( 8 ) );
                }

                return _08HourTaForSymbol;

            }
        }


        public IPeriodXTaManager PeriodDailyTa
        {
            get
            {
                if ( _dailyTaForSymbol == null )
                {
                    _dailyTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromDays( 1 ) );
                }

                return _dailyTaForSymbol;

            }
        }


        public IPeriodXTaManager PeriodWeeklyTa
        {
            get
            {
                if ( _weeklyTaForSymbol == null )
                {
                    _weeklyTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromDays( 7 ) );
                }

                return _weeklyTaForSymbol;

            }
        }

        public IPeriodXTaManager PeriodMonthlyTa
        {
            get
            {
                if ( _monthlyTaForSymbol == null )
                {
                    _monthlyTaForSymbol = new PeriodXTaManager( _symbol, TimeSpan.FromDays( 30 ) );
                }

                return _monthlyTaForSymbol;

            }
        }

        
        

        public IPeriodXTaManager GetPeriodXTa( TimeSpan period )
        {
            if( period == TimeSpan.FromSeconds( 1 ) )
            {
                return Period01SecTa;
            }
            else if( period == TimeSpan.FromMinutes( 1 ) )
            {
                return Period01MinTa;
            }
            else if( period == TimeSpan.FromMinutes( 4 ) )
            {
                return Period04MinTa;
            }
            else if( period == TimeSpan.FromMinutes( 5 ) )
            {
                return Period05MinTa;
            }
            else if( period == TimeSpan.FromMinutes( 15 ) )
            {
                return Period15MinTa;
            }
            else if( period == TimeSpan.FromMinutes( 30 ) )
            {
                return Period30MinTa;
            }
            else if( period == TimeSpan.FromHours( 1 ) )
            {
                return Period01HourTa;
            }
            else if( period == TimeSpan.FromHours( 2 ) )
            {
                return Period02HourTa;
            }
            else if( period == TimeSpan.FromHours( 3 ) )
            {
                return Period03HourTa;
            }
            else if( period == TimeSpan.FromHours( 4 ) )
            {
                return Period04HourTa;
            }
            else if( period == TimeSpan.FromHours( 6 ) )
            {
                return Period06HourTa;
            }
            else if( period == TimeSpan.FromHours( 8 ) )
            {
                return Period08HourTa;
            }
            else if( period == TimeSpan.FromDays( 1 ) )
            {
                return PeriodDailyTa;
            }
            else if( period == TimeSpan.FromDays( 7 ) )
            {
                return PeriodWeeklyTa;
            }
            else if( period == TimeSpan.FromDays( 30 ) )
            {
                return PeriodMonthlyTa;
            }

            return null;
        }

        public int GetNumOfBarsForSmaCross( TimeSpan period )
        {
            if( period <= TimeSpan.FromMinutes( 5 ) )
            {
                return 6;
            }
            else if( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 3;
            }
            else if( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 1;
            }
            else if( period >= TimeSpan.FromHours( 1 ) )
            {
                return 1;
            }

            return 1;
        }

        public ObservableCollectionEx< FxNewsEvent > EconomicCalenderItemSource
        {
            get
            {                
                return _economicCalenderItemSource;
            }
        }

        

        public FxEconomicCalendarBindingList EconomicCalenderBindingList
        {
            get
            {
                if ( _economicCalender == null )
                {
                    _economicCalenderItemSource = new ObservableCollectionEx<FxNewsEvent>( );

                    _economicCalender = new FxEconomicCalendarBindingList( _economicCalenderItemSource );
                }
                
                return _economicCalender;
            }
            
            set
            {
                _economicCalender = value;
            }
        }        

        public FxCurrentWaveInfoBindingList GetCurrentWaveInfoBindingList( string symbol )
        {
            FxCurrentWaveInfoBindingList currentWaveInfoBindingList = null;

            if( _currentWaveInfoForSymbol.TryGetValue( symbol, out currentWaveInfoBindingList ) )
            {
                return currentWaveInfoBindingList;
            }

            var itemsSource = new ObservableCollectionEx< FxCurrentWaveInfo >( );
            var eventsBindingList = new FxCurrentWaveInfoBindingList( itemsSource );

            if( _currentWaveInfoForSymbol.TryAdd( symbol, eventsBindingList ) )
            {
                return eventsBindingList;
            }
            else
            {
                if( _currentWaveInfoForSymbol.TryGetValue( symbol, out currentWaveInfoBindingList ) )
                {
                    return currentWaveInfoBindingList;
                }
            }

            throw new KeyNotFoundException( );
        }

        // Tony Lam: This is going to be the most important function of the whole integration. It will try to get every indicators, pivot point, waves and time to calcaulte the probability of the trend

        public void RecalculateTrendProbability( TimeSpan changedTime )
        {
        }
    }
}
