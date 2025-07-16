// Decompiled with JetBrains decompiler
// Type: -.FastColumnRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
namespace SciChart.Charting;

internal sealed class FastColumnRenderableSeries : 
  BaseColumnRenderableSeries
{
  public FastColumnRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastColumnRenderableSeries);
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0 || this.FillBrush != null;
  }

  public override IRange \u0023\u003DzxNQHuqrEvxH2(
    IRange _param1,
    bool _param2)
  {
    IRange abyLt9clZggmJsWhw = base.\u0023\u003DzxNQHuqrEvxH2(_param1, _param2);
    double val2 = this.ZeroLineY;
    if (_param2 && val2 <= 0.0)
      val2 = abyLt9clZggmJsWhw.Min.ToDouble();
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) Math.Min(abyLt9clZggmJsWhw.Min.ToDouble(), val2), (IComparable) Math.Max(abyLt9clZggmJsWhw.Max.ToDouble(), val2));
  }

  protected override double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    IComparable comparable = (IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[_param1.\u0023\u003DzSkvCFWUKQ7Fw()];
    return comparable.ToDouble().CompareTo(this.ZeroLineY) <= 0 ? comparable.ToDouble() : this.ZeroLineY;
  }

  protected override double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    IComparable comparable = (IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[_param1.\u0023\u003DzSkvCFWUKQ7Fw()];
    return comparable.ToDouble().CompareTo(this.ZeroLineY) <= 0 ? this.ZeroLineY : comparable.ToDouble();
  }

  protected override double \u0023\u003Dz6bPMsvVWiXoq(
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param1,
    IRenderPassData _param2)
  {
    return (double) this.\u0023\u003Dz6BuO4fnhj6SX(_param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), _param1, this.DataPointWidth);
  }

  public override IRange \u0023\u003Dzq3MgExWxza1L()
  {
    IRange abyLt9clZggmJsWhw = base.\u0023\u003Dzq3MgExWxza1L();
    if (!abyLt9clZggmJsWhw.IsDefined)
      return (IRange) DoubleRange.UndefinedRange;
    int count = this.DataSeries.get_Count();
    DoubleRange klqcJ87Zm8UwE3WEjd = abyLt9clZggmJsWhw.AsDoubleRange();
    double num = count > 1 ? klqcJ87Zm8UwE3WEjd.Diff / (double) (count - 1) / 2.0 * this.DataPointWidth : this.DataPointWidth / 2.0;
    klqcJ87Zm8UwE3WEjd.Max += num;
    klqcJ87Zm8UwE3WEjd.Min -= num;
    return abyLt9clZggmJsWhw;
  }
}
