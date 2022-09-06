//using System;
//using System.Collections.Generic;
//using fx.Common;
//using fx.Definitions;

//namespace fx.Algorithm
//{
//    /// <summary>
//    /// Class contains general helper functionality for financial classes and operation.
//    /// </summary>
//    public sealed partial class AdvancedAnalysisManager
//    {
//        public FxTradingEventsTsoCollection InitializeTradingEventsBindingList( string symbol )
//        {
//            FxTradingEventsTsoCollection tradingEventsBindingList = null;

//            if ( _tradingEventsForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {                
//                return tradingEventsBindingList;
//            }

//            var itemsSource       = new ObservableCollectionEx<FxTradingEvents>( );

//            _tradingEventsForSymbolItemSource.TryAdd( symbol, itemsSource );

//            var eventsBindingList = new FxTradingEventsTsoCollection( this, itemsSource, symbol );


//            PooledList<TACandleViewModel> candles = new PooledList<TACandleViewModel>( );
//            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlEveningStar ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlDoji ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlHammer ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlMorningStar ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlPiercing ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlShootingStar ) );


//            PooledList<TADivergenceViewModel> divergence = new PooledList<TADivergenceViewModel>( );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
//            

//            if ( _tradingEventsForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 30 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromTicks( 1 ), candles, divergence );

//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _tradingEventsForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//                {                    
//                    return tradingEventsBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public FxTradingEventsTsoCollection InitializeTradingEventsBindingList( string symbol, PooledList<TimeSpan> selectedTF )
//        {
//            FxTradingEventsTsoCollection tradingEventsBindingList = null;

//            if ( _tradingEventsForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {
//                return tradingEventsBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxTradingEvents>( );

//            _tradingEventsForSymbolItemSource.TryAdd( symbol, itemsSource );

//            var eventsBindingList = new FxTradingEventsTsoCollection( this, itemsSource, symbol );

//            PooledList<TACandleViewModel> candles = new PooledList<TACandleViewModel>( );
//            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlEveningStar ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlDoji ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlHammer ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlMorningStar ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlPiercing ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlShootingStar ) );

//            PooledList<TADivergenceViewModel> divergence = new PooledList<TADivergenceViewModel>( );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );

//            if ( _tradingEventsForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                foreach ( var tf in selectedTF )
//                {
//                    eventsBindingList.InternalInitializeEvent( tf, candles, divergence );
//                }                

//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _tradingEventsForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//                {
//                    return tradingEventsBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }


//        public void ResetTradingEventsBindingList( string symbol )
//        {
//            FxTradingEventsTsoCollection tradingEventsBindingList = null;

//            if ( _tradingEventsForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {
//                tradingEventsBindingList.ResetIndicatorList( );
//            }
//        }


//        public FxBBstrengthBindingList InitializeBBstrengthBindingList( string symbol )
//        {
//            FxBBstrengthBindingList tradingEventsBindingList = null;

//            if ( _bbStrengthForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {                
//                return tradingEventsBindingList;
//            }

//            var itemsSource       = new ObservableCollectionEx<FxBBstrength>( );
//            var eventsBindingList = new FxBBstrengthBindingList( itemsSource );
//            

//            if ( _bbStrengthForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ), 9 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ), 8 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ), 7 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ), 6 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ), 5 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), 4 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), 3 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), 2 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), 1 );


//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _bbStrengthForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//                {                    
//                    return tradingEventsBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public FxBBstrengthBindingList GetBBstrengthBindingList( string symbol )
//        {
//            FxBBstrengthBindingList tradingEventsBindingList = null;

//            if ( _bbStrengthForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {
//                return tradingEventsBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxBBstrength>( );
//            var eventsBindingList = new FxBBstrengthBindingList( itemsSource );            

//            if ( _bbStrengthForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ), 14 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ), 13 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 8 ), 12 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 6 ), 11 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ), 10 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 3 ), 9 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ), 8 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 1 ), 7 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), 6 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), 5 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), 4 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ), 3 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), 2 );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ), 1 );

//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _bbStrengthForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//                {                    
//                    return tradingEventsBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }
//        

//        public FxTradingEventsMsgBindingList InitializeTradingEventsMsgBindingList( string symbol )
//        {
//            FxTradingEventsMsgBindingList tradingEventsBindingList = null;

//            if ( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {                
//                return tradingEventsBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxTradingEventsMsg>( );
//            FxTradingEventsMsgBindingList eventsBindingList = new FxTradingEventsMsgBindingList( itemsSource );

