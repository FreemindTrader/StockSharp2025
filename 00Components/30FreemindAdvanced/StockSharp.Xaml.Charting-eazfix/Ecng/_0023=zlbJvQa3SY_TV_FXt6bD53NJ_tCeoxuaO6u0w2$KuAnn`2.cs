// Decompiled with JetBrains decompiler
// Type: #=zlbJvQa3SY_TV_FXt6bD53NJ_tCeoxuaO6u0w2$KuAnng9SymwQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public interface \u0023\u003DzlbJvQa3SY_TV_FXt6bD53NJ_tCeoxuaO6u0w2\u0024KuAnng9SymwQ\u003D\u003D<TX, TY> : 
  IDataSeries<TX, TY>,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  \u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH
  where TX : IComparable
  where TY : IComparable
{
  IList<TY> Y1Values { get; }

  IList<TY> get_Y1Values();

  void \u0023\u003Dznc8esWY\u003D(
    TX _param1,
    TY _param2,
    TY _param3);

  void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2,
    IEnumerable<TY> _param3);

  void UpdateOrderAdornerLayer(
    TX _param1,
    TY _param2,
    TY _param3);

  void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3,
    TY _param4);

  void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<TY> _param4);
}
