// Decompiled with JetBrains decompiler
// Type: #=zPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk$ROYo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D : EventArgs
{
  
  private IRange \u0023\u003Dzyf57hTxonZCNKHGBsGM3_ZE\u003D;
  
  private \u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D \u0023\u003DzKEcDCute\u0024HaDe59alg\u003D\u003D;

  internal \u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D(
    IComparable _param1,
    IComparable _param2,
    \u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D _param3)
  {
    this.\u0023\u003DzO9Y96rr7OMgq(\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(_param1, _param2));
    this.\u0023\u003Dzj4L72E7lKmim(_param3);
  }

  internal \u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D(
    IRange _param1,
    \u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D _param2)
    : this(_param1.Min, _param1.Max, _param2)
  {
  }

  public IRange \u0023\u003DzbEaDvZcDA_K8()
  {
    return this.\u0023\u003Dzyf57hTxonZCNKHGBsGM3_ZE\u003D;
  }

  private void \u0023\u003DzO9Y96rr7OMgq(
    IRange _param1)
  {
    this.\u0023\u003Dzyf57hTxonZCNKHGBsGM3_ZE\u003D = _param1;
  }

  public \u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D \u0023\u003Dzh1yYuUlCTQ26()
  {
    return this.\u0023\u003DzKEcDCute\u0024HaDe59alg\u003D\u003D;
  }

  private void \u0023\u003Dzj4L72E7lKmim(
    \u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D _param1)
  {
    this.\u0023\u003DzKEcDCute\u0024HaDe59alg\u003D\u003D = _param1;
  }
}
