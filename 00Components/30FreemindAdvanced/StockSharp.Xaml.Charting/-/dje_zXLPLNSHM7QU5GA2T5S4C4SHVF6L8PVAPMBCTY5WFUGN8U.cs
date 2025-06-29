// Decompiled with JetBrains decompiler
// Type: -.dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd
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

internal class dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd : 
  dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd
{
  
  private int \u0023\u003DzBz9v6qHDJbMP;
  
  public static readonly DependencyProperty \u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D = DependencyProperty.Register("", typeof (Color), typeof (dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd), new PropertyMetadata((object) Colors.White, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D = DependencyProperty.Register("", typeof (Color), typeof (dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd), new PropertyMetadata((object) Colors.SteelBlue, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzVvc2lVdKTrj8 = DependencyProperty.Register("", typeof (double), typeof (dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd), new PropertyMetadata((object) 0.8, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  protected \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D;
  
  protected \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D;

  public dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd);
    this.ResamplingMode = \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Mid;
  }

  public double DataPointWidth
  {
    get
    {
      return (double) this.GetValue(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzVvc2lVdKTrj8);
    }
    set
    {
      this.SetValue(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzVvc2lVdKTrj8, (object) value);
    }
  }

  public Color UpWickColor
  {
    get
    {
      return (Color) this.GetValue(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) value);
    }
  }

  public Color DownWickColor
  {
    get
    {
      return (Color) this.GetValue(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) value);
    }
  }

  protected override void \u0023\u003DznYxDSPc3ewIVoqnDfn0cVVg\u003D()
  {
    this.ResamplingMode = \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Mid;
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy1 = base.\u0023\u003Dz__R3\u0024ryThR5H(_param1, _param2, false);
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy2 = this.\u0023\u003Dz1SLEyANHenbwANn\u0024\u0024w\u003D\u003D(_param1, zldchDrVsrVyHh6WyiGy1, _param2);
    double num1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV() ? Math.Abs(zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS().Y - _param1.Y) : Math.Abs(zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS().X - _param1.X);
    if (!zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxIOIxNIOU4djmPFSiA\u003D\u003D())
    {
      bool flag1 = num1 < this.\u0023\u003DzcaynwI5AMDdY(zldchDrVsrVyHh6WyiGy2) / this.DataPointWidth / 2.0;
      ref \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D local = ref zldchDrVsrVyHh6WyiGy2;
      bool flag2;
      zldchDrVsrVyHh6WyiGy2.\u0023\u003DzkNMVgQ88lfxP(flag2 = flag1);
      int num2 = flag2 ? 1 : 0;
      local.\u0023\u003DzZjtwJshPYJrbgaR43Q\u003D\u003D(num2 != 0);
    }
    return zldchDrVsrVyHh6WyiGy2;
  }

  protected override double \u0023\u003DzcaynwI5AMDdY(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return (double) this.\u0023\u003DzBz9v6qHDJbMP;
  }

  protected override double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return _param1.\u0023\u003Dz89dSIjCLFKC0().\u0023\u003Dzb9UCYbo\u003D();
  }

  protected override double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return _param1.\u0023\u003Dzk8BrWRwbV\u0024Y\u0024().\u0023\u003Dzb9UCYbo\u003D();
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    if (this.UpWickColor.A != (byte) 0 && this.StrokeThickness > 0)
      return true;
    return this.DownWickColor.A != (byte) 0 && this.StrokeThickness > 0;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param2)
  {
    this.\u0023\u003Dzz7UraMUVt1cf<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR>("");
    using (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D vQiJuKqUi9jtIaha = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity))
    {
      this.\u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.UpWickColor);
      this.\u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.DownWickColor);
      _param1.\u0023\u003DzjyCoorxnWjneJ7dCR\u0024Tiiog\u003D(true);
      if (\u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzilOTiYzU6JIQ(this.ResamplingMode, _param2.\u0023\u003DzDVJjfdbDF74N(), (int) _param1.\u0023\u003Dz8DEW4l1E337F().Width))
        this.\u0023\u003Dz9W\u0024RRqGjh7Xy(_param1, _param2, (\u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D) vQiJuKqUi9jtIaha);
      else
        this.\u0023\u003DzQcMZBOuAsfh7MaaJgQ\u003D\u003D(_param1, _param2, (\u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D) vQiJuKqUi9jtIaha);
      _param1.\u0023\u003DzjyCoorxnWjneJ7dCR\u0024Tiiog\u003D(false);
    }
  }

  private void \u0023\u003Dz9W\u0024RRqGjh7Xy(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param2,
    \u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D _param3)
  {
    bool flag = _param2.\u0023\u003DzDoU1CJhSUWFV();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    int num1 = ftrixUnpTllY1PkTyq.\u0023\u003DzlpVGw6E\u003D();
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = this.PaletteProvider;
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
    for (int index = 0; index < num1; ++index)
    {
      \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR> iwzGyvO4YaqDkpiI = ftrixUnpTllY1PkTyq.\u0023\u003Dz\u0024CeUvME\u003D(index) as \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR>;
      double num2 = xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(iwzGyvO4YaqDkpiI.Property());
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w3 = xkzemsMs5tGkouk5w2;
      \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num3 = r84WmWxFxQ0dZvA0kR.HighPrice();
      double num4 = xkzemsMs5tGkouk5w3.\u0023\u003DzhL6gsJw\u003D(num3);
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w4 = xkzemsMs5tGkouk5w2;
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num5 = r84WmWxFxQ0dZvA0kR.LowPrice();
      double num6 = xkzemsMs5tGkouk5w4.\u0023\u003DzhL6gsJw\u003D(num5);
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num7 = r84WmWxFxQ0dZvA0kR.OpenPrice();
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = r84WmWxFxQ0dZvA0kR.Close >= num7 ? this.\u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D : this.\u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D;
      Color? nullable = new Color?();
      if (paletteProvider != null)
      {
        \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D htg5ScQrmCkwmAaNyPa = paletteProvider;
        double num8 = iwzGyvO4YaqDkpiI.Property();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num9 = r84WmWxFxQ0dZvA0kR.OpenPrice();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num10 = r84WmWxFxQ0dZvA0kR.HighPrice();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num11 = r84WmWxFxQ0dZvA0kR.LowPrice();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double close = r84WmWxFxQ0dZvA0kR.Close;
        nullable = htg5ScQrmCkwmAaNyPa.\u0023\u003DzLCyKrYI\u003D((IRenderableSeries) this, num8, num9, num10, num11, close);
        if (nullable.HasValue)
          rhwYsZxA33iRu6Id7J = _param3.\u0023\u003Dzc8S9rSE\u003D(nullable.Value, new float?());
      }
      iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point(num2, num4), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num2, num6), flag), rhwYsZxA33iRu6Id7J);
      if (nullable.HasValue)
        rhwYsZxA33iRu6Id7J.Dispose();
    }
  }

  protected virtual void \u0023\u003DzQcMZBOuAsfh7MaaJgQ\u003D\u003D(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param2,
    \u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D _param3)
  {
    bool flag = _param2.\u0023\u003DzDoU1CJhSUWFV();
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    int num1 = ftrixUnpTllY1PkTyq.\u0023\u003DzlpVGw6E\u003D();
    if (num1 == 1)
      return;
    this.\u0023\u003DzBz9v6qHDJbMP = this.\u0023\u003Dz6BuO4fnhj6SX(_param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), ftrixUnpTllY1PkTyq, this.DataPointWidth);
    this.\u0023\u003DzBz9v6qHDJbMP = this.\u0023\u003DzBz9v6qHDJbMP <= 1 || this.\u0023\u003DzBz9v6qHDJbMP % 2 != 0 ? this.\u0023\u003DzBz9v6qHDJbMP : this.\u0023\u003DzBz9v6qHDJbMP - 1;
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = this.PaletteProvider;
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
    for (int index = 0; index < num1; ++index)
    {
      \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR> iwzGyvO4YaqDkpiI = ftrixUnpTllY1PkTyq.\u0023\u003Dz\u0024CeUvME\u003D(index) as \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR>;
      \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num2 = r84WmWxFxQ0dZvA0kR.OpenPrice();
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num3 = r84WmWxFxQ0dZvA0kR.HighPrice();
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double close1 = r84WmWxFxQ0dZvA0kR.Close;
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num4 = r84WmWxFxQ0dZvA0kR.LowPrice();
      int num5 = close1 >= num2 ? 1 : 0;
      int num6 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(iwzGyvO4YaqDkpiI.Property()).\u0023\u003DzYNd6r7dW43yr();
      int num7 = ((double) num6 - (double) this.\u0023\u003DzBz9v6qHDJbMP * 0.5).\u0023\u003DzYNd6r7dW43yr();
      int num8 = ((double) num6 + (double) this.\u0023\u003DzBz9v6qHDJbMP * 0.5).\u0023\u003DzYNd6r7dW43yr();
      int num9 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num2).\u0023\u003DzYNd6r7dW43yr();
      int num10 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num3).\u0023\u003DzYNd6r7dW43yr();
      int num11 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num4).\u0023\u003DzYNd6r7dW43yr();
      int num12 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(close1).\u0023\u003DzYNd6r7dW43yr();
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = num5 != 0 ? this.\u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D : this.\u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D;
      if (paletteProvider != null)
      {
        \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D htg5ScQrmCkwmAaNyPa = paletteProvider;
        double num13 = iwzGyvO4YaqDkpiI.Property();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num14 = r84WmWxFxQ0dZvA0kR.OpenPrice();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num15 = r84WmWxFxQ0dZvA0kR.HighPrice();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num16 = r84WmWxFxQ0dZvA0kR.LowPrice();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double close2 = r84WmWxFxQ0dZvA0kR.Close;
        Color? nullable = htg5ScQrmCkwmAaNyPa.\u0023\u003DzLCyKrYI\u003D((IRenderableSeries) this, num13, num14, num15, num16, close2);
        if (nullable.HasValue)
          rhwYsZxA33iRu6Id7J = _param3.\u0023\u003Dzc8S9rSE\u003D(nullable.Value, new float?());
      }
      iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num7, (double) num9), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num9), flag), rhwYsZxA33iRu6Id7J);
      iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num12), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num8, (double) num12), flag), rhwYsZxA33iRu6Id7J);
      iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num10), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num11), flag), rhwYsZxA33iRu6Id7J);
    }
  }

  protected override void \u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1,
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param2)
  {
    switch (_param2)
    {
      case null:
      case \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrFQ3G9W9xt2vxQkAWz\u0024zVnJ _:
        base.\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(_param1, _param2);
        break;
      default:
        throw new InvalidOperationException(string.Format("", (object) ((object) this).GetType().Name, (object) typeof (\u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrFQ3G9W9xt2vxQkAWz\u0024zVnJ)));
    }
  }
}
