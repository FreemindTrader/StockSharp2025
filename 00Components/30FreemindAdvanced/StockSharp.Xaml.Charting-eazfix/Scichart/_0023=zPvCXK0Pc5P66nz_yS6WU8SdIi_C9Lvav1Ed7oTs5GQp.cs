// Decompiled with JetBrains decompiler
// Type: #=zPvCXK0Pc5P66nz_yS6WU8SdIi_C9Lvav1Ed7oTs5GQpxXEw4iX9XQzY=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003DzPvCXK0Pc5P66nz_yS6WU8SdIi_C9Lvav1Ed7oTs5GQpxXEw4iX9XQzY\u003D : 
  IDisposable,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024
{
  
  private readonly List<Point> \u0023\u003DzYw05nwk\u003D;
  
  private IPathColor \u0023\u003DzQWU4skw9ivrJ;
  
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX \u0023\u003DzEcmsYfw\u003D;
  
  private readonly Size \u0023\u003DzgYZhPyPIW8zq;

  public \u0023\u003DzPvCXK0Pc5P66nz_yS6WU8SdIi_C9Lvav1Ed7oTs5GQpxXEw4iX9XQzY\u003D(
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param1,
    Size _param2)
  {
    this.\u0023\u003DzEcmsYfw\u003D = _param1;
    this.\u0023\u003DzgYZhPyPIW8zq = _param2;
    this.\u0023\u003DzYw05nwk\u003D = new List<Point>();
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
    IPathColor _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003DzYw05nwk\u003D.Add(new Point(_param2, _param3));
    this.\u0023\u003DzQWU4skw9ivrJ = _param1;
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzYw05nwk\u003D.Add(new Point(_param1, _param2));
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public void \u0023\u003DzBNsE20w\u003D()
  {
    if (this.\u0023\u003DzYw05nwk\u003D.Count <= 0)
      return;
    \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003DzzNCP093OQhtA(\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzUIfyPrQYBsuR((IEnumerable<Point>) this.\u0023\u003DzYw05nwk\u003D, this.\u0023\u003DzgYZhPyPIW8zq, 0, 0), this.\u0023\u003DzEcmsYfw\u003D, this.\u0023\u003DzQWU4skw9ivrJ);
  }

  public void Dispose() => this.\u0023\u003DzBNsE20w\u003D();
}
