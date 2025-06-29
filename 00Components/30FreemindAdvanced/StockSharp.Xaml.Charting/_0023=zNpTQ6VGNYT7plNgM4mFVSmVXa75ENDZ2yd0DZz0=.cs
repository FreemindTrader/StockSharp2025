// Decompiled with JetBrains decompiler
// Type: #=zNpTQ6VGNYT7plNgM4mFVSmVXa75ENDZ2yd0DZz0=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal sealed class \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSmVXa75ENDZ2yd0DZz0\u003D
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
      throw new InvalidOperationException(string.Format("", (object) this.\u0023\u003DzRpCB0alVLwOC, (object) this.\u0023\u003DzEBLPoa4\u003D, (object) _param2, (object) _param1));
  }

  public void \u0023\u003DzSbjmQ1U_EtQq(IComparable _param1, string _param2)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) == 0)
      throw new InvalidOperationException(string.Format("", (object) this.\u0023\u003DzRpCB0alVLwOC, (object) this.\u0023\u003DzEBLPoa4\u003D, (object) _param2, (object) _param1));
  }

  public void \u0023\u003DzqSRI49wXjCOy(IComparable _param1, string _param2)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) != 0)
      throw new InvalidOperationException(string.Format("", (object) this.\u0023\u003DzRpCB0alVLwOC, (object) this.\u0023\u003DzEBLPoa4\u003D, (object) _param2, (object) _param1));
  }

  public void \u0023\u003DzSbjmQ1U_EtQq(IComparable _param1)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) == 0)
      throw new InvalidOperationException(string.Format("", (object) this.\u0023\u003DzRpCB0alVLwOC, (object) this.\u0023\u003DzEBLPoa4\u003D, (object) _param1));
  }

  public void \u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D(IComparable _param1, string _param2)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) > 0)
      throw new InvalidOperationException(string.Format("", (object) this.\u0023\u003DzRpCB0alVLwOC, (object) this.\u0023\u003DzEBLPoa4\u003D, (object) _param2, (object) _param1));
  }

  public void \u0023\u003DzIR3Z_Ken7pfcXCwNTw\u003D\u003D(IComparable _param1)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) < 0)
      throw new InvalidOperationException(string.Format("", (object) this.\u0023\u003DzRpCB0alVLwOC, (object) this.\u0023\u003DzEBLPoa4\u003D, (object) _param1));
  }

  public void \u0023\u003Dzc5Mtj4NgZLWC(IComparable _param1)
  {
    if (this.\u0023\u003DzEBLPoa4\u003D.CompareTo((object) _param1) <= 0)
      throw new InvalidOperationException(string.Format("", (object) this.\u0023\u003DzRpCB0alVLwOC, (object) this.\u0023\u003DzEBLPoa4\u003D, (object) _param1));
  }
}
