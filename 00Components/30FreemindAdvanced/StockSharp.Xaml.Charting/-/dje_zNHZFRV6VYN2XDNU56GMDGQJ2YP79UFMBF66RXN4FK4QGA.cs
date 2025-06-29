// Decompiled with JetBrains decompiler
// Type: -.dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd : 
  dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzEiZKgbSC39dw = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539429381), typeof (bool), typeof (dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd), new PropertyMetadata((object) true));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzcN3lc2NJhvnw = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539429583), typeof (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd), typeof (dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd), new PropertyMetadata((object) dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XYDirection));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private DateTime \u0023\u003Dz9OUWpvEvl2GW = DateTime.MinValue;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003Dzp3Spc6AS_Moc;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private TimeSpan \u0023\u003DzsYh5VRmqtHM9SOnDFzP8xw8\u003D;

  public dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd()
  {
    this.ReceiveHandledEvents = true;
    this.SetCurrentValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzdfZ5r82v29C_, (object) dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseDoubleClick);
    this.DoubleTapThreshold = TimeSpan.FromMilliseconds(500.0);
  }

  public dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd XyDirection
  {
    get
    {
      return (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd) this.GetValue(dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd.\u0023\u003DzcN3lc2NJhvnw);
    }
    set
    {
      this.SetValue(dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd.\u0023\u003DzcN3lc2NJhvnw, (object) value);
    }
  }

  public bool IsAnimated
  {
    get
    {
      return (bool) this.GetValue(dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd.\u0023\u003DzEiZKgbSC39dw);
    }
    set
    {
      this.SetValue(dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd.\u0023\u003DzEiZKgbSC39dw, (object) value);
    }
  }

  public override void \u0023\u003Dz5y8F1YNwkhnW(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (this.ExecuteOn != dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseDoubleClick)
      return;
    base.\u0023\u003Dz5y8F1YNwkhnW(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    this.\u0023\u003DzIjNc90j5mMD8();
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (this.ExecuteOn != dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseRightButton || _param1.\u0023\u003DzwuSh61ofE2mr() != (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 4)
      return;
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    this.\u0023\u003DzIjNc90j5mMD8();
  }

  protected virtual void \u0023\u003DzIjNc90j5mMD8()
  {
    if (this.ParentSurface == null)
      return;
    if (this.ParentSurface.get_ChartModifier() != null)
      this.ParentSurface.get_ChartModifier().\u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D();
    TimeSpan timeSpan = this.IsAnimated ? TimeSpan.FromMilliseconds(500.0) : TimeSpan.Zero;
    if (this.XyDirection == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XYDirection)
      this.ParentSurface.\u0023\u003Dz5v2P2MZQ6Mg5pHstYQ\u003D\u003D(timeSpan);
    else if (this.XyDirection == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection)
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
    if (!(now - z9OuWpvEvl2Gw < this.DoubleTapThreshold) || \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(position, this.\u0023\u003Dzp3Spc6AS_Moc) >= 10.0 || !this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB().IsPointWithinBounds(position) || this.ExecuteOn != dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseDoubleClick)
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
