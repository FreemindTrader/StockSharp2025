// Decompiled with JetBrains decompiler
// Type: #=zsDU9XQyTsl2DjEg2HhKpBKWzr$H44WFj$d8XySFFwpGZ8_Hs5A==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;

#nullable disable
public sealed class ViewportManager : 
  \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D
{
  protected override IRange \u0023\u003DzagcOSHrk9Og4(
    IAxis _param1)
  {
    if (_param1.get_AutoRange() != dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always)
      return _param1.VisibleRange;
    IRange abyLt9clZggmJsWhw = _param1.\u0023\u003DzFwoMKP9juTnt();
    return abyLt9clZggmJsWhw != null && abyLt9clZggmJsWhw.IsDefined ? abyLt9clZggmJsWhw : _param1.VisibleRange;
  }

  protected override IRange \u0023\u003DzzyBAd1oR0Zv2(
    IAxis _param1,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param2)
  {
    if (_param1.get_AutoRange() != dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always || _param2.\u0023\u003Dz4nxjMSnapDjJ == null || _param2.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D == null)
      return _param1.VisibleRange;
    IRange abyLt9clZggmJsWhw = _param1.\u0023\u003DzzMId\u0024f67Wftb(_param2);
    return !abyLt9clZggmJsWhw.IsDefined ? _param1.VisibleRange : abyLt9clZggmJsWhw;
  }
}
