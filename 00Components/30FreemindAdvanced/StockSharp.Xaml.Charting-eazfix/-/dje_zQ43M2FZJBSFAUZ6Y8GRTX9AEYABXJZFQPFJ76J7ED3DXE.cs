// Decompiled with JetBrains decompiler
// Type: -.dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

#nullable disable
namespace \u002D;

internal class dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd : 
  Panel,
  INotifyPropertyChanged,
  \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P7\u00243UKiUGVR88DR8huM\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz\u0024geG9XF9qNM9 = DependencyProperty.Register(nameof (DrawLabels), typeof (bool), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) true));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzHxEy7A8kQeb2 = DependencyProperty.Register(nameof (DrawMinorTicks), typeof (bool), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) true));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz6E17UGyH3Hxe = DependencyProperty.Register(nameof (DrawMajorTicks), typeof (bool), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) true));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D = DependencyProperty.Register(nameof (MajorTickLineStyle), typeof (Style), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzFgrLJbyJ32zlIW9T7fzjuLs\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D = DependencyProperty.Register(nameof (MinorTickLineStyle), typeof (Style), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzF9xGIyZlaqWjOY1DXnVnofg\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzfMY988N0StOA = DependencyProperty.Register(nameof (AxisAlignment), typeof (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Default, new PropertyChangedCallback(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzOPvUPixjU\u00244Y)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzQcEu5YLH6ffYkobAOQ\u003D\u003D = DependencyProperty.Register(nameof (AxisLabelToTickIndent), typeof (double), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) 2.0, new PropertyChangedCallback(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz8hrSVP3SyudTZXJD6w\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D = DependencyProperty.Register(nameof (IsLabelCullingEnabled), typeof (bool), typeof (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd), new PropertyMetadata((object) true));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Line \u0023\u003DzeEl93ifUiK4P = new Line();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Grid \u0023\u003DzDLF9sbNKzh9k;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Image \u0023\u003DzlbfaVnpq6N5Y;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd \u0023\u003DzNdw5dH7x9p9X;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private WriteableBitmap \u0023\u003DzWfNK9E4egtoR;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dzn_2sUEoYYgva;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzC4ZArHFLRr_I;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzORNmLYbynHFH;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd> \u0023\u003DzLumisCMIlbJuLqsFBA\u003D\u003D;

  public event PropertyChangedEventHandler PropertyChanged;

  public bool IsLabelCullingEnabled
  {
    get
    {
      return (bool) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D, (object) value);
    }
  }

  public dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd AxisAlignment
  {
    get
    {
      return (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzfMY988N0StOA);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzfMY988N0StOA, (object) value);
    }
  }

  public Style MajorTickLineStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D, (object) value);
    }
  }

  public Style MinorTickLineStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D, (object) value);
    }
  }

  public bool DrawMajorTicks
  {
    get
    {
      return (bool) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz6E17UGyH3Hxe);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz6E17UGyH3Hxe, (object) value);
    }
  }

  public bool DrawMinorTicks
  {
    get
    {
      return (bool) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzHxEy7A8kQeb2);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzHxEy7A8kQeb2, (object) value);
    }
  }

  public bool DrawLabels
  {
    get
    {
      return (bool) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz\u0024geG9XF9qNM9);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz\u0024geG9XF9qNM9, (object) value);
    }
  }

  public double AxisLabelToTickIndent
  {
    get
    {
      return (double) this.GetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzQcEu5YLH6ffYkobAOQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003DzQcEu5YLH6ffYkobAOQ\u003D\u003D, (object) value);
    }
  }

  public bool IsHorizontalAxis
  {
    get
    {
      dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd axisAlignment = this.AxisAlignment;
      return axisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom || axisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top;
    }
  }

  public Thickness LabelToTickIndent
  {
    get
    {
      return new Thickness(this.AxisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top ? this.AxisLabelToTickIndent : 0.0);
    }
  }

  public double MajorTickSize
  {
    get => !this.DrawMajorTicks ? 0.0 : this.\u0023\u003DzC4ZArHFLRr_I;
    private set
    {
      if (value.Equals(this.\u0023\u003DzC4ZArHFLRr_I))
        return;
      this.\u0023\u003DzC4ZArHFLRr_I = value;
      this.\u0023\u003Dz15moWio\u003D(nameof (MajorTickSize));
    }
  }

  public double MinorTickSize
  {
    get => !this.DrawMinorTicks ? 0.0 : this.\u0023\u003Dzn_2sUEoYYgva;
    private set
    {
      if (value.Equals(this.\u0023\u003Dzn_2sUEoYYgva))
        return;
      this.\u0023\u003Dzn_2sUEoYYgva = value;
      this.\u0023\u003Dz15moWio\u003D(nameof (MinorTickSize));
    }
  }

  public Action<dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd> AddLabels
  {
    get => this.\u0023\u003DzLumisCMIlbJuLqsFBA\u003D\u003D;
    set => this.\u0023\u003DzLumisCMIlbJuLqsFBA\u003D\u003D = value;
  }

  protected override Size MeasureOverride(Size _param1)
  {
    if (!this.\u0023\u003DzORNmLYbynHFH)
    {
      foreach (UIElement child in this.Children)
        this.\u0023\u003DzFeNr2Uw\u003D(child);
    }
    this.\u0023\u003DzypPX\u0024di_k9nd(this.AddLabels);
    this.\u0023\u003DzDLF9sbNKzh9k.Measure(_param1);
    double num1 = Math.Max(this.MinorTickSize, this.MajorTickSize);
    Size size;
    switch (this.AxisAlignment)
    {
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right:
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left:
        this.\u0023\u003DzlbfaVnpq6N5Y.Measure(new Size(num1, _param1.Height));
        double num2 = num1 + this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width;
        this.\u0023\u003DzNdw5dH7x9p9X.Measure(new Size(_param1.Width - num2, _param1.Height));
        size = new Size(num2 + this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height);
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top:
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom:
        this.\u0023\u003DzlbfaVnpq6N5Y.Measure(new Size(_param1.Width, num1));
        double num3 = num1 + this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height;
        this.\u0023\u003DzNdw5dH7x9p9X.Measure(new Size(_param1.Width, _param1.Height - num3));
        size = new Size(this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, num3 + this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Height);
        break;
      default:
        size = new Size(this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height);
        break;
    }
    return size;
  }

  private void \u0023\u003DzFeNr2Uw\u003D(UIElement _param1)
  {
    if (_param1 is Image)
      this.\u0023\u003DzlbfaVnpq6N5Y = (Image) _param1;
    if (_param1 is dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd)
      this.\u0023\u003DzNdw5dH7x9p9X = (dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd) _param1;
    if (_param1 is Grid)
      this.\u0023\u003DzDLF9sbNKzh9k = (Grid) _param1;
    this.\u0023\u003DzORNmLYbynHFH = this.\u0023\u003DzlbfaVnpq6N5Y != null && this.\u0023\u003DzNdw5dH7x9p9X != null && this.\u0023\u003DzDLF9sbNKzh9k != null;
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    double num1 = Math.Max(this.MinorTickSize, this.MajorTickSize);
    switch (this.AxisAlignment)
    {
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right:
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(0.0, 0.0, num1, _param1.Height));
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(num1, 0.0, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, _param1.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(num1 + this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, 0.0, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, _param1.Height));
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left:
        double num2 = _param1.Width - num1;
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(num2, 0.0, num1, _param1.Height));
        double num3 = num2 - this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width;
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(num3, 0.0, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, _param1.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(num3 - this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, 0.0, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, _param1.Height));
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top:
        double num4 = _param1.Height - num1;
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(0.0, num4, _param1.Width, num1));
        double num5 = num4 - this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height;
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(0.0, num5, _param1.Width, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(0.0, num5 - this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Height, _param1.Width, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Height));
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom:
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(0.0, 0.0, _param1.Width, num1));
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(0.0, num1, _param1.Width, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(0.0, num1 + this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height, _param1.Width, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Height));
        break;
    }
    return _param1;
  }

  public void \u0023\u003DzypPX\u0024di_k9nd(
    Action<dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd> _param1)
  {
    if (this.\u0023\u003DzDLF9sbNKzh9k == null || !this.DrawLabels)
      return;
    dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd quecZ77TxG2K5CaEjd1 = this.\u0023\u003DzDLF9sbNKzh9k.Children[0].\u0023\u003DzST\u0024t7rI\u003D() ? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd) this.\u0023\u003DzDLF9sbNKzh9k.Children[1] : (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd) this.\u0023\u003DzDLF9sbNKzh9k.Children[0];
    dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd quecZ77TxG2K5CaEjd2 = this.\u0023\u003DzDLF9sbNKzh9k.Children[0].\u0023\u003DzST\u0024t7rI\u003D() ? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd) this.\u0023\u003DzDLF9sbNKzh9k.Children[0] : (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd) this.\u0023\u003DzDLF9sbNKzh9k.Children[1];
    quecZ77TxG2K5CaEjd1.Visibility = Visibility.Collapsed;
    quecZ77TxG2K5CaEjd1.SizeHeightToContent = this.IsHorizontalAxis;
    quecZ77TxG2K5CaEjd1.SizeWidthToContent = !this.IsHorizontalAxis;
    _param1(quecZ77TxG2K5CaEjd1);
    quecZ77TxG2K5CaEjd1.Visibility = Visibility.Visible;
    quecZ77TxG2K5CaEjd2.Visibility = Visibility.Collapsed;
  }

  public void \u0023\u003DzEy\u0024V_bY\u003D()
  {
    this.InvalidateMeasure();
    this.InvalidateArrange();
  }

  public virtual void \u0023\u003DzuTwCwl07R0Mf(
    \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok _param1,
    float _param2)
  {
    Size size = this.\u0023\u003DzR5BsR9jdiJQz();
    int width = (int) size.Width;
    int height = (int) size.Height;
    if (this.\u0023\u003DzWfNK9E4egtoR == null || this.\u0023\u003DzWfNK9E4egtoR.PixelWidth != width || this.\u0023\u003DzWfNK9E4egtoR.PixelHeight != height)
      this.\u0023\u003DzWfNK9E4egtoR = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.GetMath(width, height);
    if (this.\u0023\u003DzWfNK9E4egtoR == null || this.\u0023\u003DzlbfaVnpq6N5Y == null)
      return;
    using (\u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H blRxkxWuDZv6cV0H = this.\u0023\u003DzWfNK9E4egtoR.\u0023\u003Dz1cRMfLZU4Eo2(this.\u0023\u003DzlbfaVnpq6N5Y, (\u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D) null))
    {
      blRxkxWuDZv6cV0H.\u0023\u003DzUf222sU\u003D();
      if (this.DrawMinorTicks && _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D() != null)
        this.\u0023\u003DzuTwCwl07R0Mf((IRenderContext2D) blRxkxWuDZv6cV0H, this.MinorTickLineStyle, this.MinorTickSize, _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D(), _param2);
      if (!this.DrawMajorTicks || _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D() == null)
        return;
      this.\u0023\u003DzuTwCwl07R0Mf((IRenderContext2D) blRxkxWuDZv6cV0H, this.MajorTickLineStyle, this.MajorTickSize, _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D(), _param2);
    }
  }

  protected virtual Size \u0023\u003DzR5BsR9jdiJQz()
  {
    return new Size(this.IsHorizontalAxis ? (double) (int) this.\u0023\u003DzDLF9sbNKzh9k.ActualWidth : (double) (int) this.MajorTickSize, this.IsHorizontalAxis ? (double) (int) this.MajorTickSize : (double) (int) this.\u0023\u003DzDLF9sbNKzh9k.ActualHeight);
  }

  protected virtual void \u0023\u003DzuTwCwl07R0Mf(
    IRenderContext2D _param1,
    Style _param2,
    double _param3,
    float[] _param4,
    float _param5)
  {
    this.\u0023\u003DzeEl93ifUiK4P.Style = _param2;
    dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.SetTheme((DependencyObject) this.\u0023\u003DzeEl93ifUiK4P, dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.GetTheme((DependencyObject) this));
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzQCf7bpfi0DqGMauSow\u003D\u003D(this.\u0023\u003DzeEl93ifUiK4P, false))
    {
      foreach (float num in _param4)
        this.\u0023\u003Dz3ZXzqAn7QFK6(_param1, rhwYsZxA33iRu6Id7J, num, _param5, _param3);
    }
  }

  private void \u0023\u003Dz3ZXzqAn7QFK6(
    IRenderContext2D _param1,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param2,
    float _param3,
    float _param4,
    double _param5)
  {
    Size size = _param1.\u0023\u003Dz8DEW4l1E337F();
    float num1 = this.IsHorizontalAxis ? (float) size.Height : (float) size.Width;
    float num2;
    float num3 = num2 = _param3 - _param4;
    float num4 = 0.0f;
    float num5 = (float) _param5;
    if (this.AxisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top || this.AxisAlignment == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left)
    {
      num5 = num1 - num5;
      num4 = num1;
    }
    Point point1;
    Point point2;
    if (this.IsHorizontalAxis)
    {
      point1 = new Point((double) num2, (double) num4);
      point2 = new Point((double) num3, (double) num5);
    }
    else
    {
      point1 = new Point((double) num4, (double) num2);
      point2 = new Point((double) num5, (double) num3);
    }
    _param1.\u0023\u003Dzk8_eoWQ\u003D(_param2, point1, point2);
  }

  public void \u0023\u003DzWvf8ESIWLp6Z()
  {
    this.\u0023\u003DzDLF9sbNKzh9k.Children.OfType<dje_zATMENTVRQXPESZUNTMAKHBSKCWZW7V9SFA5P9LBGCE4BQBNQAPN2D_ejd>().\u0023\u003Dz30RSSSygABj_<dje_zATMENTVRQXPESZUNTMAKHBSKCWZW7V9SFA5P9LBGCE4BQBNQAPN2D_ejd>(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzUirRmBtkgu2ct\u0024h0\u0024w\u003D\u003D ?? (dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzUirRmBtkgu2ct\u0024h0\u0024w\u003D\u003D = new Action<dje_zATMENTVRQXPESZUNTMAKHBSKCWZW7V9SFA5P9LBGCE4BQBNQAPN2D_ejd>(dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzYyZ8uO9pMJ7PWhNDjIGZikE\u003D)));
  }

  protected virtual void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }

  private static void \u0023\u003DzOPvUPixjU\u00244Y(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd j76J7Ed3DxexqEjd))
      return;
    j76J7Ed3DxexqEjd.\u0023\u003Dz15moWio\u003D("LabelToTickIndent");
    j76J7Ed3DxexqEjd.\u0023\u003Dz15moWio\u003D("IsHorizontalAxis");
    j76J7Ed3DxexqEjd.MajorTickSize = j76J7Ed3DxexqEjd.\u0023\u003Dz3ZlVQyqyprqK(j76J7Ed3DxexqEjd.MajorTickLineStyle);
    j76J7Ed3DxexqEjd.MinorTickSize = j76J7Ed3DxexqEjd.\u0023\u003Dz3ZlVQyqyprqK(j76J7Ed3DxexqEjd.MinorTickLineStyle);
    j76J7Ed3DxexqEjd.\u0023\u003DzEy\u0024V_bY\u003D();
  }

  private static void \u0023\u003DzFgrLJbyJ32zlIW9T7fzjuLs\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd j76J7Ed3DxexqEjd))
      return;
    Style newValue = (Style) _param1.NewValue;
    j76J7Ed3DxexqEjd.MajorTickSize = j76J7Ed3DxexqEjd.\u0023\u003Dz3ZlVQyqyprqK(newValue);
  }

  private static void \u0023\u003DzF9xGIyZlaqWjOY1DXnVnofg\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd j76J7Ed3DxexqEjd))
      return;
    Style newValue = (Style) _param1.NewValue;
    j76J7Ed3DxexqEjd.MinorTickSize = j76J7Ed3DxexqEjd.\u0023\u003Dz3ZlVQyqyprqK(newValue);
  }

  private double \u0023\u003Dz3ZlVQyqyprqK(Style _param1)
  {
    Line line1 = new Line();
    line1.Style = _param1;
    Line line2 = line1;
    return !this.IsHorizontalAxis ? line2.X2 + 1.0 : line2.Y2 + 1.0;
  }

  private static void \u0023\u003Dz8hrSVP3SyudTZXJD6w\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd j76J7Ed3DxexqEjd))
      return;
    j76J7Ed3DxexqEjd.\u0023\u003Dz15moWio\u003D("LabelToTickIndent");
  }

  internal Image \u0023\u003DzdjbuiJBxjZ2I() => this.\u0023\u003DzlbfaVnpq6N5Y;

  internal Grid \u0023\u003Dz8aaJHyxY6f5I() => this.\u0023\u003DzDLF9sbNKzh9k;

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<dje_zATMENTVRQXPESZUNTMAKHBSKCWZW7V9SFA5P9LBGCE4BQBNQAPN2D_ejd> \u0023\u003DzUirRmBtkgu2ct\u0024h0\u0024w\u003D\u003D;

    internal void \u0023\u003DzYyZ8uO9pMJ7PWhNDjIGZikE\u003D(
      dje_zATMENTVRQXPESZUNTMAKHBSKCWZW7V9SFA5P9LBGCE4BQBNQAPN2D_ejd _param1)
    {
      _param1.Children.Clear();
    }
  }
}
