// Decompiled with JetBrains decompiler
// Type: #=zUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal struct \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR : 
  \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSesUJ7CmHq1ptV2iVGi9XvX4<double>,
  IComparable
{
  
  private double _openPrice;
  
  private double _highPrice;
  
  private double _lowPrice;
  
  private double _closePrice;

  public \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
    : this()
  {
    this.\u0023\u003DzXmuA8EWhOFuv(_param1);
    this.\u0023\u003DzzKVL4DebRAZe(_param2);
    this.\u0023\u003DzDq2KB\u0024JgCjsx(_param3);
    this.Close = _param4;
  }

  public readonly double OpenPrice()
  {
    return this._openPrice;
  }

  private void \u0023\u003DzXmuA8EWhOFuv(double _param1)
  {
    this._openPrice = _param1;
  }

  public readonly double HighPrice()
  {
    return this._highPrice;
  }

  private void \u0023\u003DzzKVL4DebRAZe(double _param1)
  {
    this._highPrice = _param1;
  }

  public readonly double LowPrice()
  {
    return this._lowPrice;
  }

  private void \u0023\u003DzDq2KB\u0024JgCjsx(double _param1)
  {
    this._lowPrice = _param1;
  }

  public double Close
  {
    readonly get => this._closePrice;
    private set => this._closePrice = value;
  }

  [SpecialName]
  public double \u0023\u003Dzg1M\u0024G_5sXlam() => this.HighPrice();

  [SpecialName]
  public double \u0023\u003DzKrTvxa8MJ66h() => this.LowPrice();

  [SpecialName]
  public double \u0023\u003Dzu7q98_E\u003D() => this.Close;

  public int CompareTo(object _param1)
  {
    \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR r84WmWxFxQ0dZvA0kR = (\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR) _param1;
    if (this.\u0023\u003Dzg1M\u0024G_5sXlam() > r84WmWxFxQ0dZvA0kR.\u0023\u003Dzg1M\u0024G_5sXlam())
      return 1;
    return this.\u0023\u003DzKrTvxa8MJ66h() >= r84WmWxFxQ0dZvA0kR.\u0023\u003DzKrTvxa8MJ66h() ? 0 : -1;
  }
}
