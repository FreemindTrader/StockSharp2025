// Decompiled with JetBrains decompiler
// Type: #=zi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Common;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D : 
  \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D
{
  private readonly AbstractList<ChartDrawData.\u0023\u003DzU3TaXFs\u003D> \u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D = new AbstractList<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>(1);
  private double \u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D = double.NaN;
  private double \u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D = double.NaN;
  private readonly DateTime \u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;

  public \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D(
    DateTime _param1)
  {
    this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D = _param1;
  }

  public bool \u0023\u003DzP41gPT1crtmIqAWIKA\u003D\u003D()
  {
    return this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.Count == 0;
  }

  public IEnumerable<ChartDrawData.\u0023\u003DzU3TaXFs\u003D> \u0023\u003DzH969P7bf3uKN()
  {
    return (IEnumerable<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D;
  }

  public DateTime \u0023\u003Dzg86amuQ\u003D()
  {
    return this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
  }

  public double \u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D()
  {
    this.\u0023\u003Dz2CmIkXghdxY9();
    return this.\u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D;
  }

  public double \u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D()
  {
    this.\u0023\u003Dz2CmIkXghdxY9();
    return this.\u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D;
  }

  [SpecialName]
  public double \u0023\u003Dz2_4KSTY\u003D() => (double) this.\u0023\u003Dzg86amuQ\u003D().Ticks;

  [SpecialName]
  public double \u0023\u003Dzu7q98_E\u003D()
  {
    return this.\u0023\u003DzP41gPT1crtmIqAWIKA\u003D\u003D() ? double.NaN : (this.\u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D + this.\u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D) / 2.0;
  }

  private void \u0023\u003Dz2CmIkXghdxY9()
  {
    if (!MathHelper.IsNaN(this.\u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D))
      return;
    double num1 = double.MinValue;
    double num2 = double.MaxValue;
    using (AbstractList<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>.\u0023\u003DzdFhhG7w\u003D zdFhhG7w = this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.\u0023\u003DzRPOJ5g0\u003D())
    {
      while (zdFhhG7w.MoveNext())
      {
        ChartDrawData.\u0023\u003DzU3TaXFs\u003D current = zdFhhG7w.Current;
        if (current.\u0023\u003DzbH5YDNBwpnry() < num2)
          num2 = current.\u0023\u003DzbH5YDNBwpnry();
        if (current.\u0023\u003DzbH5YDNBwpnry() > num1)
          num1 = current.\u0023\u003DzbH5YDNBwpnry();
      }
    }
    this.\u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D = num2;
    this.\u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D = num1;
  }

  private void \u0023\u003DzX_vlX8BvHc_c()
  {
    this.\u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D = this.\u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D = double.NaN;
  }

  public void \u0023\u003DzDjRgWj_juivV(ChartDrawData.\u0023\u003DzU3TaXFs\u003D _param1)
  {
    \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D v4vdZv8GtEzAmB0rzFq = new \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DztMHJ9Rr5MdWR = _param1;
    int num = CollectionHelper.IndexOf<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>((IEnumerable<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D, new Func<ChartDrawData.\u0023\u003DzU3TaXFs\u003D, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzxj7eJsGR0DW\u0024XsrR5Q\u003D\u003D));
    if (num < 0)
      this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.Add(v4vdZv8GtEzAmB0rzFq.\u0023\u003DztMHJ9Rr5MdWR);
    else
      this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D[num] = v4vdZv8GtEzAmB0rzFq.\u0023\u003DztMHJ9Rr5MdWR;
    this.\u0023\u003DzX_vlX8BvHc_c();
  }

  public ChartDrawData.\u0023\u003DzU3TaXFs\u003D \u0023\u003DziY1yn8o8LADVXLk8uw\u003D\u003D(
    double _param1,
    double _param2)
  {
    if (_param2 < 0.0)
      throw new ArgumentException("maxDiff must be non-negative");
    if (this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.Count == 0)
      return (ChartDrawData.\u0023\u003DzU3TaXFs\u003D) null;
    this.\u0023\u003Dz2CmIkXghdxY9();
    double num1 = double.MaxValue;
    ChartDrawData.\u0023\u003DzU3TaXFs\u003D zU3TaXfs = (ChartDrawData.\u0023\u003DzU3TaXFs\u003D) null;
    using (AbstractList<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>.\u0023\u003DzdFhhG7w\u003D zdFhhG7w = this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.\u0023\u003DzRPOJ5g0\u003D())
    {
      while (zdFhhG7w.MoveNext())
      {
        ChartDrawData.\u0023\u003DzU3TaXFs\u003D current = zdFhhG7w.Current;
        double num2 = Math.Abs(current.\u0023\u003DzbH5YDNBwpnry() - _param1);
        if (num2 < num1)
        {
          num1 = num2;
          zU3TaXfs = current;
        }
      }
    }
    return num1 > _param2 ? (ChartDrawData.\u0023\u003DzU3TaXFs\u003D) null : zU3TaXfs;
  }

  public static void \u0023\u003Dz\u0024zWmmGTAbDON(
    IEnumerable<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D> _param0,
    out double _param1,
    out double _param2)
  {
    _param1 = double.MaxValue;
    _param2 = double.MinValue;
    foreach (\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D rvIvnoSs0Uo7ic5Jw in _param0.Where<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>(\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D ?? (\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D = new Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, bool>(\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzpYOQEE2WuwrooUwCYwN0rbw\u003D))))
    {
      if (rvIvnoSs0Uo7ic5Jw.\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D() < _param1)
        _param1 = rvIvnoSs0Uo7ic5Jw.\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D();
      if (rvIvnoSs0Uo7ic5Jw.\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D() > _param2)
        _param2 = rvIvnoSs0Uo7ic5Jw.\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D();
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.SomeClass34343383();
    public static Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, bool> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;

    internal bool \u0023\u003DzpYOQEE2WuwrooUwCYwN0rbw\u003D(
      \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D _param1)
    {
      return _param1 != null;
    }
  }

  private sealed class \u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D
  {
    public ChartDrawData.\u0023\u003DzU3TaXFs\u003D \u0023\u003DztMHJ9Rr5MdWR;

    internal bool \u0023\u003Dzxj7eJsGR0DW\u0024XsrR5Q\u003D\u003D(
      ChartDrawData.\u0023\u003DzU3TaXFs\u003D _param1)
    {
      return _param1.\u0023\u003DzDmTtC9WghRFa() == this.\u0023\u003DztMHJ9Rr5MdWR.\u0023\u003DzDmTtC9WghRFa();
    }
  }
}
