// Decompiled with JetBrains decompiler
// Type: #=z5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB$RrBBDXLSRoXVhBAYRK2CMZOKas
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;

#nullable disable
internal struct \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas(
  int _param1,
  int _param2,
  int _param3,
  int _param4)
{
  
  public int \u0023\u003DzP4R7yU0\u003D = _param1;
  
  public int \u0023\u003DzRNV_Dpk\u003D = _param2;
  
  public int \u0023\u003Dzp55dtus\u003D = _param3;
  
  public int \u0023\u003DzSzOWcj8\u003D = _param4;

  public void \u0023\u003DzkTP\u0024FzM\u003D(int _param1, int _param2, int _param3, int _param4)
  {
    this.\u0023\u003Dz0gbwL\u00244\u003D(_param1, _param2, _param3, _param4);
  }

  public void \u0023\u003Dz0gbwL\u00244\u003D(int _param1, int _param2, int _param3, int _param4)
  {
    this.\u0023\u003DzP4R7yU0\u003D = _param1;
    this.\u0023\u003DzRNV_Dpk\u003D = _param2;
    this.\u0023\u003Dzp55dtus\u003D = _param3;
    this.\u0023\u003DzSzOWcj8\u003D = _param4;
  }

  public int Width => this.\u0023\u003Dzp55dtus\u003D - this.\u0023\u003DzP4R7yU0\u003D;

  public int Height => this.\u0023\u003DzSzOWcj8\u003D - this.\u0023\u003DzRNV_Dpk\u003D;

  public \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas \u0023\u003DzO3\u0024NMwQ\u003D()
  {
    if (this.\u0023\u003DzP4R7yU0\u003D > this.\u0023\u003Dzp55dtus\u003D)
    {
      int zP4R7yU0 = this.\u0023\u003DzP4R7yU0\u003D;
      this.\u0023\u003DzP4R7yU0\u003D = this.\u0023\u003Dzp55dtus\u003D;
      this.\u0023\u003Dzp55dtus\u003D = zP4R7yU0;
    }
    if (this.\u0023\u003DzRNV_Dpk\u003D > this.\u0023\u003DzSzOWcj8\u003D)
    {
      int zRnvDpk = this.\u0023\u003DzRNV_Dpk\u003D;
      this.\u0023\u003DzRNV_Dpk\u003D = this.\u0023\u003DzSzOWcj8\u003D;
      this.\u0023\u003DzSzOWcj8\u003D = zRnvDpk;
    }
    return this;
  }

  public bool \u0023\u003DzPHB5nPY\u003D(
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1)
  {
    if (this.\u0023\u003Dzp55dtus\u003D > _param1.\u0023\u003Dzp55dtus\u003D)
      this.\u0023\u003Dzp55dtus\u003D = _param1.\u0023\u003Dzp55dtus\u003D;
    if (this.\u0023\u003DzSzOWcj8\u003D > _param1.\u0023\u003DzSzOWcj8\u003D)
      this.\u0023\u003DzSzOWcj8\u003D = _param1.\u0023\u003DzSzOWcj8\u003D;
    if (this.\u0023\u003DzP4R7yU0\u003D < _param1.\u0023\u003DzP4R7yU0\u003D)
      this.\u0023\u003DzP4R7yU0\u003D = _param1.\u0023\u003DzP4R7yU0\u003D;
    if (this.\u0023\u003DzRNV_Dpk\u003D < _param1.\u0023\u003DzRNV_Dpk\u003D)
      this.\u0023\u003DzRNV_Dpk\u003D = _param1.\u0023\u003DzRNV_Dpk\u003D;
    return this.\u0023\u003DzP4R7yU0\u003D <= this.\u0023\u003Dzp55dtus\u003D && this.\u0023\u003DzRNV_Dpk\u003D <= this.\u0023\u003DzSzOWcj8\u003D;
  }

  public bool \u0023\u003DzTQw5DGfvJbLF()
  {
    return this.\u0023\u003DzP4R7yU0\u003D <= this.\u0023\u003Dzp55dtus\u003D && this.\u0023\u003DzRNV_Dpk\u003D <= this.\u0023\u003DzSzOWcj8\u003D;
  }

  public bool \u0023\u003Dzc7hJRHEN9Ch4(int _param1, int _param2)
  {
    return _param1 >= this.\u0023\u003DzP4R7yU0\u003D && _param1 <= this.\u0023\u003Dzp55dtus\u003D && _param2 >= this.\u0023\u003DzRNV_Dpk\u003D && _param2 <= this.\u0023\u003DzSzOWcj8\u003D;
  }

  public bool \u0023\u003DzvBhLthYerYntQLEPioo2lZw\u003D(
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param2)
  {
    this.\u0023\u003DzP4R7yU0\u003D = _param1.\u0023\u003DzP4R7yU0\u003D;
    this.\u0023\u003DzRNV_Dpk\u003D = _param1.\u0023\u003DzRNV_Dpk\u003D;
    this.\u0023\u003Dzp55dtus\u003D = _param1.\u0023\u003Dzp55dtus\u003D;
    this.\u0023\u003DzSzOWcj8\u003D = _param1.\u0023\u003DzSzOWcj8\u003D;
    if (this.\u0023\u003DzP4R7yU0\u003D < _param2.\u0023\u003DzP4R7yU0\u003D)
      this.\u0023\u003DzP4R7yU0\u003D = _param2.\u0023\u003DzP4R7yU0\u003D;
    if (this.\u0023\u003DzRNV_Dpk\u003D < _param2.\u0023\u003DzRNV_Dpk\u003D)
      this.\u0023\u003DzRNV_Dpk\u003D = _param2.\u0023\u003DzRNV_Dpk\u003D;
    if (this.\u0023\u003Dzp55dtus\u003D > _param2.\u0023\u003Dzp55dtus\u003D)
      this.\u0023\u003Dzp55dtus\u003D = _param2.\u0023\u003Dzp55dtus\u003D;
    if (this.\u0023\u003DzSzOWcj8\u003D > _param2.\u0023\u003DzSzOWcj8\u003D)
      this.\u0023\u003DzSzOWcj8\u003D = _param2.\u0023\u003DzSzOWcj8\u003D;
    return this.\u0023\u003DzP4R7yU0\u003D < this.\u0023\u003Dzp55dtus\u003D && this.\u0023\u003DzRNV_Dpk\u003D < this.\u0023\u003DzSzOWcj8\u003D;
  }

  public bool \u0023\u003DzKkTE5vIDCYcx(
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1)
  {
    if (this.\u0023\u003DzP4R7yU0\u003D < _param1.\u0023\u003DzP4R7yU0\u003D)
      this.\u0023\u003DzP4R7yU0\u003D = _param1.\u0023\u003DzP4R7yU0\u003D;
    if (this.\u0023\u003DzRNV_Dpk\u003D < _param1.\u0023\u003DzRNV_Dpk\u003D)
      this.\u0023\u003DzRNV_Dpk\u003D = _param1.\u0023\u003DzRNV_Dpk\u003D;
    if (this.\u0023\u003Dzp55dtus\u003D > _param1.\u0023\u003Dzp55dtus\u003D)
      this.\u0023\u003Dzp55dtus\u003D = _param1.\u0023\u003Dzp55dtus\u003D;
    if (this.\u0023\u003DzSzOWcj8\u003D > _param1.\u0023\u003DzSzOWcj8\u003D)
      this.\u0023\u003DzSzOWcj8\u003D = _param1.\u0023\u003DzSzOWcj8\u003D;
    return this.\u0023\u003DzP4R7yU0\u003D < this.\u0023\u003Dzp55dtus\u003D && this.\u0023\u003DzRNV_Dpk\u003D < this.\u0023\u003DzSzOWcj8\u003D;
  }

  public static bool \u0023\u003DzAWAn_7okcxAr(
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param0,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1)
  {
    int zP4R7yU0 = _param0.\u0023\u003DzP4R7yU0\u003D;
    int zRnvDpk = _param0.\u0023\u003DzRNV_Dpk\u003D;
    int zp55dtus = _param0.\u0023\u003Dzp55dtus\u003D;
    int zSzOwcj8 = _param0.\u0023\u003DzSzOWcj8\u003D;
    if (zP4R7yU0 < _param1.\u0023\u003DzP4R7yU0\u003D)
      zP4R7yU0 = _param1.\u0023\u003DzP4R7yU0\u003D;
    if (zRnvDpk < _param1.\u0023\u003DzRNV_Dpk\u003D)
      zRnvDpk = _param1.\u0023\u003DzRNV_Dpk\u003D;
    if (zp55dtus > _param1.\u0023\u003Dzp55dtus\u003D)
      zp55dtus = _param1.\u0023\u003Dzp55dtus\u003D;
    if (zSzOwcj8 > _param1.\u0023\u003DzSzOWcj8\u003D)
      zSzOwcj8 = _param1.\u0023\u003DzSzOWcj8\u003D;
    return zP4R7yU0 < zp55dtus && zRnvDpk < zSzOwcj8;
  }

  public void \u0023\u003DzoIcYXQymsTCuibRM\u0024AbsiBA4MTxA(
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param2)
  {
    this.\u0023\u003DzP4R7yU0\u003D = _param1.\u0023\u003DzP4R7yU0\u003D;
    this.\u0023\u003DzRNV_Dpk\u003D = _param1.\u0023\u003DzRNV_Dpk\u003D;
    this.\u0023\u003Dzp55dtus\u003D = _param1.\u0023\u003Dzp55dtus\u003D;
    this.\u0023\u003Dzp55dtus\u003D = _param1.\u0023\u003DzSzOWcj8\u003D;
    if (this.\u0023\u003Dzp55dtus\u003D < _param2.\u0023\u003Dzp55dtus\u003D)
      this.\u0023\u003Dzp55dtus\u003D = _param2.\u0023\u003Dzp55dtus\u003D;
    if (this.\u0023\u003DzSzOWcj8\u003D < _param2.\u0023\u003DzSzOWcj8\u003D)
      this.\u0023\u003DzSzOWcj8\u003D = _param2.\u0023\u003DzSzOWcj8\u003D;
    if (this.\u0023\u003DzP4R7yU0\u003D > _param2.\u0023\u003DzP4R7yU0\u003D)
      this.\u0023\u003DzP4R7yU0\u003D = _param2.\u0023\u003DzP4R7yU0\u003D;
    if (this.\u0023\u003DzRNV_Dpk\u003D <= _param2.\u0023\u003DzRNV_Dpk\u003D)
      return;
    this.\u0023\u003DzRNV_Dpk\u003D = _param2.\u0023\u003DzRNV_Dpk\u003D;
  }

  public void \u0023\u003DzfONrQAdv_Dzz(int _param1)
  {
    this.\u0023\u003DzP4R7yU0\u003D -= _param1;
    this.\u0023\u003DzRNV_Dpk\u003D -= _param1;
    this.\u0023\u003Dzp55dtus\u003D += _param1;
    this.\u0023\u003DzSzOWcj8\u003D += _param1;
  }

  public void \u0023\u003DznR9\u00242Eg\u003D(int _param1, int _param2)
  {
    this.\u0023\u003DzP4R7yU0\u003D += _param1;
    this.\u0023\u003DzRNV_Dpk\u003D += _param2;
    this.\u0023\u003Dzp55dtus\u003D += _param1;
    this.\u0023\u003DzSzOWcj8\u003D += _param2;
  }

  public static bool \u0023\u003DzKzzHL6MOiRdk(
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param0,
    ref \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1,
    ref \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param2)
  {
    if (_param2.\u0023\u003DzSzOWcj8\u003D < _param0.\u0023\u003DzSzOWcj8\u003D)
    {
      if (_param1.Height != _param2.Height)
        throw new Exception("");
      _param1.\u0023\u003DzSzOWcj8\u003D += _param0.\u0023\u003DzSzOWcj8\u003D - _param2.\u0023\u003DzSzOWcj8\u003D;
      _param2.\u0023\u003DzSzOWcj8\u003D = _param0.\u0023\u003DzSzOWcj8\u003D;
      if (_param2.\u0023\u003DzSzOWcj8\u003D >= _param2.\u0023\u003DzRNV_Dpk\u003D)
        return false;
    }
    if (_param2.\u0023\u003DzRNV_Dpk\u003D > _param0.\u0023\u003DzRNV_Dpk\u003D)
    {
      if (_param1.Height != _param2.Height)
        throw new Exception("");
      _param1.\u0023\u003DzRNV_Dpk\u003D -= _param2.\u0023\u003DzRNV_Dpk\u003D - _param0.\u0023\u003DzRNV_Dpk\u003D;
      _param2.\u0023\u003DzRNV_Dpk\u003D = _param0.\u0023\u003DzRNV_Dpk\u003D;
      if (_param2.\u0023\u003DzRNV_Dpk\u003D <= _param2.\u0023\u003DzSzOWcj8\u003D)
        return false;
    }
    if (_param2.\u0023\u003DzP4R7yU0\u003D < _param0.\u0023\u003DzP4R7yU0\u003D)
    {
      if (_param1.Width != _param2.Width)
        throw new Exception("");
      _param1.\u0023\u003DzP4R7yU0\u003D += _param0.\u0023\u003DzP4R7yU0\u003D - _param2.\u0023\u003DzP4R7yU0\u003D;
      _param2.\u0023\u003DzP4R7yU0\u003D = _param0.\u0023\u003DzP4R7yU0\u003D;
      if (_param2.\u0023\u003DzP4R7yU0\u003D >= _param2.\u0023\u003Dzp55dtus\u003D)
        return false;
    }
    if (_param2.\u0023\u003Dzp55dtus\u003D > _param0.\u0023\u003Dzp55dtus\u003D)
    {
      if (_param1.Width != _param2.Width)
        throw new Exception("");
      _param1.\u0023\u003Dzp55dtus\u003D -= _param2.\u0023\u003Dzp55dtus\u003D - _param0.\u0023\u003Dzp55dtus\u003D;
      _param2.\u0023\u003Dzp55dtus\u003D = _param0.\u0023\u003Dzp55dtus\u003D;
      if (_param2.\u0023\u003Dzp55dtus\u003D <= _param2.\u0023\u003DzP4R7yU0\u003D)
        return false;
    }
    return true;
  }

  public static bool \u0023\u003DzmqCCXUaWzfQg(
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param0,
    ref \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1)
  {
    if (_param1.\u0023\u003DzSzOWcj8\u003D < _param0.\u0023\u003DzSzOWcj8\u003D)
    {
      _param1.\u0023\u003DzSzOWcj8\u003D = _param0.\u0023\u003DzSzOWcj8\u003D;
      if (_param1.\u0023\u003DzSzOWcj8\u003D >= _param1.\u0023\u003DzRNV_Dpk\u003D)
        return false;
    }
    if (_param1.\u0023\u003DzRNV_Dpk\u003D > _param0.\u0023\u003DzRNV_Dpk\u003D)
    {
      _param1.\u0023\u003DzRNV_Dpk\u003D = _param0.\u0023\u003DzRNV_Dpk\u003D;
      if (_param1.\u0023\u003DzRNV_Dpk\u003D <= _param1.\u0023\u003DzSzOWcj8\u003D)
        return false;
    }
    if (_param1.\u0023\u003DzP4R7yU0\u003D < _param0.\u0023\u003DzP4R7yU0\u003D)
    {
      _param1.\u0023\u003DzP4R7yU0\u003D = _param0.\u0023\u003DzP4R7yU0\u003D;
      if (_param1.\u0023\u003DzP4R7yU0\u003D >= _param1.\u0023\u003Dzp55dtus\u003D)
        return false;
    }
    if (_param1.\u0023\u003Dzp55dtus\u003D > _param0.\u0023\u003Dzp55dtus\u003D)
    {
      _param1.\u0023\u003Dzp55dtus\u003D = _param0.\u0023\u003Dzp55dtus\u003D;
      if (_param1.\u0023\u003Dzp55dtus\u003D <= _param1.\u0023\u003DzP4R7yU0\u003D)
        return false;
    }
    return true;
  }
}
