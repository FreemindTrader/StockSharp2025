// Decompiled with JetBrains decompiler
// Type: #=zSseiGdgwJmJ1pkmz7CEFf1QlL8mnDNxhPAkqrwW5wckD
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
internal static class \u0023\u003DzSseiGdgwJmJ1pkmz7CEFf1QlL8mnDNxhPAkqrwW5wckD
{
  internal static \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzQCf7bpfi0DqGMauSow\u003D\u003D(
    this IRenderContext2D _param0,
    Line _param1,
    bool _param2)
  {
    double[] array = _param1.StrokeDashArray.ToArray<double>();
    Color color = new Color();
    if (_param1.Stroke is SolidColorBrush stroke)
      color = stroke.Color;
    float strokeThickness = (float) _param1.StrokeThickness;
    return _param0.\u0023\u003DzL3In9ls\u003D(color, _param2, strokeThickness, _param1.Opacity, array, _param1.StrokeEndLineCap);
  }
}
