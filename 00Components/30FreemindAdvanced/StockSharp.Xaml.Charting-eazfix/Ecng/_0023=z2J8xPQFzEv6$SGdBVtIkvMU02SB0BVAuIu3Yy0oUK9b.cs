// Decompiled with JetBrains decompiler
// Type: #=z2J8xPQFzEv6$SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable enable
public sealed class \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D : 
  IPointSeries
{
  private readonly 
  #nullable disable
  \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] \u0023\u003DzJGH6M80\u003D;
  private readonly DoubleRange \u0023\u003Dz6w5dj1Plgc_m;
  private \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003Dz3s80TGV4qJM\u0024;
  private \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzU26lnNrA_u4g;
  private readonly Dictionary<double, CandlePriceLevel> \u0023\u003DzP4lbWAj_E1MXaCiGd7U\u0024TJw\u003D;
  private readonly IEnumerable<double> \u0023\u003DzKpF8UOqXDBb3yFmWUsLipO5D2P2j;
  private readonly double \u0023\u003Dz97RDdiI3O4NqwPctSvrh_bI\u003D;
  private readonly IndexRange  \u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D;
  private readonly IndexRange  \u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D;

  public \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj[] _param1,
    IndexRange  _param2,
    IRange _param3,
    double _param4)
  {
    \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.\u0023\u003Dz5NCozMpa4F4mcyQlZrFJfRI\u003D mpa4F4mcyQlZrFjfRi = new \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.\u0023\u003Dz5NCozMpa4F4mcyQlZrFJfRI\u003D();
    mpa4F4mcyQlZrFjfRi.\u0023\u003DzgSMmZSBP2RDX = _param4;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    mpa4F4mcyQlZrFjfRi._variableSome3535 = this;
    this.\u0023\u003Dz97RDdiI3O4NqwPctSvrh_bI\u003D = mpa4F4mcyQlZrFjfRi.\u0023\u003DzgSMmZSBP2RDX;
    this.\u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D = (IndexRange ) _param2.Clone();
    if (!(_param3 is IndexRange  g8Oq2rGx6KyfAreq))
      g8Oq2rGx6KyfAreq = _param2;
    this.\u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D = g8Oq2rGx6KyfAreq;
    this.\u0023\u003DzJGH6M80\u003D = new \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[_param2.Max - _param2.Min + 1];
    for (int min = _param2.Min; min <= _param2.Max; ++min)
    {
      \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj ltdBuqAinydlZs6uj = _param1[min];
      this.\u0023\u003DzJGH6M80\u003D[min - _param2.Min] = new \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB(ltdBuqAinydlZs6uj, (double) min);
    }
    double num1 = double.MaxValue;
    double num2 = double.MinValue;
    foreach (\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB j9sJkRf4wMmhD3hB in this.\u0023\u003DzJGH6M80\u003D)
    {
      if (j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() < num1)
        num1 = j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D();
      if (j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D() > num2)
        num2 = j9sJkRf4wMmhD3hB.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D();
    }
    this.\u0023\u003Dz6w5dj1Plgc_m = new DoubleRange(num1, num2);
    HashSet<double> doubleSet1 = new HashSet<double>();
    foreach ((double key, CandlePriceLevel candlePriceLevel1) in ((IEnumerable<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>) this.\u0023\u003Dz_xjf3ZVIHzP_()).Where<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>(mpa4F4mcyQlZrFjfRi.\u0023\u003Dzon\u0024_RZacJIPJ ?? (mpa4F4mcyQlZrFjfRi.\u0023\u003Dzon\u0024_RZacJIPJ = new Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, bool>(mpa4F4mcyQlZrFjfRi.\u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D))).SelectMany<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, KeyValuePair<double, CandlePriceLevel>>(mpa4F4mcyQlZrFjfRi.\u0023\u003DzuAeZVTPDgzYE ?? (mpa4F4mcyQlZrFjfRi.\u0023\u003DzuAeZVTPDgzYE = new Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, IEnumerable<KeyValuePair<double, CandlePriceLevel>>>(mpa4F4mcyQlZrFjfRi.OnChartAreaElementsRemovingAt))))
    {
      doubleSet1.Add(key);
      CandlePriceLevel candlePriceLevel2;
      if (this.\u0023\u003DzP4lbWAj_E1MXaCiGd7U\u0024TJw\u003D.TryGetValue(key, out candlePriceLevel2))
        this.\u0023\u003DzP4lbWAj_E1MXaCiGd7U\u0024TJw\u003D[key] = ((CandlePriceLevel) ref candlePriceLevel2).Join(candlePriceLevel1);
      else
        this.\u0023\u003DzP4lbWAj_E1MXaCiGd7U\u0024TJw\u003D.Add(key, candlePriceLevel1);
    }
    HashSet<double> doubleSet2 = doubleSet1;
    int index = 0;
    double[] numArray = new double[doubleSet2.Count];
    foreach (double num3 in doubleSet2)
    {
      numArray[index] = num3;
      ++index;
    }
    this.\u0023\u003DzKpF8UOqXDBb3yFmWUsLipO5D2P2j = (IEnumerable<double>) new \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<double>(numArray);
  }

  [SpecialName]
  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzwQnyySN6xaVC()
  {
    return this.\u0023\u003Dz3s80TGV4qJM\u0024 ?? (this.\u0023\u003Dz3s80TGV4qJM\u0024 = (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double>) new AbstractList<double>(((IEnumerable<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>) this.\u0023\u003DzJGH6M80\u003D).Select<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, double>(\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383.\u0023\u003Dzbt6wp4trpUeqHhxsrQ\u003D\u003D ?? (\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383.\u0023\u003Dzbt6wp4trpUeqHhxsrQ\u003D\u003D = new Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, double>(\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzOePUav1PfFtlcVZNiBhoHIg\u003D)))));
  }

  [SpecialName]
  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzPqsSI6C5MOOb()
  {
    return this.\u0023\u003DzU26lnNrA_u4g ?? (this.\u0023\u003DzU26lnNrA_u4g = (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double>) new AbstractList<double>(((IEnumerable<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>) this.\u0023\u003DzJGH6M80\u003D).Select<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, double>(\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D ?? (\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D = new Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, double>(\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003Dz0nru\u00248Fswh\u0024ovgKXz5qhZKM\u003D)))));
  }

  public \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] \u0023\u003Dz_xjf3ZVIHzP_()
  {
    return this.\u0023\u003DzJGH6M80\u003D;
  }

  protected \u0023\u003Dzro0Io1hfSw7LlH634iIk6DImkX90fd6hXMUYrBvYe4GoWtElsg\u003D\u003D<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB> \u0023\u003DzW0U7cMFYv2v\u0024()
  {
    return (\u0023\u003Dzro0Io1hfSw7LlH634iIk6DImkX90fd6hXMUYrBvYe4GoWtElsg\u003D\u003D<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>) new \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_gJxGaJZchmfb0m64fuXudqUOLrxxQ\u003D\u003D<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB>(this.\u0023\u003DzJGH6M80\u003D);
  }

  [SpecialName]
  public int \u0023\u003DzlpVGw6E\u003D() => this.\u0023\u003DzJGH6M80\u003D.Length;

  public IEnumerable<double> \u0023\u003DzQJbf5e\u0024oTzuvDw2moQ\u003D\u003D()
  {
    return this.\u0023\u003DzKpF8UOqXDBb3yFmWUsLipO5D2P2j;
  }

  [IndexerName("#=zMRIb09I=")]
  public \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D this[int _param1]
  {
    get
    {
      return (\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D) this.\u0023\u003DzJGH6M80\u003D[_param1];
    }
  }

  public double \u0023\u003DzTmtGqP_rl3YU6gjEDQ\u003D\u003D()
  {
    return this.\u0023\u003Dz97RDdiI3O4NqwPctSvrh_bI\u003D;
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

  public CandlePriceLevel \u0023\u003DzydYW1JHaibII1h_UgQ\u003D\u003D(double _param1)
  {
    CandlePriceLevel candlePriceLevel;
    this.\u0023\u003DzP4lbWAj_E1MXaCiGd7U\u0024TJw\u003D.TryGetValue(_param1, out candlePriceLevel);
    return candlePriceLevel;
  }

  private sealed class \u0023\u003Dz5NCozMpa4F4mcyQlZrFJfRI\u003D
  {
    public \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D _variableSome3535;
    public double \u0023\u003DzgSMmZSBP2RDX;
    public Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, bool> \u0023\u003Dzon\u0024_RZacJIPJ;
    public Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, 
    #nullable enable
    IEnumerable<KeyValuePair<double, CandlePriceLevel>>> \u0023\u003DzuAeZVTPDgzYE;

    public bool \u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D(
      #nullable disable
      \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB _param1)
    {
      return _param1.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D() >= this._variableSome3535.VisibleRange.Min && _param1.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzCMB4T5w\u003D() <= this._variableSome3535.VisibleRange.Max;
    }

    public 
    #nullable enable
    IEnumerable<KeyValuePair<double, CandlePriceLevel>> OnChartAreaElementsRemovingAt(
      #nullable disable
      \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB _param1)
    {
      return (IEnumerable<KeyValuePair<double, CandlePriceLevel>>) _param1.\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dzb5KHU\u00247RutjHsWssog\u003D\u003D(this.\u0023\u003DzgSMmZSBP2RDX).Item1;
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D.SomeClass34343383();
    public static Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, double> \u0023\u003Dzbt6wp4trpUeqHhxsrQ\u003D\u003D;
    public static Func<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB, double> \u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D;

    public double \u0023\u003DzOePUav1PfFtlcVZNiBhoHIg\u003D(
      \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB _param1)
    {
      return _param1.\u0023\u003Dz2_4KSTY\u003D();
    }

    public double \u0023\u003Dz0nru\u00248Fswh\u0024ovgKXz5qhZKM\u003D(
      \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB _param1)
    {
      return _param1.\u0023\u003Dzu7q98_E\u003D();
    }
  }
}
