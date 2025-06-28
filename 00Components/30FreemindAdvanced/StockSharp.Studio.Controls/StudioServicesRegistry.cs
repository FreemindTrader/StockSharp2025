// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StudioServicesRegistry
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using Ecng.Configuration;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;

#nullable disable
namespace StockSharp.Studio.Controls;

public static class StudioServicesRegistry
{
    public static IStudioCommandService CommandService => StudioCommandHelper.Service;

    public static PortfolioDataSource PortfolioDataSource => PortfolioDataSource.Instance;

    public static IPriceChartDataProvider PriceChartDataProvider
    {
        get => ConfigManager.GetService<IPriceChartDataProvider>();
    }
}
