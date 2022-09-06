
using Ecng.Configuration;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;

namespace StockSharp.Studio.Controls
{
    public static class StudioServicesRegistry
    {
        public static IStudioCommandService CommandService
        {
            get
            {
                return StudioCommandHelper.Service;
            }
        }

        public static PortfolioDataSource PortfolioDataSource
        {
            get
            {
                return ConfigManager.GetService<PortfolioDataSource>();
            }
        }

        public static IPriceChartDataProvider PriceChartDataProvider
        {
            get
            {
                return ConfigManager.GetService<IPriceChartDataProvider>();
            }
        }
    }
}
