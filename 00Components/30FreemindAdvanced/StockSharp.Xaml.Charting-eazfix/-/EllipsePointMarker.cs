// Decompiled with JetBrains decompiler
// Type: -.EllipsePointMarker
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace SciChart.Charting;

internal sealed class EllipsePointMarker : 
  BasePointMarker
{
  
  private float \u0023\u003DzxL_Q2Yk\u003D;
  
  private float _height;

  protected override void \u0023\u003DzdL613chSNlLB(
    IRenderContext2D _param1,
    IEnumerable<Point> _param2,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param3,
    IBrush2D _param4)
  {
    _param1.\u0023\u003DzoUffnI1MQnWod0s9Xg\u003D\u003D(_param3, _param4, _param2, (double) this.\u0023\u003DzxL_Q2Yk\u003D, (double) this._height);
  }

  protected override void \u0023\u003DzdL613chSNlLB(
    IRenderContext2D _param1,
    double _param2,
    double _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    IBrush2D _param5)
  {
    _param1.\u0023\u003DzIZCdW2WR6Rxw(_param4, _param5, new Point(_param2, _param3), (double) this.\u0023\u003DzxL_Q2Yk\u003D, (double) this._height);
  }

  protected override void OnPropertyChanged(
    DependencyObject _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.\u0023\u003DzxL_Q2Yk\u003D = (float) this.Width;
    this._height = (float) this.Height;
    base.OnPropertyChanged(_param1, _param2);
  }
}
