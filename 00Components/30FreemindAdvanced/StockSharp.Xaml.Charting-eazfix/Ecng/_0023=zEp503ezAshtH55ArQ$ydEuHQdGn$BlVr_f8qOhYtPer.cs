// Decompiled with JetBrains decompiler
// Type: #=zEp503ezAshtH55ArQ$ydEuHQdGn$BlVr_f8qOhYtPerHvaqyP1QAjNMMJBAJ
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;

#nullable disable
public struct RectangleDouble
{
  
  public double \u0023\u003DzP4R7yU0\u003D;
  
  public double \u0023\u003DzRNV_Dpk\u003D;
  
  public double \u0023\u003Dzp55dtus\u003D;
  
  public double \u0023\u003DzSzOWcj8\u003D;
  
  public static readonly RectangleDouble \u0023\u003DzISNDksKSYcjC = new RectangleDouble(double.MaxValue, double.MaxValue, double.MinValue, double.MinValue);

  public RectangleDouble(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003DzP4R7yU0\u003D = _param1;
    this.\u0023\u003DzRNV_Dpk\u003D = _param2;
    this.\u0023\u003Dzp55dtus\u003D = _param3;
    this.\u0023\u003DzSzOWcj8\u003D = _param4;
  }

  public RectangleDouble(
    RectangleInt _param1)
  {
    this.\u0023\u003DzP4R7yU0\u003D = (double) _param1.\u0023\u003DzP4R7yU0\u003D;
    this.\u0023\u003DzRNV_Dpk\u003D = (double) _param1.\u0023\u003DzRNV_Dpk\u003D;
    this.\u0023\u003Dzp55dtus\u003D = (double) _param1.\u0023\u003Dzp55dtus\u003D;
    this.\u0023\u003DzSzOWcj8\u003D = (double) _param1.\u0023\u003DzSzOWcj8\u003D;
  }

