// Decompiled with JetBrains decompiler
// Type: #=z4tSiDY285eyTFNy9DwhLpD1UwAzu2BuP9lqlOFDmwILYsPaMHR9_6vDu77nAwU3HyQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct \u0023\u003Dz4tSiDY285eyTFNy9DwhLpD1UwAzu2BuP9lqlOFDmwILYsPaMHR9_6vDu77nAwU3HyQ\u003D\u003D
{
  public static bool \u0023\u003Dz2weMuqtP1bYM(
    double[,] _param0,
    double[,] _param1,
    double[,] _param2)
  {
    if (_param0.GetLength(0) != 4 || _param1.GetLength(0) != 4 || _param0.GetLength(1) != 4 || _param2.GetLength(0) != 4 || _param1.GetLength(1) != 2 || _param2.GetLength(1) != 2)
      throw new FormatException("left right and result must all be the same size.");
    int length1 = _param1.GetLength(0);
    int length2 = _param1.GetLength(1);
    double[,] numArray = new double[length1, length1 + length2];
    for (int index1 = 0; index1 < length1; ++index1)
    {
      for (int index2 = 0; index2 < length1; ++index2)
        numArray[index1, index2] = _param0[index1, index2];
      for (int index3 = 0; index3 < length2; ++index3)
        numArray[index1, length1 + index3] = _param1[index1, index3];
    }
    for (int index4 = 0; index4 < length1; ++index4)
    {
      if (\u0023\u003DzPlFvps97y7rWR4vc5KUjR6bHoFebsQwYUZ5UQgCSypG\u0024EAe\u0024qwEXvbmW31ox.\u0023\u003Dzf6Qv_jk\u003D(numArray, (uint) index4) < 0)
        return false;
      double num1 = numArray[index4, index4];
      for (int index5 = index4; index5 < length1 + length2; ++index5)
        numArray[index4, index5] /= num1;
      for (int index6 = index4 + 1; index6 < length1; ++index6)
      {
        double num2 = numArray[index6, index4];
        for (int index7 = index4; index7 < length1 + length2; ++index7)
          numArray[index6, index7] -= num2 * numArray[index4, index7];
      }
    }
    for (int index8 = 0; index8 < length2; ++index8)
    {
      for (int index9 = length1 - 1; index9 >= 0; --index9)
      {
        _param2[index9, index8] = numArray[index9, length1 + index8];
        for (int index10 = index9 + 1; index10 < length1; ++index10)
          _param2[index9, index8] -= numArray[index9, index10] * _param2[index10, index8];
      }
    }
    return true;
  }
}
