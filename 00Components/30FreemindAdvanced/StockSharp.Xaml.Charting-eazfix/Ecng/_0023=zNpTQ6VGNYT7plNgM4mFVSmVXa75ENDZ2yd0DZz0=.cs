// Decompiled with JetBrains decompiler
// Type: #=zNpTQ6VGNYT7plNgM4mFVSmVXa75ENDZ2yd0DZz0=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSmVXa75ENDZ2yd0DZz0\u003D
{
  private readonly IComparable \u0023\u003DzEBLPoa4\u003D;
  private readonly string \u0023\u003DzRpCB0alVLwOC;

  public \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSmVXa75ENDZ2yd0DZz0\u003D(
    IComparable _param1,
    string _param2)
  {
    this.\u0023\u003DzEBLPoa4\u003D = _param1;
    this.\u0023\u003DzRpCB0alVLwOC = _param2;
  }

  public void \u0023\u003DzghVcXYXB9yAk(IComparable _param1, string _param2)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) >= 0)
      throw new InvalidOperationException($"The argument \"{this.\u0023\u003DzRpCB0alVLwOC}\", value={this.\u0023\u003DzEBLPoa4\u003D}, must be less than argument \"{_param2}\", value={_param1}");
  }

  public void \u0023\u003DzSbjmQ1U_EtQq(IComparable _param1, string _param2)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) == 0)
      throw new InvalidOperationException($"The argument \"{this.\u0023\u003DzRpCB0alVLwOC}\", value={this.\u0023\u003DzEBLPoa4\u003D}, must not be equal to argument \"{_param2}\", value={_param1}");
  }

  public void \u0023\u003DzqSRI49wXjCOy(IComparable _param1, string _param2)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) != 0)
      throw new InvalidOperationException($"The argument \"{this.\u0023\u003DzRpCB0alVLwOC}\", value={this.\u0023\u003DzEBLPoa4\u003D}, must be equal to argument \"{_param2}\", value={_param1}");
  }

  public void \u0023\u003DzSbjmQ1U_EtQq(IComparable _param1)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) == 0)
      throw new InvalidOperationException($"The argument \"{this.\u0023\u003DzRpCB0alVLwOC}\", value={this.\u0023\u003DzEBLPoa4\u003D}, must not be equal to {_param1}");
  }

  public void \u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D(IComparable _param1, string _param2)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) > 0)
      throw new InvalidOperationException($"The argument \"{this.\u0023\u003DzRpCB0alVLwOC}\", value={this.\u0023\u003DzEBLPoa4\u003D}, must be less than or equal to argument \"{_param2}\", value={_param1}");
  }

  public void \u0023\u003DzIR3Z_Ken7pfcXCwNTw\u003D\u003D(IComparable _param1)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) < 0)
      throw new InvalidOperationException($"The argument \"{this.\u0023\u003DzRpCB0alVLwOC}\", value={this.\u0023\u003DzEBLPoa4\u003D}, must be greater than or equal to {_param1}");
  }

  public void \u0023\u003Dzc5Mtj4NgZLWC(IComparable _param1)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) <= 0)
      throw new InvalidOperationException($"The argument \"{this.\u0023\u003DzRpCB0alVLwOC}\", value={this.\u0023\u003DzEBLPoa4\u003D}, must be greater than {_param1}");
  }
}
