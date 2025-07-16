// Decompiled with JetBrains decompiler
// Type: Vector4
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
internal struct Vector4 : 
  IEquatable<Vector4>
{
  
  public double dje_z3GBAX47U_ejd;
  
  public double dje_zLPL6EZPA_ejd;
  
  public double dje_zX8HQ8VV9_ejd;
  
  public double dje_z3L8TB878_ejd;
  
  public static Vector4 dje_zUE7VTUP4_ejd = new Vector4(1.0, 0.0, 0.0, 0.0);
  
  public static Vector4 dje_zPEJ77TB7_ejd = new Vector4(0.0, 1.0, 0.0, 0.0);
  
  public static Vector4 dje_z9CMHUJAZ_ejd = new Vector4(0.0, 0.0, 1.0, 0.0);
  
  public static Vector4 dje_zTJG9RJL7_ejd = new Vector4(0.0, 0.0, 0.0, 1.0);
  
  public static Vector4 dje_z2EADAZU3_ejd = new Vector4(0.0, 0.0, 0.0, 0.0);
  
  public static readonly Vector4 dje_zWJBJPBE9_ejd = new Vector4(1.0, 1.0, 1.0, 1.0);
  
  public static readonly int dje_zHNY3UCKLVTU9U6Q_ejd = Marshal.SizeOf<Vector4>(new Vector4());

  public Vector4(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.dje_z3GBAX47U_ejd = _param1;
    this.dje_zLPL6EZPA_ejd = _param2;
    this.dje_zX8HQ8VV9_ejd = _param3;
    this.dje_z3L8TB878_ejd = _param4;
  }

  public Vector4(
    Vector2 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = 0.0;
    this.dje_z3L8TB878_ejd = 0.0;
  }

  public Vector4(
    Vector3 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = _param1.dje_zX8HQ8VV9_ejd;
    this.dje_z3L8TB878_ejd = 0.0;
  }

  public Vector4(
    Vector3 _param1,
    double _param2)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = _param1.dje_zX8HQ8VV9_ejd;
    this.dje_z3L8TB878_ejd = _param2;
  }

  public Vector4(
    Vector4 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = _param1.dje_zX8HQ8VV9_ejd;
    this.dje_z3L8TB878_ejd = _param1.dje_z3L8TB878_ejd;
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
        case 3:
          return this.dje_z3L8TB878_ejd;
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
        case 3:
          this.dje_z3L8TB878_ejd = value;
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
      case 3:
        return this.dje_z3L8TB878_ejd;
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
      case 3:
        this.dje_z3L8TB878_ejd = _param2;
        break;
      default:
        throw new Exception();
    }
  }

  public double \u0023\u003DzxhbmvAVxpXvh()
  {
    return Math.Sqrt(this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd + this.dje_z3L8TB878_ejd * this.dje_z3L8TB878_ejd);
  }

  public double \u0023\u003DzdKlVd6PL86\u0024r()
  {
    return 1.0 / \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd + this.dje_z3L8TB878_ejd * this.dje_z3L8TB878_ejd);
  }

  public double \u0023\u003DzXNz5ccu6mHa6()
  {
    return this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd + this.dje_z3L8TB878_ejd * this.dje_z3L8TB878_ejd;
  }

  public void \u0023\u003DzC520uIs\u003D()
  {
    double num = 1.0 / this.\u0023\u003DzxhbmvAVxpXvh();
    this.dje_z3GBAX47U_ejd *= num;
    this.dje_zLPL6EZPA_ejd *= num;
    this.dje_zX8HQ8VV9_ejd *= num;
    this.dje_z3L8TB878_ejd *= num;
  }

  public void \u0023\u003Dz_fJUMYtCeW_1()
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(this.dje_z3GBAX47U_ejd * this.dje_z3GBAX47U_ejd + this.dje_zLPL6EZPA_ejd * this.dje_zLPL6EZPA_ejd + this.dje_zX8HQ8VV9_ejd * this.dje_zX8HQ8VV9_ejd + this.dje_z3L8TB878_ejd * this.dje_z3L8TB878_ejd);
    this.dje_z3GBAX47U_ejd *= num;
    this.dje_zLPL6EZPA_ejd *= num;
    this.dje_zX8HQ8VV9_ejd *= num;
    this.dje_z3L8TB878_ejd *= num;
  }

  public static Vector4 Add(
    Vector4 _param0,
    Vector4 _param1)
  {
    Vector4.Add(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void Add(
    ref Vector4 _param0,
    ref Vector4 _param1,
    out Vector4 _param2)
  {
    _param2 = new Vector4(_param0.dje_z3GBAX47U_ejd + _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd + _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd + _param1.dje_zX8HQ8VV9_ejd, _param0.dje_z3L8TB878_ejd + _param1.dje_z3L8TB878_ejd);
  }

  public static Vector4 Subtract(
    Vector4 _param0,
    Vector4 _param1)
  {
    Vector4.Subtract(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void Subtract(
    ref Vector4 _param0,
    ref Vector4 _param1,
    out Vector4 _param2)
  {
    _param2 = new Vector4(_param0.dje_z3GBAX47U_ejd - _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd - _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd - _param1.dje_zX8HQ8VV9_ejd, _param0.dje_z3L8TB878_ejd - _param1.dje_z3L8TB878_ejd);
  }

  public static Vector4 \u0023\u003DzQw38IaY\u003D(
    Vector4 _param0,
    double _param1)
  {
    Vector4.\u0023\u003DzQw38IaY\u003D(ref _param0, _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003DzQw38IaY\u003D(
    ref Vector4 _param0,
    double _param1,
    out Vector4 _param2)
  {
    _param2 = new Vector4(_param0.dje_z3GBAX47U_ejd * _param1, _param0.dje_zLPL6EZPA_ejd * _param1, _param0.dje_zX8HQ8VV9_ejd * _param1, _param0.dje_z3L8TB878_ejd * _param1);
  }

  public static Vector4 \u0023\u003DzQw38IaY\u003D(
    Vector4 _param0,
    Vector4 _param1)
  {
    Vector4.\u0023\u003DzQw38IaY\u003D(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003DzQw38IaY\u003D(
    ref Vector4 _param0,
    ref Vector4 _param1,
    out Vector4 _param2)
  {
    _param2 = new Vector4(_param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd * _param1.dje_zX8HQ8VV9_ejd, _param0.dje_z3L8TB878_ejd * _param1.dje_z3L8TB878_ejd);
  }

  public static Vector4 \u0023\u003Dzmd\u0024taME\u003D(
    Vector4 _param0,
    double _param1)
  {
    Vector4.\u0023\u003Dzmd\u0024taME\u003D(ref _param0, _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003Dzmd\u0024taME\u003D(
    ref Vector4 _param0,
    double _param1,
    out Vector4 _param2)
  {
    Vector4.\u0023\u003DzQw38IaY\u003D(ref _param0, 1.0 / _param1, out _param2);
  }

  public static Vector4 \u0023\u003Dzmd\u0024taME\u003D(
    Vector4 _param0,
    Vector4 _param1)
  {
    Vector4.\u0023\u003Dzmd\u0024taME\u003D(ref _param0, ref _param1, out _param0);
    return _param0;
  }

  public static void \u0023\u003Dzmd\u0024taME\u003D(
    ref Vector4 _param0,
    ref Vector4 _param1,
    out Vector4 _param2)
  {
    _param2 = new Vector4(_param0.dje_z3GBAX47U_ejd / _param1.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd / _param1.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd / _param1.dje_zX8HQ8VV9_ejd, _param0.dje_z3L8TB878_ejd / _param1.dje_z3L8TB878_ejd);
  }

  public static Vector4 Min(
    Vector4 _param0,
    Vector4 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
    _param0.dje_z3L8TB878_ejd = _param0.dje_z3L8TB878_ejd < _param1.dje_z3L8TB878_ejd ? _param0.dje_z3L8TB878_ejd : _param1.dje_z3L8TB878_ejd;
    return _param0;
  }

  public static void Min(
    ref Vector4 _param0,
    ref Vector4 _param1,
    out Vector4 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param2.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
    _param2.dje_z3L8TB878_ejd = _param0.dje_z3L8TB878_ejd < _param1.dje_z3L8TB878_ejd ? _param0.dje_z3L8TB878_ejd : _param1.dje_z3L8TB878_ejd;
  }

  public static Vector4 Max(
    Vector4 _param0,
    Vector4 _param1)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd > _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
    _param0.dje_z3L8TB878_ejd = _param0.dje_z3L8TB878_ejd > _param1.dje_z3L8TB878_ejd ? _param0.dje_z3L8TB878_ejd : _param1.dje_z3L8TB878_ejd;
    return _param0;
  }

  public static void Max(
    ref Vector4 _param0,
    ref Vector4 _param1,
    out Vector4 _param2)
  {
    _param2.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd > _param1.dje_z3GBAX47U_ejd ? _param0.dje_z3GBAX47U_ejd : _param1.dje_z3GBAX47U_ejd;
    _param2.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd > _param1.dje_zLPL6EZPA_ejd ? _param0.dje_zLPL6EZPA_ejd : _param1.dje_zLPL6EZPA_ejd;
    _param2.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd > _param1.dje_zX8HQ8VV9_ejd ? _param0.dje_zX8HQ8VV9_ejd : _param1.dje_zX8HQ8VV9_ejd;
    _param2.dje_z3L8TB878_ejd = _param0.dje_z3L8TB878_ejd > _param1.dje_z3L8TB878_ejd ? _param0.dje_z3L8TB878_ejd : _param1.dje_z3L8TB878_ejd;
  }

  public static Vector4 \u0023\u003Dz7h4Pu5Gr1WYQ(
    Vector4 _param0,
    Vector4 _param1,
    Vector4 _param2)
  {
    _param0.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param1.dje_z3GBAX47U_ejd : (_param0.dje_z3GBAX47U_ejd > _param2.dje_z3GBAX47U_ejd ? _param2.dje_z3GBAX47U_ejd : _param0.dje_z3GBAX47U_ejd);
    _param0.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param1.dje_zLPL6EZPA_ejd : (_param0.dje_zLPL6EZPA_ejd > _param2.dje_zLPL6EZPA_ejd ? _param2.dje_zLPL6EZPA_ejd : _param0.dje_zLPL6EZPA_ejd);
    _param0.dje_zX8HQ8VV9_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param1.dje_zX8HQ8VV9_ejd : (_param0.dje_zX8HQ8VV9_ejd > _param2.dje_zX8HQ8VV9_ejd ? _param2.dje_zX8HQ8VV9_ejd : _param0.dje_zX8HQ8VV9_ejd);
    _param0.dje_z3L8TB878_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_z3L8TB878_ejd ? _param1.dje_z3L8TB878_ejd : (_param0.dje_z3L8TB878_ejd > _param2.dje_z3L8TB878_ejd ? _param2.dje_z3L8TB878_ejd : _param0.dje_z3L8TB878_ejd);
    return _param0;
  }

  public static void \u0023\u003Dz7h4Pu5Gr1WYQ(
    ref Vector4 _param0,
    ref Vector4 _param1,
    ref Vector4 _param2,
    out Vector4 _param3)
  {
    _param3.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_z3GBAX47U_ejd ? _param1.dje_z3GBAX47U_ejd : (_param0.dje_z3GBAX47U_ejd > _param2.dje_z3GBAX47U_ejd ? _param2.dje_z3GBAX47U_ejd : _param0.dje_z3GBAX47U_ejd);
    _param3.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_zLPL6EZPA_ejd ? _param1.dje_zLPL6EZPA_ejd : (_param0.dje_zLPL6EZPA_ejd > _param2.dje_zLPL6EZPA_ejd ? _param2.dje_zLPL6EZPA_ejd : _param0.dje_zLPL6EZPA_ejd);
    _param3.dje_zX8HQ8VV9_ejd = _param0.dje_z3GBAX47U_ejd < _param1.dje_zX8HQ8VV9_ejd ? _param1.dje_zX8HQ8VV9_ejd : (_param0.dje_zX8HQ8VV9_ejd > _param2.dje_zX8HQ8VV9_ejd ? _param2.dje_zX8HQ8VV9_ejd : _param0.dje_zX8HQ8VV9_ejd);
    _param3.dje_z3L8TB878_ejd = _param0.dje_zLPL6EZPA_ejd < _param1.dje_z3L8TB878_ejd ? _param1.dje_z3L8TB878_ejd : (_param0.dje_z3L8TB878_ejd > _param2.dje_z3L8TB878_ejd ? _param2.dje_z3L8TB878_ejd : _param0.dje_z3L8TB878_ejd);
  }

  public static Vector4 \u0023\u003DzC520uIs\u003D(
    Vector4 _param0)
  {
    double num = 1.0 / _param0.\u0023\u003DzxhbmvAVxpXvh();
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    _param0.dje_zX8HQ8VV9_ejd *= num;
    _param0.dje_z3L8TB878_ejd *= num;
    return _param0;
  }

  public static void \u0023\u003DzC520uIs\u003D(
    ref Vector4 _param0,
    out Vector4 _param1)
  {
    double num = 1.0 / _param0.\u0023\u003DzxhbmvAVxpXvh();
    _param1.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * num;
    _param1.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd * num;
    _param1.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd * num;
    _param1.dje_z3L8TB878_ejd = _param0.dje_z3L8TB878_ejd * num;
  }

  public static Vector4 \u0023\u003Dz_fJUMYtCeW_1(
    Vector4 _param0)
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(_param0.dje_z3GBAX47U_ejd * _param0.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param0.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param0.dje_zX8HQ8VV9_ejd + _param0.dje_z3L8TB878_ejd * _param0.dje_z3L8TB878_ejd);
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    _param0.dje_zX8HQ8VV9_ejd *= num;
    _param0.dje_z3L8TB878_ejd *= num;
    return _param0;
  }

  public static void \u0023\u003Dz_fJUMYtCeW_1(
    ref Vector4 _param0,
    out Vector4 _param1)
  {
    double num = \u0023\u003DzAWpkoWPAfFQEtlAHXmXhio86vmI2XKycmgX\u0024bXbMoWy0WSCN5qTb7KX3DSGIbEK1Aw\u003D\u003D.\u0023\u003DzBFOpBnXKp_m76HjXMQ\u003D\u003D(_param0.dje_z3GBAX47U_ejd * _param0.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param0.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param0.dje_zX8HQ8VV9_ejd + _param0.dje_z3L8TB878_ejd * _param0.dje_z3L8TB878_ejd);
    _param1.dje_z3GBAX47U_ejd = _param0.dje_z3GBAX47U_ejd * num;
    _param1.dje_zLPL6EZPA_ejd = _param0.dje_zLPL6EZPA_ejd * num;
    _param1.dje_zX8HQ8VV9_ejd = _param0.dje_zX8HQ8VV9_ejd * num;
    _param1.dje_z3L8TB878_ejd = _param0.dje_z3L8TB878_ejd * num;
  }

  public static double \u0023\u003DzJkPZoNo\u003D(
    Vector4 _param0,
    Vector4 _param1)
  {
    return _param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.dje_zX8HQ8VV9_ejd + _param0.dje_z3L8TB878_ejd * _param1.dje_z3L8TB878_ejd;
  }

  public static void \u0023\u003DzJkPZoNo\u003D(
    ref Vector4 _param0,
    ref Vector4 _param1,
    out double _param2)
  {
    _param2 = _param0.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.dje_zX8HQ8VV9_ejd + _param0.dje_z3L8TB878_ejd * _param1.dje_z3L8TB878_ejd;
  }

  public static Vector4 \u0023\u003DzLBajzIS0VDuy(
    Vector4 _param0,
    Vector4 _param1,
    double _param2)
  {
    _param0.dje_z3GBAX47U_ejd = _param2 * (_param1.dje_z3GBAX47U_ejd - _param0.dje_z3GBAX47U_ejd) + _param0.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = _param2 * (_param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd) + _param0.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = _param2 * (_param1.dje_zX8HQ8VV9_ejd - _param0.dje_zX8HQ8VV9_ejd) + _param0.dje_zX8HQ8VV9_ejd;
    _param0.dje_z3L8TB878_ejd = _param2 * (_param1.dje_z3L8TB878_ejd - _param0.dje_z3L8TB878_ejd) + _param0.dje_z3L8TB878_ejd;
    return _param0;
  }

  public static void \u0023\u003DzLBajzIS0VDuy(
    ref Vector4 _param0,
    ref Vector4 _param1,
    double _param2,
    out Vector4 _param3)
  {
    _param3.dje_z3GBAX47U_ejd = _param2 * (_param1.dje_z3GBAX47U_ejd - _param0.dje_z3GBAX47U_ejd) + _param0.dje_z3GBAX47U_ejd;
    _param3.dje_zLPL6EZPA_ejd = _param2 * (_param1.dje_zLPL6EZPA_ejd - _param0.dje_zLPL6EZPA_ejd) + _param0.dje_zLPL6EZPA_ejd;
    _param3.dje_zX8HQ8VV9_ejd = _param2 * (_param1.dje_zX8HQ8VV9_ejd - _param0.dje_zX8HQ8VV9_ejd) + _param0.dje_zX8HQ8VV9_ejd;
    _param3.dje_z3L8TB878_ejd = _param2 * (_param1.dje_z3L8TB878_ejd - _param0.dje_z3L8TB878_ejd) + _param0.dje_z3L8TB878_ejd;
  }

  public static Vector4 \u0023\u003Dzie4S5q5_H9GgLyLdMw\u003D\u003D(
    Vector4 _param0,
    Vector4 _param1,
    Vector4 _param2,
    double _param3,
    double _param4)
  {
    return _param0 + _param3 * (_param1 - _param0) + _param4 * (_param2 - _param0);
  }

  public static void \u0023\u003Dzie4S5q5_H9GgLyLdMw\u003D\u003D(
    ref Vector4 _param0,
    ref Vector4 _param1,
    ref Vector4 _param2,
    double _param3,
    double _param4,
    out Vector4 _param5)
  {
    _param5 = _param0;
    Vector4 bbfK6VhuQ6F9ZEjd1 = _param1;
    Vector4.Subtract(ref bbfK6VhuQ6F9ZEjd1, ref _param0, out bbfK6VhuQ6F9ZEjd1);
    Vector4.\u0023\u003DzQw38IaY\u003D(ref bbfK6VhuQ6F9ZEjd1, _param3, out bbfK6VhuQ6F9ZEjd1);
    Vector4.Add(ref _param5, ref bbfK6VhuQ6F9ZEjd1, out _param5);
    Vector4 bbfK6VhuQ6F9ZEjd2 = _param2;
    Vector4.Subtract(ref bbfK6VhuQ6F9ZEjd2, ref _param0, out bbfK6VhuQ6F9ZEjd2);
    Vector4.\u0023\u003DzQw38IaY\u003D(ref bbfK6VhuQ6F9ZEjd2, _param4, out bbfK6VhuQ6F9ZEjd2);
    Vector4.Add(ref _param5, ref bbfK6VhuQ6F9ZEjd2, out _param5);
  }

  public static Vector4 \u0023\u003Dz8miGAzg\u003D(
    Vector4 _param0,
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1)
  {
    Vector4 bbfK6VhuQ6F9ZEjd;
    Vector4.\u0023\u003Dz8miGAzg\u003D(ref _param0, ref _param1, out bbfK6VhuQ6F9ZEjd);
    return bbfK6VhuQ6F9ZEjd;
  }

  public static void \u0023\u003Dz8miGAzg\u003D(
    ref Vector4 _param0,
    ref \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb\u0024vygbXu8nk5VX8Tuiw2QfMqU6inn4WBczij_KAygP156vtDeaE\u003D _param1,
    out Vector4 _param2)
  {
    _param2 = new Vector4(_param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_z3GBAX47U_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_z3GBAX47U_ejd + _param0.dje_z3L8TB878_ejd * _param1.\u0023\u003DzE3ZR8NI\u003D.dje_z3GBAX47U_ejd, _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zLPL6EZPA_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zLPL6EZPA_ejd + _param0.dje_z3L8TB878_ejd * _param1.\u0023\u003DzE3ZR8NI\u003D.dje_zLPL6EZPA_ejd, _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_zX8HQ8VV9_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_zX8HQ8VV9_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_zX8HQ8VV9_ejd + _param0.dje_z3L8TB878_ejd * _param1.\u0023\u003DzE3ZR8NI\u003D.dje_zX8HQ8VV9_ejd, _param0.dje_z3GBAX47U_ejd * _param1.\u0023\u003Dzctm1rVw\u003D.dje_z3L8TB878_ejd + _param0.dje_zLPL6EZPA_ejd * _param1.\u0023\u003Dz90ZjCUw\u003D.dje_z3L8TB878_ejd + _param0.dje_zX8HQ8VV9_ejd * _param1.\u0023\u003DzCnT1wOM\u003D.dje_z3L8TB878_ejd + _param0.dje_z3L8TB878_ejd * _param1.\u0023\u003DzE3ZR8NI\u003D.dje_z3L8TB878_ejd);
  }

  public static Vector4 \u0023\u003Dz8miGAzg\u003D(
    Vector4 _param0,
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La _param1)
  {
    Vector4 bbfK6VhuQ6F9ZEjd;
    Vector4.\u0023\u003Dz8miGAzg\u003D(ref _param0, ref _param1, out bbfK6VhuQ6F9ZEjd);
    return bbfK6VhuQ6F9ZEjd;
  }

  public static void \u0023\u003Dz8miGAzg\u003D(
    ref Vector4 _param0,
    ref \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La _param1,
    out Vector4 _param2)
  {
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La k0tx8mf6Q5GfeLxZsn6La1 = new \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La(_param0.dje_z3GBAX47U_ejd, _param0.dje_zLPL6EZPA_ejd, _param0.dje_zX8HQ8VV9_ejd, _param0.dje_z3L8TB878_ejd);
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La k0tx8mf6Q5GfeLxZsn6La2;
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La.\u0023\u003DzkRsyJTI\u003D(ref _param1, out k0tx8mf6Q5GfeLxZsn6La2);
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La k0tx8mf6Q5GfeLxZsn6La3;
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La.\u0023\u003DzQw38IaY\u003D(ref _param1, ref k0tx8mf6Q5GfeLxZsn6La1, out k0tx8mf6Q5GfeLxZsn6La3);
    \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUgldoNrrtrtOs3rdVdF4M4ErNJSUhvL_ZNRK0tx8mf6Q5GfeLXZsn6La.\u0023\u003DzQw38IaY\u003D(ref k0tx8mf6Q5GfeLxZsn6La3, ref k0tx8mf6Q5GfeLxZsn6La2, out k0tx8mf6Q5GfeLxZsn6La1);
    _param2 = new Vector4(k0tx8mf6Q5GfeLxZsn6La1.\u0023\u003Dz2_4KSTY\u003D(), k0tx8mf6Q5GfeLxZsn6La1.\u0023\u003Dzu7q98_E\u003D(), k0tx8mf6Q5GfeLxZsn6La1.\u0023\u003DzM49\u0024G3E\u003D(), k0tx8mf6Q5GfeLxZsn6La1.\u0023\u003DzMFog0bw\u003D());
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

  public Vector3 \u0023\u003Dz9wx8aZCn12QF()
  {
    return new Vector3(this.dje_z3GBAX47U_ejd, this.dje_zLPL6EZPA_ejd, this.dje_zX8HQ8VV9_ejd);
  }

  public void \u0023\u003Dz3b1ovMxwIEKI(
    Vector3 _param1)
  {
    this.dje_z3GBAX47U_ejd = _param1.dje_z3GBAX47U_ejd;
    this.dje_zLPL6EZPA_ejd = _param1.dje_zLPL6EZPA_ejd;
    this.dje_zX8HQ8VV9_ejd = _param1.dje_zX8HQ8VV9_ejd;
  }

  public static Vector4 operator +(
    Vector4 _param0,
    Vector4 _param1)
  {
    _param0.dje_z3GBAX47U_ejd += _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd += _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd += _param1.dje_zX8HQ8VV9_ejd;
    _param0.dje_z3L8TB878_ejd += _param1.dje_z3L8TB878_ejd;
    return _param0;
  }

  public static Vector4 operator -(
    Vector4 _param0,
    Vector4 _param1)
  {
    _param0.dje_z3GBAX47U_ejd -= _param1.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd -= _param1.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd -= _param1.dje_zX8HQ8VV9_ejd;
    _param0.dje_z3L8TB878_ejd -= _param1.dje_z3L8TB878_ejd;
    return _param0;
  }

  public static Vector4 operator -(
    Vector4 _param0)
  {
    _param0.dje_z3GBAX47U_ejd = -_param0.dje_z3GBAX47U_ejd;
    _param0.dje_zLPL6EZPA_ejd = -_param0.dje_zLPL6EZPA_ejd;
    _param0.dje_zX8HQ8VV9_ejd = -_param0.dje_zX8HQ8VV9_ejd;
    _param0.dje_z3L8TB878_ejd = -_param0.dje_z3L8TB878_ejd;
    return _param0;
  }

  public static Vector4 operator *(
    Vector4 _param0,
    double _param1)
  {
    _param0.dje_z3GBAX47U_ejd *= _param1;
    _param0.dje_zLPL6EZPA_ejd *= _param1;
    _param0.dje_zX8HQ8VV9_ejd *= _param1;
    _param0.dje_z3L8TB878_ejd *= _param1;
    return _param0;
  }

  public static Vector4 operator *(
    double _param0,
    Vector4 _param1)
  {
    _param1.dje_z3GBAX47U_ejd *= _param0;
    _param1.dje_zLPL6EZPA_ejd *= _param0;
    _param1.dje_zX8HQ8VV9_ejd *= _param0;
    _param1.dje_z3L8TB878_ejd *= _param0;
    return _param1;
  }

  public static Vector4 operator /(
    Vector4 _param0,
    double _param1)
  {
    double num = 1.0 / _param1;
    _param0.dje_z3GBAX47U_ejd *= num;
    _param0.dje_zLPL6EZPA_ejd *= num;
    _param0.dje_zX8HQ8VV9_ejd *= num;
    _param0.dje_z3L8TB878_ejd *= num;
    return _param0;
  }

  public static bool operator ==(
    Vector4 _param0,
    Vector4 _param1)
  {
    return _param0.Equals(_param1);
  }

  public static bool operator !=(
    Vector4 _param0,
    Vector4 _param1)
  {
    return !_param0.Equals(_param1);
  }

  public static unsafe explicit operator double*(
    Vector4 _param0)
  {
    return &_param0.dje_z3GBAX47U_ejd;
  }

  public static unsafe explicit operator IntPtr(
    Vector4 _param0)
  {
    return (IntPtr) (void*) &_param0.dje_z3GBAX47U_ejd;
  }

  public override string ToString()
  {
    return $"{this.dje_z3GBAX47U_ejd}, {this.dje_zLPL6EZPA_ejd}, {this.dje_zX8HQ8VV9_ejd}, {this.dje_z3L8TB878_ejd}";
  }

  public string \u0023\u003DzRZOLPRg\u003D(string _param1)
  {
    return $"{this.dje_z3GBAX47U_ejd.ToString(_param1)}, {this.dje_zLPL6EZPA_ejd.ToString(_param1)}, {this.dje_zX8HQ8VV9_ejd.ToString(_param1)}, {this.dje_z3L8TB878_ejd.ToString(_param1)}";
  }

  public override int GetHashCode()
  {
    return new \u0023\u003Dzn0hLvU_EFY4CYrpvjg\u003D\u003D<double, double, double, double>(this.dje_z3GBAX47U_ejd, this.dje_zLPL6EZPA_ejd, this.dje_zX8HQ8VV9_ejd, this.dje_z3L8TB878_ejd).GetHashCode();
  }

  public override bool Equals(object _param1)
  {
    return _param1 is Vector4 bbfK6VhuQ6F9ZEjd && this.Equals(bbfK6VhuQ6F9ZEjd);
  }

  public bool Equals(
    Vector4 _param1)
  {
    return this.dje_z3GBAX47U_ejd == _param1.dje_z3GBAX47U_ejd && this.dje_zLPL6EZPA_ejd == _param1.dje_zLPL6EZPA_ejd && this.dje_zX8HQ8VV9_ejd == _param1.dje_zX8HQ8VV9_ejd && this.dje_z3L8TB878_ejd == _param1.dje_z3L8TB878_ejd;
  }
}
