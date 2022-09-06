using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Studio.Core.Commands;
using fx.Charting;
using System;
using System.Collections.Generic;
using fx.Algorithm;
using static fx.Charting.ChartDrawDataEx;
using fx.Collections;
using fx.Bars;
using SciChart.Charting.Model.DataSeries;
using fx.Base;
using StockSharp.Logging;
using fx.Definitions;
using StockSharp.Messages;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        DateTime _reloadCandleFromDate = DateTime.MinValue;
        DateTime _reloadCandleToDate = DateTime.MinValue;        

        public override void Step9_OnCandleStruct( ref CandleStruct cmd, bool endOfBatch )
        {            
            if ( !_candles.TryGetValue( cmd.Series, out CandleSeriesData seriesData ) )
            {
                return;
            }

            _candlesSeries = cmd.Series;

            _candleBuffer.Add( cmd.Candle );

            if ( !_bars.DoneLoading ) return;

            if ( endOfBatch )
            {
                if ( ResponsibleTF == TimeSpan.FromMinutes( 1 ) )
                {
                    //continue;
                }

                (uint first, uint last ) barsRange = default;

                if ( _candleBuffer.Count > 2 && _reloadCandleFromDate != DateTime.MinValue && _reloadCandleToDate != DateTime.MinValue )
                {
                    var first = _candleBuffer[ 0 ].OpenTime.UtcDateTime;
                    var last = _candleBuffer[ _candleBuffer.Count -1  ].OpenTime.UtcDateTime;

                    if ( first >= _reloadCandleFromDate && last <= _reloadCandleToDate )
                    {
                        barsRange = _bars.AddReplaceCandlesRange( SelectedSecurity, _candleBuffer, ResponsibleTF, _waveScenarioNumber, true );
                    }
                    else
                    {
                        barsRange = _bars.AddReplaceCandlesRange( SelectedSecurity, _candleBuffer, ResponsibleTF, _waveScenarioNumber );
                    }
                }
                else
                {
                    barsRange = _bars.AddReplaceCandlesRange( SelectedSecurity, _candleBuffer, ResponsibleTF, _waveScenarioNumber );
                }
                    
                if ( _isNonVisual )
                {
                    return;
                }
                 

                if ( barsRange != default )
                {
                    string sendMsg = string.Format( "[{0}] ({1}) : Candle Command Batch End. Count = [{2}] ...", SelectedSecurity.Code, ResponsibleTF.ToReadable(), ( barsRange.last - barsRange.first ) );

                    LoggingHelper.AddInfoLog( this, sendMsg );

                    _candleBuffer.Clear();


                    if ( barsRange == default )
                        return;

                    InternalDrawCandles( cmd.Series, seriesData.CandleUI, _bars.MainDataBars, barsRange );
                }                
            }
        }

        public override void ReloadCandles()
        {
            ( TimeSpan, ( long from, long to)  ) range =  _bars.GetSelectedBarTimeRange();            

            var subscription      = new Subscription(_candlesSeries);

            _reloadCandleFromDate = range.Item2.from.FromLinuxTime().Date;
            _reloadCandleToDate   = range.Item2.to.FromLinuxTime().Date;

            var mdMsg             = (MarketDataMessage)subscription.SubscriptionMessage;
            mdMsg.From            = _reloadCandleFromDate;
            mdMsg.To              = _reloadCandleToDate.AddDays( 1 ).AddSeconds( -1 );
            mdMsg.Adapter         = null;            
            
            mdMsg.ExtensionInfo = new Dictionary<string, object>();
            mdMsg.ExtensionInfo.Add( "ReloadCandles", true );

            DeleteCandlesFromStorage( _reloadCandleFromDate, _reloadCandleToDate );

            _reloadCandleToDate = _reloadCandleToDate.AddDays( 1 ).AddSeconds( -1 );

            _connector.Subscribe( subscription );            
        }

        public void RequestMarketDataFromLastBar( DateTimeOffset lastBarTime )
        {
            var subscription      = new Subscription(_candlesSeries);

            var mdMsg             = (MarketDataMessage)subscription.SubscriptionMessage;
            mdMsg.From            = lastBarTime;
            mdMsg.To              = DateTime.UtcNow; ;
            mdMsg.Adapter         = null;


            /* --------------------------------------------------------------------------------------------------
            *  I need the liveTrading to download all the databar before send out message so that 
            *  Scichart, databars can be inserted all together.
            * 
            *  As for Octopus, I need it to send out databars for every day so that we can save all the everyday bars
            *  One day by one day, just in case we have a long time to redownload
            * 
            * -------------------------------------------------------------------------------------------------- 
            */
            mdMsg.ExtensionInfo = new Dictionary<string, object>();
            mdMsg.ExtensionInfo.Add( "FullDownload", true );

            _connector.Subscribe( subscription );
        }

    }


}