//            if ( _tradingEventsMsgForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                var supportedTF = FinancialHelper.SupportedTimeSpan;

//                foreach ( var tf in supportedTF )
//                {
//                    eventsBindingList.InternalInitializeEvent( tf );
//                }

//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 30 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 8 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 6 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 3 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ) );
//                //eventsBindingList.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ) );

//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//                {
//                    return tradingEventsBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        



//        public SRlevelsTsoCollection GetSupportResistanceBindingList( string symbol )
//        {
//            SRlevelsTsoCollection SRlevelsTsoCollection = null;

//            if ( _supportResistanceForSymbol.TryGetValue( symbol, out SRlevelsTsoCollection ) )
//            {                
//                return SRlevelsTsoCollection;
//            }

//            var itemsSource = new ObservableCollectionEx<SRlevel>( );

//            _supportResistanceForSymbolItemSource.TryAdd( symbol, itemsSource );

//            SRlevelsTsoCollection = new SRlevelsTsoCollection( itemsSource );

//            if ( _supportResistanceForSymbol.TryAdd( symbol, SRlevelsTsoCollection ) )
//            {
//                return SRlevelsTsoCollection;
//            }
//            else
//            {
//                if ( _supportResistanceForSymbol.TryGetValue( symbol, out SRlevelsTsoCollection ) )
//                {                    
//                    return SRlevelsTsoCollection;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }
//               

//        public FxBarPercentageBindingList GetBarPercentageBindingList( string symbol )
//        {
//            FxBarPercentageBindingList barPercentBindingList = null;

//            if ( _05MinBarPercentageSymbol.TryGetValue( symbol, out barPercentBindingList ) )
//            {
//                return barPercentBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxBarPercentage>( );

//            _barPercentageItemSource.TryAdd( symbol, itemsSource );
//             
//            barPercentBindingList = new FxBarPercentageBindingList( itemsSource );

//            if ( _05MinBarPercentageSymbol.TryAdd( symbol, barPercentBindingList ) )
//            {
//                barPercentBindingList.InternalInitializeEvent( 1 );
//                barPercentBindingList.InternalInitializeEvent( 2 );
//                barPercentBindingList.InternalInitializeEvent( 3 );
//                barPercentBindingList.InternalInitializeEvent( 4 );
//                barPercentBindingList.InternalInitializeEvent( 5 );

//                return barPercentBindingList;
//            }
//            else
//            {
//                if ( _05MinBarPercentageSymbol.TryGetValue( symbol, out barPercentBindingList ) )
//                {
//                    return barPercentBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }
//        

//        public ObservableCollectionEx<FxTradingEvents> GetTradingEventsItemSource( string symbol )
//        {
//            ObservableCollectionEx<FxTradingEvents> itemSource = null;

//            if ( _tradingEventsForSymbolItemSource.TryGetValue( symbol, out itemSource ) )
//            {

//                return itemSource;
//            }

//            return null;
//        }

//        public ObservableCollectionEx<SRlevel> GetSRLevelsItemSource( string symbol )
//        {
//            ObservableCollectionEx<SRlevel> itemSource = null;

//            if ( _supportResistanceForSymbolItemSource.TryGetValue( symbol, out itemSource ) )
//            {

//                return itemSource;
//            }

//            return null;
//        }

//        public AdvancedAnalysisResult GetAnalysisResult( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            AdvancedAnalysisResult analysisResult = null;

//            if ( _advancedAnalysisResultForSymbol.TryGetValue( symbol, out analysisResult ) )
//            {

//                return analysisResult;
//            }

//            analysisResult = new AdvancedAnalysisResult( this );

//            if ( _advancedAnalysisResultForSymbol.TryAdd( symbol, analysisResult ) )
//            {
//                return analysisResult;
//            }
//            else
//            {
//                if ( _advancedAnalysisResultForSymbol.ContainsKey( symbol ) )
//                {
//                    return _advancedAnalysisResultForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public FxTradingEventsTsoCollection GetTradingEventsBindingList( string symbol )
//        {
//            FxTradingEventsTsoCollection tradingEventsBindingList = null;

//            if ( _tradingEventsForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {

//                return tradingEventsBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxTradingEvents>( );

//            _tradingEventsForSymbolItemSource.TryAdd( symbol, itemsSource );

//            FxTradingEventsTsoCollection eventsBindingList = new FxTradingEventsTsoCollection( this, itemsSource, symbol );

