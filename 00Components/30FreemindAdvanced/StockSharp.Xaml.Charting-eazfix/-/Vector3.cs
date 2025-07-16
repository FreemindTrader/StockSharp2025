// Decompiled with JetBrains decompiler
// Type: Vector3
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
internal struct Vector3 : 
  IEquatable<Vector3>
{
  
  public double dje_z3GBAX47U_ejd;
  
  public double dje_zLPL6EZPA_ejd;
  
  public double dje_zX8HQ8VV9_ejd;
  
  public static readonly Vector3 dje_zUE7VTUP4_ejd = new Vector3(1.0, 0.0, 0.0);
  
  public static readonly Vector3 dje_zPEJ77TB7_ejd = new Vector3(0.0, 1.0, 0.0);
  
  public static readonly Vector3 dje_z9CMHUJAZ_ejd = new Vector3(0.0, 0.0, 1.0);
  
  public static readonly Vector3 dje_z2EADAZU3_ejd = new Vector3(0.0, 0.0, 0.0);
  
  public static readonly Vector3 dje_zWJBJPBE9_ejd = new Vector3(1.0, 1.0, 1.0);
  
  public static readonly Vector3 dje_zUB5C5FFUC62Z9TQ_ejd = new Vector3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
  
  public static readonly Vector3 dje_zERVCCKMAGMWDHN2_ejd = new Vector3(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
  
  public static readonly int dje_zHNY3UCKLVTU9U6Q_ejd = Marshal.SizeOf<Vector3>(new Vector3());

  public Vector3(
    double _param1,
    double _param2,
    double _param3)
  {
    this.dje_z3GBAX47U_ejd = _param1;
    this.dje_zLPL6EZPA_ejd = _param2;
    this.dje_zX8HQ8VV9_ejd = _param3;
  }

  public Vector3(
    Vector2 _param1,
    double _param2 = 0.0)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = _param2;
  }

  public Vector3(
    Vector3 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = _param1.dje_zX8HQ8VV9_ejd;
  }

  public Vector3(
    Vector4 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = _param1.dje_zX8HQ8VV9_ejd;
  }

  
  [IndexerName("#=zMRIb09I=")]
  public double this[int _param1]
  {
    get
    {
      switch (_param1)
      {
        case 0:
          return this.dje_z3GBAX47U_ejd;
        case 1:
          return this.dje_zLPL6EZPA_ejd;
        case 2:
          return this.dje_zX8HQ8VV9_ejd;
        default:
          return 0.0;
      }
    }
    set
    {
      switch (_param1)
      {
        case 0:
          this.dje_z3GBAX47U_ejd = value;
          break;
        case 1:
          this.dje_zLPL6EZPA_ejd = value;
          break;
        case 2:
          this.dje_zX8HQ8VV9_ejd = value;
          break;
        default:
          throw new Exception();
      }
    }
  }

  public double \u0023\u003Dz\u0024CeUvME\u003D(int _param1)
  {
    switch (_param1)
    {
      case 0:
        return this.dje_z3GBAX47U_ejd;
      case 1:
        return this.dje_zLPL6EZPA_ejd;
      case 2:
        return this.dje_zX8HQ8VV9_ejd;
      default:
        return 0.0;
    }
  }

  public void \u0023\u003DzS9gpfR4\u003D(int _param1, double _param2)
  {
    switch (_param1)
    {
      case 0:
        this.dje_z3GBAX47U_ejd = _param2;
        break;
      case 1:
        this.dje_zLPL6EZPA_ejd = _param2;
        break;
      case 2:
        this.dje_zX8HQ8VV9_ejd = _param2;
        break;
      default:
        throw new Exception();
    }
  }

  public double \u0023\u003DzxhbmvAVxpXvh()
  {
    return Math.Sqrt(this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd);
  }

  public double \u0023\u003DzdKlVd6PL86\u0024r()
  {
    return 1.0 / \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd);
  }

  public double \u0023\u003DzXNz5ccu6mHa6()
  {
    return this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd;
  }

  public Vector3 \u0023\u003DzWQoDUw0\u003D()
  {
    Vector3 xxpaxL8AcxpW32QaEjd = this;
    xxpaxL8AcxpW32QaEjd.\u0023\u003DzC520uIs\u003D();
    return xxpaxL8AcxpW32QaEjd;
  }

  public void \u0023\u003DzC520uIs\u003D()
  {
    double num = 1.0 / this.\u0023\u003DzxhbmvAVxpXvh();
    this.dje_z3GBAX47U_ejd *= num;
    this.dje_zLPL6EZPA_ejd *= num;
    this.dje_zX8HQ8VV9_ejd *= num;
  }

  public void \u0023\u003Dz_fJUMYtCeW_1()
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd);
    this.dje_z3GBAX47U_ejd *= num;
    this.dje_zLPL6EZPA_ejd *= num;
    this.dje_zX8HQ8VV9_ejd *= num;
  }

  public double[] \u0023\u003DzSWlLd4k\u003D()
  {
    return new double[3]
    {
      this.dje_z3GBAX47U_ejd,
      this.dje_zLPL6EZPA_ejd,
      this.dje_zX8HQ8VV9_ejd
    };
  }

  public static Vector3 Add(
    Vector3 _param0,
    Vector3 _param1)
  {
    Vector3.Add(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void Add(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out Vector3 _param2)
  {
    _param2 = new Vector3(_param0.dje_z3GBAX47U_ejd + _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd + _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd + _param1.dje_zX8HQ8VV9_ejd);
  }

  public static Vector3 Subtract(
    Vector3 _param0,
    Vector3 _param1)
  {
    Vector3.Subtract(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void Subtract(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out Vector3 _param2)
  {
    _param2 = new Vector3(_param0.dje_z3GBAX47U_ejd - _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd - _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd - _param1.dje_zX8HQ8VV9_ejd);
  }

  public static Vector3 \u0023\u003DzQw38IaY\u003D(
    Vector3 _param0,
    double _param1)
  {
    Vector3.\u0023\u003DzQw38IaY\u003D(ref _param0, _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003DzQw38IaY\u003D(
    ref Vector3 _param0,
    double _param1,
    out Vector3 _param2)
  {
    _param2 = new Vector3(_param0.dje_z3GBAX47U_ejd * _param1, _param0.dje_zLPL6EZPA_ejd * _param1, _param0.dje_zX8HQ8VV9_ejd * _param1);
  }

  public static Vector3 \u0023\u003DzQw38IaY\u003D(
    Vector3 _param0,
    Vector3 _param1)
  {
    Vector3.\u0023\u003DzQw38IaY\u003D(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003DzQw38IaY\u003D(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out Vector3 _param2)
  {
    _param2 = new Vector3(_param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd * _param1.dje_zX8HQ8VV9_ejd);
  }

  public static Vector3 \u0023\u003Dzmd\u0024taME\u003D(
    Vector3 _param0,
    double _param1)
  {
    Vector3.\u0023\u003Dzmd\u0024taME\u003D(ref _param0, _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003Dzmd\u0024taME\u003D(
    ref Vector3 _param0,
    double _param1,
    out Vector3 _param2)
  {
    Vector3.\u0023\u003DzQw38IaY\u003D(ref _param0, 1.0 / _param1, out _param2);
  }

  public static Vector3 \u0023\u003Dzmd\u0024taME\u003D(
    Vector3 _param0,
    Vector3 _param1)
  {
    Vector3.\u0023\u003Dzmd\u0024taME\u003D(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003Dzmd\u0024taME\u003D(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out Vector3 _param2)
  {
    _param2 = new Vector3(_param0.dje_z3GBAX47U_ejd / _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd / _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd / _param1.dje_zX8HQ8VV9_ejd);
  }

  public static Vector3 \u0023\u003DzXTXKynGX9VVf(
    Vector3 _param0,
    Vector3 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
    return _param0;
  }

  public static void \u0023\u003DzXTXKynGX9VVf(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out Vector3 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param2.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
  }

  public static Vector3 \u0023\u003DzqQHPvHWdZgmR(
    Vector3 _param0,
    Vector3 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd > _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
    return _param0;
  }

  public static void \u0023\u003DzqQHPvHWdZgmR(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out Vector3 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param2.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd > _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
  }

  public static Vector3 Min(
    Vector3 _param0,
    Vector3 _param1)
  {
    return _param0.\u0023\u003DzXNz5ccu6mHa6() >= _param1.\u0023\u003DzXNz5ccu6mHa6() ? _param1 : _param0;
  }

  public static Vector3 Max(
    Vector3 _param0,
    Vector3 _param1)
  {
    return _param0.\u0023\u003DzXNz5ccu6mHa6() < _param1.\u0023\u003DzXNz5ccu6mHa6() ? _param1 : _param0;
  }

  public static Vector3 \u0023\u003Dz7h4Pu5Gr1WYQ(
    Vector3 _param0,
    Vector3 _param1,
    Vector3 _param2)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param1.dje_z3GBAX47U_ejd : (_param0.dje_z3GBAX47U_ejd > _param2.dje_z3GBAX47U_ejd ? _param2.dje_z3GBAX47U_ejd : _param0.dje_z3GBAX47U_ejd);
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param1.dje_zLPL6EZPA_ejd : (_param0.dje_zLPL6EZPA_ejd > _param2.dje_zLPL6EZPA_ejd ? _param2.dje_zLPL6EZPA_ejd : _param0.dje_zLPL6EZPA_ejd);
    _param0.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param1.dje_zX8HQ8VV9_ejd : (_param0.dje_zX8HQ8VV9_ejd > _param2.dje_zX8HQ8VV9_ejd ? _param2.dje_zX8HQ8VV9_ejd : _param0.dje_zX8HQ8VV9_ejd);
    return _param0;
  }

  public static void \u0023\u003Dz7h4Pu5Gr1WYQ(
    ref Vector3 _param0,
    ref Vector3 _param1,
    ref Vector3 _param2,
    out Vector3 _param3)
  {
    _param3.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param1.dje_z3GBAX47U_ejd : (_param0.dje_z3GBAX47U_ejd > _param2.dje_z3GBAX47U_ejd ? _param2.dje_z3GBAX47U_ejd : _param0.dje_z3GBAX47U_ejd);
    _param3.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param1.dje_zLPL6EZPA_ejd : (_param0.dje_zLPL6EZPA_ejd > _param2.dje_zLPL6EZPA_ejd ? _param2.dje_zLPL6EZPA_ejd : _param0.dje_zLPL6EZPA_ejd);
    _param3.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param1.dje_zX8HQ8VV9_ejd : (_param0.dje_zX8HQ8VV9_ejd > _param2.dje_zX8HQ8VV9_ejd ? _param2.dje_zX8HQ8VV9_ejd : _param0.dje_zX8HQ8VV9_ejd);
  }

  public static Vector3 \u0023\u003DzC520uIs\u003D(
    Vector3 _param0)
  {
    double num = 1.0 / _param0.\u0023\u003DzxhbmvAVxpXvh();
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    _param0.dje_zX8HQ8VV9_ejd *= num;
    return _param0;
  }

  public static void \u0023\u003DzC520uIs\u003D(
    ref Vector3 _param0,
    out Vector3 _param1)
  {
    double num = 1.0 / _param0.\u0023\u003DzxhbmvAVxpXvh();
    _param1.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * num;
    _param1.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd * num;
    _param1.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd * num;
  }

  public static Vector3 \u0023\u003Dz_fJUMYtCeW_1(
    Vector3 _param0)
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(_param0.dje_z3GBAX47U_ejd * _param0.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param0.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param0.dje_zX8HQ8VV9_ejd);
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    _param0.dje_zX8HQ8VV9_ejd *= num;
    return _param0;
  }

  public static void \u0023\u003Dz_fJUMYtCeW_1(
    ref Vector3 _param0,
    out Vector3 _param1)
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(_param0.dje_z3GBAX47U_ejd * _param0.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param0.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param0.dje_zX8HQ8VV9_ejd);
    _param1.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * num;
    _param1.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd * num;
    _param1.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd * num;
  }

  public static double \u0023\u003DzJkPZoNo\u003D(
    Vector3 _param0,
    Vector3 _param1)
  {
    return _param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.dje_zX8HQ8VV9_ejd;
  }

  public static void \u0023\u003DzJkPZoNo\u003D(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out double _param2)
  {
    _param2 = _param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.dje_zX8HQ8VV9_ejd;
  }

  public static Vector3 \u0023\u003Dz1jcyxnU\u003D(
    Vector3 _param0,
    Vector3 _param1)
  {
    Vector3 xxpaxL8AcxpW32QaEjd;
    Vector3.\u0023\u003Dz1jcyxnU\u003D(ref _param0, ref _param1, out xxpaxL8AcxpW32QaEjd);
    return xxpaxL8AcxpW32QaEjd;
  }

  public static void \u0023\u003Dz1jcyxnU\u003D(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out Vector3 _param2)
  {
    _param2 = new Vector3(_param0.dje_zLPL6EZPA_ejd * _param1.dje_zX8HQ8VV9_ejd - _param0.dje_zX8HQ8VV9_ejd * _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd * _param1.dje_z3GBAX47U_ejd - _param0.dje_z3GBAX47U_ejd * _param1.dje_zX8HQ8VV9_ejd, _param0.dje_z3GBAX47U_ejd * _param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd * _param1.dje_z3GBAX47U_ejd);
  }

  public static Vector3 \u0023\u003DzLBajzIS0VDuy(
    Vector3 _param0,
    Vector3 _param1,
    double _param2)
  {
    _param0.dje_z3GBAX47U_ejd = _param2 * (_param1.dje_z3GBAX47U_ejd - _param0.dje_z3GBAX47U_ejd) + _param0.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param2 * (_param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd) + _param0.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = _param2 * (_param1.dje_zX8HQ8VV9_ejd - _param0.dje_zX8HQ8VV9_ejd) + _param0.dje_zX8HQ8VV9_ejd;
    return _param0;
  }

  public static void \u0023\u003DzLBajzIS0VDuy(
    ref Vector3 _param0,
    ref Vector3 _param1,
    double _param2,
    out Vector3 _param3)
  {
    _param3.dje_z3GBAX47U_ejd = _param2 * (_param1.dje_z3GBAX47U_ejd - _param0.dje_z3GBAX47U_ejd) + _param0.dje_z3GBAX47U_ejd;
    _param3.dje_zLPL6EZPA_ejd = _param2 * (_param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd) + _param0.dje_zLPL6EZPA_ejd;
    _param3.dje_zX8HQ8VV9_ejd = _param2 * (_param1.dje_zX8HQ8VV9_ejd - _param0.dje_zX8HQ8VV9_ejd) + _param0.dje_zX8HQ8VV9_ejd;
  }

  public static Vector3 \u0023\u003Dzie4S5q5_H9GgLyLdMw\u003D\u003D(
    Vector3 _param0,
    Vector3 _param1,
    Vector3 _param2,
    double _param3,
    double _param4)
  {
    return _param0 + _param3 * (_param1 - _param0) + _param4 * (_param2 - _param0);
  }

  public static void \u0023\u003Dzie4S5q5_H9GgLyLdMw\u003D\u003D(
    ref Vector3 _param0,
    ref Vector3 _param1,
    ref Vector3 _param2,
    double _param3,
    double _param4,
    out Vector3 _param5)
  {
    _param5 = _param0;
    Vector3 xxpaxL8AcxpW32QaEjd1 = _param1;
    Vector3.Subtract(ref xxpaxL8AcxpW32QaEjd1, ref _param0, out xxpaxL8AcxpW32QaEjd1);
    Vector3.\u0023\u003DzQw38IaY\u003D(ref xxpaxL8AcxpW32QaEjd1, _param3, out xxpaxL8AcxpW32QaEjd1);
    Vector3.Add(ref _param5, ref xxpaxL8AcxpW32QaEjd1, out _param5);
    Vector3 xxpaxL8AcxpW32QaEjd2 = _param2;
    Vector3.Subtract(ref xxpaxL8AcxpW32QaEjd2, ref _param0, out xxpaxL8AcxpW32QaEjd2);
    Vector3.\u0023\u003DzQw38IaY\u003D(ref xxpaxL8AcxpW32QaEjd2, _param4, out xxpaxL8AcxpW32QaEjd2);
    Vector3.Add(ref _param5, ref xxpaxL8AcxpW32QaEjd2, out _param5);
  }

  public static Vector3 \u0023\u003Dz3l8SoTV1ZzAh(
    Vector3 _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    return new Vector3(Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003DzlJZ\u00243O_bc01n())), Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003DzGGTfPIgvfHsh())), Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003Dzvmu7\u0024ib36lNH())));
  }

  public static void \u0023\u003Dz3l8SoTV1ZzAh(
    ref Vector3 _param0,
    ref \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1,
    out Vector3 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zLPL6EZPA_ejd;
    _param2.dje_zX8HQ8VV9_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zX8HQ8VV9_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zX8HQ8VV9_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zX8HQ8VV9_ejd;
  }

  public static Vector3 \u0023\u003DzGlqPpHYULjgQ(
    Vector3 _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    _param1.\u0023\u003DzkRsyJTI\u003D();
    return Vector3.\u0023\u003Dz9ASYb3e8Mrzh(_param0, _param1);
  }

  public static void \u0023\u003DzGlqPpHYULjgQ(
    ref Vector3 _param0,
    ref \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1,
    out Vector3 _param2)
  {
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D wbczijKaygP156vtDeaE = \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D.\u0023\u003DzkRsyJTI\u003D(_param1);
    Vector3.\u0023\u003Dz9ASYb3e8Mrzh(ref _param0, ref wbczijKaygP156vtDeaE, out _param2);
  }

  public static Vector3 \u0023\u003Dz9ASYb3e8Mrzh(
    Vector3 _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    return new Vector3(Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003Dzctm1rVw\u003D)), Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003Dz90ZjCUw\u003D)), Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003DzCnT1wOM\u003D)));
  }

  public static void \u0023\u003Dz9ASYb3e8Mrzh(
    ref Vector3 _param0,
    ref \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1,
    out Vector3 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zX8HQ8VV9_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zX8HQ8VV9_ejd;
    _param2.dje_zX8HQ8VV9_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zX8HQ8VV9_ejd;
  }

  public static Vector3 \u0023\u003DztJjTi6ViyMhY(
    Vector3 _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    return new Vector3(Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003DzlJZ\u00243O_bc01n())) + _param1.\u0023\u003DzE3ZR8NI\u003D.dje_z3GBAX47U_ejd, Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003DzGGTfPIgvfHsh())) + _param1.\u0023\u003DzE3ZR8NI\u003D.dje_zLPL6EZPA_ejd, Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, new Vector3(_param1.\u0023\u003Dzvmu7\u0024ib36lNH())) + _param1.\u0023\u003DzE3ZR8NI\u003D.dje_zX8HQ8VV9_ejd);
  }

  public static void \u0023\u003DztJjTi6ViyMhY(
    ref Vector3 _param0,
    ref \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1,
    out Vector3 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_z3GBAX47U_ejd + _param1.\u0023\u003DzE3ZR8NI\u003D.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zLPL6EZPA_ejd + _param1.\u0023\u003DzE3ZR8NI\u003D.dje_zLPL6EZPA_ejd;
    _param2.dje_zX8HQ8VV9_ejd = _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zX8HQ8VV9_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zX8HQ8VV9_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zX8HQ8VV9_ejd + _param1.\u0023\u003DzE3ZR8NI\u003D.dje_zX8HQ8VV9_ejd;
  }

  public static void \u0023\u003Dz8miGAzg\u003D(
    Vector3[] _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    for (int index = 0; index < _param0.Length; ++index)
      _param0[index] = Vector3.\u0023\u003Dz8miGAzg\u003D(_param0[index], _param1);
  }

  public static Vector3 \u0023\u003Dz8miGAzg\u003D(
    Vector3 _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    Vector3 xxpaxL8AcxpW32QaEjd;
    Vector3.\u0023\u003Dz8miGAzg\u003D(ref _param0, ref _param1, out xxpaxL8AcxpW32QaEjd);
    return xxpaxL8AcxpW32QaEjd;
  }

  public static void \u0023\u003Dz8miGAzg\u003D(
    ref Vector3 _param0,
    ref \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1,
    out Vector3 _param2)
  {
    Vector4 bbfK6VhuQ6F9ZEjd = new Vector4(_param0.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd, 1.0);
    Vector4.\u0023\u003Dz8miGAzg\u003D(ref bbfK6VhuQ6F9ZEjd, ref _param1, out bbfK6VhuQ6F9ZEjd);
    _param2 = bbfK6VhuQ6F9ZEjd.\u0023\u003Dz9wx8aZCn12QF();
  }

  public static Vector3 \u0023\u003Dz8miGAzg\u003D(
    Vector3 _param0,
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La _param1)
  {
    Vector3 xxpaxL8AcxpW32QaEjd;
    Vector3.\u0023\u003Dz8miGAzg\u003D(ref _param0, ref _param1, out xxpaxL8AcxpW32QaEjd);
    return xxpaxL8AcxpW32QaEjd;
  }

  public static void \u0023\u003Dz8miGAzg\u003D(
    ref Vector3 _param0,
    ref \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La _param1,
    out Vector3 _param2)
  {
    Vector3 xxpaxL8AcxpW32QaEjd1 = _param1.\u0023\u003Dz9wx8aZCn12QF();
    Vector3 xxpaxL8AcxpW32QaEjd2;
    Vector3.\u0023\u003Dz1jcyxnU\u003D(ref xxpaxL8AcxpW32QaEjd1, ref _param0, out xxpaxL8AcxpW32QaEjd2);
    Vector3 xxpaxL8AcxpW32QaEjd3;
    Vector3.\u0023\u003DzQw38IaY\u003D(ref _param0, _param1.\u0023\u003DzMFog0bw\u003D(), out xxpaxL8AcxpW32QaEjd3);
    Vector3.Add(ref xxpaxL8AcxpW32QaEjd2, ref xxpaxL8AcxpW32QaEjd3, out xxpaxL8AcxpW32QaEjd2);
    Vector3.\u0023\u003Dz1jcyxnU\u003D(ref xxpaxL8AcxpW32QaEjd1, ref xxpaxL8AcxpW32QaEjd2, out xxpaxL8AcxpW32QaEjd2);
    Vector3.\u0023\u003DzQw38IaY\u003D(ref xxpaxL8AcxpW32QaEjd2, 2.0, out xxpaxL8AcxpW32QaEjd2);
    Vector3.Add(ref _param0, ref xxpaxL8AcxpW32QaEjd2, out _param2);
  }

  public static void \u0023\u003Dz8miGAzg\u003D(
    Vector3[] _param0,
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La _param1)
  {
    for (int index = 0; index < _param0.Length; ++index)
      _param0[index] = Vector3.\u0023\u003Dz8miGAzg\u003D(_param0[index], _param1);
  }

  public static Vector3 \u0023\u003DzeZgX47MToa8F(
    Vector3 _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    Vector3 xxpaxL8AcxpW32QaEjd;
    Vector3.\u0023\u003DzeZgX47MToa8F(ref _param0, ref _param1, out xxpaxL8AcxpW32QaEjd);
    return xxpaxL8AcxpW32QaEjd;
  }

  public static void \u0023\u003DzeZgX47MToa8F(
    ref Vector3 _param0,
    ref \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1,
    out Vector3 _param2)
  {
    Vector4 bbfK6VhuQ6F9ZEjd = new Vector4(_param0);
    Vector4.\u0023\u003Dz8miGAzg\u003D(ref bbfK6VhuQ6F9ZEjd, ref _param1, out bbfK6VhuQ6F9ZEjd);
    _param2.dje_z3GBAX47U_ejd = bbfK6VhuQ6F9ZEjd.dje_z3GBAX47U_ejd / bbfK6VhuQ6F9ZEjd.dje_z3L8TB878_ejd;
    _param2.dje_zLPL6EZPA_ejd = bbfK6VhuQ6F9ZEjd.dje_zLPL6EZPA_ejd / bbfK6VhuQ6F9ZEjd.dje_z3L8TB878_ejd;
    _param2.dje_zX8HQ8VV9_ejd = bbfK6VhuQ6F9ZEjd.dje_zX8HQ8VV9_ejd / bbfK6VhuQ6F9ZEjd.dje_z3L8TB878_ejd;
  }

  public static double \u0023\u003DzoHsGiDori6LC(
    Vector3 _param0,
    Vector3 _param1)
  {
    return Math.Acos(Vector3.\u0023\u003DzJkPZoNo\u003D(_param0, _param1) / (_param0.\u0023\u003DzxhbmvAVxpXvh() * _param1.\u0023\u003DzxhbmvAVxpXvh()));
  }

  public static void \u0023\u003DzoHsGiDori6LC(
    ref Vector3 _param0,
    ref Vector3 _param1,
    out double _param2)
  {
    double num;
    Vector3.\u0023\u003DzJkPZoNo\u003D(ref _param0, ref _param1, out num);
    _param2 = Math.Acos(num / (_param0.\u0023\u003DzxhbmvAVxpXvh() * _param1.\u0023\u003DzxhbmvAVxpXvh()));
  }

  public Vector2 \u0023\u003Dz1ngzHGRUP9al()
  {
    return new Vector2(this.dje_z3GBAX47U_ejd, this.dje_zLPL6EZPA_ejd);
  }

  public void \u0023\u003DzVAfG8m1ZGQOK(
    Vector2 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
  }

  public static Vector3 operator +(
    Vector3 _param0,
    Vector3 _param1)
  {
    _param0.dje_z3GBAX47U_ejd += _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd += _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd += _param1.dje_zX8HQ8VV9_ejd;
    return _param0;
  }

  public static Vector3 operator -(
    Vector3 _param0,
    Vector3 _param1)
  {
    _param0.dje_z3GBAX47U_ejd -= _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd -= _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd -= _param1.dje_zX8HQ8VV9_ejd;
    return _param0;
  }

  public static Vector3 operator -(
    Vector3 _param0)
  {
    _param0.dje_z3GBAX47U_ejd = -_param0.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = -_param0.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = -_param0.dje_zX8HQ8VV9_ejd;
    return _param0;
  }

  public static Vector3 operator *(
    Vector3 _param0,
    Vector3 _param1)
  {
    _param0.dje_z3GBAX47U_ejd *= _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd *= _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd *= _param1.dje_zX8HQ8VV9_ejd;
    return _param0;
  }

  public static Vector3 operator *(
    Vector3 _param0,
    double _param1)
  {
    _param0.dje_z3GBAX47U_ejd *= _param1;
    _param0.dje_zLPL6EZPA_ejd *= _param1;
    _param0.dje_zX8HQ8VV9_ejd *= _param1;
    return _param0;
  }

  public static Vector3 operator *(
    double _param0,
    Vector3 _param1)
  {
    _param1.dje_z3GBAX47U_ejd *= _param0;
    _param1.dje_zLPL6EZPA_ejd *= _param0;
    _param1.dje_zX8HQ8VV9_ejd *= _param0;
    return _param1;
  }

  public static Vector3 operator /(
    double _param0,
    Vector3 _param1)
  {
    return new Vector3(_param0 / _param1.dje_z3GBAX47U_ejd, _param0 / _param1.dje_zLPL6EZPA_ejd, _param0 / _param1.dje_zX8HQ8VV9_ejd);
  }

  public static Vector3 operator /(
    Vector3 _param0,
    double _param1)
  {
    double num = 1.0 / _param1;
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    _param0.dje_zX8HQ8VV9_ejd *= num;
    return _param0;
  }

  public static bool operator ==(
    Vector3 _param0,
    Vector3 _param1)
  {
    return _param0.Equals(_param1);
  }

  public static bool operator !=(
    Vector3 _param0,
    Vector3 _param1)
  {
    return !_param0.Equals(_param1);
  }

  public override string ToString()
  {
    return $"[{this.dje_z3GBAX47U_ejd}, {this.dje_zLPL6EZPA_ejd}, {this.dje_zX8HQ8VV9_ejd}]";
  }

  public override int GetHashCode()
  {
    return new double[3]
    {
      this.dje_z3GBAX47U_ejd,
      this.dje_zLPL6EZPA_ejd,
      this.dje_zX8HQ8VV9_ejd
    }.GetHashCode();
  }

  public override bool Equals(object _param1)
  {
    return _param1 is Vector3 xxpaxL8AcxpW32QaEjd && this.Equals(xxpaxL8AcxpW32QaEjd);
  }

  public bool \u0023\u003DzhxbsSqM\u003D(
    Vector3 _param1,
    double _param2)
  {
    return this.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd + _param2 && this.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd - _param2 && this.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd + _param2 && this.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd - _param2 && this.dje_zX8HQ8VV9_ejd < _param1.dje_zX8HQ8VV9_ejd + _param2 && this.dje_zX8HQ8VV9_ejd > _param1.dje_zX8HQ8VV9_ejd - _param2;
  }

  public bool Equals(
    Vector3 _param1)
  {
    return this.dje_z3GBAX47U_ejd == _param1.dje_z3GBAX47U_ejd && this.dje_zLPL6EZPA_ejd == _param1.dje_zLPL6EZPA_ejd && this.dje_zX8HQ8VV9_ejd == _param1.dje_zX8HQ8VV9_ejd;
  }

  public static double \u0023\u003DzqQHPvHWdZgmR(
    Vector3 _param0)
  {
    return Math.Max(_param0.dje_z3GBAX47U_ejd, Math.Max(_param0.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd));
  }

  public static double \u0023\u003DzXTXKynGX9VVf(
    Vector3 _param0)
  {
    return Math.Min(_param0.dje_z3GBAX47U_ejd, Math.Min(_param0.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd));
  }
}
