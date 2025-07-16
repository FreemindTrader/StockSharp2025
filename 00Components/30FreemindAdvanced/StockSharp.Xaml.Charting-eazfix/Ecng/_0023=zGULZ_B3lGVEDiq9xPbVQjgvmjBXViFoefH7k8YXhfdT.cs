// Decompiled with JetBrains decompiler
// Type: #=zGULZ_B3lGVEDiq9xPbVQjgvmjBXViFoefH7k8YXhfdTqAasIOppKfaJ8_zxxVQenXg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjgvmjBXViFoefH7k8YXhfdTqAasIOppKfaJ8_zxxVQenXg\u003D\u003D : 
  \u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GFfexjP4JI2sgmYygvp7gvAFg\u003D\u003D
{
  private RectangleInt \u0023\u003DzIvf0fGm5UadN;

  public \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjgvmjBXViFoefH7k8YXhfdTqAasIOppKfaJ8_zxxVQenXg\u003D\u003D(
    IImageByte _param1)
    : base(_param1)
  {
    this.\u0023\u003DzIvf0fGm5UadN = new RectangleInt(0, 0, _param1.Width - 1, _param1.Height - 1);
  }

  public override void \u0023\u003DzwGTxcjtWPAOb(
    IImageByte _param1)
  {
    base.\u0023\u003DzwGTxcjtWPAOb(_param1);
    this.\u0023\u003DzIvf0fGm5UadN = new RectangleInt(0, 0, _param1.Width - 1, _param1.Height - 1);
  }

  public bool \u0023\u003DzI7XtJBES5uqX(int _param1, int _param2, int _param3, int _param4)
  {
    RectangleInt xvhBayrK2CmzoKas = new RectangleInt(_param1, _param2, _param3, _param4);
    xvhBayrK2CmzoKas.\u0023\u003DzO3\u0024NMwQ\u003D();
    if (xvhBayrK2CmzoKas.\u0023\u003DzPHB5nPY\u003D(new RectangleInt(0, 0, this.Width - 1, this.Height - 1)))
    {
      this.\u0023\u003DzIvf0fGm5UadN = xvhBayrK2CmzoKas;
      return true;
    }
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzP4R7yU0\u003D = 1;
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzRNV_Dpk\u003D = 1;
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003Dzp55dtus\u003D = 0;
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzSzOWcj8\u003D = 0;
    return false;
  }

  public void \u0023\u003DzThyAY8tY1g4ajpiZ6g\u003D\u003D(bool _param1)
  {
    if (_param1)
    {
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzP4R7yU0\u003D = 0;
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzRNV_Dpk\u003D = 0;
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003Dzp55dtus\u003D = this.Width - 1;
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzSzOWcj8\u003D = this.Height - 1;
    }
    else
    {
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzP4R7yU0\u003D = 1;
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzRNV_Dpk\u003D = 1;
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003Dzp55dtus\u003D = 0;
      this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzSzOWcj8\u003D = 0;
    }
  }

  public void \u0023\u003DzqHKjpLjqy7vhONrY6w\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzP4R7yU0\u003D = _param1;
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzRNV_Dpk\u003D = _param2;
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003Dzp55dtus\u003D = _param3;
    this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzSzOWcj8\u003D = _param4;
  }

  public bool \u0023\u003DzHzs\u0024qbI0q05\u0024(int _param1, int _param2)
  {
    return _param1 >= this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzP4R7yU0\u003D && _param2 >= this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzRNV_Dpk\u003D && _param1 <= this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003Dzp55dtus\u003D && _param2 <= this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzSzOWcj8\u003D;
  }

  public RectangleInt \u0023\u003DzbRHAWK1yd7\u00242()
  {
    return this.\u0023\u003DzIvf0fGm5UadN;
  }

  private int \u0023\u003DztOKmm78IPiiJ()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzP4R7yU0\u003D;
  }

  private int \u0023\u003DzlKJuvbCCF6NL()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzRNV_Dpk\u003D;
  }

  private int \u0023\u003Dz8zeNC1AAl5kt()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003Dzp55dtus\u003D;
  }

  private int \u0023\u003DzXAiUnNdKWTQu()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzSzOWcj8\u003D;
  }

  public RectangleInt \u0023\u003DzG2hDaohxD94m5lhGOQ\u003D\u003D()
  {
    return this.\u0023\u003DzIvf0fGm5UadN;
  }

  public int \u0023\u003DzAet7cu26fh2X()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzP4R7yU0\u003D;
  }

  public int \u0023\u003DzfZL3jazHRtGX()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzRNV_Dpk\u003D;
  }

  public int \u0023\u003DzV_\u00243GqvaLXrW()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003Dzp55dtus\u003D;
  }

  public int \u0023\u003Dzmi9a4auiWfC2()
  {
    return this.\u0023\u003DzIvf0fGm5UadN.\u0023\u003DzSzOWcj8\u003D;
  }

  public void \u0023\u003DzS3VDevc\u003D(
    IColorType _param1)
  {
    RGBA_Bytes nwsEwePinXgsJj4Q = new RGBA_Bytes(_param1.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D(), _param1.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D(), _param1.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D(), _param1.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D());
    if (this.Width == 0)
      return;
    for (int index = 0; index < this.Height; ++index)
      base.\u0023\u003DzIw2\u00246wVb_WCdm8lNiA\u003D\u003D(0, index, this.Width, nwsEwePinXgsJj4Q);
  }

  public override void \u0023\u003DzLWpPHs7BoU1N(
    int _param1,
    int _param2,
    byte[] _param3,
    int _param4)
  {
    if (!this.\u0023\u003DzHzs\u0024qbI0q05\u0024(_param1, _param2))
      return;
    base.\u0023\u003DzLWpPHs7BoU1N(_param1, _param2, _param3, _param4);
  }

  public override RGBA_Bytes \u0023\u003DzFiIk5SM\u003D(
    int _param1,
    int _param2)
  {
    return !this.\u0023\u003DzHzs\u0024qbI0q05\u0024(_param1, _param2) ? new RGBA_Bytes() : base.\u0023\u003DzFiIk5SM\u003D(_param1, _param2);
  }

  public override void \u0023\u003DzIw2\u00246wVb_WCdm8lNiA\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes _param4)
  {
    if (_param1 > _param3)
    {
      int num = _param3;
      _param3 = _param1;
      _param1 = num;
    }
    if (_param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param2 < this.\u0023\u003DzlKJuvbCCF6NL() || _param1 > this.\u0023\u003Dz8zeNC1AAl5kt() || _param3 < this.\u0023\u003DztOKmm78IPiiJ())
      return;
    if (_param1 < this.\u0023\u003DztOKmm78IPiiJ())
      _param1 = this.\u0023\u003DztOKmm78IPiiJ();
    if (_param3 > this.\u0023\u003Dz8zeNC1AAl5kt())
      _param3 = this.\u0023\u003Dz8zeNC1AAl5kt();
    base.\u0023\u003DzIw2\u00246wVb_WCdm8lNiA\u003D\u003D(_param1, _param2, _param3 - _param1 + 1, _param4);
  }

  public override void \u0023\u003DzWDirdRobfratFs26wg\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes _param4)
  {
    if (_param2 > _param3)
    {
      int num = _param3;
      _param3 = _param2;
      _param2 = num;
    }
    if (_param1 > this.\u0023\u003Dz8zeNC1AAl5kt() || _param1 < this.\u0023\u003DztOKmm78IPiiJ() || _param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param3 < this.\u0023\u003DzlKJuvbCCF6NL())
      return;
    if (_param2 < this.\u0023\u003DzlKJuvbCCF6NL())
      _param2 = this.\u0023\u003DzlKJuvbCCF6NL();
    if (_param3 > this.\u0023\u003DzXAiUnNdKWTQu())
      _param3 = this.\u0023\u003DzXAiUnNdKWTQu();
    base.\u0023\u003DzWDirdRobfratFs26wg\u003D\u003D(_param1, _param2, _param3 - _param2 + 1, _param4);
  }

  public override void \u0023\u003DzoE1u2venaydfoEqpjg\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes _param4,
    byte _param5)
  {
    if (_param1 > _param3)
    {
      int num = _param3;
      _param3 = _param1;
      _param1 = num;
    }
    if (_param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param2 < this.\u0023\u003DzlKJuvbCCF6NL() || _param1 > this.\u0023\u003Dz8zeNC1AAl5kt() || _param3 < this.\u0023\u003DztOKmm78IPiiJ())
      return;
    if (_param1 < this.\u0023\u003DztOKmm78IPiiJ())
      _param1 = this.\u0023\u003DztOKmm78IPiiJ();
    if (_param3 > this.\u0023\u003Dz8zeNC1AAl5kt())
      _param3 = this.\u0023\u003Dz8zeNC1AAl5kt();
    base.\u0023\u003DzoE1u2venaydfoEqpjg\u003D\u003D(_param1, _param2, _param3, _param4, _param5);
  }

  public override void \u0023\u003Dzz4pZLJmkX8\u0024Augw7tA\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes _param4,
    byte _param5)
  {
    if (_param2 > _param3)
    {
      int num = _param3;
      _param3 = _param2;
      _param2 = num;
    }
    if (_param1 > this.\u0023\u003Dz8zeNC1AAl5kt() || _param1 < this.\u0023\u003DztOKmm78IPiiJ() || _param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param3 < this.\u0023\u003DzlKJuvbCCF6NL())
      return;
    if (_param2 < this.\u0023\u003DzlKJuvbCCF6NL())
      _param2 = this.\u0023\u003DzlKJuvbCCF6NL();
    if (_param3 > this.\u0023\u003DzXAiUnNdKWTQu())
      _param3 = this.\u0023\u003DzXAiUnNdKWTQu();
    base.\u0023\u003Dzz4pZLJmkX8\u0024Augw7tA\u003D\u003D(_param1, _param2, _param3, _param4, _param5);
  }

  public override void \u0023\u003Dz\u00244g26jU6qlj5jJbMN_d_Aru2N2xf(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes _param4,
    byte[] _param5,
    int _param6)
  {
    if (_param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param2 < this.\u0023\u003DzlKJuvbCCF6NL())
      return;
    if (_param1 < this.\u0023\u003DztOKmm78IPiiJ())
    {
      _param3 -= this.\u0023\u003DztOKmm78IPiiJ() - _param1;
      if (_param3 <= 0)
        return;
      _param6 += this.\u0023\u003DztOKmm78IPiiJ() - _param1;
      _param1 = this.\u0023\u003DztOKmm78IPiiJ();
    }
    if (_param1 + _param3 > this.\u0023\u003Dz8zeNC1AAl5kt())
    {
      _param3 = this.\u0023\u003Dz8zeNC1AAl5kt() - _param1 + 1;
      if (_param3 <= 0)
        return;
    }
    base.\u0023\u003Dz\u00244g26jU6qlj5jJbMN_d_Aru2N2xf(_param1, _param2, _param3, _param4, _param5, _param6);
  }

  public override void \u0023\u003Dz5yPQgEdavuyT81_RwrDy\u002430AFlUD(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes _param4,
    byte[] _param5,
    int _param6)
  {
    if (_param1 > this.\u0023\u003Dz8zeNC1AAl5kt() || _param1 < this.\u0023\u003DztOKmm78IPiiJ())
      return;
    if (_param2 < this.\u0023\u003DzlKJuvbCCF6NL())
    {
      _param3 -= this.\u0023\u003DzlKJuvbCCF6NL() - _param2;
      if (_param3 <= 0)
        return;
      _param6 += this.\u0023\u003DzlKJuvbCCF6NL() - _param2;
      _param2 = this.\u0023\u003DzlKJuvbCCF6NL();
    }
    if (_param2 + _param3 > this.\u0023\u003DzXAiUnNdKWTQu())
    {
      _param3 = this.\u0023\u003DzXAiUnNdKWTQu() - _param2 + 1;
      if (_param3 <= 0)
        return;
    }
    base.\u0023\u003Dz5yPQgEdavuyT81_RwrDy\u002430AFlUD(_param1, _param2, _param3, _param4, _param5, _param6);
  }

  public override void \u0023\u003DzWPXGdZTW9yddqSlTow\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes[] _param4,
    int _param5)
  {
    if (_param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param2 < this.\u0023\u003DzlKJuvbCCF6NL())
      return;
    if (_param1 < this.\u0023\u003DztOKmm78IPiiJ())
    {
      int num = this.\u0023\u003DztOKmm78IPiiJ() - _param1;
      _param3 -= num;
      if (_param3 <= 0)
        return;
      _param5 += num;
      _param1 = this.\u0023\u003DztOKmm78IPiiJ();
    }
    if (_param1 + _param3 > this.\u0023\u003Dz8zeNC1AAl5kt())
    {
      _param3 = this.\u0023\u003Dz8zeNC1AAl5kt() - _param1 + 1;
      if (_param3 <= 0)
        return;
    }
    base.\u0023\u003DzWPXGdZTW9yddqSlTow\u003D\u003D(_param1, _param2, _param3, _param4, _param5);
  }

  public override void \u0023\u003Dz1iCm\u00249JJu\u0024SR_MfckQ\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes[] _param4,
    int _param5)
  {
    if (_param1 > this.\u0023\u003Dz8zeNC1AAl5kt() || _param1 < this.\u0023\u003DztOKmm78IPiiJ())
      return;
    if (_param2 < this.\u0023\u003DzlKJuvbCCF6NL())
    {
      int num = this.\u0023\u003DzlKJuvbCCF6NL() - _param2;
      _param3 -= num;
      if (_param3 <= 0)
        return;
      _param5 += num;
      _param2 = this.\u0023\u003DzlKJuvbCCF6NL();
    }
    if (_param2 + _param3 > this.\u0023\u003DzXAiUnNdKWTQu())
    {
      _param3 = this.\u0023\u003DzXAiUnNdKWTQu() - _param2 + 1;
      if (_param3 <= 0)
        return;
    }
    base.\u0023\u003Dz1iCm\u00249JJu\u0024SR_MfckQ\u003D\u003D(_param1, _param2, _param3, _param4, _param5);
  }

  public override void \u0023\u003Dz_Cr7XQfsG3x4FPQiULaFBxE\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes[] _param4,
    int _param5,
    byte[] _param6,
    int _param7,
    bool _param8)
  {
    int num1 = _param3;
    if (_param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param2 < this.\u0023\u003DzlKJuvbCCF6NL())
      return;
    if (_param1 < this.\u0023\u003DztOKmm78IPiiJ())
    {
      int num2 = this.\u0023\u003DztOKmm78IPiiJ() - _param1;
      num1 -= num2;
      if (num1 <= 0)
        return;
      if (_param6 != null)
        _param7 += num2;
      _param5 += num2;
      _param1 = this.\u0023\u003DztOKmm78IPiiJ();
    }
    if (_param1 + num1 - 1 > this.\u0023\u003Dz8zeNC1AAl5kt())
    {
      num1 = this.\u0023\u003Dz8zeNC1AAl5kt() - _param1 + 1;
      if (num1 <= 0)
        return;
    }
    base.\u0023\u003Dz_Cr7XQfsG3x4FPQiULaFBxE\u003D(_param1, _param2, num1, _param4, _param5, _param6, _param7, _param8);
  }

  public void \u0023\u003DzTMGYcms3hj4T(
    IImageByte _param1)
  {
    this.\u0023\u003DzCadMMgc\u003D(_param1, new RectangleInt(0, 0, _param1.Width, _param1.Height), 0, 0);
  }

  public override void \u0023\u003DzR2zHA_0\u003D(
    int _param1,
    int _param2,
    RGBA_Bytes _param3)
  {
    if ((long) (uint) _param1 >= (long) this.Width || (long) (uint) _param2 >= (long) this.Height)
      return;
    base.\u0023\u003DzR2zHA_0\u003D(_param1, _param2, _param3);
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    IImageByte _param1,
    RectangleInt _param2,
    int _param3,
    int _param4)
  {
    RectangleInt xvhBayrK2CmzoKas1 = _param2;
    xvhBayrK2CmzoKas1.\u0023\u003DznR9\u00242Eg\u003D(_param3, _param4);
    RectangleInt xvhBayrK2CmzoKas2 = new RectangleInt();
    if (!xvhBayrK2CmzoKas2.\u0023\u003DzvBhLthYerYntQLEPioo2lZw\u003D(xvhBayrK2CmzoKas1, this.\u0023\u003DzIvf0fGm5UadN))
      return;
    xvhBayrK2CmzoKas2.\u0023\u003DznR9\u00242Eg\u003D(-_param3, -_param4);
    base.\u0023\u003DzCadMMgc\u003D(_param1, xvhBayrK2CmzoKas2, _param3, _param4);
  }

  public RectangleInt \u0023\u003DzH6f1Ws0uS7YO\u0024nKhcQ\u003D\u003D(
    ref RectangleInt _param1,
    ref RectangleInt _param2,
    int _param3,
    int _param4)
  {
    RectangleInt xvhBayrK2CmzoKas1 = new RectangleInt(0, 0, 0, 0);
    RectangleInt xvhBayrK2CmzoKas2 = this.\u0023\u003DzbRHAWK1yd7\u00242();
    ++xvhBayrK2CmzoKas2.\u0023\u003Dzp55dtus\u003D;
    ++xvhBayrK2CmzoKas2.\u0023\u003DzSzOWcj8\u003D;
    if (_param2.\u0023\u003DzP4R7yU0\u003D < 0)
    {
      _param1.\u0023\u003DzP4R7yU0\u003D -= _param2.\u0023\u003DzP4R7yU0\u003D;
      _param2.\u0023\u003DzP4R7yU0\u003D = 0;
    }
    if (_param2.\u0023\u003DzRNV_Dpk\u003D < 0)
    {
      _param1.\u0023\u003DzRNV_Dpk\u003D -= _param2.\u0023\u003DzRNV_Dpk\u003D;
      _param2.\u0023\u003DzRNV_Dpk\u003D = 0;
    }
    if (_param2.\u0023\u003Dzp55dtus\u003D > _param3)
      _param2.\u0023\u003Dzp55dtus\u003D = _param3;
    if (_param2.\u0023\u003DzSzOWcj8\u003D > _param4)
      _param2.\u0023\u003DzSzOWcj8\u003D = _param4;
    if (_param1.\u0023\u003DzP4R7yU0\u003D < xvhBayrK2CmzoKas2.\u0023\u003DzP4R7yU0\u003D)
    {
      _param2.\u0023\u003DzP4R7yU0\u003D += xvhBayrK2CmzoKas2.\u0023\u003DzP4R7yU0\u003D - _param1.\u0023\u003DzP4R7yU0\u003D;
      _param1.\u0023\u003DzP4R7yU0\u003D = xvhBayrK2CmzoKas2.\u0023\u003DzP4R7yU0\u003D;
    }
    if (_param1.\u0023\u003DzRNV_Dpk\u003D < xvhBayrK2CmzoKas2.\u0023\u003DzRNV_Dpk\u003D)
    {
      _param2.\u0023\u003DzRNV_Dpk\u003D += xvhBayrK2CmzoKas2.\u0023\u003DzRNV_Dpk\u003D - _param1.\u0023\u003DzRNV_Dpk\u003D;
      _param1.\u0023\u003DzRNV_Dpk\u003D = xvhBayrK2CmzoKas2.\u0023\u003DzRNV_Dpk\u003D;
    }
    if (_param1.\u0023\u003Dzp55dtus\u003D > xvhBayrK2CmzoKas2.\u0023\u003Dzp55dtus\u003D)
      _param1.\u0023\u003Dzp55dtus\u003D = xvhBayrK2CmzoKas2.\u0023\u003Dzp55dtus\u003D;
    if (_param1.\u0023\u003DzSzOWcj8\u003D > xvhBayrK2CmzoKas2.\u0023\u003DzSzOWcj8\u003D)
      _param1.\u0023\u003DzSzOWcj8\u003D = xvhBayrK2CmzoKas2.\u0023\u003DzSzOWcj8\u003D;
    xvhBayrK2CmzoKas1.\u0023\u003Dzp55dtus\u003D = _param1.\u0023\u003Dzp55dtus\u003D - _param1.\u0023\u003DzP4R7yU0\u003D;
    xvhBayrK2CmzoKas1.\u0023\u003DzSzOWcj8\u003D = _param1.\u0023\u003DzSzOWcj8\u003D - _param1.\u0023\u003DzRNV_Dpk\u003D;
    if (xvhBayrK2CmzoKas1.\u0023\u003Dzp55dtus\u003D > _param2.\u0023\u003Dzp55dtus\u003D - _param2.\u0023\u003DzP4R7yU0\u003D)
      xvhBayrK2CmzoKas1.\u0023\u003Dzp55dtus\u003D = _param2.\u0023\u003Dzp55dtus\u003D - _param2.\u0023\u003DzP4R7yU0\u003D;
    if (xvhBayrK2CmzoKas1.\u0023\u003DzSzOWcj8\u003D > _param2.\u0023\u003DzSzOWcj8\u003D - _param2.\u0023\u003DzRNV_Dpk\u003D)
      xvhBayrK2CmzoKas1.\u0023\u003DzSzOWcj8\u003D = _param2.\u0023\u003DzSzOWcj8\u003D - _param2.\u0023\u003DzRNV_Dpk\u003D;
    return xvhBayrK2CmzoKas1;
  }

  public override void \u0023\u003DzCHZdvpXxHmsxsEnO0xInJE8\u003D(
    int _param1,
    int _param2,
    int _param3,
    RGBA_Bytes[] _param4,
    int _param5,
    byte[] _param6,
    int _param7,
    bool _param8)
  {
    if (_param1 > this.\u0023\u003Dz8zeNC1AAl5kt() || _param1 < this.\u0023\u003DztOKmm78IPiiJ())
      return;
    if (_param2 < this.\u0023\u003DzlKJuvbCCF6NL())
    {
      int num = this.\u0023\u003DzlKJuvbCCF6NL() - _param2;
      _param3 -= num;
      if (_param3 <= 0)
        return;
      if (_param6 != null)
        _param7 += num;
      _param5 += num;
      _param2 = this.\u0023\u003DzlKJuvbCCF6NL();
    }
    if (_param2 + _param3 > this.\u0023\u003DzXAiUnNdKWTQu())
    {
      _param3 = this.\u0023\u003DzXAiUnNdKWTQu() - _param2 + 1;
      if (_param3 <= 0)
        return;
    }
    base.\u0023\u003DzCHZdvpXxHmsxsEnO0xInJE8\u003D(_param1, _param2, _param3, _param4, _param5, _param6, _param7, _param8);
  }
}
