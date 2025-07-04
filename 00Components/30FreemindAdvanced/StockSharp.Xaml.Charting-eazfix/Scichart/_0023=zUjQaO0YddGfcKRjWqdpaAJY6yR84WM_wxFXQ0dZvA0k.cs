// Decompiled with JetBrains decompiler
// Type: #=zUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal struct \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR : 
  IComparable,
  \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSesUJ7CmHq1ptV2iVGi9XvX4<double>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DziHpFjupL57O5oHu1Mw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzD8EFLWLAF8etf1TLXA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzByFaQYKUGa5UYX0fCQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzIo3eteZ50RUQ\u0024tgctA\u003D\u003D;

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

  public readonly double \u0023\u003DzGze4a8XU7KvB()
  {
    return this.\u0023\u003DziHpFjupL57O5oHu1Mw\u003D\u003D;
  }

  private void \u0023\u003DzXmuA8EWhOFuv(double _param1)
  {
    this.\u0023\u003DziHpFjupL57O5oHu1Mw\u003D\u003D = _param1;
  }

  public readonly double \u0023\u003DzolXXlhDBER_c()
  {
    return this.\u0023\u003DzD8EFLWLAF8etf1TLXA\u003D\u003D;
  }

  private void \u0023\u003DzzKVL4DebRAZe(double _param1)
  {
    this.\u0023\u003DzD8EFLWLAF8etf1TLXA\u003D\u003D = _param1;
  }

  public readonly double \u0023\u003DzchuwVU\u00245sIH8()
  {
    return this.\u0023\u003DzByFaQYKUGa5UYX0fCQ\u003D\u003D;
  }

  private void \u0023\u003DzDq2KB\u0024JgCjsx(double _param1)
  {
    this.\u0023\u003DzByFaQYKUGa5UYX0fCQ\u003D\u003D = _param1;
  }

  public double Close
  {
    readonly get => this.\u0023\u003DzIo3eteZ50RUQ\u0024tgctA\u003D\u003D;
    private set => this.\u0023\u003DzIo3eteZ50RUQ\u0024tgctA\u003D\u003D = value;
  }

  [SpecialName]
  public double \u0023\u003Dzg1M\u0024G_5sXlam() => this.\u0023\u003DzolXXlhDBER_c();

  [SpecialName]
  public double \u0023\u003DzKrTvxa8MJ66h() => this.\u0023\u003DzchuwVU\u00245sIH8();

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
