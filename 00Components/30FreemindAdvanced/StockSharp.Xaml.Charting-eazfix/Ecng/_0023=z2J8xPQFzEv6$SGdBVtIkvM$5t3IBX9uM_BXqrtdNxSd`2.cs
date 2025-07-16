// Decompiled with JetBrains decompiler
// Type: #=z2J8xPQFzEv6$SGdBVtIkvM$5t3IBX9uM_BXqrtdNxSdMVJP41w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public interface \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvM\u00245t3IBX9uM_BXqrtdNxSdMVJP41w\u003D\u003D<TX, TY> : 
  IDataSeries<TX, TY>,
  ISuspendable,
  \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA6LApz0w6piN5MPsjC14et6W,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D
  where TX : IComparable
  where TY : IComparable
{
  IList<TY> HighValues { get; }

  IList<TY> get_HighValues();

  IList<TY> LowValues { get; }

  IList<TY> get_LowValues();
}
