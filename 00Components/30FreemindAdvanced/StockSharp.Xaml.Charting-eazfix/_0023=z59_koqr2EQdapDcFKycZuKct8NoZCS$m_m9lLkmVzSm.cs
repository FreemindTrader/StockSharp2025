// Decompiled with JetBrains decompiler
// Type: #=z59_koqr2EQdapDcFKycZuKct8NoZCS$m_m9lLkmVzSm3TrzIUE_fBok=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003Dz59_koqr2EQdapDcFKycZuKct8NoZCS\u0024m_m9lLkmVzSm3TrzIUE_fBok\u003D : 
  IDisposable,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX \u0023\u003DzEcmsYfw\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Size \u0023\u003DzgYZhPyPIW8zq;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzXZq\u0024gjyIo\u00244n;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IPathColor \u0023\u003DzQWU4skw9ivrJ;

  public \u0023\u003Dz59_koqr2EQdapDcFKycZuKct8NoZCS\u0024m_m9lLkmVzSm3TrzIUE_fBok\u003D(
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param1,
    \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D _param2,
    Size _param3)
  {
    this.\u0023\u003DzEcmsYfw\u003D = _param1;
    this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J = _param2;
    this.\u0023\u003DzgYZhPyPIW8zq = _param3;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
    IPathColor _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003DzQWU4skw9ivrJ = _param1;
    this.\u0023\u003DzNq_YOflx6uAn(_param2, _param3);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  private void \u0023\u003DzNq_YOflx6uAn(double _param1, double _param2)
  {
    Point point1 = new Point(_param1, _param2);
    if (!point1.\u0023\u003DzxGhbraO0gg9\u0024(this.\u0023\u003DzgYZhPyPIW8zq))
      return;
    Point point2 = this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J.\u0023\u003DzsTReN_n58EEf(point1);
    if (this.\u0023\u003DzXZq\u0024gjyIo\u00244n != null)
      this.\u0023\u003DzXZq\u0024gjyIo\u00244n.\u0023\u003DzfRDRUq8\u003D(point2.X, point2.Y);
    else
      this.\u0023\u003DzXZq\u0024gjyIo\u00244n = this.\u0023\u003DzEcmsYfw\u003D.\u0023\u003Dz7ZSU06M\u003D(this.\u0023\u003DzQWU4skw9ivrJ, point2.X, point2.Y);
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzNq_YOflx6uAn(_param1, _param2);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public void \u0023\u003DzBNsE20w\u003D()
  {
    if (this.\u0023\u003DzXZq\u0024gjyIo\u00244n == null)
      return;
    this.\u0023\u003DzXZq\u0024gjyIo\u00244n.\u0023\u003DzBNsE20w\u003D();
    this.\u0023\u003DzXZq\u0024gjyIo\u00244n = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
  }

  public void Dispose() => this.\u0023\u003DzBNsE20w\u003D();
}
