// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IWpfChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Xaml.Charting;

public interface IWpfChart : IChart, IPersistable, IChartBuilder, IThemeableChart
{
  IEnumerable<Subscription> Subscriptions { get; }

  Subscription TryGetSubscription(IChartElement element);

  void SetSubscription(IChartElement element, Subscription subscription);

  void AddElement(IChartArea area, IChartCandleElement element, Subscription subscription);

  void AddElement(
    IChartArea area,
    IChartIndicatorElement element,
    Subscription subscription,
    IIndicator indicator);
}
