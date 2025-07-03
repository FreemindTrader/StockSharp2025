// Decompiled with JetBrains decompiler
// Type: #=zDH5EtXmKZCpH30z_eydhO42Ygtxj0A9MtVBlpvd5g5Ii2CglZA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;

#nullable disable
internal interface \u0023\u003DzDH5EtXmKZCpH30z_eydhO42Ygtxj0A9MtVBlpvd5g5Ii2CglZA\u003D\u003D<T, \u0023\u003DzE8zkRfY\u003D> : 
  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<T, \u0023\u003DzE8zkRfY\u003D>,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  ISuspendable,
  \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrFQ3G9W9xt2vxQkAWz\u0024zVnJ
  where T : IComparable
  where \u0023\u003DzE8zkRfY\u003D : IComparable
{
  IList<\u0023\u003DzE8zkRfY\u003D> OpenValues { get; }

  IList<\u0023\u003DzE8zkRfY\u003D> get_OpenValues();

  IList<\u0023\u003DzE8zkRfY\u003D> HighValues { get; }

  IList<\u0023\u003DzE8zkRfY\u003D> get_HighValues();

  IList<\u0023\u003DzE8zkRfY\u003D> LowValues { get; }

  IList<\u0023\u003DzE8zkRfY\u003D> get_LowValues();

  IList<\u0023\u003DzE8zkRfY\u003D> CloseValues { get; }

  IList<\u0023\u003DzE8zkRfY\u003D> get_CloseValues();

  void \u0023\u003Dznc8esWY\u003D(
    T _param1,
    \u0023\u003DzE8zkRfY\u003D _param2,
    \u0023\u003DzE8zkRfY\u003D _param3,
    \u0023\u003DzE8zkRfY\u003D _param4,
    \u0023\u003DzE8zkRfY\u003D _param5);

  void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<T> _param1,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param2,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param3,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param4,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param5);

  void \u0023\u003DzFkV86a8\u003D(
    T _param1,
    \u0023\u003DzE8zkRfY\u003D _param2,
    \u0023\u003DzE8zkRfY\u003D _param3,
    \u0023\u003DzE8zkRfY\u003D _param4,
    \u0023\u003DzE8zkRfY\u003D _param5);

  void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    T _param2,
    \u0023\u003DzE8zkRfY\u003D _param3,
    \u0023\u003DzE8zkRfY\u003D _param4,
    \u0023\u003DzE8zkRfY\u003D _param5,
    \u0023\u003DzE8zkRfY\u003D _param6);

  void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<T> _param2,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param3,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param4,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param5,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param6);
}
