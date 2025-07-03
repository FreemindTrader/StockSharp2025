// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartHelper
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Logging;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Statistics;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#nullable disable
namespace StockSharp.Xaml.Charting;

public static class ChartHelper
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly Lazy<IndicatorColorProvider> \u0023\u003DzOejVh3VT5Dah = new Lazy<IndicatorColorProvider>();

  public static Stream SaveToImage(this IChart chart)
  {
    if (chart == null)
      throw new ArgumentNullException(nameof (chart));
    if (chart is Chart elem)
    {
      MemoryStream file = new MemoryStream();
      elem.GetImage().SaveImage((Stream) file);
      file.Position = 0L;
      return (Stream) file;
    }
    throw new ArgumentException(StringHelper.Put(LocalizedStrings.UnknownType, new object[1]
    {
      (object) chart.GetType()
    }), nameof (chart));
  }

  internal static string \u0023\u003DzNsF8TrDL0ndB(this Subscription _param0)
  {
    if (_param0 == null)
      return string.Empty;
    return StringHelper.Put(LocalizedStrings.ChartSeriesTitleParams, new object[2]
    {
      (object) _param0.SecurityId,
      _param0.DataType.Arg
    });
  }

  public static string CurrChartTheme()
  {
    return ApplicationThemeHelper.ApplicationThemeName.ToChartTheme();
  }

  public static string ToChartTheme(this string appTheme)
  {
    return !appTheme.IsDark() ? "Chrome" : "ExpressionDark";
  }

  public static void UpdateTheme(this IThemeableChart chart)
  {
    if (chart == null)
      throw new ArgumentNullException(nameof (chart));
    chart.ChartTheme = ChartHelper.CurrChartTheme();
  }

  public static IEnumerable<IndicatorType> GetIndicatorTypes()
  {
    return IChartExtensions.IndicatorProvider.All;
  }

  public static void FillDefaultValues(this OptimizerChart3D chart)
  {
    ChartHelper.\u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D skXz9pb7XdulaJda = new ChartHelper.\u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D();
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D = chart;
    if (skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D == null)
      throw new ArgumentNullException(nameof (chart));
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.X = skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.XValues.FirstOrDefault<IChart3DParameter>();
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.Y = skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.YValues.Where<IChart3DParameter>(new Func<IChart3DParameter, bool>(skXz9pb7XdulaJda.\u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D)).FirstOrDefault<IChart3DParameter>() ?? skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.YValues.FirstOrDefault<IChart3DParameter>();
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.Z = skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.ZValues.FirstOrDefault<IStatisticParameter>();
  }

  internal static Chart \u0023\u003DzW4D5ZlUSfXRJ(this IChartElement _param0)
  {
    return (Chart) _param0.TryGetChart();
  }

  public static IIndicator TryGetIndicator(this IChartIndicatorElement element)
  {
    return element.\u0023\u003DzW4D5ZlUSfXRJ()?.GetIndicator(element);
  }

  public static Subscription TryGetSubscription(this IChartElement element)
  {
    return element.\u0023\u003DzW4D5ZlUSfXRJ()?.TryGetSubscription(element);
  }

  public static IIndicatorColorProvider TryColorProvider
  {
    get => ConfigManager.TryGetService<IIndicatorColorProvider>();
  }

  public static IIndicatorColorProvider EnsureColorProvider()
  {
    return ChartHelper.TryColorProvider ?? (IIndicatorColorProvider) ChartHelper.\u0023\u003DzOejVh3VT5Dah.Value;
  }

  public static void DrawPnL(
    this EquityCurveChart chart,
    Strategy strategy,
    IChartBandElement pnl,
    IChartBandElement unrealized,
    IChartBandElement commission)
  {
    if (chart == null)
      throw new ArgumentNullException(nameof (chart));
    if (strategy == null)
      throw new ArgumentNullException(nameof (strategy));
    if (pnl == null)
      throw new ArgumentNullException(nameof (pnl));
    if (unrealized == null)
      throw new ArgumentNullException(nameof (unrealized));
    if (commission == null)
      throw new ArgumentNullException(nameof (commission));
    IChartDrawData data = chart.CreateData();
    data.Group(((BaseLogSource) strategy).CurrentTime).Add(pnl, strategy.PnL).Add(unrealized, strategy.PnLManager.UnrealizedPnL).Add(commission, strategy.Commission.GetValueOrDefault());
    chart.Draw(data);
  }

  [Obsolete("Use Subscription overload.")]
  public static void AddElement(
    this IWpfChart chart,
    IChartArea area,
    IChartCandleElement element,
    CandleSeries series)
  {
    if (chart == null)
      throw new ArgumentNullException(nameof (chart));
    chart.AddElement(area, element, new Subscription(series));
  }

  [Obsolete("Use Subscription overload.")]
  public static void AddElement(
    this IWpfChart chart,
    IChartArea area,
    IChartIndicatorElement element,
    CandleSeries series,
    IIndicator indicator)
  {
    if (chart == null)
      throw new ArgumentNullException(nameof (chart));
    chart.AddElement(area, element, new Subscription(series), indicator);
  }

  private sealed class \u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D
  {
    public OptimizerChart3D \u0023\u003Dz_3vetAU\u003D;

    internal bool \u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D(IChart3DParameter _param1)
    {
      return _param1 != this.\u0023\u003Dz_3vetAU\u003D.X;
    }
  }
}
