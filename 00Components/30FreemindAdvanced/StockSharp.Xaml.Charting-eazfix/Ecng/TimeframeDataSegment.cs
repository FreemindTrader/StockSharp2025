// Decompiled with JetBrains decompiler
// Type: #=z6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_$RkzGkLTdBuqAINYDLZs6uj
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Common;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable disable
public sealed class TimeframeDataSegment(DateTime datetime, int count) : IPoint
{
    public DateTime Time
    {
        get
        {
            return this._dsTimeframe;
        }
    }

    public double MaxPrice
    {
        get
        {
            return this._maxPrice;
        }

        set
        {
            this._maxPrice = value;
        }
    }

    public double MinPrice
    {
        get
        {
            return this._minPrice;
        }

        set
        {
            this._minPrice = value;
        }
    }

    public int Count => this._count;

    private readonly SyncObject _syncObject;
    private readonly Dictionary<double, CandlePriceLevel> _double2CandlePriceLevel;
    private readonly Dictionary<double, (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal)> _complexCandlePriceLevel;
    private readonly int _count = count;
    private readonly DateTime _dsTimeframe = datetime;

    private double _maxPrice = double.MinValue;
    private double _minPrice = double.MaxValue;
    private double \u0023\u003DzuxTw7qKKg5S5JhQCyYjYZck\u003D;
        private double? \u0023\u003DzU\u0024pHYLKYv6OFV1n6ndxeCjk\u003D;
  

  

