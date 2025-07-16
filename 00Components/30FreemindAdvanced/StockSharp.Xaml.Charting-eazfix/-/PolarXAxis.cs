// Decompiled with JetBrains decompiler
// Type: -.PolarXAxis
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class PolarXAxis : 
  NumericAxis
{
  public PolarXAxis()
  {
    this.DefaultStyleKey = (object) typeof (PolarXAxis);
    this.TickCoordinatesProvider = (\u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrm\u0024HOuIKCn_Ala213x1NpKA) new \u0023\u003DzzsyKnUNUDKjF7rDv70izN57tMaDGMF0FACJEIzVgAJoem9mflw\u003D\u003D();
  }

  public override bool IsPolarAxis => true;

  public override bool IsHorizontalAxis => true;

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003DzMUpWftuaeZ8o().SizeChanged += new SizeChangedEventHandler(this.\u0023\u003Dz0uUIXFWQeRl24snRJn9H6i0\u003D);
  }

  public override \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003Dz0RktzzbyC\u002468()
  {
    return base.\u0023\u003Dz0RktzzbyC\u002468() with
    {
      \u0023\u003DzWaaoT4e35MUnXIdKsg\u003D\u003D = this.IsPolarAxis
    };
  }

  protected override double \u0023\u003Dz46iyKtU9fraN() => 0.0;

  protected override void \u0023\u003DzbUPOl6ZpNIOI(
    IRenderContext2D _param1,
    Style _param2,
    IEnumerable<float> _param3)
  {
    this.\u0023\u003DzeEl93ifUiK4P.Style = _param2;
    ThemeManager.SetTheme((DependencyObject) this.\u0023\u003DzeEl93ifUiK4P, ThemeManager.GetTheme((DependencyObject) this));
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzQCf7bpfi0DqGMauSow\u003D\u003D(this.\u0023\u003DzeEl93ifUiK4P, true))
    {
      if (!this.IsXAxis)
        return;
      float[] array = _param3.ToArray<float>();
      \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig = this.Services.GetService<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8();
      double num = \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(b99xo8DgCb3haWTig.\u0023\u003Dz8DEW4l1E337F());
      Point point1 = b99xo8DgCb3haWTig.\u0023\u003DzsTReN_n58EEf(new Point(0.0, 0.0));
      for (int index = 0; index < array.Length; ++index)
      {
        Point point2 = b99xo8DgCb3haWTig.\u0023\u003DzsTReN_n58EEf(new Point((double) array[index], num));
        _param1.\u0023\u003Dzk8_eoWQ\u003D(rhwYsZxA33iRu6Id7J, point1, point2);
      }
    }
  }

  public override double \u0023\u003Dz4wEfDhMr\u0024V6c() => 0.0;

  protected override Point \u0023\u003DzY0xm0mTfvYAx(float _param1, float _param2)
  {
    return new Point((double) _param2, (double) _param1);
  }

  public override \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzjuB\u0024Pa8\u003D(
    IComparable _param1)
  {
    \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D uaaCmXpj7JdmPvp0w = base.\u0023\u003DzjuB\u0024Pa8\u003D(_param1);
    uaaCmXpj7JdmPvp0w.AxisAlignment = AxisAlignment.Top;
    uaaCmXpj7JdmPvp0w.IsHorizontal = true;
    return uaaCmXpj7JdmPvp0w;
  }

  private void \u0023\u003Dz0uUIXFWQeRl24snRJn9H6i0\u003D(
    object _param1,
    SizeChangedEventArgs _param2)
  {
    PolarPanel.SetThickness((UIElement) this.\u0023\u003DzMUpWftuaeZ8o(), this.AxisAlignment == AxisAlignment.Top || this.AxisAlignment == AxisAlignment.Bottom ? this.\u0023\u003DzMUpWftuaeZ8o().ActualHeight : this.\u0023\u003DzMUpWftuaeZ8o().ActualWidth);
  }
}
