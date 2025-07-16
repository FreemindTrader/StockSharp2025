// Decompiled with JetBrains decompiler
// Type: #=zpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
public sealed class \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY> : 
  DataSeries<TX, TY>,
  IDataSeries<TX, TY>,
  \u0023\u003DzlbJvQa3SY_TV_FXt6bD53NJ_tCeoxuaO6u0w2\u0024KuAnng9SymwQ\u003D\u003D<TX, TY>,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  \u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH
  where TX : IComparable
  where TY : IComparable
{
  
  private ISeriesColumn<TY> \u0023\u003DzdV7qEkhxNf5\u0024 = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  private readonly \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY, TY)> \u0023\u003Dz3laIKDGah\u0024Kh;

  public \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq()
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh = new \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY, TY)>(new Action<IList<(TX, TY, TY)>>(this.\u0023\u003DzGXoUV7Q\u0024_bM\u0024));
  }

  public override IRange YRange
  {
    get
    {
      TY zE8zkRfY1 = default (TY);
      TY zE8zkRfY2 = default (TY);
      TY zE8zkRfY3 = default (TY);
      TY zE8zkRfY4 = default (TY);
      \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz\u0024zWmmGTAbDON<TY>((IEnumerable<TY>) this.\u0023\u003DzoEP49rI\u003D, out zE8zkRfY1, out zE8zkRfY2);
      \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz\u0024zWmmGTAbDON<TY>((IEnumerable<TY>) this.\u0023\u003DzdV7qEkhxNf5\u0024, out zE8zkRfY3, out zE8zkRfY4);
      return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Min(zE8zkRfY1, zE8zkRfY3), (IComparable) DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Max(zE8zkRfY2, zE8zkRfY4));
    }
  }

  public override DataSeriesType DataSeriesType
  {
    get => (DataSeriesType) 2;
  }

  IList \u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH.\u0023\u003DzStu4Q8haFXFi7P9dDSHFmKq_LUZj9YMqbYHsDsW7jsd6UjbP7AU\u0024qLo\u003D()
  {
    return (IList) this.\u0023\u003DzdV7qEkhxNf5\u0024;
  }

  public IList<TY> Y1Values
  {
    get => (IList<TY>) this.\u0023\u003DzdV7qEkhxNf5\u0024;
  }

  public override bool HasValues
  {
    get
    {
      return this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzdV7qEkhxNf5\u0024.\u0023\u003Dz5vfl0A4nWD8Q();
    }
  }

  public override void \u0023\u003DzfEbP\u00247w\u003D(int _param1)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      TY yvalue = this.YValues[_param1];
      TY y1Value = this.Y1Values[_param1];
      TX xvalue = this.XValues[_param1];
      this.XValues.RemoveAt(_param1);
      this.YValues.RemoveAt(_param1);
      this.Y1Values.RemoveAt(_param1);
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
      this.\u0023\u003DzdV7qEkhxNf5\u0024.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D();
    }
  }

  public override IDataSeries<TX, TY> \u0023\u003DzQ8SgRgQ\u003D()
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY> cqssyY52HuuP8Kgkq = new \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>();
      cqssyY52HuuP8Kgkq.FifoCapacity = this.FifoCapacity;
      cqssyY52HuuP8Kgkq.AcceptsUnsortedData = this.AcceptsUnsortedData;
      cqssyY52HuuP8Kgkq.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) this.XValues, (IEnumerable<TY>) this.YValues, (IEnumerable<TY>) this.Y1Values);
      return (IDataSeries<TX, TY>) cqssyY52HuuP8Kgkq;
    }
  }

  public override TY GetYMinAt(
    int _param1,
    TY _param2)
  {
    TY y1Value = this.Y1Values[_param1];
    TY yvalue = this.YValues[_param1];
    return !DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(yvalue) && !DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(y1Value) ? DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Min(DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Min(yvalue, y1Value), _param2) : _param2;
  }

  public override TY GetYMaxAt(
    int _param1,
    TY _param2)
  {
    TY y1Value = this.Y1Values[_param1];
    TY yvalue = this.YValues[_param1];
    return !DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(yvalue) && !DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(y1Value) ? DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Max(DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Max(yvalue, y1Value), _param2) : _param2;
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
      return (IPointSeries) new \u0023\u003Dz59_koqr2EQdapDcFKycZuMFujzBx_Vn_sKSeFk9GdLpI(_param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, TY>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzoEP49rI\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6), _param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, TY>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzdV7qEkhxNf5\u0024, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6));
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
        TY y1Value = this.Y1Values[_param1];
        zldchDrVsrVyHh6WyiGy.\u0023\u003Dz3JT1kQLA9WwW((IComparable) y1Value);
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
        this.\u0023\u003DzdV7qEkhxNf5\u0024 = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
      }
      else
      {
        this.\u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TX>();
        this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
        this.\u0023\u003DzdV7qEkhxNf5\u0024 = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
      }
      ((ICollection<TX>) this.\u0023\u003DzmIwKipw\u003D).Clear();
      ((ICollection<TY>) this.\u0023\u003DzoEP49rI\u003D).Clear();
      ((ICollection<TY>) this.\u0023\u003DzdV7qEkhxNf5\u0024).Clear();
      this.\u0023\u003Dz3laIKDGah\u0024Kh?.Clear();
    }
  }

  public override void Append(
    TX _param1,
    params TY[] _param2)
  {
    if (_param2.Length != 2)
      this.\u0023\u003Dz2OnEmwtzurH2(2);
    this.\u0023\u003Dznc8esWY\u003D(_param1, _param2[0], _param2[1]);
  }

  public override void Append(
    IEnumerable<TX> _param1,
    params IEnumerable<TY>[] _param2)
  {
    if (_param2.Length != 2)
      this.\u0023\u003Dz2OnEmwtzurH2(2);
    this.\u0023\u003Dznc8esWY\u003D(_param1, _param2[0], _param2[1]);
  }

  public void \u0023\u003Dznc8esWY\u003D(
    TX _param1,
    TY _param2,
    TY _param3)
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((_param1, _param2, _param3));
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2,
    IEnumerable<TY> _param3)
  {
    if (_param1.\u0023\u003DzMeGSfVE\u003D<TX>())
      return;
    IEnumerator<TX> enumerator1 = _param1.GetEnumerator();
    IEnumerator<TY> enumerator2 = _param2.GetEnumerator();
    IEnumerator<TY> enumerator3 = _param3.GetEnumerator();
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
    TY _param3)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int index = this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzFH1yjjY\u003D(this.IsSorted, (IComparable) _param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
      if (index == -1)
        return;
      this.\u0023\u003DzoEP49rI\u003D[index] = _param2;
      this.\u0023\u003DzdV7qEkhxNf5\u0024[index] = _param3;
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
    }
  }

  public void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3,
    TY _param4)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      this.XValues.Insert(_param1, _param2);
      this.YValues.Insert(_param1, _param3);
      this.Y1Values.Insert(_param1, _param4);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003Dzs9WSchJIpnF0(this.\u0023\u003DzmIwKipw\u003D, _param1, _param2, this.AcceptsUnsortedData);
    }
  }

  public void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<TY> _param4)
  {
    \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz2wWd_ME\u003D<TY>(_param3);
    \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dzj_Gd1fY\u003D<TY>(_param3);
    \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz2wWd_ME\u003D<TY>(_param4);
    \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dzj_Gd1fY\u003D<TY>(_param4);
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int count1 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param2);
      int count2 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param3);
      this.\u0023\u003DzdV7qEkhxNf5\u0024.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param4);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzPY2yStN8KbO\u0024(this.\u0023\u003DzmIwKipw\u003D, _param1, count2 - count1, _param2, this.AcceptsUnsortedData);
    }
  }

  private void \u0023\u003DzGXoUV7Q\u0024_bM\u0024(
    IList<(TX, TY, TY)> _param1)
  {
    lock (this.SyncRoot)
    {
      IEnumerable<TX> zulcL8Ras = _param1.Select<(TX, TY, TY), TX>(\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D ?? (\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D = new Func<(TX, TY, TY), TX>(\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003DzSLcMrIGBCU9mMDhywOkmIqQ\u003D)));
      int count = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(zulcL8Ras);
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, TY), TY>(\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.\u0023\u003Dz9nak7HwDXuTSrEOMIQ\u003D\u003D ?? (\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.\u0023\u003Dz9nak7HwDXuTSrEOMIQ\u003D\u003D = new Func<(TX, TY, TY), TY>(\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003DzypZc3tG24P1dwvd7udNc4ls\u003D))));
      this.\u0023\u003DzdV7qEkhxNf5\u0024.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, TY), TY>(\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.\u0023\u003DzCZpoELBrGQZVZ\u002423Jw\u003D\u003D ?? (\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.\u0023\u003DzCZpoELBrGQZVZ\u002423Jw\u003D\u003D = new Func<(TX, TY, TY), TY>(\u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003DziKi3IUJ3eOzPID9MI7uBPjs\u003D))));
      this.DataDistributionCalculator.\u0023\u003DzeU6gWqHRfREz(this.\u0023\u003DzmIwKipw\u003D, count, zulcL8Ras, this.AcceptsUnsortedData);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, TY>.SomeClass34343383();
    public static Func<(TX, TY, TY), TX> \u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D;
    public static Func<(TX, TY, TY), TY> \u0023\u003Dz9nak7HwDXuTSrEOMIQ\u003D\u003D;
    public static Func<(TX, TY, TY), TY> \u0023\u003DzCZpoELBrGQZVZ\u002423Jw\u003D\u003D;

    public TX \u0023\u003DzSLcMrIGBCU9mMDhywOkmIqQ\u003D(
      (TX, TY, TY) _param1)
    {
      return _param1.Item1;
    }

    public TY \u0023\u003DzypZc3tG24P1dwvd7udNc4ls\u003D(
      (TX, TY, TY) _param1)
    {
      return _param1.Item2;
    }

    public TY \u0023\u003DziKi3IUJ3eOzPID9MI7uBPjs\u003D(
      (TX, TY, TY) _param1)
    {
      return _param1.Item3;
    }
  }
}
