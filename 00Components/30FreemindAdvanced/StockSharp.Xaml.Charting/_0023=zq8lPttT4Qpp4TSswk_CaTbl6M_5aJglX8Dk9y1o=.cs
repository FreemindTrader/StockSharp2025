// Decompiled with JetBrains decompiler
// Type: #=zq8lPttT4Qpp4TSswk_CaTbl6M_5aJglX8Dk9y1o=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Windows.Media;

#nullable disable
internal static class \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTbl6M_5aJglX8Dk9y1o\u003D
{
  internal static bool \u0023\u003Dzprl3UX8\u003D(this Brush _param0)
  {
    if (_param0.Opacity == 0.0)
      return true;
    return _param0 is SolidColorBrush solidColorBrush && solidColorBrush.Color.A == (byte) 0;
  }

  internal static Color \u0023\u003DzTI4bfbI\u003D(this Brush _param0)
  {
    switch (_param0)
    {
      case SolidColorBrush solidColorBrush:
        return solidColorBrush.Color;
      case LinearGradientBrush linearGradientBrush:
        return linearGradientBrush.GradientStops[0].Color;
      default:
        return Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0);
    }
  }
}
