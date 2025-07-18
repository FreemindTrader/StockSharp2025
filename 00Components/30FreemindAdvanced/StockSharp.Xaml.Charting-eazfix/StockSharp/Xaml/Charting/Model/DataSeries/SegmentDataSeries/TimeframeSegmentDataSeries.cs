// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries.TimeframeSegmentDataSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries;

public class TimeframeSegmentDataSeries : 
  BindableObject ,
  IDataSeries<DateTime, double>,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  \u0023\u003DztorG3HTUDpMsfjPqFEEe9HUBXNG1JlzD7u56rrBDlcZ3bDSt5iNajas\u003D
{
  public const Decimal MinPriceStep = 0.000001M;
  public static readonly TimeSpan MaxTimeframe = TimeSpan.FromDays(30.0);
  public static readonly IMath<DateTime> XMath = MathHelper.GetMath<DateTime>();
  private string _seriesName;
  private readonly \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj _volumeProfile = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj(new DateTime(), 0);
  private readonly AbstractList<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> _segments = new AbstractList<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>();
  private readonly AbstractList<DateTime> _segmentDates = new AbstractList<DateTime>();
  private readonly TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D _yValues;
  private readonly ChartCandleElement _element;
  private Tuple<DateTime, DateTime, long> _curPeriod;

  public TimeframeSegmentDataSeries(TimeSpan? timeframe, ChartCandleElement element)
  {
    if (timeframe.HasValue && (!(timeframe.Value > TimeSpan.Zero) || !(timeframe.Value <= TimeframeSegmentDataSeries.MaxTimeframe)))
      throw new ArgumentOutOfRangeException(nameof (timeframe));
    this._yValues = new TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D(this);
    this.Timeframe = timeframe;
    this._element = element ?? throw new ArgumentNullException(nameof (element));
  }

  private \u0023\u003Dzro0Io1hfSw7LlH634iIk6DImkX90fd6hXMUYrBvYe4GoWtElsg\u003D\u003D<DateTime> SegmentDates
  {
    get => this._segmentDates.\u0023\u003Dzq2zVESXBEvJM();
  }

  public DataSeriesType DataSeriesType
  {
    get => (DataSeriesType) 9;
  }

  public TimeSpan? Timeframe { get; }

  public event EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> DataSeriesChanged;

  public object SyncRoot { get; } = new object();

  public bool AcceptsUnsortedData
  {
    get => false;
    set
    {
    }
  }

  public ISciChartSurface ParentSurface { get; set; }

  public string SeriesName
  {
    get => this._seriesName;
    set
    {
      this._seriesName = value;
      EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> dataSeriesChanged = this.DataSeriesChanged;
      if (dataSeriesChanged == null)
        return;
      dataSeriesChanged((object) this, new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1));
    }
  }

  public bool HasValues => this._segmentDates.Count > 0;

  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EIsSecondary
  {
    get => false;
  }

  public int Count => this._segmentDates.Count;

  public IComparable YMin => this.YRange.Min;

  public IComparable YMax => this.YRange.Max;

  IComparable \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXMin
  {
    get => this.XRange.Min;
  }

  IComparable \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXMax
  {
    get => this.XRange.Max;
  }

  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EIsFifo
  {
    get => false;
  }

  public bool IsSorted => true;

  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> Segments
  {
    get
    {
      return (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>) this._segments;
    }
  }

  protected \u0023\u003Dzro0Io1hfSw7LlH634iIk6DImkX90fd6hXMUYrBvYe4GoWtElsg\u003D\u003D<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> SegmentsReadOnly
  {
    get => this._segments.\u0023\u003Dzq2zVESXBEvJM();
  }

  public Type XType => typeof (DateTime);

  public Type YType => typeof (double);

  public IComparable LatestYValue => (IComparable) null;

  private IList<DateTime> XValues => (IList<DateTime>) this.SegmentDates;

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXValues
  {
    get => (IList) this.XValues;
  }

  IList<DateTime> IDataSeries<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EXValues
  {
    get => this.XValues;
  }

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EYValues
  {
    get => (IList) this._yValues;
  }

  IList<double> IDataSeries<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EYValues
  {
    get => (IList<double>) this._yValues;
  }

  public void Clear()
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("ERROR: TimeframeSegmentDataSeries.Clear(): not supported", Array.Empty<object>());
  }

  public void InvalidateParentSurface(
    RangeMode rangeMode)
  {
    if (this.ParentSurface == null)
      return;
    switch (rangeMode)
    {
      case (RangeMode) 0:
        this.ParentSurface.InvalidateElement();
        break;
      case (RangeMode) 1:
        this.ParentSurface.ZoomExtents();
        break;
      case (RangeMode) 2:
        this.ParentSurface.ZoomExtentsY();
        break;
    }
  }

  public IndexRange  GetIndicesRange(
    IRange visibleRange)
  {
    return this.GetIndicesRange(visibleRange, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3);
  }

  private IndexRange  GetIndicesRange(
    IRange range,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D downSearchMode,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D upSearchMode)
  {
    IndexRange  indicesRange = new IndexRange (0, -1);
    if (this._segmentDates.Count == 0)
      return indicesRange;
    if (!(range.Clone() is IndexRange  indexRange))
      indexRange = this.SearchDataIndexesOn(range, downSearchMode, upSearchMode);
    return this.NormalizeIndexRange(indexRange);
  }

  IRange \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    IRange xRange)
  {
    IndexRange  g8Oq2rGx6KyfAreq = this.SearchDataIndexesOn(xRange, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3);
    return !g8Oq2rGx6KyfAreq.IsDefined ? (IRange) new DoubleRange(double.MinValue, double.MaxValue) : this.GetWindowedYRange(g8Oq2rGx6KyfAreq);
  }

  IRange \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    IndexRange  indexRange,
    bool getPositiveRange)
  {
    return !indexRange.IsDefined ? (IRange) new DoubleRange(double.MinValue, double.MaxValue) : this.GetWindowedYRange(indexRange);
  }

  IRange \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    IRange xRange,
    bool getPositiveRange)
  {
    return ((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this).\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(xRange);
  }

  public IRange XRange
  {
    get
    {
      if (!this._segmentDates.Any<DateTime>())
        return (IRange) new DoubleRange(double.MinValue, double.MaxValue);
      DateTime segmentDate = this._segmentDates[0];
      AbstractList<DateTime> segmentDates = this._segmentDates;
      DateTime dateTime = segmentDates[segmentDates.Count - 1];
      return (IRange) new DateRange(segmentDate, dateTime).AsDoubleRange();
    }
  }

  public IRange YRange
  {
    get
    {
      if (this._segments.Count == 0)
        return (IRange) new DoubleRange(double.MinValue, double.MaxValue);
      double num1 = double.MaxValue;
      double num2 = double.MinValue;
      using (AbstractList<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>.\u0023\u003DzdFhhG7w\u003D zdFhhG7w = this._segments.\u0023\u003DzRPOJ5g0\u003D())
      {
        while (zdFhhG7w.MoveNext())
        {
          \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj current = zdFhhG7w.Current;
          if (current.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() < num1)
            num1 = current.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D();
          if (current.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D() > num2)
            num2 = current.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D();
        }
      }
      return (IRange) new DoubleRange(num1, num2);
    }
  }

  private IndexRange  SearchDataIndexesOn(
    IRange range,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D downSearchMode,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D upSearchMode)
  {
    if (range is IndexRange  g8Oq2rGx6KyfAreq)
      return (IndexRange ) g8Oq2rGx6KyfAreq.Clone();
    IndexRange  indexRange = new IndexRange (-1, -1);
    DateRange dx26vpI1xWpwwNqJw = !(range is DoubleRange klqcJ87Zm8UwE3WEjd) ? range as DateRange : new DateRange(new DateTime((long) klqcJ87Zm8UwE3WEjd.Min), new DateTime((long) klqcJ87Zm8UwE3WEjd.Max));
    if (dx26vpI1xWpwwNqJw == null)
    {
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("ERROR: SearchDataIndexesOn: unable to convert range type={0}", new object[1]
      {
        (object) range.GetType().Name
      });
      return this.NormalizeIndexRange(indexRange);
    }
    DateTime[] dateTimeArray = this._segmentDates.\u0023\u003DzSWlLd4k\u003D();
    indexRange.Min = dateTimeArray.\u0023\u003DzFH1yjjY\u003D(true, (IComparable) dx26vpI1xWpwwNqJw.Min, downSearchMode);
    indexRange.Max = dateTimeArray.\u0023\u003DzFH1yjjY\u003D(true, (IComparable) dx26vpI1xWpwwNqJw.Max, upSearchMode);
    return this.NormalizeIndexRange(indexRange);
  }

  private IndexRange  NormalizeIndexRange(
    IndexRange  indexRange)
  {
    int count = this._segmentDates.Count;
    if (indexRange.IsDefined)
    {
      if (indexRange.Min > count - 1 || indexRange.Max < 0)
      {
        indexRange.Min = indexRange.Max = 0;
      }
      else
      {
        indexRange.Min = Math.Max(indexRange.Min, 0);
        indexRange.Max = Math.Max(Math.Min(indexRange.Max, count - 1), indexRange.Min);
      }
    }
    if (indexRange.Min.CompareTo(indexRange.Max) > 0)
      indexRange.Min = 0;
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("GetIndicesRange(timeframesegment): Min={0}, Max={1}", new object[2]
    {
      (object) indexRange.Min,
      (object) indexRange.Max
    });
    return indexRange;
  }

  public static Tuple<DateTime, DateTime, long> GetTimeframePeriod(
    DateTime dt,
    TimeSpan period,
    Tuple<DateTime, DateTime, long> prevPeriod = null)
  {
    if (!(period > TimeSpan.Zero) || !(period <= TimeframeSegmentDataSeries.MaxTimeframe))
      throw new ArgumentOutOfRangeException(nameof (period));
    if (prevPeriod != null && dt < prevPeriod.Item2 && dt >= prevPeriod.Item1)
      return prevPeriod;
    long ticks = period.Ticks;
    long num;
    DateTime dateTime1;
    DateTime dateTime2;
    if (period < TimeSpan.FromDays(1.0))
    {
      num = dt.TimeOfDay.Ticks / ticks;
      dateTime1 = dt.Date + TimeSpan.FromTicks(num * ticks);
      dateTime2 = dateTime1 + period;
      if (dateTime2.Day != dateTime1.Day)
        dateTime2 = dateTime2.Date;
    }
    else if (period == TimeSpan.FromDays(1.0))
    {
      dateTime1 = new DateTime(dt.Year, dt.Month, dt.Day);
      dateTime2 = dateTime1 + period;
      num = (long) (dateTime1.DayOfYear - 1);
    }
    else if (period == TimeSpan.FromDays(7.0))
    {
      dateTime1 = new DateTime(dt.Year, dt.Month, dt.Day) - TimeSpan.FromDays((double) dt.DayOfWeek);
      dateTime2 = dateTime1 + period;
      num = (long) dateTime1.WeekNumber();
    }
    else
    {
      if (period > TimeframeSegmentDataSeries.MaxTimeframe)
        throw new NotSupportedException($"Periods more than {TimeframeSegmentDataSeries.MaxTimeframe} days are not supported.");
      DateTime dateTime3 = new DateTime(dt.Year, 1, 1);
      num = (long) (int) ((dateTime3 - dt).Ticks / ticks);
      dateTime1 = dateTime3 + TimeSpan.FromTicks(num * ticks);
      dateTime2 = dateTime1 + period;
      if (dateTime2.Year != dateTime1.Year)
        dateTime2 = new DateTime(dateTime2.Year, 1, 1);
    }
    return Tuple.Create<DateTime, DateTime, long>(dateTime1, dateTime2, num);
  }

  public static double[] GeneratePrices(double min, double max, double step)
  {
    min = min.NormalizePrice(step);
    max = max.NormalizePrice(step);
    double[] prices = new double[1 + (int) Math.Round((max - min) / step)];
    for (int index = 0; index < prices.Length; ++index)
      prices[index] = (min + (double) index * step).NormalizePrice(step);
    return prices;
  }

  public void Append(DateTime time, double price, CandlePriceLevel level)
  {
    bool flag;
    lock (this.SyncRoot)
    {
      if (this.Timeframe.HasValue)
      {
        this._curPeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(time, this.Timeframe.Value, this._curPeriod);
        if (this._segments.Count > 0)
        {
          AbstractList<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> segments = this._segments;
          if (segments[segments.Count - 1].\u0023\u003Dzg86amuQ\u003D() > this._curPeriod.Item1)
            throw new ArgumentOutOfRangeException(nameof (time), "data must be ordered by time");
        }
        flag = this.AddOrUpdateSegment(this._curPeriod.Item1, price, level, true);
      }
      else
        flag = this.AddOrUpdateSegment(time, price, level, true);
    }
    if (!flag)
      return;
    EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> dataSeriesChanged = this.DataSeriesChanged;
    if (dataSeriesChanged == null)
      return;
    dataSeriesChanged((object) this, new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1));
  }

  public void Update(DateTime time, double price, CandlePriceLevel level)
  {
    bool flag;
    lock (this.SyncRoot)
    {
      if (this.Timeframe.HasValue)
      {
        this._curPeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(time, this.Timeframe.Value, this._curPeriod);
        if (this._segments.Count > 0)
        {
          AbstractList<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> segments = this._segments;
          if (segments[segments.Count - 1].\u0023\u003Dzg86amuQ\u003D() > this._curPeriod.Item1)
            throw new ArgumentOutOfRangeException(nameof (time), "data must be ordered by time");
        }
        flag = this.AddOrUpdateSegment(this._curPeriod.Item1, price, level, false);
      }
      else
        flag = this.AddOrUpdateSegment(time, price, level, false);
    }
    if (!flag)
      return;
    EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> dataSeriesChanged = this.DataSeriesChanged;
    if (dataSeriesChanged == null)
      return;
    dataSeriesChanged((object) this, new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1));
  }

  private bool AddOrUpdateSegment(DateTime time, double price, CandlePriceLevel level, bool sum)
  {
    if (((CandlePriceLevel) ref level).TotalVolume < 0M)
      throw new ArgumentOutOfRangeException(nameof (level), (object) ((CandlePriceLevel) ref level).TotalVolume, LocalizedStrings.InvalidValue);
    if (sum && ((CandlePriceLevel) ref level).TotalVolume == 0M)
      return false;
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj ltdBuqAinydlZs6uj;
    if (this._segments.Count != 0)
    {
      AbstractList<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> segments1 = this._segments;
      if (!(segments1[segments1.Count - 1].\u0023\u003Dzg86amuQ\u003D() != time))
      {
        AbstractList<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj> segments2 = this._segments;
        ltdBuqAinydlZs6uj = segments2[segments2.Count - 1];
        goto label_8;
      }
    }
    ltdBuqAinydlZs6uj = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj(time, this._segments.Count);
    this._segments.Add(ltdBuqAinydlZs6uj);
    this._segmentDates.Add(ltdBuqAinydlZs6uj.\u0023\u003Dzg86amuQ\u003D());
label_8:
    if (sum)
    {
      ltdBuqAinydlZs6uj.\u0023\u003DznhN7nDY\u003D(price, level);
      this._volumeProfile.\u0023\u003DznhN7nDY\u003D(price, level);
    }
    else
    {
      ltdBuqAinydlZs6uj.\u0023\u003DzbSuRr0A\u003D(price, level);
      this._volumeProfile.\u0023\u003DzbSuRr0A\u003D(price, level);
    }
    return true;
  }

  public double GetYMinAt(int index, double existingYMin)
  {
    return Math.Min(this._segments[index].\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D(), existingYMin);
  }

  public double GetYMaxAt(int index, double existingYMax)
  {
    return Math.Max(this._segments[index].\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D(), existingYMax);
  }

  public IRange GetWindowedYRange(
    IndexRange  xIndexRange)
  {
    IndexRange  g8Oq2rGx6KyfAreq = (IndexRange ) xIndexRange.Clone();
    if (g8Oq2rGx6KyfAreq.Min < 0)
      g8Oq2rGx6KyfAreq.Min = 0;
    if (g8Oq2rGx6KyfAreq.Max >= this._segments.Count)
      g8Oq2rGx6KyfAreq.Max = this._segments.Count - 1;
    (double, double) tuple = \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj.\u0023\u003Dz\u0024zWmmGTAbDON(this._segments.Skip<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>(g8Oq2rGx6KyfAreq.Min).Take<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj>(g8Oq2rGx6KyfAreq.Max - g8Oq2rGx6KyfAreq.Min + 1));
    return (IRange) new DoubleRange(tuple.Item1, tuple.Item2);
  }

  public IPointSeries ToPointSeries(
    ResamplingMode resamplingMode,
    IndexRange  pointRange,
    int viewportWidth,
    bool isCategoryAxis,
    bool? dataIsDisplayedAs2D,
    IRange visibleXRange,
    IPointResamplerFactory factory,
    object pointSeriesArg = null)
  {
    return !pointRange.IsDefined ? (IPointSeries) null : (IPointSeries) new \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D(this.Segments.\u0023\u003DzRr4AYdnHaTxa(), pointRange, visibleXRange, (double) (this._element.PriceStep ?? 0.000001M));
  }

  public CandlePriceLevel GetVolumeByPrice(double price, double priceStep)
  {
    return this._volumeProfile.\u0023\u003DzHH6Br74\u003D(price, priceStep);
  }

  public bool IsSuspended
  {
    get
    {
      return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y((ISuspendable) this);
    }
  }

  public IUpdateSuspender SuspendUpdates()
  {
    ISciChartSurface parentSurface = this.ParentSurface;
    if (parentSurface == null)
      return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this);
    System.Threading.Monitor.Enter(parentSurface.\u0023\u003Dzjatnj7TNvda7());
    return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this, parentSurface.\u0023\u003Dzjatnj7TNvda7());
  }

  public void ResumeUpdates(
    IUpdateSuspender suspender)
  {
    if (suspender.ResumeTargetOnDispose)
    {
      EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> dataSeriesChanged = this.DataSeriesChanged;
      if (dataSeriesChanged != null)
        dataSeriesChanged((object) this, new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 3));
    }
    if (suspender.Tag == null)
      return;
    System.Threading.Monitor.Exit(suspender.Tag);
  }

  public void DecrementSuspend()
  {
  }

  void IDataSeries<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EAppend(
    DateTime dt,
    params double[] yValues)
  {
    throw new NotImplementedException();
  }

  public void Append(IEnumerable<DateTime> dtList, params IEnumerable<double>[] yValues)
  {
    throw new NotImplementedException();
  }

  [Obsolete("IsAttached is obsolete because there is no DataSeriesSet now")]
  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EIsAttached
  {
    get => throw new NotImplementedException();
  }

  int? \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EFifoCapacity
  {
    get => throw new NotImplementedException();
    set => throw new NotImplementedException();
  }

  void IDataSeries<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002ERemove(
    DateTime x)
  {
    throw new NotImplementedException();
  }

  void IDataSeries<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002ERemoveAt(
    int index)
  {
    throw new NotImplementedException();
  }

  void IDataSeries<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002ERemoveRange(
    int startIndex,
    int count)
  {
    throw new NotImplementedException();
  }

  IDataSeries<DateTime, double> IDataSeries<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EClone()
  {
    throw new NotImplementedException();
  }

  int \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EFindIndex(
    IComparable x,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D searchMode)
  {
    throw new NotImplementedException();
  }

  int \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EFindClosestPoint(
    IComparable x,
    IComparable y,
    double xyScaleRatio,
    double maxXDistance)
  {
    throw new NotImplementedException();
  }

  int \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EFindClosestLine(
    IComparable x,
    IComparable y,
    double xyScaleRatio,
    double maxXDistance,
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D drawNanAs)
  {
    throw new NotImplementedException();
  }

  public virtual void OnBeginRenderPass()
  {
  }

  IPointSeries \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EToPointSeries(
    IList column,
    ResamplingMode resamplingMode,
    IndexRange  pointRange,
    int viewportWidth,
    bool isCategoryAxis)
  {
    throw new NotImplementedException();
  }

  HitTestInfo \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EToHitTestInfo(
    int index)
  {
    return HitTestInfo.\u0023\u003Dzz_6Dy9M\u003D;
  }

  private sealed class \u0023\u003DzV\u0024R8yw8\u003D(TimeframeSegmentDataSeries _param1) : 
    IList,
    IEnumerable,
    ICollection,
    IList<double>,
    ICollection<double>,
    IEnumerable<double>
  {
    
    private readonly TimeframeSegmentDataSeries _parentElement = _param1 ?? throw new ArgumentNullException("parent");

    public int Count => this._parentElement._segments.Count;

    public object SyncRoot
    {
      get => ((ICollection) this._parentElement._segments).SyncRoot;
    }

    public bool IsSynchronized
    {
      get => ((ICollection) this._parentElement._segments).IsSynchronized;
    }

    public bool IsReadOnly => true;

    public bool IsFixedSize => false;

    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<double> GetEnumerator()
    {
      return this._parentElement._segments.Select<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj, double>(TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383.SomeIntenalMethod003D ?? (TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383.SomeIntenalMethod003D = new Func<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj, double>(TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003Dz_cR1AGMqLDwfRLrn\u0024w\u003D\u003D))).GetEnumerator();
    }

    bool IList.\u0023\u003Dz07_U1xKJVCxa7bIf\u0024A\u003D\u003D(object _param1)
    {
      return _param1 is double num && this.Contains(num);
    }

    public bool Contains(double _param1)
    {
      return this._parentElement._segments.Select<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj, double>(TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383.\u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D ?? (TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383.\u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D = new Func<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj, double>(TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003Dzt14kkhn5JWbwz4bEVw\u003D\u003D))).Contains<double>(_param1);
    }

    int IList.\u0023\u003DzRqsurumTDWAgVqHVtg\u003D\u003D(object _param1)
    {
      return !(_param1 is double num) ? -1 : this.IndexOf(num);
    }

    public int IndexOf(double _param1)
    {
      int count = this._parentElement._segments.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this._parentElement._segments[index].\u0023\u003Dzu7q98_E\u003D().DoubleEquals(_param1))
          return index;
      }
      return -1;
    }

    public double this[int _param1]
    {
      get => this._parentElement._segments[_param1].\u0023\u003Dzu7q98_E\u003D();
      set => throw new NotImplementedException();
    }

    object IList.\u0023\u003Dzsw6uZQAY38X4SXUVUM6sxbU\u003D(int _param1) => (object) this[_param1];

    void IList.\u0023\u003DzPS8zWbReapv0MVOGfNSMdFU\u003D(int _param1, object _param2)
    {
      throw new NotImplementedException();
    }

    public void Clone(double[] _param1, int _param2) => throw new NotImplementedException();

    public void Clone(Array _param1, int _param2) => throw new NotImplementedException();

    public void Add(double _param1) => throw new NotImplementedException();

    public void Clear() => throw new NotImplementedException();

    public void Insert(int _param1, double _param2) => throw new NotImplementedException();

    public bool Remove(double _param1) => throw new NotImplementedException();

    public void RemoveAt(int _param1) => throw new NotImplementedException();

    int IList.\u0023\u003Dz9l8NFg1zgw0y\u0024EjyXw\u003D\u003D(object _param1)
    {
      throw new NotImplementedException();
    }

    void IList.\u0023\u003DzuxPIbwR6tOrj8Wpe9w\u003D\u003D(int _param1, object _param2)
    {
      throw new NotImplementedException();
    }

    void IList.\u0023\u003Dzx\u0024PvHCNWSNwq55LIBQ\u003D\u003D(object _param1)
    {
      throw new NotImplementedException();
    }

    [Serializable]
    private sealed class SomeClass34343383
    {
      public static readonly TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383 SomeMethond0343 = new TimeframeSegmentDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.SomeClass34343383();
      public static Func<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj, double> SomeIntenalMethod003D;
      public static Func<\u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj, double> \u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D;

      public double \u0023\u003Dz_cR1AGMqLDwfRLrn\u0024w\u003D\u003D(
        \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj _param1)
      {
        return _param1.\u0023\u003Dzu7q98_E\u003D();
      }

      public double \u0023\u003Dzt14kkhn5JWbwz4bEVw\u003D\u003D(
        \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj _param1)
      {
        return _param1.\u0023\u003Dzu7q98_E\u003D();
      }
    }
  }
}
