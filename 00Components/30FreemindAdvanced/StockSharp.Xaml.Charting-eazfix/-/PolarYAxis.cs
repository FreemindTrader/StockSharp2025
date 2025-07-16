// Decompiled with JetBrains decompiler
// Type: -.PolarYAxis
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class PolarYAxis : 
  NumericAxis
{
  
  public static readonly DependencyProperty \u0023\u003DzyrnkYrk\u003D = DependencyProperty.Register(nameof (Angle), typeof (double), typeof (PolarYAxis), new PropertyMetadata((object) 0.0));

  public PolarYAxis()
  {
    this.DefaultStyleKey = (object) typeof (PolarYAxis);
  }

  public double Angle
  {
    get
    {
      return (double) this.GetValue(PolarYAxis.\u0023\u003DzyrnkYrk\u003D);
    }
    set
    {
      this.SetValue(PolarYAxis.\u0023\u003DzyrnkYrk\u003D, (object) value);
    }
  }

  public override bool IsPolarAxis => true;

  protected override void \u0023\u003DzbUPOl6ZpNIOI(
    IRenderContext2D _param1,
    Style _param2,
    IEnumerable<float> _param3)
  {
    this.\u0023\u003DzeEl93ifUiK4P.Style = _param2;
    dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.SetTheme((DependencyObject) this.\u0023\u003DzeEl93ifUiK4P, dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.GetTheme((DependencyObject) this));
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzQCf7bpfi0DqGMauSow\u003D\u003D(this.\u0023\u003DzeEl93ifUiK4P, false))
    {
      if (this.IsXAxis)
        return;
      Point point = this.Services.GetService<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8().\u0023\u003DzsTReN_n58EEf(new Point(0.0, 0.0));
      SolidColorBrush solidColorBrush = new SolidColorBrush();
      using (IBrush2D xrgcdFbSdWgN9GcT8 = _param1.\u0023\u003Dze8WyDhI\u003D((Brush) solidColorBrush, 1.0, \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerPrimitive))
      {
        foreach (float num in _param3)
          _param1.\u0023\u003DzIZCdW2WR6Rxw(rhwYsZxA33iRu6Id7J, xrgcdFbSdWgN9GcT8, point, (double) num * 2.0, (double) num * 2.0);
      }
    }
  }

  public override double \u0023\u003Dz4wEfDhMr\u0024V6c() => 0.0;

  public override \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003Dz0RktzzbyC\u002468()
  {
    \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY vcA0rHxkV5W8VrNvy = base.\u0023\u003Dz0RktzzbyC\u002468() with
    {
      \u0023\u003DzWaaoT4e35MUnXIdKsg\u003D\u003D = this.IsPolarAxis
    };
    if (Math.Abs(this.IsHorizontalAxis ? this.ActualWidth : this.ActualHeight) < double.Epsilon)
      vcA0rHxkV5W8VrNvy.\u0023\u003DzdTxNrgQ\u003D /= 2.0;
    return vcA0rHxkV5W8VrNvy;
  }
}
