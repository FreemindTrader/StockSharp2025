// Decompiled with JetBrains decompiler
// Type: #=z03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD$SAQu$H2DU4Co=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
public sealed class \u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D : 
  dje_zRZN2N3AMLJBXJD5QUJNGUET4WSTZAVXWDYQQFKDCKHYXDHP8L7XC4_ejd
{
  
  public static readonly DependencyProperty \u0023\u003Dzp3T0E8s\u003D = DependencyProperty.Register(nameof (ActionType), typeof (dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd), typeof (\u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D), new PropertyMetadata((object) dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd.Pan, new PropertyChangedCallback(\u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003Dz80RJIH_dDgLn7Di7fNcKxH8\u003D)));
  
  private Action<Point, double> \u0023\u003DzE5Q0sUQO_NoQ;

  public \u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D()
  {
    this.GrowFactor = 0.05;
    this.\u0023\u003DzE5Q0sUQO_NoQ = new Action<Point, double>(this.\u0023\u003Dz6fc78SIV6E\u0024a);
  }

  public dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd ActionType
  {
    get
    {
      return (dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd) this.GetValue(\u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D.\u0023\u003Dzp3T0E8s\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D.\u0023\u003Dzp3T0E8s\u003D, (object) value);
    }
  }

  private void \u0023\u003DzIjNc90j5mMD8(Point _param1, double _param2)
  {
    this.\u0023\u003DzIjNc90j5mMD8(_param1, _param2, _param2);
  }

  public override void \u0023\u003DzQTINWhMByBmJ(
    ModifierMouseArgs _param1)
  {
    base.\u0023\u003DzQTINWhMByBmJ(_param1);
    using (this.ParentSurface.SuspendUpdates())
    {
      double num = (double) -_param1.\u0023\u003DzDuDuL4DDV5GL() / 120.0;
      XyDirection xyDirection = this.XyDirection;
      dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd actionType = this.ActionType;
      switch (_param1.Modifier())
      {
        case MouseModifier.None:
          this.ActionType = dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd.Pan;
          this.XyDirection = XyDirection.YDirection;
          break;
        case MouseModifier.Shift:
          return;
        case MouseModifier.Ctrl:
          this.ActionType = dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd.Zoom;
          break;
        case MouseModifier.Shift | MouseModifier.Ctrl:
          return;
        case MouseModifier.Alt:
          this.ActionType = dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd.Pan;
          this.XyDirection = XyDirection.XDirection;
          break;
        default:
          return;
      }
      _param1.Handled(true);
      this.\u0023\u003DzE5Q0sUQO_NoQ(this.GetPointRelativeTo(_param1.MousePoint(), (IHitTestable) this.ModifierSurface), num);
      this.XyDirection = xyDirection;
      this.ActionType = actionType;
    }
  }

  private void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, double _param2)
  {
    if (this.XyDirection == XyDirection.YDirection || this.XyDirection == XyDirection.XYDirection)
    {
      foreach (IAxis yax in this.YAxes)
      {
        double num1 = yax.IsHorizontalAxis ? yax.Width : yax.Height;
        double num2 = _param2 * this.GrowFactor * num1;
        yax.\u0023\u003DzquLnA5Y\u003D(num2, ClipMode.None);
      }
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Growing YRange: {0}", new object[1]
      {
        (object) _param2
      });
    }
    if (this.XyDirection != XyDirection.XDirection && this.XyDirection != XyDirection.XYDirection)
      return;
    foreach (IAxis xax in this.XAxes)
    {
      int num3 = xax.IsHorizontalAxis ? 1 : 0;
      bool? isHorizontalAxis = this.XAxis?.IsHorizontalAxis;
      int num4 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
      if (num3 == num4 & isHorizontalAxis.HasValue)
      {
        double num5 = xax.IsHorizontalAxis ? xax.Width : xax.Height;
        double num6 = -_param2 * this.GrowFactor * num5;
        xax.\u0023\u003DzquLnA5Y\u003D(num6, ClipMode.None);
      }
      else
        break;
    }
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Growing XRange: {0}", new object[1]
    {
      (object) (int) _param2
    });
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D.SomeClass34343383();

    public void \u0023\u003Dz80RJIH_dDgLn7Di7fNcKxH8\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      \u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D wnvQfyDSaQuH2Du4Co = _param1 as \u0023\u003Dz03BSxVLolBnG92GmtCJpdvzaoAKJNUfy2ZvWNvQFyD\u0024SAQu\u0024H2DU4Co\u003D;
      dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd newValue = (dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd) _param2.NewValue;
      if (wnvQfyDSaQuH2Du4Co == null)
        return;
      wnvQfyDSaQuH2Du4Co.\u0023\u003DzE5Q0sUQO_NoQ = newValue == dje_z9BFR77JLJVFHSFBQ8JKKJKYW2MP748H2UZ_ejd.Pan ? new Action<Point, double>(wnvQfyDSaQuH2Du4Co.\u0023\u003Dz6fc78SIV6E\u0024a) : new Action<Point, double>(wnvQfyDSaQuH2Du4Co.\u0023\u003DzIjNc90j5mMD8);
    }
  }
}
