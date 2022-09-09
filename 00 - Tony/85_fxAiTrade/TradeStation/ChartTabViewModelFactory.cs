using fx.Algorithm;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using System;
using System.Threading;

#pragma warning disable CS0618

namespace FreemindAITrade.ViewModels
{
    public class ChartTabViewModelFactory
    {
        public IChartTabViewModel Create( IMutltiTimeFrameSessionDataRepo dataRepo, string caption, string imagePath, TimeSpan reponsible, Security sec, Connector connector, bool isBarIntegrityCheck, bool loadAll, CancellationTokenSource exitToken )
        {
            var output = new LiveTradeViewModel( dataRepo, caption, imagePath, reponsible, sec, connector, isBarIntegrityCheck, 1, false, loadAll, exitToken );

            ServicesRegistry.LogManager.Sources.Add( output );

            return output;
        }

        public IChartTabViewModel CreateAlt( IMutltiTimeFrameSessionDataRepo dataRepo, string caption, string imagePath, TimeSpan reponsible, Security sec, Connector connector, bool isBarIntegrityCheck, int count, CancellationTokenSource exitToken )
        {
            var output = new LiveTradeViewModel( dataRepo, caption, imagePath, reponsible, sec, connector, isBarIntegrityCheck, count, false, false, exitToken );

            ServicesRegistry.LogManager.Sources.Add( output );

            return output;
        }

        public IChartTabViewModel CreateNonVisual( IMutltiTimeFrameSessionDataRepo dataRepo, string caption, string imagePath, TimeSpan reponsible, Security sec, Connector connector, bool isBarIntegrityCheck, CancellationTokenSource exitToken )
        {
            var output = new LiveTradeViewModel( dataRepo, caption, imagePath, reponsible, sec, connector, isBarIntegrityCheck, 1, true, false, exitToken );

            ServicesRegistry.LogManager.Sources.Add( output );

            return output;
        }

        public IChartTabViewModel Create( IMutltiTimeFrameSessionDataRepo dataRepo, string caption, string imagePath, TimeSpan reponsible, Security sec, fxHistoricEmulationConnector connector, CandleManager manager, Portfolio portfolio, DateTime startDate, DateTime endDate, CancellationTokenSource exitToken )
        {
            return new BackTesterViewModel( dataRepo, connector, manager, caption, imagePath, reponsible, sec, portfolio, startDate, endDate, exitToken );
        }
    }
}
