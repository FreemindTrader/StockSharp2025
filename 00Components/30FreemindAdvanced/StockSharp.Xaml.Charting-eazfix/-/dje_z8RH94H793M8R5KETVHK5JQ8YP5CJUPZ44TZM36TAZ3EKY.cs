// Decompiled with JetBrains decompiler
// Type: -.dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

#nullable disable
namespace \u002D;

internal sealed class dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd : Control
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzqGwbHdeZ8yMA;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzMznVIhtVfrf4;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzEJSs\u0024dHmxP7mLneAlg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private DragDeltaEventHandler \u0023\u003Dz3ZgR3o1uqquxhzhHEQ\u003D\u003D;

  public dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd);
  }

  public void \u0023\u003DzudBnU1DPUfSSp\u00245wCA\u003D\u003D(DragDeltaEventHandler _param1)
  {
    DragDeltaEventHandler deltaEventHandler = this.\u0023\u003Dz3ZgR3o1uqquxhzhHEQ\u003D\u003D;
    DragDeltaEventHandler comparand;
    do
    {
      comparand = deltaEventHandler;
      deltaEventHandler = Interlocked.CompareExchange<DragDeltaEventHandler>(ref this.\u0023\u003Dz3ZgR3o1uqquxhzhHEQ\u003D\u003D, comparand + _param1, comparand);
    }
    while (deltaEventHandler != comparand);
  }

  public void \u0023\u003DzBCbwyke1cPkjZ1Hl1g\u003D\u003D(DragDeltaEventHandler _param1)
  {
    DragDeltaEventHandler deltaEventHandler = this.\u0023\u003Dz3ZgR3o1uqquxhzhHEQ\u003D\u003D;
    DragDeltaEventHandler comparand;
    do
    {
      comparand = deltaEventHandler;
      deltaEventHandler = Interlocked.CompareExchange<DragDeltaEventHandler>(ref this.\u0023\u003Dz3ZgR3o1uqquxhzhHEQ\u003D\u003D, comparand - _param1, comparand);
    }
    while (deltaEventHandler != comparand);
  }

  protected override void OnMouseLeftButtonDown(MouseButtonEventArgs _param1)
  {
    base.OnMouseLeftButtonDown(_param1);
    if (this.\u0023\u003DzqGwbHdeZ8yMA)
      return;
    this.\u0023\u003DzqGwbHdeZ8yMA = true;
    _param1.Handled = true;
    this.\u0023\u003DzMznVIhtVfrf4 = _param1.GetPosition((IInputElement) this);
    this.CaptureMouse();
  }

  protected override void OnMouseMove(MouseEventArgs _param1)
  {
    base.OnMouseMove(_param1);
    if (!this.\u0023\u003DzqGwbHdeZ8yMA)
      return;
    Point position = _param1.GetPosition((IInputElement) this);
    if (!Point.op_Inequality(position, this.\u0023\u003DzEJSs\u0024dHmxP7mLneAlg\u003D\u003D))
      return;
    this.\u0023\u003DzEJSs\u0024dHmxP7mLneAlg\u003D\u003D = position;
    this.\u0023\u003DzzLRAcqYaHVI4hor3iw\u003D\u003D(position.X - this.\u0023\u003DzMznVIhtVfrf4.X, position.Y - this.\u0023\u003DzMznVIhtVfrf4.Y);
  }

  protected override void OnMouseLeftButtonUp(MouseButtonEventArgs _param1)
  {
    base.OnMouseLeftButtonUp(_param1);
    this.\u0023\u003DzqGwbHdeZ8yMA = false;
    this.ReleaseMouseCapture();
    this.\u0023\u003DzMznVIhtVfrf4.X = 0.0;
    this.\u0023\u003DzMznVIhtVfrf4.Y = 0.0;
  }

  protected virtual void \u0023\u003DzzLRAcqYaHVI4hor3iw\u003D\u003D(double _param1, double _param2)
  {
    if (this.\u0023\u003Dz3ZgR3o1uqquxhzhHEQ\u003D\u003D == null)
      return;
    this.\u0023\u003Dz3ZgR3o1uqquxhzhHEQ\u003D\u003D((object) this, new DragDeltaEventArgs(_param1, _param2));
  }
}
