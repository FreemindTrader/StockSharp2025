// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.Transactions.TransactionDataSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace StockSharp.Xaml.Charting.Model.DataSeries.Transactions;

internal class TransactionDataSeries : 
  BindableObject ,
  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  ISuspendable
{
  private string _seriesName;
  private readonly \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<DateTime, double> _candlesSeries;
  private readonly \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D> _data = new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>();
  private readonly TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D _yValues;
  private List<ChartDrawData.sTrade> _transactionBuffer = new List<ChartDrawData.sTrade>();
  private bool _flushingBufferedTransactions;

  public TransactionDataSeries(
    \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<DateTime, double> candles)
  {
    this._candlesSeries = candles ?? throw new ArgumentNullException("");
    this._yValues = new TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D(this);
    this._candlesSeries.DataSeriesChanged += new EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X>(this.CandlesSeries_OnDataSeriesChanged);
  }

  private IList<DateTime> _dates => this._candlesSeries.XValues;

  public \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D DataSeriesType
  {
    get => (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 10;
  }

  public event EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> DataSeriesChanged;

  public object SyncRoot { get; } = new object();

  public ISciChartSurface ParentSurface { get; set; }

  public bool AcceptsUnsortedData
  {
    get => true;
    set
    {
    }
  }

  public bool IsSecondary => true;

  public string SeriesName
  {
    get => this._seriesName;
    set
    {
      this._seriesName = value;
      this.OnDataSeriesChanged((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
    }
  }

  public bool HasValues => this._dates.Count > 0;

  public int Count => this._dates.Count;

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

  public bool IsFifo => false;

  public bool IsSorted => true;

  internal \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D> Data
  {
    get
    {
      return (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>) this._data;
    }
  }

  public Type XType => typeof (DateTime);

  public Type YType => typeof (double);

  public IComparable LatestYValue => (IComparable) null;

  private IList<DateTime> XValues => this._dates;

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXValues
  {
    get => (IList) this.XValues;
  }

  IList<DateTime> \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EXValues
  {
    get => this.XValues;
  }

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EYValues
  {
    get => (IList) this._yValues;
  }

  IList<double> \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EYValues
  {
    get => (IList<double>) this._yValues;
  }

  public void Clear()
  {
    lock (this.SyncRoot)
    {
      this._data.Clear();
      this._transactionBuffer.Clear();
      this.EnsureData();
      this.OnDataSeriesChanged((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 3);
    }
  }

  private void CandlesSeries_OnDataSeriesChanged(
    object sender,
    \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X e)
  {
    if ((e.\u0023\u003Dz8a6NTXl5eUny() & (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 2) == (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 2)
      this.Clear();
    this.EnsureData();
    this.AddOrUpdateBufferedTransactions();
  }

  private void OnDataSeriesChanged(
    \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D flags)
  {
    EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> dataSeriesChanged = this.DataSeriesChanged;
    if (dataSeriesChanged == null)
      return;
    dataSeriesChanged((object) this, new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X(flags));
  }

  private void EnsureData()
  {
    if (!this._candlesSeries.IsSorted)
      throw new NotSupportedException("");
    lock (this.SyncRoot)
    {
      if (this._data.Count == this._candlesSeries.Count)
        return;
      if (this._data.Count < this._candlesSeries.Count)
      {
        this._data.\u0023\u003Dz6_E5\u0024pE\u003D(Enumerable.Range(0, this._candlesSeries.Count - this._data.Count).Select<int, \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>(TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzLuwuNd6jgC14H9fckQ\u003D\u003D ?? (TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzLuwuNd6jgC14H9fckQ\u003D\u003D = new Func<int, \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>(TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzNUWylnO6KRBGmmAdIYVLLt0\u003D))));
        return;
      }
    }
    throw new InvalidOperationException("");
  }

  public void InvalidateParentSurface(
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_f7seBjAlONQGY47Zh8\u003D rangeMode)
  {
    this._candlesSeries.InvalidateParentSurface(rangeMode);
  }

  public IndexRange  GetIndicesRange(
    IRange visibleRange)
  {
    return this._candlesSeries.GetIndicesRange(visibleRange);
  }

  IRange \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    IRange xRange)
  {
    IndexRange  indicesRange = this.GetIndicesRange(xRange);
    return !indicesRange.IsDefined ? (IRange) new DoubleRange(double.MinValue, double.MaxValue) : this.GetWindowedYRange(indicesRange);
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
      return !this._dates.Any<DateTime>() ? (IRange) new DoubleRange(double.MinValue, double.MaxValue) : (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(this._dates[0], this._dates[this._dates.Count - 1]).AsDoubleRange();
    }
  }

  public IRange YRange
  {
    get
    {
      if (this._dates.Count == 0)
        return (IRange) new DoubleRange(double.MinValue, double.MaxValue);
      double num1 = double.MaxValue;
      double num2 = double.MinValue;
      foreach (\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D rvIvnoSs0Uo7ic5Jw in this._data.Where<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>(TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzGnRvVl1tJI2ALnJuvg\u003D\u003D ?? (TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzGnRvVl1tJI2ALnJuvg\u003D\u003D = new Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, bool>(TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzQew5\u0024BD3XizwlemJVIlpNho\u003D))))
      {
        num1 = MathHelper.Min(num1, rvIvnoSs0Uo7ic5Jw.\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D());
        num2 = MathHelper.Max(num2, rvIvnoSs0Uo7ic5Jw.\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D());
      }
      return (IRange) new DoubleRange(num1, num2);
    }
  }

  public void AddOrUpdateTransaction(ChartDrawData.sTrade trans)
  {
    if ((trans != null ? (trans.GetTransactionString().\u0023\u003DzCCMM80zDpO6N<char>() ? 1 : 0) : 1) != 0)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 1);
      interpolatedStringHandler.AppendLiteral("");
      interpolatedStringHandler.AppendFormatted<ChartDrawData.sTrade>(trans);
      throw new ArgumentException(interpolatedStringHandler.ToStringAndClear());
    }
    lock (this.SyncRoot)
    {
      lock (this._candlesSeries.SyncRoot)
      {
        this._transactionBuffer.Add(trans);
        this.AddOrUpdateBufferedTransactions();
      }
    }
  }

  private void AddOrUpdateBufferedTransactions()
  {
    bool flag = false;
    lock (this.SyncRoot)
    {
      lock (this._candlesSeries.SyncRoot)
      {
        if (this._transactionBuffer.Count == 0 || this._flushingBufferedTransactions)
          return;
        TimeSpan? timeframe = this._candlesSeries.Timeframe;
        if (!timeframe.HasValue)
          return;
        TimeSpan valueOrDefault = timeframe.GetValueOrDefault();
        try
        {
          this._flushingBufferedTransactions = true;
          this._candlesSeries.\u0023\u003DzGXoUV7Q\u0024_bM\u0024();
          this.EnsureData();
          List<ChartDrawData.sTrade> zU3TaXfsList = new List<ChartDrawData.sTrade>();
          foreach (ChartDrawData.sTrade zU3TaXfs in this._transactionBuffer)
          {
            int index = this.XValues.\u0023\u003Dzk3Bi7DnEdNPh<DateTime>(zU3TaXfs.UtcTime(), (IComparer<DateTime>) null);
            if (index < 0)
              index = ~index - 1;
            if (index >= 0)
            {
              DateTime date = this._dates[index];
              if (zU3TaXfs.UtcTime() >= date + valueOrDefault)
              {
                zU3TaXfsList.Add(zU3TaXfs);
              }
              else
              {
                \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D> data = this._data;
                int num = index;
                if (data[num] == null)
                  data[num] = new \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D(date);
                this._data[index].\u0023\u003DzDjRgWj_juivV(zU3TaXfs);
                flag = true;
              }
            }
          }
          this._transactionBuffer = zU3TaXfsList;
        }
        finally
        {
          this._flushingBufferedTransactions = false;
        }
      }
    }
    if (!flag)
      return;
    this.OnDataSeriesChanged((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public double GetYMinAt(int index, double existingYMin)
  {
    return Math.Min(this._data[index].\u0023\u003Dz_0RMJpfkCRvPs4ToyQ\u003D\u003D(), existingYMin);
  }

  public double GetYMaxAt(int index, double existingYMax)
  {
    return Math.Max(this._data[index].\u0023\u003DzFQXj8Eq5AMNn7N8nJA\u003D\u003D(), existingYMax);
  }

  public IRange GetWindowedYRange(
    IndexRange  xIndexRange)
  {
    this.EnsureData();
    IndexRange  g8Oq2rGx6KyfAreq = (IndexRange ) xIndexRange.Clone();
    if (g8Oq2rGx6KyfAreq.Min < 0)
      g8Oq2rGx6KyfAreq.Min = 0;
    if (g8Oq2rGx6KyfAreq.Max >= this._data.Count)
      g8Oq2rGx6KyfAreq.Max = this._data.Count - 1;
    double num1;
    double num2;
    \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D.\u0023\u003Dz\u0024zWmmGTAbDON(this._data.Skip<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>(g8Oq2rGx6KyfAreq.Min).Take<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D>(g8Oq2rGx6KyfAreq.Max - g8Oq2rGx6KyfAreq.Min + 1), out num1, out num2);
    return (IRange) new DoubleRange(num1, num2);
  }

  public \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ToPointSeries(
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D resamplingMode,
    IndexRange  pointRange,
    int viewportWidth,
    bool isCategoryAxis,
    bool? dataIsDisplayedAs2D,
    IRange visibleXRange,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D factory,
    object pointSeriesArg = null)
  {
    return !pointRange.IsDefined ? (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) null : (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) new \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUs\u0024JSMgoXEdmnE5TCobCGwh7srNhlw\u003D\u003D(this._data.\u0023\u003DzRr4AYdnHaTxa(), pointRange, visibleXRange);
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
      this.OnDataSeriesChanged((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 3);
    if (suspender.Tag == null)
      return;
    System.Threading.Monitor.Exit(suspender.Tag);
  }

  public void DecrementSuspend()
  {
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EAppend(
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

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002ERemove(
    DateTime x)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002ERemoveAt(
    int index)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002ERemoveRange(
    int startIndex,
    int count)
  {
    throw new NotImplementedException();
  }

  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double> \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<DateTime, double>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CSystem\u002EDateTime\u002CSystem\u002EDouble\u003E\u002EClone()
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

  \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EToPointSeries(
    IList column,
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D resamplingMode,
    IndexRange  pointRange,
    int viewportWidth,
    bool isCategoryAxis)
  {
    throw new NotImplementedException();
  }

  \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EToHitTestInfo(
    int index)
  {
    return \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new TransactionDataSeries.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<int, \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D> \u0023\u003DzLuwuNd6jgC14H9fckQ\u003D\u003D;
    public static Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, bool> \u0023\u003DzGnRvVl1tJI2ALnJuvg\u003D\u003D;

    internal \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D \u0023\u003DzNUWylnO6KRBGmmAdIYVLLt0\u003D(
      int _param1)
    {
      return (\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D) null;
    }

    internal bool \u0023\u003DzQew5\u0024BD3XizwlemJVIlpNho\u003D(
      \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D _param1)
    {
      return _param1 != null;
    }
  }

  private sealed class \u0023\u003DzV\u0024R8yw8\u003D : 
    IList<double>,
    ICollection<double>,
    IEnumerable<double>,
    IEnumerable,
    IList,
    ICollection
  {
    
    private readonly TransactionDataSeries _drawData;

    public \u0023\u003DzV\u0024R8yw8\u003D(TransactionDataSeries _param1)
    {
      this._drawData = _param1;
    }

    public int Count => this._drawData._data.Count;

    public object SyncRoot => ((ICollection) this._drawData._data).SyncRoot;

    public bool IsSynchronized
    {
      get => ((ICollection) this._drawData._data).IsSynchronized;
    }

    public bool IsReadOnly => true;

    public bool IsFixedSize => false;

    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<double> GetEnumerator()
    {
      return this._drawData.Data.Select<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, double>(TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D ?? (TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D = new Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, double>(TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz_cR1AGMqLDwfRLrn\u0024w\u003D\u003D))).GetEnumerator();
    }

    bool IList.\u0023\u003Dz07_U1xKJVCxa7bIf\u0024A\u003D\u003D(object _param1)
    {
      return _param1 is double num && this.Contains(num);
    }

    public bool Contains(double _param1)
    {
      return this._drawData._data.Select<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, double>(TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D ?? (TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D = new Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, double>(TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzt14kkhn5JWbwz4bEVw\u003D\u003D))).Contains<double>(_param1);
    }

    int IList.\u0023\u003DzRqsurumTDWAgVqHVtg\u003D\u003D(object _param1)
    {
      return _param1 is double num ? this.IndexOf(num) : -1;
    }

    public int IndexOf(double _param1)
    {
      int count = this._drawData._data.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this._drawData._data[index].\u0023\u003Dzu7q98_E\u003D().\u0023\u003Dz0AeaaBjWIeEn(_param1))
          return index;
      }
      return -1;
    }

    public double this[int _param1]
    {
      get => this._drawData._data[_param1].\u0023\u003Dzu7q98_E\u003D();
      set => throw new NotImplementedException();
    }

    object IList.\u0023\u003Dzsw6uZQAY38X4SXUVUM6sxbU\u003D(int _param1) => (object) this[_param1];

    void IList.\u0023\u003DzPS8zWbReapv0MVOGfNSMdFU\u003D(int _param1, object _param2)
    {
      throw new NotImplementedException();
    }

    void ICollection<double>.\u0023\u003Dzgul\u0024fBnxR9FHcu_L4TDaY3A3EDT8AcVVZA\u003D\u003D(
      double[] _param1,
      int _param2)
    {
      throw new NotImplementedException();
    }

    void ICollection.\u0023\u003DzFAvqYrLd8QaQMLuvPOiGwFQ\u003D(Array _param1, int _param2)
    {
      throw new NotImplementedException();
    }

    void ICollection<double>.\u0023\u003Dz\u0024h7vKCjpxYzLlkC0PyEmISwnu5tGxM7L\u0024A\u003D\u003D(
      double _param1)
    {
      throw new NotImplementedException();
    }

    void IList<double>.\u0023\u003DzbqEPmotrTTZpKMId6Cjc7yxOnqsQhrUULQ\u003D\u003D(
      int _param1,
      double _param2)
    {
      throw new NotImplementedException();
    }

    bool ICollection<double>.\u0023\u003DzeaHz\u0024WS2O6O8OkpAxXeXqhk6QIwZtZCXMw\u003D\u003D(
      double _param1)
    {
      throw new NotImplementedException();
    }

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

    public void Clear() => throw new NotImplementedException();

    public void RemoveAt(int _param1) => throw new NotImplementedException();

    [Serializable]
    private sealed class \u0023\u003Dz7qOdpi4\u003D
    {
      public static readonly TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new TransactionDataSeries.\u0023\u003DzV\u0024R8yw8\u003D.\u0023\u003Dz7qOdpi4\u003D();
      public static Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, double> \u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D;
      public static Func<\u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D, double> \u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D;

      internal double \u0023\u003Dz_cR1AGMqLDwfRLrn\u0024w\u003D\u003D(
        \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D _param1)
      {
        return _param1.\u0023\u003Dzu7q98_E\u003D();
      }

      internal double \u0023\u003Dzt14kkhn5JWbwz4bEVw\u003D\u003D(
        \u0023\u003Dzi5rhlbggKV0KkQxOnrSREyuuUy2OemjHnRvIVnoSs0UO7ic5Jw\u003D\u003D _param1)
      {
        return _param1.\u0023\u003Dzu7q98_E\u003D();
      }
    }
  }
}
