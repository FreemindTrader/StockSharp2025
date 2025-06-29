// Decompiled with JetBrains decompiler
// Type: -.dje_zUTTWYHVMUS8G93MS4LKZ375YU8GTCWJNKQWWN8HJ3ZVEDUZEZJK5A_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zUTTWYHVMUS8G93MS4LKZ375YU8GTCWJNKQWWN8HJ3ZVEDUZEZJK5A_ejd : 
  dje_zCCKWRE6ZAZTUZM9ZU5ZMRRYMRCBJV5TT9Z7CKVUJYFUMMKZ_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l \u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzKpDdtddDPCNA;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dzskk3W82lFCC7;

  protected override Size ArrangeOverride(Size _param1)
  {
    this.\u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb = new \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l(_param1.Width, _param1.Height);
    double thickness = dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.GetThickness((UIElement) this.\u0023\u003DzHZDgUSdfqmkx());
    this.\u0023\u003DzKpDdtddDPCNA = \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param1);
    this.\u0023\u003Dzskk3W82lFCC7 = this.\u0023\u003DzKpDdtddDPCNA - thickness;
    return base.ArrangeOverride(_param1);
  }

  protected override Rect \u0023\u003Dzzigj\u0024danccVcsgdXBQ\u003D\u003D(
    Size _param1,
    UIElement _param2)
  {
    double num1;
    double num2 = dje_zUTTWYHVMUS8G93MS4LKZ375YU8GTCWJNKQWWN8HJ3ZVEDUZEZJK5A_ejd.\u0023\u003Dz4J4V2YD7QY\u00243(_param2, out num1);
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
    double top = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetTop(_param1);
    double centerTop = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterTop(_param1);
    if (!top.\u0023\u003DzeNpB9guo_tur())
    {
      num = this.\u0023\u003DzKpDdtddDPCNA - top;
      _param2 = 0.0;
    }
    else if (!centerTop.\u0023\u003DzeNpB9guo_tur())
    {
      num = this.\u0023\u003DzKpDdtddDPCNA - centerTop;
      _param2 = 0.5;
    }
    else
    {
      double bottom = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetBottom(_param1);
      if (!bottom.\u0023\u003DzeNpB9guo_tur())
      {
        num = this.\u0023\u003Dzskk3W82lFCC7 + bottom;
        _param2 = 1.0;
      }
      else
      {
        double centerBottom = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterBottom(_param1);
        if (!centerBottom.\u0023\u003DzeNpB9guo_tur())
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
    double left = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetLeft(_param0);
    double centerLeft = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterLeft(_param0);
    if (!left.\u0023\u003DzeNpB9guo_tur())
    {
      num = left;
      _param1 = 0.0;
    }
    else if (!centerLeft.\u0023\u003DzeNpB9guo_tur())
    {
      num = centerLeft;
      _param1 = 0.5;
    }
    else
    {
      double right = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetRight(_param0);
      if (!right.\u0023\u003DzeNpB9guo_tur())
      {
        num = 360.0 - right;
        _param1 = 1.0;
      }
      else
      {
        double centerRight = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterRight(_param0);
        if (!centerRight.\u0023\u003DzeNpB9guo_tur())
        {
          num = 360.0 - centerRight;
          _param1 = 0.5;
        }
      }
    }
    return num;
  }
}
