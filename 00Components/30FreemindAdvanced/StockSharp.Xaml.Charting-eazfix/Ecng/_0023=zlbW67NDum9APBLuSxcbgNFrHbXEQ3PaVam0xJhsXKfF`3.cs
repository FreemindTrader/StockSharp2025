// Decompiled with JetBrains decompiler
// Type: #=zlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
public sealed class \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D> : 
  DataSeries<TX, TY>,
  IDataSeries<TX, TY>,
  \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhip318DMhXaWOsdXxIKq2Zfn_,
  \u0023\u003DzKsGTwu6B0A6eMUO4QALnGEFyuKuisWdfO6O5SJH\u00244l_vSJGnVw\u003D\u003D<TX, TY, \u0023\u003DzPqz5cUs\u003D>,
  ISuspendable,
  IDataSeries
  where TX : IComparable
  where TY : IComparable
  where \u0023\u003DzPqz5cUs\u003D : IComparable
{
  
  public static readonly IMath<\u0023\u003DzPqz5cUs\u003D> \u0023\u003DzVgDDgro\u003D = MathHelper.GetMath<\u0023\u003DzPqz5cUs\u003D>();
  
  private readonly \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY, \u0023\u003DzPqz5cUs\u003D)> \u0023\u003Dz3laIKDGah\u0024Kh;
  
  protected ISeriesColumn<\u0023\u003DzPqz5cUs\u003D> \u0023\u003Dz9QqJzD0\u003D = (ISeriesColumn<\u0023\u003DzPqz5cUs\u003D>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<\u0023\u003DzPqz5cUs\u003D>();

  public \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7()
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh = new \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY, \u0023\u003DzPqz5cUs\u003D)>(new Action<IList<(TX, TY, \u0023\u003DzPqz5cUs\u003D)>>(this.\u0023\u003DzGXoUV7Q\u0024_bM\u0024));
  }

  public override DataSeriesType DataSeriesType
  {
    get => (DataSeriesType) 3;
  }

  IList \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhip318DMhXaWOsdXxIKq2Zfn_.\u0023\u003DzITX8mZ2jbGEtwuB21HaSb\u0024gdPlp7UDv9ooAuI7I33qmqua0gRA_Sgmc\u003D()
  {
    return (IList) this.\u0023\u003Dz9QqJzD0\u003D;
  }

  public IList<\u0023\u003DzPqz5cUs\u003D> ZValues
  {
    get => (IList<\u0023\u003DzPqz5cUs\u003D>) this.\u0023\u003Dz9QqJzD0\u003D;
  }

  public override bool HasValues
  {
    get
    {
      return this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003Dz9QqJzD0\u003D.\u0023\u003Dz5vfl0A4nWD8Q();
    }
  }

  public override void \u0023\u003DzfEbP\u00247w\u003D(int _param1)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      this.XValues.RemoveAt(_param1);
      this.YValues.RemoveAt(_param1);
      this.ZValues.RemoveAt(_param1);
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
      this.\u0023\u003Dz9QqJzD0\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D();
    }
  }

  public override IDataSeries<TX, TY> \u0023\u003DzQ8SgRgQ\u003D()
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D> q3PaVam0xJhsXkfF7 = new \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>();
      q3PaVam0xJhsXkfF7.FifoCapacity = this.FifoCapacity;
      q3PaVam0xJhsXkfF7.AcceptsUnsortedData = this.AcceptsUnsortedData;
      q3PaVam0xJhsXkfF7.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) this.XValues, (IEnumerable<TY>) this.YValues, (IEnumerable<\u0023\u003DzPqz5cUs\u003D>) this.ZValues);
      return (IDataSeries<TX, TY>) q3PaVam0xJhsXkfF7;
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
      return (IPointSeries) new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnK_qtBWB9G5KlgWK_lJR7a0x(_param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, TY>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzoEP49rI\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6), _param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, \u0023\u003DzPqz5cUs\u003D>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003Dz9QqJzD0\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6));
  }

  public override void OnBeginRenderPass()
  {
    base.OnBeginRenderPass();
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
  }

  public override HitTestInfo \u0023\u003DzDKPxuEruV71w(
    int _param1)
  {
    lock (this.SyncRoot)
    {
      HitTestInfo zldchDrVsrVyHh6WyiGy = base.\u0023\u003DzDKPxuEruV71w(_param1);
      if (!zldchDrVsrVyHh6WyiGy.\u0023\u003DzMeGSfVE\u003D())
      {
        \u0023\u003DzPqz5cUs\u003D zvalue = this.ZValues[_param1];
        zldchDrVsrVyHh6WyiGy.ZValue = (IComparable) zvalue;
      }
      return zldchDrVsrVyHh6WyiGy;
    }
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
        this.\u0023\u003Dz9QqJzD0\u003D = (ISeriesColumn<\u0023\u003DzPqz5cUs\u003D>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<\u0023\u003DzPqz5cUs\u003D>(num);
      }
      else
      {
        this.\u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TX>();
        this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
        this.\u0023\u003Dz9QqJzD0\u003D = (ISeriesColumn<\u0023\u003DzPqz5cUs\u003D>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<\u0023\u003DzPqz5cUs\u003D>();
      }
      ((ICollection<TX>) this.\u0023\u003DzmIwKipw\u003D).Clear();
      ((ICollection<TY>) this.\u0023\u003DzoEP49rI\u003D).Clear();
      ((ICollection<\u0023\u003DzPqz5cUs\u003D>) this.\u0023\u003Dz9QqJzD0\u003D).Clear();
      this.\u0023\u003Dz3laIKDGah\u0024Kh?.Clear();
    }
  }

  public override void Append(
    TX _param1,
    params TY[] _param2)
  {
    throw new InvalidOperationException($"Append(TX x, params TY[] yValues) in type {this.GetType().Name} must receive X, Y and Z values. Please use the Append(x,y,z) overload");
  }

  public override void Append(
    IEnumerable<TX> _param1,
    params IEnumerable<TY>[] _param2)
  {
    throw new InvalidOperationException($"Append(TX x, params TY[] yValues) in type {this.GetType().Name} must receive X, Y and Z values. Please use the Append(x,y,z) overload");
  }

  public void \u0023\u003Dznc8esWY\u003D(
    TX _param1,
    TY _param2,
    \u0023\u003DzPqz5cUs\u003D _param3)
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((_param1, _param2, _param3));
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2,
    IEnumerable<\u0023\u003DzPqz5cUs\u003D> _param3)
  {
    if (_param1.\u0023\u003DzMeGSfVE\u003D<TX>())
      return;
    IEnumerator<TX> enumerator1 = _param1.GetEnumerator();
    IEnumerator<TY> enumerator2 = _param2.GetEnumerator();
    IEnumerator<\u0023\u003DzPqz5cUs\u003D> enumerator3 = _param3.GetEnumerator();
    lock (this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dzjatnj7TNvda7())
    {
      while (enumerator1.MoveNext())
      {
        if (enumerator2.MoveNext())
        {
          if (enumerator3.MoveNext())
            this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((enumerator1.Current, enumerator2.Current, enumerator3.Current));
          else
            break;
        }
        else
          break;
      }
    }
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public void UpdateOrderAdornerLayer(
    TX _param1,
    TY _param2,
    \u0023\u003DzPqz5cUs\u003D _param3,
    int _param4)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int num = this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzFH1yjjY\u003D(this.IsSorted, (IComparable) _param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
      if (num == -1)
        return;
      int index = num - _param4;
      if (index < 0)
        throw new InvalidOperationException();
      this.\u0023\u003DzoEP49rI\u003D[index] = _param2;
      this.\u0023\u003Dz9QqJzD0\u003D[index] = _param3;
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
    }
  }

  public void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3,
    \u0023\u003DzPqz5cUs\u003D _param4)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      this.XValues.Insert(_param1, _param2);
      this.YValues.Insert(_param1, _param3);
      this.ZValues.Insert(_param1, _param4);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003Dzs9WSchJIpnF0(this.\u0023\u003DzmIwKipw\u003D, _param1, _param2, this.AcceptsUnsortedData);
    }
  }

  public void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<\u0023\u003DzPqz5cUs\u003D> _param4)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int count1 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param2);
      int count2 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param3);
      this.\u0023\u003Dz9QqJzD0\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param4);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzPY2yStN8KbO\u0024(this.\u0023\u003DzmIwKipw\u003D, _param1, count2 - count1, _param2, this.AcceptsUnsortedData);
    }
  }

  private void \u0023\u003DzGXoUV7Q\u0024_bM\u0024(
    IList<(TX, TY, \u0023\u003DzPqz5cUs\u003D)> _param1)
  {
    lock (this.SyncRoot)
    {
      IEnumerable<TX> zulcL8Ras = _param1.Select<(TX, TY, \u0023\u003DzPqz5cUs\u003D), TX>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D ?? (\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D = new Func<(TX, TY, \u0023\u003DzPqz5cUs\u003D), TX>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003DzpMHI4jfrG6pvyYLWoaHs3No\u003D)));
      int count = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(zulcL8Ras);
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, \u0023\u003DzPqz5cUs\u003D), TY>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D ?? (\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D = new Func<(TX, TY, \u0023\u003DzPqz5cUs\u003D), TY>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003Dzw6g7ZuECV2i7kaLCerU_JP8\u003D))));
      this.\u0023\u003Dz9QqJzD0\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzPqz5cUs\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzMfTyzDitoO\u0024eks3zDA\u003D\u003D ?? (\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzMfTyzDitoO\u0024eks3zDA\u003D\u003D = new Func<(TX, TY, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzPqz5cUs\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003DzXqGoTxjIqWhMYTQ3LamDD1s\u003D))));
      this.DataDistributionCalculator.\u0023\u003DzeU6gWqHRfREz(this.\u0023\u003DzmIwKipw\u003D, count, zulcL8Ras, this.AcceptsUnsortedData);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<TX, TY, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383();
    public static Func<(TX, TY, \u0023\u003DzPqz5cUs\u003D), TX> \u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D;
    public static Func<(TX, TY, \u0023\u003DzPqz5cUs\u003D), TY> \u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D;
    public static Func<(TX, TY, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzPqz5cUs\u003D> \u0023\u003DzMfTyzDitoO\u0024eks3zDA\u003D\u003D;

    public TX \u0023\u003DzpMHI4jfrG6pvyYLWoaHs3No\u003D(
      (TX, TY, \u0023\u003DzPqz5cUs\u003D) _param1)
    {
      return _param1.Item1;
    }

    public TY \u0023\u003Dzw6g7ZuECV2i7kaLCerU_JP8\u003D(
      (TX, TY, \u0023\u003DzPqz5cUs\u003D) _param1)
    {
      return _param1.Item2;
    }

    public \u0023\u003DzPqz5cUs\u003D \u0023\u003DzXqGoTxjIqWhMYTQ3LamDD1s\u003D(
      (TX, TY, \u0023\u003DzPqz5cUs\u003D) _param1)
    {
      return _param1.Item3;
    }
  }
}
