// Decompiled with JetBrains decompiler
// Type: #=z6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_$RkzGkLTdBuqAINYDLZs6uj
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Common;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj(
  DateTime _param1,
  int _param2) : \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D
{
  private readonly SyncObject \u0023\u003DzqaMm3lQ\u003D;
  private readonly Dictionary<double, CandlePriceLevel> \u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D;
  private readonly Dictionary<double, (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal)> \u0023\u003DzCTCYaQHJ0R9odjhJvA\u003D\u003D;
  private readonly int \u0023\u003DzI313B3z6lrj1oSNb8A\u003D\u003D = _param2;
  private readonly DateTime _utcTime = _param1;
  private double? \u0023\u003DzU\u0024pHYLKYv6OFV1n6ndxeCjk\u003D;
  private double \u0023\u003DzzqBTFWij9YGM\u0024sNd08bAy5Q\u003D = double.MinValue;
  private double \u0023\u003Dzrit88Hav9xHPgSKGjMesGrI\u003D = double.MaxValue;
  private double \u0023\u003DzuxTw7qKKg5S5JhQCyYjYZck\u003D;

  public int \u0023\u003DzCMB4T5w\u003D() => this.\u0023\u003DzI313B3z6lrj1oSNb8A\u003D\u003D;

  public DateTime \u0023\u003Dzg86amuQ\u003D()
  {
    return this._utcTime;
  }

  public double? \u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D()
  {
    return this.\u0023\u003DzU\u0024pHYLKYv6OFV1n6ndxeCjk\u003D;
  }

  private void \u0023\u003DzQRRGgC1gl1\u0024KtIDeRw\u003D\u003D(double? _param1)
  {
    this.\u0023\u003DzU\u0024pHYLKYv6OFV1n6ndxeCjk\u003D = _param1;
  }

  public double \u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D()
  {
    return this.\u0023\u003DzzqBTFWij9YGM\u0024sNd08bAy5Q\u003D;
  }

  private void \u0023\u003DzI4D409H57hc0_0cc5w\u003D\u003D(double _param1)
  {
    this.\u0023\u003DzzqBTFWij9YGM\u0024sNd08bAy5Q\u003D = _param1;
  }

  public double \u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D()
  {
    return this.\u0023\u003Dzrit88Hav9xHPgSKGjMesGrI\u003D;
  }

  private void \u0023\u003DzZQRSGbwz8FhjSa00Mw\u003D\u003D(double _param1)
  {
    this.\u0023\u003Dzrit88Hav9xHPgSKGjMesGrI\u003D = _param1;
  }

  public double \u0023\u003DznrHfMbDuUs5Ac94Iyw\u003D\u003D()
  {
    return this.\u0023\u003DzuxTw7qKKg5S5JhQCyYjYZck\u003D;
  }

  private void \u0023\u003DzHbzFF6xwFdtDn0Qf\u0024A\u003D\u003D(double _param1)
  {
    this.\u0023\u003DzuxTw7qKKg5S5JhQCyYjYZck\u003D = _param1;
  }

  [SpecialName]
  public double \u0023\u003Dz2_4KSTY\u003D() => (double) this.\u0023\u003Dzg86amuQ\u003D().Ticks;

  [SpecialName]
  public double \u0023\u003Dzu7q98_E\u003D()
  {
    return this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D.Count <= 0 ? double.NaN : (this.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D() + this.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D()) / 2.0;
  }

  public void \u0023\u003DznhN7nDY\u003D(double _param1, CandlePriceLevel _param2)
  {
    lock (this.\u0023\u003DzqaMm3lQ\u003D)
    {
      if (((CandlePriceLevel) ref _param2).TotalVolume == 0M)
        return;
      CandlePriceLevel candlePriceLevel;
      if (this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D.TryGetValue(_param1, out candlePriceLevel))
      {
        this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D[_param1] = ((CandlePriceLevel) ref candlePriceLevel).Join(_param2);
      }
      else
      {
        this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D.Add(_param1, _param2);
        this.\u0023\u003DzdgJannK9g8sQ(_param1);
      }
      this.\u0023\u003DzmI7Jw5E1G6nW();
    }
  }

  public void \u0023\u003DzbSuRr0A\u003D(double _param1, CandlePriceLevel _param2)
  {
    lock (this.\u0023\u003DzqaMm3lQ\u003D)
    {
      if (((CandlePriceLevel) ref _param2).TotalVolume == 0M)
        return;
      CandlePriceLevel candlePriceLevel;
      if (this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D.TryGetValue(_param1, out candlePriceLevel))
      {
        if (((CandlePriceLevel) ref candlePriceLevel).TotalVolume == ((CandlePriceLevel) ref _param2).TotalVolume)
          return;
        this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D[_param1] = _param2;
      }
      else
        this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D.Add(_param1, _param2);
      this.\u0023\u003DzdgJannK9g8sQ(_param1);
      this.\u0023\u003DzmI7Jw5E1G6nW();
    }
  }

  private void \u0023\u003DzmI7Jw5E1G6nW()
  {
    this.\u0023\u003DzCTCYaQHJ0R9odjhJvA\u003D\u003D.Clear();
  }

  private void \u0023\u003DzdgJannK9g8sQ(double _param1)
  {
    if (_param1 < this.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D())
      this.\u0023\u003DzZQRSGbwz8FhjSa00Mw\u003D\u003D(_param1);
    if (_param1 > this.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D())
      this.\u0023\u003DzI4D409H57hc0_0cc5w\u003D\u003D(_param1);
    double? nullable = this.\u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D();
    nullable.GetValueOrDefault();
    if (!nullable.HasValue)
      this.\u0023\u003DzQRRGgC1gl1\u0024KtIDeRw\u003D\u003D(new double?(_param1));
    this.\u0023\u003DzHbzFF6xwFdtDn0Qf\u0024A\u003D\u003D(_param1);
  }

  private static double \u0023\u003DzC520uIs\u003D(double _param0, double _param1)
  {
    return Math.Floor(_param0 / _param1) * _param1;
  }

  public CandlePriceLevel \u0023\u003DzHH6Br74\u003D(double _param1, double _param2)
  {
    if (_param2 <= 0.0)
      throw new ArgumentOutOfRangeException(XXX.SSS(-539436960), (object) _param2, LocalizedStrings.InvalidValue);
    lock (this.\u0023\u003DzqaMm3lQ\u003D)
    {
      \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D ww2IeiAf0VerjZev4 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D();
      (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal) tuple;
      if (this.\u0023\u003DzCTCYaQHJ0R9odjhJvA\u003D\u003D.TryGetValue(_param2, out tuple))
        return CollectionHelper.TryGetValue<double, CandlePriceLevel>((IDictionary<double, CandlePriceLevel>) tuple.Item2, \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003DzC520uIs\u003D(_param1, _param2));
      ww2IeiAf0VerjZev4.\u0023\u003DzyGPsrAMWMEta = \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003DzC520uIs\u003D(_param1, _param2);
      ww2IeiAf0VerjZev4.\u0023\u003DzHRqMWo6vWNAR = ww2IeiAf0VerjZev4.\u0023\u003DzyGPsrAMWMEta + _param2;
      CandlePriceLevel candlePriceLevel3 = new CandlePriceLevel();
      ((CandlePriceLevel) ref candlePriceLevel3).Price = (Decimal) _param1;
      CandlePriceLevel candlePriceLevel2 = candlePriceLevel3;
      foreach ((_, candlePriceLevel3) in this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D.Where<KeyValuePair<double, CandlePriceLevel>>(ww2IeiAf0VerjZev4.\u0023\u003Dzon\u0024_RZacJIPJ ?? (ww2IeiAf0VerjZev4.\u0023\u003Dzon\u0024_RZacJIPJ = new Func<KeyValuePair<double, CandlePriceLevel>, bool>(ww2IeiAf0VerjZev4.\u0023\u003DzXf_ZBuKDXxtPns8flw\u003D\u003D))))
      {
        CandlePriceLevel candlePriceLevel4 = candlePriceLevel3;
        candlePriceLevel2 = ((CandlePriceLevel) ref candlePriceLevel4).Join(candlePriceLevel2);
      }
      return candlePriceLevel2;
    }
  }

  public (KeyValuePair<double, CandlePriceLevel>[], Decimal) \u0023\u003Dzb5KHU\u00247RutjHsWssog\u003D\u003D(
    double _param1)
  {
    if (_param1 <= 0.0)
      throw new ArgumentOutOfRangeException(XXX.SSS(-539436939), (object) _param1, LocalizedStrings.InvalidValue);
    lock (this.\u0023\u003DzqaMm3lQ\u003D)
    {
      (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal) tuple;
      if (this.\u0023\u003DzCTCYaQHJ0R9odjhJvA\u003D\u003D.TryGetValue(_param1, out tuple))
        return (tuple.Item1, tuple.Item3);
      Decimal num1 = 0M;
      SortedDictionary<double, CandlePriceLevel> source = new SortedDictionary<double, CandlePriceLevel>();
      foreach ((double key1, CandlePriceLevel candlePriceLevel1) in this.\u0023\u003Dzmpn7D6HOiDmMk8BUKATzwUw\u003D)
      {
        double num2 = _param1;
        double key2 = \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003DzC520uIs\u003D(key1, num2);
        CandlePriceLevel candlePriceLevel2;
        if (source.TryGetValue(key2, out candlePriceLevel2))
        {
          source[key2] = ((CandlePriceLevel) ref candlePriceLevel2).Join(candlePriceLevel1);
          num1 = MathHelper.Max(num1, ((CandlePriceLevel) ref candlePriceLevel2).TotalVolume + ((CandlePriceLevel) ref candlePriceLevel1).TotalVolume);
        }
        else
        {
          source[key2] = candlePriceLevel1;
          num1 = MathHelper.Max(num1, ((CandlePriceLevel) ref candlePriceLevel1).TotalVolume);
        }
      }
      KeyValuePair<double, CandlePriceLevel>[] array = source.ToArray<KeyValuePair<double, CandlePriceLevel>>();
      if (this.\u0023\u003DzCTCYaQHJ0R9odjhJvA\u003D\u003D.Count == 5)
        this.\u0023\u003DzCTCYaQHJ0R9odjhJvA\u003D\u003D.Clear();
      this.\u0023\u003DzCTCYaQHJ0R9odjhJvA\u003D\u003D[_param1] = (array, source, num1);
      return (array, num1);
    }
  }

  public static (double, double) \u0023\u003Dz\u0024zWmmGTAbDON(
    IEnumerable<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> _param0)
  {
    double num1 = double.MaxValue;
    double num2 = double.MinValue;
    foreach (\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj ltdBuqAinydlZs6uj in _param0)
    {
      if (ltdBuqAinydlZs6uj.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() < num1)
        num1 = ltdBuqAinydlZs6uj.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D();
      if (ltdBuqAinydlZs6uj.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D() > num2)
        num2 = ltdBuqAinydlZs6uj.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D();
    }
    return (num1, num2);
  }

  private sealed class \u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D
  {
    public double \u0023\u003DzyGPsrAMWMEta;
    public double \u0023\u003DzHRqMWo6vWNAR;
    public Func<KeyValuePair<double, CandlePriceLevel>, bool> \u0023\u003Dzon\u0024_RZacJIPJ;

    internal bool \u0023\u003DzXf_ZBuKDXxtPns8flw\u003D\u003D(
      KeyValuePair<double, CandlePriceLevel> _param1)
    {
      return _param1.Key >= this.\u0023\u003DzyGPsrAMWMEta && _param1.Key < this.\u0023\u003DzHRqMWo6vWNAR;
    }
  }
}
