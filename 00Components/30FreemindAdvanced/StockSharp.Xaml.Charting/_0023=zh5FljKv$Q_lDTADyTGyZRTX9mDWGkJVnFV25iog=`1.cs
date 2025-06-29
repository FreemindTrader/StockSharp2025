// Decompiled with JetBrains decompiler
// Type: #=zh5FljKv$Q_lDTADyTGyZRTX9mDWGkJVnFV25iog=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<\u0023\u003DzH9HNkng\u003D> : 
  IDisposable
  where \u0023\u003DzH9HNkng\u003D : IDisposable
{
  
  private readonly \u0023\u003DzH9HNkng\u003D \u0023\u003DzvQeD1pE\u003D;

  public \u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D(
    \u0023\u003DzH9HNkng\u003D _param1)
  {
    this.\u0023\u003DzvQeD1pE\u003D = _param1;
  }

  ~\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D()
  {
    this.\u0023\u003DzTuM3X1E\u003D(false);
  }

  internal \u0023\u003DzH9HNkng\u003D \u0023\u003DzQAdOJsjJeOwf()
  {
    return this.\u0023\u003DzvQeD1pE\u003D;
  }

  public void Dispose()
  {
    this.\u0023\u003DzTuM3X1E\u003D(true);
    GC.SuppressFinalize((object) this);
  }

  protected virtual void \u0023\u003DzTuM3X1E\u003D(bool _param1)
  {
    if ((object) this.\u0023\u003DzvQeD1pE\u003D == null)
      return;
    this.\u0023\u003DzvQeD1pE\u003D.Dispose();
  }

  public static explicit operator \u0023\u003DzH9HNkng\u003D(
    \u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<\u0023\u003DzH9HNkng\u003D> _param0)
  {
    return _param0.\u0023\u003DzvQeD1pE\u003D;
  }
}
