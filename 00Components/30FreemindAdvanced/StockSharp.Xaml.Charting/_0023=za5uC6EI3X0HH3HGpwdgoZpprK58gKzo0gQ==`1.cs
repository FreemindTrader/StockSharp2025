// Decompiled with JetBrains decompiler
// Type: #=za5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.ComponentModel;

#nullable disable
internal interface IRange<T> : 
  IRange,
  ICloneable,
  INotifyPropertyChanged
  where T : IComparable
{
  T Min { get; set; }

  T Max { get; set; }

  T Diff { get; }

  IRange<T> GrowBy(
    double _param1,
    double _param2);

  IRange<T> SetMinMax(
    double _param1,
    double _param2);

  IRange<T> \u0023\u003DzeiifnZI\u003D(
    IRange<T> _param1);
}
