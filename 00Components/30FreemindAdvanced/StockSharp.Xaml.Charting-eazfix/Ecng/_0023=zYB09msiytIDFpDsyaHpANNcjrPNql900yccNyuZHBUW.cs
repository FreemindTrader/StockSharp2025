// Decompiled with JetBrains decompiler
// Type: #=zYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
public interface \u0023\u003DzYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6 : 
  IDisposable,
  IInvalidatableElement,
  IRenderSurface,
  IHitTestable
{
  ReadOnlyCollection<IRenderableSeries> \u0023\u003Dzvxq3X_8T\u0024Noo();

  IRenderContext2D \u0023\u003Dz1cRMfLZU4Eo2();

  bool \u0023\u003DzdBvSINdoeQWX(
    IRenderableSeries _param1);

  void \u0023\u003DzJoneIt0\u003D(
    IRenderableSeries _param1);

  void \u0023\u003DzJoneIt0\u003D(
    IEnumerable<IRenderableSeries> _param1);

  void \u0023\u003Dz_SCZwjM\u003D(
    IRenderableSeries _param1);
}
