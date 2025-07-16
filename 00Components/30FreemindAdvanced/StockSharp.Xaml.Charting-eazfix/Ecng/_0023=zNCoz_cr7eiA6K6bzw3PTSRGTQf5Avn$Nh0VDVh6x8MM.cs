// Decompiled with JetBrains decompiler
// Type: #=zNCoz_cr7eiA6K6bzw3PTSRGTQf5Avn$Nh0VDVh6x8MMDygc2mw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
public sealed class \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRGTQf5Avn\u0024Nh0VDVh6x8MMDygc2mw\u003D\u003D : 
  IDisposable,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024,
  \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX
{
  
  private readonly IRenderContext2D \u0023\u003DzVxwXLcXPtvCC;
  
  private readonly \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT \u0023\u003DzE7bFkpMkNiWA;
  
  private \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzkbpZHOIN98su;
  
  private IBrush2D \u0023\u003Dznk36UXXVVS0u;

  public \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRGTQf5Avn\u0024Nh0VDVh6x8MMDygc2mw\u003D\u003D(
    IRenderContext2D _param1,
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT _param2)
  {
    this.\u0023\u003DzVxwXLcXPtvCC = _param1;
    this.\u0023\u003DzE7bFkpMkNiWA = _param2;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
    IPathColor _param1,
    double _param2,
    double _param3)
  {
    if (_param1 != null)
    {
      this.\u0023\u003DzkbpZHOIN98su = (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) _param1;
      this.\u0023\u003Dznk36UXXVVS0u = this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dze8WyDhI\u003D(_param1.Color, 1.0, new bool?());
    }
    else
    {
      this.\u0023\u003DzkbpZHOIN98su = (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) null;
      this.\u0023\u003Dznk36UXXVVS0u = (IBrush2D) null;
    }
    this.\u0023\u003DzE7bFkpMkNiWA.\u0023\u003Dz7ZSU06M\u003D(this.\u0023\u003DzVxwXLcXPtvCC, this.\u0023\u003DzkbpZHOIN98su, this.\u0023\u003Dznk36UXXVVS0u);
    this.\u0023\u003DzE7bFkpMkNiWA.Draw(this.\u0023\u003DzVxwXLcXPtvCC, _param2, _param3, this.\u0023\u003DzkbpZHOIN98su, this.\u0023\u003Dznk36UXXVVS0u);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzE7bFkpMkNiWA.Draw(this.\u0023\u003DzVxwXLcXPtvCC, _param1, _param2, this.\u0023\u003DzkbpZHOIN98su, this.\u0023\u003Dznk36UXXVVS0u);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public void \u0023\u003DzBNsE20w\u003D()
  {
    this.\u0023\u003DzE7bFkpMkNiWA.\u0023\u003DzBNsE20w\u003D(this.\u0023\u003DzVxwXLcXPtvCC);
    this.\u0023\u003Dznk36UXXVVS0u = (IBrush2D) null;
    this.\u0023\u003DzkbpZHOIN98su = (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) null;
  }

  void IDisposable.Dispose()
  {
    this.\u0023\u003DzBNsE20w\u003D();
  }
}
