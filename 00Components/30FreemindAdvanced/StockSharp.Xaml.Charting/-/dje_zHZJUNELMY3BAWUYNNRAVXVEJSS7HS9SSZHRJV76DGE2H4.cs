// Decompiled with JetBrains decompiler
// Type: -.dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Windows;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd : 
  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ
{
  protected override \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003DzFLmJq0JJlr0n()
  {
    return this.\u0023\u003Dz4uoxB8oLWxeL(this.AxisId);
  }

  protected override Cursor \u0023\u003Dzwc3e5oDhVoYg(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    return !_param1.get_IsPolarAxis() ? base.\u0023\u003Dzwc3e5oDhVoYg(_param1) : Cursors.None;
  }

  protected override bool \u0023\u003DzqFBxYEN\u0024frAq(Point _param1, Rect _param2, bool _param3)
  {
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB xaxis = this.XAxis;
    if ((xaxis != null ? (xaxis.get_IsPolarAxis() ? 1 : 0) : 0) == 0)
      return !base.\u0023\u003DzqFBxYEN\u0024frAq(_param1, _param2, _param3);
    _param2.Height /= 2.0;
    return !_param2.Contains(_param1);
  }

  protected override void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, Point _param2)
  {
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    double num1 = _param1.X - _param2.X;
    double num2 = _param2.Y - _param1.Y;
    dynWmoFzgH4RlWB0lB.\u0023\u003DzquLnA5Y\u003D(dynWmoFzgH4RlWB0lB.IsHorizontalAxis ? -num1 : num2, dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.None);
  }

  protected override \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzFQz4aIsJtfEk(
    Point _param1,
    Point _param2,
    bool _param3,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param4)
  {
    \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c txZaHyXliZ9wXjzC = _param4.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();
    double num1 = _param1.X - _param2.X;
    double num2 = _param2.Y - _param1.Y;
    double num3 = _param4.IsHorizontalAxis ? -num1 : num2;
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = _param3 ? txZaHyXliZ9wXjzC.\u0023\u003DzMKO149dmZRdJ(_param4.VisibleRange, num3) : txZaHyXliZ9wXjzC.\u0023\u003Dz5asXHk1unXCX(_param4.VisibleRange, num3);
    if (_param4.get_VisibleRangeLimit() != null)
      abyLt9clZggmJsWhw.\u0023\u003DzJIqIiUw\u003D(_param4.get_VisibleRangeLimit(), _param4.get_VisibleRangeLimitMode());
    return abyLt9clZggmJsWhw;
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (!_param1.\u0023\u003DzCJb5Ya_8UZCR())
      return;
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
  }

  public override void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (!_param1.\u0023\u003DzCJb5Ya_8UZCR())
      return;
    base.\u0023\u003Dz11bcnbUrALaA(_param1);
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (!_param1.\u0023\u003DzCJb5Ya_8UZCR())
      return;
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
  }
}