  public void \u0023\u003DzkTP\u0024FzM\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003Dz0gbwL\u00244\u003D(_param1, _param2, _param3, _param4);
  }

  public static bool operator ==(
    RectangleDouble _param0,
    RectangleDouble _param1)
  {
    return _param0.\u0023\u003DzP4R7yU0\u003D == _param1.\u0023\u003DzP4R7yU0\u003D && _param0.\u0023\u003DzRNV_Dpk\u003D == _param1.\u0023\u003DzRNV_Dpk\u003D && _param0.\u0023\u003Dzp55dtus\u003D == _param1.\u0023\u003Dzp55dtus\u003D && _param0.\u0023\u003DzSzOWcj8\u003D == _param1.\u0023\u003DzSzOWcj8\u003D;
  }

  public static bool operator !=(
    RectangleDouble _param0,
    RectangleDouble _param1)
  {
    return _param0.\u0023\u003DzP4R7yU0\u003D != _param1.\u0023\u003DzP4R7yU0\u003D || _param0.\u0023\u003DzRNV_Dpk\u003D != _param1.\u0023\u003DzRNV_Dpk\u003D || _param0.\u0023\u003Dzp55dtus\u003D != _param1.\u0023\u003Dzp55dtus\u003D || _param0.\u0023\u003DzSzOWcj8\u003D != _param1.\u0023\u003DzSzOWcj8\u003D;
  }

  public override int GetHashCode()
  {
    return new \u0023\u003DzNGmTkojZHEZZcN1OLQ\u003D\u003D<double, double, double, double>(this.\u0023\u003DzP4R7yU0\u003D, this.\u0023\u003Dzp55dtus\u003D, this.\u0023\u003DzRNV_Dpk\u003D, this.\u0023\u003DzSzOWcj8\u003D).GetHashCode();
  }

  public override bool Equals(object _param1)
  {
    return _param1.GetType() == typeof (RectangleDouble) && this == (RectangleDouble) _param1;
  }

  public void \u0023\u003Dz0gbwL\u00244\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003DzP4R7yU0\u003D = _param1;
    this.\u0023\u003DzRNV_Dpk\u003D = _param2;
    this.\u0023\u003Dzp55dtus\u003D = _param3;
    this.\u0023\u003DzSzOWcj8\u003D = _param4;
  }

  public double Width => this.\u0023\u003Dzp55dtus\u003D - this.\u0023\u003DzP4R7yU0\u003D;

  public double Height => this.\u0023\u003DzSzOWcj8\u003D - this.\u0023\u003DzRNV_Dpk\u003D;

  public RectangleDouble \u0023\u003DzO3\u0024NMwQ\u003D()
  {
    if (this.\u0023\u003DzP4R7yU0\u003D > this.\u0023\u003Dzp55dtus\u003D)
    {
      double zP4R7yU0 = this.\u0023\u003DzP4R7yU0\u003D;
      this.\u0023\u003DzP4R7yU0\u003D = this.\u0023\u003Dzp55dtus\u003D;
      this.\u0023\u003Dzp55dtus\u003D = zP4R7yU0;
    }
    if (this.\u0023\u003DzRNV_Dpk\u003D > this.\u0023\u003DzSzOWcj8\u003D)
    {
      double zRnvDpk = this.\u0023\u003DzRNV_Dpk\u003D;
      this.\u0023\u003DzRNV_Dpk\u003D = this.\u0023\u003DzSzOWcj8\u003D;
      this.\u0023\u003DzSzOWcj8\u003D = zRnvDpk;
    }
    return this;
  }

  public bool \u0023\u003DzPHB5nPY\u003D(
    RectangleDouble _param1)
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

  public bool \u0023\u003DzOEi94Z4\u003D(double _param1, double _param2)
  {
    return _param1 >= this.\u0023\u003DzP4R7yU0\u003D && _param1 <= this.\u0023\u003Dzp55dtus\u003D && _param2 >= this.\u0023\u003DzRNV_Dpk\u003D && _param2 <= this.\u0023\u003DzSzOWcj8\u003D;
  }

  public bool \u0023\u003DzOEi94Z4\u003D(
    RectangleDouble _param1)
  {
    return this.\u0023\u003DzOEi94Z4\u003D(_param1.\u0023\u003DzP4R7yU0\u003D, _param1.\u0023\u003DzRNV_Dpk\u003D) && this.\u0023\u003DzOEi94Z4\u003D(_param1.\u0023\u003Dzp55dtus\u003D, _param1.\u0023\u003DzSzOWcj8\u003D);
  }

  public bool \u0023\u003DzOEi94Z4\u003D(
    Vector2 _param1)
  {
    return this.\u0023\u003DzOEi94Z4\u003D(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd);
  }

  public bool \u0023\u003DzvBhLthYerYntQLEPioo2lZw\u003D(
    RectangleDouble _param1,
    RectangleDouble _param2)
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
    RectangleDouble _param1)
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

  public void \u0023\u003DzoIcYXQymsTCuibRM\u0024AbsiBA4MTxA(
    RectangleDouble _param1,
    RectangleDouble _param2)
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

  public void \u0023\u003DzIM0oMSVPmJpe(
    RectangleDouble _param1)
  {
    if (this.\u0023\u003Dzp55dtus\u003D < _param1.\u0023\u003Dzp55dtus\u003D)
      this.\u0023\u003Dzp55dtus\u003D = _param1.\u0023\u003Dzp55dtus\u003D;
    if (this.\u0023\u003DzSzOWcj8\u003D < _param1.\u0023\u003DzSzOWcj8\u003D)
      this.\u0023\u003DzSzOWcj8\u003D = _param1.\u0023\u003DzSzOWcj8\u003D;
    if (this.\u0023\u003DzP4R7yU0\u003D > _param1.\u0023\u003DzP4R7yU0\u003D)
      this.\u0023\u003DzP4R7yU0\u003D = _param1.\u0023\u003DzP4R7yU0\u003D;
    if (this.\u0023\u003DzRNV_Dpk\u003D <= _param1.\u0023\u003DzRNV_Dpk\u003D)
      return;
    this.\u0023\u003DzRNV_Dpk\u003D = _param1.\u0023\u003DzRNV_Dpk\u003D;
  }

  public void \u0023\u003DzIM0oMSVPmJpe(double _param1, double _param2)
  {
    if (this.\u0023\u003Dzp55dtus\u003D < _param1)
      this.\u0023\u003Dzp55dtus\u003D = _param1;
    if (this.\u0023\u003DzSzOWcj8\u003D < _param2)
      this.\u0023\u003DzSzOWcj8\u003D = _param2;
    if (this.\u0023\u003DzP4R7yU0\u003D > _param1)
      this.\u0023\u003DzP4R7yU0\u003D = _param1;
    if (this.\u0023\u003DzRNV_Dpk\u003D <= _param2)
      return;
    this.\u0023\u003DzRNV_Dpk\u003D = _param2;
  }

  public void \u0023\u003DzfONrQAdv_Dzz(double _param1)
  {
    this.\u0023\u003DzP4R7yU0\u003D -= _param1;
    this.\u0023\u003DzRNV_Dpk\u003D -= _param1;
    this.\u0023\u003Dzp55dtus\u003D += _param1;
    this.\u0023\u003DzSzOWcj8\u003D += _param1;
  }

  public void \u0023\u003DznR9\u00242Eg\u003D(
    Vector2 _param1)
  {
    this.\u0023\u003DznR9\u00242Eg\u003D(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd);
  }

  public void \u0023\u003DznR9\u00242Eg\u003D(double _param1, double _param2)
  {
    this.\u0023\u003DzP4R7yU0\u003D += _param1;
    this.\u0023\u003DzRNV_Dpk\u003D += _param2;
    this.\u0023\u003Dzp55dtus\u003D += _param1;
    this.\u0023\u003DzSzOWcj8\u003D += _param2;
  }

  public static RectangleDouble operator *(
    RectangleDouble _param0,
    double _param1)
  {
    return new RectangleDouble(_param0.\u0023\u003DzP4R7yU0\u003D * _param1, _param0.\u0023\u003DzRNV_Dpk\u003D * _param1, _param0.\u0023\u003Dzp55dtus\u003D * _param1, _param0.\u0023\u003DzSzOWcj8\u003D * _param1);
  }

  public static RectangleDouble operator *(
    double _param0,
    RectangleDouble _param1)
  {
    return new RectangleDouble(_param1.\u0023\u003DzP4R7yU0\u003D * _param0, _param1.\u0023\u003DzRNV_Dpk\u003D * _param0, _param1.\u0023\u003Dzp55dtus\u003D * _param0, _param1.\u0023\u003DzSzOWcj8\u003D * _param0);
  }

  public double \u0023\u003Dzr77tDiSUrgME()
  {
    return (this.\u0023\u003Dzp55dtus\u003D - this.\u0023\u003DzP4R7yU0\u003D) / 2.0;
  }

  public void \u0023\u003DzfONrQAdv_Dzz(
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYqc9paYUOELp42Wm9LewSjdeS3Z7h7fzLONw1_qb _param1)
  {
    this.\u0023\u003DzP4R7yU0\u003D -= _param1.\u0023\u003DzP4R7yU0\u003D;
    this.\u0023\u003Dzp55dtus\u003D += _param1.\u0023\u003Dzp55dtus\u003D;
    this.\u0023\u003DzRNV_Dpk\u003D -= _param1.\u0023\u003DzRNV_Dpk\u003D;
    this.\u0023\u003DzSzOWcj8\u003D += _param1.\u0023\u003DzSzOWcj8\u003D;
  }
}