//            PooledList<TACandleViewModel> candles = new PooledList<TACandleViewModel>( );
//            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBear ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlEngulfingBull ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlDarkCloudCover ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlEveningStar ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlDoji ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlHammer ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlMorningStar ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlPiercing ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlTriStarBear ) );
//            candles.Add( new TACandleViewModel( TACandle.CdlShootingStar ) );

//            PooledList<TADivergenceViewModel> divergence = new PooledList<TADivergenceViewModel>( );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );
//            divergence.Add( new TADivergenceViewModel( TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM ) );

//            if ( _tradingEventsForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 30 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 8 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 6 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 3 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ), candles, divergence );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ), candles, divergence );

//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _tradingEventsForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//                {
//                    return tradingEventsBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }
//        
//        public FxTradingEventsMsgBindingList GetTradingEventsMsgBindingList( string symbol )
//        {
//            FxTradingEventsMsgBindingList tradingEventsBindingList = null;

//            if ( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//            {
//                return tradingEventsBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxTradingEventsMsg>( );
//            FxTradingEventsMsgBindingList eventsBindingList = new FxTradingEventsMsgBindingList( itemsSource );



//            if ( _tradingEventsMsgForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 30 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 7 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromDays( 1 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 8 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 6 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 4 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 3 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromHours( 2 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 60 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 30 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 15 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 5 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 4 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromMinutes( 1 ) );
//                eventsBindingList.InternalInitializeEvent( TimeSpan.FromSeconds( 1 ) );

//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _tradingEventsMsgForSymbol.TryGetValue( symbol, out tradingEventsBindingList ) )
//                {
//                    return tradingEventsBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public void AddDailyPivotPoint( string instrument, Indicator pivotPoint )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _dailyPivotsForSymbol.ContainsKey( symbol ) )
//            {
//                _dailyPivotsForSymbol[ symbol ] = pivotPoint;
//            }
//            else
//            {
//                _dailyPivotsForSymbol.TryAdd( symbol, pivotPoint );
//            }
//        }

//        public Indicator GetDailyPivotPointIndicator( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _dailyPivotsForSymbol.ContainsKey( symbol ) )
//            {
//                return _dailyPivotsForSymbol[ symbol ];
//            }

//            return null;
//        }

//        public void AddWeeklyPivotPoint( string instrument, Indicator pivotPoint )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _weeklyPivotsForSymbol.ContainsKey( symbol ) )
//            {
//                _weeklyPivotsForSymbol[ symbol ] = pivotPoint;
//            }
//            else
//            {
//                _weeklyPivotsForSymbol.TryAdd( symbol, pivotPoint );
//            }
//        }

//        public Indicator GetWeeklyPivotPointIndicator( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _weeklyPivotsForSymbol.ContainsKey( symbol ) )
//            {
//                return _weeklyPivotsForSymbol[ symbol ];
//            }

//            return null;
//        }

//        public void AddMonthlyPivotPoint( string instrument, Indicator pivotPoint )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _monthlyPivotsForSymbol.ContainsKey( symbol ) )
//            {
//                _monthlyPivotsForSymbol[ symbol ] = pivotPoint;
//            }
//            else
//            {
//                _monthlyPivotsForSymbol.TryAdd( symbol, pivotPoint );
//            }
//        }

//        public Indicator GetMonthlyPivotPointIndicator( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _monthlyPivotsForSymbol.ContainsKey( symbol ) )
//            {
//                return _monthlyPivotsForSymbol[ symbol ];
//            }

//            return null;
//        }

//        public PeriodXTaManager GetPeriod01SecTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _1SecForSymbol.ContainsKey( symbol ) )
//            {
//                return _1SecForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager oneSecondBar = new PeriodXTaManager( symbol, TimeSpan.FromSeconds( 1 ) );

//            if ( _1SecForSymbol.TryAdd( symbol, oneSecondBar ) )
//            {
//                return oneSecondBar;
//            }
//            else
//            {
//                if ( _1SecForSymbol.ContainsKey( symbol ) )
//                {
//                    return _1SecForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod01MinTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _1MinTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _1MinTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager oneMinuteBar = new PeriodXTaManager( symbol, TimeSpan.FromMinutes( 1 ) );

//            if ( _1MinTaForSymbol.TryAdd( symbol, oneMinuteBar ) )
//            {
//                return oneMinuteBar;
//            }
//            else
//            {
//                if ( _1MinTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _1MinTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod04MinTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _4MinTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _4MinTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromMinutes( 4 ) );

