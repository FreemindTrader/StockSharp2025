// Decompiled with JetBrains decompiler
// Type: #=z6Nstc7nJIhBo4TjHStFZzVN5Cm5fXKBZqg0aibOWqtqjvKK8et6cWATFdfyGHXuHvNEa$wU=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal sealed class \u0023\u003Dz6Nstc7nJIhBo4TjHStFZzVN5Cm5fXKBZqg0aibOWqtqjvKK8et6cWATFdfyGHXuHvNEa\u0024wU\u003D<T>
{
  private T[] \u0023\u003DzeYBkClQ\u003D;
  private int \u0023\u003DzG2qqjnQ\u003D;
  private int \u0023\u003Dzpu5MoRI\u003D;
  private int \u0023\u003DzHHmkwjoAoMbJ;
  private int \u0023\u003DzMTOJEBE\u003D;

  public \u0023\u003Dz6Nstc7nJIhBo4TjHStFZzVN5Cm5fXKBZqg0aibOWqtqjvKK8et6cWATFdfyGHXuHvNEa\u0024wU\u003D(
    int _param1)
  {
    this.\u0023\u003DzHHmkwjoAoMbJ = _param1;
    this.\u0023\u003DzMTOJEBE\u003D = (1 << _param1) - 1;
    this.\u0023\u003DzeYBkClQ\u003D = new T[1 << _param1];
    this.\u0023\u003Dzpu5MoRI\u003D = 0;
    this.\u0023\u003DzG2qqjnQ\u003D = 0;
  }

  public int \u0023\u003DzlpVGw6E\u003D() => this.\u0023\u003DzG2qqjnQ\u003D;

  public T \u0023\u003DzqOGsK89GmOsq()
  {
    return this.\u0023\u003DzeYBkClQ\u003D[this.\u0023\u003Dzpu5MoRI\u003D & this.\u0023\u003DzMTOJEBE\u003D];
  }

  public void \u0023\u003DzBkxoLC0\u003D(T _param1)
  {
    if (this.\u0023\u003DzG2qqjnQ\u003D == this.\u0023\u003DzeYBkClQ\u003D.Length)
    {
      int num = this.\u0023\u003Dzpu5MoRI\u003D & this.\u0023\u003DzMTOJEBE\u003D;
      ++this.\u0023\u003DzHHmkwjoAoMbJ;
      this.\u0023\u003DzMTOJEBE\u003D = (1 << this.\u0023\u003DzHHmkwjoAoMbJ) - 1;
      T[] destinationArray = new T[1 << this.\u0023\u003DzHHmkwjoAoMbJ];
      Array.Copy((Array) this.\u0023\u003DzeYBkClQ\u003D, num, (Array) destinationArray, 0, this.\u0023\u003DzG2qqjnQ\u003D - num);
      Array.Copy((Array) this.\u0023\u003DzeYBkClQ\u003D, 0, (Array) destinationArray, this.\u0023\u003DzG2qqjnQ\u003D - num, num);
      this.\u0023\u003DzeYBkClQ\u003D = destinationArray;
      this.\u0023\u003Dzpu5MoRI\u003D = 0;
    }
    this.\u0023\u003DzeYBkClQ\u003D[this.\u0023\u003Dzpu5MoRI\u003D + this.\u0023\u003DzG2qqjnQ\u003D++ & this.\u0023\u003DzMTOJEBE\u003D] = _param1;
  }

  public T \u0023\u003DzXHr_BBI\u003D()
  {
    T zH9Hnkng = this.\u0023\u003DzeYBkClQ\u003D[this.\u0023\u003Dzpu5MoRI\u003D & this.\u0023\u003DzMTOJEBE\u003D];
    if (this.\u0023\u003DzG2qqjnQ\u003D <= 0)
      return zH9Hnkng;
    ++this.\u0023\u003Dzpu5MoRI\u003D;
    --this.\u0023\u003DzG2qqjnQ\u003D;
    return zH9Hnkng;
  }
}
