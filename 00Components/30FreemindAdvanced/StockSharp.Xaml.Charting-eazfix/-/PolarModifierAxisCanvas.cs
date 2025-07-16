// Decompiled with JetBrains decompiler
// Type: -.PolarModifierAxisCanvas
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class PolarModifierAxisCanvas : 
  ModifierAxisCanvas
{
  
  private \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l \u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb;
  
  private double \u0023\u003DzKpDdtddDPCNA;
  
  private double \u0023\u003Dzskk3W82lFCC7;

  protected override Size ArrangeOverride(Size _param1)
  {
    this.\u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb = new \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l(_param1.Width, _param1.Height);
    double thickness = PolarPanel.GetThickness((UIElement) this.\u0023\u003DzHZDgUSdfqmkx());
    this.\u0023\u003DzKpDdtddDPCNA = \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param1);
    this.\u0023\u003Dzskk3W82lFCC7 = this.\u0023\u003DzKpDdtddDPCNA - thickness;
    return base.ArrangeOverride(_param1);
  }

  protected override Rect \u0023\u003Dzzigj\u0024danccVcsgdXBQ\u003D\u003D(
    Size _param1,
    UIElement _param2)
  {
    double num1;
    double num2 = PolarModifierAxisCanvas.\u0023\u003Dz4J4V2YD7QY\u00243(_param2, out num1);
    double num3;
    double num4 = this.\u0023\u003DztSobte99GK8x(_param2, out num3);
    Point point = this.\u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb.\u0023\u003DztSU4xSLxm6xoY2ZEZw\u003D\u003D(num2, num4);
    RotateTransform rotateTransform = new RotateTransform()
    {
      Angle = num2 + 90.0,
      CenterX = num1,
      CenterY = num3
    };
    _param2.RenderTransform = (Transform) rotateTransform;
    _param2.RenderTransformOrigin = new Point(num1, num3);
    point.X -= _param2.DesiredSize.Width * num1;
    point.Y -= _param2.DesiredSize.Height * num3;
    return new Rect(point, _param2.DesiredSize);
  }

  private double \u0023\u003DztSobte99GK8x(UIElement _param1, out double _param2)
  {
    double num = 0.0;
    _param2 = 0.0;
    double top = AxisCanvas.GetTop(_param1);
    double centerTop = AxisCanvas.GetCenterTop(_param1);
    if (!top.IsNaN())
    {
      num = this.\u0023\u003DzKpDdtddDPCNA - top;
      _param2 = 0.0;
    }
    else if (!centerTop.IsNaN())
    {
      num = this.\u0023\u003DzKpDdtddDPCNA - centerTop;
      _param2 = 0.5;
    }
    else
    {
      double bottom = AxisCanvas.GetBottom(_param1);
      if (!bottom.IsNaN())
      {
        num = this.\u0023\u003Dzskk3W82lFCC7 + bottom;
        _param2 = 1.0;
      }
      else
      {
        double centerBottom = AxisCanvas.GetCenterBottom(_param1);
        if (!centerBottom.IsNaN())
        {
          num = this.\u0023\u003Dzskk3W82lFCC7 + centerBottom;
          _param2 = 0.5;
        }
      }
    }
    return num;
  }

  private static double \u0023\u003Dz4J4V2YD7QY\u00243(UIElement _param0, out double _param1)
  {
    double num = 0.0;
    _param1 = 0.0;
    double left = AxisCanvas.GetLeft(_param0);
    double centerLeft = AxisCanvas.GetCenterLeft(_param0);
    if (!left.IsNaN())
    {
      num = left;
      _param1 = 0.0;
    }
    else if (!centerLeft.IsNaN())
    {
      num = centerLeft;
      _param1 = 0.5;
    }
    else
    {
      double right = AxisCanvas.GetRight(_param0);
      if (!right.IsNaN())
      {
        num = 360.0 - right;
        _param1 = 1.0;
      }
      else
      {
        double centerRight = AxisCanvas.GetCenterRight(_param0);
        if (!centerRight.IsNaN())
        {
          num = 360.0 - centerRight;
          _param1 = 0.5;
        }
      }
    }
    return num;
  }
}
