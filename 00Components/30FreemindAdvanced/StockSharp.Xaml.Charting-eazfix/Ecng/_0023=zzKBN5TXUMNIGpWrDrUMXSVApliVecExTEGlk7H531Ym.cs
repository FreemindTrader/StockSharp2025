// Decompiled with JetBrains decompiler
// Type: #=zzKBN5TXUMNIGpWrDrUMXSVApliVecExTEGlk7H531YmTGRKDgfgGbW31OoVL8yjU4vPp5Cc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Collections.Generic;
using System.Windows;

#nullable disable
public sealed class \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSVApliVecExTEGlk7H531YmTGRKDgfgGbW31OoVL8yjU4vPp5Cc\u003D : 
  \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ
{
  private readonly IRenderContext2D \u0023\u003DzVxwXLcXPtvCC;

  public \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSVApliVecExTEGlk7H531YmTGRKDgfgGbW31OoVL8yjU4vPp5Cc\u003D(
    IRenderContext2D _param1)
  {
    this.\u0023\u003DzVxwXLcXPtvCC = _param1;
  }

  public void \u0023\u003Dz7zUbWtTKc3tA(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    Point _param2,
    Point _param3)
  {
    this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dz7zUbWtTKc3tA(_param1, _param2, _param3);
  }

  public void \u0023\u003DzkpjYNfwbvIK8(
    Point _param1,
    Point _param2,
    IBrush2D _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    double _param5)
  {
    this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003DzVRUUvzhAr5SR(_param3, _param1, _param2, _param5);
    if ((double) _param4.StrokeThickness <= 0.0 || _param4.Color.A == (byte) 0)
      return;
    this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dz7zUbWtTKc3tA(_param4, _param1, _param2);
  }

  public void \u0023\u003Dzk8_eoWQ\u003D(
    Point _param1,
    Point _param2,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param3)
  {
    if ((double) _param3.StrokeThickness <= 0.0 || _param3.Color.A == (byte) 0)
      return;
    this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dzk8_eoWQ\u003D(_param3, _param1, _param2);
  }

  public void \u0023\u003Dz_I15ZX7u91\u0024T(
    IBrush2D _param1,
    Point[] _param2)
  {
    this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dz_I15ZX7u91\u0024T(_param1, (IEnumerable<Point>) _param2);
  }

  public void \u0023\u003DzNq_YOflx6uAn(
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT _param1,
    Point _param2,
    IBrush2D _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4)
  {
    _param1.Draw(this.\u0023\u003DzVxwXLcXPtvCC, _param2.X, _param2.Y, _param4, _param3);
  }

  public void \u0023\u003DzzNCP093OQhtA(
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT _param1,
    IEnumerable<Point> _param2)
  {
    _param1.Draw(this.\u0023\u003DzVxwXLcXPtvCC, _param2);
  }
}
