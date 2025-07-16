// Decompiled with JetBrains decompiler
// Type: -.ModifierAxisCanvas
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Charting;

public class ModifierAxisCanvas : 
  AxisCanvas,
  IAnnotationCanvas,
  IHitTestable
{
  
  private AxisBase \u0023\u003Dz_HMHLf7iUOmptZScZA\u003D\u003D;

  public AxisBase \u0023\u003DzHZDgUSdfqmkx()
  {
    return this.\u0023\u003Dz_HMHLf7iUOmptZScZA\u003D\u003D;
  }

  public void \u0023\u003DzkF76BMTQOROh(
    AxisBase _param1)
  {
    this.\u0023\u003Dz_HMHLf7iUOmptZScZA\u003D\u003D = _param1;
  }

  public Point TranslatePoint(
    Point _param1,
    IHitTestable _param2)
  {
    return this.TranslatePoint(_param1, _param2);
  }

  public bool IsPointWithinBounds(Point _param1) => this.IsPointWithinBounds(_param1);

  public Rect GetBoundsRelativeTo(
    IHitTestable _param1)
  {
    return this.GetBoundsRelativeTo(_param1);
  }

  UIElementCollection IAnnotationCanvas.\u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxEd_WVrGrYRkgPcNsetxgTjiPcBUJA\u003D\u003D()
  {
    return this.Children;
  }

  double IHitTestable.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double IHitTestable.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }
}
