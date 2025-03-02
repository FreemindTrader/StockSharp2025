// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StudioServicesRegistry
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

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
                return PortfolioDataSource.Instance;
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
