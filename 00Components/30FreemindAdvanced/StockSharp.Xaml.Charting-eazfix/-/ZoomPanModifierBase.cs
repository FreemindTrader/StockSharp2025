// Decompiled with JetBrains decompiler
// Type: -.ZoomPanModifierBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#nullable disable
namespace SciChart.Charting;

internal abstract class ZoomPanModifierBase : 
  ChartModifierBase
{
  
  private Point \u0023\u003DzhcsKvLfw_p5c;
  
  private Point \u0023\u003DzeDqneUWYjgVB;
  
  public static readonly DependencyProperty \u0023\u003DzcN3lc2NJhvnw = DependencyProperty.Register(nameof (XyDirection), typeof (XyDirection), typeof (ZoomPanModifierBase), new PropertyMetadata((object) XyDirection.XYDirection));
  
  public static readonly DependencyProperty \u0023\u003DzxJ9WluMHlZpH = DependencyProperty.Register(nameof (ClipModeX), typeof (ClipMode), typeof (ZoomPanModifierBase), new PropertyMetadata((object) ClipMode.StretchAtExtents));
  
  public static readonly DependencyProperty \u0023\u003DzNWOI1oh1IMgh = DependencyProperty.Register(nameof (ZoomExtentsY), typeof (bool), typeof (ZoomPanModifierBase), new PropertyMetadata((object) true));
  
  private bool \u0023\u003DzFKYv1dU45rO8JWT1sg\u003D\u003D;

  protected ZoomPanModifierBase()
  {
    this.SetCurrentValue(ChartModifierBase.ExecuteOnProperty, (object) ExecuteOn.MouseLeftButton);
    this.\u0023\u003Dz3aV1iPcGyuhxDI4kpQEmSBg\u003D(false);
  }

  public bool ZoomExtentsY
  {
    get
    {
      return (bool) this.GetValue(ZoomPanModifierBase.\u0023\u003DzNWOI1oh1IMgh);
    }
    set
    {
      this.SetValue(ZoomPanModifierBase.\u0023\u003DzNWOI1oh1IMgh, (object) value);
    }
  }

  public XyDirection XyDirection
  {
    get
    {
      return (XyDirection) this.GetValue(ZoomPanModifierBase.\u0023\u003DzcN3lc2NJhvnw);
    }
    set
    {
      this.SetValue(ZoomPanModifierBase.\u0023\u003DzcN3lc2NJhvnw, (object) value);
    }
  }

  public ClipMode ClipModeX
  {
    get
    {
      return (ClipMode) this.GetValue(ZoomPanModifierBase.\u0023\u003DzxJ9WluMHlZpH);
    }
    set
    {
      this.SetValue(ZoomPanModifierBase.\u0023\u003DzxJ9WluMHlZpH, (object) value);
    }
  }

  public bool IsDragging
  {
    get => this.\u0023\u003DzFKYv1dU45rO8JWT1sg\u003D\u003D;
    protected set => this.\u0023\u003DzFKYv1dU45rO8JWT1sg\u003D\u003D = value;
  }

  public override void OnModifierMouseDown(
    ModifierMouseArgs _param1)
  {
    if (this.IsDragging || !this.MatchesExecuteOn(_param1.MouseButtons(), this.ExecuteOn) || this.XAxes.\u0023\u003DzCCMM80zDpO6N<IAxis>() || !_param1.IsMaster() || !this.ModifierSurface.GetBoundsRelativeTo((IHitTestable) this.\u0023\u003Dzwc4Gzka23TGB()).Contains(_param1.MousePoint()))
      return;
    Point point1 = _param1.MousePoint();
    base.OnModifierMouseDown(_param1);
    _param1.Handled(true);
    this.\u0023\u003DzNqFH9\u00244\u003D(Cursors.Hand);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D ao0kEjY6wcnQ6fBfXg = \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
    object[] objArray = new object[3]
    {
      (object) ((object) this).GetType().Name,
      null,
      null
    };
    Point point2 = _param1.MousePoint();
    objArray[1] = (object) point2.X;
    point2 = _param1.MousePoint();
    objArray[2] = (object) point2.Y;
    ao0kEjY6wcnQ6fBfXg.\u0023\u003Dz3jAE7bQ\u003D("{0} MouseDown: x={1}, y={2}", objArray);
    if (_param1.IsMaster())
      this.ModifierSurface.CaptureMouse();
    this.\u0023\u003DzhcsKvLfw_p5c = point1;
    this.\u0023\u003DzeDqneUWYjgVB = this.\u0023\u003DzhcsKvLfw_p5c;
    this.IsDragging = true;
  }

  public override void OnModifierMouseUp(
    ModifierMouseArgs _param1)
  {
    base.OnModifierMouseUp(_param1);
    _param1.Handled(true);
    this.IsDragging = false;
    this.\u0023\u003DzhcsKvLfw_p5c = new Point();
    if (_param1.IsMaster())
      this.ModifierSurface.ReleaseMouseCapture();
    this.\u0023\u003DzNqFH9\u00244\u003D(Cursors.Arrow);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseUp: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.MousePoint().X,
      (object) _param1.MousePoint().Y
    });
  }

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
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
    this.\u0023\u003DzHcrX_TM\u003D(point, this.\u0023\u003DzeDqneUWYjgVB, this.\u0023\u003DzhcsKvLfw_p5c);
    this.\u0023\u003DzeDqneUWYjgVB = point;
  }

  public abstract void \u0023\u003DzHcrX_TM\u003D(Point _param1, Point _param2, Point _param3);
}
