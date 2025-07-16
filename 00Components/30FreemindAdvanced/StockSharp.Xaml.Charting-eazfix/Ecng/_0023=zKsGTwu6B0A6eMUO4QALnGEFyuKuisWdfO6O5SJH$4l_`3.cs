// Decompiled with JetBrains decompiler
// Type: #=zKsGTwu6B0A6eMUO4QALnGEFyuKuisWdfO6O5SJH$4l_vSJGnVw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public interface \u0023\u003DzKsGTwu6B0A6eMUO4QALnGEFyuKuisWdfO6O5SJH\u00244l_vSJGnVw\u003D\u003D<TX, TY, \u0023\u003DzPqz5cUs\u003D> : 
  IDataSeries<TX, TY>,
  \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhip318DMhXaWOsdXxIKq2Zfn_,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D
  where TX : IComparable
  where TY : IComparable
  where \u0023\u003DzPqz5cUs\u003D : IComparable
{
  IList<\u0023\u003DzPqz5cUs\u003D> ZValues { get; }

  IList<\u0023\u003DzPqz5cUs\u003D> get_ZValues();

  void \u0023\u003Dznc8esWY\u003D(
    TX _param1,
    TY _param2,
    \u0023\u003DzPqz5cUs\u003D _param3);

  void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2,
    IEnumerable<\u0023\u003DzPqz5cUs\u003D> _param3);

  void UpdateOrderAdornerLayer(
    TX _param1,
    TY _param2,
    \u0023\u003DzPqz5cUs\u003D _param3,
    int _param4);

  void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3,
    \u0023\u003DzPqz5cUs\u003D _param4);

  void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<\u0023\u003DzPqz5cUs\u003D> _param4);
}
