// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.TextAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

[TemplatePart(Name = "PART_InputTextArea", Type = typeof (TextBox))]
internal class TextAnnotation : AnchorPointAnnotation
{
  public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539343540), typeof (CornerRadius), typeof (TextAnnotation), new PropertyMetadata((object) new CornerRadius()));
  public static readonly DependencyProperty TextProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427542), typeof (string), typeof (TextAnnotation), new PropertyMetadata((object) string.Empty));
  public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539440794), typeof (TextAlignment), typeof (TextAnnotation), new PropertyMetadata((object) TextAlignment.Left));
  public static readonly DependencyProperty TextStretchProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539342945), typeof (Stretch), typeof (TextAnnotation), new PropertyMetadata((object) Stretch.None));
  private TextBox _inputTextArea;

  public TextAnnotation()
  {
    this.DefaultStyleKey = (object) typeof (TextAnnotation);
    DependencyProperty contextMenuProperty = FrameworkElement.ContextMenuProperty;
    ContextMenu contextMenu = new ContextMenu();
    contextMenu.Visibility = Visibility.Collapsed;
    this.SetCurrentValue(contextMenuProperty, (object) contextMenu);
  }

  public CornerRadius CornerRadius
  {
    get => (CornerRadius) this.GetValue(TextAnnotation.CornerRadiusProperty);
    set => this.SetValue(TextAnnotation.CornerRadiusProperty, (object) value);
  }

  public TextAlignment TextAlignment
  {
    get => (TextAlignment) this.GetValue(TextAnnotation.TextAlignmentProperty);
    set => this.SetValue(TextAnnotation.TextAlignmentProperty, (object) value);
  }

  public string Text
  {
    get => (string) this.GetValue(TextAnnotation.TextProperty);
    set => this.SetValue(TextAnnotation.TextProperty, (object) value);
  }

  private Stretch TextStretch
  {
    get => (Stretch) this.GetValue(TextAnnotation.TextStretchProperty);
    set => this.SetValue(TextAnnotation.TextStretchProperty, (object) value);
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.AnnotationRoot = (FrameworkElement) this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Border>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539343255));
    this._inputTextArea = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<TextBox>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539343445));
    Binding binding = new Binding(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539342907))
    {
      RelativeSource = RelativeSource.TemplatedParent
    };
    this._inputTextArea.SetBinding(FrameworkElement.ContextMenuProperty, (BindingBase) binding);
    this.PerformFocusOnInputTextArea();
  }

  protected override void FocusInputTextArea()
  {
    if (this._inputTextArea == null)
      return;
    this._inputTextArea.IsEnabled = true;
    this._inputTextArea.Focus();
  }

  public void RemoveFocusFromInputTextArea() => this.RemoveFocusInputTextArea();

  protected override void RemoveFocusInputTextArea()
  {
    if (this._inputTextArea == null)
      return;
    this._inputTextArea.IsEnabled = false;
  }

  private \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D CoerceValues()
  {
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates = this.GetCoordinates(this.GetCanvas(this.AnnotationCanvas), this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D(), this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D());
    double z6aJoeqoqAzym = coordinates.\u0023\u003Dz6aJoeqoqAzym;
    double zWp13vlQiZcJc = coordinates.\u0023\u003DzWp13vlQiZCJc;
    Point point = new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE);
    if (double.IsNaN(z6aJoeqoqAzym) || double.IsNaN(zWp13vlQiZcJc))
    {
      double xCoord = point.X + this.ActualWidth;
      double yCoord = point.Y + this.ActualHeight;
      IComparable[] comparableArray = this.FromCoordinates(xCoord, yCoord);
      this.SetCurrentValue(AnnotationBase.X2Property, (object) comparableArray[0]);
      this.SetCurrentValue(AnnotationBase.Y2Property, (object) comparableArray[1]);
      coordinates.\u0023\u003Dz6aJoeqoqAzym = xCoord;
      coordinates.\u0023\u003DzWp13vlQiZCJc = yCoord;
    }
    return coordinates;
  }

  public override bool IsPointWithinBounds(Point point)
  {
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D nnpojF4sCpkA8pp0g = this.CoerceValues();
    return new Rect(new Point(nnpojF4sCpkA8pp0g.\u0023\u003DzS2_K6sVvd5IY, nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE), new Point(nnpojF4sCpkA8pp0g.\u0023\u003Dz6aJoeqoqAzym, nnpojF4sCpkA8pp0g.\u0023\u003DzWp13vlQiZCJc)).Contains(point);
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new TextAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new TextAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  internal sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(TextAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<TextAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      _param1 = this.\u0023\u003Dz_iIh83yfe01U().GetAnchorAnnotationCoordinates(_param1);
      double zS2K6sVvd5Iy = _param1.\u0023\u003DzS2_K6sVvd5IY;
      double z6aJoeqoqAzym = _param1.\u0023\u003Dz6aJoeqoqAzym;
      double z2J4l3QuGwZhe = _param1.\u0023\u003Dz2J4l3QUGwZHE;
      double zWp13vlQiZcJc = _param1.\u0023\u003DzWp13vlQiZCJc;
      if (z6aJoeqoqAzym < zS2K6sVvd5Iy)
        \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref zS2K6sVvd5Iy, ref z6aJoeqoqAzym);
      if (zWp13vlQiZcJc < z2J4l3QuGwZhe)
        \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref z2J4l3QuGwZhe, ref zWp13vlQiZcJc);
      double num1 = z6aJoeqoqAzym - zS2K6sVvd5Iy + 1.0;
      double num2 = zWp13vlQiZcJc - z2J4l3QuGwZhe + 1.0;
      if (!zS2K6sVvd5Iy.\u0023\u003DzutrFxOU\u003D() || !z2J4l3QuGwZhe.\u0023\u003DzutrFxOU\u003D())
        return;
      if (num1.\u0023\u003DzutrFxOU\u003D())
        this.\u0023\u003Dz_iIh83yfe01U().MinWidth = num1;
      if (num2.\u0023\u003DzutrFxOU\u003D())
        this.\u0023\u003Dz_iIh83yfe01U().MinHeight = num2;
      Canvas.SetLeft((UIElement) this.\u0023\u003Dz_iIh83yfe01U(), zS2K6sVvd5Iy);
      Canvas.SetTop((UIElement) this.\u0023\u003Dz_iIh83yfe01U(), z2J4l3QuGwZhe);
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      _param1 = this.\u0023\u003Dz_iIh83yfe01U().GetAnchorAnnotationCoordinates(_param1);
      if (double.IsNaN(_param1.\u0023\u003Dz6aJoeqoqAzym) || double.IsNaN(_param1.\u0023\u003DzWp13vlQiZCJc))
        _param1 = this.\u0023\u003Dz_iIh83yfe01U().CoerceValues();
      return new Point[1]
      {
        new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE)
      };
    }

    protected override void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      ref double _param2,
      ref double _param3,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param4)
    {
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + _param2;
      double d2 = _param1.\u0023\u003Dz6aJoeqoqAzym + _param2;
      double d3 = _param1.\u0023\u003Dz2J4l3QUGwZHE + _param3;
      double d4 = _param1.\u0023\u003DzWp13vlQiZCJc + _param3;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.\u0023\u003Dzu2ObQ3hMALTN()) || !this.\u0023\u003DzpTsgWlwWfZwP(d3, _param4.\u0023\u003Dz2kO1mtG\u0024bEUM()) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.\u0023\u003Dzu2ObQ3hMALTN()) || !this.\u0023\u003DzpTsgWlwWfZwP(d4, _param4.\u0023\u003Dz2kO1mtG\u0024bEUM()))
      {
        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
        double val2_1 = double.IsNaN(d2) ? val1_1 : d2;
        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
        double val2_2 = double.IsNaN(d4) ? val1_2 : d4;
        if (Math.Max(val1_1, val2_1) < 0.0)
          _param2 -= Math.Max(val1_1, val2_1);
        if (Math.Min(val1_1, val2_1) > _param4.\u0023\u003Dzu2ObQ3hMALTN())
          _param2 -= Math.Min(val1_1, val2_1) - (_param4.\u0023\u003Dzu2ObQ3hMALTN() - 1.0);
        if (Math.Max(val1_2, val2_2) < 0.0)
          _param3 -= Math.Max(val1_2, val2_2);
        if (Math.Min(val1_2, val2_2) > _param4.\u0023\u003Dz2kO1mtG\u0024bEUM())
          _param3 -= Math.Min(val1_2, val2_2) - (_param4.\u0023\u003Dz2kO1mtG\u0024bEUM() - 1.0);
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += _param2;
      _param1.\u0023\u003Dz6aJoeqoqAzym += _param2;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += _param3;
      _param1.\u0023\u003DzWp13vlQiZCJc += _param3;
      IComparable[] comparableArray1 = this.\u0023\u003DzvQ1aszE\u003D(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray1[0]);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.Y1Property, (object) comparableArray1[1]);
      IComparable[] comparableArray2 = this.\u0023\u003DzvQ1aszE\u003D(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.X2Property, (object) comparableArray2[0]);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.Y2Property, (object) comparableArray2[1]);
    }
  }

  internal sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(TextAnnotation _param1) : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<TextAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D nnpojF4sCpkA8pp0g = this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
      Canvas.SetLeft((UIElement) this.\u0023\u003Dz_iIh83yfe01U(), nnpojF4sCpkA8pp0g.\u0023\u003DzS2_K6sVvd5IY - this.\u0023\u003Dz_iIh83yfe01U().HorizontalOffset);
      Canvas.SetTop((UIElement) this.\u0023\u003Dz_iIh83yfe01U(), nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE - this.\u0023\u003Dz_iIh83yfe01U().VerticalOffset);
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D nnpojF4sCpkA8pp0g = this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
      return new Point[1]
      {
        new Point(nnpojF4sCpkA8pp0g.\u0023\u003DzS2_K6sVvd5IY - this.\u0023\u003Dz_iIh83yfe01U().HorizontalOffset, nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE - this.\u0023\u003Dz_iIh83yfe01U().VerticalOffset)
      };
    }

    protected override bool \u0023\u003DzRe9EEbV7q4ey(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      Size _param2)
    {
      return (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.Width || _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 ? 1 : (_param1.\u0023\u003Dz2J4l3QUGwZHE > _param2.Height ? 1 : 0)) == 0;
    }

    protected override void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      Point _param2,
      Point _param3,
      Size _param4)
    {
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + _param2.X;
      double d2 = _param1.\u0023\u003Dz6aJoeqoqAzym + _param3.X;
      double d3 = _param1.\u0023\u003Dz2J4l3QUGwZHE + _param2.Y;
      double d4 = _param1.\u0023\u003DzWp13vlQiZCJc + _param3.Y;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.Width) || !this.\u0023\u003DzpTsgWlwWfZwP(d3, _param4.Height) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.Width) || !this.\u0023\u003DzpTsgWlwWfZwP(d4, _param4.Height))
      {
        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
        double val2_1 = double.IsNaN(d2) ? val1_1 : d2;
        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
        double val2_2 = double.IsNaN(d4) ? val1_2 : d4;
        if (Math.Max(val1_1, val2_1) < 0.0)
        {
          double num = Math.Max(val1_1, val2_1);
          _param2.X -= num;
          _param3.X -= num;
        }
        if (Math.Min(val1_1, val2_1) > _param4.Width)
        {
          double num = Math.Min(val1_1, val2_1) - (_param4.Width - 1.0);
          _param2.X -= num;
          _param3.X -= num;
        }
        if (Math.Max(val1_2, val2_2) < 0.0)
        {
          double num = Math.Max(val1_2, val2_2);
          _param2.Y -= num;
          _param3.Y -= num;
        }
        if (Math.Min(val1_2, val2_2) > _param4.Height)
        {
          double num = Math.Min(val1_2, val2_2) - (_param4.Height - 1.0);
          _param2.Y -= num;
          _param3.Y -= num;
        }
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += _param2.X;
      _param1.\u0023\u003Dz6aJoeqoqAzym += _param3.X;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += _param2.Y;
      _param1.\u0023\u003DzWp13vlQiZCJc += _param3.Y;
      IComparable[] comparableArray1 = this.\u0023\u003DzvQ1aszE\u003D(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray1[0]);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.Y1Property, (object) comparableArray1[1]);
      IComparable[] comparableArray2 = this.\u0023\u003DzvQ1aszE\u003D(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.X2Property, (object) comparableArray2[0]);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.Y2Property, (object) comparableArray2[1]);
    }
  }
}
