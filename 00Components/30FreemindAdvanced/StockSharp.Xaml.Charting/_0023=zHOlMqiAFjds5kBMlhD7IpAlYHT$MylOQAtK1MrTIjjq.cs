// Decompiled with JetBrains decompiler
// Type: #=zHOlMqiAFjds5kBMlhD7IpAlYHT$MylOQAtK1MrTIjjq_96VfnOFGV3c=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAlYHT\u0024MylOQAtK1MrTIjjq_96VfnOFGV3c\u003D : 
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024,
  IDisposable
{
  
  private \u0023\u003Dzo13esGCwfQJn\u0024h2kOXY\u0024_bD_Pyrb\u0024d0P2noEI5c\u003D \u0023\u003DzQWU4skw9ivrJ;
  
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX \u0023\u003DzEcmsYfw\u003D;
  
  private readonly Rect \u0023\u003DzyU_ksG\u0024ZRcLw;
  
  private double \u0023\u003DzFEDR40ugZMK3;
  
  private double \u0023\u003DzGcDWpYNQwUmC;
  
  private \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzrZyFxk8\u003D;
  
  private Size \u0023\u003DzgYZhPyPIW8zq;

  public \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAlYHT\u0024MylOQAtK1MrTIjjq_96VfnOFGV3c\u003D(
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param1,
    Size _param2)
  {
    this.\u0023\u003DzEcmsYfw\u003D = _param1;
    this.\u0023\u003DzyU_ksG\u0024ZRcLw = new Rect(new Point(0.0, 0.0), _param2);
    this.\u0023\u003DzgYZhPyPIW8zq = _param2;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
    \u0023\u003Dzo13esGCwfQJn\u0024h2kOXY\u0024_bD_Pyrb\u0024d0P2noEI5c\u003D _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003DzQWU4skw9ivrJ = _param1;
    this.\u0023\u003DzFEDR40ugZMK3 = _param2;
    this.\u0023\u003DzGcDWpYNQwUmC = _param3;
    this.\u0023\u003DzrZyFxk8\u003D = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
    double _param1,
    double _param2)
  {
    bool flag = \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAlYHT\u0024MylOQAtK1MrTIjjq_96VfnOFGV3c\u003D.\u0023\u003DzxGhbraO0gg9\u0024(_param1, _param2, this.\u0023\u003DzgYZhPyPIW8zq);
    if (this.\u0023\u003DzrZyFxk8\u003D != null)
    {
      if (flag)
      {
        this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzfRDRUq8\u003D(_param1, _param2);
      }
      else
      {
        double zFedR40ugZmK3 = this.\u0023\u003DzFEDR40ugZMK3;
        double zGcDwpYnQwUmC = this.\u0023\u003DzGcDWpYNQwUmC;
        double num1 = _param1;
        double num2 = _param2;
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(this.\u0023\u003DzyU_ksG\u0024ZRcLw, ref zFedR40ugZmK3, ref zGcDwpYnQwUmC, ref num1, ref num2);
        this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzfRDRUq8\u003D(num1, num2);
        this.\u0023\u003DzBNsE20w\u003D();
      }
    }
    else
    {
      double zFedR40ugZmK3 = this.\u0023\u003DzFEDR40ugZMK3;
      double zGcDwpYnQwUmC = this.\u0023\u003DzGcDWpYNQwUmC;
      double num3 = _param1;
      double num4 = _param2;
      if (flag)
      {
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(this.\u0023\u003DzyU_ksG\u0024ZRcLw, ref zFedR40ugZmK3, ref zGcDwpYnQwUmC, ref num3, ref num4);
        this.\u0023\u003DzrZyFxk8\u003D = this.\u0023\u003DzEcmsYfw\u003D.\u0023\u003Dz7ZSU06M\u003D(this.\u0023\u003DzQWU4skw9ivrJ, zFedR40ugZmK3, zGcDwpYnQwUmC);
        this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzfRDRUq8\u003D(num3, num4);
      }
      else if (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(this.\u0023\u003DzyU_ksG\u0024ZRcLw, ref zFedR40ugZmK3, ref zGcDwpYnQwUmC, ref num3, ref num4))
      {
        this.\u0023\u003DzrZyFxk8\u003D = this.\u0023\u003DzEcmsYfw\u003D.\u0023\u003Dz7ZSU06M\u003D(this.\u0023\u003DzQWU4skw9ivrJ, zFedR40ugZmK3, zGcDwpYnQwUmC);
        this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzfRDRUq8\u003D(num3, num4);
        this.\u0023\u003DzBNsE20w\u003D();
      }
    }
    this.\u0023\u003DzFEDR40ugZMK3 = _param1;
    this.\u0023\u003DzGcDWpYNQwUmC = _param2;
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public void \u0023\u003DzBNsE20w\u003D()
  {
    if (this.\u0023\u003DzrZyFxk8\u003D == null)
      return;
    this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzBNsE20w\u003D();
    this.\u0023\u003DzrZyFxk8\u003D = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
  }

  public void Dispose() => this.\u0023\u003DzBNsE20w\u003D();

  private static bool \u0023\u003DzxGhbraO0gg9\u0024(double _param0, double _param1, Size _param2)
  {
    return _param0 >= 0.0 && _param0 < _param2.Width && _param1 >= 0.0 && _param1 < _param2.Height;
  }
}
