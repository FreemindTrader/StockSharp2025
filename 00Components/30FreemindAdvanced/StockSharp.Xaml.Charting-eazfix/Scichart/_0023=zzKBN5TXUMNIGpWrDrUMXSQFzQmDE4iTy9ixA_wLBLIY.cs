// Decompiled with JetBrains decompiler
// Type: #=zzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#nullable disable
internal abstract class \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ : 
  ChartModifierBase
{
  
  private static readonly DependencyProperty \u0023\u003Dzy4Y1epMcZKTS = DependencyProperty.Register(nameof (AxisId), typeof (string), typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003Dzn0IbA1m7B6Pr)));
  
  public static readonly DependencyProperty \u0023\u003DzhGlyqoMEq_wf = DependencyProperty.Register(nameof (DragMode), typeof (\u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D), typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ), new PropertyMetadata((object) \u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D.Scale));
  
  public static readonly DependencyProperty \u0023\u003DzY3btX2_4ZgpM = DependencyProperty.Register(nameof (MinTouchArea), typeof (double), typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ), new PropertyMetadata((object) 0.0));
  
  private static readonly Cursor \u0023\u003DzFl6tvxA\u003D = Cursors.Arrow;
  
  private bool \u0023\u003DzqGwbHdeZ8yMA;
  
  private Point \u0023\u003DzeDqneUWYjgVB;
  
  private bool \u0023\u003Dz0RQE5sVUyiev;

  protected \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ()
  {
    this.\u0023\u003Dz3aV1iPcGyuhxDI4kpQEmSBg\u003D(false);
  }

  public \u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D DragMode
  {
    get
    {
      return (\u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D) this.GetValue(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzhGlyqoMEq_wf);
    }
    set
    {
      this.SetValue(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzhGlyqoMEq_wf, (object) value);
    }
  }

  public string AxisId
  {
    get
    {
      return (string) this.GetValue(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003Dzy4Y1epMcZKTS);
    }
    set
    {
      this.SetValue(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003Dzy4Y1epMcZKTS, (object) value);
    }
  }

  public double MinTouchArea
  {
    get
    {
      return (double) this.GetValue(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzY3btX2_4ZgpM);
    }
    set
    {
      this.SetValue(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzY3btX2_4ZgpM, (object) value);
    }
  }

  public bool IsDragging
  {
    get => this.\u0023\u003DzqGwbHdeZ8yMA;
    protected set => this.\u0023\u003DzqGwbHdeZ8yMA = value;
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003DzTXojzBH7awRH((Cursor) null);
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this.\u0023\u003DzTXojzBH7awRH(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzFl6tvxA\u003D);
  }

  private void \u0023\u003DzTXojzBH7awRH(Cursor _param1)
  {
    IAxis dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    dynWmoFzgH4RlWB0lB?.\u0023\u003DzqFIyyIbnwGLq(_param1 ?? this.\u0023\u003Dzwc3e5oDhVoYg(dynWmoFzgH4RlWB0lB));
  }

  protected override void OnIsEnabledChanged()
  {
    if (this.IsEnabled)
      return;
    this.\u0023\u003DzTXojzBH7awRH(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzFl6tvxA\u003D);
  }

  protected abstract IAxis \u0023\u003DzFLmJq0JJlr0n();

  protected virtual bool \u0023\u003DzqFBxYEN\u0024frAq(Point _param1, Rect _param2, bool _param3)
  {
    if (_param3)
      _param2.Width /= 2.0;
    else
      _param2.Height /= 2.0;
    return !_param2.Contains(_param1);
  }

  protected virtual Cursor \u0023\u003Dzwc3e5oDhVoYg(
    IAxis _param1)
  {
    Cursor cursor = _param1.IsHorizontalAxis ? Cursors.SizeWE : Cursors.SizeNS;
    return !this.IsEnabled ? \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzFl6tvxA\u003D : cursor;
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    ModifierMouseArgs _param1)
  {
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    IAxis dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    if (this.IsDragging || !this.\u0023\u003DzK46Xo3q3PoYX(_param1.MouseButtons(), this.ExecuteOn) || dynWmoFzgH4RlWB0lB == null)
      return;
    Rect boundsRelativeTo = dynWmoFzgH4RlWB0lB.GetBoundsRelativeTo((IHitTestable) this.\u0023\u003Dzwc4Gzka23TGB());
    if (dynWmoFzgH4RlWB0lB.IsHorizontalAxis && boundsRelativeTo.Height < this.MinTouchArea)
    {
      boundsRelativeTo.Y -= (this.MinTouchArea - boundsRelativeTo.Height) / 2.0;
      boundsRelativeTo.Height = this.MinTouchArea;
    }
    if (!dynWmoFzgH4RlWB0lB.IsHorizontalAxis && boundsRelativeTo.Width < this.MinTouchArea)
    {
      boundsRelativeTo.X -= (this.MinTouchArea - boundsRelativeTo.Width) / 2.0;
      boundsRelativeTo.Width = this.MinTouchArea;
    }
    if (!boundsRelativeTo.Contains(_param1.MousePoint()))
      return;
    this.\u0023\u003Dz0RQE5sVUyiev = this.\u0023\u003DzqFBxYEN\u0024frAq(_param1.MousePoint(), boundsRelativeTo, dynWmoFzgH4RlWB0lB.IsHorizontalAxis);
    if (dynWmoFzgH4RlWB0lB.get_FlipCoordinates())
      this.\u0023\u003Dz0RQE5sVUyiev = !this.\u0023\u003Dz0RQE5sVUyiev;
    this.\u0023\u003DzeDqneUWYjgVB = _param1.MousePoint();
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseDown: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.MousePoint().X,
      (object) _param1.MousePoint().Y
    });
    if (_param1.IsMaster())
      dynWmoFzgH4RlWB0lB.CaptureMouse();
    this.\u0023\u003DzqGwbHdeZ8yMA = true;
    _param1.Handled(true);
  }

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DzTXojzBH7awRH((Cursor) null);
    if (!this.IsDragging)
      return;
    base.OnModifierMouseMove(_param1);
    _param1.Handled(true);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseMove: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.MousePoint().X,
      (object) _param1.MousePoint().Y
    });
    Point point = _param1.MousePoint();
    if (this.DragMode == \u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D.Scale)
      this.\u0023\u003DzKcp02aUNjDpn(point, this.\u0023\u003DzeDqneUWYjgVB, this.\u0023\u003Dz0RQE5sVUyiev);
    else
      this.\u0023\u003Dz6fc78SIV6E\u0024a(point, this.\u0023\u003DzeDqneUWYjgVB);
    this.\u0023\u003DzeDqneUWYjgVB = point;
  }

  public override void OnModifierMouseUp(
    ModifierMouseArgs _param1)
  {
    if (!this.IsDragging)
      return;
    base.OnModifierMouseUp(_param1);
    _param1.Handled(true);
    this.\u0023\u003DzqGwbHdeZ8yMA = false;
    if (_param1.IsMaster())
      this.\u0023\u003DzFLmJq0JJlr0n().ReleaseMouseCapture();
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseUp: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.MousePoint().X,
      (object) _param1.MousePoint().Y
    });
  }

  protected abstract void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, Point _param2);

  protected virtual void \u0023\u003DzKcp02aUNjDpn(Point _param1, Point _param2, bool _param3)
  {
    IAxis dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    IRange abyLt9clZggmJsWhw = this.\u0023\u003DzFQz4aIsJtfEk(_param1, _param2, _param3, dynWmoFzgH4RlWB0lB);
    if (dynWmoFzgH4RlWB0lB.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always)
      ((DependencyObject) dynWmoFzgH4RlWB0lB).SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz3kyPJRWoiKq0, (object) this.\u0023\u003Dz6JLjSbJbacdN(abyLt9clZggmJsWhw, dynWmoFzgH4RlWB0lB));
    else
      dynWmoFzgH4RlWB0lB.VisibleRange = abyLt9clZggmJsWhw;
  }

  protected abstract IRange \u0023\u003DzFQz4aIsJtfEk(
    Point _param1,
    Point _param2,
    bool _param3,
    IAxis _param4);

  protected virtual DoubleRange \u0023\u003Dz6JLjSbJbacdN(
    IRange _param1,
    IAxis _param2)
  {
    DoubleRange klqcJ87Zm8UwE3WEjd1 = _param1.AsDoubleRange();
    double max = klqcJ87Zm8UwE3WEjd1.Max;
    double min = klqcJ87Zm8UwE3WEjd1.Min;
    DoubleRange klqcJ87Zm8UwE3WEjd2 = _param2.VisibleRange.AsDoubleRange();
    IRange<double> hgpwdgoZpprK58gKzo0gQ = _param2.GrowBy ?? (IRange<double>) new DoubleRange(0.0, 0.0);
    double num1 = (klqcJ87Zm8UwE3WEjd2.Min + klqcJ87Zm8UwE3WEjd2.Min * hgpwdgoZpprK58gKzo0gQ.Max + klqcJ87Zm8UwE3WEjd2.Max * hgpwdgoZpprK58gKzo0gQ.Min) / (1.0 + hgpwdgoZpprK58gKzo0gQ.Min + hgpwdgoZpprK58gKzo0gQ.Max);
    double num2 = (klqcJ87Zm8UwE3WEjd2.Max + num1 * hgpwdgoZpprK58gKzo0gQ.Max) / (1.0 + hgpwdgoZpprK58gKzo0gQ.Max);
    double num3 = (max - num2) / (num2 - num1);
    double num4 = num1;
    return new DoubleRange((min - num4) / (-num2 + num1), num3);
  }

  private static void \u0023\u003Dzn0IbA1m7B6Pr(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ de4iTy9ixAWLbliyq = (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ) _param0;
    string oldValue = (string) _param1.OldValue;
    if (!de4iTy9ixAWLbliyq.IsAttached)
      return;
    (de4iTy9ixAWLbliyq is dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd ? de4iTy9ixAWLbliyq.\u0023\u003DzI0EiGDjWkH8S(oldValue) : de4iTy9ixAWLbliyq.\u0023\u003Dz4uoxB8oLWxeL(oldValue))?.\u0023\u003DzqFIyyIbnwGLq(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzFl6tvxA\u003D);
    de4iTy9ixAWLbliyq.\u0023\u003DzTXojzBH7awRH((Cursor) null);
  }
}
