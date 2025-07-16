// Decompiled with JetBrains decompiler
// Type: #=zDH5EtXmKZCpH30z_eydhO42Ygtxj0A9MtVBlpvd5g5Ii2CglZA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public interface \u0023\u003DzDH5EtXmKZCpH30z_eydhO42Ygtxj0A9MtVBlpvd5g5Ii2CglZA\u003D\u003D<TX, TY> : 
  IDataSeries<TX, TY>,
  IOhlcDataSeries,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D
  where TX : IComparable
  where TY : IComparable
{
  IList<TY> OpenValues { get; }

  IList<TY> get_OpenValues();

  IList<TY> HighValues { get; }

  IList<TY> get_HighValues();

  IList<TY> LowValues { get; }

  IList<TY> get_LowValues();

  IList<TY> CloseValues { get; }

  IList<TY> get_CloseValues();

  void \u0023\u003Dznc8esWY\u003D(
    TX _param1,
    TY _param2,
    TY _param3,
    TY _param4,
    TY _param5);

  void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<TY> _param4,
    IEnumerable<TY> _param5);

  void UpdateOrderAdornerLayer(
    TX _param1,
    TY _param2,
    TY _param3,
    TY _param4,
    TY _param5);

  void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3,
    TY _param4,
    TY _param5,
    TY _param6);

  void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<TY> _param4,
    IEnumerable<TY> _param5,
    IEnumerable<TY> _param6);
}
