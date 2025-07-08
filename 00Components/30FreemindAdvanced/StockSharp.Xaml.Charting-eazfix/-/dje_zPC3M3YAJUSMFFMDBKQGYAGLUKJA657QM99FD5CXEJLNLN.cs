// Decompiled with JetBrains decompiler
// Type: -.dje_zPC3M3YAJUSMFFMDBKQGYAGLUKJA657QM99FD5CXEJLNLNQCGTL4JQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace \u002D;

internal sealed class dje_zPC3M3YAJUSMFFMDBKQGYAGLUKJA657QM99FD5CXEJLNLNQCGTL4JQ_ejd : 
  dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd
{
  
  private dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd \u0023\u003DzPe8_oE9VyZOl41cxBA\u003D\u003D;
  
  private double \u0023\u003DzYklHyyiv14LN;
  
  private \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok \u0023\u003DzqHa24tYbLx0LAtSkPnzT2rI\u003D;
  
  private float \u0023\u003DzYo5GhqlKs7_nIGPcXw\u003D\u003D;
  
  private \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l \u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb;

  protected override Size MeasureOverride(Size _param1)
  {
    this.\u0023\u003DzypPX\u0024di_k9nd(this.AddLabels);
    this.\u0023\u003DzPe8_oE9VyZOl41cxBA\u003D\u003D = this.Children[0] as dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd;
    this.\u0023\u003DzPe8_oE9VyZOl41cxBA\u003D\u003D.Measure(_param1);
    foreach (UIElement child in this.\u0023\u003DzPe8_oE9VyZOl41cxBA\u003D\u003D.Children)
    {
      if (child is Image image)
      {
        this.\u0023\u003DzlbfaVnpq6N5Y = image;
        this.\u0023\u003DzlbfaVnpq6N5Y.SizeChanged -= new SizeChangedEventHandler(this.\u0023\u003Dz7L8_DVjjYCLl);
        this.\u0023\u003DzlbfaVnpq6N5Y.SizeChanged += new SizeChangedEventHandler(this.\u0023\u003Dz7L8_DVjjYCLl);
      }
      if (child is Grid grid)
        this.\u0023\u003DzDLF9sbNKzh9k = grid;
    }
    return this.\u0023\u003DzPe8_oE9VyZOl41cxBA\u003D\u003D.DesiredSize;
  }

  private void \u0023\u003Dz7L8_DVjjYCLl(object _param1, SizeChangedEventArgs _param2)
  {
    this.\u0023\u003DzuTwCwl07R0Mf(this.\u0023\u003DzqHa24tYbLx0LAtSkPnzT2rI\u003D, this.\u0023\u003DzYo5GhqlKs7_nIGPcXw\u003D\u003D);
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    this.\u0023\u003DzPe8_oE9VyZOl41cxBA\u003D\u003D.Arrange(new Rect(new Point(0.0, 0.0), _param1));
    return _param1;
  }

  public override void \u0023\u003DzuTwCwl07R0Mf(
    \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok _param1,
    float _param2)
  {
    base.\u0023\u003DzuTwCwl07R0Mf(_param1, _param2);
    this.\u0023\u003DzqHa24tYbLx0LAtSkPnzT2rI\u003D = _param1;
    this.\u0023\u003DzYo5GhqlKs7_nIGPcXw\u003D\u003D = _param2;
  }

  protected override void \u0023\u003DzuTwCwl07R0Mf(
    IRenderContext2D _param1,
    Style _param2,
    double _param3,
    float[] _param4,
    float _param5)
  {
    this.\u0023\u003DzeEl93ifUiK4P.Style = _param2;
    dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.SetTheme((DependencyObject) this.\u0023\u003DzeEl93ifUiK4P, dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.GetTheme((DependencyObject) this));
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzQCf7bpfi0DqGMauSow\u003D\u003D(this.\u0023\u003DzeEl93ifUiK4P, true))
    {
      foreach (float num in _param4)
        this.\u0023\u003Dz3ZXzqAn7QFK6(_param1, rhwYsZxA33iRu6Id7J, num, _param3);
    }
  }

  private void \u0023\u003Dz3ZXzqAn7QFK6(
    IRenderContext2D _param1,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param2,
    float _param3,
    double _param4)
  {
    double num = this.\u0023\u003DzYklHyyiv14LN - this.MajorTickSize;
    Point point1 = this.\u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb.\u0023\u003DztSU4xSLxm6xoY2ZEZw\u003D\u003D((double) _param3, num);
    Point point2 = this.\u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb.\u0023\u003DztSU4xSLxm6xoY2ZEZw\u003D\u003D((double) _param3, num + _param4);
    _param1.\u0023\u003Dzk8_eoWQ\u003D(_param2, point1, point2);
  }

  protected override Size \u0023\u003DzR5BsR9jdiJQz()
  {
    Size renderSize = this.\u0023\u003DzdjbuiJBxjZ2I().RenderSize;
    this.\u0023\u003DziAttZBhsZA0vFhELFvwoTIOB_Klb = new \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l(renderSize.Width, renderSize.Height);
    this.\u0023\u003DzYklHyyiv14LN = \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(renderSize);
    return renderSize;
  }
}
