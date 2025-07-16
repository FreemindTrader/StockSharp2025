// Decompiled with JetBrains decompiler
// Type: #=zJZzdBuNdGPIV6c3AUcyRfvQwvbu0Gd4Imzwk5GIfgC73
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public interface \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfvQwvbu0Gd4Imzwk5GIfgC73<TX, TY> : 
  IDataSeries<TX, TY>,
  ISuspendable,
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoazVlQnP90XoMutgGcLyCRUcP,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D
  where TX : IComparable
  where TY : IComparable
{
  void Append(TX _param1, TY _param2);

  void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2);

  void UpdateOrderAdornerLayer(
    TX _param1,
    TY _param2);

  void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3);

  void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3);
}