  public double? \u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D()
  {
    return this.\u0023\u003DzU\u0024pHYLKYv6OFV1n6ndxeCjk\u003D;
  }

private void \u0023\u003DzQRRGgC1gl1\u0024KtIDeRw\u003D\u003D(double? _param1)
  {
    this.\u0023\u003DzU\u0024pHYLKYv6OFV1n6ndxeCjk\u003D = _param1;
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
public double X => (double)this.Time.Ticks;

[SpecialName]
public double Y
{
    return this._double2CandlePriceLevel.Count <= 0 ? double.NaN : (this.MaxPrice + this.MinPrice) / 2.0;
}

public void \u0023\u003DznhN7nDY\u003D(double _param1, CandlePriceLevel _param2)
  {
    lock (this._syncObject)
    {
      if (((CandlePriceLevel) ref _param2).TotalVolume == 0M)
        return;
CandlePriceLevel candlePriceLevel;
if ( this._double2CandlePriceLevel.TryGetValue(_param1, out candlePriceLevel) )
{
    this._double2CandlePriceLevel[_param1] = ( (CandlePriceLevel)ref candlePriceLevel ).Join(_param2);
}
else
{
    this._double2CandlePriceLevel.Add(_param1, _param2);
    this.\u0023\u003DzdgJannK9g8sQ(_param1);
}
this.\u0023\u003DzmI7Jw5E1G6nW();
    }
  }

  public void \u0023\u003DzbSuRr0A\u003D(double _param1, CandlePriceLevel _param2)
  {
    lock (this._syncObject)
    {
      if (((CandlePriceLevel) ref _param2).TotalVolume == 0M)
        return;
CandlePriceLevel candlePriceLevel;
if ( this._double2CandlePriceLevel.TryGetValue(_param1, out candlePriceLevel) )
{
    if ( ( (CandlePriceLevel)ref candlePriceLevel ).TotalVolume == ( (CandlePriceLevel)ref _param2 ).TotalVolume )
        return;
    this._double2CandlePriceLevel[_param1] = _param2;
}
else
    this._double2CandlePriceLevel.Add(_param1, _param2);
this.\u0023\u003DzdgJannK9g8sQ(_param1);
this.\u0023\u003DzmI7Jw5E1G6nW();
    }
  }

  private void \u0023\u003DzmI7Jw5E1G6nW()
  {
    this._complexCandlePriceLevel.Clear();
}

private void \u0023\u003DzdgJannK9g8sQ(double _param1)
  {
    if ( _param1 < this.MinPrice )
        this.MinPrice = ( _param1 );
    if ( _param1 > this.MaxPrice )
        this.MaxPrice = ( _param1 );
    double? nullable = this.\u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D();
    nullable.GetValueOrDefault();
    if ( !nullable.HasValue )
        this.\u0023\u003DzQRRGgC1gl1\u0024KtIDeRw\u003D\u003D( new double?(_param1) );
    this.\u0023\u003DzHbzFF6xwFdtDn0Qf\u0024A\u003D\u003D( _param1 );
}

private static double \u0023\u003DzC520uIs\u003D(double _param0, double _param1)
  {
    return Math.Floor(_param0 / _param1) * _param1;
  }

  public CandlePriceLevel \u0023\u003DzHH6Br74\u003D(double _param1, double _param2)
  {
    if (_param2 <= 0.0)
      throw new ArgumentOutOfRangeException("priceStep", (object)_param2, LocalizedStrings.InvalidValue);
lock ( this._syncObject )
{
    TimeframeDataSegment.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D ww2IeiAf0VerjZev4 = new TimeframeDataSegment.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D();
    (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal) tuple;
    if ( this._complexCandlePriceLevel.TryGetValue(_param2, out tuple) )
        return CollectionHelper.TryGetValue<double, CandlePriceLevel>((IDictionary<double, CandlePriceLevel>)tuple.Item2, TimeframeDataSegment.\u0023\u003DzC520uIs\u003D(_param1, _param2));
    ww2IeiAf0VerjZev4.\u0023\u003DzyGPsrAMWMEta = TimeframeDataSegment.\u0023\u003DzC520uIs\u003D(_param1, _param2);
    ww2IeiAf0VerjZev4.\u0023\u003DzHRqMWo6vWNAR = ww2IeiAf0VerjZev4.\u0023\u003DzyGPsrAMWMEta + _param2;
    CandlePriceLevel candlePriceLevel3 = new CandlePriceLevel();
    ( (CandlePriceLevel)ref candlePriceLevel3 ).Price = (Decimal)_param1;
    CandlePriceLevel candlePriceLevel2 = candlePriceLevel3;
    foreach ( (_, candlePriceLevel3) in this._double2CandlePriceLevel.Where<KeyValuePair<double, CandlePriceLevel>>(ww2IeiAf0VerjZev4._memeber02 ?? ( ww2IeiAf0VerjZev4._memeber02 = new Func<KeyValuePair<double, CandlePriceLevel>, bool>(ww2IeiAf0VerjZev4.\u0023\u003DzXf_ZBuKDXxtPns8flw\u003D\u003D) )) )
    {
        CandlePriceLevel candlePriceLevel4 = candlePriceLevel3;
        candlePriceLevel2 = ( (CandlePriceLevel)ref candlePriceLevel4 ).Join(candlePriceLevel2);
    }
    return candlePriceLevel2;
}
  }

  public (KeyValuePair<double, CandlePriceLevel>[], Decimal) \u0023\u003Dzb5KHU\u00247RutjHsWssog\u003D\u003D(
    double _param1)
  {
    if (_param1 <= 0.0)
      throw new ArgumentOutOfRangeException("priceStep", (object)_param1, LocalizedStrings.InvalidValue);
lock ( this._syncObject )
{
    (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal) tuple;
    if ( this._complexCandlePriceLevel.TryGetValue(_param1, out tuple) )
        return (tuple.Item1, tuple.Item3);
    Decimal num1 = 0M;
    SortedDictionary<double, CandlePriceLevel> source = new SortedDictionary<double, CandlePriceLevel>();
    foreach ( (double key1, CandlePriceLevel candlePriceLevel1) in this._double2CandlePriceLevel )
    {
        double num2 = _param1;
        double key2 = TimeframeDataSegment.\u0023\u003DzC520uIs\u003D(key1, num2);
        CandlePriceLevel candlePriceLevel2;
        if ( source.TryGetValue(key2, out candlePriceLevel2) )
        {
            source[key2] = ( (CandlePriceLevel)ref candlePriceLevel2 ).Join(candlePriceLevel1);
            num1 = MathHelper.Max(num1, ( (CandlePriceLevel)ref candlePriceLevel2 ).TotalVolume + ( (CandlePriceLevel)ref candlePriceLevel1 ).TotalVolume);
        }
        else
        {
            source[key2] = candlePriceLevel1;
            num1 = MathHelper.Max(num1, ( (CandlePriceLevel)ref candlePriceLevel1 ).TotalVolume);
        }
    }
    KeyValuePair<double, CandlePriceLevel>[] array = source.ToArray<KeyValuePair<double, CandlePriceLevel>>();
    if ( this._complexCandlePriceLevel.Count == 5 )
        this._complexCandlePriceLevel.Clear();
    this._complexCandlePriceLevel[_param1] = (array, source, num1);
    return (array, num1);
}
  }

  public static (double, double) \u0023\u003Dz\u0024zWmmGTAbDON(
    IEnumerable<TimeframeDataSegment> _param0)
  {
    double num1 = double.MaxValue;
double num2 = double.MinValue;
foreach ( TimeframeDataSegment ltdBuqAinydlZs6uj in _param0 )
{
    if ( ltdBuqAinydlZs6uj.MinPrice < num1 )
        num1 = ltdBuqAinydlZs6uj.MinPrice;
    if ( ltdBuqAinydlZs6uj.MaxPrice > num2 )
        num2 = ltdBuqAinydlZs6uj.MaxPrice;
}
return (num1, num2);
  }

  private sealed class \u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D
  {
    public double \u0023\u003DzyGPsrAMWMEta;
public double \u0023\u003DzHRqMWo6vWNAR;
public Func<KeyValuePair<double, CandlePriceLevel>, bool> _memeber02;

public bool \u0023\u003DzXf_ZBuKDXxtPns8flw\u003D\u003D(
      KeyValuePair<double, CandlePriceLevel> _param1)
    {
      return _param1.Key >= this.\u0023\u003DzyGPsrAMWMEta && _param1.Key < this.\u0023\u003DzHRqMWo6vWNAR;
    }
  }
}
