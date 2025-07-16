// Decompiled with JetBrains decompiler
// Type: -.YAxisDragModifier
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;
using System.Windows.Input;

#nullable disable
namespace SciChart.Charting;

internal sealed class YAxisDragModifier : 
  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ
{
  protected override IAxis \u0023\u003DzFLmJq0JJlr0n()
  {
    return this.\u0023\u003Dz4uoxB8oLWxeL(this.AxisId);
  }

  protected override Cursor \u0023\u003Dzwc3e5oDhVoYg(
    IAxis _param1)
  {
    return !_param1.get_IsPolarAxis() ? base.\u0023\u003Dzwc3e5oDhVoYg(_param1) : Cursors.None;
  }

  protected override bool \u0023\u003DzqFBxYEN\u0024frAq(Point _param1, Rect _param2, bool _param3)
  {
    IAxis xaxis = this.XAxis;
    if ((xaxis != null ? (xaxis.get_IsPolarAxis() ? 1 : 0) : 0) == 0)
      return !base.\u0023\u003DzqFBxYEN\u0024frAq(_param1, _param2, _param3);
    _param2.Height /= 2.0;
    return !_param2.Contains(_param1);
  }

  protected override void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, Point _param2)
  {
    IAxis dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    double num1 = _param1.X - _param2.X;
    double num2 = _param2.Y - _param1.Y;
    dynWmoFzgH4RlWB0lB.\u0023\u003DzquLnA5Y\u003D(dynWmoFzgH4RlWB0lB.IsHorizontalAxis ? -num1 : num2, ClipMode.None);
  }

  protected override IRange \u0023\u003DzFQz4aIsJtfEk(
    Point _param1,
    Point _param2,
    bool _param3,
    IAxis _param4)
  {
    \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c txZaHyXliZ9wXjzC = _param4.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();
    double num1 = _param1.X - _param2.X;
    double num2 = _param2.Y - _param1.Y;
    double num3 = _param4.IsHorizontalAxis ? -num1 : num2;
    IRange abyLt9clZggmJsWhw = _param3 ? txZaHyXliZ9wXjzC.\u0023\u003DzMKO149dmZRdJ(_param4.VisibleRange, num3) : txZaHyXliZ9wXjzC.\u0023\u003Dz5asXHk1unXCX(_param4.VisibleRange, num3);
    if (_param4.get_VisibleRangeLimit() != null)
      abyLt9clZggmJsWhw.\u0023\u003DzJIqIiUw\u003D(_param4.get_VisibleRangeLimit(), _param4.get_VisibleRangeLimitMode());
    return abyLt9clZggmJsWhw;
  }

  public override void OnModifierMouseDown(
    ModifierMouseArgs _param1)
  {
    if (!_param1.IsMaster())
      return;
    base.OnModifierMouseDown(_param1);
  }

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
    if (!_param1.IsMaster())
      return;
    base.OnModifierMouseMove(_param1);
  }

  public override void OnModifierMouseUp(
    ModifierMouseArgs _param1)
  {
    if (!_param1.IsMaster())
      return;
    base.OnModifierMouseUp(_param1);
  }
}
