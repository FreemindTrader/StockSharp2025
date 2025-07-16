// Decompiled with JetBrains decompiler
// Type: #=ztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.ComponentModel;

#nullable disable
public interface IRange :
  INotifyPropertyChanged,
  ICloneable
{
    IComparable Min
    {
        get; set;
    }

    IComparable Max
    {
        get; set;
    }

    bool IsDefined
    {
        get;
    }

    IComparable Diff
    {
        get;
    }

    bool IsZero
    {
        get;
    }

    DoubleRange AsDoubleRange();

    IRange GrowBy(
      double _param1,
      double _param2);

  IRange SetMinMax(
    double _param1,
    double _param2 );

    IRange \u0023\u003DzsutwFhFKqYRf34G8vw\u003D\u003D(
      double _param1,
      double _param2,
      IRange _param3);

  IRange \u0023\u003DzJIqIiUw\u003D(
    IRange _param1);

  IRange \u0023\u003DzJIqIiUw\u003D(
    IRange _param1,
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D _param2);

  IRange \u0023\u003DzeiifnZI\u003D(
    IRange _param1);

  bool \u0023\u003DzU0feMzXFLecQ( IComparable _param1 );
}
