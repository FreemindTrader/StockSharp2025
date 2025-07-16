// Decompiled with JetBrains decompiler
// Type: #=z2J8xPQFzEv6$SGdBVtIkvM$5t3IBX9uM_BXqrtdNxSdMVJP41w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public interface \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvM\u00245t3IBX9uM_BXqrtdNxSdMVJP41w\u003D\u003D<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D> : 
  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>,
  ISuspendable,
  \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA6LApz0w6piN5MPsjC14et6W,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D
  where \u0023\u003DzulcL8RA\u003D : IComparable
  where \u0023\u003DzE8zkRfY\u003D : IComparable
{
  IList<\u0023\u003DzE8zkRfY\u003D> HighValues { get; }

  IList<\u0023\u003DzE8zkRfY\u003D> get_HighValues();

  IList<\u0023\u003DzE8zkRfY\u003D> LowValues { get; }

  IList<\u0023\u003DzE8zkRfY\u003D> get_LowValues();
}
