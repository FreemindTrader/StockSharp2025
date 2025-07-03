// Decompiled with JetBrains decompiler
// Type: #=zdJvCkWEpdQt1kv1Y55xjvXUD$c40UHSqnHjJIKlCXmEZS_a9vc2bs90=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvXUD\u0024c40UHSqnHjJIKlCXmEZS_a9vc2bs90\u003D
{
  private readonly double \u0023\u003DzpYI5BlRzFVM9Y0pOKA\u003D\u003D;
  private readonly IAxis \u0023\u003DzLXQXNXQ\u003D;
  private readonly double \u0023\u003DzJpbCbio\u003D;
  private double \u0023\u003DzApIg1NM\u003D;
  private double \u0023\u003DzbYn_88U\u003D;

  public \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvXUD\u0024c40UHSqnHjJIKlCXmEZS_a9vc2bs90\u003D(
    IAxis _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003DzLXQXNXQ\u003D = _param1;
    this.\u0023\u003DzJpbCbio\u003D = _param2;
    this.\u0023\u003DzpYI5BlRzFVM9Y0pOKA\u003D\u003D = _param2 > 0.0 ? _param3 / _param2 : 0.0;
    this.\u0023\u003Dz7dZR55k\u003D(_param1.VisibleRange);
  }

  public \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvXUD\u0024c40UHSqnHjJIKlCXmEZS_a9vc2bs90\u003D(
    IAxis _param1,
    double _param2)
    : this(_param1, _param2, 0.0)
  {
  }

  public double \u0023\u003Dzu79DeCo\u003D() => this.\u0023\u003DzApIg1NM\u003D;

  public double \u0023\u003Dzs8tfezEAVTzJ() => this.\u0023\u003DzbYn_88U\u003D;

  public double \u0023\u003DzXvzOhjLoNdjd()
  {
    return Math.Max(this.\u0023\u003DzApIg1NM\u003D * this.\u0023\u003DzJpbCbio\u003D, 0.0).\u0023\u003DzZsq6ZfbZQvsf();
  }

  public double \u0023\u003DzTI3RDYmCozNk()
  {
    return Math.Max((1.0 - this.\u0023\u003DzbYn_88U\u003D) * this.\u0023\u003DzJpbCbio\u003D, 0.0).\u0023\u003DzZsq6ZfbZQvsf();
  }

  public void \u0023\u003Dz7dZR55k\u003D(
    IRange _param1)
  {
    if (_param1 == null)
    {
      this.\u0023\u003DzD9Yw\u0024iTKpgBA();
    }
    else
    {
      DoubleRange klqcJ87Zm8UwE3WEjd1 = _param1.AsDoubleRange();
      IRange abyLt9clZggmJsWhw = this.\u0023\u003DzLXQXNXQ\u003D.get_DataRange();
      if (abyLt9clZggmJsWhw == null)
      {
        this.\u0023\u003DzD9Yw\u0024iTKpgBA();
      }
      else
      {
        if (this.\u0023\u003DzLXQXNXQ\u003D.GrowBy != null)
          abyLt9clZggmJsWhw = abyLt9clZggmJsWhw.GrowBy(this.\u0023\u003DzLXQXNXQ\u003D.GrowBy.Min, this.\u0023\u003DzLXQXNXQ\u003D.GrowBy.Max);
        DoubleRange klqcJ87Zm8UwE3WEjd2 = abyLt9clZggmJsWhw.AsDoubleRange();
        double diff = klqcJ87Zm8UwE3WEjd2.Diff;
        if (diff.CompareTo(0.0) == 0)
          this.\u0023\u003DzD9Yw\u0024iTKpgBA();
        else
          this.\u0023\u003DzmTUBEm_gfEtH(klqcJ87Zm8UwE3WEjd2, klqcJ87Zm8UwE3WEjd1, diff);
      }
    }
  }

  private void \u0023\u003DzD9Yw\u0024iTKpgBA()
  {
    this.\u0023\u003DzApIg1NM\u003D = 0.0;
    this.\u0023\u003DzbYn_88U\u003D = 1.0;
  }

  private void \u0023\u003DzmTUBEm_gfEtH(
    DoubleRange _param1,
    DoubleRange _param2,
    double _param3)
  {
    double num1;
    double num2;
    if (this.\u0023\u003DzLXQXNXQ\u003D.get_IsLogarithmicAxis())
    {
      double logarithmicBase = ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this.\u0023\u003DzLXQXNXQ\u003D).get_LogarithmicBase();
      double num3 = Math.Log(_param1.Max, logarithmicBase) - Math.Log(_param1.Min, logarithmicBase);
      double num4 = Math.Log(_param2.Min, logarithmicBase) - Math.Log(_param1.Min, logarithmicBase);
      double num5 = Math.Log(_param2.Max, logarithmicBase) - Math.Log(_param1.Min, logarithmicBase);
      num1 = num4 / num3;
      double num6 = num3;
      num2 = num5 / num6;
    }
    else
    {
      num1 = (_param2.Min - _param1.Min) / _param3;
      num2 = (_param2.Max - _param1.Min) / _param3;
    }
    this.\u0023\u003DzkX7okADmmcqj(ref num1, ref num2);
    if (this.\u0023\u003DzLXQXNXQ\u003D.get_FlipCoordinates() ^ !this.\u0023\u003DzLXQXNXQ\u003D.\u0023\u003DzFrVmckt\u0024NpG6())
    {
      this.\u0023\u003DzApIg1NM\u003D = 1.0 - num2;
      this.\u0023\u003DzbYn_88U\u003D = 1.0 - num1;
    }
    else
    {
      this.\u0023\u003DzApIg1NM\u003D = num1;
      this.\u0023\u003DzbYn_88U\u003D = num2;
    }
  }

  private void \u0023\u003DzkX7okADmmcqj(ref double _param1, ref double _param2)
  {
    _param1 = \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D(_param1, 0.0, 1.0);
    _param2 = \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D(_param2, 0.0, 1.0);
    if (Math.Abs(_param2 - _param1) < this.\u0023\u003DzpYI5BlRzFVM9Y0pOKA\u003D\u003D)
    {
      double num1 = _param1 + (_param2 - _param1) / 2.0;
      double num2 = this.\u0023\u003DzpYI5BlRzFVM9Y0pOKA\u003D\u003D / 2.0;
      _param1 = num1 - num2;
      _param2 = num1 + num2;
    }
    if (_param2 > 1.0)
    {
      double num = _param2 - 1.0;
      _param2 -= num;
      _param1 -= num;
    }
    else
    {
      if (_param1 >= 0.0)
        return;
      double num = 0.0 - _param1;
      _param2 += num;
      _param1 += num;
    }
  }

  public IRange \u0023\u003DzfRDRUq8\u003D(
    double _param1)
  {
    double num1 = (this.\u0023\u003DzApIg1NM\u003D + (this.\u0023\u003DzbYn_88U\u003D - this.\u0023\u003DzApIg1NM\u003D) / 2.0) * this.\u0023\u003DzJpbCbio\u003D;
    double num2 = _param1 - num1;
    return this.\u0023\u003Dz7FKHKl8\u003D(num2, num2);
  }

  public IRange \u0023\u003Dz7FKHKl8\u003D(
    double _param1,
    double _param2)
  {
    double num1 = _param1 / this.\u0023\u003DzJpbCbio\u003D;
    double num2 = _param2 / this.\u0023\u003DzJpbCbio\u003D;
    double num3 = this.\u0023\u003DzApIg1NM\u003D + num1;
    double num4 = this.\u0023\u003DzbYn_88U\u003D + num2;
    bool flag1 = Math.Abs(_param1) > double.Epsilon;
    bool flag2 = Math.Abs(_param2) > double.Epsilon;
    if (flag1 & flag2)
    {
      num3 = this.\u0023\u003DzApIg1NM\u003D + this.\u0023\u003Dz2mPNUMbn8s2y(num1);
      num4 = this.\u0023\u003DzbYn_88U\u003D + this.\u0023\u003Dz2mPNUMbn8s2y(num2);
    }
    else if (flag1)
      num3 = \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D(num3, 0.0, this.\u0023\u003DzbYn_88U\u003D - this.\u0023\u003DzpYI5BlRzFVM9Y0pOKA\u003D\u003D);
    else
      num4 = \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D(num4, this.\u0023\u003DzApIg1NM\u003D + this.\u0023\u003DzpYI5BlRzFVM9Y0pOKA\u003D\u003D, 1.0);
    this.\u0023\u003DzApIg1NM\u003D = num3;
    this.\u0023\u003DzbYn_88U\u003D = num4;
    return this.\u0023\u003DzZByBk4zFcJKy();
  }

  private double \u0023\u003Dz2mPNUMbn8s2y(double _param1)
  {
    if (_param1 < 0.0 && Math.Abs(_param1) > this.\u0023\u003DzApIg1NM\u003D)
      _param1 = -this.\u0023\u003DzApIg1NM\u003D;
    if (_param1 > 1.0 - this.\u0023\u003DzbYn_88U\u003D)
      _param1 = 1.0 - this.\u0023\u003DzbYn_88U\u003D;
    return _param1;
  }

  public IRange \u0023\u003DzZByBk4zFcJKy()
  {
    IRange abyLt9clZggmJsWhw1 = (IRange) null;
    if (this.\u0023\u003DzLXQXNXQ\u003D != null && this.\u0023\u003DzLXQXNXQ\u003D.VisibleRange != null)
    {
      IRange abyLt9clZggmJsWhw2 = this.\u0023\u003DzLXQXNXQ\u003D.get_DataRange();
      if (abyLt9clZggmJsWhw2 != null)
      {
        if (this.\u0023\u003DzLXQXNXQ\u003D.GrowBy != null)
          abyLt9clZggmJsWhw2 = abyLt9clZggmJsWhw2.GrowBy(this.\u0023\u003DzLXQXNXQ\u003D.GrowBy.Min, this.\u0023\u003DzLXQXNXQ\u003D.GrowBy.Max);
        DoubleRange klqcJ87Zm8UwE3WEjd = abyLt9clZggmJsWhw2.AsDoubleRange();
        double diff = klqcJ87Zm8UwE3WEjd.Diff;
        double num1;
        double num2;
        if (this.\u0023\u003DzLXQXNXQ\u003D.get_FlipCoordinates() ^ !this.\u0023\u003DzLXQXNXQ\u003D.\u0023\u003DzFrVmckt\u0024NpG6())
        {
          num1 = 1.0 - this.\u0023\u003DzbYn_88U\u003D;
          num2 = 1.0 - this.\u0023\u003DzApIg1NM\u003D;
        }
        else
        {
          num2 = this.\u0023\u003DzbYn_88U\u003D;
          num1 = this.\u0023\u003DzApIg1NM\u003D;
        }
        double num3;
        double num4;
        if (this.\u0023\u003DzLXQXNXQ\u003D.get_IsLogarithmicAxis())
        {
          double logarithmicBase = ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this.\u0023\u003DzLXQXNXQ\u003D).get_LogarithmicBase();
          double num5 = Math.Log(klqcJ87Zm8UwE3WEjd.Max, logarithmicBase) - Math.Log(klqcJ87Zm8UwE3WEjd.Min, logarithmicBase);
          double num6 = num1 * num5;
          double num7 = num2 * num5;
          double num8 = Math.Log(klqcJ87Zm8UwE3WEjd.Min, logarithmicBase);
          num3 = Math.Pow(logarithmicBase, num6 + num8);
          num4 = Math.Pow(logarithmicBase, num7 + num8);
        }
        else
        {
          num3 = num1 * diff + klqcJ87Zm8UwE3WEjd.Min;
          num4 = num2 * diff + klqcJ87Zm8UwE3WEjd.Min;
        }
        if (num3 < num4 && num3.IsFiniteNumber() && num4.IsFiniteNumber())
          abyLt9clZggmJsWhw1 = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(this.\u0023\u003DzLXQXNXQ\u003D.VisibleRange.GetType(), (IComparable) num3, (IComparable) num4);
      }
      else
        this.\u0023\u003DzD9Yw\u0024iTKpgBA();
    }
    return abyLt9clZggmJsWhw1;
  }
}
