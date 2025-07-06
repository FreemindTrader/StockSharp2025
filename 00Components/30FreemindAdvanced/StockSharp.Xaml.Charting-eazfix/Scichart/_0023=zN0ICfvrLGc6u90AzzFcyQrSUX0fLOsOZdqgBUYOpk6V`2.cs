// Decompiled with JetBrains decompiler
// Type: #=zN0ICfvrLGc6u90AzzFcyQrSUX0fLOsOZdqgBUYOpk6V$
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrSUX0fLOsOZdqgBUYOpk6V\u0024<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D> : 
  \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D,
  \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZnF8py_1UHtuLwP\u0024jqT0El1T
  where \u0023\u003DzulcL8RA\u003D : IComparable
  where \u0023\u003DzE8zkRfY\u003D : IComparable
{
  private readonly int _xAxisCount;
  private readonly \u0023\u003Dzboj3ckhISv7k6koCkTeIfz13hju1HWdmJxf054ica0kRcYMVoV1Kpw0\u003D \u0023\u003DzXfO9DgaVRj7B;
  private readonly Func<int, \u0023\u003DzulcL8RA\u003D> \u0023\u003Dzkv0SgdOPhi_0;
  private readonly Func<int, \u0023\u003DzE8zkRfY\u003D> \u0023\u003DzyoB24lWE6a_0;
  private readonly int \u0023\u003DzqvN2BUtRI\u0024I\u0024;

  public \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrSUX0fLOsOZdqgBUYOpk6V\u0024(
    \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D _param1,
    Func<int, \u0023\u003DzulcL8RA\u003D> _param2,
    Func<int, \u0023\u003DzE8zkRfY\u003D> _param3,
    int _param4)
  {
    this.\u0023\u003DzqvN2BUtRI\u0024I\u0024 = _param1.\u0023\u003DzUOzVYhDwNbf3();
    this._xAxisCount = _param4;
    this.\u0023\u003Dzkv0SgdOPhi_0 = _param2;
    this.\u0023\u003DzyoB24lWE6a_0 = _param3;
    this.\u0023\u003DzXfO9DgaVRj7B = (\u0023\u003Dzboj3ckhISv7k6koCkTeIfz13hju1HWdmJxf054ica0kRcYMVoV1Kpw0\u003D) _param1;
  }

  [SpecialName]
  public double \u0023\u003Dz2_4KSTY\u003D()
  {
    return this.\u0023\u003Dzkv0SgdOPhi_0(this._xAxisCount).ToDouble();
  }

  [SpecialName]
  public double \u0023\u003Dzu7q98_E\u003D()
  {
    return this.\u0023\u003DzyoB24lWE6a_0(this.\u0023\u003DzqvN2BUtRI\u0024I\u0024).ToDouble();
  }

  [SpecialName]
  public double \u0023\u003Dz4UDE5UByX\u00245LtE1gJA\u003D\u003D()
  {
    return this.\u0023\u003Dzkv0SgdOPhi_0(this._xAxisCount).ToDouble();
  }

  [SpecialName]
  public double \u0023\u003Dzk\u0024jdyMQeK6oiQLL\u00248w\u003D\u003D()
  {
    return this.\u0023\u003Dzkv0SgdOPhi_0(this._xAxisCount + 1).ToDouble();
  }

  [SpecialName]
  public double \u0023\u003DzhQUKpHOzWGMGzdOfNg\u003D\u003D()
  {
    return this.\u0023\u003DzyoB24lWE6a_0(0).ToDouble();
  }

  [SpecialName]
  public double \u0023\u003DzWoua01IozoKYJ1eZRw\u003D\u003D()
  {
    return this.\u0023\u003DzyoB24lWE6a_0(this.\u0023\u003DzqvN2BUtRI\u0024I\u0024).ToDouble();
  }

  public IList<int> \u0023\u003Dzv5pY0E1wS\u00245oXMKltw\u003D\u003D(
    \u0023\u003DzPlFvps97y7rWR4vc5KUjR5Ch17PMi3H3ortKPxkjv7KR _param1)
  {
    return (IList<int>) new \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrSUX0fLOsOZdqgBUYOpk6V\u0024<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>.\u0023\u003Dzlzy8GZzVmC8F(this.\u0023\u003DzqvN2BUtRI\u0024I\u0024, this.\u0023\u003DzXfO9DgaVRj7B.\u0023\u003DzsqzVNAysbdO6\u0024LhbKg\u003D\u003D(_param1), this._xAxisCount);
  }

  public IList<double> \u0023\u003Dz9\u0024EVq3uRz9gb()
  {
    return (IList<double>) new \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrSUX0fLOsOZdqgBUYOpk6V\u0024<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>.\u0023\u003DzabwcW4v2z9DE(this.\u0023\u003DzqvN2BUtRI\u0024I\u0024, this.\u0023\u003DzXfO9DgaVRj7B.\u0023\u003Dz4vbsSxWXNPZJ(), this._xAxisCount);
  }

  private sealed class \u0023\u003DzabwcW4v2z9DE : 
    IEnumerable,
    IList<double>,
    ICollection<double>,
    IEnumerable<double>
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int \u0023\u003DztUQ677I\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int _xAxisCount;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double[,] \u0023\u003DzYum_He4MtIHN;

    public \u0023\u003DzabwcW4v2z9DE(int _param1, double[,] _param2, int _param3)
    {
      this._xAxisCount = _param3;
      this.\u0023\u003DztUQ677I\u003D = _param1;
      this.\u0023\u003DzYum_He4MtIHN = _param2;
    }

    public int IndexOf(double _param1) => throw new NotImplementedException();

    public void Insert(int _param1, double _param2) => throw new NotImplementedException();

    public void RemoveAt(int _param1) => throw new NotImplementedException();

    public double this[int _param1]
    {
      get => this.\u0023\u003DzYum_He4MtIHN[_param1, this._xAxisCount];
      set => throw new NotImplementedException();
    }

    public void Add(double _param1) => throw new NotImplementedException();

    public void Clear() => throw new NotImplementedException();

    public bool Contains(double _param1) => throw new NotImplementedException();

    public void Clone(double[] _param1, int _param2) => throw new NotImplementedException();

    public int Count => this.\u0023\u003DztUQ677I\u003D;

    public bool IsReadOnly => throw new NotImplementedException();

    public bool Remove(double _param1) => throw new NotImplementedException();

    public IEnumerator<double> GetEnumerator() => throw new NotImplementedException();

    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      throw new NotImplementedException();
    }
  }

  private sealed class \u0023\u003Dzlzy8GZzVmC8F : 
    IEnumerable,
    IList<int>,
    ICollection<int>,
    IEnumerable<int>
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int \u0023\u003DztUQ677I\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int _xAxisCount;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int[,] \u0023\u003DzgRliYKKJGYe4hNlN8A\u003D\u003D;

    public \u0023\u003Dzlzy8GZzVmC8F(int _param1, int[,] _param2, int _param3)
    {
      this._xAxisCount = _param3;
      this.\u0023\u003DztUQ677I\u003D = _param1;
      this.\u0023\u003DzgRliYKKJGYe4hNlN8A\u003D\u003D = _param2;
    }

    public int IndexOf(int _param1) => throw new NotImplementedException();

    public void Insert(int _param1, int _param2) => throw new NotImplementedException();

    public void RemoveAt(int _param1) => throw new NotImplementedException();

    public int this[int _param1]
    {
      get
      {
        return this.\u0023\u003DzgRliYKKJGYe4hNlN8A\u003D\u003D[_param1, this._xAxisCount];
      }
      set => throw new NotImplementedException();
    }

    public void Add(int _param1) => throw new NotImplementedException();

    public void Clear() => throw new NotImplementedException();

    public bool Contains(int _param1) => throw new NotImplementedException();

    public void Clone(int[] _param1, int _param2) => throw new NotImplementedException();

    public int Count => this.\u0023\u003DztUQ677I\u003D;

    public bool IsReadOnly => throw new NotImplementedException();

    public bool Remove(int _param1) => throw new NotImplementedException();

    public IEnumerator<int> GetEnumerator() => throw new NotImplementedException();

    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      throw new NotImplementedException();
    }
  }
}
