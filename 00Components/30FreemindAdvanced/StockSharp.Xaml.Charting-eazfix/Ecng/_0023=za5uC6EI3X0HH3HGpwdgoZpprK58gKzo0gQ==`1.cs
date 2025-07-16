// Decompiled with JetBrains decompiler
// Type: #=za5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.ComponentModel;

#nullable disable
public interface IRange<T> : 
  INotifyPropertyChanged,
  ICloneable,
  IRange
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