//            if ( _4MinTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _4MinTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _4MinTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod05MinTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _5MinTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _5MinTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromMinutes( 5 ) );

//            if ( _5MinTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _5MinTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _5MinTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }


//        public PeriodXTaManager GetPeriod15MinTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _15MinTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _15MinTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromMinutes( 15 ) );

//            if ( _15MinTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _15MinTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _15MinTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod30MinTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _30MinTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _30MinTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromMinutes( 30 ) );

//            if ( _30MinTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _30MinTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _30MinTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod01HourTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _01HourTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _01HourTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromHours( 1 ) );

//            if ( _01HourTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _01HourTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _01HourTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod02HourTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _02HourTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _02HourTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromHours( 2 ) );

//            if ( _02HourTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _02HourTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _02HourTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod03HourTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _03HourTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _03HourTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromHours( 3 ) );

//            if ( _03HourTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _03HourTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _03HourTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod04HourTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _04HourTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _04HourTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromHours( 4 ) );

//            if ( _04HourTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _04HourTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _04HourTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod06HourTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _06HourTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _06HourTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromHours( 6 ) );

//            if ( _06HourTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _06HourTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _06HourTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriod08HourTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _08HourTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _08HourTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromHours( 8 ) );

//            if ( _08HourTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _08HourTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _08HourTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriodDailyTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _dailyTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _dailyTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromDays( 1 ) );

//            if ( _dailyTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _dailyTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _dailyTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriodWeeklyTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _weeklyTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _weeklyTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromDays( 7 ) );

//            if ( _weeklyTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _weeklyTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _weeklyTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }


//        public PeriodXTaManager GetPeriodMonthlyTa( string instrument )
//        {
//            var symbol = FinancialHelper.GetNormalizedSymbol( instrument );

//            if ( _monthlyTaForSymbol.ContainsKey( symbol ) )
//            {
//                return _monthlyTaForSymbol[ symbol ];
//            }

//            // The total number of 1 minutes per one trading week is 7320 minutes, we will add 30 more minutes to it. Making it 7350
//            // We will store one minute databar for previous week + this week's one minute bar
//            PeriodXTaManager manager = new PeriodXTaManager( symbol, TimeSpan.FromDays( 30 ) );

//            if ( _monthlyTaForSymbol.TryAdd( symbol, manager ) )
//            {
//                return manager;
//            }
//            else
//            {
//                if ( _monthlyTaForSymbol.ContainsKey( symbol ) )
//                {
//                    return _monthlyTaForSymbol[ symbol ];
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        public PeriodXTaManager GetPeriodXTa( string symbol, TimeSpan period )
//        {
//            PeriodXTaManager periodXTa = null;

//            var advAnalysisMgr = AdvancedAnalysisManager.Instance;

//            if ( period == TimeSpan.FromSeconds( 1 ) )
//            {
//                return advAnalysisMgr.GetPeriod01SecTa( symbol );
//            }
//            else if ( period == TimeSpan.FromMinutes( 1 ) )
//            {
//                return advAnalysisMgr.GetPeriod01MinTa( symbol );
//            }
//            else if ( period == TimeSpan.FromMinutes( 4 ) )
//            {
//                return advAnalysisMgr.GetPeriod04MinTa( symbol );
//            }
//            else if ( period == TimeSpan.FromMinutes( 5 ) )
//            {
//                return advAnalysisMgr.GetPeriod05MinTa( symbol );
//            }
//            else if ( period == TimeSpan.FromMinutes( 15 ) )
//            {
//                return advAnalysisMgr.GetPeriod15MinTa( symbol );
//            }
//            else if ( period == TimeSpan.FromMinutes( 30 ) )
//            {
//                return advAnalysisMgr.GetPeriod30MinTa( symbol );
//            }
//            else if ( period == TimeSpan.FromHours( 1 ) )
//            {
//                return advAnalysisMgr.GetPeriod01HourTa( symbol );
//            }
//            else if ( period == TimeSpan.FromHours( 2 ) )
//            {
//                return advAnalysisMgr.GetPeriod02HourTa( symbol );
//            }
//            else if ( period == TimeSpan.FromHours( 3 ) )
//            {
//                return advAnalysisMgr.GetPeriod03HourTa( symbol );
//            }
//            else if ( period == TimeSpan.FromHours( 4 ) )
//            {
//                return advAnalysisMgr.GetPeriod04HourTa( symbol );
//            }
//            else if ( period == TimeSpan.FromHours( 6 ) )
//            {
//                return advAnalysisMgr.GetPeriod06HourTa( symbol );
//            }
//            else if ( period == TimeSpan.FromHours( 8 ) )
//            {
//                return advAnalysisMgr.GetPeriod08HourTa( symbol );
//            }
//            else if ( period == TimeSpan.FromDays( 1 ) )
//            {
//                return advAnalysisMgr.GetPeriodDailyTa( symbol );
//            }
//            else if ( period == TimeSpan.FromDays( 7 ) )
//            {
//                return advAnalysisMgr.GetPeriodWeeklyTa( symbol );
//            }
//            else if ( period == TimeSpan.FromDays( 30 ) )
//            {
//                return advAnalysisMgr.GetPeriodMonthlyTa( symbol );
//            }

