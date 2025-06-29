// Decompiled with JetBrains decompiler
// Type: #=zzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#nullable disable
internal abstract class \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ : 
  dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly DependencyProperty \u0023\u003Dzy4Y1epMcZKTS = DependencyProperty.Register(XXX.SSS(-539428834), typeof (string), typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ), new PropertyMetadata((object) XXX.SSS(-539431487), new PropertyChangedCallback(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003Dzn0IbA1m7B6Pr)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzhGlyqoMEq_wf = DependencyProperty.Register(XXX.SSS(-539428627), typeof (\u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D), typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ), new PropertyMetadata((object) \u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D.Scale));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzY3btX2_4ZgpM = DependencyProperty.Register(XXX.SSS(-539428638), typeof (double), typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ), new PropertyMetadata((object) 0.0));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly Cursor \u0023\u003DzFl6tvxA\u003D = Cursors.Arrow;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzqGwbHdeZ8yMA;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzeDqneUWYjgVB;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    dynWmoFzgH4RlWB0lB?.\u0023\u003DzqFIyyIbnwGLq(_param1 ?? this.\u0023\u003Dzwc3e5oDhVoYg(dynWmoFzgH4RlWB0lB));
  }

  protected override void \u0023\u003DzCM2UQyuakisf()
  {
    if (this.IsEnabled)
      return;
    this.\u0023\u003DzTXojzBH7awRH(\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzFl6tvxA\u003D);
  }

  protected abstract \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003DzFLmJq0JJlr0n();

  protected virtual bool \u0023\u003DzqFBxYEN\u0024frAq(Point _param1, Rect _param2, bool _param3)
  {
    if (_param3)
      _param2.Width /= 2.0;
    else
      _param2.Height /= 2.0;
    return !_param2.Contains(_param1);
  }

  protected virtual Cursor \u0023\u003Dzwc3e5oDhVoYg(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    Cursor cursor = _param1.IsHorizontalAxis ? Cursors.SizeWE : Cursors.SizeNS;
    return !this.IsEnabled ? \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ.\u0023\u003DzFl6tvxA\u003D : cursor;
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    if (this.IsDragging || !this.\u0023\u003DzK46Xo3q3PoYX(_param1.\u0023\u003DzwuSh61ofE2mr(), this.ExecuteOn) || dynWmoFzgH4RlWB0lB == null)
      return;
    Rect boundsRelativeTo = dynWmoFzgH4RlWB0lB.GetBoundsRelativeTo((\u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z) this.\u0023\u003Dzwc4Gzka23TGB());
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
    if (!boundsRelativeTo.Contains(_param1.\u0023\u003DztkyOk5amPcz3()))
      return;
    this.\u0023\u003Dz0RQE5sVUyiev = this.\u0023\u003DzqFBxYEN\u0024frAq(_param1.\u0023\u003DztkyOk5amPcz3(), boundsRelativeTo, dynWmoFzgH4RlWB0lB.IsHorizontalAxis);
    if (dynWmoFzgH4RlWB0lB.get_FlipCoordinates())
      this.\u0023\u003Dz0RQE5sVUyiev = !this.\u0023\u003Dz0RQE5sVUyiev;
    this.\u0023\u003DzeDqneUWYjgVB = _param1.\u0023\u003DztkyOk5amPcz3();
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(XXX.SSS(-539428657), new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().X,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().Y
    });
    if (_param1.\u0023\u003DzCJb5Ya_8UZCR())
      dynWmoFzgH4RlWB0lB.CaptureMouse();
    this.\u0023\u003DzqGwbHdeZ8yMA = true;
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
  }

  public override void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DzTXojzBH7awRH((Cursor) null);
    if (!this.IsDragging)
      return;
    base.\u0023\u003Dz11bcnbUrALaA(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(XXX.SSS(-539428695), new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().X,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().Y
    });
    Point point = _param1.\u0023\u003DztkyOk5amPcz3();
    if (this.DragMode == \u0023\u003DzuxHg1RShgoNoz91lIJvsfKlLGFv49zadlg\u003D\u003D.Scale)
      this.\u0023\u003DzKcp02aUNjDpn(point, this.\u0023\u003DzeDqneUWYjgVB, this.\u0023\u003Dz0RQE5sVUyiev);
    else
      this.\u0023\u003Dz6fc78SIV6E\u0024a(point, this.\u0023\u003DzeDqneUWYjgVB);
    this.\u0023\u003DzeDqneUWYjgVB = point;
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (!this.IsDragging)
      return;
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    this.\u0023\u003DzqGwbHdeZ8yMA = false;
    if (_param1.\u0023\u003DzCJb5Ya_8UZCR())
      this.\u0023\u003DzFLmJq0JJlr0n().ReleaseMouseCapture();
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(XXX.SSS(-539428725), new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().X,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().Y
    });
  }

  protected abstract void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, Point _param2);

  protected virtual void \u0023\u003DzKcp02aUNjDpn(Point _param1, Point _param2, bool _param3)
  {
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = this.\u0023\u003DzFQz4aIsJtfEk(_param1, _param2, _param3, dynWmoFzgH4RlWB0lB);
    if (dynWmoFzgH4RlWB0lB.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always)
      ((DependencyObject) dynWmoFzgH4RlWB0lB).SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz3kyPJRWoiKq0, (object) this.\u0023\u003Dz6JLjSbJbacdN(abyLt9clZggmJsWhw, dynWmoFzgH4RlWB0lB));
    else
      dynWmoFzgH4RlWB0lB.VisibleRange = abyLt9clZggmJsWhw;
  }

  protected abstract \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzFQz4aIsJtfEk(
    Point _param1,
    Point _param2,
    bool _param3,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param4);

  protected virtual dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003Dz6JLjSbJbacdN(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param2)
  {
    dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd klqcJ87Zm8UwE3WEjd1 = _param1.\u0023\u003DzfODy_Nxn8OGy();
    double max = klqcJ87Zm8UwE3WEjd1.Max;
    double min = klqcJ87Zm8UwE3WEjd1.Min;
    dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd klqcJ87Zm8UwE3WEjd2 = _param2.VisibleRange.\u0023\u003DzfODy_Nxn8OGy();
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double> hgpwdgoZpprK58gKzo0gQ = _param2.get_GrowBy() ?? (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double>) new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(0.0, 0.0);
    double num1 = (klqcJ87Zm8UwE3WEjd2.Min + klqcJ87Zm8UwE3WEjd2.Min * hgpwdgoZpprK58gKzo0gQ.Max + klqcJ87Zm8UwE3WEjd2.Max * hgpwdgoZpprK58gKzo0gQ.Min) / (1.0 + hgpwdgoZpprK58gKzo0gQ.Min + hgpwdgoZpprK58gKzo0gQ.Max);
    double num2 = (klqcJ87Zm8UwE3WEjd2.Max + num1 * hgpwdgoZpprK58gKzo0gQ.Max) / (1.0 + hgpwdgoZpprK58gKzo0gQ.Max);
    double num3 = (max - num2) / (num2 - num1);
    double num4 = num1;
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd((min - num4) / (-num2 + num1), num3);
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
