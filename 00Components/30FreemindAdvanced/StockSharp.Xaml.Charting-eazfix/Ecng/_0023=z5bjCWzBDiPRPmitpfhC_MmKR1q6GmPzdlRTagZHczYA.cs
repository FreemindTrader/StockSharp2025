// Decompiled with JetBrains decompiler
// Type: #=z5bjCWzBDiPRPmitpfhC_MmKR1q6GmPzdlRTagZHczYAkpSxbE2YxU441B3l$5TtZ7AyE7mnx5AMf
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct \u0023\u003Dz5bjCWzBDiPRPmitpfhC_MmKR1q6GmPzdlRTagZHczYAkpSxbE2YxU441B3l\u00245TtZ7AyE7mnx5AMf : 
  IImageFilterFunction
{
  public double radius() => 1.0;

  public double calc_weight(double _param1)
  {
    if (Math.Abs(_param1) >= 1.0)
      return 0.0;
    return _param1 < 0.0 ? 1.0 + _param1 : 1.0 - _param1;
  }
}
