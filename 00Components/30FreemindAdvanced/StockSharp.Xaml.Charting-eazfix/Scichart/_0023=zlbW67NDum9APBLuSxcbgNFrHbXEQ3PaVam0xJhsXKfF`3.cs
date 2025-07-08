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
internal sealed class \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D> : 
  \u0023\u003DzawTMm83sNsuVHdgLsihy4d2BjT37wAANIYiDnU4YaDg5<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>,
  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>,
  \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhip318DMhXaWOsdXxIKq2Zfn_,
  \u0023\u003DzKsGTwu6B0A6eMUO4QALnGEFyuKuisWdfO6O5SJH\u00244l_vSJGnVw\u003D\u003D<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>,
  ISuspendable,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D
  where \u0023\u003DzulcL8RA\u003D : IComparable
  where \u0023\u003DzE8zkRfY\u003D : IComparable
  where \u0023\u003DzPqz5cUs\u003D : IComparable
{
  
  public static readonly IMath<\u0023\u003DzPqz5cUs\u003D> \u0023\u003DzVgDDgro\u003D = MathHelper.GetMath<\u0023\u003DzPqz5cUs\u003D>();
  
  private readonly \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D)> \u0023\u003Dz3laIKDGah\u0024Kh;
  
  protected \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzPqz5cUs\u003D> \u0023\u003Dz9QqJzD0\u003D = (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzPqz5cUs\u003D>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<\u0023\u003DzPqz5cUs\u003D>();

  public \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7()
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh = new \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D)>(new Action<IList<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D)>>(this.\u0023\u003DzGXoUV7Q\u0024_bM\u0024));
  }

  public override \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D DataSeriesType
  {
    get => (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 3;
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

  public override \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D> \u0023\u003DzQ8SgRgQ\u003D()
  {
    lock (this.SyncRoot)
    {
      this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
      \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D> q3PaVam0xJhsXkfF7 = new \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>();
      q3PaVam0xJhsXkfF7.FifoCapacity = this.FifoCapacity;
      q3PaVam0xJhsXkfF7.AcceptsUnsortedData = this.AcceptsUnsortedData;
      q3PaVam0xJhsXkfF7.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) this.XValues, (IEnumerable<\u0023\u003DzE8zkRfY\u003D>) this.YValues, (IEnumerable<\u0023\u003DzPqz5cUs\u003D>) this.ZValues);
      return (\u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>) q3PaVam0xJhsXkfF7;
    }
  }

  public override \u0023\u003DzE8zkRfY\u003D GetYMinAt(
    int _param1,
    \u0023\u003DzE8zkRfY\u003D _param2)
  {
    \u0023\u003DzE8zkRfY\u003D yvalue = this.YValues[_param1];
    return !\u0023\u003DzawTMm83sNsuVHdgLsihy4d2BjT37wAANIYiDnU4YaDg5<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>.\u0023\u003DzkNQuj6U\u003D.IsNaN(yvalue) ? \u0023\u003DzawTMm83sNsuVHdgLsihy4d2BjT37wAANIYiDnU4YaDg5<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>.\u0023\u003DzkNQuj6U\u003D.Min(_param2, yvalue) : _param2;
  }

  public override \u0023\u003DzE8zkRfY\u003D GetYMaxAt(
    int _param1,
    \u0023\u003DzE8zkRfY\u003D _param2)
  {
    \u0023\u003DzE8zkRfY\u003D yvalue = this.YValues[_param1];
    return !\u0023\u003DzawTMm83sNsuVHdgLsihy4d2BjT37wAANIYiDnU4YaDg5<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>.\u0023\u003DzkNQuj6U\u003D.IsNaN(yvalue) ? \u0023\u003DzawTMm83sNsuVHdgLsihy4d2BjT37wAANIYiDnU4YaDg5<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>.\u0023\u003DzkNQuj6U\u003D.Max(_param2, yvalue) : _param2;
  }

  public override \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ToPointSeries(
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D _param1,
    IndexRange  _param2,
    int _param3,
    bool _param4,
    bool? _param5,
    IRange _param6,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D _param7,
    object _param8 = null)
  {
    lock (this.SyncRoot)
      return (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnK_qtBWB9G5KlgWK_lJR7a0x(_param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003DzoEP49rI\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6), _param7.\u0023\u003DzA9cE1Xcn5A4Bx3OLTNdvGuw\u003D<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzPqz5cUs\u003D>().\u0023\u003Dzg_KsNhI\u003D(_param1, _param2, _param3, this.IsFifo, _param4, (IList) this.\u0023\u003DzmIwKipw\u003D, (IList) this.\u0023\u003Dz9QqJzD0\u003D, new bool?(this.DataDistributionCalculator.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()), new bool?(this.DataDistributionCalculator.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()), _param5, _param6));
  }

  public override void OnBeginRenderPass()
  {
    base.OnBeginRenderPass();
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003DzY9qzIPY\u003D();
  }

  public override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzDKPxuEruV71w(
    int _param1)
  {
    lock (this.SyncRoot)
    {
      \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = base.\u0023\u003DzDKPxuEruV71w(_param1);
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
        this.\u0023\u003DzmIwKipw\u003D = (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzulcL8RA\u003D>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<\u0023\u003DzulcL8RA\u003D>(num);
        this.\u0023\u003DzoEP49rI\u003D = (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzE8zkRfY\u003D>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<\u0023\u003DzE8zkRfY\u003D>(num);
        this.\u0023\u003Dz9QqJzD0\u003D = (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzPqz5cUs\u003D>) new \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<\u0023\u003DzPqz5cUs\u003D>(num);
      }
      else
      {
        this.\u0023\u003DzmIwKipw\u003D = (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzulcL8RA\u003D>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<\u0023\u003DzulcL8RA\u003D>();
        this.\u0023\u003DzoEP49rI\u003D = (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzE8zkRfY\u003D>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<\u0023\u003DzE8zkRfY\u003D>();
        this.\u0023\u003Dz9QqJzD0\u003D = (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzPqz5cUs\u003D>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<\u0023\u003DzPqz5cUs\u003D>();
      }
      ((ICollection<\u0023\u003DzulcL8RA\u003D>) this.\u0023\u003DzmIwKipw\u003D).Clear();
      ((ICollection<\u0023\u003DzE8zkRfY\u003D>) this.\u0023\u003DzoEP49rI\u003D).Clear();
      ((ICollection<\u0023\u003DzPqz5cUs\u003D>) this.\u0023\u003Dz9QqJzD0\u003D).Clear();
      this.\u0023\u003Dz3laIKDGah\u0024Kh?.\u0023\u003DzUf222sU\u003D();
    }
  }

  public override void Append(
    \u0023\u003DzulcL8RA\u003D _param1,
    params \u0023\u003DzE8zkRfY\u003D[] _param2)
  {
    throw new InvalidOperationException($"Append(TX x, params TY[] yValues) in type {this.GetType().Name} must receive X, Y and Z values. Please use the Append(x,y,z) overload");
  }

  public override void Append(
    IEnumerable<\u0023\u003DzulcL8RA\u003D> _param1,
    params IEnumerable<\u0023\u003DzE8zkRfY\u003D>[] _param2)
  {
    throw new InvalidOperationException($"Append(TX x, params TY[] yValues) in type {this.GetType().Name} must receive X, Y and Z values. Please use the Append(x,y,z) overload");
  }

  public void \u0023\u003Dznc8esWY\u003D(
    \u0023\u003DzulcL8RA\u003D _param1,
    \u0023\u003DzE8zkRfY\u003D _param2,
    \u0023\u003DzPqz5cUs\u003D _param3)
  {
    this.\u0023\u003Dz3laIKDGah\u0024Kh.\u0023\u003Dznc8esWY\u003D((_param1, _param2, _param3));
    this.\u0023\u003DztwHsLGWnHCVU((\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSt8unP4d2NFH7w5lsUs\u003D) 1);
  }

  public void \u0023\u003Dznc8esWY\u003D(
    IEnumerable<\u0023\u003DzulcL8RA\u003D> _param1,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param2,
    IEnumerable<\u0023\u003DzPqz5cUs\u003D> _param3)
  {
    if (_param1.\u0023\u003DzMeGSfVE\u003D<\u0023\u003DzulcL8RA\u003D>())
      return;
    IEnumerator<\u0023\u003DzulcL8RA\u003D> enumerator1 = _param1.GetEnumerator();
    IEnumerator<\u0023\u003DzE8zkRfY\u003D> enumerator2 = _param2.GetEnumerator();
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

  public void \u0023\u003DzFkV86a8\u003D(
    \u0023\u003DzulcL8RA\u003D _param1,
    \u0023\u003DzE8zkRfY\u003D _param2,
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
    \u0023\u003DzulcL8RA\u003D _param2,
    \u0023\u003DzE8zkRfY\u003D _param3,
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
    IEnumerable<\u0023\u003DzulcL8RA\u003D> _param2,
    IEnumerable<\u0023\u003DzE8zkRfY\u003D> _param3,
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
    IList<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D)> _param1)
  {
    lock (this.SyncRoot)
    {
      IEnumerable<\u0023\u003DzulcL8RA\u003D> zulcL8Ras = _param1.Select<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzulcL8RA\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D ?? (\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D = new Func<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzulcL8RA\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003DzpMHI4jfrG6pvyYLWoaHs3No\u003D)));
      int count = ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) this.\u0023\u003DzmIwKipw\u003D).get_Count();
      this.\u0023\u003DzmIwKipw\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(zulcL8Ras);
      this.\u0023\u003DzoEP49rI\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzE8zkRfY\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D ?? (\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D = new Func<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzE8zkRfY\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003Dzw6g7ZuECV2i7kaLCerU_JP8\u003D))));
      this.\u0023\u003Dz9QqJzD0\u003D.\u0023\u003Dz6_E5\u0024pE\u003D(_param1.Select<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzPqz5cUs\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzMfTyzDitoO\u0024eks3zDA\u003D\u003D ?? (\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.\u0023\u003DzMfTyzDitoO\u0024eks3zDA\u003D\u003D = new Func<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzPqz5cUs\u003D>(\u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003DzXqGoTxjIqWhMYTQ3LamDD1s\u003D))));
      this.DataDistributionCalculator.\u0023\u003DzeU6gWqHRfREz(this.\u0023\u003DzmIwKipw\u003D, count, zulcL8Ras, this.AcceptsUnsortedData);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D>.SomeClass34343383();
    public static Func<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzulcL8RA\u003D> \u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D;
    public static Func<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzE8zkRfY\u003D> \u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D;
    public static Func<(\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D), \u0023\u003DzPqz5cUs\u003D> \u0023\u003DzMfTyzDitoO\u0024eks3zDA\u003D\u003D;

    internal \u0023\u003DzulcL8RA\u003D \u0023\u003DzpMHI4jfrG6pvyYLWoaHs3No\u003D(
      (\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D) _param1)
    {
      return _param1.Item1;
    }

    internal \u0023\u003DzE8zkRfY\u003D \u0023\u003Dzw6g7ZuECV2i7kaLCerU_JP8\u003D(
      (\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D) _param1)
    {
      return _param1.Item2;
    }

    internal \u0023\u003DzPqz5cUs\u003D \u0023\u003DzXqGoTxjIqWhMYTQ3LamDD1s\u003D(
      (\u0023\u003DzulcL8RA\u003D, \u0023\u003DzE8zkRfY\u003D, \u0023\u003DzPqz5cUs\u003D) _param1)
    {
      return _param1.Item3;
    }
  }
}
