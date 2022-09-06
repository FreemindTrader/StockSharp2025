using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using fx.Definitions;
using fx.Common;
using fx.Definitions.UndoRedo;
using fx.Algorithm;
using Ecng.Collections;
using StockSharp.Logging;
using fx.Indicators;
using StockSharp.Localization;
using fx.Charting;
using System.Windows.Media;
using StockSharp.Xaml;
using StockSharp.Studio.Core.Configuration;
using Ecng.Common;
using StockSharp.Studio.Core.Services;
using Ecng.Serialization;
using Ecng.Xaml;
using DevExpress.Xpf.Core;
using System;
using StockSharp.BusinessEntities;
using StockSharp.Algo;
using StockSharp.Algo.Testing;
using StockSharp.Algo.Candles;

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

        public IChartTabViewModel Create( IMutltiTimeFrameSessionDataRepo dataRepo, string caption, string imagePath, TimeSpan reponsible, Security sec, fxHistoricEmulationConnector connector, CandleManager manager, Portfolio portfolio, DateTime startDate, DateTime endDate, CancellationTokenSource exitToken)
        {
            return new BackTesterViewModel( dataRepo, connector, manager, caption, imagePath, reponsible, sec, portfolio, startDate, endDate, exitToken );
        }
    }
}
