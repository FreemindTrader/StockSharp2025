// Decompiled with JetBrains decompiler
// Type: #=zi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
  private readonly \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<ChartDrawData.sTrade> \u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D = new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<ChartDrawData.sTrade>(1);
  private double \u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D = double.NaN;
  private double \u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D = double.NaN;
  private readonly DateTime _utcTime;

  public \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D(
    DateTime _param1)
  {
    this._utcTime = _param1;
  }

  public bool \u0023\u003DzP41gPT1crtmIqAWIKA\u003D\u003D()
  {
    return this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.Count == 0;
  }

  public IEnumerable<ChartDrawData.sTrade> \u0023\u003DzH969P7bf3uKN()
  {
    return (IEnumerable<ChartDrawData.sTrade>) this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D;
  }

  public DateTime UtcTime()
  {
    return this._utcTime;
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
  public double Property() => (double) this.UtcTime().Ticks;

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
    using (\u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<ChartDrawData.sTrade>.\u0023\u003DzdFhhG7w\u003D zdFhhG7w = this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.\u0023\u003DzRPOJ5g0\u003D())
    {
      while (zdFhhG7w.MoveNext())
      {
        ChartDrawData.sTrade current = zdFhhG7w.Current;
        if (current.Price() < num2)
          num2 = current.Price();
        if (current.Price() > num1)
          num1 = current.Price();
      }
    }
    this.\u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D = num2;
    this.\u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D = num1;
  }

  private void \u0023\u003DzX_vlX8BvHc_c()
  {
    this.\u0023\u003DzTLYhkIK\u0024cpPpvOmoiA\u003D\u003D = this.\u0023\u003DzC8VtIhwQeL9NZ9wdrA\u003D\u003D = double.NaN;
  }

  public void \u0023\u003DzDjRgWj_juivV(ChartDrawData.sTrade _param1)
  {
    \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D v4vdZv8GtEzAmB0rzFq = new \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DztMHJ9Rr5MdWR = _param1;
    int num = CollectionHelper.IndexOf<ChartDrawData.sTrade>((IEnumerable<ChartDrawData.sTrade>) this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D, new Func<ChartDrawData.sTrade, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzxj7eJsGR0DW\u0024XsrR5Q\u003D\u003D));
    if (num < 0)
      this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.Add(v4vdZv8GtEzAmB0rzFq.\u0023\u003DztMHJ9Rr5MdWR);
    else
      this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D[num] = v4vdZv8GtEzAmB0rzFq.\u0023\u003DztMHJ9Rr5MdWR;
    this.\u0023\u003DzX_vlX8BvHc_c();
  }

  public ChartDrawData.sTrade \u0023\u003DziY1yn8o8LADVXLk8uw\u003D\u003D(
    double _param1,
    double _param2)
  {
    if (_param2 < 0.0)
      throw new ArgumentException("");
    if (this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.Count == 0)
      return (ChartDrawData.sTrade) null;
    this.\u0023\u003Dz2CmIkXghdxY9();
    double num1 = double.MaxValue;
    ChartDrawData.sTrade zU3TaXfs = (ChartDrawData.sTrade) null;
    using (\u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<ChartDrawData.sTrade>.\u0023\u003DzdFhhG7w\u003D zdFhhG7w = this.\u0023\u003DzpM2i_gRuePvm8kUTJXT7RYg\u003D.\u0023\u003DzRPOJ5g0\u003D())
    {
      while (zdFhhG7w.MoveNext())
      {
        ChartDrawData.sTrade current = zdFhhG7w.Current;
        double num2 = Math.Abs(current.Price() - _param1);
        if (num2 < num1)
        {
          num1 = num2;
          zU3TaXfs = current;
        }
      }
    }
    return num1 > _param2 ? (ChartDrawData.sTrade) null : zU3TaXfs;
  }

  public static void \u0023\u003Dz\u0024zWmmGTAbDON(
    IEnumerable<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D> _param0,
    out double _param1,
    out double _param2)
  {
    _param1 = double.MaxValue;
    _param2 = double.MinValue;
    foreach (\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D rvIvnoSs0Uo7ic5Jw in _param0.Where<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>(\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D ?? (\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D = new Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, bool>(\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzpYOQEE2WuwrooUwCYwN0rbw\u003D))))
    {
      if (rvIvnoSs0Uo7ic5Jw.\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D() < _param1)
        _param1 = rvIvnoSs0Uo7ic5Jw.\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D();
      if (rvIvnoSs0Uo7ic5Jw.\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D() > _param2)
        _param2 = rvIvnoSs0Uo7ic5Jw.\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D();
    }
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, bool> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;

    internal bool \u0023\u003DzpYOQEE2WuwrooUwCYwN0rbw\u003D(
      \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D _param1)
    {
      return _param1 != null;
    }
  }

  private sealed class \u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D
  {
    public ChartDrawData.sTrade \u0023\u003DztMHJ9Rr5MdWR;

    internal bool \u0023\u003Dzxj7eJsGR0DW\u0024XsrR5Q\u003D\u003D(
      ChartDrawData.sTrade _param1)
    {
      return _param1.GetTransactionString() == this.\u0023\u003DztMHJ9Rr5MdWR.GetTransactionString();
    }
  }
}
