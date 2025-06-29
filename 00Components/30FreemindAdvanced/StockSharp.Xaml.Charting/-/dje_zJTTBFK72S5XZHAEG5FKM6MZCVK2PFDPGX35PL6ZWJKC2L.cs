// Decompiled with JetBrains decompiler
// Type: -.dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal abstract class dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd : 
  dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzhcsKvLfw_p5c;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzeDqneUWYjgVB;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzcN3lc2NJhvnw = DependencyProperty.Register(XXX.SSS(-539429583), typeof (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd), typeof (dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd), new PropertyMetadata((object) dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XYDirection));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzxJ9WluMHlZpH = DependencyProperty.Register(XXX.SSS(-539429234), typeof (dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd), typeof (dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd), new PropertyMetadata((object) dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.StretchAtExtents));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzNWOI1oh1IMgh = DependencyProperty.Register(XXX.SSS(-539429784), typeof (bool), typeof (dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd), new PropertyMetadata((object) true));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzFKYv1dU45rO8JWT1sg\u003D\u003D;

  protected dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd()
  {
    this.SetCurrentValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzdfZ5r82v29C_, (object) dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseLeftButton);
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

  public dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd ClipModeX
  {
    get
    {
      return (dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd) this.GetValue(dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd.\u0023\u003DzxJ9WluMHlZpH);
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
    if (this.IsDragging || !this.\u0023\u003DzK46Xo3q3PoYX(_param1.\u0023\u003DzwuSh61ofE2mr(), this.ExecuteOn) || this.XAxes.\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>() || !_param1.\u0023\u003DzCJb5Ya_8UZCR() || !this.ModifierSurface.GetBoundsRelativeTo((\u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z) this.\u0023\u003Dzwc4Gzka23TGB()).Contains(_param1.\u0023\u003DztkyOk5amPcz3()))
      return;
    Point point1 = _param1.\u0023\u003DztkyOk5amPcz3();
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    this.\u0023\u003DzNqFH9\u00244\u003D(Cursors.Hand);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D ao0kEjY6wcnQ6fBfXg = \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
    string str = XXX.SSS(-539428657);
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
    ao0kEjY6wcnQ6fBfXg.\u0023\u003Dz3jAE7bQ\u003D(str, objArray);
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
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(XXX.SSS(-539428725), new object[3]
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
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(XXX.SSS(-539428695), new object[3]
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
