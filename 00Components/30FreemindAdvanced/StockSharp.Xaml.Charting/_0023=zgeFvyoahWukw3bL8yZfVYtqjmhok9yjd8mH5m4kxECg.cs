// Decompiled with JetBrains decompiler
// Type: #=zgeFvyoahWukw3bL8yZfVYtqjmhok9yjd8mH5m4kxECgD_8hOTrQuVzBMlu2MMYB2IQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Windows;
using System.Windows.Shapes;

#nullable disable
internal sealed class \u0023\u003DzgeFvyoahWukw3bL8yZfVYtqjmhok9yjd8mH5m4kxECgD_8hOTrQuVzBMlu2MMYB2IQ\u003D\u003D : 
  \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI84SlzGeHM63aaGG8vJyxtBAVakvxTUthBw\u003D
{
  private readonly \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D \u0023\u003Dz0mppXFo\u003D;

  public \u0023\u003DzgeFvyoahWukw3bL8yZfVYtqjmhok9yjd8mH5m4kxECgD_8hOTrQuVzBMlu2MMYB2IQ\u003D\u003D(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    this.\u0023\u003Dz0mppXFo\u003D = _param1;
  }

  public Line \u0023\u003Dz7ftWcP9VIwGB(Point _param1, bool _param2)
  {
    Line line = (Line) null;
    if (_param1.Y.\u0023\u003DzutrFxOU\u003D() & _param2 || _param1.X.\u0023\u003DzutrFxOU\u003D() && !_param2)
    {
      line = new Line();
      if (_param2)
      {
        line.X1 = 0.0;
        line.X2 = this.\u0023\u003Dz0mppXFo\u003D.ModifierSurface.\u0023\u003Dzu2ObQ3hMALTN();
        line.Y1 = _param1.Y;
        line.Y2 = _param1.Y;
      }
      else
      {
        line.X1 = _param1.X;
        line.X2 = _param1.X;
        line.Y1 = 0.0;
        line.Y2 = this.\u0023\u003Dz0mppXFo\u003D.ModifierSurface.\u0023\u003Dz2kO1mtG\u0024bEUM();
      }
    }
    return line;
  }
}
