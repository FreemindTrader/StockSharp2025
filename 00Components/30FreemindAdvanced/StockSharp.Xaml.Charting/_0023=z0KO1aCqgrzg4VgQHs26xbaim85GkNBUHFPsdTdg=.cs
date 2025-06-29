// Decompiled with JetBrains decompiler
// Type: #=z0KO1aCqgrzg4VgQHs26xbaim85GkNBUHFPsdTdg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows.Threading;

#nullable disable
internal static class \u0023\u003Dz0KO1aCqgrzg4VgQHs26xbaim85GkNBUHFPsdTdg\u003D
{
  internal static void \u0023\u003DznvGFbrlLtrNN(this Dispatcher _param0, Action _param1)
  {
    _param0.BeginInvoke((Delegate) _param1, Array.Empty<object>());
  }

  internal static void \u0023\u003Dz40vIrjqAtFMX(this Dispatcher _param0, Action _param1)
  {
    if (_param0 == null || _param0.CheckAccess())
      _param1();
    else
      _param0.BeginInvoke((Delegate) _param1, Array.Empty<object>());
  }
}
