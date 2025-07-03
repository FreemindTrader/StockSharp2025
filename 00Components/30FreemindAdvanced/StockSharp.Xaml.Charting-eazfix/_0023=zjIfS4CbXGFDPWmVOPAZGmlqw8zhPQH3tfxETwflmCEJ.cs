// Decompiled with JetBrains decompiler
// Type: #=zjIfS4CbXGFDPWmVOPAZGmlqw8zhPQH3tfxETwflmCEJq30zQcQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows;
using System.Windows.Media.Animation;

#nullable disable
internal sealed class \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmlqw8zhPQH3tfxETwflmCEJq30zQcQ\u003D\u003D
{
  private TimeSpan \u0023\u003DzXOURnwA\u003D;
  private UIElement \u0023\u003DzkP4vjPw\u003D;
  private string \u0023\u003DzMKEjAAU\u003D;
  private double \u0023\u003Dzd4Wxe5w\u003D;
  private double \u0023\u003DzAHNI_S0\u003D;
  private EventHandler \u0023\u003DzZla_cGQ\u003D;

  public \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmlqw8zhPQH3tfxETwflmCEJq30zQcQ\u003D\u003D \u0023\u003DzsAWEByc\u003D(
    UIElement _param1)
  {
    this.\u0023\u003DzkP4vjPw\u003D = _param1;
    return this;
  }

  public \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmlqw8zhPQH3tfxETwflmCEJq30zQcQ\u003D\u003D \u0023\u003DzVaJqVV8\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003Dzd4Wxe5w\u003D = _param1;
    this.\u0023\u003DzAHNI_S0\u003D = _param2;
    return this;
  }

  public \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmlqw8zhPQH3tfxETwflmCEJq30zQcQ\u003D\u003D \u0023\u003Dz29rfIbQ\u003D(
    string _param1)
  {
    this.\u0023\u003DzMKEjAAU\u003D = _param1;
    return this;
  }

  public \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmlqw8zhPQH3tfxETwflmCEJq30zQcQ\u003D\u003D \u0023\u003DzcAWbdKs\u003D(
    TimeSpan _param1)
  {
    this.\u0023\u003DzXOURnwA\u003D = _param1;
    return this;
  }

  public \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmlqw8zhPQH3tfxETwflmCEJq30zQcQ\u003D\u003D \u0023\u003DzENNJss_nigUT(
    EventHandler _param1)
  {
    this.\u0023\u003DzZla_cGQ\u003D = _param1;
    return this;
  }

  public void \u0023\u003Dz0g8_jDM\u003D()
  {
    SplineDoubleKeyFrame splineDoubleKeyFrame1 = new SplineDoubleKeyFrame();
    splineDoubleKeyFrame1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
    splineDoubleKeyFrame1.Value = this.\u0023\u003Dzd4Wxe5w\u003D;
    SplineDoubleKeyFrame splineDoubleKeyFrame2 = splineDoubleKeyFrame1;
    SplineDoubleKeyFrame splineDoubleKeyFrame3 = new SplineDoubleKeyFrame();
    splineDoubleKeyFrame3.KeyTime = KeyTime.FromTimeSpan(this.\u0023\u003DzXOURnwA\u003D);
    splineDoubleKeyFrame3.KeySpline = new KeySpline()
    {
      ControlPoint1 = new Point(0.73, 0.14),
      ControlPoint2 = new Point(0.1, 1.0)
    };
    splineDoubleKeyFrame3.Value = this.\u0023\u003DzAHNI_S0\u003D;
    SplineDoubleKeyFrame splineDoubleKeyFrame4 = splineDoubleKeyFrame3;
    DoubleAnimationUsingKeyFrames element = new DoubleAnimationUsingKeyFrames()
    {
      KeyFrames = {
        (DoubleKeyFrame) splineDoubleKeyFrame2,
        (DoubleKeyFrame) splineDoubleKeyFrame4
      }
    };
    Storyboard.SetTarget((DependencyObject) element, (DependencyObject) this.\u0023\u003DzkP4vjPw\u003D);
    Storyboard.SetTargetProperty((DependencyObject) element, new PropertyPath(this.\u0023\u003DzMKEjAAU\u003D, Array.Empty<object>()));
    if (this.\u0023\u003DzZla_cGQ\u003D != null)
      element.Completed += this.\u0023\u003DzZla_cGQ\u003D;
    Storyboard storyboard = new Storyboard();
    storyboard.Children.Add((Timeline) element);
    storyboard.Begin();
  }
}
