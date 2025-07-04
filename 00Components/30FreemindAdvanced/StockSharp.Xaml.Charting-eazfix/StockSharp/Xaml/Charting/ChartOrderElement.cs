// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartOrderElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

[Display(ResourceType = typeof (LocalizedStrings), Name = "Orders")]
public class ChartOrderElement : 
  ChartTransactionElement<ChartOrderElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartOrderElement,
  IChartTransactionElement
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzkZyAKIN\u0024NPN3;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzUAvz8ZozfnUe;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ChartOrderDisplayFilter \u0023\u003DzMivvMSAVwLAH;

  public ChartOrderElement()
  {
    this.SellColor = Colors.Transparent;
    this.BuyColor = Colors.Transparent;
    this.ErrorColor = Colors.DarkOrange;
    this.ErrorStrokeColor = Colors.DarkOrange;
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "TransactionErrorColor", Description = "TransactionErrorColor", Order = 100)]
  public System.Windows.Media.Color ErrorColor
  {
    get => this.\u0023\u003DzkZyAKIN\u0024NPN3;
    set
    {
      if (this.\u0023\u003DzkZyAKIN\u0024NPN3 == value)
        return;
      this.\u0023\u003DzkZyAKIN\u0024NPN3 = value;
      this.RaisePropertyChanged(nameof (ErrorColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ErrorBorderColor", Description = "ErrorBorderColor", Order = 101)]
  public System.Windows.Media.Color ErrorStrokeColor
  {
    get => this.\u0023\u003DzUAvz8ZozfnUe;
    set
    {
      if (this.\u0023\u003DzUAvz8ZozfnUe == value)
        return;
      this.\u0023\u003DzUAvz8ZozfnUe = value;
      this.RaisePropertyChanged(nameof (ErrorStrokeColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "OrdersDisplayFilter", Description = "OrdersDisplayFilter", Order = 110)]
  public ChartOrderDisplayFilter Filter
  {
    get => this.\u0023\u003DzMivvMSAVwLAH;
    set
    {
      if (this.\u0023\u003DzMivvMSAVwLAH == value)
        return;
      this.\u0023\u003DzMivvMSAVwLAH = value;
      this.RaisePropertyChanged(nameof (Filter));
    }
  }

  System.Drawing.Color IChartOrderElement.ErrorColor
  {
    get => this.ErrorColor.FromWpf();
    set => this.ErrorColor = value.ToWpf();
  }

  System.Drawing.Color IChartOrderElement.ErrorStrokeColor
  {
    get => this.ErrorStrokeColor.FromWpf();
    set => this.ErrorStrokeColor = value.ToWpf();
  }

  System.Drawing.Color IChartTransactionElement.BuyColor
  {
    get => this.BuyColor.FromWpf();
    set => this.BuyColor = value.ToWpf();
  }

  System.Drawing.Color IChartTransactionElement.BuyStrokeColor
  {
    get => this.BuyStrokeColor.FromWpf();
    set => this.BuyStrokeColor = value.ToWpf();
  }

  System.Drawing.Color IChartTransactionElement.SellColor
  {
    get => this.SellColor.FromWpf();
    set => this.SellColor = value.ToWpf();
  }

  System.Drawing.Color IChartTransactionElement.SellStrokeColor
  {
    get => this.SellStrokeColor.FromWpf();
    set => this.SellStrokeColor = value.ToWpf();
  }

  protected override bool OnDraw(ChartDrawData data)
  {
    List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D> source = data.\u0023\u003DzaZ5Qc3xeNY95((IChartOrderElement) this);
    return source != null && !CollectionHelper.IsEmpty<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>((ICollection<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) source) && ((IDrawableChartElement) this).StartDrawing(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source.Cast<ChartDrawData.IDrawValue>(), source.Count));
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.ErrorColor = storage.GetValue<int>("ErrorColor", 0).ToColor();
    this.ErrorStrokeColor = storage.GetValue<int>("ErrorStrokeColor", 0).ToColor();
    this.Filter = storage.GetValue<ChartOrderDisplayFilter>("Filter", ChartOrderDisplayFilter.All);
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<int>("ErrorColor", this.ErrorColor.ToInt());
    storage.SetValue<int>("ErrorStrokeColor", this.ErrorStrokeColor.ToInt());
    storage.SetValue<string>("Filter", Converter.To<string>((object) this.Filter));
  }

  internal override ChartOrderElement CopyTo(ChartOrderElement _param1)
  {
    _param1.ErrorColor = this.ErrorColor;
    _param1.ErrorStrokeColor = this.ErrorStrokeColor;
    _param1.Filter = this.Filter;
    return base.CopyTo(_param1);
  }
}
