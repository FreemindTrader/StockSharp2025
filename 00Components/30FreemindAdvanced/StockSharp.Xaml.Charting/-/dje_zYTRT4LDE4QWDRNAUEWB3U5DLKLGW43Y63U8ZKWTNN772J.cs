// Decompiled with JetBrains decompiler
// Type: -.dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd : 
  dje_zATMENTVRQXPESZUNTMAKHBSKCWZW7V9SFA5P9LBGCE4BQBNQAPN2D_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzi25EO4Vacs4x = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539335273), typeof (double), typeof (dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd), new PropertyMetadata((object) 0.0));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l \u0023\u003DzvSLoDw0u_VlbzZTKzvTcOWMRVcRiYDKvgQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzYklHyyiv14LN;

  public double MaxChildSize
  {
    get
    {
      return (double) this.GetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd.\u0023\u003Dzi25EO4Vacs4x);
    }
    set
    {
      this.SetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd.\u0023\u003Dzi25EO4Vacs4x, (object) value);
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
    return this.Children.OfType<UIElement>().Select<UIElement, double>(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D ?? (dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D = new Func<UIElement, double>(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzp9tEXoUc\u0024I1ve1Eq\u0024dbvW4E\u003D))).\u0023\u003DzAAksTMXIKE7d<double>().GetValueOrDefault();
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
    if (_param2 is dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd y9QajdhH6H6U9EEjd)
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
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zYTRT4LDE4QWDRNAUEWB3U5DLKLGW43Y63U8ZKWTNN772J5S54KNPDFXK4R5Q_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<UIElement, double> \u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D;

    internal double \u0023\u003Dzp9tEXoUc\u0024I1ve1Eq\u0024dbvW4E\u003D(UIElement _param1)
    {
      return _param1.DesiredSize.Height;
    }
  }
}
