using fx.Algorithm;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freemind.Strategies
{
    public class FreemindStrategy : Strategy
    {
        //ICandleManager _candleManager = null;

        //CandleSeries _series = null;

        AdvancedAnalysisManager _AdvAnalysisMgr = null;

        public FreemindStrategy( AdvancedAnalysisManager advMgr )
        {
            ServicesRegistry.LogManager.Sources.Add( this );
            _AdvAnalysisMgr = advMgr;            
        }

        protected override void OnStarted( )
        {           
            //_candleManager = Connector;

            //_candleManager.Processing += CandleManager_Processing;

            _AdvAnalysisMgr.MacdCrossEvent += _AdvAnalysisMgr_MacdCrossEvent;

            _AdvAnalysisMgr.MacdValueChangedEvent += _AdvAnalysisMgr_MacdValueChangedEvent;

            base.OnStarted( );
        }

        private void _AdvAnalysisMgr_MacdValueChangedEvent( object sender, fx.Definitions.MacdValueEventArgs e )
        {
            //this.LogDebug( "MACD Changed, Period ={0}, Macd = {1} Signal ={2}", e.Period, e.Macd, e.MacdSignal );
        }

        private void _AdvAnalysisMgr_MacdCrossEvent( object sender, fx.Definitions.MacdCrossEventArgs e )
        {
            this.LogInfo( "MACD crossed detected, Period ={0}, Direction = {1} CrossIndex = {2}", e.Period, e.Direction, e.CrossIndex );
            
        }

        private void CandleManager_Processing( CandleSeries series, Candle candle )
        {
            if( candle.State != StockSharp.Messages.CandleStates.Finished )
                return;


        }

        protected override void OnStopped( )
        {
            base.OnStopped( );
        }        
    }
}
