// Decompiled with JetBrains decompiler
// Type: -.FastXORenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public sealed class FastXORenderableSeries : 
  FastOhlcRenderableSeries
{
  
  public static readonly DependencyProperty \u0023\u003DzIrmMBlMvjNiI = DependencyProperty.Register(nameof (XOBoxSize), typeof (double), typeof (FastXORenderableSeries), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));

  public FastXORenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastXORenderableSeries);
    this.SetCurrentValue(FastOhlcRenderableSeries.\u0023\u003DzVvc2lVdKTrj8, (object) 1.0);
  }

  public double XOBoxSize
  {
    get
    {
      return (double) this.GetValue(FastXORenderableSeries.\u0023\u003DzIrmMBlMvjNiI);
    }
    set
    {
      this.SetValue(FastXORenderableSeries.\u0023\u003DzIrmMBlMvjNiI, (object) value);
    }
  }

  protected override void \u0023\u003DzQcMZBOuAsfh7MaaJgQ\u003D\u003D(
    IRenderContext2D _param1,
    IRenderPassData _param2,
    \u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D _param3)
  {
    bool flag1 = _param2.\u0023\u003DzDoU1CJhSUWFV();
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    double xoBoxSize = this.XOBoxSize;
    int num1 = ftrixUnpTllY1PkTyq.\u0023\u003DzlpVGw6E\u003D();
    if (num1 == 1 || xoBoxSize <= 0.0)
      return;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    int num2 = this.\u0023\u003Dz6BuO4fnhj6SX(xkzemsMs5tGkouk5w1, ftrixUnpTllY1PkTyq, this.DataPointWidth);
    double num3 = Math.Abs(xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(xoBoxSize) - xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(0.0));
    bool flag2 = (double) num2 < 3.0 || num3 < 3.0;
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = this.PaletteProvider;
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
    IBrush2D xrgcdFbSdWgN9GcT8_1 = _param1.\u0023\u003Dze8WyDhI\u003D(this.UpWickColor, this.Opacity, new bool?());
    IBrush2D xrgcdFbSdWgN9GcT8_2 = _param1.\u0023\u003Dze8WyDhI\u003D(this.DownWickColor, this.Opacity, new bool?());
    for (int index = 0; index < num1; ++index)
    {
      \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR> iwzGyvO4YaqDkpiI = (\u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR>) ftrixUnpTllY1PkTyq.\u0023\u003Dz\u0024CeUvME\u003D(index);
      \u0023\u003DzUjQaO0YddGfcKRjWqdpaAJY6yR84WM_wxFXQ0dZvA0kR r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num4 = NumberUtil.RoundUp(r84WmWxFxQ0dZvA0kR.\u0023\u003DzolXXlhDBER_c(), xoBoxSize);
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num5 = NumberUtil.RoundDown(r84WmWxFxQ0dZvA0kR.\u0023\u003DzchuwVU\u00245sIH8(), xoBoxSize);
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double close1 = r84WmWxFxQ0dZvA0kR.Close;
      r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
      double num6 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzGze4a8XU7KvB();
      bool flag3 = close1 >= num6;
      int num7 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(iwzGyvO4YaqDkpiI.\u0023\u003Dz2_4KSTY\u003D()).\u0023\u003DzYNd6r7dW43yr();
      int num8 = ((double) num7 - (double) num2 * 0.5).\u0023\u003DzYNd6r7dW43yr();
      int num9 = ((double) num7 + (double) num2 * 0.5).\u0023\u003DzYNd6r7dW43yr();
      int val2_1 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num4).\u0023\u003DzYNd6r7dW43yr();
      int val1 = xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num5).\u0023\u003DzYNd6r7dW43yr();
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = flag3 ? this.\u0023\u003DzCuIJmYsDJgLxaAWxjg\u003D\u003D : this.\u0023\u003Dz\u0024a39MFSHPCym0Lo_LQ\u003D\u003D;
      IBrush2D xrgcdFbSdWgN9GcT8_3 = flag3 ? xrgcdFbSdWgN9GcT8_1 : xrgcdFbSdWgN9GcT8_2;
      if (paletteProvider != null)
      {
        \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D htg5ScQrmCkwmAaNyPa = paletteProvider;
        double num10 = iwzGyvO4YaqDkpiI.\u0023\u003Dz2_4KSTY\u003D();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num11 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzGze4a8XU7KvB();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num12 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzolXXlhDBER_c();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double num13 = r84WmWxFxQ0dZvA0kR.\u0023\u003DzchuwVU\u00245sIH8();
        r84WmWxFxQ0dZvA0kR = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
        double close2 = r84WmWxFxQ0dZvA0kR.Close;
        Color? nullable = htg5ScQrmCkwmAaNyPa.\u0023\u003DzLCyKrYI\u003D((IRenderableSeries) this, num10, num11, num12, num13, close2);
        if (nullable.HasValue)
        {
          rhwYsZxA33iRu6Id7J = _param3.\u0023\u003Dzc8S9rSE\u003D(nullable.Value, new float?());
          xrgcdFbSdWgN9GcT8_3 = _param1.\u0023\u003Dze8WyDhI\u003D(nullable.Value, this.Opacity, new bool?());
        }
      }
      int val2_2 = Math.Max(val1, val2_1);
      int val2_3 = Math.Min(val1, val2_1);
      if (flag2)
        _param1.\u0023\u003DzVRUUvzhAr5SR(xrgcdFbSdWgN9GcT8_3, this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num8, (double) val1), flag1), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num9, (double) val2_1), flag1), 0.0);
      else if (flag3)
      {
        for (double num14 = (double) val2_2; num14 > (double) val2_3; num14 -= num3)
        {
          double num15 = Math.Max(num14 - num3, (double) val2_3);
          if (num14 - num15 >= 3.0)
          {
            iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num8, num14), flag1), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num9, num15), flag1), rhwYsZxA33iRu6Id7J);
            iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num9, num14), flag1), this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num8, num15), flag1), rhwYsZxA33iRu6Id7J);
          }
        }
      }
      else
      {
        for (double num16 = (double) val2_3; num16 < (double) val2_2; num16 += num3)
        {
          double num17 = Math.Min(num16 + num3, (double) val2_2) - num16;
          if (num17 >= 3.0)
            _param1.\u0023\u003DzIZCdW2WR6Rxw(rhwYsZxA33iRu6Id7J, (IBrush2D) null, new Point((double) num7, num16 + num17 / 2.0), (double) num2, num17);
        }
      }
    }
  }
}
