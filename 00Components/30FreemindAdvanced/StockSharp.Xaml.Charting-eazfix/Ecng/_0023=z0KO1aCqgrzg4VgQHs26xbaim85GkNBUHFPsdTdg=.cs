// Decompiled with JetBrains decompiler
// Type: #=z0KO1aCqgrzg4VgQHs26xbaim85GkNBUHFPsdTdg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows.Threading;

#nullable disable
public static class \u0023\u003Dz0KO1aCqgrzg4VgQHs26xbaim85GkNBUHFPsdTdg\u003D
{
  public static void \u0023\u003DznvGFbrlLtrNN(this Dispatcher _param0, Action _param1)
  {
    _param0.BeginInvoke((Delegate) _param1, Array.Empty<object>());
  }

  public static void \u0023\u003Dz40vIrjqAtFMX(this Dispatcher _param0, Action _param1)
  {
    if (_param0 == null || _param0.CheckAccess())
      _param1();
    else
      _param0.BeginInvoke((Delegate) _param1, Array.Empty<object>());
  }
}
