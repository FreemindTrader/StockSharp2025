// Decompiled with JetBrains decompiler
// Type: Vector2
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
public struct Vector2 : 
  IEquatable<Vector2>
{
  
  public double dje_z3GBAX47U_ejd;
  
  public double dje_zLPL6EZPA_ejd;
  
  public static Vector2 dje_zUE7VTUP4_ejd = new Vector2(1.0, 0.0);
  
  public static Vector2 dje_zPEJ77TB7_ejd = new Vector2(0.0, 1.0);
  
  public static Vector2 dje_z2EADAZU3_ejd = new Vector2(0.0, 0.0);
  
  public static readonly Vector2 dje_zWJBJPBE9_ejd = new Vector2(1.0, 1.0);
  
  public static readonly int dje_zHNY3UCKLVTU9U6Q_ejd = Marshal.SizeOf<Vector2>(new Vector2());

  public Vector2(
    double _param1,
    double _param2)
  {
    this.dje_z3GBAX47U_ejd = _param1;
    this.dje_zLPL6EZPA_ejd = _param2;
  }

  public Vector2(
    Vector3 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
  }

  
  [IndexerName("#=zMRIb09I=")]
  public double this[int _param1]
  {
    get
    {
      if (_param1 == 0)
        return this.dje_z3GBAX47U_ejd;
      return _param1 == 1 ? this.dje_zLPL6EZPA_ejd : 0.0;
    }
    set
    {
      if (_param1 != 0)
      {
        if (_param1 != 1)
          throw new Exception();
        this.dje_zLPL6EZPA_ejd = value;
      }
      else
        this.dje_z3GBAX47U_ejd = value;
    }
  }

  public double \u0023\u003Dz\u0024CeUvME\u003D(int _param1)
  {
    if (_param1 == 0)
      return this.dje_z3GBAX47U_ejd;
    return _param1 == 1 ? this.dje_zLPL6EZPA_ejd : 0.0;
  }

  public void \u0023\u003DzS9gpfR4\u003D(int _param1, double _param2)
  {
    if (_param1 != 0)
    {
      if (_param1 != 1)
        throw new Exception();
      this.dje_zLPL6EZPA_ejd = _param2;
    }
    else
      this.dje_z3GBAX47U_ejd = _param2;
  }

  public double \u0023\u003DzxhbmvAVxpXvh()
  {
    return Math.Sqrt(this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd);
  }

  public double \u0023\u003DzXNz5ccu6mHa6()
  {
    return this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd;
  }

  public void \u0023\u003Dz1zxzaac\u003D(double _param1)
  {
    this = Vector2.\u0023\u003Dz1zxzaac\u003D(this, _param1);
  }

  public double \u0023\u003DzVnqACZw\u003D()
  {
    return Math.Atan2(this.dje_zLPL6EZPA_ejd, this.dje_z3GBAX47U_ejd);
  }

  public double \u0023\u003Dz\u0024dldkvHokmZUWayECw\u003D\u003D()
  {
    return \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzE1pg1ELlS2z_(this.\u0023\u003DzVnqACZw\u003D());
  }

  public Vector2 \u0023\u003DzKg_f0jWc1HrDMMo9mjGVkVqv7F9e()
  {
    return new Vector2(this.dje_zLPL6EZPA_ejd, -this.dje_z3GBAX47U_ejd);
  }

  public Vector2 \u0023\u003DzwdvWjygaNnkEbw9USoxNFijPomHC()
  {
    return new Vector2(-this.dje_zLPL6EZPA_ejd, this.dje_z3GBAX47U_ejd);
  }

  public Vector2 \u0023\u003DzWQoDUw0\u003D()
  {
    Vector2 jnelpsqX4Q78W2Ejd = this;
    jnelpsqX4Q78W2Ejd.\u0023\u003DzC520uIs\u003D();
    return jnelpsqX4Q78W2Ejd;
  }

  public void \u0023\u003DzC520uIs\u003D()
  {
    double num = 1.0 / this.\u0023\u003DzxhbmvAVxpXvh();
    this.dje_z3GBAX47U_ejd *= num;
    this.dje_zLPL6EZPA_ejd *= num;
  }

  public static Vector2 Add(
    Vector2 _param0,
    Vector2 _param1)
  {
    Vector2.Add(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void Add(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2 = new Vector2(_param0.dje_z3GBAX47U_ejd + _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd + _param1.dje_zLPL6EZPA_ejd);
  }

  public static Vector2 Subtract(
    Vector2 _param0,
    Vector2 _param1)
  {
    Vector2.Subtract(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void Subtract(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2 = new Vector2(_param0.dje_z3GBAX47U_ejd - _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd - _param1.dje_zLPL6EZPA_ejd);
  }

  public static Vector2 \u0023\u003DzQw38IaY\u003D(
    Vector2 _param0,
    double _param1)
  {
    Vector2.\u0023\u003DzQw38IaY\u003D(ref _param0, _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003DzQw38IaY\u003D(
    ref Vector2 _param0,
    double _param1,
    out Vector2 _param2)
  {
    _param2 = new Vector2(_param0.dje_z3GBAX47U_ejd * _param1, _param0.dje_zLPL6EZPA_ejd * _param1);
  }

  public static Vector2 \u0023\u003DzQw38IaY\u003D(
    Vector2 _param0,
    Vector2 _param1)
  {
    Vector2.\u0023\u003DzQw38IaY\u003D(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003DzQw38IaY\u003D(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2 = new Vector2(_param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd);
  }

  public static Vector2 \u0023\u003Dzmd\u0024taME\u003D(
    Vector2 _param0,
    double _param1)
  {
    Vector2.\u0023\u003Dzmd\u0024taME\u003D(ref _param0, _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003Dzmd\u0024taME\u003D(
    ref Vector2 _param0,
    double _param1,
    out Vector2 _param2)
  {
    Vector2.\u0023\u003DzQw38IaY\u003D(ref _param0, 1.0 / _param1, out _param2);
  }

  public static Vector2 \u0023\u003Dzmd\u0024taME\u003D(
    Vector2 _param0,
    Vector2 _param1)
  {
    Vector2.\u0023\u003Dzmd\u0024taME\u003D(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003Dzmd\u0024taME\u003D(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2 = new Vector2(_param0.dje_z3GBAX47U_ejd / _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd / _param1.dje_zLPL6EZPA_ejd);
  }

  public static Vector2 Min(
    Vector2 _param0,
    Vector2 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static void Min(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
  }

  public static Vector2 Max(
    Vector2 _param0,
    Vector2 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static void Max(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
  }

  public static Vector2 \u0023\u003Dz7h4Pu5Gr1WYQ(
    Vector2 _param0,
    Vector2 _param1,
    Vector2 _param2)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param1.dje_z3GBAX47U_ejd : (_param0.dje_z3GBAX47U_ejd > _param2.dje_z3GBAX47U_ejd ? _param2.dje_z3GBAX47U_ejd : _param0.dje_z3GBAX47U_ejd);
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param1.dje_zLPL6EZPA_ejd : (_param0.dje_zLPL6EZPA_ejd > _param2.dje_zLPL6EZPA_ejd ? _param2.dje_zLPL6EZPA_ejd : _param0.dje_zLPL6EZPA_ejd);
    return _param0;
  }

  public static void \u0023\u003Dz7h4Pu5Gr1WYQ(
    ref Vector2 _param0,
    ref Vector2 _param1,
    ref Vector2 _param2,
    out Vector2 _param3)
  {
    _param3.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param1.dje_z3GBAX47U_ejd : (_param0.dje_z3GBAX47U_ejd > _param2.dje_z3GBAX47U_ejd ? _param2.dje_z3GBAX47U_ejd : _param0.dje_z3GBAX47U_ejd);
    _param3.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param1.dje_zLPL6EZPA_ejd : (_param0.dje_zLPL6EZPA_ejd > _param2.dje_zLPL6EZPA_ejd ? _param2.dje_zLPL6EZPA_ejd : _param0.dje_zLPL6EZPA_ejd);
  }

  public static Vector2 \u0023\u003DzC520uIs\u003D(
    Vector2 _param0)
  {
    double num = 1.0 / _param0.\u0023\u003DzxhbmvAVxpXvh();
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    return _param0;
  }

  public static void \u0023\u003DzC520uIs\u003D(
    ref Vector2 _param0,
    out Vector2 _param1)
  {
    double num = 1.0 / _param0.\u0023\u003DzxhbmvAVxpXvh();
    _param1.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * num;
    _param1.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd * num;
  }

  public static Vector2 \u0023\u003Dz_fJUMYtCeW_1(
    Vector2 _param0)
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(_param0.dje_z3GBAX47U_ejd * _param0.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param0.dje_zLPL6EZPA_ejd);
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    return _param0;
  }

  public static void \u0023\u003Dz_fJUMYtCeW_1(
    ref Vector2 _param0,
    out Vector2 _param1)
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(_param0.dje_z3GBAX47U_ejd * _param0.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param0.dje_zLPL6EZPA_ejd);
    _param1.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * num;
    _param1.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd * num;
  }

  public static double \u0023\u003DzJkPZoNo\u003D(
    Vector2 _param0,
    Vector2 _param1)
  {
    return _param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd;
  }

  public static void \u0023\u003DzJkPZoNo\u003D(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out double _param2)
  {
    _param2 = _param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd;
  }

  public static double \u0023\u003Dz1jcyxnU\u003D(
    Vector2 _param0,
    Vector2 _param1)
  {
    return _param0.dje_z3GBAX47U_ejd * _param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd * _param1.dje_z3GBAX47U_ejd;
  }

  public static Vector2 \u0023\u003Dz1zxzaac\u003D(
    Vector2 _param0,
    double _param1)
  {
    Vector2 jnelpsqX4Q78W2Ejd;
    Vector2.\u0023\u003Dz1zxzaac\u003D(ref _param0, _param1, out jnelpsqX4Q78W2Ejd);
    return jnelpsqX4Q78W2Ejd;
  }

  public static void \u0023\u003Dz1zxzaac\u003D(
    ref Vector2 _param0,
    double _param1,
    out Vector2 _param2)
  {
    double num1 = Math.Cos(_param1);
    double num2 = Math.Sin(_param1);
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * num1 - _param0.dje_zLPL6EZPA_ejd * num2;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd * num1 + _param0.dje_z3GBAX47U_ejd * num2;
  }

  public static Vector2 \u0023\u003DzLBajzIS0VDuy(
    Vector2 _param0,
    Vector2 _param1,
    double _param2)
  {
    _param0.dje_z3GBAX47U_ejd = _param2 * (_param1.dje_z3GBAX47U_ejd - _param0.dje_z3GBAX47U_ejd) + _param0.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param2 * (_param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd) + _param0.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static void \u0023\u003DzLBajzIS0VDuy(
    ref Vector2 _param0,
    ref Vector2 _param1,
    double _param2,
    out Vector2 _param3)
  {
    _param3.dje_z3GBAX47U_ejd = _param2 * (_param1.dje_z3GBAX47U_ejd - _param0.dje_z3GBAX47U_ejd) + _param0.dje_z3GBAX47U_ejd;
    _param3.dje_zLPL6EZPA_ejd = _param2 * (_param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd) + _param0.dje_zLPL6EZPA_ejd;
  }

  public static Vector2 \u0023\u003Dzie4S5q5_H9GgLyLdMw\u003D\u003D(
    Vector2 _param0,
    Vector2 _param1,
    Vector2 _param2,
    double _param3,
    double _param4)
  {
    return _param0 + _param3 * (_param1 - _param0) + _param4 * (_param2 - _param0);
  }

  public static void \u0023\u003Dzie4S5q5_H9GgLyLdMw\u003D\u003D(
    ref Vector2 _param0,
    ref Vector2 _param1,
    ref Vector2 _param2,
    double _param3,
    double _param4,
    out Vector2 _param5)
  {
    _param5 = _param0;
    Vector2 jnelpsqX4Q78W2Ejd1 = _param1;
    Vector2.Subtract(ref jnelpsqX4Q78W2Ejd1, ref _param0, out jnelpsqX4Q78W2Ejd1);
    Vector2.\u0023\u003DzQw38IaY\u003D(ref jnelpsqX4Q78W2Ejd1, _param3, out jnelpsqX4Q78W2Ejd1);
    Vector2.Add(ref _param5, ref jnelpsqX4Q78W2Ejd1, out _param5);
    Vector2 jnelpsqX4Q78W2Ejd2 = _param2;
    Vector2.Subtract(ref jnelpsqX4Q78W2Ejd2, ref _param0, out jnelpsqX4Q78W2Ejd2);
    Vector2.\u0023\u003DzQw38IaY\u003D(ref jnelpsqX4Q78W2Ejd2, _param4, out jnelpsqX4Q78W2Ejd2);
    Vector2.Add(ref _param5, ref jnelpsqX4Q78W2Ejd2, out _param5);
  }

  public static Vector2 \u0023\u003Dz8miGAzg\u003D(
    Vector2 _param0,
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La _param1)
  {
    Vector2 jnelpsqX4Q78W2Ejd;
    Vector2.\u0023\u003Dz8miGAzg\u003D(ref _param0, ref _param1, out jnelpsqX4Q78W2Ejd);
    return jnelpsqX4Q78W2Ejd;
  }

  public static void \u0023\u003Dz8miGAzg\u003D(
    ref Vector2 _param0,
    ref \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La _param1,
    out Vector2 _param2)
  {
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La k0tx8mf6Q5GfeLxZsn6La1 = new \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La(_param0.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd, 0.0, 0.0);
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La k0tx8mf6Q5GfeLxZsn6La2;
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La.\u0023\u003DzkRsyJTI\u003D(ref _param1, out k0tx8mf6Q5GfeLxZsn6La2);
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La k0tx8mf6Q5GfeLxZsn6La3;
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La.\u0023\u003DzQw38IaY\u003D(ref _param1, ref k0tx8mf6Q5GfeLxZsn6La1, out k0tx8mf6Q5GfeLxZsn6La3);
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La.\u0023\u003DzQw38IaY\u003D(ref k0tx8mf6Q5GfeLxZsn6La3, ref k0tx8mf6Q5GfeLxZsn6La2, out k0tx8mf6Q5GfeLxZsn6La1);
    _param2 = new Vector2(k0tx8mf6Q5GfeLxZsn6La1.X, k0tx8mf6Q5GfeLxZsn6La1.Y);
  }

  public static Vector2 \u0023\u003DzXTXKynGX9VVf(
    Vector2 _param0,
    Vector2 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static void \u0023\u003DzXTXKynGX9VVf(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
  }

  public static Vector2 \u0023\u003DzqQHPvHWdZgmR(
    Vector2 _param0,
    Vector2 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static void \u0023\u003DzqQHPvHWdZgmR(
    ref Vector2 _param0,
    ref Vector2 _param1,
    out Vector2 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
  }

  public static Vector2 operator +(
    Vector2 _param0,
    Vector2 _param1)
  {
    _param0.dje_z3GBAX47U_ejd += _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd += _param1.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static Vector2 operator -(
    Vector2 _param0,
    Vector2 _param1)
  {
    _param0.dje_z3GBAX47U_ejd -= _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd -= _param1.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static Vector2 operator -(
    Vector2 _param0)
  {
    _param0.dje_z3GBAX47U_ejd = -_param0.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = -_param0.dje_zLPL6EZPA_ejd;
    return _param0;
  }

  public static Vector2 operator *(
    Vector2 _param0,
    double _param1)
  {
    _param0.dje_z3GBAX47U_ejd *= _param1;
    _param0.dje_zLPL6EZPA_ejd *= _param1;
    return _param0;
  }

  public static Vector2 operator *(
    double _param0,
    Vector2 _param1)
  {
    _param1.dje_z3GBAX47U_ejd *= _param0;
    _param1.dje_zLPL6EZPA_ejd *= _param0;
    return _param1;
  }

  public static Vector2 operator /(
    Vector2 _param0,
    double _param1)
  {
    double num = 1.0 / _param1;
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    return _param0;
  }

  public static Vector2 operator /(
    double _param0,
    Vector2 _param1)
  {
    _param1.dje_z3GBAX47U_ejd = _param0 / _param1.dje_z3GBAX47U_ejd;
    _param1.dje_zLPL6EZPA_ejd = _param0 / _param1.dje_zLPL6EZPA_ejd;
    return _param1;
  }

  public static bool operator ==(
    Vector2 _param0,
    Vector2 _param1)
  {
    return _param0.Equals(_param1);
  }

  public static bool operator !=(
    Vector2 _param0,
    Vector2 _param1)
  {
    return !_param0.Equals(_param1);
  }

  public override string ToString() => $"({this.dje_z3GBAX47U_ejd}, {this.dje_zLPL6EZPA_ejd})";

  public override int GetHashCode()
  {
    return new double[2]
    {
      this.dje_z3GBAX47U_ejd,
      this.dje_zLPL6EZPA_ejd
    }.GetHashCode();
  }

  public override bool Equals(object _param1)
  {
    return _param1 is Vector2 jnelpsqX4Q78W2Ejd && this.Equals(jnelpsqX4Q78W2Ejd);
  }

  public bool Equals(
    Vector2 _param1)
  {
    return this.dje_z3GBAX47U_ejd == _param1.dje_z3GBAX47U_ejd && this.dje_zLPL6EZPA_ejd == _param1.dje_zLPL6EZPA_ejd;
  }

  public bool \u0023\u003DzhxbsSqM\u003D(
    Vector2 _param1,
    double _param2)
  {
    return this.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd + _param2 && this.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd - _param2 && this.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd + _param2 && this.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd - _param2;
  }
}
