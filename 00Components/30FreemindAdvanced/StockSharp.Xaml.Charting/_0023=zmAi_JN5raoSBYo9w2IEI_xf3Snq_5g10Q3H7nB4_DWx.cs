// Decompiled with JetBrains decompiler
// Type: #=zmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw$eNHICc2N3v3PZtDYjg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;

#nullable disable
internal struct \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D(
  int _param1,
  int _param2)
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public int \u0023\u003DzwP120vA\u003D = _param1;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public int \u0023\u003Dzi8jDI4I\u003D = _param2;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public int \u0023\u003Dzeids9mY\u003D = 0;

  public bool \u0023\u003DzGoZaFyE\u003D(
    \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xf3Snq_5g10Q3H7nB4_DWxk20wjU4Dw\u0024eNHICc2N3v3PZtDYjg\u003D _param1)
  {
    double num1 = (double) (_param1.\u0023\u003DzwP120vA\u003D - this.\u0023\u003DzwP120vA\u003D);
    double num2 = (double) (_param1.\u0023\u003Dzi8jDI4I\u003D - this.\u0023\u003Dzi8jDI4I\u003D);
    return (this.\u0023\u003Dzeids9mY\u003D = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(Math.Sqrt(num1 * num1 + num2 * num2))) > 384;
  }
}
