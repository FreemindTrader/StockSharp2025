// Decompiled with JetBrains decompiler
// Type: #=z4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95$saf5kXBsY
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;

#nullable disable
public sealed class ModifierMouseArgs : 
  \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD
{
  private int \u0023\u003DzFgTWW01mCjPdtdHq\u0024w\u003D\u003D;
  private Point \u0023\u003Dz6DEuOECqrNW2Rcv59oplVbY\u003D;
  private MouseButtons \u0023\u003Dzl91tJJo4vHObtC_p4tM0NyA\u003D;
  private MouseModifier \u0023\u003DzDTv7gc3WhhVElepiAw\u003D\u003D;

  public ModifierMouseArgs()
  {
  }

  public ModifierMouseArgs(
    Point _param1,
    MouseButtons _param2,
    MouseModifier _param3,
    bool _param4,
    IReceiveMouseEvents  _param5)
    : this(_param1, _param2, _param3, 0, _param4, _param5)
  {
  }

  public ModifierMouseArgs(
    Point _param1,
    MouseButtons _param2,
    MouseModifier _param3,
    int _param4,
    bool _param5,
    IReceiveMouseEvents  _param6 = null)
    : base(_param6, _param5)
  {
    this.\u0023\u003DzW78pCE7viucL(_param1);
    this.\u0023\u003DzhWBaoH4TqLHj(_param2);
    this.\u0023\u003DzPt90sMu0yVdn(_param3);
    this.\u0023\u003Dzirc\u002451h3LmN1(_param4);
  }

  public int \u0023\u003DzDuDuL4DDV5GL() => this.\u0023\u003DzFgTWW01mCjPdtdHq\u0024w\u003D\u003D;

  public void \u0023\u003Dzirc\u002451h3LmN1(int _param1)
  {
    this.\u0023\u003DzFgTWW01mCjPdtdHq\u0024w\u003D\u003D = _param1;
  }

  public Point MousePoint() => this.\u0023\u003Dz6DEuOECqrNW2Rcv59oplVbY\u003D;

  public void \u0023\u003DzW78pCE7viucL(Point _param1)
  {
    this.\u0023\u003Dz6DEuOECqrNW2Rcv59oplVbY\u003D = _param1;
  }

  public MouseButtons MouseButtons()
  {
    return this.\u0023\u003Dzl91tJJo4vHObtC_p4tM0NyA\u003D;
  }

  public void \u0023\u003DzhWBaoH4TqLHj(
    MouseButtons _param1)
  {
    this.\u0023\u003Dzl91tJJo4vHObtC_p4tM0NyA\u003D = _param1;
  }

  public MouseModifier Modifier()
  {
    return this.\u0023\u003DzDTv7gc3WhhVElepiAw\u003D\u003D;
  }

  public void \u0023\u003DzPt90sMu0yVdn(
    MouseModifier _param1)
  {
    this.\u0023\u003DzDTv7gc3WhhVElepiAw\u003D\u003D = _param1;
  }
}
