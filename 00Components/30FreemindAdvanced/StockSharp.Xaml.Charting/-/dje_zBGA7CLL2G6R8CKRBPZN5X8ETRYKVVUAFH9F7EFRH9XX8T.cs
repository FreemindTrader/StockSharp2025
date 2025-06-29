// Decompiled with JetBrains decompiler
// Type: -.dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

[TemplatePart(Name = "TransformRoot", Type = typeof (Grid))]
[TemplatePart(Name = "Presenter", Type = typeof (ContentPresenter))]
internal sealed class dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd : 
  ContentControl
{
  
  public static readonly DependencyProperty \u0023\u003Dz32W8b1i6xoYS = DependencyProperty.Register(XXX.SSS(-539440922), typeof (Transform), typeof (dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003Dz0HuiTL\u0024SKGca)));
  
  private Panel \u0023\u003DzhIjAwTPpB57_;
  
  private ContentPresenter \u0023\u003DzZ\u00240zMrM8YIwq;
  
  private MatrixTransform \u0023\u003DzVHHoBke2PxUh;
  
  private Matrix \u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D;
  
  private Size \u0023\u003Dz60AC8Q2WG7I_ = Size.Empty;

  public dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd);
    this.IsTabStop = false;
  }

  public new Transform LayoutTransform
  {
    get
    {
      return (Transform) this.GetValue(dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003Dz32W8b1i6xoYS);
    }
    set
    {
      this.SetValue(dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003Dz32W8b1i6xoYS, (object) value);
    }
  }

  private FrameworkElement \u0023\u003DzAzxBnC4KOJLN()
  {
    if (this.\u0023\u003DzZ\u00240zMrM8YIwq == null)
      return (FrameworkElement) null;
    return this.\u0023\u003DzZ\u00240zMrM8YIwq.Content is FrameworkElement content ? content : (FrameworkElement) this.\u0023\u003DzZ\u00240zMrM8YIwq;
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003DzhIjAwTPpB57_ = (Panel) (this.GetTemplateChild(XXX.SSS(-539440948)) as Grid);
    this.\u0023\u003DzZ\u00240zMrM8YIwq = this.GetTemplateChild(XXX.SSS(-539440936)) as ContentPresenter;
    this.\u0023\u003DzVHHoBke2PxUh = new MatrixTransform();
    if (this.\u0023\u003DzhIjAwTPpB57_ != null)
      this.\u0023\u003DzhIjAwTPpB57_.RenderTransform = (Transform) this.\u0023\u003DzVHHoBke2PxUh;
    this.\u0023\u003DzpWkDqcnF_D9B();
  }

  private static void \u0023\u003Dz0HuiTL\u0024SKGca(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd) _param0).\u0023\u003DzFuJzw\u0024c\u003D((Transform) _param1.NewValue);
  }

  public void \u0023\u003DzpWkDqcnF_D9B()
  {
    this.\u0023\u003DzFuJzw\u0024c\u003D(this.LayoutTransform);
  }

  private void \u0023\u003DzFuJzw\u0024c\u003D(Transform _param1)
  {
    this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D = dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003Dz83uKsjUrBFp7(this.\u0023\u003DzSgKrxhowo3cf(_param1), 4);
    if (this.\u0023\u003DzVHHoBke2PxUh != null)
      this.\u0023\u003DzVHHoBke2PxUh.Matrix = this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D;
    this.InvalidateMeasure();
  }

  private Matrix \u0023\u003DzSgKrxhowo3cf(Transform _param1)
  {
    switch (_param1)
    {
      case TransformGroup transformGroup:
        Matrix matrix = Matrix.Identity;
        foreach (Transform child in transformGroup.Children)
          matrix = dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003DzCewUhnWkyYIz(matrix, this.\u0023\u003DzSgKrxhowo3cf(child));
        return matrix;
      case RotateTransform rotateTransform:
        double num1 = 2.0 * Math.PI * rotateTransform.Angle / 360.0;
        double num2 = Math.Sin(num1);
        double num3 = Math.Cos(num1);
        return new Matrix(num3, num2, -num2, num3, 0.0, 0.0);
      case ScaleTransform scaleTransform:
        return new Matrix(scaleTransform.ScaleX, 0.0, 0.0, scaleTransform.ScaleY, 0.0, 0.0);
      case SkewTransform skewTransform:
        double angleX = skewTransform.AngleX;
        return new Matrix(1.0, 2.0 * Math.PI * skewTransform.AngleY / 360.0, 2.0 * Math.PI * angleX / 360.0, 1.0, 0.0, 0.0);
      case MatrixTransform matrixTransform:
        return matrixTransform.Matrix;
      default:
        return Matrix.Identity;
    }
  }

  protected override Size MeasureOverride(Size _param1)
  {
    FrameworkElement frameworkElement = this.\u0023\u003DzAzxBnC4KOJLN();
    if (this.\u0023\u003DzhIjAwTPpB57_ == null || frameworkElement == null)
      return Size.Empty;
    this.\u0023\u003DzhIjAwTPpB57_.Measure(!Size.op_Equality(this.\u0023\u003Dz60AC8Q2WG7I_, Size.Empty) ? this.\u0023\u003Dz60AC8Q2WG7I_ : this.\u0023\u003DzthQJ9zgZOkOvr7IaO4b9cuE\u003D(_param1));
    Rect rect = dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003DzEzTnxQC3CD4\u0024(new Rect(0.0, 0.0, this.\u0023\u003DzhIjAwTPpB57_.DesiredSize.Width, this.\u0023\u003DzhIjAwTPpB57_.DesiredSize.Height), this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D);
    return new Size(rect.Width, rect.Height);
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    FrameworkElement frameworkElement = this.\u0023\u003DzAzxBnC4KOJLN();
    if (this.\u0023\u003DzhIjAwTPpB57_ == null || frameworkElement == null)
      return _param1;
    Size size = this.\u0023\u003DzthQJ9zgZOkOvr7IaO4b9cuE\u003D(_param1);
    if (dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003DzsTf\u0024wp8hY4v\u0024kW\u0024FiA\u003D\u003D(size, this.\u0023\u003DzhIjAwTPpB57_.DesiredSize))
      size = this.\u0023\u003DzhIjAwTPpB57_.DesiredSize;
    Rect rect = dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003DzEzTnxQC3CD4\u0024(new Rect(0.0, 0.0, size.Width, size.Height), this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D);
    this.\u0023\u003DzhIjAwTPpB57_.Arrange(new Rect(-rect.Left + (_param1.Width - rect.Width) / 2.0, -rect.Top + (_param1.Height - rect.Height) / 2.0, size.Width, size.Height));
    if (dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003DzsTf\u0024wp8hY4v\u0024kW\u0024FiA\u003D\u003D(size, frameworkElement.RenderSize) && Size.op_Equality(Size.Empty, this.\u0023\u003Dz60AC8Q2WG7I_))
    {
      this.\u0023\u003Dz60AC8Q2WG7I_ = new Size(frameworkElement.ActualWidth, frameworkElement.ActualHeight);
      this.InvalidateMeasure();
    }
    else
      this.\u0023\u003Dz60AC8Q2WG7I_ = Size.Empty;
    return _param1;
  }

  private Size \u0023\u003DzthQJ9zgZOkOvr7IaO4b9cuE\u003D(Size _param1)
  {
    Size size = Size.Empty;
    bool flag1 = double.IsInfinity(_param1.Width);
    if (flag1)
      _param1.Width = _param1.Height;
    bool flag2 = double.IsInfinity(_param1.Height);
    if (flag2)
      _param1.Height = _param1.Width;
    double m11 = this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D.M11;
    double m12 = this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D.M12;
    double m21 = this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D.M21;
    double m22 = this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D.M22;
    double num1 = Math.Abs(_param1.Width / m11);
    double num2 = Math.Abs(_param1.Width / m21);
    double num3 = Math.Abs(_param1.Height / m12);
    double num4 = Math.Abs(_param1.Height / m22);
    double val1_1 = num1 / 2.0;
    double val1_2 = num2 / 2.0;
    double val1_3 = num3 / 2.0;
    double val1_4 = num4 / 2.0;
    double num5 = -(num2 / num1);
    double num6 = -(num4 / num3);
    if (0.0 == _param1.Width || 0.0 == _param1.Height)
      size = new Size(_param1.Width, _param1.Height);
    else if (flag1 & flag2)
      size = new Size(double.PositiveInfinity, double.PositiveInfinity);
    else if (!dje_zBGA7CLL2G6R8CKRBPZN5X8ETRYKVVUAFH9F7EFRH9XX8TBSFHHKNNCMTL28J6PFVYW_ejd.\u0023\u003DzV9X1hDY95bUE(this.\u0023\u003DzoNEXi9IWk2BNsqQ_lTShmRs\u003D))
      size = new Size(0.0, 0.0);
    else if (0.0 == m12 || 0.0 == m21)
    {
      double val2_1 = flag2 ? double.PositiveInfinity : num4;
      double val2_2 = flag1 ? double.PositiveInfinity : num1;
      if (0.0 == m12 && 0.0 == m21)
        size = new Size(val2_2, val2_1);
      else if (0.0 == m12)
      {
        double num7 = Math.Min(val1_2, val2_1);
        size = new Size(val2_2 - Math.Abs(m21 * num7 / m11), num7);
      }
      else if (0.0 == m21)
      {
        double num8 = Math.Min(val1_3, val2_2);
        size = new Size(num8, val2_1 - Math.Abs(m12 * num8 / m22));
      }
    }
    else if (0.0 == m11 || 0.0 == m22)
    {
      double val2_3 = flag2 ? double.PositiveInfinity : num3;
      double val2_4 = flag1 ? double.PositiveInfinity : num2;
      if (0.0 == m11 && 0.0 == m22)
        size = new Size(val2_3, val2_4);
      else if (0.0 == m11)
      {
        double num9 = Math.Min(val1_4, val2_4);
        size = new Size(val2_3 - Math.Abs(m22 * num9 / m12), num9);
      }
      else if (0.0 == m22)
      {
        double num10 = Math.Min(val1_1, val2_3);
        size = new Size(num10, val2_4 - Math.Abs(m11 * num10 / m21));
      }
    }
    else if (val1_2 <= num6 * val1_1 + num4)
      size = new Size(val1_1, val1_2);
    else if (val1_4 <= num5 * val1_3 + num2)
    {
      size = new Size(val1_3, val1_4);
    }
    else
    {
      double num11 = (num4 - num2) / (num5 - num6);
      size = new Size(num11, num5 * num11 + num2);
    }
    return size;
  }

  private static bool \u0023\u003DzsTf\u0024wp8hY4v\u0024kW\u0024FiA\u003D\u003D(
    Size _param0,
    Size _param1)
  {
    return _param0.Width + 0.0001 < _param1.Width || _param0.Height + 0.0001 < _param1.Height;
  }

  private static Matrix \u0023\u003Dz83uKsjUrBFp7(Matrix _param0, int _param1)
  {
    return new Matrix(Math.Round(_param0.M11, _param1), Math.Round(_param0.M12, _param1), Math.Round(_param0.M21, _param1), Math.Round(_param0.M22, _param1), _param0.OffsetX, _param0.OffsetY);
  }

  private static Rect \u0023\u003DzEzTnxQC3CD4\u0024(Rect _param0, Matrix _param1)
  {
    Point point1 = _param1.Transform(new Point(_param0.Left, _param0.Top));
    Point point2 = _param1.Transform(new Point(_param0.Right, _param0.Top));
    Point point3 = _param1.Transform(new Point(_param0.Left, _param0.Bottom));
    Point point4 = _param1.Transform(new Point(_param0.Right, _param0.Bottom));
    double num1 = Math.Min(Math.Min(point1.X, point2.X), Math.Min(point3.X, point4.X));
    double num2 = Math.Min(Math.Min(point1.Y, point2.Y), Math.Min(point3.Y, point4.Y));
    double num3 = Math.Max(Math.Max(point1.X, point2.X), Math.Max(point3.X, point4.X));
    double num4 = Math.Max(Math.Max(point1.Y, point2.Y), Math.Max(point3.Y, point4.Y));
    return new Rect(num1, num2, num3 - num1, num4 - num2);
  }

  private static Matrix \u0023\u003DzCewUhnWkyYIz(Matrix _param0, Matrix _param1)
  {
    return new Matrix(_param0.M11 * _param1.M11 + _param0.M12 * _param1.M21, _param0.M11 * _param1.M12 + _param0.M12 * _param1.M22, _param0.M21 * _param1.M11 + _param0.M22 * _param1.M21, _param0.M21 * _param1.M12 + _param0.M22 * _param1.M22, _param0.OffsetX * _param1.M11 + _param0.OffsetY * _param1.M21 + _param1.OffsetX, _param0.OffsetX * _param1.M12 + _param0.OffsetY * _param1.M22 + _param1.OffsetY);
  }

  private static bool \u0023\u003DzV9X1hDY95bUE(Matrix _param0)
  {
    return 0.0 != _param0.M11 * _param0.M22 - _param0.M12 * _param0.M21;
  }

  [Conditional("DIAGNOSTICWRITELINE")]
  private static void \u0023\u003DzPj880SE7lIpT(string _param0)
  {
  }
}
