// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.CustomAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal class CustomAnnotation : AnchorPointAnnotation
{
  public CustomAnnotation()
  {
    this.DefaultStyleKey = (object) typeof (CustomAnnotation);
    this.AnnotationRoot = (FrameworkElement) this;
  }

  protected override void OnContentChanged(object oldContent, object newContent)
  {
    base.OnContentChanged(oldContent, newContent);
    if (!(newContent is FrameworkElement frameworkElement))
      frameworkElement = (FrameworkElement) this;
    this.AnnotationRoot = frameworkElement;
    this.Refresh();
  }

  protected override void OnContentTemplateChanged(
    DataTemplate oldContentTemplate,
    DataTemplate newContentTemplate)
  {
    base.OnContentTemplateChanged(oldContentTemplate, newContentTemplate);
    this.AnnotationRoot = (FrameworkElement) this.Content ?? (FrameworkElement) this;
    this.Refresh();
  }

  public override bool IsPointWithinBounds(Point point)
  {
    FrameworkElement annotationRoot = this.AnnotationRoot;
    return annotationRoot != null && annotationRoot.\u0023\u003DzbOxVzAyGdX66(point);
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new CustomAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new CustomAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  private static void PlaceCustomAnnotation(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    CustomAnnotation annotation)
  {
    coordinates = annotation.GetAnchorAnnotationCoordinates(coordinates);
    double zS2K6sVvd5Iy = coordinates.\u0023\u003DzS2_K6sVvd5IY;
    double z2J4l3QuGwZhe = coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    Canvas.SetLeft((UIElement) annotation, zS2K6sVvd5Iy);
    Canvas.SetTop((UIElement) annotation, z2J4l3QuGwZhe);
  }

  private static Point[] CalculateBasePoints(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    CustomAnnotation annotation)
  {
    coordinates = annotation.GetAnchorAnnotationCoordinates(coordinates);
    if (annotation.AnnotationRoot == null)
      return Array.Empty<Point>();
    return new Point[1]
    {
      new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE)
    };
  }

  internal sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(CustomAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<CustomAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      CustomAnnotation.PlaceCustomAnnotation(_param1, this.\u0023\u003Dz_iIh83yfe01U());
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return CustomAnnotation.CalculateBasePoints(_param1, this.\u0023\u003Dz_iIh83yfe01U());
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param2)
    {
      return (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.\u0023\u003Dzu2ObQ3hMALTN() || _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 ? 1 : (_param1.\u0023\u003Dz2J4l3QUGwZHE > _param2.\u0023\u003Dz2kO1mtG\u0024bEUM() ? 1 : 0)) == 0;
    }

    protected override void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      ref double _param2,
      ref double _param3,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param4)
    {
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + _param2;
      double d2 = _param1.\u0023\u003Dz2J4l3QUGwZHE + _param3;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.\u0023\u003Dzu2ObQ3hMALTN()) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.\u0023\u003Dz2kO1mtG\u0024bEUM()))
      {
        double num1 = double.IsNaN(d1) ? 0.0 : d1;
        double num2 = double.IsNaN(d2) ? 0.0 : d2;
        if (num1 < 0.0)
          _param2 -= num1;
        if (num1 > _param4.\u0023\u003Dzu2ObQ3hMALTN())
          _param2 -= num1 - (_param4.\u0023\u003Dzu2ObQ3hMALTN() - 1.0);
        if (num2 < 0.0)
          _param3 -= num2;
        if (num2 > _param4.\u0023\u003Dz2kO1mtG\u0024bEUM())
          _param3 -= num2 - (_param4.\u0023\u003Dz2kO1mtG\u0024bEUM() - 1.0);
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += _param2;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += _param3;
      IComparable[] comparableArray = this.\u0023\u003DzvQ1aszE\u003D(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray[0]);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.Y1Property, (object) comparableArray[1]);
    }
  }

  internal sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(CustomAnnotation _param1) : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<CustomAnnotation>(_param1)
  {
    protected override bool \u0023\u003DzRe9EEbV7q4ey(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      Size _param2)
    {
      return (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.Width || _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 ? 1 : (_param1.\u0023\u003Dz2J4l3QUGwZHE > _param2.Height ? 1 : 0)) == 0;
    }

    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      CustomAnnotation.PlaceCustomAnnotation(this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1), this.\u0023\u003Dz_iIh83yfe01U());
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return CustomAnnotation.CalculateBasePoints(this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1), this.\u0023\u003Dz_iIh83yfe01U());
    }

    protected override void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      Point _param2,
      Point _param3,
      Size _param4)
    {
      double x = _param2.X;
      double y = _param2.Y;
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + x;
      double d2 = _param1.\u0023\u003Dz2J4l3QUGwZHE + y;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.Width) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.Height))
      {
        double num1 = double.IsNaN(d1) ? 0.0 : d1;
        double num2 = double.IsNaN(d2) ? 0.0 : d2;
        if (num1 < 0.0)
          x -= num1;
        if (num1 > _param4.Width)
          x -= num1 - (_param4.Width - 1.0);
        if (num2 < 0.0)
          y -= num2;
        if (num2 > _param4.Height)
          y -= num2 - (_param4.Height - 1.0);
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += x;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += y;
      IComparable[] comparableArray = this.\u0023\u003DzvQ1aszE\u003D(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray[0]);
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.Y1Property, (object) comparableArray[1]);
    }
  }
}
