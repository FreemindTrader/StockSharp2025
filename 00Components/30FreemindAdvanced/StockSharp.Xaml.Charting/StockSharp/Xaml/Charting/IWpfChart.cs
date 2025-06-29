// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IWpfChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The interface describing the chart control.</summary>
public interface IWpfChart : IChart, IChartBuilder, IThemeableChart, IPersistable
{
  /// <summary>Subscriptions.</summary>
  IEnumerable<Subscription> Subscriptions { get; }

  /// <summary>
  /// To get the data source for <see cref="T:StockSharp.Charting.IChartElement" />.
  /// </summary>
  /// <param name="element">The chart element.</param>
  /// <returns>Market-data source.</returns>
  Subscription TryGetSubscription(IChartElement element);

  /// <summary>To set the source for the element.</summary>
  /// <param name="element">The chart element.</param>
  /// <param name="subscription">Subscription.</param>
  void SetSubscription(IChartElement element, Subscription subscription);

  /// <summary>To add an element to the chart.</summary>
  /// <param name="area">Chart area.</param>
  /// <param name="element">The chart element.</param>
  /// <param name="subscription">
  ///   <see cref="T:StockSharp.BusinessEntities.Subscription" />
  /// </param>
  void AddElement(IChartArea area, IChartCandleElement element, Subscription subscription);

  /// <summary>To add an element to the chart.</summary>
  /// <param name="area">Chart area.</param>
  /// <param name="element">The chart element.</param>
  /// <param name="subscription">
  ///   <see cref="T:StockSharp.BusinessEntities.Subscription" />
  /// </param>
  /// <param name="indicator">Indicator.</param>
  void AddElement(
    IChartArea area,
    IChartIndicatorElement element,
    Subscription subscription,
    IIndicator indicator);
}
