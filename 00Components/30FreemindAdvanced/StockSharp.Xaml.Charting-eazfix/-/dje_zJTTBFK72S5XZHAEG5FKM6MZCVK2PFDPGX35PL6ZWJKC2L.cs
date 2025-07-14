// Decompiled with JetBrains decompiler
// Type: -.dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#nullable disable
namespace \u002D;

internal abstract class dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd : 
  ChartModifierBase
{
  
  private Point \u0023\u003DzhcsKvLfw_p5c;
  
  private Point \u0023\u003DzeDqneUWYjgVB;
  
  public static readonly DependencyProperty \u0023\u003DzcN3lc2NJhvnw = DependencyProperty.Register(nameof (XyDirection), typeof (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd), typeof (dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd), new PropertyMetadata((object) dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XYDirection));
  
  public static readonly DependencyProperty \u0023\u003DzxJ9WluMHlZpH = DependencyProperty.Register(nameof (ClipModeX), typeof (ClipMode), typeof (dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd), new PropertyMetadata((object) ClipMode.StretchAtExtents));
  
  public static readonly DependencyProperty \u0023\u003DzNWOI1oh1IMgh = DependencyProperty.Register(nameof (ZoomExtentsY), typeof (bool), typeof (dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd), new PropertyMetadata((object) true));
  
  private bool \u0023\u003DzFKYv1dU45rO8JWT1sg\u003D\u003D;

  protected dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd()
  {
    this.SetCurrentValue(ChartModifierBase.\u0023\u003DzdfZ5r82v29C_, (object) ExecuteOn.MouseLeftButton);
    this.\u0023\u003Dz3aV1iPcGyuhxDI4kpQEmSBg\u003D(false);
  }

  public bool ZoomExtentsY
  {
    get
    {
      return (bool) this.GetValue(dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd.\u0023\u003DzNWOI1oh1IMgh);
    }
    set
    {
      this.SetValue(dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd.\u0023\u003DzNWOI1oh1IMgh, (object) value);
    }
  }

  public dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd XyDirection
  {
    get
    {
      return (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd) this.GetValue(dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd.\u0023\u003DzcN3lc2NJhvnw);
    }
    set
    {
      this.SetValue(dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd.\u0023\u003DzcN3lc2NJhvnw, (object) value);
    }
  }

  public ClipMode ClipModeX
  {
    get
    {
      return (ClipMode) this.GetValue(dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd.\u0023\u003DzxJ9WluMHlZpH);
    }
    set
    {
      this.SetValue(dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd.\u0023\u003DzxJ9WluMHlZpH, (object) value);
    }
  }

  public bool IsDragging
  {
    get => this.\u0023\u003DzFKYv1dU45rO8JWT1sg\u003D\u003D;
    protected set => this.\u0023\u003DzFKYv1dU45rO8JWT1sg\u003D\u003D = value;
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (this.IsDragging || !this.\u0023\u003DzK46Xo3q3PoYX(_param1.\u0023\u003DzwuSh61ofE2mr(), this.ExecuteOn) || this.XAxes.\u0023\u003DzCCMM80zDpO6N<IAxis>() || !_param1.\u0023\u003DzCJb5Ya_8UZCR() || !this.ModifierSurface.GetBoundsRelativeTo((IHitTestable) this.\u0023\u003Dzwc4Gzka23TGB()).Contains(_param1.\u0023\u003DztkyOk5amPcz3()))
      return;
    Point point1 = _param1.\u0023\u003DztkyOk5amPcz3();
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    this.\u0023\u003DzNqFH9\u00244\u003D(Cursors.Hand);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D ao0kEjY6wcnQ6fBfXg = \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
    object[] objArray = new object[3]
    {
      (object) ((object) this).GetType().Name,
      null,
      null
    };
    Point point2 = _param1.\u0023\u003DztkyOk5amPcz3();
    objArray[1] = (object) point2.X;
    point2 = _param1.\u0023\u003DztkyOk5amPcz3();
    objArray[2] = (object) point2.Y;
    ao0kEjY6wcnQ6fBfXg.\u0023\u003Dz3jAE7bQ\u003D("{0} MouseDown: x={1}, y={2}", objArray);
    if (_param1.\u0023\u003DzCJb5Ya_8UZCR())
      this.ModifierSurface.CaptureMouse();
    this.\u0023\u003DzhcsKvLfw_p5c = point1;
    this.\u0023\u003DzeDqneUWYjgVB = this.\u0023\u003DzhcsKvLfw_p5c;
    this.IsDragging = true;
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    this.IsDragging = false;
    this.\u0023\u003DzhcsKvLfw_p5c = new Point();
    if (_param1.\u0023\u003DzCJb5Ya_8UZCR())
      this.ModifierSurface.ReleaseMouseCapture();
    this.\u0023\u003DzNqFH9\u00244\u003D(Cursors.Arrow);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseUp: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().X,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().Y
    });
  }

  public override void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (!this.IsDragging)
      return;
    base.\u0023\u003Dz11bcnbUrALaA(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseMove: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().X,
      (object) _param1.\u0023\u003DztkyOk5amPcz3().Y
    });
    Point point = _param1.\u0023\u003DztkyOk5amPcz3();
    this.\u0023\u003DzHcrX_TM\u003D(point, this.\u0023\u003DzeDqneUWYjgVB, this.\u0023\u003DzhcsKvLfw_p5c);
    this.\u0023\u003DzeDqneUWYjgVB = point;
  }

  public abstract void \u0023\u003DzHcrX_TM\u003D(Point _param1, Point _param2, Point _param3);
}
