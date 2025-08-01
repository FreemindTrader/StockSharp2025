// Decompiled with JetBrains decompiler
// Type: #=zTbSy5Tg7CNKewHb2FguXq$9fYrtRMypdmYI2qF8ZEFkx
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public interface IDataSeries<TX, TY> : 
  ISuspendable,
  IDataSeries
  where TX : IComparable
  where TY : IComparable
{
  IList<TX> XValues { get; }

  IList<TX> get_XValues();

  IList<TY> YValues { get; }

  IList<TY> get_YValues();

  void Append(TX _param1, TY[] _param2);

  void Append(
    IEnumerable<TX> _param1,
    IEnumerable<TY>[] _param2);

  void GuiUpdateAndClear(TX _param1);

  void \u0023\u003DzfEbP\u00247w\u003D(int _param1);

  void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2);

  IDataSeries<TX, TY> \u0023\u003DzQ8SgRgQ\u003D();

  TY GetYMinAt(int _param1, TY _param2);

  TY GetYMaxAt(int _param1, TY _param2);
}
