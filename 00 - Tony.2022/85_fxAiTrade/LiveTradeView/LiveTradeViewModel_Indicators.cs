using fx.Charting;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Studio.Core.Commands;
using System;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        private void Step08_SubscribeIndicatorUIEventHandler( IndicatorUI element, CandleSeries series, IIndicator indicator )
        {
            var command = new ChartAddElementExCommand( element, series, indicator );
            if ( _loading )
            {
                Step5_OnChartAddUIsToChart( command );
            }
            else
            {
                command.Process( this, false );
            }

            OnUiChanged();
        }
    }
}