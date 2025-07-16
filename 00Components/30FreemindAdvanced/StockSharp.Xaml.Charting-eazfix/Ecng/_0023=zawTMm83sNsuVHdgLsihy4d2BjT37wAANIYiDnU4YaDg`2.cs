// Decompiled with JetBrains decompiler
// Type: #=zawTMm83sNsuVHdgLsihy4d2BjT37wAANIYiDnU4YaDg5
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;

#nullable disable
public abstract class DataSeries<TX, TY> : 
  BindableObject ,
  IDataSeries<TX, TY>,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D
  where TX : IComparable
  where TY : IComparable
{
  
  protected ISeriesColumn<TX> \u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TX>();
  
  protected ISeriesColumn<TY> \u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  public static readonly IMath<TY> \u0023\u003DzkNQuj6U\u003D = MathHelper.GetMath<TY>();
  
  public static readonly IMath<TX> \u0023\u003DzNvw4Cjs\u003D = MathHelper.GetMath<TX>();
  
  private int? \u0023\u003DzMtfcPdzt_KKZ;
  
  private string \u0023\u003DzQEj6PheC_Wki9bX7qw\u003D\u003D;
  
  private readonly object myLock = new object();
  
  private \u0023\u003DzbZGwufOdFTewaG24h4AgEt0e4y2LR89MbvhnTaKf4YV_es8hzZJVk08\u003D<TX> \u0023\u003DzKF7SyeFzhmMUJe1KXX2lKTrpRhKv\u0024hO8LQ\u003D\u003D;
  
  private WeakReference _drawingSurface;
  
  private bool \u0023\u003DzAbiy0SLJJpkme\u0024JoSLWkGeTc6oGI;
  
  private bool \u0023\u003DzSZVYV\u0024SzdMAeghhiRw\u003D\u003D;

  protected DataSeries() => this.Clear();

  public event EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> DataSeriesChanged;

  public object SyncRoot => this.myLock;

  public \u0023\u003DzbZGwufOdFTewaG24h4AgEt0e4y2LR89MbvhnTaKf4YV_es8hzZJVk08\u003D<TX> DataDistributionCalculator
  {
    get => this.\u0023\u003DzKF7SyeFzhmMUJe1KXX2lKTrpRhKv\u0024hO8LQ\u003D\u003D;
    set => this.\u0023\u003DzKF7SyeFzhmMUJe1KXX2lKTrpRhKv\u0024hO8LQ\u003D\u003D = value;
  }

  public ISciChartSurface ParentSurface
  {
    get
    {
      return this._drawingSurface == null ? (ISciChartSurface) null : (ISciChartSurface) this._drawingSurface.Target;
    }
    set
    {
      this._drawingSurface = (WeakReference) null;
      if (value == null)
        return;
      this._drawingSurface = new WeakReference((object) value);
    }
  }

  public Type XType => typeof (TX);

  public Type YType => typeof (TY);

  public virtual IRange XRange
  {
    get
    {
      lock (this.SyncRoot)
      {
        TX zulcL8Ra1 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.MaxValue;
        TX zulcL8Ra2 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.MinValue;
        if (this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D())
        {
          IList<TX> zmIwKipw = (IList<TX>) this.\u0023\u003DzmIwKipw\u003D;
          if (zmIwKipw.Count > 0)
          {
            zulcL8Ra1 = zmIwKipw[0];
            zulcL8Ra2 = zmIwKipw[zmIwKipw.Count - 1];
          }
        }
        else
          \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz\u0024zWmmGTAbDON<TX>((IEnumerable<TX>) this.\u0023\u003DzmIwKipw\u003D, out zulcL8Ra1, out zulcL8Ra2);
        return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) zulcL8Ra1, (IComparable) zulcL8Ra2);
      }
    }
  }

  public virtual IRange YRange
  {
    get
    {
      lock (this.SyncRoot)
      {
        TY zE8zkRfY1;
        TY zE8zkRfY2;
        \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz\u0024zWmmGTAbDON<TY>((IEnumerable<TY>) this.\u0023\u003DzoEP49rI\u003D, out zE8zkRfY1, out zE8zkRfY2);
        return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) zE8zkRfY1, (IComparable) zE8zkRfY2);
      }
    }
  }

  public IComparable YMin => this.YRange.Min;

  public IComparable LatestYValue
  {
    get
    {
      return this.YValues.Count != 0 ? (IComparable) this.YValues[this.YValues.Count - 1] : (IComparable) null;
    }
  }

  public IComparable YMax => this.YRange.Max;

  public IComparable XMin => this.XRange.Min;

  public IComparable XMax => this.XRange.Max;

  public int Count
  {
    get
    {
      return ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzoEP49rI\u003D).get_Count();
    }
  }

  public bool AcceptsUnsortedData
  {
    get => this.\u0023\u003DzAbiy0SLJJpkme\u0024JoSLWkGeTc6oGI;
    set => this.\u0023\u003DzAbiy0SLJJpkme\u0024JoSLWkGeTc6oGI = value;
  }

  public string SeriesName
  {
    get => this.\u0023\u003DzQEj6PheC_Wki9bX7qw\u003D\u003D;
    set
    {
      this.\u0023\u003DzQEj6PheC_Wki9bX7qw\u003D\u003D = value;
      this.OnPropertyChanged(nameof (SeriesName));
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
    }
  }

  [Obsolete("IsAttached is obsolete because there is no DataSeries now")]
  public bool IsAttached => false;

  public abstract DataSeriesType DataSeriesType { get; }

  public TY this[
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAx0mWOjt4u1N2TxXzxA\u003D _param1,
    int _param2]
  {
    get
    {
      switch (_param1)
      {
        case \u0023\u003DzEJoJjwSelM_K3zbmiw1OAx0mWOjt4u1N2TxXzxA\u003D.Close:
        case \u0023\u003DzEJoJjwSelM_K3zbmiw1OAx0mWOjt4u1N2TxXzxA\u003D.Y:
          return this.\u0023\u003DzoEP49rI\u003D[_param2];
        default:
          throw new InvalidOperationException($"The enumeration value {_param1} has not been catered");
      }
    }
  }

  public abstract bool HasValues { get; }

  public virtual bool IsSecondary
  {
    get => this.\u0023\u003DzSZVYV\u0024SzdMAeghhiRw\u003D\u003D;
    set => this.\u0023\u003DzSZVYV\u0024SzdMAeghhiRw\u003D\u003D = value;
  }

  public IList<TX> XValues
  {
    get => (IList<TX>) this.\u0023\u003DzmIwKipw\u003D;
  }

  public IList<TY> YValues
  {
    get => (IList<TY>) this.\u0023\u003DzoEP49rI\u003D;
  }

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.\u0023\u003DzYvV7blprrv\u0024kuBcS9cPJhJJohvL59CNzH6MmQ7z89X5qTi4r9w\u003D\u003D()
  {
    return (IList) this.\u0023\u003DzmIwKipw\u003D;
  }

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDJOVfkyz3mjQ4rJi2BB2r9T\u0024Ws8RkQ\u003D\u003D()
  {
    return (IList) this.\u0023\u003DzoEP49rI\u003D;
  }

  public bool IsFifo => this.\u0023\u003DzMtfcPdzt_KKZ.HasValue;

  public bool IsSorted
  {
    get => this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D();
  }

  public int? FifoCapacity
  {
    get => this.\u0023\u003DzMtfcPdzt_KKZ;
    set
    {
      this.\u0023\u003DzMtfcPdzt_KKZ = value;
      this.Clear();
    }
  }

  public abstract void Append(
    TX _param1,
    params TY[] _param2);

  public abstract void Append(
    IEnumerable<TX> _param1,
    params IEnumerable<TY>[] _param2);

  public void GuiUpdateAndClear(TX _param1)
  {
    int num = ((IList) this.XValues).\u0023\u003DzFH1yjjY\u003D(this.IsSorted, (IComparable) _param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
    if (num == -1)
      return;
    this.\u0023\u003DzfEbP\u00247w\u003D(num);
  }

  public abstract void \u0023\u003DzfEbP\u00247w\u003D(int _param1);

  public abstract void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2);

  public void Clear()
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dzz6J3NZIzdU\u0024X();
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 3);
      this.DataDistributionCalculator = \u0023\u003DzzsyKnUNUDKjF7rDv70izNy4bx\u0024saGFs108aHH7lGphGuZX7JpGJEopE\u003D.\u0023\u003DzpxJeWbQ\u003D<TX>(this.IsFifo);
      this.DataDistributionCalculator.Clear();
    }
  }

  public abstract IDataSeries<TX, TY> \u0023\u003DzQ8SgRgQ\u003D();

  public IndexRange  GetIndicesRange(
    IRange _param1)
  {
    return this.\u0023\u003DzIhLaI03B608r(_param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3);
  }

  private IndexRange  \u0023\u003DzIhLaI03B608r(
    IRange _param1,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3)
  {
    IndexRange  g8Oq2rGx6KyfAreq1 = new IndexRange (0, -1);
    if (((ICollection) this.\u0023\u003DzmIwKipw\u003D).Count > 0)
    {
      if (!(_param1.Clone() is IndexRange  g8Oq2rGx6KyfAreq2))
        g8Oq2rGx6KyfAreq2 = this.\u0023\u003DzyDqRcdYVp5tx(_param1, _param2, _param3);
      g8Oq2rGx6KyfAreq1 = this.\u0023\u003Dzz0Vabf8AWG1b(g8Oq2rGx6KyfAreq2);
    }
    return g8Oq2rGx6KyfAreq1;
  }

  private IndexRange  \u0023\u003DzyDqRcdYVp5tx(
    IRange _param1,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3)
  {
    int count = ((ICollection) this.\u0023\u003DzmIwKipw\u003D).Count;
    IndexRange  g8Oq2rGx6KyfAreq = new IndexRange (-1, -1);
    if (!this.IsSorted)
    {
      g8Oq2rGx6KyfAreq.Min = 0;
      g8Oq2rGx6KyfAreq.Max = count - 1;
    }
    else
    {
      Type conversionType = typeof (TX);
      IComparable comparable1 = _param1.Min;
      IComparable comparable2 = _param1.Max;
      if (NumberUtil.IsIntegerType(conversionType))
      {
        comparable1 = (IComparable) Math.Floor(comparable1.ToDouble());
        comparable2 = (IComparable) Math.Ceiling(comparable2.ToDouble());
      }
      TX zulcL8Ra1 = (TX) Convert.ChangeType((object) comparable1, conversionType, (IFormatProvider) CultureInfo.InvariantCulture);
      TX zulcL8Ra2 = (TX) Convert.ChangeType((object) comparable2, conversionType, (IFormatProvider) CultureInfo.InvariantCulture);
      TX zulcL8Ra3 = this.\u0023\u003DzmIwKipw\u003D[0];
      if (this.\u0023\u003DzmIwKipw\u003D[count - 1].CompareTo((object) zulcL8Ra1) >= 0 && zulcL8Ra3.CompareTo((object) zulcL8Ra2) <= 0)
      {
        g8Oq2rGx6KyfAreq.Min = this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzFH1yjjY\u003D(true, (IComparable) zulcL8Ra1, _param2);
        g8Oq2rGx6KyfAreq.Max = this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzFH1yjjY\u003D(true, (IComparable) zulcL8Ra2, _param3);
      }
    }
    return g8Oq2rGx6KyfAreq;
  }

  private IndexRange  \u0023\u003Dzz0Vabf8AWG1b(
    IndexRange  _param1)
  {
    int count = ((ICollection) this.\u0023\u003DzmIwKipw\u003D).Count;
    if (_param1.IsDefined)
    {
      _param1.Min = Math.Max(_param1.Min, 0);
      _param1.Max = Math.Min(_param1.Max, count - 1);
    }
    if (_param1.Min.CompareTo(_param1.Max) > 0)
      _param1.Min = 0;
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("GetIndicesRange: Min={0}, Max={1}", new object[2]
    {
      (object) _param1.Min,
      (object) _param1.Max
    });
    return _param1;
  }

  public abstract IPointSeries ToPointSeries(
    ResamplingMode _param1,
    IndexRange  _param2,
    int _param3,
    bool _param4,
    bool? _param5,
    IRange _param6,
    IPointResamplerFactory _param7,
    object _param8 = null);

  [Obsolete("ToPointSeries overload has been deprecated, use ToPointSeries instead, and cast to correct type of point series", true)]
  public IPointSeries \u0023\u003DzqBr63KVJqTvw(
    IList _param1,
    ResamplingMode _param2,
    IndexRange  _param3,
    int _param4,
    bool _param5)
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IRange _param1)
  {
    return this.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(_param1, false);
  }

  public IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IRange _param1,
    bool _param2)
  {
    IndexRange  g8Oq2rGx6KyfAreq = _param1 != null ? this.\u0023\u003DzIhLaI03B608r(_param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1) : throw new ArgumentNullException("xRange");
    return !g8Oq2rGx6KyfAreq.IsDefined ? (IRange) this.\u0023\u003DzLc65\u0024pc\u003D((IComparable) DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.MinValue, (IComparable) DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.MaxValue) : this.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(g8Oq2rGx6KyfAreq, _param2);
  }

  public IRange GetWindowedYRange(
    IndexRange  _param1)
  {
    return this.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(_param1, false);
  }

  public IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IndexRange  _param1,
    bool _param2)
  {
    lock (this.SyncRoot)
    {
      TY zE8zkRfY1 = DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.MinValue;
      TY zE8zkRfY2 = DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.MaxValue;
      int num1 = Math.Max(_param1.Min, 0);
      int num2 = Math.Min(_param1.Max, this.Count - 1);
      for (int index = num1; index <= num2; ++index)
      {
        TY yminAt = this.GetYMinAt(index, zE8zkRfY2);
        zE8zkRfY2 = _param2 ? this.\u0023\u003DzBoZWoWvDT6SA<TY>(zE8zkRfY2, yminAt) : yminAt;
        zE8zkRfY1 = this.GetYMaxAt(index, zE8zkRfY1);
      }
      return (IRange) this.\u0023\u003DzLc65\u0024pc\u003D((IComparable) zE8zkRfY2, (IComparable) zE8zkRfY1);
    }
  }

  public int \u0023\u003DzFH1yjjY\u003D(
    IComparable _param1,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param2 = (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "x");
    if (_param1.GetType() != typeof (TX))
    {
      double num = _param1.GetType() == typeof (double) ? (double) _param1 : throw new InvalidOperationException($"The X-value type {_param1.GetType()} does not match the DataSeries X-Type");
      TX zulcL8Ra1 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ZeroValue;
      TX zulcL8Ra2 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Inc(ref zulcL8Ra1);
      _param1 = (IComparable) DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Mult(zulcL8Ra2, num);
    }
    return this.XValues.\u0023\u003DzFH1yjjY\u003D<TX>(this.IsSorted, _param1, _param2);
  }

  public virtual HitTestInfo \u0023\u003DzDKPxuEruV71w(
    int _param1)
  {
    lock (this.SyncRoot)
    {
      if (_param1 < 0 || _param1 >= this.Count)
        return HitTestInfo.\u0023\u003Dzz_6Dy9M\u003D;
      TX xvalue = this.XValues[_param1];
      TY yvalue = this.YValues[_param1];
      HitTestInfo zldchDrVsrVyHh6WyiGy = new HitTestInfo();
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzOCYm7g4gfYSc(this.SeriesName);
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzQ9xCEGz0Gl\u0024q(this.DataSeriesType);
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dz2Iv\u0024sxQuGDBR((IComparable) xvalue);
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzBswzhzuQHrrX((IComparable) yvalue);
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzV4wgjRUOXtRf(_param1);
      return zldchDrVsrVyHh6WyiGy;
    }
  }

  public void InvalidateParentSurface(
    RangeMode _param1)
  {
    if (this.ParentSurface == null)
      return;
    switch (_param1)
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

  protected T \u0023\u003DzBoZWoWvDT6SA<T>(T _param1, T _param2) where T : IComparable
  {
    if (typeof (T) == typeof (DateTime) || typeof (T) == typeof (TimeSpan))
      return _param1;
    IComparable comparable1 = (IComparable) DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ZeroValue;
    IComparable comparable2 = (IComparable) DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.ZeroValue;
    IComparable comparable3 = typeof (T) == typeof (TX) ? comparable1 : comparable2;
    return _param2.CompareTo((object) comparable3) <= 0 ? _param1 : _param2;
  }

  protected abstract void \u0023\u003Dzz6J3NZIzdU\u0024X();

  public abstract TY GetYMinAt(
    int _param1,
    TY _param2);

  public abstract TY GetYMaxAt(
    int _param1,
    TY _param2);

  public TX \u0023\u003DzOCLFX69GGSn8(
    int _param1,
    TX _param2)
  {
    TX xvalue = this.XValues[_param1];
    return !xvalue.IsDefined() ? _param2 : DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Min(_param2, xvalue);
  }

  public TY \u0023\u003DzStrzQ_NInQ8M(
    TY _param1,
    TY _param2,
    ISeriesColumn<TY> _param3)
  {
    return this.IsFifo ? _param3.\u0023\u003DzC52X0FE\u003D() : DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Min(_param1, _param2);
  }

  public TY \u0023\u003DzB0YQXW9xZ973(
    TY _param1,
    TY _param2,
    ISeriesColumn<TY> _param3)
  {
    return this.IsFifo ? _param3.\u0023\u003DzxKsmolQ\u003D() : DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Max(_param1, _param2);
  }

  public TX \u0023\u003DzStrzQ_NInQ8M(
    TX _param1,
    TX _param2,
    ISeriesColumn<TX> _param3)
  {
    return this.IsFifo ? _param3.\u0023\u003DzC52X0FE\u003D() : DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Min(_param1, _param2);
  }

  public TX \u0023\u003DzB0YQXW9xZ973(
    TX _param1,
    TX _param2,
    ISeriesColumn<TX> _param3)
  {
    return this.IsFifo ? _param3.\u0023\u003DzxKsmolQ\u003D() : DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Max(_param1, _param2);
  }

  public void \u0023\u003DztwHsLGWnHCVU(
    \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D _param1)
  {
    this.OnPropertyChanged("LatestYValue");
    EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> zyPb5vD3pcSel = this.\u0023\u003DzyPb5vD3pcSel;
    if (zyPb5vD3pcSel == null)
      return;
    zyPb5vD3pcSel((object) this, new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X(_param1));
  }

  private DoubleRange \u0023\u003DzLc65\u0024pc\u003D(
    IComparable _param1,
    IComparable _param2)
  {
    if (typeof (TY) == typeof (DateTime))
      return new DoubleRange((double) ((DateTime) _param1).Ticks, (double) ((DateTime) _param2).Ticks);
    return !_param1.IsDefined() || !_param2.IsDefined() ? new DoubleRange(double.MinValue, double.MaxValue) : new DoubleRange(_param1.ToDouble(), _param2.ToDouble());
  }

  protected void \u0023\u003Dz2OnEmwtzurH2(int _param1)
  {
    throw new InvalidOperationException($"Append(TX x, params TY[] yValues) in type {this.GetType().Name} must receive only {_param1} list(s) of y values.");
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
    Monitor.Enter(parentSurface.\u0023\u003Dzjatnj7TNvda7());
    return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this, parentSurface.\u0023\u003Dzjatnj7TNvda7());
  }

  public void ResumeUpdates(
    IUpdateSuspender _param1)
  {
    if (_param1.ResumeTargetOnDispose)
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 3);
    if (_param1.Tag == null)
      return;
    Monitor.Exit(_param1.Tag);
  }

  public void DecrementSuspend()
  {
  }

  public virtual int FindClosestPoint(
    IComparable _param1,
    IComparable _param2,
    double _param3,
    double _param4)
  {
    int closestPoint = -1;
    if (_param1 == null || _param2 == null)
      return closestPoint;
    lock (this.myLock)
    {
      int count = this.XValues.Count;
      int num1 = 0;
      int num2 = count - 1;
      TX[] zulcL8RaArray = this.XValues.\u0023\u003Dz1bvQV4SZTWpA<TX>();
      TY[] zE8zkRfYArray = this.YValues.\u0023\u003Dz1bvQV4SZTWpA<TY>();
      TX zulcL8Ra1 = (TX) _param1;
      double num3 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ToDouble(zulcL8Ra1);
      if (this.IsSorted)
      {
        if (_param3.CompareTo(0.0) == 0)
        {
          closestPoint = zulcL8RaArray.\u0023\u003Dzfidg2fLfXmXy<TX>(count, zulcL8Ra1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1, DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D);
        }
        else
        {
          TX zulcL8Ra2 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ZeroValue;
          TX zulcL8Ra3 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Inc(ref zulcL8Ra2);
          if (typeof (TX) == typeof (DateTime) && _param4 >= (double) DateTime.MaxValue.Ticks)
            _param4 = (double) (DateTime.MaxValue.Ticks - 1L);
          TX zulcL8Ra4 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Mult(zulcL8Ra3, _param4);
          TX zulcL8Ra5 = zulcL8Ra4.CompareTo((object) zulcL8Ra1) > 0 ? zulcL8Ra1 : DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Subtract(zulcL8Ra1, zulcL8Ra4);
          TX zulcL8Ra6 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Add(zulcL8Ra1, zulcL8Ra4);
          int num4 = zulcL8RaArray.\u0023\u003Dzfidg2fLfXmXy<TX>(count, zulcL8Ra5, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2, DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D);
          int num5 = zulcL8RaArray.\u0023\u003Dzfidg2fLfXmXy<TX>(count, zulcL8Ra6, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3, DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D);
          if (num4 >= 0)
            num1 = num4;
          if (num5 >= 0)
            num2 = num5;
          num2 -= num1;
        }
      }
      if (closestPoint == -1)
      {
        double num6 = double.MaxValue;
        int num7 = num1 + num2 + 1;
        for (int index = num1; index < num7; ++index)
        {
          double num8 = Math.Abs(DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ToDouble(zulcL8RaArray[index]) - num3);
          if (_param3.CompareTo(0.0) != 0)
          {
            double num9 = DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.ToDouble(DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Subtract(zE8zkRfYArray[index], (TY) _param2));
            num8 += Math.Abs(num9) * _param3;
          }
          if (num8 < num6)
          {
            num6 = num8;
            closestPoint = index;
          }
        }
      }
      return closestPoint;
    }
  }

  public virtual int FindClosestLine(
    IComparable _param1,
    IComparable _param2,
    double _param3,
    double _param4,
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D _param5)
  {
    int num1 = this.Count;
    int closestLine = -1;
    if (num1 < 2 || _param1 == null || _param2 == null)
      return closestLine;
    lock (this.myLock)
    {
      TX[] zulcL8RaArray = this.XValues.\u0023\u003Dz1bvQV4SZTWpA<TX>();
      TY[] zE8zkRfYArray = this.YValues.\u0023\u003Dz1bvQV4SZTWpA<TY>();
      int index1 = 0;
      int count = this.XValues.Count;
      TX zulcL8Ra1 = (TX) _param1;
      TY zE8zkRfY1 = (TY) _param2;
      if (this.IsSorted)
      {
        TX zulcL8Ra2 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ZeroValue;
        zulcL8Ra2 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Inc(ref zulcL8Ra2);
        zulcL8Ra2 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Mult(zulcL8Ra2, _param4);
        int index2 = zulcL8RaArray.\u0023\u003Dzfidg2fLfXmXy<TX>(count, DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Subtract(zulcL8Ra1, zulcL8Ra2), (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2, DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D);
        int index3 = zulcL8RaArray.\u0023\u003Dzfidg2fLfXmXy<TX>(count, DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.Add(zulcL8Ra1, zulcL8Ra2), (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3, DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D);
        if (_param5 == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines)
        {
          while (index2 > 0 && DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(zE8zkRfYArray[index2]))
            --index2;
          while (index3 < this.Count - 1 && DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(zE8zkRfYArray[index3]))
            ++index3;
          if (index3 < this.Count - 1)
            ++index3;
        }
        closestLine = index1 = index2;
        num1 = index3 - index2 + 1;
      }
      double num2 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ToDouble(zulcL8Ra1);
      double num3 = DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.ToDouble(zE8zkRfY1);
      Point point = new Point(num2, num3 * _param3);
      double num4 = double.MaxValue;
      double num5 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ToDouble(zulcL8RaArray[index1]);
      double num6 = DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.ToDouble(zE8zkRfYArray[index1]) * _param3;
      double num7 = num2 - _param4;
      double num8 = num2 + _param4;
      int num9 = index1;
      int num10 = index1 + num1;
      for (int index4 = index1 + 1; index4 < num10; ++index4)
      {
        TY zE8zkRfY2 = zE8zkRfYArray[index4];
        if (DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(zE8zkRfY2))
        {
          if (_param5 == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.Gaps)
            num6 = double.NaN;
        }
        else
        {
          double num11 = DataSeries<TX, TY>.\u0023\u003DzNvw4Cjs\u003D.ToDouble(zulcL8RaArray[index4]);
          double num12 = DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.ToDouble(zE8zkRfY2) * _param3;
          if ((num5 >= num7 || num11 >= num7 ? (num5 <= num8 ? 1 : (num11 <= num8 ? 1 : 0)) : 0) != 0)
          {
            double num13 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(point, new Point(num5, num6), new Point(num11, num12), true);
            if (num13 < num4)
            {
              closestLine = num9;
              num4 = num13;
            }
          }
          num5 = num11;
          num6 = num12;
          num9 = index4;
        }
      }
      return closestLine;
    }
  }

  public virtual void OnBeginRenderPass()
  {
  }
}
