// Decompiled with JetBrains decompiler
// Type: #=zTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

#nullable disable
internal interface \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT
{
  void Draw(
    IRenderContext2D _param1,
    IEnumerable<Point> _param2);

  void Draw(
    IRenderContext2D _param1,
    double _param2,
    double _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    IBrush2D _param5);

  Color Stroke { get; set; }

  Color Fill { get; set; }

  double Width { get; set; }

  double Height { get; set; }

  double StrokeThickness { get; set; }

  void \u0023\u003Dz7ZSU06M\u003D(
    IRenderContext2D _param1,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param2,
    IBrush2D _param3);

  void \u0023\u003DzBNsE20w\u003D(
    IRenderContext2D _param1);
}
