// Decompiled with JetBrains decompiler
// Type: #=zbcX$ot$Zhy6wUdB9J7NC3zBj0gwfrZ83Dds5jpCOZ4BhB_9FqI1TxBQx0ZXbRK0ob9fwKig=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3zBj0gwfrZ83Dds5jpCOZ4BhB_9FqI1TxBQx0ZXbRK0ob9fwKig\u003D : 
  \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqHtfqtJazhWZBigYHx_OlcSfAmDhFjqopC1alblq1qI3TQ\u003D\u003D
{
  private \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas \u0023\u003DzIvf0fGm5UadN;

  public \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3zBj0gwfrZ83Dds5jpCOZ4BhB_9FqI1TxBQx0ZXbRK0ob9fwKig\u003D(
    \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D _param1)
    : base(_param1)
  {
    this.\u0023\u003DzIvf0fGm5UadN = new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas(0, 0, _param1.Width - 1, _param1.Height - 1);
  }

  public override void \u0023\u003DzwGTxcjtWPAOb(
    \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D _param1)
  {
    base.\u0023\u003DzwGTxcjtWPAOb(_param1);
    this.\u0023\u003DzIvf0fGm5UadN = new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas(0, 0, _param1.Width - 1, _param1.Height - 1);
  }

  public bool \u0023\u003DzI7XtJBES5uqX(int _param1, int _param2, int _param3, int _param4)
  {
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas xvhBayrK2CmzoKas = new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas(_param1, _param2, _param3, _param4);
    xvhBayrK2CmzoKas.\u0023\u003DzO3\u0024NMwQ\u003D();
    if (xvhBayrK2CmzoKas.\u0023\u003DzPHB5nPY\u003D(new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas(0, 0, this.Width - 1, this.Height - 1)))
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

  public \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas \u0023\u003DzbRHAWK1yd7\u00242()
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

  public \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas \u0023\u003DzG2hDaohxD94m5lhGOQ\u003D\u003D()
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
    \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvQs1O7lhMhK_KMMEEQN4PF8pm_6qiHiKh2FEigKI _param1)
  {
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D af0lRdvTglyLevjU0 = _param1.\u0023\u003DzLtGfTF6UBAlmvu08d_2IGiE\u003D();
    if (this.Width == 0)
      return;
    for (int index = 0; index < this.Height; ++index)
      base.\u0023\u003DzIw2\u00246wVb_WCdm8lNiA\u003D\u003D(0, index, this.Width, af0lRdvTglyLevjU0);
  }

  public override void \u0023\u003DzLWpPHs7BoU1N(
    int _param1,
    int _param2,
    float[] _param3,
    int _param4)
  {
    if (!this.\u0023\u003DzHzs\u0024qbI0q05\u0024(_param1, _param2))
      return;
    base.\u0023\u003DzLWpPHs7BoU1N(_param1, _param2, _param3, _param4);
  }

  public override \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D \u0023\u003DzFiIk5SM\u003D(
    int _param1,
    int _param2)
  {
    return !this.\u0023\u003DzHzs\u0024qbI0q05\u0024(_param1, _param2) ? new \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D() : base.\u0023\u003DzFiIk5SM\u003D(_param1, _param2);
  }

  public override void \u0023\u003DzIw2\u00246wVb_WCdm8lNiA\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4)
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4)
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
    byte[] _param5,
    int _param6)
  {
    int num = _param3;
    if (_param2 > this.\u0023\u003DzXAiUnNdKWTQu() || _param2 < this.\u0023\u003DzlKJuvbCCF6NL())
      return;
    if (_param1 < this.\u0023\u003DztOKmm78IPiiJ())
    {
      num -= this.\u0023\u003DztOKmm78IPiiJ() - _param1;
      if (num <= 0)
        return;
      _param6 += this.\u0023\u003DztOKmm78IPiiJ() - _param1;
      _param1 = this.\u0023\u003DztOKmm78IPiiJ();
    }
    if (_param1 + num > this.\u0023\u003Dz8zeNC1AAl5kt())
    {
      num = this.\u0023\u003Dz8zeNC1AAl5kt() - _param1 + 1;
      if (num <= 0)
        return;
    }
    base.\u0023\u003Dz\u00244g26jU6qlj5jJbMN_d_Aru2N2xf(_param1, _param2, num, _param4, _param5, _param6);
  }

  public override void \u0023\u003Dz5yPQgEdavuyT81_RwrDy\u002430AFlUD(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
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
    \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D _param1)
  {
    this.\u0023\u003DzCadMMgc\u003D(_param1, new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas(0, 0, _param1.Width, _param1.Height), 0, 0);
  }

  public override void \u0023\u003DzR2zHA_0\u003D(
    int _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param3)
  {
    if ((long) (uint) _param1 >= (long) this.Width || (long) (uint) _param2 >= (long) this.Height)
      return;
    base.\u0023\u003DzR2zHA_0\u003D(_param1, _param2, _param3);
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D _param1,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param2,
    int _param3,
    int _param4)
  {
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas xvhBayrK2CmzoKas1 = _param2;
    xvhBayrK2CmzoKas1.\u0023\u003DznR9\u00242Eg\u003D(_param3, _param4);
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas xvhBayrK2CmzoKas2 = new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas();
    if (!xvhBayrK2CmzoKas2.\u0023\u003DzvBhLthYerYntQLEPioo2lZw\u003D(xvhBayrK2CmzoKas1, this.\u0023\u003DzIvf0fGm5UadN))
      return;
    xvhBayrK2CmzoKas2.\u0023\u003DznR9\u00242Eg\u003D(-_param3, -_param4);
    base.\u0023\u003DzCadMMgc\u003D(_param1, xvhBayrK2CmzoKas2, _param3, _param4);
  }

  public \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas \u0023\u003DzH6f1Ws0uS7YO\u0024nKhcQ\u003D\u003D(
    ref \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1,
    ref \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param2,
    int _param3,
    int _param4)
  {
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas xvhBayrK2CmzoKas1 = new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas(0, 0, 0, 0);
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas xvhBayrK2CmzoKas2 = this.\u0023\u003DzbRHAWK1yd7\u00242();
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
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
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
