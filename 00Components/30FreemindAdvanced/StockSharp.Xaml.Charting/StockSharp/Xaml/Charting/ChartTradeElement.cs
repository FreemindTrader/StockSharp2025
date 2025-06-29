// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartTradeElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart element representing trades.</summary>
[Display(ResourceType = typeof (LocalizedStrings), Name = "Trades")]
[TypeConverter(typeof (ExpandableObjectConverter))]
public class ChartTradeElement : 
  ChartTransactionElement<ChartTradeElement>,
  IChartTradeElement,
  IChartTransactionElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data)
  {
    List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D> source = data.\u0023\u003DzaZ5Qc3xeNY95((IChartTradeElement) this);
    return source != null && !CollectionHelper.IsEmpty<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>((ICollection<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) source) && ((\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X) this).\u0023\u003Dz2dQykb\u0024x9fU4(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source.Cast<ChartDrawData.IDrawValue>(), source.Count));
  }

  Color IChartTransactionElement.BuyColor
  {
    get => this.BuyColor.FromWpf();
    set => this.BuyColor = value.ToWpf();
  }

  Color IChartTransactionElement.BuyStrokeColor
  {
    get => this.BuyStrokeColor.FromWpf();
    set => this.BuyStrokeColor = value.ToWpf();
  }

  Color IChartTransactionElement.SellColor
  {
    get => this.SellColor.FromWpf();
    set => this.SellColor = value.ToWpf();
  }

  Color IChartTransactionElement.SellStrokeColor
  {
    get => this.SellStrokeColor.FromWpf();
    set => this.SellStrokeColor = value.ToWpf();
  }
}
