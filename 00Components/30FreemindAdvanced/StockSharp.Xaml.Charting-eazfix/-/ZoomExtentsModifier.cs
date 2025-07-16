// Decompiled with JetBrains decompiler
// Type: -.ZoomExtentsModifier
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Charting;

public sealed class ZoomExtentsModifier : 
  ChartModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003DzEiZKgbSC39dw = DependencyProperty.Register(nameof (IsAnimated), typeof (bool), typeof (ZoomExtentsModifier), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzcN3lc2NJhvnw = DependencyProperty.Register(nameof (XyDirection), typeof (XyDirection), typeof (ZoomExtentsModifier), new PropertyMetadata((object) XyDirection.XYDirection));
  
  private DateTime \u0023\u003Dz9OUWpvEvl2GW = DateTime.MinValue;
  
  private Point \u0023\u003Dzp3Spc6AS_Moc;
  
  private TimeSpan \u0023\u003DzsYh5VRmqtHM9SOnDFzP8xw8\u003D;

  public ZoomExtentsModifier()
  {
    this.ReceiveHandledEvents = true;
    this.SetCurrentValue(ChartModifierBase.ExecuteOnProperty, (object) ExecuteOn.MouseDoubleClick);
    this.DoubleTapThreshold = TimeSpan.FromMilliseconds(500.0);
  }

  public XyDirection XyDirection
  {
    get
    {
      return (XyDirection) this.GetValue(ZoomExtentsModifier.\u0023\u003DzcN3lc2NJhvnw);
    }
    set
    {
      this.SetValue(ZoomExtentsModifier.\u0023\u003DzcN3lc2NJhvnw, (object) value);
    }
  }

  public bool IsAnimated
  {
    get
    {
      return (bool) this.GetValue(ZoomExtentsModifier.\u0023\u003DzEiZKgbSC39dw);
    }
    set
    {
      this.SetValue(ZoomExtentsModifier.\u0023\u003DzEiZKgbSC39dw, (object) value);
    }
  }

  public override void \u0023\u003Dz5y8F1YNwkhnW(
    ModifierMouseArgs _param1)
  {
    if (this.ExecuteOn != ExecuteOn.MouseDoubleClick)
      return;
    base.\u0023\u003Dz5y8F1YNwkhnW(_param1);
    _param1.Handled(true);
    this.\u0023\u003DzIjNc90j5mMD8();
  }

  public override void OnModifierMouseUp(
    ModifierMouseArgs _param1)
  {
    if (this.ExecuteOn != ExecuteOn.MouseRightButton || _param1.MouseButtons() != (MouseButtons) 4)
      return;
    base.OnModifierMouseUp(_param1);
    _param1.Handled(true);
    this.\u0023\u003DzIjNc90j5mMD8();
  }

  protected virtual void \u0023\u003DzIjNc90j5mMD8()
  {
    if (this.ParentSurface == null)
      return;
    if (this.ParentSurface.get_ChartModifier() != null)
      this.ParentSurface.get_ChartModifier().\u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D();
    TimeSpan timeSpan = this.IsAnimated ? TimeSpan.FromMilliseconds(500.0) : TimeSpan.Zero;
    if (this.XyDirection == XyDirection.XYDirection)
      this.ParentSurface.\u0023\u003Dz5v2P2MZQ6Mg5pHstYQ\u003D\u003D(timeSpan);
    else if (this.XyDirection == XyDirection.YDirection)
      this.ParentSurface.\u0023\u003Dzlt5y\u0024abM\u0024EiJBWUsR3G_Wrc\u003D(timeSpan);
    else
      this.ParentSurface.\u0023\u003Dz8NovIOacEzVlET_SOgsaL_w\u003D(timeSpan);
  }

  public TimeSpan DoubleTapThreshold
  {
    get => this.\u0023\u003DzsYh5VRmqtHM9SOnDFzP8xw8\u003D;
    set => this.\u0023\u003DzsYh5VRmqtHM9SOnDFzP8xw8\u003D = value;
  }

  public override void \u0023\u003Dz0yya794Z8OaI(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    base.\u0023\u003Dz0yya794Z8OaI(_param1);
    if (_param1.\u0023\u003DzeKSkpjwaiSdieql2hyn60Uw\u003D().Count<TouchPoint>() != 1 || this.ParentSurface == null || this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB() == null)
      return;
    TouchPoint touchPoint = _param1.\u0023\u003DzeKSkpjwaiSdieql2hyn60Uw\u003D().Single<TouchPoint>();
    if (touchPoint == null || touchPoint.Action != TouchAction.Down)
      return;
    DateTime now = DateTime.Now;
    Point position = touchPoint.Position;
    DateTime z9OuWpvEvl2Gw = this.\u0023\u003Dz9OUWpvEvl2GW;
    if (!(now - z9OuWpvEvl2Gw < this.DoubleTapThreshold) || \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(position, this.\u0023\u003Dzp3Spc6AS_Moc) >= 10.0 || !this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB().IsPointWithinBounds(position) || this.ExecuteOn != ExecuteOn.MouseDoubleClick)
      return;
    this.\u0023\u003DzIjNc90j5mMD8();
  }

  public override void \u0023\u003DzsSwjrBzrsGPJ(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    base.\u0023\u003DzsSwjrBzrsGPJ(_param1);
    if (_param1.\u0023\u003DzeKSkpjwaiSdieql2hyn60Uw\u003D().Count<TouchPoint>() != 1)
      return;
    this.\u0023\u003Dz9OUWpvEvl2GW = DateTime.Now;
    this.\u0023\u003Dzp3Spc6AS_Moc = _param1.\u0023\u003DzeKSkpjwaiSdieql2hyn60Uw\u003D().Single<TouchPoint>().Position;
  }
}
