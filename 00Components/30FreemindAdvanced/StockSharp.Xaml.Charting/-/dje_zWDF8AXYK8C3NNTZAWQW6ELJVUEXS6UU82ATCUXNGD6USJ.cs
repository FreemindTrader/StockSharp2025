// Decompiled with JetBrains decompiler
// Type: -.dje_zWDF8AXYK8C3NNTZAWQW6ELJVUEXS6UU82ATCUXNGD6USJ79V72QM33XZFQAX5LUE5RZSC236CFNLTGUDL4R63UZ85LQQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zWDF8AXYK8C3NNTZAWQW6ELJVUEXS6UU82ATCUXNGD6USJ79V72QM33XZFQAX5LUE5RZSC236CFNLTGUDL4R63UZ85LQQ_ejd : 
  dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd
{
  
  private \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dzr7mBO7bq\u0024j8u;

  public dje_zWDF8AXYK8C3NNTZAWQW6ELJVUEXS6UU82ATCUXNGD6USJ79V72QM33XZFQAX5LUE5RZSC236CFNLTGUDL4R63UZ85LQQ_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zWDF8AXYK8C3NNTZAWQW6ELJVUEXS6UU82ATCUXNGD6USJ79V72QM33XZFQAX5LUE5RZSC236CFNLTGUDL4R63UZ85LQQ_ejd);
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

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    double num1 = this.\u0023\u003Dz1runmyhnjbZYf6YRbnCukUGsf9D0YvUs2A\u003D\u003D(_param2 - (double) this.StrokeThickness / 2.0);
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy1 = this.\u0023\u003Dzr7PRxQcLL3EF(_param1, num1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1, true);
    if (!zldchDrVsrVyHh6WyiGy1.\u0023\u003Dzmh1LiTa467ce())
    {
      \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy2 = this.\u0023\u003Dzr7PRxQcLL3EF(_param1, _param2, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1, false);
      if (!zldchDrVsrVyHh6WyiGy2.\u0023\u003Dzmh1LiTa467ce() && zldchDrVsrVyHh6WyiGy2.\u0023\u003DzSkvCFWUKQ7Fw() != -1 && this.DataSeries.\u0023\u003DzPqsSI6C5MOOb().Count != 0)
      {
        double num2 = ((IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[zldchDrVsrVyHh6WyiGy2.\u0023\u003DzSkvCFWUKQ7Fw()]).\u0023\u003Dzb9UCYbo\u003D();
        \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
        Point point = this.\u0023\u003Dzop6vn0GowyiR(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV());
        double y = point.Y;
        double num3 = xkzemsMs5tGkouk5w.\u0023\u003DzACwLhyc\u003D(y);
        double num4;
        if (!this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV())
        {
          point = zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS();
          num4 = point.X;
        }
        else
        {
          point = zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS();
          num4 = point.Y;
        }
        double num5 = num4;
        double num6;
        double num7;
        if (num2.CompareTo(this.ZeroLineY) > 0)
        {
          num6 = this.ZeroLineY;
          num7 = num2;
        }
        else
        {
          num6 = num2;
          num7 = this.ZeroLineY;
        }
        zldchDrVsrVyHh6WyiGy2.\u0023\u003Dzn3o1RS9wuET8(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz99srMqSdWO6y(_param1.X, num5 - _param2, num5 + _param2) && dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz99srMqSdWO6y(num3, num6, num7));
      }
      if (zldchDrVsrVyHh6WyiGy2.\u0023\u003Dzmh1LiTa467ce())
        return zldchDrVsrVyHh6WyiGy2;
    }
    return zldchDrVsrVyHh6WyiGy1;
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0 || this.PointMarker != null;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param2)
  {
    bool flag = _param2.\u0023\u003DzDoU1CJhSUWFV();
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    int num1 = ftrixUnpTllY1PkTyq.\u0023\u003DzlpVGw6E\u003D();
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = this.PaletteProvider;
    using (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D vQiJuKqUi9jtIaha = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity))
    {
      this.\u0023\u003Dzr7mBO7bq\u0024j8u = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.SeriesColor);
      float num2 = (float) this.\u0023\u003DzySDi0_ve2vLaE3cXlA\u003D\u003D();
      \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
      for (int index = 0; index < num1; ++index)
      {
        \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D kld48pAvUlrTzJ1tmfY = ftrixUnpTllY1PkTyq.\u0023\u003Dz\u0024CeUvME\u003D(index);
        if (!double.IsNaN(kld48pAvUlrTzJ1tmfY.\u0023\u003Dzu7q98_E\u003D()))
        {
          float num3 = (float) _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D());
          float num4 = (float) _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(kld48pAvUlrTzJ1tmfY.\u0023\u003Dzu7q98_E\u003D());
          Point point1 = this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num3, (double) num4), flag);
          Point point2 = this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num3, (double) num2), flag);
          if (paletteProvider != null)
          {
            Color? nullable = paletteProvider.\u0023\u003DzP50Orng\u003D((IRenderableSeries) this, kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D(), kld48pAvUlrTzJ1tmfY.\u0023\u003Dzu7q98_E\u003D());
            if (nullable.HasValue)
            {
              \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(nullable.Value);
              iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(point1, point2, rhwYsZxA33iRu6Id7J);
              continue;
            }
          }
          iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(point1, point2, this.\u0023\u003Dzr7mBO7bq\u0024j8u);
        }
      }
    }
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = this.\u0023\u003Dz_Y6pODRV4VXF();
    if (hw1ki13vvK4WxOgoljkHyInT == null)
      return;
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzmuYww2BKaMZ86Wjblw\u003D\u003D(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D(), hw1ki13vvK4WxOgoljkHyInT), ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D());
  }
}
