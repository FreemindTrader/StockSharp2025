// Decompiled with JetBrains decompiler
// Type: #=zdkTsoRIhz16dAJ0Ha_QZUs$JSMgoXEdmnE5TCobCGwh7srNhlw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D : 
  IPointSeries
{
  private readonly \u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D[] \u0023\u003DzYw05nwk\u003D;
  private readonly DoubleRange \u0023\u003Dz6w5dj1Plgc_m;
  private \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003Dz3s80TGV4qJM\u0024;
  private \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzU26lnNrA_u4g;
  private readonly IndexRange  \u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D;
  private readonly IndexRange  \u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D;

  public \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D(
    \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D[] _param1,
    IndexRange  _param2,
    IRange _param3)
  {
    this.\u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D = (IndexRange ) _param2.Clone();
    if (!(_param3 is IndexRange  g8Oq2rGx6KyfAreq))
      g8Oq2rGx6KyfAreq = _param2;
    this.\u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D = g8Oq2rGx6KyfAreq;
    this.\u0023\u003DzYw05nwk\u003D = new \u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D[_param2.Max - _param2.Min + 1];
    int num1 = Math.Max(0, _param2.Min);
    int num2 = Math.Min(_param1.Length - 1, _param2.Max);
    for (int index = num1; index <= num2; ++index)
    {
      \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D rvIvnoSs0Uo7ic5Jw = _param1[index];
      this.\u0023\u003DzYw05nwk\u003D[index - _param2.Min] = new \u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D(rvIvnoSs0Uo7ic5Jw, index);
    }
    double num3 = double.MaxValue;
    double num4 = double.MinValue;
    foreach (\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D y0XZklPx9ZIbrBkg in this.\u0023\u003DzYw05nwk\u003D)
    {
      if (y0XZklPx9ZIbrBkg.\u0023\u003Dz2TNMZ47XeEe8() != null)
      {
        if (y0XZklPx9ZIbrBkg.\u0023\u003Dz2TNMZ47XeEe8().\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D() < num3)
          num3 = y0XZklPx9ZIbrBkg.\u0023\u003Dz2TNMZ47XeEe8().\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D();
        if (y0XZklPx9ZIbrBkg.\u0023\u003Dz2TNMZ47XeEe8().\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D() > num4)
          num4 = y0XZklPx9ZIbrBkg.\u0023\u003Dz2TNMZ47XeEe8().\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D();
      }
    }
    this.\u0023\u003Dz6w5dj1Plgc_m = new DoubleRange(num3, num4);
  }

  [SpecialName]
  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzwQnyySN6xaVC()
  {
    return this.\u0023\u003Dz3s80TGV4qJM\u0024 ?? (this.\u0023\u003Dz3s80TGV4qJM\u0024 = (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double>) new AbstractList<double>(((IEnumerable<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D>) this.\u0023\u003DzYw05nwk\u003D).Select<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D, double>(\u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383._public_static_Func_Order_bool_001 ?? (\u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383._public_static_Func_Order_bool_001 = new Func<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D, double>(\u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003Dzp2\u0024EsxlIMTtWjmvGLH7fWK4\u003D)))));
  }

  [SpecialName]
  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzPqsSI6C5MOOb()
  {
    return this.\u0023\u003DzU26lnNrA_u4g ?? (this.\u0023\u003DzU26lnNrA_u4g = (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double>) new AbstractList<double>(((IEnumerable<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D>) this.\u0023\u003DzYw05nwk\u003D).Select<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D, double>(\u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D ?? (\u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D = new Func<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D, double>(\u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzucPTYZfZR1DykS2JQTABmho\u003D)))));
  }

  public \u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D[] \u0023\u003Dz9I1m\u0024aaHezaI()
  {
    return this.\u0023\u003DzYw05nwk\u003D;
  }

  [SpecialName]
  public int \u0023\u003DzlpVGw6E\u003D() => this.\u0023\u003DzYw05nwk\u003D.Length;

  [IndexerName("#=zMRIb09I=")]
  public IPoint this[int _param1]
  {
    get
    {
      return (IPoint) this.\u0023\u003DzYw05nwk\u003D[_param1];
    }
  }

  public IndexRange  \u0023\u003Dz5O4ly_BelNuL()
  {
    return this.\u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D;
  }

  public IndexRange  VisibleRange
  {
    get => this.\u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D;
  }

  public DoubleRange \u0023\u003DzxNQHuqrEvxH2()
  {
    return this.\u0023\u003Dz6w5dj1Plgc_m;
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D.SomeClass34343383();
    public static Func<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D, double> _public_static_Func_Order_bool_001;
    public static Func<\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D, double> \u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D;

    public double \u0023\u003Dzp2\u0024EsxlIMTtWjmvGLH7fWK4\u003D(
      \u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D _param1)
    {
      return _param1.X;
    }

    public double \u0023\u003DzucPTYZfZR1DykS2JQTABmho\u003D(
      \u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3mT6zCFc5JN2Y0_xZklPX9Z\u0024IbrBkg\u003D\u003D _param1)
    {
      return _param1.Y;
    }
  }
}
