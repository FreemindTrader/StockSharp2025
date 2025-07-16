// Decompiled with JetBrains decompiler
// Type: #=zUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
public sealed class \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY> : 
  DataSeries<TX, TY>,
  IDataSeries<TX, TY>,
  IOhlcDataSeries,
  \u0023\u003DzDH5EtXmKZCpH30z_eydhO42Ygtxj0A9MtVBlpvd5g5Ii2CglZA\u003D\u003D<TX, TY>,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  \u0023\u003DztorG3HTUDpMsfjPqFEEe9HUBXNG1JlzD7u56rrBDlcZ3bDSt5iNajas\u003D
  where TX : IComparable
  where TY : IComparable
{
  
  private ISeriesColumn<TY> \u0023\u003DzjCADV9yqxvmI = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  private ISeriesColumn<TY> \u0023\u003DzyaS0FCQuIbTz = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  private ISeriesColumn<TY> \u0023\u003Dz_YpB1WqdIoXd = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  private ISeriesColumn<TY> \u0023\u003DzHO8H7X_wl7Th;
  
  private readonly \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY, TY, TY, TY)> \u0023\u003Dz3laIKDGah\u0024Kh;
  
  private TimeSpan? \u0023\u003DzM5bivBrwoPG_dwiiOcdDrOjpvebr;

  public \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D()
  {
    this.\u0023\u003DzHO8H7X_wl7Th = this.\u0023\u003DzoEP49rI\u003D;
    this.\u0023\u003Dz3laIKDGah\u0024Kh = new \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(TX, TY, TY, TY, TY)>(new Action<IList<(TX, TY, TY, TY, TY)>>(this.\u0023\u003DzGXoUV7Q\u0024_bM\u0024));
  }

  public TimeSpan? Timeframe
  {
    get => this.\u0023\u003DzM5bivBrwoPG_dwiiOcdDrOjpvebr;
    set => this.\u0023\u003DzM5bivBrwoPG_dwiiOcdDrOjpvebr = value;
  }

  public override IRange YRange
  {
    get
    {
      TY zE8zkRfY = \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dzj_Gd1fY\u003D<TY>((IEnumerable<TY>) this.\u0023\u003DzyaS0FCQuIbTz);
      return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz2wWd_ME\u003D<TY>((IEnumerable<TY>) this.\u0023\u003Dz_YpB1WqdIoXd), (IComparable) zE8zkRfY);
    }
  }

  public override bool HasValues
  {
    get
    {
      return this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzjCADV9yqxvmI.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzyaS0FCQuIbTz.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003Dz_YpB1WqdIoXd.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzHO8H7X_wl7Th.\u0023\u003Dz5vfl0A4nWD8Q();
    }
  }

  public override DataSeriesType DataSeriesType
  {
    get => (DataSeriesType) 1;
  }

  public override HitTestInfo \u0023\u003DzDKPxuEruV71w(
    int _param1)
  {
    lock (this.SyncRoot)
    {
      HitTestInfo zldchDrVsrVyHh6WyiGy = base.\u0023\u003DzDKPxuEruV71w(_param1);
      if (!zldchDrVsrVyHh6WyiGy.\u0023\u003DzMeGSfVE\u003D())
      {
        zldchDrVsrVyHh6WyiGy.\u0023\u003Dz5MmJ0gJc_yvJ((IComparable) this.OpenValues[_param1]);
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzA4iUVKOE1DJm((IComparable) this.HighValues[_param1]);
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzAvOnsWR70_Q9((IComparable) this.LowValues[_param1]);
        zldchDrVsrVyHh6WyiGy.\u0023\u003Dz566Xwh\u0024CapeW((IComparable) this.CloseValues[_param1]);
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
        this.\u0023\u003DzjCADV9yqxvmI = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
        this.\u0023\u003DzyaS0FCQuIbTz = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
        this.\u0023\u003Dz_YpB1WqdIoXd = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
        this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
        this.\u0023\u003DzHO8H7X_wl7Th = this.\u0023\u003DzoEP49rI\u003D;
      }
      else
      {
        this.\u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TX>();
        this.\u0023\u003DzjCADV9yqxvmI = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
        this.\u0023\u003DzyaS0FCQuIbTz = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
        this.\u0023\u003Dz_YpB1WqdIoXd = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
        this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
        this.\u0023\u003DzHO8H7X_wl7Th = this.\u0023\u003DzoEP49rI\u003D;
      }
      ((ICollection<TX>) this.\u0023\u003DzmIwKipw\u003D).Clear();
      ((ICollection<TY>) this.\u0023\u003DzjCADV9yqxvmI).Clear();
      ((ICollection<TY>) this.\u0023\u003DzyaS0FCQuIbTz).Clear();
      ((ICollection<TY>) this.\u0023\u003Dz_YpB1WqdIoXd).Clear();
      ((ICollection<TY>) this.\u0023\u003DzHO8H7X_wl7Th).Clear();
      this.\u0023\u003Dz3laIKDGah\u0024Kh?.Clear();
    }
  }

  public override void \u0023\u003DzfEbP\u00247w\u003D(int _param1)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      this.XValues.RemoveAt(_param1);
      this.YValues.RemoveAt(_param1);
      this.OpenValues.RemoveAt(_param1);
      this.HighValues.RemoveAt(_param1);
      this.LowValues.RemoveAt(_param1);
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
      this.\u0023\u003DzjCADV9yqxvmI.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
      this.\u0023\u003DzyaS0FCQuIbTz.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
      this.\u0023\u003Dz_YpB1WqdIoXd.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D();
    }
  }

  public override IDataSeries<TX, TY> \u0023\u003DzQ8SgRgQ\u003D()
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY> s5kum66IsFwjrEpTng = new \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>();
      s5kum66IsFwjrEpTng.FifoCapacity = this.FifoCapacity;
      s5kum66IsFwjrEpTng.AcceptsUnsortedData = this.AcceptsUnsortedData;
      s5kum66IsFwjrEpTng.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) this.XValues, (IEnumerable<TY>) this.OpenValues, (IEnumerable<TY>) this.HighValues, (IEnumerable<TY>) this.LowValues, (IEnumerable<TY>) this.CloseValues);
      return (IDataSeries<TX, TY>) s5kum66IsFwjrEpTng;
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
    \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym? nullable = _param8 as \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym?;
    if (!nullable.HasValue)
    {
      lock (this.SyncRoot)
      {
        ResamplingMode tlZqozNbfOhxiykg1 = _param1 == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Mid;
        ResamplingMode tlZqozNbfOhxiykg2 = _param1 == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Max;
        ResamplingMode tlZqozNbfOhxiykg3 = _param1 == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Min;
        \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOby\u0024vWkeqZwE94P6zz4sY0BD_b\u0024iDA\u003D\u003D e94P6zz4sY0BdBIDa = _param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, TY>();
        return (IPointSeries) new \u0023\u003Dz4simfJ\u0024MaSW7GKJ8rfRfj96\u0024WwidGdMbGmKL5cr6UQQj(e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzjCADV9yqxvmI, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6), e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg2, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzyaS0FCQuIbTz, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6), e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg3, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003Dz_YpB1WqdIoXd, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6), e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzHO8H7X_wl7Th, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6));
      }
    }
    ISeriesColumn<TY> imiZzTvoZnJ0QeWy6u = nullable.Value == \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Open ? this.\u0023\u003DzjCADV9yqxvmI : (nullable.Value == \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.High ? this.\u0023\u003DzyaS0FCQuIbTz : (nullable.Value == \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Low ? this.\u0023\u003Dz_YpB1WqdIoXd : this.\u0023\u003DzHO8H7X_wl7Th));
    lock (this.SyncRoot)
      return _param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, TY>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) imiZzTvoZnJ0QeWy6u, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6);
  }

  public override void OnBeginRenderPass()
  {
    base.OnBeginRenderPass();
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
  }

  IList IOhlcDataSeries.\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW54c2mwiNc4WXjSzSlwdUwXKQKZNJW2jstDg\u003D()
  {
    return (IList) this.\u0023\u003DzjCADV9yqxvmI;
  }

  IList IOhlcDataSeries.\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c_eUQoh_jqpYInpgSOLXu1sQAZZdp6te1No\u003D()
  {
    return (IList) this.\u0023\u003DzyaS0FCQuIbTz;
  }

  IList IOhlcDataSeries.\u0023\u003DzMDDpCIYr0KRiCa3HPMUguoY\u0024wcyLX59pC88CVEuKYg4H1EPmKVNTo5E\u003D()
  {
    return (IList) this.\u0023\u003Dz_YpB1WqdIoXd;
  }

  IList IOhlcDataSeries.\u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3TgDIMagtqnJdtgozQMeO8\u0024E4Q0xJhgxck\u003D()
  {
    return (IList) this.\u0023\u003DzHO8H7X_wl7Th;
  }

  public IList<TY> OpenValues
  {
    get => (IList<TY>) this.\u0023\u003DzjCADV9yqxvmI;
  }

  public IList<TY> HighValues
  {
    get => (IList<TY>) this.\u0023\u003DzyaS0FCQuIbTz;
  }

  public IList<TY> LowValues
  {
    get => (IList<TY>) this.\u0023\u003Dz_YpB1WqdIoXd;
  }

  public IList<TY> CloseValues
  {
    get => (IList<TY>) this.\u0023\u003DzHO8H7X_wl7Th;
  }

  public override void Append(
    TX _param1,
    params TY[] _param2)
  {
    if (_param2.Length != 4)
      this.\u0023\u003Dz2OnEmwtzurH2(4);
    this.\u0023\u003Dznc8esWY\u003D(_param1, _param2[0], _param2[1], _param2[2], _param2[3]);
  }

  public override void Append(
    IEnumerable<TX> _param1,
    params IEnumerable<TY>[] _param2)
  {
    if (_param2.Length != 4)
      this.\u0023\u003Dz2OnEmwtzurH2(4);
    this.\u0023\u003Dznc8esWY\u003D(_param1, _param2[0], _param2[1], _param2[2], _param2[3]);
  }

  public void \u0023\u003Dznc8esWY\u003D(
    TX _param1,
    TY _param2,
    TY _param3,
    TY _param4,
    TY _param5)
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((_param1, _param2, _param3, _param4, _param5));
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<TY> _param4,
    IEnumerable<TY> _param5)
  {
    if (_param1.\u0023\u003DzMeGSfVE\u003D<TX>())
      return;
    IEnumerator<TX> enumerator1 = _param1.GetEnumerator();
    IEnumerator<TY> enumerator2 = _param2.GetEnumerator();
    IEnumerator<TY> enumerator3 = _param3.GetEnumerator();
    IEnumerator<TY> enumerator4 = _param4.GetEnumerator();
    IEnumerator<TY> enumerator5 = _param5.GetEnumerator();
    lock (this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dzjatnj7TNvda7())
    {
      while (enumerator1.MoveNext())
      {
        if (enumerator2.MoveNext())
        {
          if (enumerator3.MoveNext())
          {
            if (enumerator4.MoveNext())
            {
              if (enumerator5.MoveNext())
                this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((enumerator1.Current, enumerator2.Current, enumerator3.Current, enumerator4.Current, enumerator5.Current));
              else
                break;
            }
            else
              break;
          }
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
    TY _param3,
    TY _param4,
    TY _param5)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int index = this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzFH1yjjY\u003D(this.IsSorted, (IComparable) _param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
      if (index == -1)
        return;
      this.\u0023\u003DzjCADV9yqxvmI[index] = _param2;
      this.\u0023\u003DzyaS0FCQuIbTz[index] = _param3;
      this.\u0023\u003Dz_YpB1WqdIoXd[index] = _param4;
      this.\u0023\u003DzHO8H7X_wl7Th[index] = _param5;
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
    }
  }

  public void \u0023\u003DzzfUM_io\u003D(
    int _param1,
    TX _param2,
    TY _param3,
    TY _param4,
    TY _param5,
    TY _param6)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      this.XValues.Insert(_param1, _param2);
      this.OpenValues.Insert(_param1, _param3);
      this.HighValues.Insert(_param1, _param4);
      this.LowValues.Insert(_param1, _param5);
      this.CloseValues.Insert(_param1, _param6);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003Dzs9WSchJIpnF0(this.\u0023\u003DzmIwKipw\u003D, _param1, _param2, this.AcceptsUnsortedData);
    }
  }

  public void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<TX> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<TY> _param4,
    IEnumerable<TY> _param5,
    IEnumerable<TY> _param6)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      int count1 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param2);
      int count2 = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzjCADV9yqxvmI.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param3);
      this.\u0023\u003DzyaS0FCQuIbTz.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param4);
      this.\u0023\u003Dz_YpB1WqdIoXd.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param5);
      this.\u0023\u003DzHO8H7X_wl7Th.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param6);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzPY2yStN8KbO\u0024(this.\u0023\u003DzmIwKipw\u003D, _param1, count2 - count1, _param2, this.AcceptsUnsortedData);
    }
  }

  public override TY GetYMaxAt(
    int _param1,
    TY _param2)
  {
    TY highValue = this.HighValues[_param1];
    return DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(highValue) ? _param2 : DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Max(_param2, highValue);
  }

  public override TY GetYMinAt(
    int _param1,
    TY _param2)
  {
    TY lowValue = this.LowValues[_param1];
    return DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(lowValue) ? _param2 : DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Min(_param2, lowValue);
  }

  public void \u0023\u003DzGXoUV7Q\u0024_bM\u0024()
  {
    lock (this.SyncRoot)
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
  }

  private void \u0023\u003DzGXoUV7Q\u0024_bM\u0024(
    IList<(TX, TY, TY, TY, TY)> _param1)
  {
    lock (this.SyncRoot)
    {
      IEnumerable<TX> zulcL8Ras = _param1.Select<(TX, TY, TY, TY, TY), TX>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003DzPdPSPWl213y\u00246nt9EQ\u003D\u003D ?? (\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003DzPdPSPWl213y\u00246nt9EQ\u003D\u003D = new Func<(TX, TY, TY, TY, TY), TX>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003DzmIPZEwgchz\u0024sCHTuGwrsJTY\u003D)));
      int count = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(zulcL8Ras);
      this.\u0023\u003DzjCADV9yqxvmI.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003Dz3i1lPFWkqPq\u0024Bv_MiA\u003D\u003D ?? (\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003Dz3i1lPFWkqPq\u0024Bv_MiA\u003D\u003D = new Func<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003DzzYZOLRwyB3XEoUeK\u0024G2I14A\u003D))));
      this.\u0023\u003DzyaS0FCQuIbTz.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003DzytA694d12YK1M5HxGg\u003D\u003D ?? (\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003DzytA694d12YK1M5HxGg\u003D\u003D = new Func<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003Dz3WdUimM4quneZzxXPVH0AMs\u003D))));
      this.\u0023\u003Dz_YpB1WqdIoXd.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003DzZ56xLcbG8XBu52\u0024XtQ\u003D\u003D ?? (\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003DzZ56xLcbG8XBu52\u0024XtQ\u003D\u003D = new Func<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003Dzv7wB\u0024Xs8LHaA0XIEpOZO7Mk\u003D))));
      this.\u0023\u003DzHO8H7X_wl7Th.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003Dz\u0024i0ZdiXPWL4LNqdRiw\u003D\u003D ?? (\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.\u0023\u003Dz\u0024i0ZdiXPWL4LNqdRiw\u003D\u003D = new Func<(TX, TY, TY, TY, TY), TY>(\u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383.SomeMethond0343.\u0023\u003DzX54OEBE42FtywkH4BPLC8ew\u003D))));
      this.DataDistributionCalculator.\u0023\u003DzeU6gWqHRfREz(this.\u0023\u003DzmIwKipw\u003D, count, zulcL8Ras, this.AcceptsUnsortedData);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<TX, TY>.SomeClass34343383();
    public static Func<(TX, TY, TY, TY, TY), TX> \u0023\u003DzPdPSPWl213y\u00246nt9EQ\u003D\u003D;
    public static Func<(TX, TY, TY, TY, TY), TY> \u0023\u003Dz3i1lPFWkqPq\u0024Bv_MiA\u003D\u003D;
    public static Func<(TX, TY, TY, TY, TY), TY> \u0023\u003DzytA694d12YK1M5HxGg\u003D\u003D;
    public static Func<(TX, TY, TY, TY, TY), TY> \u0023\u003DzZ56xLcbG8XBu52\u0024XtQ\u003D\u003D;
    public static Func<(TX, TY, TY, TY, TY), TY> \u0023\u003Dz\u0024i0ZdiXPWL4LNqdRiw\u003D\u003D;

    public TX \u0023\u003DzmIPZEwgchz\u0024sCHTuGwrsJTY\u003D(
      (TX, TY, TY, TY, TY) _param1)
    {
      return _param1.Item1;
    }

    public TY \u0023\u003DzzYZOLRwyB3XEoUeK\u0024G2I14A\u003D(
      (TX, TY, TY, TY, TY) _param1)
    {
      return _param1.Item2;
    }

    public TY \u0023\u003Dz3WdUimM4quneZzxXPVH0AMs\u003D(
      (TX, TY, TY, TY, TY) _param1)
    {
      return _param1.Item3;
    }

    public TY \u0023\u003Dzv7wB\u0024Xs8LHaA0XIEpOZO7Mk\u003D(
      (TX, TY, TY, TY, TY) _param1)
    {
      return _param1.Item4;
    }

    public TY \u0023\u003DzX54OEBE42FtywkH4BPLC8ew\u003D(
      (TX, TY, TY, TY, TY) _param1)
    {
      return _param1.Item5;
    }
  }
}
