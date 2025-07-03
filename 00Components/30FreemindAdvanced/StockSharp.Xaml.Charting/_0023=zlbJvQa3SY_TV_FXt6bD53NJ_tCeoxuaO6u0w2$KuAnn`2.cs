// Decompiled with JetBrains decompiler
// Type: #=zlbJvQa3SY_TV_FXt6bD53NJ_tCeoxuaO6u0w2$KuAnng9SymwQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;

#nullable disable
internal interface \u0023\u003DzlbJvQa3SY_TV_FXt6bD53NJ_tCeoxuaO6u0w2\u0024KuAnng9SymwQ\u003D\u003D<T, \u0023\u003DzE8zkRfY\u003D> : 
  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<T, \u0023\u003DzE8zkRfY\u003D>,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  ISuspendable,
  \u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH
  where T : IComparable
  where \u0023\u003DzE8zkRfY\u003D : IComparable
{
  IList<\u0023\u003DzE8zkRfY\u003D> Y1Values { get; }

  IList<\u0023\u003DzE8zkRfY\u003D> get_Y1Values();

  void \u0023\u003Dznc8esWY\u003D(
    T _param1,
    \u0023\u003DzE8zkRfY\u003D _param2,
    \u0023\u003DzE8zkRfY\u003D _param3);

  void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<T> _param1,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param2,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param3);

  void \u0023\u003DzFkV86a8\u003D(
    T _param1,
    \u0023\u003DzE8zkRfY\u003D _param2,
    \u0023\u003DzE8zkRfY\u003D _param3);

  void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    T _param2,
    \u0023\u003DzE8zkRfY\u003D _param3,
    \u0023\u003DzE8zkRfY\u003D _param4);

  void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<T> _param2,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param3,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param4);
}
