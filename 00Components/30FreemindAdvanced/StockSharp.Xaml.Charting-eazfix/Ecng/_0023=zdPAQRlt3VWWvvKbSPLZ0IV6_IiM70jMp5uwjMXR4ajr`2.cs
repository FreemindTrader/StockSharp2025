// Decompiled with JetBrains decompiler
// Type: #=zdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
public class XyDataSeries<TX, TY> : 
  DataSeries<TX, TY>,
  IDataSeries<TX, TY>,
  \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfvQwvbu0Gd4Imzwk5GIfgC73<TX, TY>,
  ISuspendable,
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoazVlQnP90XoMutgGcLyCRUcP,
  IDataSeries
  where TX : IComparable
  where TY : IComparable
{
  
  private readonly \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY)> \u0023\u003Dz3laIKDGah\u0024Kh;

  public XyDataSeries()
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh = new \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY)>(new Action<IList<(TX, TY)>>(this.\u0023\u003DzGXoUV7Q\u0024_bM\u0024));
  }

  public override DataSeriesType DataSeriesType
  {
    get => (DataSeriesType) 0;
  }

  public override bool HasValues
  {
    get
    {
      return this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz5vfl0A4nWD8Q();
    }
  }

  public override IPointSeries ToPointSeries(
    ResamplingMode _param1,
    IndexRange  _param2,
    int _param3,
    bool _param4,
    bool? _param5,
    IRange _param6,
    IPointResamplerFactory _param7,
    object _param8 = null)
  {
    lock (this.SyncRoot)
      return _param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, TY>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzoEP49rI\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6);
  }

  public override void OnBeginRenderPass()
  {
    base.OnBeginRenderPass();
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
  }

  protected override void \u0023\u003Dzz6J3NZIzdU\u0024X()
  {
    lock (this.SyncRoot)
    {
      if (this.FifoCapacity.HasValue)
      {
        int num = this.FifoCapacity.Value;
        this.\u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TX>(num);
        this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
      }
      else
      {
        this.\u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TX>();
        this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
      }
      ((ICollection<TX>) this.\u0023\u003DzmIwKipw\u003D).Clear();
      ((ICollection<TY>) this.\u0023\u003DzoEP49rI\u003D).Clear();
      this.\u0023\u003Dz3laIKDGah\u0024Kh?.Clear();
    }
  }

  public override void \u0023\u003DzfEbP\u00247w\u003D(int _param1)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      TY yvalue = this.YValues[_param1];
      TX xvalue = this.XValues[_param1];
      this.XValues.RemoveAt(_param1);
      this.YValues.RemoveAt(_param1);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D();
    }
  }

  public override void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D();
    }
  }

  public override IDataSeries<TX, TY> \u0023\u003DzQ8SgRgQ\u003D()
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      XyDataSeries<TX, TY> m70jMp5uwjMxR4ajr = new XyDataSeries<TX, TY>();
      m70jMp5uwjMxR4ajr.set_FifoCapacity(this.FifoCapacity);
      m70jMp5uwjMxR4ajr.set_AcceptsUnsortedData(this.AcceptsUnsortedData);
      m70jMp5uwjMxR4ajr.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) this.XValues, (IEnumerable<TY>) this.YValues);
      return (IDataSeries<TX, TY>) m70jMp5uwjMxR4ajr;
    }
  }

  public override void Append(
    TX _param1,
    params TY[] _param2)
  {
    if (_param2.Length != 1)
      this.\u0023\u003Dz2OnEmwtzurH2(1);
    this.Append(_param1, _param2[0]);
  }

  public override void Append(
    IEnumerable<TX> _param1,
    params IEnumerable<TY>[] _param2)
  {
    if (_param2.Length != 1)
      this.\u0023\u003Dz2OnEmwtzurH2(1);
    this.\u0023\u003Dznc8esWY\u003D(_param1, _param2[0]);
  }

  public virtual void Append(TX _param1, TY _param2)
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((_param1, _param2));
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public virtual void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2)
  {
    if (_param1.\u0023\u003DzMeGSfVE\u003D<TX>())
      return;
    IEnumerator<TX> enumerator1 = _param1.GetEnumerator();
    IEnumerator<TY> enumerator2 = _param2.GetEnumerator();
    lock (this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dzjatnj7TNvda7())
    {
      while (enumerator1.MoveNext())
      {
        if (enumerator2.MoveNext())
          this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((enumerator1.Current, enumerator2.Current));
        else
          break;
      }
    }
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public virtual void UpdateOrderAdornerLayer(
    TX _param1,
    TY _param2)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int index = this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzFH1yjjY\u003D(this.IsSorted, (IComparable) _param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
      if (index == -1)
        return;
      this.\u0023\u003DzoEP49rI\u003D[index] = _param2;
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
    }
  }

  public virtual void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      this.XValues.Insert(_param1, _param2);
      this.YValues.Insert(_param1, _param3);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003Dzs9WSchJIpnF0(this.\u0023\u003DzmIwKipw\u003D, _param1, _param2, this.AcceptsUnsortedData);
    }
  }

  public virtual void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int count1 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param2);
      int count2 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param3);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzPY2yStN8KbO\u0024(this.\u0023\u003DzmIwKipw\u003D, _param1, count2 - count1, _param2, this.AcceptsUnsortedData);
    }
  }

  public override TY GetYMinAt(
    int _param1,
    TY _param2)
  {
    TY yvalue = this.YValues[_param1];
    return !DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(yvalue) ? DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Min(_param2, yvalue) : _param2;
  }

  public override TY GetYMaxAt(
    int _param1,
    TY _param2)
  {
    TY yvalue = this.YValues[_param1];
    return !DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(yvalue) ? DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Max(_param2, yvalue) : _param2;
  }

  private void \u0023\u003DzGXoUV7Q\u0024_bM\u0024(
    IList<(TX, TY)> _param1)
  {
    lock (this.SyncRoot)
    {
      IEnumerable<TX> zulcL8Ras = _param1.Select<(TX, TY), TX>(XyDataSeries<TX, TY>.SomeClass34343383.pubStatic_Action_ChartAxis_ ?? (XyDataSeries<TX, TY>.SomeClass34343383.pubStatic_Action_ChartAxis_ = new Func<(TX, TY), TX>(XyDataSeries<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003Dz01HqdUysEJn5lH\u0024ib6NXi0A\u003D)));
      int count = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(zulcL8Ras);
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY), TY>(XyDataSeries<TX, TY>.SomeClass34343383.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D ?? (XyDataSeries<TX, TY>.SomeClass34343383.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D = new Func<(TX, TY), TY>(XyDataSeries<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003Dz1_IuEwLrYewTrhAZNPbXTd0\u003D))));
      this.DataDistributionCalculator.\u0023\u003DzeU6gWqHRfREz(this.\u0023\u003DzmIwKipw\u003D, count, zulcL8Ras, this.AcceptsUnsortedData);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly XyDataSeries<TX, TY>.SomeClass34343383 SomeMethond0343 = new XyDataSeries<TX, TY>.SomeClass34343383();
    public static Func<(TX, TY), TX> pubStatic_Action_ChartAxis_;
    public static Func<(TX, TY), TY> \u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D;

    public TX \u0023\u003Dz01HqdUysEJn5lH\u0024ib6NXi0A\u003D(
      (TX, TY) _param1)
    {
      return _param1.Item1;
    }

    public TY \u0023\u003Dz1_IuEwLrYewTrhAZNPbXTd0\u003D(
      (TX, TY) _param1)
    {
      return _param1.Item2;
    }
  }
}
