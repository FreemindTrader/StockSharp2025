// Decompiled with JetBrains decompiler
// Type: -.AxisPanel
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
namespace SciChart.Charting;

internal class AxisPanel : 
  Panel,
  INotifyPropertyChanged,
  IAxisPanel
{
  
  public static readonly DependencyProperty \u0023\u003Dz\u0024geG9XF9qNM9 = DependencyProperty.Register(nameof (DrawLabels), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzHxEy7A8kQeb2 = DependencyProperty.Register(nameof (DrawMinorTicks), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003Dz6E17UGyH3Hxe = DependencyProperty.Register(nameof (DrawMajorTicks), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D = DependencyProperty.Register(nameof (MajorTickLineStyle), typeof (Style), typeof (AxisPanel), new PropertyMetadata((object) null, new PropertyChangedCallback(AxisPanel.\u0023\u003DzFgrLJbyJ32zlIW9T7fzjuLs\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D = DependencyProperty.Register(nameof (MinorTickLineStyle), typeof (Style), typeof (AxisPanel), new PropertyMetadata((object) null, new PropertyChangedCallback(AxisPanel.\u0023\u003DzF9xGIyZlaqWjOY1DXnVnofg\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzfMY988N0StOA = DependencyProperty.Register(nameof (AxisAlignment), typeof (AxisAlignment), typeof (AxisPanel), new PropertyMetadata((object) AxisAlignment.Default, new PropertyChangedCallback(AxisPanel.\u0023\u003DzOPvUPixjU\u00244Y)));
  
  public static readonly DependencyProperty \u0023\u003DzQcEu5YLH6ffYkobAOQ\u003D\u003D = DependencyProperty.Register(nameof (AxisLabelToTickIndent), typeof (double), typeof (AxisPanel), new PropertyMetadata((object) 2.0, new PropertyChangedCallback(AxisPanel.\u0023\u003Dz8hrSVP3SyudTZXJD6w\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D = DependencyProperty.Register(nameof (IsLabelCullingEnabled), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
  
  protected Line \u0023\u003DzeEl93ifUiK4P = new Line();
  
  protected Grid \u0023\u003DzDLF9sbNKzh9k;
  
  protected Image \u0023\u003DzlbfaVnpq6N5Y;
  
  protected AxisTitle \u0023\u003DzNdw5dH7x9p9X;
  
  private WriteableBitmap \u0023\u003DzWfNK9E4egtoR;
  
  private double \u0023\u003Dzn_2sUEoYYgva;
  
  private double \u0023\u003DzC4ZArHFLRr_I;
  
  private bool \u0023\u003DzORNmLYbynHFH;
  
  private Action<AxisCanvas> \u0023\u003DzLumisCMIlbJuLqsFBA\u003D\u003D;

  public event PropertyChangedEventHandler PropertyChanged;

  public bool IsLabelCullingEnabled
  {
    get
    {
      return (bool) this.GetValue(AxisPanel.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D, (object) value);
    }
  }

  public AxisAlignment AxisAlignment
  {
    get
    {
      return (AxisAlignment) this.GetValue(AxisPanel.\u0023\u003DzfMY988N0StOA);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003DzfMY988N0StOA, (object) value);
    }
  }

  public Style MajorTickLineStyle
  {
    get
    {
      return (Style) this.GetValue(AxisPanel.\u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D, (object) value);
    }
  }

  public Style MinorTickLineStyle
  {
    get
    {
      return (Style) this.GetValue(AxisPanel.\u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D, (object) value);
    }
  }

  public bool DrawMajorTicks
  {
    get
    {
      return (bool) this.GetValue(AxisPanel.\u0023\u003Dz6E17UGyH3Hxe);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003Dz6E17UGyH3Hxe, (object) value);
    }
  }

  public bool DrawMinorTicks
  {
    get
    {
      return (bool) this.GetValue(AxisPanel.\u0023\u003DzHxEy7A8kQeb2);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003DzHxEy7A8kQeb2, (object) value);
    }
  }

  public bool DrawLabels
  {
    get
    {
      return (bool) this.GetValue(AxisPanel.\u0023\u003Dz\u0024geG9XF9qNM9);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003Dz\u0024geG9XF9qNM9, (object) value);
    }
  }

  public double AxisLabelToTickIndent
  {
    get
    {
      return (double) this.GetValue(AxisPanel.\u0023\u003DzQcEu5YLH6ffYkobAOQ\u003D\u003D);
    }
    set
    {
      this.SetValue(AxisPanel.\u0023\u003DzQcEu5YLH6ffYkobAOQ\u003D\u003D, (object) value);
    }
  }

  public bool IsHorizontalAxis
  {
    get
    {
      AxisAlignment axisAlignment = this.AxisAlignment;
      return axisAlignment == AxisAlignment.Bottom || axisAlignment == AxisAlignment.Top;
    }
  }

  public Thickness LabelToTickIndent
  {
    get
    {
      return new Thickness(this.AxisAlignment == AxisAlignment.Right ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == AxisAlignment.Bottom ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == AxisAlignment.Left ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == AxisAlignment.Top ? this.AxisLabelToTickIndent : 0.0);
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
      this.OnPropertyChanged(nameof (MajorTickSize));
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
      this.OnPropertyChanged(nameof (MinorTickSize));
    }
  }

  public Action<AxisCanvas> AddLabels
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
    this.AddTickLabels(this.AddLabels);
    this.\u0023\u003DzDLF9sbNKzh9k.Measure(_param1);
    double num1 = Math.Max(this.MinorTickSize, this.MajorTickSize);
    Size size;
    switch (this.AxisAlignment)
    {
      case AxisAlignment.Right:
      case AxisAlignment.Left:
        this.\u0023\u003DzlbfaVnpq6N5Y.Measure(new Size(num1, _param1.Height));
        double num2 = num1 + this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width;
        this.\u0023\u003DzNdw5dH7x9p9X.Measure(new Size(_param1.Width - num2, _param1.Height));
        size = new Size(num2 + this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height);
        break;
      case AxisAlignment.Top:
      case AxisAlignment.Bottom:
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
    if (_param1 is AxisTitle)
      this.\u0023\u003DzNdw5dH7x9p9X = (AxisTitle) _param1;
    if (_param1 is Grid)
      this.\u0023\u003DzDLF9sbNKzh9k = (Grid) _param1;
    this.\u0023\u003DzORNmLYbynHFH = this.\u0023\u003DzlbfaVnpq6N5Y != null && this.\u0023\u003DzNdw5dH7x9p9X != null && this.\u0023\u003DzDLF9sbNKzh9k != null;
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    double num1 = Math.Max(this.MinorTickSize, this.MajorTickSize);
    switch (this.AxisAlignment)
    {
      case AxisAlignment.Right:
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(0.0, 0.0, num1, _param1.Height));
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(num1, 0.0, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, _param1.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(num1 + this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, 0.0, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, _param1.Height));
        break;
      case AxisAlignment.Left:
        double num2 = _param1.Width - num1;
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(num2, 0.0, num1, _param1.Height));
        double num3 = num2 - this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width;
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(num3, 0.0, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Width, _param1.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(num3 - this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, 0.0, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Width, _param1.Height));
        break;
      case AxisAlignment.Top:
        double num4 = _param1.Height - num1;
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(0.0, num4, _param1.Width, num1));
        double num5 = num4 - this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height;
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(0.0, num5, _param1.Width, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(0.0, num5 - this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Height, _param1.Width, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Height));
        break;
      case AxisAlignment.Bottom:
        this.\u0023\u003DzlbfaVnpq6N5Y.Arrange(new Rect(0.0, 0.0, _param1.Width, num1));
        this.\u0023\u003DzDLF9sbNKzh9k.Arrange(new Rect(0.0, num1, _param1.Width, this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height));
        this.\u0023\u003DzNdw5dH7x9p9X.Arrange(new Rect(0.0, num1 + this.\u0023\u003DzDLF9sbNKzh9k.DesiredSize.Height, _param1.Width, this.\u0023\u003DzNdw5dH7x9p9X.DesiredSize.Height));
        break;
    }
    return _param1;
  }

  public void AddTickLabels(
    Action<AxisCanvas> _param1)
  {
    if (this.\u0023\u003DzDLF9sbNKzh9k == null || !this.DrawLabels)
      return;
    AxisCanvas quecZ77TxG2K5CaEjd1 = this.\u0023\u003DzDLF9sbNKzh9k.Children[0].\u0023\u003DzST\u0024t7rI\u003D() ? (AxisCanvas) this.\u0023\u003DzDLF9sbNKzh9k.Children[1] : (AxisCanvas) this.\u0023\u003DzDLF9sbNKzh9k.Children[0];
    AxisCanvas quecZ77TxG2K5CaEjd2 = this.\u0023\u003DzDLF9sbNKzh9k.Children[0].\u0023\u003DzST\u0024t7rI\u003D() ? (AxisCanvas) this.\u0023\u003DzDLF9sbNKzh9k.Children[0] : (AxisCanvas) this.\u0023\u003DzDLF9sbNKzh9k.Children[1];
    quecZ77TxG2K5CaEjd1.Visibility = Visibility.Collapsed;
    quecZ77TxG2K5CaEjd1.SizeHeightToContent = this.IsHorizontalAxis;
    quecZ77TxG2K5CaEjd1.SizeWidthToContent = !this.IsHorizontalAxis;
    _param1(quecZ77TxG2K5CaEjd1);
    quecZ77TxG2K5CaEjd1.Visibility = Visibility.Visible;
    quecZ77TxG2K5CaEjd2.Visibility = Visibility.Collapsed;
  }

  public void Invalidate()
  {
    this.InvalidateMeasure();
    this.InvalidateArrange();
  }

  public virtual void DrawTicks(
    TickCoordinates _param1,
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
      blRxkxWuDZv6cV0H.Clear();
      if (this.DrawMinorTicks && _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D() != null)
        this.DrawTicks((IRenderContext2D) blRxkxWuDZv6cV0H, this.MinorTickLineStyle, this.MinorTickSize, _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D(), _param2);
      if (!this.DrawMajorTicks || _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D() == null)
        return;
      this.DrawTicks((IRenderContext2D) blRxkxWuDZv6cV0H, this.MajorTickLineStyle, this.MajorTickSize, _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D(), _param2);
    }
  }

  protected virtual Size \u0023\u003DzR5BsR9jdiJQz()
  {
    return new Size(this.IsHorizontalAxis ? (double) (int) this.\u0023\u003DzDLF9sbNKzh9k.ActualWidth : (double) (int) this.MajorTickSize, this.IsHorizontalAxis ? (double) (int) this.MajorTickSize : (double) (int) this.\u0023\u003DzDLF9sbNKzh9k.ActualHeight);
  }

  protected virtual void DrawTicks(
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
    if (this.AxisAlignment == AxisAlignment.Top || this.AxisAlignment == AxisAlignment.Left)
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

  public void ClearLabels()
  {
    this.\u0023\u003DzDLF9sbNKzh9k.Children.OfType<TickLabelAxisCanvas>().\u0023\u003Dz30RSSSygABj_<TickLabelAxisCanvas>(AxisPanel.SomeClass34343383.\u0023\u003DzUirRmBtkgu2ct\u0024h0\u0024w\u003D\u003D ?? (AxisPanel.SomeClass34343383.\u0023\u003DzUirRmBtkgu2ct\u0024h0\u0024w\u003D\u003D = new Action<TickLabelAxisCanvas>(AxisPanel.SomeClass34343383.SomeMethond0343.\u0023\u003DzYyZ8uO9pMJ7PWhNDjIGZikE\u003D)));
  }

  protected virtual void OnPropertyChanged(string _param1)
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
    if (!(_param0 is AxisPanel j76J7Ed3DxexqEjd))
      return;
    j76J7Ed3DxexqEjd.OnPropertyChanged("LabelToTickIndent");
    j76J7Ed3DxexqEjd.OnPropertyChanged("IsHorizontalAxis");
    j76J7Ed3DxexqEjd.MajorTickSize = j76J7Ed3DxexqEjd.\u0023\u003Dz3ZlVQyqyprqK(j76J7Ed3DxexqEjd.MajorTickLineStyle);
    j76J7Ed3DxexqEjd.MinorTickSize = j76J7Ed3DxexqEjd.\u0023\u003Dz3ZlVQyqyprqK(j76J7Ed3DxexqEjd.MinorTickLineStyle);
    j76J7Ed3DxexqEjd.Invalidate();
  }

  private static void \u0023\u003DzFgrLJbyJ32zlIW9T7fzjuLs\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is AxisPanel j76J7Ed3DxexqEjd))
      return;
    Style newValue = (Style) _param1.NewValue;
    j76J7Ed3DxexqEjd.MajorTickSize = j76J7Ed3DxexqEjd.\u0023\u003Dz3ZlVQyqyprqK(newValue);
  }

  private static void \u0023\u003DzF9xGIyZlaqWjOY1DXnVnofg\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is AxisPanel j76J7Ed3DxexqEjd))
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
    if (!(_param0 is AxisPanel j76J7Ed3DxexqEjd))
      return;
    j76J7Ed3DxexqEjd.OnPropertyChanged("LabelToTickIndent");
  }

  internal Image \u0023\u003DzdjbuiJBxjZ2I() => this.\u0023\u003DzlbfaVnpq6N5Y;

  internal Grid \u0023\u003Dz8aaJHyxY6f5I() => this.\u0023\u003DzDLF9sbNKzh9k;

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly AxisPanel.SomeClass34343383 SomeMethond0343 = new AxisPanel.SomeClass34343383();
    public static Action<TickLabelAxisCanvas> \u0023\u003DzUirRmBtkgu2ct\u0024h0\u0024w\u003D\u003D;

    internal void \u0023\u003DzYyZ8uO9pMJ7PWhNDjIGZikE\u003D(
      TickLabelAxisCanvas _param1)
    {
      _param1.Children.Clear();
    }
  }
}
