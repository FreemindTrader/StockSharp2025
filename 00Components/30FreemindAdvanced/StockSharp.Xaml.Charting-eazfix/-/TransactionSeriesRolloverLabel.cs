// Decompiled with JetBrains decompiler
// Type: -.TransactionSeriesRolloverLabel
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Charting;

public sealed class TransactionSeriesRolloverLabel : 
  \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D
{
  
  private ChartDrawData.\u0023\u003DzU3TaXFs\u003D \u0023\u003DzSgRFDbs\u003D;
  
  private readonly Lazy<TransactionSeriesRolloverLabel> \u0023\u003Dzu24j_FM\u003D;
  
  private string \u0023\u003DzJz0NEpw\u003D;
  
  private string dpoChangedEventArgs;
  
  private string \u0023\u003DzHhOSPlQ\u003D;
  
  private string \u0023\u003Dzi\u0024Pgiec\u003D;

  public TransactionSeriesRolloverLabel(
    IRenderableSeries _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.\u0023\u003DzSgRFDbs\u003D = _param2.Transaction;
    this.\u0023\u003Dzu24j_FM\u003D = new Lazy<TransactionSeriesRolloverLabel>(new Func<TransactionSeriesRolloverLabel>(this.\u0023\u003DzRc0chjE\u003D));
  }

  public string Header => this.\u0023\u003Dzu24j_FM\u003D.Value.\u0023\u003DzJz0NEpw\u003D;

  public string Action => this.\u0023\u003Dzu24j_FM\u003D.Value.dpoChangedEventArgs;

  public string Time => this.\u0023\u003Dzu24j_FM\u003D.Value.\u0023\u003DzHhOSPlQ\u003D;

  public string Error => this.\u0023\u003Dzu24j_FM\u003D.Value.\u0023\u003Dzi\u0024Pgiec\u003D;

  public ChartDrawData.\u0023\u003DzU3TaXFs\u003D Transaction
  {
    get => this.\u0023\u003DzSgRFDbs\u003D;
    set
    {
      this.\u0023\u003DzwGPLgl8\u003D<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>(ref this.\u0023\u003DzSgRFDbs\u003D, value, nameof (Transaction));
    }
  }

  private TransactionSeriesRolloverLabel \u0023\u003DzRc0chjE\u003D()
  {
    this.\u0023\u003DzJz0NEpw\u003D = this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003DzHE7yEzyy7k7_() ? LocalizedStrings.Order : LocalizedStrings.Trade;
    this.dpoChangedEventArgs = $"{(this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003DzUYTxG_Bgl8ih() == null ? LocalizedStrings.Buy2 : LocalizedStrings.Sell2)} {this.\u0023\u003DzSgRFDbs\u003D.Volume}@{this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003DzbH5YDNBwpnry()}";
    this.\u0023\u003DzHhOSPlQ\u003D = $"{LocalizedStrings.Time}: {this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003Dzg86amuQ\u003D():d MMM yyyy}, {this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003Dzg86amuQ\u003D():T}";
    if (this.\u0023\u003DzSgRFDbs\u003D.IsError)
      this.\u0023\u003Dzi\u0024Pgiec\u003D = $"{LocalizedStrings.Error}: {this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003Dzj4eGSep8GqT3()}";
    return this;
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.Transaction = ((TransactionSeriesRolloverLabel) _param1).Transaction;
  }
}
