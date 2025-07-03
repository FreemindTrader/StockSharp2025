// Decompiled with JetBrains decompiler
// Type: #=zGEJCq9O47NiPLstzce6jeG0gHHLN$x1lawsGi$H02uo2h0XchZjPEs1bmCfeGhythDHVWo26UKCe
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzGEJCq9O47NiPLstzce6jeG0gHHLN\u0024x1lawsGi\u0024H02uo2h0XchZjPEs1bmCfeGhythDHVWo26UKCe : 
  IDisposable,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024,
  \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly double \u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly List<double> \u0023\u003Dze90r\u0024X6mOh6sMbwaRw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz3A_LTZ7wmTQL;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX \u0023\u003DzEcmsYfw\u003D;

  public \u0023\u003DzGEJCq9O47NiPLstzce6jeG0gHHLN\u0024x1lawsGi\u0024H02uo2h0XchZjPEs1bmCfeGhythDHVWo26UKCe(
    IRenderContext2D _param1,
    \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D _param2,
    double _param3)
  {
    this.\u0023\u003DzEcmsYfw\u003D = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003Dz6W6QZGLrJ_xbPUC8cLFVBnmEa0aB(_param1, _param2);
    this.\u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D = _param3;
    this.\u0023\u003Dze90r\u0024X6mOh6sMbwaRw\u003D\u003D = new List<double>();
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
    IPathColor _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003Dz3A_LTZ7wmTQL = this.\u0023\u003DzEcmsYfw\u003D.\u0023\u003Dz7ZSU06M\u003D(_param1, _param2, _param3);
    this.\u0023\u003Dze90r\u0024X6mOh6sMbwaRw\u003D\u003D.Add(_param2);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(_param1, _param2);
    this.\u0023\u003Dze90r\u0024X6mOh6sMbwaRw\u003D\u003D.Add(_param1);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public void \u0023\u003DzBNsE20w\u003D()
  {
    for (int index = this.\u0023\u003Dze90r\u0024X6mOh6sMbwaRw\u003D\u003D.Count - 1; index >= 0; --index)
      this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(this.\u0023\u003Dze90r\u0024X6mOh6sMbwaRw\u003D\u003D[index], this.\u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D);
    this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzBNsE20w\u003D();
    this.\u0023\u003Dz3A_LTZ7wmTQL = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
    this.\u0023\u003Dze90r\u0024X6mOh6sMbwaRw\u003D\u003D.Clear();
  }

  void IDisposable.Dispose()
  {
    this.\u0023\u003DzBNsE20w\u003D();
  }
}
