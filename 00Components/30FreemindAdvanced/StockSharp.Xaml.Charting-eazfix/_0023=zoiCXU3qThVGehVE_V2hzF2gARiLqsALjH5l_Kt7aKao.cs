// Decompiled with JetBrains decompiler
// Type: #=zoiCXU3qThVGehVE_V2hzF2gARiLqsALjH5l_Kt7aKaoa
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003DzoiCXU3qThVGehVE_V2hzF2gARiLqsALjH5l_Kt7aKaoa : 
  \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A
{
  private readonly double \u0023\u003DzMXPRhxbl7IIq;

  internal \u0023\u003DzoiCXU3qThVGehVE_V2hzF2gARiLqsALjH5l_Kt7aKaoa(
    double _param1,
    double _param2,
    double _param3,
    int _param4,
    uint _param5 = 10)
    : base(_param1, _param2, _param4, _param5)
  {
    this.\u0023\u003DzMXPRhxbl7IIq = _param3;
  }

  public override void \u0023\u003Dz2RD3F8MtvzO1()
  {
    double num1 = Math.Log(this.\u0023\u003DzRanwdSJnFyTa, this.\u0023\u003DzMXPRhxbl7IIq);
    double y1 = num1 > 0.0 ? Math.Ceiling(num1) : Math.Floor(num1);
    double num2 = Math.Floor(Math.Log(Math.Abs(this.\u0023\u003Dz9SNCNR_cYKQr), this.\u0023\u003DzMXPRhxbl7IIq));
    double y2 = Math.Max(Math.Abs(Math.Sign(this.\u0023\u003Dz9SNCNR_cYKQr) == -1 ? y1 + num2 : y1 - num2), 1.0);
    double y3 = this.\u0023\u003Dz_RGjpdoFyFI_(this.\u0023\u003Dz_RGjpdoFyFI_(y1 - num2, false) / (double) \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A.\u0023\u003DzmbQYIoVdQhkN, true);
    this.\u0023\u003DzRanwdSJnFyTa = y3;
    this.\u0023\u003Dz9SNCNR_cYKQr = (Math.Pow(this.\u0023\u003DzMXPRhxbl7IIq, y3) - 1.0) / (double) this.\u0023\u003Dz5BKXmnjcYbh2SvP6Xw\u003D\u003D;
    double num3 = Math.Pow(this.\u0023\u003DzMXPRhxbl7IIq, y1);
    this.\u0023\u003Dzb9g4_DOjBJMx = new DoubleRange(num3 / Math.Pow(this.\u0023\u003DzMXPRhxbl7IIq, y2), num3);
  }
}
