// Decompiled with JetBrains decompiler
// Type: #=zPlFvps97y7rWR4vc5KUjR6bHoFebsQwYUZ5UQgCSypG$EAe$qwEXvbmW31ox
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal static class \u0023\u003DzPlFvps97y7rWR4vc5KUjR6bHoFebsQwYUZ5UQgCSypG\u0024EAe\u0024qwEXvbmW31ox
{
  private static void \u0023\u003Dz\u0024qTWKsG3dS\u00241hxzQkkwNIls\u003D(
    double[,] _param0,
    uint _param1,
    double[,] _param2,
    uint _param3)
  {
    int length = _param0.GetLength(1);
    if (_param2.GetLength(1) != length)
      throw new FormatException("a1 and a2 must have the same second dimension.");
    for (int index = 0; index < length; ++index)
    {
      double num = _param0[(int) _param1, index];
      _param0[(int) _param1, index] = _param2[(int) _param3, index];
      _param2[(int) _param3, index] = num;
    }
  }

  public static int \u0023\u003Dzf6Qv_jk\u003D(double[,] _param0, uint _param1)
  {
    int index1 = (int) _param1;
    double num1 = -1.0;
    int length = _param0.GetLength(0);
    for (int index2 = (int) _param1; index2 < length; ++index2)
    {
      double num2;
      if ((num2 = Math.Abs(_param0[index2, (int) _param1])) > num1 && num2 != 0.0)
      {
        num1 = num2;
        index1 = index2;
      }
    }
    if (_param0[index1, (int) _param1] == 0.0)
      return -1;
    if (index1 == (int) _param1)
      return 0;
    \u0023\u003DzPlFvps97y7rWR4vc5KUjR6bHoFebsQwYUZ5UQgCSypG\u0024EAe\u0024qwEXvbmW31ox.\u0023\u003Dz\u0024qTWKsG3dS\u00241hxzQkkwNIls\u003D(_param0, (uint) index1, _param0, _param1);
    return index1;
  }
}
