// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartBuilder
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Charting;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// Default implementation of <see cref="T:StockSharp.Charting.IChartBuilder" />.
/// </summary>
public class ChartBuilder : IChartBuilder
{
  /// <inheritdoc />
  public IChartArea CreateArea() => (IChartArea) new ChartArea();

  /// <inheritdoc />
  public IChartAxis CreateAxis() => (IChartAxis) new ChartAxis();

  /// <inheritdoc />
  public IChartCandleElement CreateCandleElement()
  {
    return (IChartCandleElement) new ChartCandleElement();
  }

  /// <inheritdoc />
  public IChartIndicatorElement CreateIndicatorElement()
  {
    return (IChartIndicatorElement) new ChartIndicatorElement();
  }

  /// <inheritdoc />
  public IChartActiveOrdersElement CreateActiveOrdersElement()
  {
    return (IChartActiveOrdersElement) new ChartActiveOrdersElement();
  }

  /// <inheritdoc />
  public IChartAnnotationElement CreateAnnotation()
  {
    return (IChartAnnotationElement) new ChartAnnotation();
  }

  /// <inheritdoc />
  public IChartBandElement CreateBandElement() => (IChartBandElement) new ChartBandElement();

  /// <inheritdoc />
  public IChartLineElement CreateLineElement() => (IChartLineElement) new ChartLineElement();

  /// <inheritdoc />
  public IChartLineElement CreateBubbleElement() => (IChartLineElement) new ChartBubbleElement();

  /// <inheritdoc />
  public IChartOrderElement CreateOrderElement() => (IChartOrderElement) new ChartOrderElement();

  /// <inheritdoc />
  public IChartTradeElement CreateTradeElement() => (IChartTradeElement) new ChartTradeElement();
}
