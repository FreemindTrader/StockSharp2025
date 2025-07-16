// Decompiled with JetBrains decompiler
// Type: -.PolarTickLabelAxisCanvas
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class PolarTickLabelAxisCanvas : 
  TickLabelAxisCanvas
{
  
  public static readonly DependencyProperty \u0023\u003Dzi25EO4Vacs4x = DependencyProperty.Register(nameof (MaxChildSize), typeof (double), typeof (PolarTickLabelAxisCanvas), new PropertyMetadata((object) 0.0));
  
  private \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l \u0023\u003DzvSLoDw0u_VlbzZTKzvTcOWMRVcRiYDKvgQ\u003D\u003D;
  
  private double \u0023\u003DzYklHyyiv14LN;

  public double MaxChildSize
  {
    get
    {
      return (double) this.GetValue(PolarTickLabelAxisCanvas.\u0023\u003Dzi25EO4Vacs4x);
    }
    set
    {
      this.SetValue(PolarTickLabelAxisCanvas.\u0023\u003Dzi25EO4Vacs4x, (object) value);
    }
  }

  protected override Size MeasureOverride(Size _param1)
  {
    Size size = base.MeasureOverride(_param1);
    this.MaxChildSize = this.\u0023\u003DzkKroB5ilL7RL();
    return size;
  }

  private double \u0023\u003DzkKroB5ilL7RL()
  {
    return this.Children.OfType<UIElement>().Select<UIElement, double>(PolarTickLabelAxisCanvas.SomeClass34343383.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D ?? (PolarTickLabelAxisCanvas.SomeClass34343383.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D = new Func<UIElement, double>(PolarTickLabelAxisCanvas.SomeClass34343383.SomeMethond0343.\u0023\u003Dzp9tEXoUc\u0024I1ve1Eq\u0024dbvW4E\u003D))).\u0023\u003DzAAksTMXIKE7d<double>().GetValueOrDefault();
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    this.\u0023\u003DzvSLoDw0u_VlbzZTKzvTcOWMRVcRiYDKvgQ\u003D\u003D = new \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l(_param1.Width, _param1.Height);
    this.\u0023\u003DzYklHyyiv14LN = \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param1);
    return base.ArrangeOverride(_param1);
  }

  protected override Rect \u0023\u003Dzzigj\u0024danccVcsgdXBQ\u003D\u003D(
    Size _param1,
    UIElement _param2)
  {
    Rect rect = Rect.Empty;
    if (_param2 is DefaultTickLabel y9QajdhH6H6U9EEjd)
    {
      double x = y9QajdhH6H6U9EEjd.Position.X;
      double num1 = this.\u0023\u003DzYklHyyiv14LN - this.MaxChildSize / 2.0;
      Point point = this.\u0023\u003DzvSLoDw0u_VlbzZTKzvTcOWMRVcRiYDKvgQ\u003D\u003D.\u0023\u003DztSU4xSLxm6xoY2ZEZw\u003D\u003D(x, num1);
      double num2 = point.X - _param2.DesiredSize.Width / 2.0;
      double num3 = point.Y - _param2.DesiredSize.Height / 2.0;
      RotateTransform rotateTransform = new RotateTransform()
      {
        Angle = x + 90.0,
        CenterX = 0.5,
        CenterY = 0.5
      };
      _param2.RenderTransform = (Transform) rotateTransform;
      _param2.RenderTransformOrigin = new Point(0.5, 0.5);
      rect = new Rect(new Point(num2, num3), _param2.DesiredSize);
    }
    return rect;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly PolarTickLabelAxisCanvas.SomeClass34343383 SomeMethond0343 = new PolarTickLabelAxisCanvas.SomeClass34343383();
    public static Func<UIElement, double> \u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D;

    internal double \u0023\u003Dzp9tEXoUc\u0024I1ve1Eq\u0024dbvW4E\u003D(UIElement _param1)
    {
      return _param1.DesiredSize.Height;
    }
  }
}
