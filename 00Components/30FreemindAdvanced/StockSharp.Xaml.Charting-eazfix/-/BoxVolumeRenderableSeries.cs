// Decompiled with JetBrains decompiler
// Type: -.BoxVolumeRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Messages;
using StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class BoxVolumeRenderableSeries : 
  \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D
{
  
  public static readonly DependencyProperty \u0023\u003DzY\u0024hEpvHX6SG5MENm\u0024w\u003D\u003D = DependencyProperty.Register(nameof (Timeframe2), typeof (TimeSpan?), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) TimeSpan.FromMinutes(5.0), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9), new CoerceValueCallback(BoxVolumeRenderableSeries.\u0023\u003DzbNu7yh8kw5s3YRvNpAP5CbU\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzsXfDaxv8JwjzCERgkg\u003D\u003D = DependencyProperty.Register(nameof (Timeframe3), typeof (TimeSpan?), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) TimeSpan.FromMinutes(15.0), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9), new CoerceValueCallback(BoxVolumeRenderableSeries.\u0023\u003DzbNu7yh8kw5s3YRvNpAP5CbU\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz1cQ4wVMzHjSWdOxgiAXT\u0024gw\u003D = DependencyProperty.Register(nameof (Timeframe2Color), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 36, (byte) 36, (byte) 36), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzFin1pB0TmtTClsb5xicohbw\u003D = DependencyProperty.Register(nameof (Timeframe2FrameColor), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb(byte.MaxValue, (byte) 102, (byte) 0), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dzu_UBGSutECvla_Z4FBIjQVw\u003D = DependencyProperty.Register(nameof (Timeframe3Color), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 0, (byte) 55, (byte) 24), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzJmV2YqfjqzBN = DependencyProperty.Register(nameof (CellFontColor), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 90, (byte) 90, (byte) 90), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dz\u0024pEkT\u0024GyBsLd = DependencyProperty.Register(nameof (HighVolColor), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Colors.LawnGreen, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzeOkHtL891mol_E40\u0024w\u003D\u003D = DependencyProperty.Register(nameof (HighVolBackground), typeof (Color), typeof (BoxVolumeRenderableSeries), new PropertyMetadata((object) Colors.LightBlue, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));

  public BoxVolumeRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (BoxVolumeRenderableSeries);
  }

  public TimeSpan? Timeframe2
  {
    get
    {
      return (TimeSpan?) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003DzY\u0024hEpvHX6SG5MENm\u0024w\u003D\u003D);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003DzY\u0024hEpvHX6SG5MENm\u0024w\u003D\u003D, (object) value);
    }
  }

  public Color Timeframe2Color
  {
    get
    {
      return (Color) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003Dz1cQ4wVMzHjSWdOxgiAXT\u0024gw\u003D);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003Dz1cQ4wVMzHjSWdOxgiAXT\u0024gw\u003D, (object) value);
    }
  }

  public Color Timeframe2FrameColor
  {
    get
    {
      return (Color) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003DzFin1pB0TmtTClsb5xicohbw\u003D);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003DzFin1pB0TmtTClsb5xicohbw\u003D, (object) value);
    }
  }

  public TimeSpan? Timeframe3
  {
    get
    {
      return (TimeSpan?) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003DzsXfDaxv8JwjzCERgkg\u003D\u003D);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003DzsXfDaxv8JwjzCERgkg\u003D\u003D, (object) value);
    }
  }

  public Color Timeframe3Color
  {
    get
    {
      return (Color) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003Dzu_UBGSutECvla_Z4FBIjQVw\u003D);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003Dzu_UBGSutECvla_Z4FBIjQVw\u003D, (object) value);
    }
  }

  public Color CellFontColor
  {
    get
    {
      return (Color) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003DzJmV2YqfjqzBN);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003DzJmV2YqfjqzBN, (object) value);
    }
  }

  public Color HighVolColor
  {
    get
    {
      return (Color) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003Dz\u0024pEkT\u0024GyBsLd);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003Dz\u0024pEkT\u0024GyBsLd, (object) value);
    }
  }

  public Color HighVolBackground
  {
    get
    {
      return (Color) this.GetValue(BoxVolumeRenderableSeries.\u0023\u003DzeOkHtL891mol_E40\u0024w\u003D\u003D);
    }
    set
    {
      this.SetValue(BoxVolumeRenderableSeries.\u0023\u003DzeOkHtL891mol_E40\u0024w\u003D\u003D, (object) value);
    }
  }

  protected override void \u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D()
  {
    base.\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D();
    this.CoerceValue(BoxVolumeRenderableSeries.\u0023\u003DzY\u0024hEpvHX6SG5MENm\u0024w\u003D\u003D);
    this.CoerceValue(BoxVolumeRenderableSeries.\u0023\u003DzsXfDaxv8JwjzCERgkg\u003D\u003D);
  }

  private static object \u0023\u003DzbNu7yh8kw5s3YRvNpAP5CbU\u003D(
    DependencyObject _param0,
    object _param1)
  {
    BoxVolumeRenderableSeries glA76LmrlQ4YzEjd = (BoxVolumeRenderableSeries) _param0;
    TimeSpan? nullable1 = (TimeSpan?) _param1;
    if (!nullable1.HasValue)
      return _param1;
    if (!glA76LmrlQ4YzEjd.\u0023\u003DzbEc2QrSSD6cXzW5oVw\u003D\u003D().HasValue)
      return (object) null;
    TimeSpan timeSpan1 = nullable1.Value;
    TimeSpan? nullable2 = glA76LmrlQ4YzEjd.\u0023\u003DzbEc2QrSSD6cXzW5oVw\u003D\u003D();
    TimeSpan timeSpan2 = nullable2.Value;
    if (!(timeSpan1 < timeSpan2))
    {
      long ticks1 = nullable1.Value.Ticks;
      nullable2 = glA76LmrlQ4YzEjd.\u0023\u003DzbEc2QrSSD6cXzW5oVw\u003D\u003D();
      long ticks2 = nullable2.Value.Ticks;
      if (ticks1 % ticks2 == 0L)
        return _param1;
    }
    return (object) glA76LmrlQ4YzEjd.\u0023\u003DzbEc2QrSSD6cXzW5oVw\u003D\u003D();
  }

  public override IndexRange  \u0023\u003DzVAnbwOJn98Ya(
    IndexRange  _param1)
  {
    TimeSpan? nullable = this.\u0023\u003DzbEc2QrSSD6cXzW5oVw\u003D\u003D();
    if (!nullable.HasValue)
      return _param1;
    TimeSpan? timeframe2 = this.Timeframe2;
    ref TimeSpan? local1 = ref timeframe2;
    TimeSpan valueOrDefault;
    long val1;
    if (!local1.HasValue)
    {
      val1 = 0L;
    }
    else
    {
      valueOrDefault = local1.GetValueOrDefault();
      val1 = valueOrDefault.Ticks;
    }
    TimeSpan? timeframe3 = this.Timeframe3;
    ref TimeSpan? local2 = ref timeframe3;
    long val2;
    if (!local2.HasValue)
    {
      val2 = 0L;
    }
    else
    {
      valueOrDefault = local2.GetValueOrDefault();
      val2 = valueOrDefault.Ticks;
    }
    long num1 = Math.Max(val1, val2);
    valueOrDefault = nullable.Value;
    long ticks = valueOrDefault.Ticks;
    int num2 = (int) (num1 / ticks);
    return new IndexRange (_param1.Min - num2 + 1, _param1.Max + num2 - 1);
  }

  protected override void \u0023\u003DzKsKC4kB3l9RI(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    BoxVolumeRenderableSeries.\u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D kvt89B1lUeA7EdfukJs = new BoxVolumeRenderableSeries.\u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D();
    if (!(this.DataSeries is TimeframeSegmentDataSeries))
      return;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    TimeSpan? nullable1 = this.\u0023\u003DzbEc2QrSSD6cXzW5oVw\u003D\u003D();
    TimeSpan? nullable2 = nullable1;
    TimeSpan zero1 = TimeSpan.Zero;
    TimeSpan? nullable3 = (nullable2.HasValue ? (nullable2.GetValueOrDefault() > zero1 ? 1 : 0) : 0) != 0 ? this.Timeframe2 : new TimeSpan?();
    TimeSpan? nullable4 = nullable1;
    TimeSpan zero2 = TimeSpan.Zero;
    TimeSpan? nullable5;
    if ((nullable4.HasValue ? (nullable4.GetValueOrDefault() > zero2 ? 1 : 0) : 0) == 0)
    {
      nullable4 = new TimeSpan?();
      nullable5 = nullable4;
    }
    else
      nullable5 = this.Timeframe3;
    TimeSpan? nullable6 = nullable5;
    Color cellFontColor = this.CellFontColor;
    Color highVolColor = this.HighVolColor;
    Color highVolBackground = this.HighVolBackground;
    double height = _param1.\u0023\u003Dz8DEW4l1E337F().Height;
    \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D anaOdfaeo1Ed4fSw = (\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D) this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] j9sJkRf4wMmhD3hBArray = anaOdfaeo1Ed4fSw.\u0023\u003Dz_xjf3ZVIHzP_();
    int length = j9sJkRf4wMmhD3hBArray.Length;
    double num1 = anaOdfaeo1Ed4fSw.\u0023\u003DzTmtGqP_rl3YU6gjEDQ\u003D\u003D();
    IndexRange  visibleRange = anaOdfaeo1Ed4fSw.VisibleRange;
    if (length == 0)
      return;
    if (nullable1.HasValue)
    {
      nullable4 = nullable3;
      TimeSpan? nullable7 = nullable1;
      if ((nullable4.HasValue & nullable7.HasValue ? (nullable4.GetValueOrDefault() < nullable7.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      {
        nullable7 = nullable6;
        nullable4 = nullable1;
        if ((nullable7.HasValue & nullable4.HasValue ? (nullable7.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0 && (!nullable3.HasValue || nullable3.Value.Ticks % nullable1.Value.Ticks == 0L) && (!nullable6.HasValue || nullable6.Value.Ticks % nullable1.Value.Ticks == 0L))
          goto label_11;
      }
      throw new InvalidOperationException($"invalid timeframes. tf1={nullable1}, tf2={nullable3}, tf3={nullable6}");
    }
label_11:
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("BoxVolume: started render {0} segments. Indexes: {1}-{2}, VisibleRange: {3}-{4}", new object[5]
    {
      (object) j9sJkRf4wMmhD3hBArray.Length,
      (object) j9sJkRf4wMmhD3hBArray[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D(),
      (object) j9sJkRf4wMmhD3hBArray[j9sJkRf4wMmhD3hBArray.Length - 1].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D(),
      (object) visibleRange.Min,
      (object) visibleRange.Max
    });
    double val1_1 = Math.Abs(xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(1.0) - xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(0.0));
    double num2 = Math.Abs(xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hBArray[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D()) - xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hBArray[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() + num1));
    double num3 = num2 / 2.0;
    double num4 = val1_1 / 2.0;
    kvt89B1lUeA7EdfukJs.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D = xkzemsMs5tGkouk5w2.GetDataValue(-num2);
    kvt89B1lUeA7EdfukJs.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D = xkzemsMs5tGkouk5w2.GetDataValue(height + num2);
    long? ticks1 = nullable3?.Ticks;
    long? ticks2 = nullable1?.Ticks;
    long? nullable8 = ticks1.HasValue & ticks2.HasValue ? new long?(ticks1.GetValueOrDefault() / ticks2.GetValueOrDefault()) : new long?();
    long val1_2 = nullable8 ?? 1L;
    nullable8 = nullable6?.Ticks;
    long? ticks3 = nullable1?.Ticks;
    long val2 = (nullable8.HasValue & ticks3.HasValue ? new long?(nullable8.GetValueOrDefault() / ticks3.GetValueOrDefault()) : new long?()) ?? 1L;
    List<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB> source1 = new List<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>((int) Math.Max(val1_2, val2));
    if (kvt89B1lUeA7EdfukJs.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D > kvt89B1lUeA7EdfukJs.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D)
      throw new InvalidOperationException($"minDrawPrice({kvt89B1lUeA7EdfukJs.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D}) > maxDrawPrice({kvt89B1lUeA7EdfukJs.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D})");
    using (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D vQiJuKqUi9jtIaha = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, false, (float) this.StrokeThickness, this.Opacity))
    {
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J1 = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.Timeframe3Color);
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J2 = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.Timeframe2Color);
      Color color1 = this.Timeframe2Color;
      IBrush2D xrgcdFbSdWgN9GcT8_1 = vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(Color.FromArgb((byte) ((uint) color1.A / 2U), color1.R, color1.G, color1.B));
      color1 = this.Timeframe3Color;
      IBrush2D xrgcdFbSdWgN9GcT8_2 = vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(Color.FromArgb((byte) ((uint) color1.A / 2U), color1.R, color1.G, color1.B));
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J3 = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.Timeframe2FrameColor);
      \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
      if (nullable6.HasValue)
      {
        TimeSpan? nullable9 = nullable6;
        int num5;
        for (int index = 0; index < length; index = num5 + 1)
        {
          \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz9oVGnVjC1spU(source1, j9sJkRf4wMmhD3hBArray, index, nullable9.Value);
          num5 = index + (source1.Count - 1);
          (double num6, double num7) = \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003Dz\u0024zWmmGTAbDON(source1.Select<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>(BoxVolumeRenderableSeries.SomeClass34343383.\u0023\u003DzmsyM35DYmF_LBTPpdw\u003D\u003D ?? (BoxVolumeRenderableSeries.SomeClass34343383.\u0023\u003DzmsyM35DYmF_LBTPpdw\u003D\u003D = new Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>(BoxVolumeRenderableSeries.SomeClass34343383.SomeMethond0343.\u0023\u003DzAX4p\u0024u4IoeCbuab4z\u0024EgZIE\u003D))));
          double num8 = xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(source1[0].\u0023\u003Dz2_4KSTY\u003D()) - num4;
          \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w3 = xkzemsMs5tGkouk5w1;
          List<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB> j9sJkRf4wMmhD3hBList = source1;
          double num9 = j9sJkRf4wMmhD3hBList[j9sJkRf4wMmhD3hBList.Count - 1].\u0023\u003Dz2_4KSTY\u003D();
          double num10 = xkzemsMs5tGkouk5w3.\u0023\u003DzhL6gsJw\u003D(num9) + num4;
          double num11 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num7) - num3;
          double num12 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num6) + num3;
          int num13 = 1 + (int) Math.Round((num7 - num6) / num1);
          \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzIhSOYOaWRQ6n(_param1, iluL6N4L8CsqVgQq, new Point(num8, num11), new Point(num10, num12), source1.Count, num13, rhwYsZxA33iRu6Id7J1, rhwYsZxA33iRu6Id7J1, xrgcdFbSdWgN9GcT8_2);
        }
      }
      if (nullable3.HasValue)
      {
        TimeSpan? nullable10 = nullable3;
        int num14;
        for (int index = 0; index < length; index = num14 + 1)
        {
          \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz9oVGnVjC1spU(source1, j9sJkRf4wMmhD3hBArray, index, nullable10.Value);
          num14 = index + (source1.Count - 1);
          (double num15, double num16) = \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003Dz\u0024zWmmGTAbDON(source1.Select<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>(BoxVolumeRenderableSeries.SomeClass34343383.\u0023\u003Dzkt5Z2jROBjPMkf1ugQ\u003D\u003D ?? (BoxVolumeRenderableSeries.SomeClass34343383.\u0023\u003Dzkt5Z2jROBjPMkf1ugQ\u003D\u003D = new Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>(BoxVolumeRenderableSeries.SomeClass34343383.SomeMethond0343.\u0023\u003Dz9pKdOVCrootEnagP5\u0024\u0024duuk\u003D))));
          double num17 = xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(source1[0].\u0023\u003Dz2_4KSTY\u003D()) - num4;
          \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w4 = xkzemsMs5tGkouk5w1;
          List<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB> j9sJkRf4wMmhD3hBList = source1;
          double num18 = j9sJkRf4wMmhD3hBList[j9sJkRf4wMmhD3hBList.Count - 1].\u0023\u003Dz2_4KSTY\u003D();
          double num19 = xkzemsMs5tGkouk5w4.\u0023\u003DzhL6gsJw\u003D(num18) + num4;
          double num20 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num16) - num3;
          double num21 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num15) + num3;
          int num22 = 1 + (int) Math.Round((num16 - num15) / num1);
          \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzIhSOYOaWRQ6n(_param1, iluL6N4L8CsqVgQq, new Point(num17, num20), new Point(num19, num21), source1.Count, num22, rhwYsZxA33iRu6Id7J3, rhwYsZxA33iRu6Id7J2, xrgcdFbSdWgN9GcT8_1);
        }
      }
      Size size = new Size(val1_1, num2);
      IBrush2D xrgcdFbSdWgN9GcT8_3 = vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(cellFontColor);
      vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(highVolColor);
      IBrush2D xrgcdFbSdWgN9GcT8_4 = vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(highVolBackground);
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J4 = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.\u0023\u003Dzc3UwYbhl1TD\u0024(), new float?((float) (this.StrokeThickness + 2)));
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J5 = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(this.\u0023\u003DzMrEHemSZ_hHJ(), new float?((float) (this.StrokeThickness + 2)));
      if (val1_1 >= 2.0 && num2 >= 2.0)
      {
        for (int index = 0; index < length; ++index)
        {
          \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB j9sJkRf4wMmhD3hB = j9sJkRf4wMmhD3hBArray[index];
          double num23 = xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hB.\u0023\u003Dz2_4KSTY\u003D()) - num4;
          (KeyValuePair<double, CandlePriceLevel>[] source2, Decimal num24) = j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dzb5KHU\u00247RutjHsWssog\u003D\u003D(num1);
          bool flag1 = this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003Dzyv\u0024EfaBUnbgQ(size, num24.GetDecimalLength());
          double? nullable11 = new double?();
          double num25 = 0.0;
          foreach ((double key, CandlePriceLevel candlePriceLevel1) in ((IEnumerable<KeyValuePair<double, CandlePriceLevel>>) source2).Where<KeyValuePair<double, CandlePriceLevel>>(kvt89B1lUeA7EdfukJs.\u0023\u003DzoD2HtVGZvKav ?? (kvt89B1lUeA7EdfukJs.\u0023\u003DzoD2HtVGZvKav = new Func<KeyValuePair<double, CandlePriceLevel>, bool>(kvt89B1lUeA7EdfukJs.\u0023\u003DzpVAgEOwSabx5AMO9Ew\u003D\u003D))))
          {
            double num26 = key;
            CandlePriceLevel candlePriceLevel2 = candlePriceLevel1;
            double num27 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num26) - num3;
            key = nullable11.GetValueOrDefault();
            if (!nullable11.HasValue)
            {
              key = num27;
              nullable11 = new double?(key);
            }
            num25 = num27;
            Rect rect1 = new Rect(num23, num27, val1_1, num2);
            Rect rect2 = new Rect(num23 + 1.0, num27 + 1.0, val1_1 - 2.0, num2 - 2.0);
            Decimal totalVolume = ((CandlePriceLevel) ref candlePriceLevel2).TotalVolume;
            if (totalVolume > 0M)
            {
              bool flag2 = totalVolume == num24;
              if (flag1)
              {
                if (flag2)
                  _param1.\u0023\u003DzVRUUvzhAr5SR(xrgcdFbSdWgN9GcT8_4, rect2.TopLeft, rect2.BottomRight, 0.0);
                string str = totalVolume.ToString();
                (float, FontWeight, bool) tuple = this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003DzwjCzmT8\u003D(size, totalVolume.GetDecimalLength(), 0.0f);
                float num28 = tuple.Item1;
                FontWeight fontWeight = tuple.Item2;
                if (tuple.Item3)
                {
                  Color color2 = flag2 ? highVolColor : cellFontColor;
                  _param1.\u0023\u003DzI6mwN\u0024I\u003D(str, rect1, AlignmentX.Center, AlignmentY.Center, color2, num28, this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003DzfFpWmUYdz7xm(), fontWeight);
                }
              }
              else
                _param1.\u0023\u003DzVRUUvzhAr5SR(flag2 ? xrgcdFbSdWgN9GcT8_4 : xrgcdFbSdWgN9GcT8_3, rect2.TopLeft, rect2.BottomRight, 0.0);
            }
          }
          if (nullable11.HasValue && j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D().HasValue)
          {
            double? nullable12 = j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D();
            key = j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DznrHfMbDuUs5Ac94Iyw\u003D\u003D();
            \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J6 = nullable12.GetValueOrDefault() <= key & nullable12.HasValue ? rhwYsZxA33iRu6Id7J4 : rhwYsZxA33iRu6Id7J5;
            _param1.\u0023\u003Dzk8_eoWQ\u003D(rhwYsZxA33iRu6Id7J6, new Point(num23, nullable11.Value), new Point(num23, num25 + num2));
          }
        }
      }
      else
      {
        if (nullable3.HasValue || nullable6.HasValue)
          return;
        for (int index = 0; index < length; ++index)
        {
          \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB j9sJkRf4wMmhD3hB = j9sJkRf4wMmhD3hBArray[index];
          double num29 = xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hB.\u0023\u003Dz2_4KSTY\u003D()) - num4;
          if (j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() <= kvt89B1lUeA7EdfukJs.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D && j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D() >= kvt89B1lUeA7EdfukJs.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D)
          {
            double num30 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D());
            double num31 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D());
            Rect rect = new Rect(num29, num31, Math.Max(val1_1, 1.0), Math.Max(Math.Abs(num31 - num30), 1.0));
            _param1.\u0023\u003DzVRUUvzhAr5SR(xrgcdFbSdWgN9GcT8_3, rect.TopLeft, rect.BottomRight, 0.0);
          }
        }
      }
    }
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly BoxVolumeRenderableSeries.SomeClass34343383 SomeMethond0343 = new BoxVolumeRenderableSeries.SomeClass34343383();
    public static Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> \u0023\u003DzmsyM35DYmF_LBTPpdw\u003D\u003D;
    public static Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> \u0023\u003Dzkt5Z2jROBjPMkf1ugQ\u003D\u003D;

    internal \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj \u0023\u003DzAX4p\u0024u4IoeCbuab4z\u0024EgZIE\u003D(
      \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB _param1)
    {
      return _param1.\u0023\u003Dz0IPdd6wsmxZJ();
    }

    internal \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj \u0023\u003Dz9pKdOVCrootEnagP5\u0024\u0024duuk\u003D(
      \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB _param1)
    {
      return _param1.\u0023\u003Dz0IPdd6wsmxZJ();
    }
  }

  private sealed class \u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D
  {
    public double \u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D;
    public double \u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D;
    public Func<KeyValuePair<double, CandlePriceLevel>, bool> \u0023\u003DzoD2HtVGZvKav;

    internal bool \u0023\u003DzpVAgEOwSabx5AMO9Ew\u003D\u003D(
      KeyValuePair<double, CandlePriceLevel> _param1)
    {
      return _param1.Key > this.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D && _param1.Key < this.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D;
    }
  }
}
