// Decompiled with JetBrains decompiler
// Type: #=z6RuI7w1XIc38iQTeDB5TvbEktpRLPkDoFPTWmVY0oepT
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System.Diagnostics;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvbEktpRLPkDoFPTWmVY0oepT(
  IAnnotation _param1) : 
  \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D(_param1)
{
  
  private bool \u0023\u003DzqGwbHdeZ8yMA;
  
  private Point \u0023\u003DzhcsKvLfw_p5c;

  public override void \u0023\u003DzFeNr2Uw\u003D()
  {
  }

  public override void \u0023\u003DzGDdLHa8\u003D()
  {
  }

  public override void \u0023\u003DzUf222sU\u003D()
  {
  }

  public void \u0023\u003DzqglgC8jdkW7w()
  {
  }

  public override void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003Dz11bcnbUrALaA(_param1);
    IAnnotation hhh93Q0DqkV5Sv90k = this.\u0023\u003Dzy2oKVLXXOFmI();
    if (!this.\u0023\u003DzqGwbHdeZ8yMA)
      return;
    Point point = this.\u0023\u003DzUTaLYgNA\u00243iO25vv\u0024g\u003D\u003D(_param1.\u0023\u003DztkyOk5amPcz3());
    double num1 = point.X < 0.0 || point.X > this.\u0023\u003DzVuf430fCLR2l().ActualWidth ? 0.0 : point.X - this.\u0023\u003DzhcsKvLfw_p5c.X;
    double num2 = point.Y < 0.0 || point.Y > this.\u0023\u003DzVuf430fCLR2l().ActualHeight ? 0.0 : point.Y - this.\u0023\u003DzhcsKvLfw_p5c.Y;
    double num3 = hhh93Q0DqkV5Sv90k.get_DragDirections() == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? 0.0 : num1;
    double num4 = hhh93Q0DqkV5Sv90k.get_DragDirections() == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection ? 0.0 : num2;
    hhh93Q0DqkV5Sv90k.MoveAnnotation(num3, num4);
    this.\u0023\u003DzhcsKvLfw_p5c = point;
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    this.\u0023\u003DzGDdLHa8\u003D();
  }

  public new void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    IAnnotation hhh93Q0DqkV5Sv90k = this.\u0023\u003Dzy2oKVLXXOFmI();
    if (!hhh93Q0DqkV5Sv90k.get_IsEditable())
      return;
    this.\u0023\u003DzqGwbHdeZ8yMA = true;
    this.\u0023\u003DzhcsKvLfw_p5c = this.\u0023\u003DzUTaLYgNA\u00243iO25vv\u0024g\u003D\u003D(_param1.\u0023\u003DztkyOk5amPcz3());
    hhh93Q0DqkV5Sv90k.CaptureMouse();
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    IAnnotation hhh93Q0DqkV5Sv90k = this.\u0023\u003Dzy2oKVLXXOFmI();
    if (!this.\u0023\u003DzqGwbHdeZ8yMA)
      return;
    this.\u0023\u003DzqGwbHdeZ8yMA = false;
    hhh93Q0DqkV5Sv90k.ReleaseMouseCapture();
  }
}
