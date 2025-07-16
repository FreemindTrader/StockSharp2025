// Decompiled with JetBrains decompiler
// Type: -.ClusterProfileRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Messages;
using StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class ClusterProfileRenderableSeries : 
  \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D
{
  
  public static readonly DependencyProperty \u0023\u003Dz3vhKFHvmUfSR = DependencyProperty.Register(nameof (SeparatorLineColor), typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Color.FromArgb((byte) 50, byte.MaxValue, byte.MaxValue, byte.MaxValue), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzTpSTo8U\u003D = DependencyProperty.Register(nameof (LineColor), typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Colors.DarkGray, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzFLdFQ9M\u003D = DependencyProperty.Register(nameof (TextColor), typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Color.FromRgb((byte) 90, (byte) 90, (byte) 90), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzpELAaZtMaBgQ = DependencyProperty.Register(nameof (ClusterColor), typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzcL2qfCOpQZK5 = DependencyProperty.Register(nameof (ClusterMaxColor), typeof (Color), typeof (ClusterProfileRenderableSeries), new PropertyMetadata((object) Colors.LimeGreen, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  private ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t \u0023\u003Dz1ShH2k5rZ_Qh;

  public ClusterProfileRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (ClusterProfileRenderableSeries);
  }

  public Color SeparatorLineColor
  {
    get
    {
      return (Color) this.GetValue(ClusterProfileRenderableSeries.\u0023\u003Dz3vhKFHvmUfSR);
    }
    set
    {
      this.SetValue(ClusterProfileRenderableSeries.\u0023\u003Dz3vhKFHvmUfSR, (object) value);
    }
  }

  public Color LineColor
  {
    get
    {
      return (Color) this.GetValue(ClusterProfileRenderableSeries.\u0023\u003DzTpSTo8U\u003D);
    }
    set
    {
      this.SetValue(ClusterProfileRenderableSeries.\u0023\u003DzTpSTo8U\u003D, (object) value);
    }
  }

  public Color TextColor
  {
    get
    {
      return (Color) this.GetValue(ClusterProfileRenderableSeries.\u0023\u003DzFLdFQ9M\u003D);
    }
    set
    {
      this.SetValue(ClusterProfileRenderableSeries.\u0023\u003DzFLdFQ9M\u003D, (object) value);
    }
  }

  public Color ClusterColor
  {
    get
    {
      return (Color) this.GetValue(ClusterProfileRenderableSeries.\u0023\u003DzpELAaZtMaBgQ);
    }
    set
    {
      this.SetValue(ClusterProfileRenderableSeries.\u0023\u003DzpELAaZtMaBgQ, (object) value);
    }
  }

  public Color ClusterMaxColor
  {
    get
    {
      return (Color) this.GetValue(ClusterProfileRenderableSeries.\u0023\u003DzcL2qfCOpQZK5);
    }
    set
    {
      this.SetValue(ClusterProfileRenderableSeries.\u0023\u003DzcL2qfCOpQZK5, (object) value);
    }
  }

  protected override void \u0023\u003DzKsKC4kB3l9RI(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    ClusterProfileRenderableSeries.\u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D vm6DexIkyzzokCaW = new ClusterProfileRenderableSeries.\u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D();
    if (!(this.DataSeries is TimeframeSegmentDataSeries))
      return;
    \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D anaOdfaeo1Ed4fSw = (\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D) this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] source = anaOdfaeo1Ed4fSw.\u0023\u003Dz_xjf3ZVIHzP_();
    double num1 = anaOdfaeo1Ed4fSw.\u0023\u003DzTmtGqP_rl3YU6gjEDQ\u003D\u003D();
    ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t zNgOs9Qu1W4t1 = new ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t();
    zNgOs9Qu1W4t1.\u0023\u003DzCwhW74E\u003D = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    zNgOs9Qu1W4t1.\u0023\u003Dzcd8FewQ\u003D = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    zNgOs9Qu1W4t1.\u0023\u003DzvTWpIBJwyiW8 = _param1.\u0023\u003Dz8DEW4l1E337F().Height;
    zNgOs9Qu1W4t1.\u0023\u003Dzv5V3epBeFArY = _param1.\u0023\u003Dz8DEW4l1E337F().Width;
    zNgOs9Qu1W4t1.\u0023\u003Dzo\u0024RDoi4\u003D = _param1;
    zNgOs9Qu1W4t1.\u0023\u003DzICsqKMvNb6xH = this.TextColor;
    ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t zNgOs9Qu1W4t2 = zNgOs9Qu1W4t1;
    this.\u0023\u003Dz1ShH2k5rZ_Qh = zNgOs9Qu1W4t1;
    ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t zNgOs9Qu1W4t3 = zNgOs9Qu1W4t2;
    zNgOs9Qu1W4t3.\u0023\u003DzfuDyOX2LBzuJ = Math.Abs(zNgOs9Qu1W4t3.\u0023\u003DzCwhW74E\u003D.\u0023\u003DzhL6gsJw\u003D(1.0) - zNgOs9Qu1W4t3.\u0023\u003DzCwhW74E\u003D.\u0023\u003DzhL6gsJw\u003D(0.0));
    zNgOs9Qu1W4t3.\u0023\u003Dzjn3THFzIvZVlaGcYZg\u003D\u003D = Math.Abs(zNgOs9Qu1W4t3.\u0023\u003Dzcd8FewQ\u003D.\u0023\u003DzhL6gsJw\u003D(source[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D()) - zNgOs9Qu1W4t3.\u0023\u003Dzcd8FewQ\u003D.\u0023\u003DzhL6gsJw\u003D(source[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() + num1));
    zNgOs9Qu1W4t3.\u0023\u003DzMA0J5dNH7C28 = zNgOs9Qu1W4t3.\u0023\u003DzfuDyOX2LBzuJ / 2.0;
    zNgOs9Qu1W4t3.\u0023\u003DzrMwTP3B_pVQXintTrQ\u003D\u003D = zNgOs9Qu1W4t3.\u0023\u003Dzjn3THFzIvZVlaGcYZg\u003D\u003D / 2.0;
    vm6DexIkyzzokCaW.\u0023\u003DzoVxmkd0\u003D = this.ClusterColor;
    vm6DexIkyzzokCaW.\u0023\u003Dz6GGz9n8\u003D = this.ClusterMaxColor;
    Color textColor = this.TextColor;
    double num2 = zNgOs9Qu1W4t3.\u0023\u003Dzcd8FewQ\u003D.GetDataValue(zNgOs9Qu1W4t3.\u0023\u003DzvTWpIBJwyiW8 + zNgOs9Qu1W4t3.\u0023\u003Dzjn3THFzIvZVlaGcYZg\u003D\u003D);
    double num3 = zNgOs9Qu1W4t3.\u0023\u003Dzcd8FewQ\u003D.GetDataValue(-zNgOs9Qu1W4t3.\u0023\u003Dzjn3THFzIvZVlaGcYZg\u003D\u003D);
    vm6DexIkyzzokCaW.\u0023\u003DzEB3J6TW\u0024NW8A = anaOdfaeo1Ed4fSw.VisibleRange;
    vm6DexIkyzzokCaW.\u0023\u003DzYGnDwpm8DuE5 = this.\u0023\u003DzdOa9LxKWm9_R();
    vm6DexIkyzzokCaW.\u0023\u003Dz2f9hyDdoCC2j = this.\u0023\u003DzMGEDv1J2E_VL();
    if (source.Length < 1)
      return;
    if (num2 > num3)
      throw new InvalidOperationException($"minDrawPrice({num2}) > maxDrawPrice({num3})");
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D ao0kEjY6wcnQ6fBfXg = \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
    object[] objArray = new object[5]
    {
      (object) source.Length,
      (object) source[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D(),
      null,
      null,
      null
    };
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] j9sJkRf4wMmhD3hBArray = source;
    objArray[2] = (object) j9sJkRf4wMmhD3hBArray[j9sJkRf4wMmhD3hBArray.Length - 1].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D();
    objArray[3] = (object) vm6DexIkyzzokCaW.\u0023\u003DzEB3J6TW\u0024NW8A.Min;
    objArray[4] = (object) vm6DexIkyzzokCaW.\u0023\u003DzEB3J6TW\u0024NW8A.Max;
    ao0kEjY6wcnQ6fBfXg.\u0023\u003Dz3jAE7bQ\u003D("ClusterProfile: started render {0} segments. Indexes: {1}-{2}, VisibleRange: {3}-{4}", objArray);
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] array = ((IEnumerable<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>) source).Where<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>(new Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, bool>(vm6DexIkyzzokCaW.\u0023\u003DzkaVrqD4ggnSfmpwokg\u003D\u003D)).ToArray<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>();
    if (!\u0023\u003DzsIIzg9COgILMyUKVNisy8sT1ePq3.\u0023\u003DzDCv6G5Q\u003D<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>(array))
      return;
    vm6DexIkyzzokCaW.\u0023\u003Dz\u0024sDnaZw\u003D = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, false, (float) this.StrokeThickness, this.Opacity);
    try
    {
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J1 = vm6DexIkyzzokCaW.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this.LineColor);
      zNgOs9Qu1W4t3.\u0023\u003DzjvQqbJNxf61B = vm6DexIkyzzokCaW.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this.SeparatorLineColor);
      bool flag = zNgOs9Qu1W4t3.\u0023\u003DzfuDyOX2LBzuJ >= 3.0;
      int num4 = this.StrokeThickness + 2;
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J2 = vm6DexIkyzzokCaW.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this.\u0023\u003Dzc3UwYbhl1TD\u0024(), new float?((float) num4));
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J3 = vm6DexIkyzzokCaW.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this.\u0023\u003DzMrEHemSZ_hHJ(), new float?((float) num4));
      foreach (\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB j9sJkRf4wMmhD3hB in array)
      {
        ClusterProfileRenderableSeries.\u0023\u003DzNWRG_3QX2TpIkVlC1gGPnPM\u003D qx2TpIkVlC1gGpnPm = new ClusterProfileRenderableSeries.\u0023\u003DzNWRG_3QX2TpIkVlC1gGPnPM\u003D();
        qx2TpIkVlC1gGpnPm.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D = vm6DexIkyzzokCaW;
        \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj ltdBuqAinydlZs6uj = j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ();
        double num5 = zNgOs9Qu1W4t3.\u0023\u003DzCwhW74E\u003D.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hB.\u0023\u003Dz2_4KSTY\u003D());
        double zfuDyOx2LbzuJ = zNgOs9Qu1W4t3.\u0023\u003DzfuDyOX2LBzuJ;
        double num6 = num5;
        if (j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() <= num3 && ltdBuqAinydlZs6uj.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D() >= num2)
        {
          double num7 = zNgOs9Qu1W4t3.\u0023\u003Dzcd8FewQ\u003D.\u0023\u003DzhL6gsJw\u003D(ltdBuqAinydlZs6uj.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D()) - zNgOs9Qu1W4t3.\u0023\u003DzrMwTP3B_pVQXintTrQ\u003D\u003D;
          double num8 = zNgOs9Qu1W4t3.\u0023\u003Dzcd8FewQ\u003D.\u0023\u003DzhL6gsJw\u003D(ltdBuqAinydlZs6uj.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D()) + zNgOs9Qu1W4t3.\u0023\u003DzrMwTP3B_pVQXintTrQ\u003D\u003D;
          (qx2TpIkVlC1gGpnPm.\u0023\u003DzVx\u0024XeSiQ8z2B, qx2TpIkVlC1gGpnPm.\u0023\u003DztZE33alm_OfT) = ltdBuqAinydlZs6uj.\u0023\u003Dzb5KHU\u00247RutjHsWssog\u003D\u003D(num1);
          _param1.\u0023\u003Dzk8_eoWQ\u003D(rhwYsZxA33iRu6Id7J1, new Point(num6, num7 + 1.0), new Point(num6, num8));
          if (flag)
          {
            ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3 zZk6lEwFfR3 = new ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3(qx2TpIkVlC1gGpnPm.\u0023\u003DzVx\u0024XeSiQ8z2B.Length, (double) qx2TpIkVlC1gGpnPm.\u0023\u003DztZE33alm_OfT, new Action<ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3>(qx2TpIkVlC1gGpnPm.\u0023\u003DzKQDGlIy9KhTj9Lexow\u003D\u003D));
            zZk6lEwFfR3.\u0023\u003DzXYIhitL0hhv8(textColor);
            double num9 = zNgOs9Qu1W4t3.\u0023\u003DzfuDyOX2LBzuJ - 1.0;
            this.\u0023\u003DzI2RPSVn5Y6gq(ltdBuqAinydlZs6uj, zZk6lEwFfR3, zNgOs9Qu1W4t3, num6 + 1.0, num9, (ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX) 3, 0.0f, (float) num4, rhwYsZxA33iRu6Id7J2, rhwYsZxA33iRu6Id7J3);
          }
        }
      }
    }
    finally
    {
      if (vm6DexIkyzzokCaW.\u0023\u003Dz\u0024sDnaZw\u003D != null)
        vm6DexIkyzzokCaW.\u0023\u003Dz\u0024sDnaZw\u003D.Dispose();
    }
  }

  private void \u0023\u003DzI2RPSVn5Y6gq(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj _param1,
    ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3 _param2,
    ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t _param3,
    double _param4,
    double _param5,
    ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX _param6,
    float _param7,
    float _param8,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param9,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param10)
  {
    ClusterProfileRenderableSeries.SomeClass343 vqd1Qhu2nAw1nzwT0;
    vqd1Qhu2nAw1nzwT0._variableSome3535 = this;
    vqd1Qhu2nAw1nzwT0.\u0023\u003Dzn1rOk95o\u0024Igg = _param7;
    vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D = _param3;
    if (_param5 <= 1.0)
      return;
    double num1 = Math.Max(1.0, _param5 - 5.0);
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w;
    int num2;
    int num3;
    if (_param6 == (ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX) 0 || _param6 == (ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX) 2)
    {
      vqd1Qhu2nAw1nzwT0.\u0023\u003DzyHjx46Y\u003D = true;
      xkzemsMs5tGkouk5w = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003DzCwhW74E\u003D;
      num2 = 1;
      num3 = _param6 == (ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX) 0 ? -1 : 1;
    }
    else
    {
      vqd1Qhu2nAw1nzwT0.\u0023\u003DzyHjx46Y\u003D = false;
      xkzemsMs5tGkouk5w = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzcd8FewQ\u003D;
      num2 = _param6 == (ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX) 3 ? 1 : -1;
      num3 = -1;
    }
    _param2.Reset();
    if (vqd1Qhu2nAw1nzwT0.\u0023\u003DzyHjx46Y\u003D)
    {
      bool flag = (double) vqd1Qhu2nAw1nzwT0.\u0023\u003Dzn1rOk95o\u0024Igg > 0.0 || this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003Dzyv\u0024EfaBUnbgQ(new Size(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003DzfuDyOX2LBzuJ, this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003Dz6puXp2RlkrXBtk\u0024DRw\u003D\u003D()), 1);
      AlignmentY alignmentY = _param6 == (ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX) 0 ? AlignmentY.Top : AlignmentY.Bottom;
      double num4 = _param2.MaxValue;
      while (_param2.\u0023\u003DzMecBUKQ\u003D())
      {
        CandlePriceLevel candlePriceLevel = _param2.Value;
        double num5 = xkzemsMs5tGkouk5w.\u0023\u003DzhL6gsJw\u003D(_param2.\u0023\u003Dzj_WGgNBd9Q\u00248());
        double num6 = _param4;
        double num7 = num5 + vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003DzfuDyOX2LBzuJ - 1.0;
        double num8 = num6 + (double) num3 * (num1 * (double) ((CandlePriceLevel) ref candlePriceLevel).TotalVolume / num4);
        if (num5 > num7)
          \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.Swap(ref num5, ref num7);
        if (num6 > num8)
          \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.Swap(ref num6, ref num8);
        if (num8 - num6 >= 0.5)
        {
          Point point1 = new Point(num5, num6);
          Point point2 = new Point(num7, num8);
          this.\u0023\u003DqzeqmnZTqb5SATfsIQ_O5sWIkga1\u0024q50R1X9YE2NjksY\u003D(point1, point2, _param2, ref vqd1Qhu2nAw1nzwT0);
          if (_param2.\u0023\u003DzvrdHPuKzjDX2() != null)
            vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003Dz7zUbWtTKc3tA(_param2.\u0023\u003DzvrdHPuKzjDX2(), point1, point2);
          if (flag)
          {
            double num9 = num6 + 2.0;
            this.\u0023\u003DqzGXhMsmVCYSyMDoXdrCgMm8RfgHFHxnEzYaDa133vfcf5PZsq1idcRiykHpkrWhj(_param2, candlePriceLevel, new Point(num5, num9), new Point(num7, num6 + (double) num3 * num1), AlignmentX.Center, alignmentY, ref vqd1Qhu2nAw1nzwT0);
          }
        }
      }
    }
    else
    {
      bool flag = (double) vqd1Qhu2nAw1nzwT0.\u0023\u003Dzn1rOk95o\u0024Igg > 0.0 || vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzjn3THFzIvZVlaGcYZg\u003D\u003D >= this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003Dz6puXp2RlkrXBtk\u0024DRw\u003D\u003D() && num1 >= this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003DzYu4\u0024yZJXP2Ni2CKGCQ\u003D\u003D();
      AlignmentX alignmentX = _param6 == (ClusterProfileRenderableSeries.\u0023\u003Dz8jhANg_IhueX) 3 ? AlignmentX.Left : AlignmentX.Right;
      double num10 = _param2.MaxValue;
      if (_param1.\u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D().HasValue)
      {
        double? nullable = _param1.\u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D();
        double num11 = _param1.\u0023\u003DznrHfMbDuUs5Ac94Iyw\u003D\u003D();
        \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = nullable.GetValueOrDefault() <= num11 & nullable.HasValue ? _param9 : _param10;
        double num12 = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzcd8FewQ\u003D.\u0023\u003DzhL6gsJw\u003D(_param1.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D());
        double num13 = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzcd8FewQ\u003D.\u0023\u003DzhL6gsJw\u003D(_param1.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D());
        vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003Dzk8_eoWQ\u003D(rhwYsZxA33iRu6Id7J, new Point(_param4, num12), new Point(_param4, num13));
        _param4 += (double) _param8;
      }
      while (_param2.\u0023\u003DzMecBUKQ\u003D())
      {
        CandlePriceLevel candlePriceLevel = _param2.Value;
        double num14 = _param4;
        double num15 = xkzemsMs5tGkouk5w.\u0023\u003DzhL6gsJw\u003D(_param2.\u0023\u003Dzj_WGgNBd9Q\u00248()) - (double) num3 * vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003DzrMwTP3B_pVQXintTrQ\u003D\u003D;
        double num16 = num14 + (num10 > 0.0 ? (double) num2 * (num1 * (double) ((CandlePriceLevel) ref candlePriceLevel).TotalVolume / num10) : 0.0);
        double num17 = num15 + (double) num3 * vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzjn3THFzIvZVlaGcYZg\u003D\u003D - (double) num3;
        if (num14 > num16)
          \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.Swap(ref num14, ref num16);
        if (num15 > num17)
          \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.Swap(ref num15, ref num17);
        if (num16 - num14 >= 0.5)
        {
          Point point3 = new Point(num14, num15);
          Point point4 = new Point(num16, num17);
          this.\u0023\u003DqzeqmnZTqb5SATfsIQ_O5sWIkga1\u0024q50R1X9YE2NjksY\u003D(point3, point4, _param2, ref vqd1Qhu2nAw1nzwT0);
          if (_param2.\u0023\u003DzvrdHPuKzjDX2() != null)
            vqd1Qhu2nAw1nzwT0.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003Dz7zUbWtTKc3tA(_param2.\u0023\u003DzvrdHPuKzjDX2(), point3, point4);
          if (flag)
          {
            double num18 = num14 + 2.0;
            this.\u0023\u003DqzGXhMsmVCYSyMDoXdrCgMm8RfgHFHxnEzYaDa133vfcf5PZsq1idcRiykHpkrWhj(_param2, candlePriceLevel, new Point(num18, num15), new Point(num18 + (num10 > 0.0 ? (double) num2 * num1 : 0.0), num17), alignmentX, AlignmentY.Center, ref vqd1Qhu2nAw1nzwT0);
          }
        }
      }
    }
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz1SLEyANHenbwANn\u0024\u0024w\u003D\u003D(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3)
  {
    ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t z1ShH2k5rZQh = this.\u0023\u003Dz1ShH2k5rZ_Qh;
    int num1 = _param2.\u0023\u003DzSkvCFWUKQ7Fw();
    if (!(this.DataSeries is TimeframeSegmentDataSeries dataSeries) || dataSeries.Count < 1 || z1ShH2k5rZQh == null || num1 < 0 || num1 >= dataSeries.Count)
      return \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    double num2 = z1ShH2k5rZQh.\u0023\u003DzCwhW74E\u003D.\u0023\u003DzhL6gsJw\u003D((double) num1);
    double num3 = num2 + z1ShH2k5rZQh.\u0023\u003DzfuDyOX2LBzuJ;
    if (_param1.X < num2 || _param1.X > num3)
      return \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    _param2.\u0023\u003Dzo2ftAfxjqC04(new Point(num3, _param2.\u0023\u003DzxZfJER0dbHuS().Y));
    return _param2;
  }

  private void \u0023\u003DqzGXhMsmVCYSyMDoXdrCgMm8RfgHFHxnEzYaDa133vfcf5PZsq1idcRiykHpkrWhj(
    ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3 _param1,
    CandlePriceLevel _param2,
    Point _param3,
    Point _param4,
    AlignmentX _param5,
    AlignmentY _param6,
    ref ClusterProfileRenderableSeries.SomeClass343 _param7)
  {
    string str = ((CandlePriceLevel) ref _param2).TotalVolume.ToString();
    Rect rect = new Rect(_param3, _param4);
    (float, FontWeight, bool) tuple = this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003DzwjCzmT8\u003D(rect.Size, str.Length, _param7.\u0023\u003Dzn1rOk95o\u0024Igg);
    float num = tuple.Item1;
    FontWeight fontWeight = tuple.Item2;
    if (!tuple.Item3)
      return;
    _param7.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003DzI6mwN\u0024I\u003D(str, rect, _param5, _param6, _param1.\u0023\u003DzyY8mVIfKiJZB(), num, this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003DzfFpWmUYdz7xm(), _param1.\u0023\u003DzvrdHPuKzjDX2() == null ? FontWeights.Normal : fontWeight);
  }

  private void \u0023\u003DqzeqmnZTqb5SATfsIQ_O5sWIkga1\u0024q50R1X9YE2NjksY\u003D(
    Point _param1,
    Point _param2,
    ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3 _param3,
    ref ClusterProfileRenderableSeries.SomeClass343 _param4)
  {
    CandlePriceLevel candlePriceLevel1 = _param3.Value;
    Decimal totalVolume = ((CandlePriceLevel) ref candlePriceLevel1).TotalVolume;
    if (totalVolume == 0M)
      return;
    CandlePriceLevel candlePriceLevel2 = _param3.Value;
    Decimal buyVolume = ((CandlePriceLevel) ref candlePriceLevel2).BuyVolume;
    CandlePriceLevel candlePriceLevel3 = _param3.Value;
    Decimal sellVolume = ((CandlePriceLevel) ref candlePriceLevel3).SellVolume;
    if (!this.\u0023\u003DzRNPBYH\u0024r1v_XVePnsQd4gRM\u003D() || buyVolume == 0M && sellVolume == 0M)
    {
      _param4.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003DzVRUUvzhAr5SR(_param3.\u0023\u003DzbkA2jkc3mMzd(), _param1, _param2, 0.0);
    }
    else
    {
      double num1 = (double) (buyVolume / totalVolume);
      double num2 = (double) (sellVolume / totalVolume);
      if (_param4.\u0023\u003DzyHjx46Y\u003D)
      {
        double num3 = _param2.Y - _param1.Y;
        double num4 = num3 * num1;
        double num5 = num3 * num2;
        double num6 = _param2.Y - num4;
        if (num4 > 0.0)
          _param4.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003DzVRUUvzhAr5SR(_param3.\u0023\u003Dz_RA752i\u0024ujMT(), new Point(_param1.X, num6), num5 > 0.0 ? _param2 : new Point(_param2.X, num6), 0.0);
        if (num5 <= 0.0)
          return;
        _param4.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003DzVRUUvzhAr5SR(_param3.\u0023\u003Dz19xf9agkvSZP(), _param1, new Point(_param2.X, num6), 0.0);
      }
      else
      {
        double num7 = _param2.X - _param1.X;
        double num8 = num7 * num1;
        double num9 = num7 * num2;
        double num10 = _param1.X + num8;
        if (num8 > 0.0)
          _param4.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003DzVRUUvzhAr5SR(_param3.\u0023\u003Dz_RA752i\u0024ujMT(), _param1, num9 > 0.0 ? new Point(num10, _param2.Y) : _param2, 0.0);
        if (num9 <= 0.0)
          return;
        _param4.\u0023\u003Dz7pohrFs\u003D.\u0023\u003Dzo\u0024RDoi4\u003D.\u0023\u003DzVRUUvzhAr5SR(_param3.\u0023\u003Dz19xf9agkvSZP(), new Point(num10, _param1.Y), _param2, 0.0);
      }
    }
  }

  private enum \u0023\u003Dz8jhANg_IhueX
  {
  }

  [StructLayout(LayoutKind.Auto)]
  private struct SomeClass343
  {
    
    public ClusterProfileRenderableSeries _variableSome3535;
    
    public float \u0023\u003Dzn1rOk95o\u0024Igg;
    
    public ClusterProfileRenderableSeries.\u0023\u003DzNgOs\u00249Qu1W4t \u0023\u003Dz7pohrFs\u003D;
    
    public bool \u0023\u003DzyHjx46Y\u003D;
  }

  private sealed class \u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D
  {
    public IndexRange  \u0023\u003DzEB3J6TW\u0024NW8A;
    public \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D \u0023\u003Dz\u0024sDnaZw\u003D;
    public Color \u0023\u003Dz6GGz9n8\u003D;
    public Color \u0023\u003DzoVxmkd0\u003D;
    public Color \u0023\u003DzYGnDwpm8DuE5;
    public Color \u0023\u003Dz2f9hyDdoCC2j;

    internal bool \u0023\u003DzkaVrqD4ggnSfmpwokg\u003D\u003D(
      \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB _param1)
    {
      return _param1.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D() >= this.\u0023\u003DzEB3J6TW\u0024NW8A.Min && _param1.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D() <= this.\u0023\u003DzEB3J6TW\u0024NW8A.Max;
    }
  }

  private sealed class \u0023\u003DzNWRG_3QX2TpIkVlC1gGPnPM\u003D
  {
    public KeyValuePair<double, CandlePriceLevel>[] \u0023\u003DzVx\u0024XeSiQ8z2B;
    public Decimal \u0023\u003DztZE33alm_OfT;
    public ClusterProfileRenderableSeries.\u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;

    internal void \u0023\u003DzKQDGlIy9KhTj9Lexow\u003D\u003D(
      ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3 _param1)
    {
      KeyValuePair<double, CandlePriceLevel> keyValuePair = this.\u0023\u003DzVx\u0024XeSiQ8z2B[_param1.\u0023\u003DzCMB4T5w\u003D()];
      _param1.\u0023\u003DzbAXAYTekRt44(keyValuePair.Key);
      _param1.Value = keyValuePair.Value;
      ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3 zZk6lEwFfR3 = _param1;
      CandlePriceLevel candlePriceLevel = keyValuePair.Value;
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = ((CandlePriceLevel) ref candlePriceLevel).TotalVolume == this.\u0023\u003DztZE33alm_OfT ? this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dz6GGz9n8\u003D) : (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) null;
      zZk6lEwFfR3.\u0023\u003DzD0deLsQPIPa1(rhwYsZxA33iRu6Id7J);
      _param1.\u0023\u003DzH2mMb6hVQv8L(this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003DzNryPIU0\u003D(this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzoVxmkd0\u003D));
      _param1.\u0023\u003DzWc4nteoFjlK8(this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003DzNryPIU0\u003D(this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzYGnDwpm8DuE5));
      _param1.\u0023\u003DzEzZ5jxwt3nr_(this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003DzNryPIU0\u003D(this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dz2f9hyDdoCC2j));
    }
  }

  private sealed class \u0023\u003DzNgOs\u00249Qu1W4t
  {
    public \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003DzCwhW74E\u003D;
    public \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003Dzcd8FewQ\u003D;
    public double \u0023\u003Dzv5V3epBeFArY;
    public double \u0023\u003DzvTWpIBJwyiW8;
    public double \u0023\u003DzfuDyOX2LBzuJ;
    public double \u0023\u003DzMA0J5dNH7C28;
    public double \u0023\u003Dzjn3THFzIvZVlaGcYZg\u003D\u003D;
    public double \u0023\u003DzrMwTP3B_pVQXintTrQ\u003D\u003D;
    public Color \u0023\u003DzICsqKMvNb6xH;
    public IRenderContext2D \u0023\u003Dzo\u0024RDoi4\u003D;
    public \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzjvQqbJNxf61B;
  }

  private sealed class \u0023\u003DzZk\u00246lEW\u0024ffR3(
    int _param1,
    double _param2,
    Action<ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3> _param3)
  {
    private readonly int \u0023\u003DztUQ677I\u003D = _param1;
    private readonly Action<ClusterProfileRenderableSeries.\u0023\u003DzZk\u00246lEW\u0024ffR3> \u0023\u003Dzk07uMPLrlfJU = _param3;
    private int \u0023\u003DzKx97DYo\u003D;
    private double \u0023\u003DzWYQbZV2cMJfI2mtzRyu_6hw\u003D;
    private CandlePriceLevel \u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D;
    private IBrush2D \u0023\u003DzBnafa9Z1RbkKZVQGZw\u003D\u003D;
    private IBrush2D \u0023\u003Dz1uicr92vv9K656pOaQRy5pI\u003D;
    private IBrush2D \u0023\u003DzBPf324Ov6mo\u0024BLMcjrukJjM\u003D;
    private Color \u0023\u003DzjLQ0oTDnIKFQFtRIXg\u003D\u003D;
    private readonly double \u0023\u003Dz_Fbd0JvmfxR0BaQ2VA\u003D\u003D = _param2;
    private \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dz4jeZsNFPe6\u0024QBz4C5w\u003D\u003D;

    public int \u0023\u003DzCMB4T5w\u003D() => this.\u0023\u003DzKx97DYo\u003D;

    public void Reset() => this.\u0023\u003DzKx97DYo\u003D = -1;

    public bool \u0023\u003DzMecBUKQ\u003D()
    {
      if (++this.\u0023\u003DzKx97DYo\u003D >= this.\u0023\u003DztUQ677I\u003D)
        return false;
      this.\u0023\u003Dzk07uMPLrlfJU(this);
      return true;
    }

    public double \u0023\u003Dzj_WGgNBd9Q\u00248()
    {
      return this.\u0023\u003DzWYQbZV2cMJfI2mtzRyu_6hw\u003D;
    }

    public void \u0023\u003DzbAXAYTekRt44(double _param1)
    {
      this.\u0023\u003DzWYQbZV2cMJfI2mtzRyu_6hw\u003D = _param1;
    }

    public CandlePriceLevel Value
    {
      get => this.\u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D;
      set => this.\u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D = value;
    }

    public IBrush2D \u0023\u003DzbkA2jkc3mMzd()
    {
      return this.\u0023\u003DzBnafa9Z1RbkKZVQGZw\u003D\u003D;
    }

    public void \u0023\u003DzH2mMb6hVQv8L(
      IBrush2D _param1)
    {
      this.\u0023\u003DzBnafa9Z1RbkKZVQGZw\u003D\u003D = _param1;
    }

    public IBrush2D \u0023\u003Dz_RA752i\u0024ujMT()
    {
      return this.\u0023\u003Dz1uicr92vv9K656pOaQRy5pI\u003D;
    }

    public void \u0023\u003DzWc4nteoFjlK8(
      IBrush2D _param1)
    {
      this.\u0023\u003Dz1uicr92vv9K656pOaQRy5pI\u003D = _param1;
    }

    public IBrush2D \u0023\u003Dz19xf9agkvSZP()
    {
      return this.\u0023\u003DzBPf324Ov6mo\u0024BLMcjrukJjM\u003D;
    }

    public void \u0023\u003DzEzZ5jxwt3nr_(
      IBrush2D _param1)
    {
      this.\u0023\u003DzBPf324Ov6mo\u0024BLMcjrukJjM\u003D = _param1;
    }

    public Color \u0023\u003DzyY8mVIfKiJZB() => this.\u0023\u003DzjLQ0oTDnIKFQFtRIXg\u003D\u003D;

    public void \u0023\u003DzXYIhitL0hhv8(Color _param1)
    {
      this.\u0023\u003DzjLQ0oTDnIKFQFtRIXg\u003D\u003D = _param1;
    }

    public double MaxValue => this.\u0023\u003Dz_Fbd0JvmfxR0BaQ2VA\u003D\u003D;

    public \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzvrdHPuKzjDX2()
    {
      return this.\u0023\u003Dz4jeZsNFPe6\u0024QBz4C5w\u003D\u003D;
    }

    public void \u0023\u003DzD0deLsQPIPa1(
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1)
    {
      this.\u0023\u003Dz4jeZsNFPe6\u0024QBz4C5w\u003D\u003D = _param1;
    }
  }
}
