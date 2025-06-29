// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartHelper
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>
/// Extension class for <see cref="T:StockSharp.Charting.IChart" />.
/// </summary>
public static class ChartHelper
{
  
  private static readonly Lazy<IndicatorColorProvider> \u0023\u003DzOejVh3VT5Dah = new Lazy<IndicatorColorProvider>();

  /// <summary>To save the chart as image.</summary>
  /// <param name="chart">Chart.</param>
  /// <returns>The content of image.</returns>
  public static Stream SaveToImage(this IChart chart)
  {
    if (chart == null)
      throw new ArgumentNullException("");
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
    }), "");
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

  /// <summary>Get current chart theme.</summary>
  /// <returns>Chart theme.</returns>
  public static string CurrChartTheme()
  {
    return ApplicationThemeHelper.ApplicationThemeName.ToChartTheme();
  }

  /// <summary>Convert Devexpress theme to chart theme.</summary>
  /// <param name="appTheme">Devexpress theme.</param>
  /// <returns>Chart theme.</returns>
  public static string ToChartTheme(this string appTheme)
  {
    return !appTheme.IsDark() ? "" : "";
  }

  /// <summary>Update theme for the specified chart.</summary>
  /// <param name="chart">Chart.</param>
  public static void UpdateTheme(this IThemeableChart chart)
  {
    if (chart == null)
      throw new ArgumentNullException("");
    chart.ChartTheme = ChartHelper.CurrChartTheme();
  }

  /// <summary>Get all indicator types.</summary>
  /// <returns>All indicator types.</returns>
  public static IEnumerable<IndicatorType> GetIndicatorTypes()
  {
    return IChartExtensions.IndicatorProvider.All;
  }

  /// <summary>
  /// Fill <see cref="T:StockSharp.Xaml.Charting.OptimizerChart3D" /> axis values.
  /// </summary>
  /// <param name="chart">
  ///   <see cref="T:StockSharp.Xaml.Charting.OptimizerChart3D" />
  /// </param>
  public static void FillDefaultValues(this OptimizerChart3D chart)
  {
    ChartHelper.SomeClass111 skXz9pb7XdulaJda = new ChartHelper.SomeClass111();
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D = chart;
    if (skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D == null)
      throw new ArgumentNullException("");
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.X = skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.XValues.FirstOrDefault<IChart3DParameter>();
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.Y = skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.YValues.Where<IChart3DParameter>(new Func<IChart3DParameter, bool>(skXz9pb7XdulaJda.\u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D)).FirstOrDefault<IChart3DParameter>() ?? skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.YValues.FirstOrDefault<IChart3DParameter>();
    skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.Z = skXz9pb7XdulaJda.\u0023\u003Dz_3vetAU\u003D.ZValues.FirstOrDefault<IStatisticParameter>();
  }

  internal static Chart \u0023\u003DzW4D5ZlUSfXRJ(this IChartElement _param0)
  {
    return (Chart) _param0.TryGetChart();
  }

  /// <summary>
  /// Try get <see cref="T:StockSharp.Algo.Indicators.IIndicator" /> for the specified element.
  /// </summary>
  /// <param name="element">
  ///   <see cref="T:StockSharp.Charting.IChartIndicatorElement" />
  /// </param>
  /// <returns>
  ///   <see cref="T:StockSharp.Algo.Indicators.IIndicator" />
  /// </returns>
  public static IIndicator TryGetIndicator(this IChartIndicatorElement element)
  {
    return element.\u0023\u003DzW4D5ZlUSfXRJ()?.GetIndicator(element);
  }

  /// <summary>
  /// Try get <see cref="T:StockSharp.BusinessEntities.Subscription" /> for the specified element.
  /// </summary>
  /// <param name="element">
  ///   <see cref="T:StockSharp.Charting.IChartElement" />
  /// </param>
  /// <returns>
  ///   <see cref="T:StockSharp.BusinessEntities.Subscription" />
  /// </returns>
  public static Subscription TryGetSubscription(this IChartElement element)
  {
    return element.\u0023\u003DzW4D5ZlUSfXRJ()?.TryGetSubscription(element);
  }

  /// <summary>
  /// <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.IIndicatorColorProvider" />.
  ///     </summary>
  public static IIndicatorColorProvider TryColorProvider
  {
    get => ConfigManager.TryGetService<IIndicatorColorProvider>();
  }

  /// <summary>Ensure get color provider.</summary>
  /// <returns>
  /// <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.IIndicatorColorProvider" />.</returns>
  public static IIndicatorColorProvider EnsureColorProvider()
  {
    return ChartHelper.TryColorProvider ?? (IIndicatorColorProvider) ChartHelper.\u0023\u003DzOejVh3VT5Dah.Value;
  }

  /// <summary>Draw equity curve.</summary>
  /// <param name="chart">
  ///   <see cref="T:StockSharp.Xaml.Charting.EquityCurveChart" />
  /// </param>
  /// <param name="strategy">
  ///   <see cref="T:StockSharp.Algo.Strategies.Strategy" />
  /// </param>
  /// <param name="pnl">
  /// <see cref="T:StockSharp.Charting.IChartBandElement" /> for <see cref="P:StockSharp.Algo.Strategies.Strategy.PnL" />.</param>
  /// <param name="unrealized">
  /// <see cref="T:StockSharp.Charting.IChartBandElement" /> for <see cref="P:StockSharp.Algo.PnL.IPnLManager.UnrealizedPnL" />.</param>
  /// <param name="commission">
  /// <see cref="T:StockSharp.Charting.IChartBandElement" /> for <see cref="P:StockSharp.Algo.Strategies.Strategy.Commission" />.</param>
  public static void DrawPnL(
    this EquityCurveChart chart,
    Strategy strategy,
    IChartBandElement pnl,
    IChartBandElement unrealized,
    IChartBandElement commission)
  {
    if (chart == null)
      throw new ArgumentNullException("");
    if (strategy == null)
      throw new ArgumentNullException("");
    if (pnl == null)
      throw new ArgumentNullException("");
    if (unrealized == null)
      throw new ArgumentNullException("");
    if (commission == null)
      throw new ArgumentNullException("");
    IChartDrawData data = chart.CreateData();
    data.Group(((BaseLogSource) strategy).CurrentTime).Add(pnl, strategy.PnL).Add(unrealized, strategy.PnLManager.UnrealizedPnL).Add(commission, strategy.Commission.GetValueOrDefault());
    chart.Draw(data);
  }

  /// <summary>
  /// </summary>
  [Obsolete("Use Subscription overload.")]
  public static void AddElement(
    this IWpfChart chart,
    IChartArea area,
    IChartCandleElement element,
    CandleSeries series)
  {
    if (chart == null)
      throw new ArgumentNullException("");
    chart.AddElement(area, element, new Subscription(series));
  }

  /// <summary>
  /// </summary>
  [Obsolete("Use Subscription overload.")]
  public static void AddElement(
    this IWpfChart chart,
    IChartArea area,
    IChartIndicatorElement element,
    CandleSeries series,
    IIndicator indicator)
  {
    if (chart == null)
      throw new ArgumentNullException("");
    chart.AddElement(area, element, new Subscription(series), indicator);
  }

  private sealed class SomeClass111
  {
    public OptimizerChart3D \u0023\u003Dz_3vetAU\u003D;

    internal bool \u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D(IChart3DParameter _param1)
    {
      return _param1 != this.\u0023\u003Dz_3vetAU\u003D.X;
    }
  }
}
