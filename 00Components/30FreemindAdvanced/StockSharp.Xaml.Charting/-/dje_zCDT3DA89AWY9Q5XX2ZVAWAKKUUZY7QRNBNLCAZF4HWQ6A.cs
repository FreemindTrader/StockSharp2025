// Decompiled with JetBrains decompiler
// Type: -.dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUZY7QRNBNLCAZF4HWQ6A3MJPY73EMQ2VAQAAB3ZH2RUU2VAK3EQE9D32HKF4_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUZY7QRNBNLCAZF4HWQ6A3MJPY73EMQ2VAQAAB3ZH2RUU2VAK3EQE9D32HKF4_ejd : 
  dje_zHYRPNQHHBLUELC2PHC86VUXPKVH2ZMKXPKFTYYKX6SKX4JY2EDLNT4C8JKXXPNRQ42CXGSK8E94CEP2_ejd
{
  public dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUZY7QRNBNLCAZF4HWQ6A3MJPY73EMQ2VAQAAB3ZH2RUU2VAK3EQE9D32HKF4_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUZY7QRNBNLCAZF4HWQ6A3MJPY73EMQ2VAQAAB3ZH2RUU2VAK3EQE9D32HKF4_ejd);
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0 || this.FillBrush != null;
  }

  public override \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzxNQHuqrEvxH2(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1,
    bool _param2)
  {
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = base.\u0023\u003DzxNQHuqrEvxH2(_param1, _param2);
    double val2 = this.ZeroLineY;
    if (_param2 && val2 <= 0.0)
      val2 = abyLt9clZggmJsWhw.Min.\u0023\u003Dzb9UCYbo\u003D();
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) Math.Min(abyLt9clZggmJsWhw.Min.\u0023\u003Dzb9UCYbo\u003D(), val2), (IComparable) Math.Max(abyLt9clZggmJsWhw.Max.\u0023\u003Dzb9UCYbo\u003D(), val2));
  }

  protected override double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    IComparable comparable = (IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[_param1.\u0023\u003DzSkvCFWUKQ7Fw()];
    return comparable.\u0023\u003Dzb9UCYbo\u003D().CompareTo(this.ZeroLineY) <= 0 ? comparable.\u0023\u003Dzb9UCYbo\u003D() : this.ZeroLineY;
  }

  protected override double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    IComparable comparable = (IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[_param1.\u0023\u003DzSkvCFWUKQ7Fw()];
    return comparable.\u0023\u003Dzb9UCYbo\u003D().CompareTo(this.ZeroLineY) <= 0 ? this.ZeroLineY : comparable.\u0023\u003Dzb9UCYbo\u003D();
  }

  protected override double \u0023\u003Dz6bPMsvVWiXoq(
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param1,
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param2)
  {
    return (double) this.\u0023\u003Dz6BuO4fnhj6SX(_param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), _param1, this.DataPointWidth);
  }

  public override \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003Dzq3MgExWxza1L()
  {
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = base.\u0023\u003Dzq3MgExWxza1L();
    if (!abyLt9clZggmJsWhw.IsDefined)
      return (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd.\u0023\u003DzUNx30p_smDNA();
    int count = this.DataSeries.get_Count();
    dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd klqcJ87Zm8UwE3WEjd = abyLt9clZggmJsWhw.\u0023\u003DzfODy_Nxn8OGy();
    double num = count > 1 ? klqcJ87Zm8UwE3WEjd.Diff / (double) (count - 1) / 2.0 * this.DataPointWidth : this.DataPointWidth / 2.0;
    klqcJ87Zm8UwE3WEjd.Max += num;
    klqcJ87Zm8UwE3WEjd.Min -= num;
    return abyLt9clZggmJsWhw;
  }
}
