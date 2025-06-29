// Decompiled with JetBrains decompiler
// Type: #=zriJlGz16EBVXNzRMW4Dc4q2zQrD$AZGpxk7MS915UrPT
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;

#nullable disable
internal sealed class \u0023\u003DzriJlGz16EBVXNzRMW4Dc4q2zQrD\u0024AZGpxk7MS915UrPT
{
  private readonly List<Action> \u0023\u003Dz0KTv89o\u003D = new List<Action>();

  public void \u0023\u003DzBkxoLC0\u003D(Action _param1)
  {
    this.\u0023\u003Dz0KTv89o\u003D.Add(_param1);
  }

  public void \u0023\u003DzY9qzIPY\u003D()
  {
    foreach (Action action in this.\u0023\u003Dz0KTv89o\u003D)
      action();
    this.\u0023\u003Dz0KTv89o\u003D.Clear();
  }
}
