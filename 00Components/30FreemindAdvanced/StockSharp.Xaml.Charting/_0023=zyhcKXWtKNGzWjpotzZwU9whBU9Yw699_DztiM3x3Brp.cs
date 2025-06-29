// Decompiled with JetBrains decompiler
// Type: #=zyhcKXWtKNGzWjpotzZwU9whBU9Yw699_DztiM3x3BrpxyflUng==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzyhcKXWtKNGzWjpotzZwU9whBU9Yw699_DztiM3x3BrpxyflUng\u003D\u003D
{
  private readonly Action \u0023\u003DzDPt0\u0024eo\u003D;

  public \u0023\u003DzyhcKXWtKNGzWjpotzZwU9whBU9Yw699_DztiM3x3BrpxyflUng\u003D\u003D(Action _param1)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539439938));
    this.\u0023\u003DzDPt0\u0024eo\u003D = _param1;
    CompositionTarget.Rendering += new EventHandler(this.\u0023\u003DzHr8C02XUg4o9);
  }

  private void \u0023\u003DzHr8C02XUg4o9(object _param1, EventArgs _param2)
  {
    CompositionTarget.Rendering -= new EventHandler(this.\u0023\u003DzHr8C02XUg4o9);
    this.\u0023\u003DzDPt0\u0024eo\u003D();
  }
}
