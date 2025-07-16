// Decompiled with JetBrains decompiler
// Type: #=zY$Fbb9SLuCh5D6t_XKcJqEwEg9j_x8XOiyOqAPcZI6sx0QNvBw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqEwEg9j_x8XOiyOqAPcZI6sx0QNvBw\u003D\u003D : 
  \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDO3VFM5XbySLODko9bHLvrDkMuy0qw\u003D\u003D
{
  private TickCoordinates \u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D;
  private IRange \u0023\u003DzQpx\u0024u4Y\u0024rfZ\u0024;
  private double \u0023\u003DzwSMW5jQsRSmc;

  public override TickCoordinates \u0023\u003DzU4j4bt2YhYuc(
    double[] _param1,
    double[] _param2)
  {
    bool flag1 = !this.\u0023\u003DzHZDgUSdfqmkx().VisibleRange.Equals((object) this.\u0023\u003DzQpx\u0024u4Y\u0024rfZ\u0024);
    bool flag2 = this.\u0023\u003DzHZDgUSdfqmkx().ActualWidth.CompareTo(this.\u0023\u003DzwSMW5jQsRSmc) != 0;
    if (this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D.\u0023\u003DzE9zGTWQAFP9k() | flag2)
      this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D = base.\u0023\u003DzU4j4bt2YhYuc(_param1, _param2);
    else if (flag1)
      this.\u0023\u003DzMRbc7eZGFu0n(this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D);
    else
      this.\u0023\u003DzPQmsxQlX6LPB(this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D);
    this.\u0023\u003DzQpx\u0024u4Y\u0024rfZ\u0024 = this.\u0023\u003DzHZDgUSdfqmkx().VisibleRange;
    this.\u0023\u003DzwSMW5jQsRSmc = this.\u0023\u003DzHZDgUSdfqmkx().ActualWidth;
    return this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D;
  }

  private void \u0023\u003DzMRbc7eZGFu0n(
    TickCoordinates _param1)
  {
    if (this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() == null)
      return;
    for (int index = 0; index < _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D().Length; ++index)
    {
      IComparable comparable = this.\u0023\u003DzHZDgUSdfqmkx().GetDataValue((double) _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D()[index]);
      _param1.\u0023\u003Dza3zX1a5AAgFk()[index] = comparable.ToDouble();
    }
    for (int index = 0; index < _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D().Length; ++index)
    {
      IComparable comparable = this.\u0023\u003DzHZDgUSdfqmkx().GetDataValue((double) _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D()[index]);
      _param1.\u0023\u003Dzyqh0CrzbJnzy()[index] = comparable.ToDouble();
    }
  }

  private void \u0023\u003DzPQmsxQlX6LPB(
    TickCoordinates _param1)
  {
    if (this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() == null)
      return;
    for (int index = 0; index < _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D().Length; ++index)
    {
      float num = (float) this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003DzhL6gsJw\u003D((IComparable) _param1.\u0023\u003Dza3zX1a5AAgFk()[index]);
      _param1.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D()[index] = num;
    }
    for (int index = 0; index < _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D().Length; ++index)
    {
      float num = (float) this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003DzhL6gsJw\u003D((IComparable) _param1.\u0023\u003Dzyqh0CrzbJnzy()[index]);
      _param1.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D()[index] = num;
    }
  }
}