//            return periodXTa;
//        }

//        public int GetNumOfBarsForSmaCross( TimeSpan period )
//        {
//            if ( period <= TimeSpan.FromMinutes( 5 ) )
//            {
//                return 6;
//            }
//            else if ( period == TimeSpan.FromMinutes( 15 ) )
//            {
//                return 3;
//            }
//            else if ( period == TimeSpan.FromMinutes( 30 ) )
//            {
//                return 1;
//            }
//            else if ( period >= TimeSpan.FromHours( 1 ) )
//            {
//                return 1;
//            }

//            return 1;
//        }

//        

//        public ObservableCollectionEx<FxNewsEvent> GetEconomicCalenderItemSource( string symbol )
//        {
//            ObservableCollectionEx<FxNewsEvent> itemSource = null;

//            if ( _symbolsToEconomicCalenderItemSource.TryGetValue( symbol, out itemSource ) )
//            {

//                return itemSource;
//            }

//            return null;
//        }

//        public ObservableCollectionEx<FxBarPercentage> GetBarPercentageItemSource( string symbol )
//        {
//            ObservableCollectionEx<FxBarPercentage> itemSource = null;

//            if ( _barPercentageItemSource.TryGetValue( symbol, out itemSource ) )
//            {

//                return itemSource;
//            }

//            return null;
//        }

//        public FxEconomicCalendarBindingList GetEconomicCalenderBindingList( string symbol )
//        {
//            FxEconomicCalendarBindingList economicCalenderBindingList = null;

//            if ( _symbolsToEconomicCalender.TryGetValue( symbol, out economicCalenderBindingList ) )
//            {
//                return economicCalenderBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxNewsEvent>( );

//            _symbolsToEconomicCalenderItemSource.TryAdd( symbol, itemsSource );

//            var eventsBindingList = new FxEconomicCalendarBindingList( itemsSource );

//            if ( _symbolsToEconomicCalender.TryAdd( symbol, eventsBindingList ) )
//            {
//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _symbolsToEconomicCalender.TryGetValue( symbol, out economicCalenderBindingList ) )
//                {
//                    return economicCalenderBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }

//        

//        public bool SetEconomicCalenderBindingList( string symbol, FxEconomicCalendarBindingList newOne )
//        {
//            return ( _symbolsToEconomicCalender.TryAddOrReplace( symbol, newOne ) );
//        }

//        public FxCurrentWaveInfoBindingList GetCurrentWaveInfoBindingList( string symbol )
//        {
//            FxCurrentWaveInfoBindingList currentWaveInfoBindingList = null;

//            if ( _currentWaveInfoForSymbol.TryGetValue( symbol, out currentWaveInfoBindingList ) )
//            {
//                return currentWaveInfoBindingList;
//            }

//            var itemsSource = new ObservableCollectionEx<FxCurrentWaveInfo>( );
//            var eventsBindingList = new FxCurrentWaveInfoBindingList( itemsSource );



//            if ( _currentWaveInfoForSymbol.TryAdd( symbol, eventsBindingList ) )
//            {
//                return eventsBindingList;
//            }
//            else
//            {
//                if ( _currentWaveInfoForSymbol.TryGetValue( symbol, out currentWaveInfoBindingList ) )
//                {
//                    return currentWaveInfoBindingList;
//                }
//            }

//            throw new KeyNotFoundException( );
//        }
//        

//        // Tony Lam: This is going to be the most important function of the whole integration. It will try to get every indicators, pivot point, waves and time to calcaulte the probability of the trend

//        public void RecalculateTrendProbability( TimeSpan changedTime )
//        {

//        }
//    }
//}
