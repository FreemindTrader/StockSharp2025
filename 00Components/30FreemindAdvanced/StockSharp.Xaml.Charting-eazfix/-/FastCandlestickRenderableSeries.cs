// Decompiled with JetBrains decompiler
// Type: -.FastCandlestickRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public sealed class FastCandlestickRenderableSeries : 
  BaseRenderableSeries
{
  
  public static readonly DependencyProperty \u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D = DependencyProperty.Register(nameof (UpWickColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) Colors.White, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D = DependencyProperty.Register(nameof (DownWickColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) Colors.SteelBlue, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzQE5RIB4g32gf = DependencyProperty.Register(nameof (UpBodyBrush), typeof (Brush), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) new SolidColorBrush(Colors.Transparent), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzzR4yyf\u0024wfFYI = DependencyProperty.Register(nameof (DownBodyBrush), typeof (Brush), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) new SolidColorBrush(Colors.SteelBlue), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzVvc2lVdKTrj8 = DependencyProperty.Register(nameof (DataPointWidth), typeof (double), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) 0.8, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzV9_siG4mU9CA = DependencyProperty.Register(nameof (UpBodyColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) Colors.Transparent, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzvQM2q1v\u0024pfQc = DependencyProperty.Register(nameof (DownBodyColor), typeof (Color), typeof (FastCandlestickRenderableSeries), new PropertyMetadata((object) Colors.SteelBlue, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  private \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D;
  
  private \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D;
  
  private IBrush2D \u0023\u003Dzg8VA2CqF2xNU;
  
  private IBrush2D \u0023\u003DzdX\u00241d3EiaJKH;
  
  private int \u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D;

  public FastCandlestickRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastCandlestickRenderableSeries);
    this.ResamplingMode = ResamplingMode.Mid;
  }

  public double DataPointWidth
  {
    get
    {
      return (double) this.GetValue(FastCandlestickRenderableSeries.\u0023\u003DzVvc2lVdKTrj8);
    }
    set
    {
      this.SetValue(FastCandlestickRenderableSeries.\u0023\u003DzVvc2lVdKTrj8, (object) value);
    }
  }

  public Color UpWickColor
  {
    get
    {
      return (Color) this.GetValue(FastCandlestickRenderableSeries.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D);
    }
    set
    {
      this.SetValue(FastCandlestickRenderableSeries.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) value);
    }
  }

  public Color DownWickColor
  {
    get
    {
      return (Color) this.GetValue(FastCandlestickRenderableSeries.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D);
    }
    set
    {
      this.SetValue(FastCandlestickRenderableSeries.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) value);
    }
  }

  [Obsolete("We're sorry! FastCandlestickRenderableSeries.UpBodyColor is obsolete, please use UpBodyBrush instead", true)]
  public Color UpBodyColor
  {
    get => throw new NotSupportedException();
    set => throw new NotSupportedException();
  }

  public Brush UpBodyBrush
  {
    get
    {
      return (Brush) this.GetValue(FastCandlestickRenderableSeries.\u0023\u003DzQE5RIB4g32gf);
    }
    set
    {
      this.SetValue(FastCandlestickRenderableSeries.\u0023\u003DzQE5RIB4g32gf, (object) value);
    }
  }

  [Obsolete("We're sorry! FastCandlestickRenderableSeries.DownBodyColor is obsolete, please use DownBodyBrush instead", true)]
  public Color DownBodyColor
  {
    get => throw new NotSupportedException();
    set => throw new NotSupportedException();
  }

  public Brush DownBodyBrush
  {
    get
    {
      return (Brush) this.GetValue(FastCandlestickRenderableSeries.\u0023\u003DzzR4yyf\u0024wfFYI);
    }
    set
    {
      this.SetValue(FastCandlestickRenderableSeries.\u0023\u003DzzR4yyf\u0024wfFYI, (object) value);
    }
  }

  protected override void \u0023\u003DznYxDSPc3ewIVoqnDfn0cVVg\u003D()
  {
    this.ResamplingMode = ResamplingMode.Mid;
  }

  protected override HitTestInfo \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    HitTestInfo zldchDrVsrVyHh6WyiGy1 = base.\u0023\u003Dz__R3\u0024ryThR5H(_param1, _param2, false);
    HitTestInfo zldchDrVsrVyHh6WyiGy2 = this.\u0023\u003Dz1SLEyANHenbwANn\u0024\u0024w\u003D\u003D(_param1, zldchDrVsrVyHh6WyiGy1, _param2);
    double num1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV() ? Math.Abs(zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS().Y - _param1.Y) : Math.Abs(zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS().X - _param1.X);
    if (!zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxIOIxNIOU4djmPFSiA\u003D\u003D())
    {
      bool flag1 = num1 < this.\u0023\u003DzcaynwI5AMDdY(zldchDrVsrVyHh6WyiGy2) / this.DataPointWidth / 2.0;
      ref HitTestInfo local = ref zldchDrVsrVyHh6WyiGy2;
      bool flag2;
      zldchDrVsrVyHh6WyiGy2.\u0023\u003DzkNMVgQ88lfxP(flag2 = flag1);
      int num2 = flag2 ? 1 : 0;
      local.\u0023\u003DzZjtwJshPYJrbgaR43Q\u003D\u003D(num2 != 0);
    }
    return zldchDrVsrVyHh6WyiGy2;
  }

  protected override double \u0023\u003DzcaynwI5AMDdY(
    HitTestInfo _param1)
  {
    return (double) this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D;
  }

  protected override double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    HitTestInfo _param1)
  {
    return _param1.\u0023\u003Dz89dSIjCLFKC0().ToDouble();
  }

  protected override double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    HitTestInfo _param1)
  {
    return _param1.\u0023\u003Dzk8BrWRwbV\u0024Y\u0024().ToDouble();
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.UpWickColor.A != (byte) 0 && this.StrokeThickness > 0 || this.DownWickColor.A != (byte) 0 && this.StrokeThickness > 0 || this.UpBodyBrush != null || this.DownBodyBrush != null;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    this.\u0023\u003Dzz7UraMUVt1cf<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR>("OhlcDataSeries");
    IndexRange  g8Oq2rGx6KyfAreq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDVJjfdbDF74N();
    using (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D vQiJuKqUi9jtIaha = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity))
    {
      using (this.\u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.UpWickColor))
      {
        using (this.\u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.DownWickColor))
        {
          this.\u0023\u003Dzg8VA2CqF2xNU = this.\u0023\u003Dze8WyDhI\u003D(_param1, this.UpBodyBrush);
          this.\u0023\u003DzdX\u00241d3EiaJKH = this.\u0023\u003Dze8WyDhI\u003D(_param1, this.DownBodyBrush);
          _param1.\u0023\u003DzX6V3YcdlNDO2((IDisposable) this.\u0023\u003Dzg8VA2CqF2xNU);
          _param1.\u0023\u003DzX6V3YcdlNDO2((IDisposable) this.\u0023\u003DzdX\u00241d3EiaJKH);
          _param1.\u0023\u003DzjyCoorxnWjneJ7dCR\u0024Tiiog\u003D(true);
          if (\u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzilOTiYzU6JIQ(this.ResamplingMode, g8Oq2rGx6KyfAreq, (int) _param1.\u0023\u003Dz8DEW4l1E337F().Width))
            this.\u0023\u003Dz9W\u0024RRqGjh7Xy(_param1, _param2, (\u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D) vQiJuKqUi9jtIaha);
          else
            this.\u0023\u003DzQcMZBOuAsfh7MaaJgQ\u003D\u003D(_param1, _param2, (\u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D) vQiJuKqUi9jtIaha);
          _param1.\u0023\u003DzjyCoorxnWjneJ7dCR\u0024Tiiog\u003D(false);
        }
      }
    }
  }

  private IBrush2D \u0023\u003Dze8WyDhI\u003D(
    IRenderContext2D _param1,
    Brush _param2)
  {
    return !(_param2 is SolidColorBrush solidColorBrush) ? _param1.\u0023\u003Dze8WyDhI\u003D(_param2, this.Opacity, \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerPrimitive) : _param1.\u0023\u003Dze8WyDhI\u003D(solidColorBrush.Color, this.Opacity, new bool?());
  }

  private void \u0023\u003Dz9W\u0024RRqGjh7Xy(
    IRenderContext2D _param1,
    IRenderPassData _param2,
    \u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D _param3)
  {
    bool flag = _param2.\u0023\u003DzDoU1CJhSUWFV();
    IPointSeries ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    int num1 = ftrixUnpTllY1PkTyq.Count();
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = this.PaletteProvider;
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
    for (int index = 0; index < num1; ++index)
    {
      \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR> iwzGyvO4YaqDkpiI = ftrixUnpTllY1PkTyq.\u0023\u003Dz\u0024CeUvME\u003D(index) as \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR>;
      double num2 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(iwzGyvO4YaqDkpiI.\u0023\u003Dz2_4KSTY\u003D());
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
      \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num3 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzolXXlhDBER_c();
      double num4 = xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(num3);
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num5 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzchuwVU\u00245sIH8();
      double num6 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num5);
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num7 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzGze4a8XU7KvB();
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = r84WmWxFxQ0dZvA0kR.Close >= num7 ? this.\u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D : this.\u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D;
      if (paletteProvider != null)
      {
        \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D htg5ScQrmCkwmAaNyPa = paletteProvider;
        double num8 = iwzGyvO4YaqDkpiI.\u0023\u003Dz2_4KSTY\u003D();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num9 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzGze4a8XU7KvB();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num10 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzolXXlhDBER_c();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num11 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzchuwVU\u00245sIH8();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double close = r84WmWxFxQ0dZvA0kR.Close;
        Color? nullable = htg5ScQrmCkwmAaNyPa.\u0023\u003DzLCyKrYI\u003D((IRenderableSeries) this, num8, num9, num10, num11, close);
        if (nullable.HasValue)
          rhwYsZxA33iRu6Id7J = _param3.\u0023\u003Dzc8S9rSE\u003D(nullable.Value, new float?());
      }
      iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point(num2, num4), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num2, num6), flag), rhwYsZxA33iRu6Id7J);
    }
  }

  private void \u0023\u003DzQcMZBOuAsfh7MaaJgQ\u003D\u003D(
    IRenderContext2D _param1,
    IRenderPassData _param2,
    \u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D _param3)
  {
    bool flag1 = _param2.\u0023\u003DzDoU1CJhSUWFV();
    double num1 = this.\u0023\u003DzNfVFwxaLW3jC(_param2);
    IPointSeries ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    int num2 = ftrixUnpTllY1PkTyq.Count();
    if (num2 == 1)
      return;
    this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D = this.\u0023\u003Dz6BuO4fnhj6SX(_param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), ftrixUnpTllY1PkTyq, this.DataPointWidth);
    this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D = this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D <= 1 || this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D % 2 != 0 ? this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D : this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D - 1;
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = this.PaletteProvider;
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
    for (int index = 0; index < num2; ++index)
    {
      \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR> iwzGyvO4YaqDkpiI = ftrixUnpTllY1PkTyq.\u0023\u003Dz\u0024CeUvME\u003D(index) as \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR>;
      \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num3 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzGze4a8XU7KvB();
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num4 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzolXXlhDBER_c();
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double close = r84WmWxFxQ0dZvA0kR.Close;
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num5 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzchuwVU\u00245sIH8();
      bool flag2 = close >= num3;
      int num6 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(iwzGyvO4YaqDkpiI.\u0023\u003Dz2_4KSTY\u003D()).\u0023\u003DzYNd6r7dW43yr();
      int num7 = ((double) num6 - (double) this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D * 0.5).\u0023\u003DzYNd6r7dW43yr();
      int num8 = ((double) num6 + (double) this.\u0023\u003Dz3tYwy\u0024y50OE3nZjFGg\u003D\u003D * 0.5).\u0023\u003DzYNd6r7dW43yr();
      int num9 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(flag2 ? close : num3).\u0023\u003DzYNd6r7dW43yr();
      int num10 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num4).\u0023\u003DzYNd6r7dW43yr();
      int num11 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num5).\u0023\u003DzYNd6r7dW43yr();
      int num12 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(flag2 ? num3 : close).\u0023\u003DzYNd6r7dW43yr();
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = flag2 ? this.\u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D : this.\u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D;
      IBrush2D xrgcdFbSdWgN9GcT8 = flag2 ? this.\u0023\u003Dzg8VA2CqF2xNU : this.\u0023\u003DzdX\u00241d3EiaJKH;
      if (paletteProvider != null)
      {
        Color? nullable = paletteProvider.\u0023\u003DzLCyKrYI\u003D((IRenderableSeries) this, iwzGyvO4YaqDkpiI.\u0023\u003Dz2_4KSTY\u003D(), num3, num4, num5, close);
        if (nullable.HasValue)
        {
          rhwYsZxA33iRu6Id7J = _param3.\u0023\u003Dzc8S9rSE\u003D(nullable.Value, new float?());
          xrgcdFbSdWgN9GcT8 = _param1.\u0023\u003Dze8WyDhI\u003D(nullable.Value, this.Opacity, new bool?());
        }
      }
      iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num10), flag1), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num9), flag1), rhwYsZxA33iRu6Id7J);
      iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num12), flag1), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num6, (double) num11), flag1), rhwYsZxA33iRu6Id7J);
      if (flag2)
        NumberUtil.Swap(ref num9, ref num12);
      iluL6N4L8CsqVgQq.\u0023\u003DzkpjYNfwbvIK8(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num7, (double) num9), flag1), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num8, (double) num12), flag1), xrgcdFbSdWgN9GcT8, rhwYsZxA33iRu6Id7J, num1);
    }
  }

  protected override void \u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1,
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param2)
  {
    switch (_param2)
    {
      case null:
      case IOhlcDataSeries _:
        base.\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(_param1, _param2);
        break;
      default:
        throw new InvalidOperationException($"{((object) this).GetType().Name} expects a DataSeries of type {typeof (IOhlcDataSeries)}. Please ensure the correct data has been bound to the Renderable Series");
    }
  }
}
