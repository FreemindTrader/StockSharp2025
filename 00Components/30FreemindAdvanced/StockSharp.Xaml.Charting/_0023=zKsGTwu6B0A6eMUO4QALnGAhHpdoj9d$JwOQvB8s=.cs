// Decompiled with JetBrains decompiler
// Type: #=zKsGTwu6B0A6eMUO4QALnGAhHpdoj9d$JwOQvB8s=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
internal static class \u0023\u003DzKsGTwu6B0A6eMUO4QALnGAhHpdoj9d\u0024JwOQvB8s\u003D
{
  internal static void \u0023\u003DzH0osWQkV_Y8_(this Panel _param0, object _param1, int _param2)
  {
    FrameworkElement element = _param1 as FrameworkElement;
    if (_param0 == null || element == null || _param0 == element || _param0.Children.Contains((UIElement) element))
      return;
    if (element.Parent is Panel parent && parent.Children.Contains((UIElement) element))
      parent.Children.Remove((UIElement) element);
    if (_param2 >= 0 && _param2 < _param0.Children.Count)
      _param0.Children.Insert(_param2, (UIElement) element);
    else
      _param0.Children.Add((UIElement) element);
  }

  internal static void \u0023\u003DzH0osWQkV_Y8_(
    this \u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D _param0,
    object _param1,
    int _param2)
  {
    (_param0 as Panel).\u0023\u003DzH0osWQkV_Y8_(_param1, _param2);
  }

  internal static void \u0023\u003DzH0osWQkV_Y8_(
    this \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param0,
    object _param1,
    int _param2)
  {
    (_param0 as Panel).\u0023\u003DzH0osWQkV_Y8_(_param1, _param2);
  }

  internal static void \u0023\u003DziYdJ\u00246cCiBha(this Panel _param0, object _param1)
  {
    if (_param0 == null || !(_param1 is UIElement element) || !_param0.Children.Contains(element))
      return;
    _param0.Children.Remove(element);
  }

  internal static void \u0023\u003DziYdJ\u00246cCiBha(
    this \u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D _param0,
    object _param1)
  {
    (_param0 as Panel).\u0023\u003DziYdJ\u00246cCiBha(_param1);
  }

  internal static void \u0023\u003DziYdJ\u00246cCiBha(
    this \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param0,
    object _param1)
  {
    (_param0 as Panel).\u0023\u003DziYdJ\u00246cCiBha(_param1);
  }

  internal static void \u0023\u003Dzk8_eoWQ\u003D(
    this Panel _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Brush _param5,
    double _param6)
  {
    Line line = new Line();
    line.Stroke = _param5;
    line.StrokeThickness = _param6;
    line.X1 = (double) _param1;
    line.X2 = (double) _param3;
    line.Y1 = (double) _param2;
    line.Y2 = (double) _param4;
    Line element = line;
    _param0.Children.Add((UIElement) element);
  }
}
