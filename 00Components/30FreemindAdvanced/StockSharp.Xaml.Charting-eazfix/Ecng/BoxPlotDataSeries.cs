// Decompiled with JetBrains decompiler
// Type: #=z_$BhX3lQii9_VUtVozqEezGJrigJfmRXcKJON_mXNNQFlZ3CVw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
public sealed class BoxPlotDataSeries<TX, TY> : 
  DataSeries<TX, TY>
  where TX : IComparable
  where TY : IComparable
{
  
  private ISeriesColumn<TY> \u0023\u003DzwrNTkEFZoB0H = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  private ISeriesColumn<TY> \u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  private ISeriesColumn<TY> \u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
  
  private ISeriesColumn<TY> \u0023\u003DztR5HGEq8pm\u00248 = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();

  public override bool HasValues
  {
    get
    {
      return this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzwrNTkEFZoB0H.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D.\u0023\u003Dz5vfl0A4nWD8Q() && this.\u0023\u003DztR5HGEq8pm\u00248.\u0023\u003Dz5vfl0A4nWD8Q();
    }
  }

  public override DataSeriesType DataSeriesType
  {
    get => (DataSeriesType) 5;
  }

  public IList<TY> MedianValues => this.YValues;

  public IList<TY> MinimumValues
  {
    get => (IList<TY>) this.\u0023\u003DzwrNTkEFZoB0H;
  }

  public IList<TY> MaximumValues
  {
    get => (IList<TY>) this.\u0023\u003DztR5HGEq8pm\u00248;
  }

  public IList<TY> UpperQuartileValues
  {
    get => (IList<TY>) this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D;
  }

  public IList<TY> LowerQuartileValues
  {
    get => (IList<TY>) this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D;
  }

  protected override void \u0023\u003Dzz6J3NZIzdU\u0024X()
  {
    if (this.FifoCapacity.HasValue)
    {
      int num = this.FifoCapacity.Value;
      this.\u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TX>(num);
      this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
      this.\u0023\u003DzwrNTkEFZoB0H = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
      this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
      this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
      this.\u0023\u003DztR5HGEq8pm\u00248 = (ISeriesColumn<TY>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<TY>(num);
    }
    else
    {
      this.\u0023\u003DzmIwKipw\u003D = (ISeriesColumn<TX>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TX>();
      this.\u0023\u003DzoEP49rI\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
      this.\u0023\u003DzwrNTkEFZoB0H = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
      this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
      this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
      this.\u0023\u003DztR5HGEq8pm\u00248 = (ISeriesColumn<TY>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<TY>();
    }
    ((ICollection<TX>) this.\u0023\u003DzmIwKipw\u003D).Clear();
    ((ICollection<TY>) this.\u0023\u003DzoEP49rI\u003D).Clear();
    ((ICollection<TY>) this.\u0023\u003DzwrNTkEFZoB0H).Clear();
    ((ICollection<TY>) this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D).Clear();
    ((ICollection<TY>) this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D).Clear();
    ((ICollection<TY>) this.\u0023\u003DztR5HGEq8pm\u00248).Clear();
  }

  public override void \u0023\u003DzfEbP\u00247w\u003D(int _param1)
  {
    lock (this.SyncRoot)
    {
      TY yvalue = this.YValues[_param1];
      TX xvalue = this.XValues[_param1];
      this.XValues.RemoveAt(_param1);
      this.YValues.RemoveAt(_param1);
      ((IList<TY>) this.\u0023\u003DztR5HGEq8pm\u00248).RemoveAt(_param1);
      ((IList<TY>) this.\u0023\u003DzwrNTkEFZoB0H).RemoveAt(_param1);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D();
    }
  }

  public override void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2)
  {
    this.\u0023\u003DzmIwKipw\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
    this.\u0023\u003DzoEP49rI\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
    this.\u0023\u003DztR5HGEq8pm\u00248.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
    this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
    this.\u0023\u003DzoEP49rI\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
    this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
    this.\u0023\u003DzwrNTkEFZoB0H.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
    this.DataDistributionCalculator.\u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D();
  }

  public override IDataSeries<TX, TY> \u0023\u003DzQ8SgRgQ\u003D()
  {
    lock (this.SyncRoot)
    {
      BoxPlotDataSeries<TX, TY> kjonMXnnqFlZ3Cvw = new BoxPlotDataSeries<TX, TY>();
      kjonMXnnqFlZ3Cvw.FifoCapacity = this.FifoCapacity;
      kjonMXnnqFlZ3Cvw.AcceptsUnsortedData = this.AcceptsUnsortedData;
      kjonMXnnqFlZ3Cvw.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) this.XValues, (IEnumerable<TY>) this.YValues, (IEnumerable<TY>) this.\u0023\u003DzwrNTkEFZoB0H, (IEnumerable<TY>) this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D, (IEnumerable<TY>) this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D, (IEnumerable<TY>) this.\u0023\u003DztR5HGEq8pm\u00248);
      return (IDataSeries<TX, TY>) kjonMXnnqFlZ3Cvw;
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
    {
      ResamplingMode tlZqozNbfOhxiykg1 = _param1 == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Mid;
      ResamplingMode tlZqozNbfOhxiykg2 = _param1 == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Max;
      ResamplingMode tlZqozNbfOhxiykg3 = _param1 == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Min;
      \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOby\u0024vWkeqZwE94P6zz4sY0BD_b\u0024iDA\u003D\u003D e94P6zz4sY0BdBIDa = _param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<TX, TY>();
      IPointSeries ftrixUnpTllY1PkTyq1 = e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzoEP49rI\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6);
      IPointSeries ftrixUnpTllY1PkTyq2 = e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6);
      IPointSeries ftrixUnpTllY1PkTyq3 = e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6);
      IPointSeries ftrixUnpTllY1PkTyq4 = e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg2, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DztR5HGEq8pm\u00248, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6);
      IPointSeries ftrixUnpTllY1PkTyq5 = e94P6zz4sY0BdBIDa.\u0023\u003Dzg_KsNhI\u003D(tlZqozNbfOhxiykg3, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzwrNTkEFZoB0H, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6);
      return (IPointSeries) new \u0023\u003DzKsGTwu6B0A6eMUO4QALnGMy6VZRl6ViWfvaAKHBRvgtX(ftrixUnpTllY1PkTyq1, ftrixUnpTllY1PkTyq5, ftrixUnpTllY1PkTyq3, ftrixUnpTllY1PkTyq2, ftrixUnpTllY1PkTyq4);
    }
  }

  public override HitTestInfo \u0023\u003DzDKPxuEruV71w(
    int _param1)
  {
    lock (this.SyncRoot)
    {
      HitTestInfo zldchDrVsrVyHh6WyiGy = base.\u0023\u003DzDKPxuEruV71w(_param1);
      if (!zldchDrVsrVyHh6WyiGy.\u0023\u003DzMeGSfVE\u003D())
      {
        zldchDrVsrVyHh6WyiGy.Minimum = (IComparable) this.\u0023\u003DzwrNTkEFZoB0H[_param1];
        zldchDrVsrVyHh6WyiGy.Maximum = (IComparable) this.\u0023\u003DztR5HGEq8pm\u00248[_param1];
        zldchDrVsrVyHh6WyiGy.\u0023\u003Dzx\u0024\u00244gcz\u0024aseuFGr8MQ\u003D\u003D((IComparable) this.\u0023\u003DzoEP49rI\u003D[_param1]);
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzmTk424cEMQG0Xxaf2vmG3GQ\u003D((IComparable) this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D[_param1]);
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzoqxH\u0024NzPSGcA35HNEpCrqVs\u003D((IComparable) this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D[_param1]);
      }
      return zldchDrVsrVyHh6WyiGy;
    }
  }

  public override void Append(
    TX _param1,
    params TY[] _param2)
  {
    if (_param2.Length != 5)
      this.\u0023\u003Dz2OnEmwtzurH2(5);
    this.\u0023\u003Dznc8esWY\u003D(_param1, _param2[0], _param2[1], _param2[2], _param2[3], _param2[4]);
  }

  public override void Append(
    IEnumerable<TX> _param1,
    params IEnumerable<TY>[] _param2)
  {
    if (_param2.Length != 5)
      this.\u0023\u003Dz2OnEmwtzurH2(5);
    this.\u0023\u003Dznc8esWY\u003D(_param1, _param2[0], _param2[1], _param2[2], _param2[3], _param2[4]);
  }

  private void \u0023\u003Dznc8esWY\u003D(
    TX _param1,
    TY _param2,
    TY _param3,
    TY _param4,
    TY _param5,
    TY _param6)
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003DzmIwKipw\u003D.Add(_param1);
      this.\u0023\u003DzoEP49rI\u003D.Add(_param2);
      this.\u0023\u003DzwrNTkEFZoB0H.Add(_param3);
      this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D.Add(_param4);
      this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D.Add(_param5);
      this.\u0023\u003DztR5HGEq8pm\u00248.Add(_param6);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzFIf7JZ5S\u0024Wr_(this.\u0023\u003DzmIwKipw\u003D, _param1, this.AcceptsUnsortedData);
    }
  }

  public void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<TX> _param1,
    IEnumerable<TY> _param2,
    IEnumerable<TY> _param3,
    IEnumerable<TY> _param4,
    IEnumerable<TY> _param5,
    IEnumerable<TY> _param6)
  {
    if (_param1.\u0023\u003DzMeGSfVE\u003D<TX>())
      return;
    lock (this.SyncRoot)
    {
      int count = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param1);
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param2);
      this.\u0023\u003DzwrNTkEFZoB0H.\u0023\u003Dz6_E5\u0024pE\u003D(_param3);
      this.\u0023\u003Dz620vIy7RNcC1\u0024PtA3Bz5y0g\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param4);
      this.\u0023\u003DzX3iilDqpwsGtt5lwpfMAKTk\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param5);
      this.\u0023\u003DztR5HGEq8pm\u00248.\u0023\u003Dz6_E5\u0024pE\u003D(_param6);
      this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
      this.DataDistributionCalculator.\u0023\u003DzeU6gWqHRfREz(this.\u0023\u003DzmIwKipw\u003D, count, _param1, this.AcceptsUnsortedData);
    }
  }

  public override TY GetYMaxAt(
    int _param1,
    TY _param2)
  {
    TY zE8zkRfY = this.\u0023\u003DztR5HGEq8pm\u00248[_param1];
    return DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(zE8zkRfY) ? _param2 : DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Max(_param2, zE8zkRfY);
  }

  public override TY GetYMinAt(
    int _param1,
    TY _param2)
  {
    TY zE8zkRfY = this.\u0023\u003DzwrNTkEFZoB0H[_param1];
    return DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.IsNaN(zE8zkRfY) ? _param2 : DataSeries<TX, TY>.\u0023\u003DzkNQuj6U\u003D.Min(_param2, zE8zkRfY);
  }
}
