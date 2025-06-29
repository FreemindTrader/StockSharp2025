// Decompiled with JetBrains decompiler
// Type: #=z6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR
{
  private double[] \u0023\u003DzNnNx0_c\u003D = new double[16 /*0x10*/];

  public \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR()
  {
    this.\u0023\u003DzWv0\u0024a14\u003D(0, 0, 1.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(1, 1, 1.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(2, 2, 1.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 3, 1.0);
  }

  public \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param1)
  {
    for (int index = 0; index < 16 /*0x10*/; ++index)
      this.\u0023\u003DzNnNx0_c\u003D[index] = _param1.\u0023\u003DzNnNx0_c\u003D[index];
  }

  public \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR(double[] _param1)
  {
    this.\u0023\u003DzsNJdwlQ\u003D(_param1);
  }

  [IndexerName("#=zMRIb09I=")]
  public double this[int _param1]
  {
    get => this.\u0023\u003DzNnNx0_c\u003D[_param1];
    set => this.\u0023\u003DzNnNx0_c\u003D[_param1] = value;
  }

  public double \u0023\u003Dz\u0024CeUvME\u003D(int _param1)
  {
    return this.\u0023\u003DzNnNx0_c\u003D[_param1];
  }

  public void \u0023\u003DzS9gpfR4\u003D(int _param1, double _param2)
  {
    this.\u0023\u003DzNnNx0_c\u003D[_param1] = _param2;
  }

  [IndexerName("#=zMRIb09I=")]
  public double this[int _param1, int _param2]
  {
    get => this.\u0023\u003DzR8apb4E\u003D(_param1, _param2);
    set => this.\u0023\u003DzWv0\u0024a14\u003D(_param1, _param2, value);
  }

  public double \u0023\u003Dz\u0024CeUvME\u003D(int _param1, int _param2)
  {
    return this.\u0023\u003DzR8apb4E\u003D(_param1, _param2);
  }

  public void \u0023\u003DzS9gpfR4\u003D(int _param1, int _param2, double _param3)
  {
    this.\u0023\u003DzWv0\u0024a14\u003D(_param1, _param2, _param3);
  }

  public double \u0023\u003DzR8apb4E\u003D(int _param1, int _param2)
  {
    return this.\u0023\u003DzNnNx0_c\u003D[_param1 * 4 + _param2];
  }

  public void \u0023\u003DzWv0\u0024a14\u003D(int _param1, int _param2, double _param3)
  {
    this.\u0023\u003DzNnNx0_c\u003D[_param1 * 4 + _param2] = _param3;
  }

  public void \u0023\u003Dz4M_pW8k\u003D(int _param1, int _param2, double _param3)
  {
    this.\u0023\u003DzNnNx0_c\u003D[_param1 * 4 + _param2] += _param3;
  }

  public void \u0023\u003DzRPagJIE\u003D()
  {
    this.\u0023\u003DzUplxldM\u003D();
    this.\u0023\u003DzWv0\u0024a14\u003D(0, 0, 1.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(1, 1, 1.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(2, 2, 1.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 3, 1.0);
  }

  public void \u0023\u003DzUplxldM\u003D()
  {
    for (int index = 0; index < 16 /*0x10*/; ++index)
      this.\u0023\u003DzNnNx0_c\u003D[index] = 0.0;
  }

  private static void \u0023\u003DzGTfHcnf2J\u00244c6ToIJw\u003D\u003D(
    double[] _param0,
    double[] _param1)
  {
    double[] numArray1 = new double[12];
    double[] numArray2 = new double[16 /*0x10*/];
    for (int index = 0; index < 4; ++index)
    {
      numArray2[index] = _param0[index * 4];
      numArray2[index + 4] = _param0[index * 4 + 1];
      numArray2[index + 8] = _param0[index * 4 + 2];
      numArray2[index + 12] = _param0[index * 4 + 3];
    }
    numArray1[0] = numArray2[10] * numArray2[15];
    numArray1[1] = numArray2[11] * numArray2[14];
    numArray1[2] = numArray2[9] * numArray2[15];
    numArray1[3] = numArray2[11] * numArray2[13];
    numArray1[4] = numArray2[9] * numArray2[14];
    numArray1[5] = numArray2[10] * numArray2[13];
    numArray1[6] = numArray2[8] * numArray2[15];
    numArray1[7] = numArray2[11] * numArray2[12];
    numArray1[8] = numArray2[8] * numArray2[14];
    numArray1[9] = numArray2[10] * numArray2[12];
    numArray1[10] = numArray2[8] * numArray2[13];
    numArray1[11] = numArray2[9] * numArray2[12];
    _param1[0] = numArray1[0] * numArray2[5] + numArray1[3] * numArray2[6] + numArray1[4] * numArray2[7];
    _param1[0] -= numArray1[1] * numArray2[5] + numArray1[2] * numArray2[6] + numArray1[5] * numArray2[7];
    _param1[1] = numArray1[1] * numArray2[4] + numArray1[6] * numArray2[6] + numArray1[9] * numArray2[7];
    _param1[1] -= numArray1[0] * numArray2[4] + numArray1[7] * numArray2[6] + numArray1[8] * numArray2[7];
    _param1[2] = numArray1[2] * numArray2[4] + numArray1[7] * numArray2[5] + numArray1[10] * numArray2[7];
    _param1[2] -= numArray1[3] * numArray2[4] + numArray1[6] * numArray2[5] + numArray1[11] * numArray2[7];
    _param1[3] = numArray1[5] * numArray2[4] + numArray1[8] * numArray2[5] + numArray1[11] * numArray2[6];
    _param1[3] -= numArray1[4] * numArray2[4] + numArray1[9] * numArray2[5] + numArray1[10] * numArray2[6];
    _param1[4] = numArray1[1] * numArray2[1] + numArray1[2] * numArray2[2] + numArray1[5] * numArray2[3];
    _param1[4] -= numArray1[0] * numArray2[1] + numArray1[3] * numArray2[2] + numArray1[4] * numArray2[3];
    _param1[5] = numArray1[0] * numArray2[0] + numArray1[7] * numArray2[2] + numArray1[8] * numArray2[3];
    _param1[5] -= numArray1[1] * numArray2[0] + numArray1[6] * numArray2[2] + numArray1[9] * numArray2[3];
    _param1[6] = numArray1[3] * numArray2[0] + numArray1[6] * numArray2[1] + numArray1[11] * numArray2[3];
    _param1[6] -= numArray1[2] * numArray2[0] + numArray1[7] * numArray2[1] + numArray1[10] * numArray2[3];
    _param1[7] = numArray1[4] * numArray2[0] + numArray1[9] * numArray2[1] + numArray1[10] * numArray2[2];
    _param1[7] -= numArray1[5] * numArray2[0] + numArray1[8] * numArray2[1] + numArray1[11] * numArray2[2];
    numArray1[0] = numArray2[2] * numArray2[7];
    numArray1[1] = numArray2[3] * numArray2[6];
    numArray1[2] = numArray2[1] * numArray2[7];
    numArray1[3] = numArray2[3] * numArray2[5];
    numArray1[4] = numArray2[1] * numArray2[6];
    numArray1[5] = numArray2[2] * numArray2[5];
    numArray1[6] = numArray2[0] * numArray2[7];
    numArray1[7] = numArray2[3] * numArray2[4];
    numArray1[8] = numArray2[0] * numArray2[6];
    numArray1[9] = numArray2[2] * numArray2[4];
    numArray1[10] = numArray2[0] * numArray2[5];
    numArray1[11] = numArray2[1] * numArray2[4];
    _param1[8] = numArray1[0] * numArray2[13] + numArray1[3] * numArray2[14] + numArray1[4] * numArray2[15];
    _param1[8] -= numArray1[1] * numArray2[13] + numArray1[2] * numArray2[14] + numArray1[5] * numArray2[15];
    _param1[9] = numArray1[1] * numArray2[12] + numArray1[6] * numArray2[14] + numArray1[9] * numArray2[15];
    _param1[9] -= numArray1[0] * numArray2[12] + numArray1[7] * numArray2[14] + numArray1[8] * numArray2[15];
    _param1[10] = numArray1[2] * numArray2[12] + numArray1[7] * numArray2[13] + numArray1[10] * numArray2[15];
    _param1[10] -= numArray1[3] * numArray2[12] + numArray1[6] * numArray2[13] + numArray1[11] * numArray2[15];
    _param1[11] = numArray1[5] * numArray2[12] + numArray1[8] * numArray2[13] + numArray1[11] * numArray2[14];
    _param1[11] -= numArray1[4] * numArray2[12] + numArray1[9] * numArray2[13] + numArray1[10] * numArray2[14];
    _param1[12] = numArray1[2] * numArray2[10] + numArray1[5] * numArray2[11] + numArray1[1] * numArray2[9];
    _param1[12] -= numArray1[4] * numArray2[11] + numArray1[0] * numArray2[9] + numArray1[3] * numArray2[10];
    _param1[13] = numArray1[8] * numArray2[11] + numArray1[0] * numArray2[8] + numArray1[7] * numArray2[10];
    _param1[13] -= numArray1[6] * numArray2[10] + numArray1[9] * numArray2[11] + numArray1[1] * numArray2[8];
    _param1[14] = numArray1[6] * numArray2[9] + numArray1[11] * numArray2[11] + numArray1[3] * numArray2[8];
    _param1[14] -= numArray1[10] * numArray2[11] + numArray1[2] * numArray2[8] + numArray1[7] * numArray2[9];
    _param1[15] = numArray1[10] * numArray2[10] + numArray1[4] * numArray2[8] + numArray1[9] * numArray2[9];
    _param1[15] -= numArray1[8] * numArray2[9] + numArray1[11] * numArray2[10] + numArray1[5] * numArray2[8];
    double num = 1.0 / (numArray2[0] * _param1[0] + numArray2[1] * _param1[1] + numArray2[2] * _param1[2] + numArray2[3] * _param1[3]);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      _param1[index] *= num;
  }

  public bool \u0023\u003DzfjtzGXJ364Qe(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param1)
  {
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR.\u0023\u003DzGTfHcnf2J\u00244c6ToIJw\u003D\u003D(_param1.\u0023\u003DzNnNx0_c\u003D, this.\u0023\u003DzNnNx0_c\u003D);
    return true;
  }

  public bool \u0023\u003DzkRsyJTI\u003D()
  {
    return this.\u0023\u003DzfjtzGXJ364Qe(new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR(this));
  }

  private void \u0023\u003DzBjmVsJ_QC1LIspYHLH4pGj0\u003D(int _param1, int _param2)
  {
    double num = this.\u0023\u003DzR8apb4E\u003D(_param1, _param2);
    this.\u0023\u003DzWv0\u0024a14\u003D(_param1, _param2, this.\u0023\u003DzR8apb4E\u003D(_param2, _param1));
    this.\u0023\u003DzWv0\u0024a14\u003D(_param2, _param1, num);
  }

  public void \u0023\u003DzdtrF\u0024YWmMfIRZwQmkOUtrJY\u003D()
  {
    this.\u0023\u003DzBjmVsJ_QC1LIspYHLH4pGj0\u003D(0, 1);
    this.\u0023\u003DzBjmVsJ_QC1LIspYHLH4pGj0\u003D(0, 2);
    this.\u0023\u003DzBjmVsJ_QC1LIspYHLH4pGj0\u003D(0, 3);
    this.\u0023\u003DzBjmVsJ_QC1LIspYHLH4pGj0\u003D(1, 2);
    this.\u0023\u003DzBjmVsJ_QC1LIspYHLH4pGj0\u003D(1, 3);
    this.\u0023\u003DzBjmVsJ_QC1LIspYHLH4pGj0\u003D(2, 3);
  }

  public dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd \u0023\u003DzehEdRywaTK1H()
  {
    return new dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd(this.\u0023\u003DzR8apb4E\u003D(3, 0), this.\u0023\u003DzR8apb4E\u003D(3, 1), this.\u0023\u003DzR8apb4E\u003D(3, 2));
  }

  public void \u0023\u003DzLHdkEaFYIub2(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 0, _param1.dje_z3GBAX47U_ejd);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 1, _param1.dje_zLPL6EZPA_ejd);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 2, _param1.dje_zX8HQ8VV9_ejd);
  }

  public void \u0023\u003DzphEILio\u003D(double _param1, double _param2, double _param3)
  {
    this.\u0023\u003DzUplxldM\u003D();
    for (int index = 0; index < 4; ++index)
      this.\u0023\u003DzWv0\u0024a14\u003D(index, index, 1.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 0, _param1);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 1, _param2);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 2, _param3);
  }

  public void \u0023\u003DzphEILio\u003D(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    this.\u0023\u003DzphEILio\u003D(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd, _param1.dje_zX8HQ8VV9_ejd);
  }

  public void \u0023\u003DzkOI2IsE\u003D(double _param1, double _param2, double _param3)
  {
    this.\u0023\u003DzkOI2IsE\u003D(new dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd(_param1, _param2, _param3));
  }

  public void \u0023\u003DzkOI2IsE\u003D(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    nyZdGkxOhAi6zOpwR.\u0023\u003DzphEILio\u003D(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd, _param1.dje_zX8HQ8VV9_ejd);
    this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR);
  }

  public void \u0023\u003DzPYXY6bQ\u003D(float _param1, float _param2, float _param3)
  {
    this.\u0023\u003DzPYXY6bQ\u003D((double) _param1, (double) _param2, (double) _param3);
  }

  public void \u0023\u003DzPYXY6bQ\u003D(double _param1, double _param2, double _param3)
  {
    this.\u0023\u003DzUplxldM\u003D();
    this.\u0023\u003DzWv0\u0024a14\u003D(0, 0, _param1);
    this.\u0023\u003DzWv0\u0024a14\u003D(1, 1, _param2);
    this.\u0023\u003DzWv0\u0024a14\u003D(2, 2, _param3);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 3, 1.0);
  }

  public void \u0023\u003DzdrBvBpw\u003D(uint _param1, double _param2)
  {
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    nyZdGkxOhAi6zOpwR.\u0023\u003Dz1zxzaac\u003D(_param1, _param2);
    this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR);
  }

  public void \u0023\u003Dz1zxzaac\u003D(uint _param1, double _param2)
  {
    double num1;
    double num2;
    if (_param2 != 0.0)
    {
      num1 = Math.Cos(_param2);
      num2 = Math.Sin(_param2);
    }
    else
    {
      num1 = 1.0;
      num2 = 0.0;
    }
    switch (_param1)
    {
      case 0:
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 0, 1.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 1, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 2, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 3, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 0, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 1, num1);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 2, num2);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 3, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 0, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 1, -num2);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 2, num1);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 3, 0.0);
        break;
      case 1:
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 0, num1);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 1, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 2, -num2);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 3, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 0, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 1, 1.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 2, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 3, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 0, num2);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 1, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 2, num1);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 3, 0.0);
        break;
      case 2:
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 0, num1);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 1, num2);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 2, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(0, 3, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 0, -num2);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 1, num1);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 2, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(1, 3, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 0, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 1, 0.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 2, 1.0);
        this.\u0023\u003DzWv0\u0024a14\u003D(2, 3, 0.0);
        break;
    }
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 0, 0.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 1, 0.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 2, 0.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(3, 3, 1.0);
  }

  public void \u0023\u003Dz1zxzaac\u003D(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1,
    double _param2)
  {
    _param1.\u0023\u003DzC520uIs\u003D();
    double num1 = Math.Cos(_param2);
    double num2 = Math.Sin(_param2);
    double num3 = 1.0 - num1;
    this.\u0023\u003DzNnNx0_c\u003D[0] = num3 * _param1.dje_z3GBAX47U_ejd * _param1.dje_z3GBAX47U_ejd + num1;
    this.\u0023\u003DzNnNx0_c\u003D[4] = num3 * _param1.dje_z3GBAX47U_ejd * _param1.dje_zLPL6EZPA_ejd - num2 * _param1.dje_zX8HQ8VV9_ejd;
    this.\u0023\u003DzNnNx0_c\u003D[8] = num3 * _param1.dje_z3GBAX47U_ejd * _param1.dje_zX8HQ8VV9_ejd + num2 * _param1.dje_zLPL6EZPA_ejd;
    this.\u0023\u003DzNnNx0_c\u003D[12] = 0.0;
    this.\u0023\u003DzNnNx0_c\u003D[1] = num3 * _param1.dje_z3GBAX47U_ejd * _param1.dje_zLPL6EZPA_ejd + num2 * _param1.dje_zX8HQ8VV9_ejd;
    this.\u0023\u003DzNnNx0_c\u003D[5] = num3 * _param1.dje_zLPL6EZPA_ejd * _param1.dje_zLPL6EZPA_ejd + num1;
    this.\u0023\u003DzNnNx0_c\u003D[9] = num3 * _param1.dje_zLPL6EZPA_ejd * _param1.dje_zX8HQ8VV9_ejd - num2 * _param1.dje_z3GBAX47U_ejd;
    this.\u0023\u003DzNnNx0_c\u003D[13] = 0.0;
    this.\u0023\u003DzNnNx0_c\u003D[2] = num3 * _param1.dje_z3GBAX47U_ejd * _param1.dje_zX8HQ8VV9_ejd - num2 * _param1.dje_zLPL6EZPA_ejd;
    this.\u0023\u003DzNnNx0_c\u003D[6] = num3 * _param1.dje_zLPL6EZPA_ejd * _param1.dje_zX8HQ8VV9_ejd + num2 * _param1.dje_z3GBAX47U_ejd;
    this.\u0023\u003DzNnNx0_c\u003D[10] = num3 * _param1.dje_zX8HQ8VV9_ejd * _param1.dje_zX8HQ8VV9_ejd + num1;
    this.\u0023\u003DzNnNx0_c\u003D[14] = 0.0;
    this.\u0023\u003DzNnNx0_c\u003D[3] = 0.0;
    this.\u0023\u003DzNnNx0_c\u003D[7] = 0.0;
    this.\u0023\u003DzNnNx0_c\u003D[11] = 0.0;
    this.\u0023\u003DzNnNx0_c\u003D[15] = 1.0;
  }

  public bool \u0023\u003DzhxbsSqM\u003D(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param1,
    double _param2)
  {
    for (int index1 = 0; index1 < 4; ++index1)
    {
      for (int index2 = 0; index2 < 4; ++index2)
      {
        if (this.\u0023\u003DzR8apb4E\u003D(index1, index2) < _param1.\u0023\u003DzR8apb4E\u003D(index1, index2) - _param2 || this.\u0023\u003DzR8apb4E\u003D(index1, index2) > _param1.\u0023\u003DzR8apb4E\u003D(index1, index2) + _param2)
          return false;
      }
    }
    return true;
  }

  public void \u0023\u003DzX1DkRa3qY70\u0024(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6,
    double _param7,
    double _param8,
    double _param9)
  {
    bool flag = false;
    if (_param7 != 1.0 || _param8 != 1.0 || _param9 != 1.0)
    {
      if (flag)
      {
        \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
        nyZdGkxOhAi6zOpwR.\u0023\u003DzPYXY6bQ\u003D(_param7, _param8, _param9);
        this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR);
      }
      else
      {
        this.\u0023\u003DzPYXY6bQ\u003D(_param7, _param8, _param9);
        flag = true;
      }
    }
    if (_param4 != 0.0)
    {
      if (flag)
      {
        \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
        nyZdGkxOhAi6zOpwR.\u0023\u003Dz1zxzaac\u003D(0U, _param4);
        this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR);
      }
      else
      {
        this.\u0023\u003Dz1zxzaac\u003D(0U, _param4);
        flag = true;
      }
    }
    if (_param5 != 0.0)
    {
      if (flag)
      {
        \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
        nyZdGkxOhAi6zOpwR.\u0023\u003Dz1zxzaac\u003D(1U, _param5);
        this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR);
      }
      else
      {
        this.\u0023\u003Dz1zxzaac\u003D(1U, _param5);
        flag = true;
      }
    }
    if (_param6 != 0.0)
    {
      if (flag)
      {
        \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
        nyZdGkxOhAi6zOpwR.\u0023\u003Dz1zxzaac\u003D(2U, _param6);
        this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR);
      }
      else
      {
        this.\u0023\u003Dz1zxzaac\u003D(2U, _param6);
        flag = true;
      }
    }
    if (_param1 == 0.0 && _param2 == 0.0 && _param3 == 0.0)
      return;
    if (flag)
    {
      \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
      nyZdGkxOhAi6zOpwR.\u0023\u003DzphEILio\u003D(_param1, _param2, _param3);
      this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR);
    }
    else
    {
      this.\u0023\u003DzphEILio\u003D(_param1, _param2, _param3);
      flag = true;
    }
    if (flag)
      return;
    this.\u0023\u003DzRPagJIE\u003D();
  }

  public void \u0023\u003DzX1DkRa3qY70\u0024(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1,
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param2,
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param3)
  {
    this.\u0023\u003DzX1DkRa3qY70\u0024(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd, _param1.dje_zX8HQ8VV9_ejd, _param2.dje_z3GBAX47U_ejd, _param2.dje_zLPL6EZPA_ejd, _param2.dje_zX8HQ8VV9_ejd, _param3.dje_z3GBAX47U_ejd, _param3.dje_zLPL6EZPA_ejd, _param3.dje_zX8HQ8VV9_ejd);
  }

  public void \u0023\u003DzbhiPzOru62nf2NFsVA\u003D\u003D(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1,
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param2)
  {
    this.\u0023\u003DzphEILio\u003D(_param1);
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd xxpaxL8AcxpW32QaEjd1 = _param2;
    xxpaxL8AcxpW32QaEjd1.\u0023\u003DzC520uIs\u003D();
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd xxpaxL8AcxpW32QaEjd2 = new dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd(1.0, 0.0, 0.0);
    double num = Math.Cos(0.17453293005625406);
    if (dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd.\u0023\u003DzJkPZoNo\u003D(xxpaxL8AcxpW32QaEjd1, xxpaxL8AcxpW32QaEjd2) > num)
      xxpaxL8AcxpW32QaEjd2 = new dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd(0.0, 1.0, 0.0);
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd xxpaxL8AcxpW32QaEjd3 = dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd.\u0023\u003Dz1jcyxnU\u003D(xxpaxL8AcxpW32QaEjd2, xxpaxL8AcxpW32QaEjd1);
    xxpaxL8AcxpW32QaEjd3.\u0023\u003DzC520uIs\u003D();
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd xxpaxL8AcxpW32QaEjd4 = dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd.\u0023\u003Dz1jcyxnU\u003D(xxpaxL8AcxpW32QaEjd1, xxpaxL8AcxpW32QaEjd3);
    for (int index = 0; index < 3; ++index)
    {
      this.\u0023\u003DzWv0\u0024a14\u003D(0, index, xxpaxL8AcxpW32QaEjd4.\u0023\u003Dz\u0024CeUvME\u003D(index));
      this.\u0023\u003DzWv0\u0024a14\u003D(1, index, xxpaxL8AcxpW32QaEjd1.\u0023\u003Dz\u0024CeUvME\u003D(index));
      this.\u0023\u003DzWv0\u0024a14\u003D(2, index, xxpaxL8AcxpW32QaEjd3.\u0023\u003Dz\u0024CeUvME\u003D(index));
    }
    this.\u0023\u003DzWv0\u0024a14\u003D(0, 3, 0.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(1, 3, 0.0);
    this.\u0023\u003DzWv0\u0024a14\u003D(2, 3, 0.0);
  }

  public void \u0023\u003DzO1JiwkCbqOzn(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6,
    double _param7,
    double _param8,
    double _param9)
  {
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR1 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR2 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR3 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR4 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR5 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR6 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR7 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR8 = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR();
    nyZdGkxOhAi6zOpwR1.\u0023\u003DzPYXY6bQ\u003D(_param7, _param8, _param9);
    nyZdGkxOhAi6zOpwR2.\u0023\u003Dz1zxzaac\u003D(0U, _param4);
    nyZdGkxOhAi6zOpwR3.\u0023\u003Dz1zxzaac\u003D(1U, _param5);
    nyZdGkxOhAi6zOpwR4.\u0023\u003Dz1zxzaac\u003D(2U, _param6);
    nyZdGkxOhAi6zOpwR5.\u0023\u003DzphEILio\u003D(_param1, _param2, _param3);
    nyZdGkxOhAi6zOpwR6.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR5, nyZdGkxOhAi6zOpwR4);
    nyZdGkxOhAi6zOpwR7.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR6, nyZdGkxOhAi6zOpwR3);
    nyZdGkxOhAi6zOpwR8.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR7, nyZdGkxOhAi6zOpwR2);
    this.\u0023\u003DzQw38IaY\u003D(nyZdGkxOhAi6zOpwR8, nyZdGkxOhAi6zOpwR1);
  }

  public void \u0023\u003DzO1JiwkCbqOzn(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1,
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param2,
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param3)
  {
    this.\u0023\u003DzO1JiwkCbqOzn(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd, _param1.dje_zX8HQ8VV9_ejd, _param2.dje_z3GBAX47U_ejd, _param2.dje_zLPL6EZPA_ejd, _param2.dje_zX8HQ8VV9_ejd, _param3.dje_z3GBAX47U_ejd, _param3.dje_zLPL6EZPA_ejd, _param3.dje_zX8HQ8VV9_ejd);
  }

  public void \u0023\u003Dz3l8SoTV1ZzAh(double[] _param1)
  {
    double[] numArray = (double[]) _param1.Clone();
    _param1[0] = this.\u0023\u003DzR8apb4E\u003D(0, 0) * numArray[0] + this.\u0023\u003DzR8apb4E\u003D(0, 1) * numArray[1] + this.\u0023\u003DzR8apb4E\u003D(0, 2) * numArray[2] + this.\u0023\u003DzR8apb4E\u003D(0, 3) * numArray[3];
    _param1[1] = this.\u0023\u003DzR8apb4E\u003D(1, 0) * numArray[0] + this.\u0023\u003DzR8apb4E\u003D(1, 1) * numArray[1] + this.\u0023\u003DzR8apb4E\u003D(1, 2) * numArray[2] + this.\u0023\u003DzR8apb4E\u003D(1, 3) * numArray[3];
    _param1[2] = this.\u0023\u003DzR8apb4E\u003D(2, 0) * numArray[0] + this.\u0023\u003DzR8apb4E\u003D(2, 1) * numArray[1] + this.\u0023\u003DzR8apb4E\u003D(2, 2) * numArray[2] + this.\u0023\u003DzR8apb4E\u003D(2, 3) * numArray[3];
    _param1[3] = this.\u0023\u003DzR8apb4E\u003D(3, 0) * numArray[0] + this.\u0023\u003DzR8apb4E\u003D(3, 1) * numArray[1] + this.\u0023\u003DzR8apb4E\u003D(3, 2) * numArray[2] + this.\u0023\u003DzR8apb4E\u003D(3, 3) * numArray[3];
  }

  public void \u0023\u003Dz3l8SoTV1ZzAh(
    ref dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd xxpaxL8AcxpW32QaEjd = _param1;
    this.\u0023\u003Dz3l8SoTV1ZzAh(out _param1, xxpaxL8AcxpW32QaEjd);
  }

  public void \u0023\u003Dz3l8SoTV1ZzAh(
    out dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1,
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param2)
  {
    this.\u0023\u003DzNKmWt8d4scwk(out _param1, _param2);
    _param1.dje_z3GBAX47U_ejd += this.\u0023\u003DzR8apb4E\u003D(3, 0);
    _param1.dje_zLPL6EZPA_ejd += this.\u0023\u003DzR8apb4E\u003D(3, 1);
    _param1.dje_zX8HQ8VV9_ejd += this.\u0023\u003DzR8apb4E\u003D(3, 2);
  }

  public void \u0023\u003DzNKmWt8d4scwk(
    ref dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd xxpaxL8AcxpW32QaEjd = new dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd(_param1);
    this.\u0023\u003DzNKmWt8d4scwk(out _param1, xxpaxL8AcxpW32QaEjd);
  }

  public void \u0023\u003DzNKmWt8d4scwk(
    out dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1,
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param2)
  {
    _param1.dje_z3GBAX47U_ejd = this.\u0023\u003DzR8apb4E\u003D(0, 0) * _param2.dje_z3GBAX47U_ejd + this.\u0023\u003DzR8apb4E\u003D(1, 0) * _param2.dje_zLPL6EZPA_ejd + this.\u0023\u003DzR8apb4E\u003D(2, 0) * _param2.dje_zX8HQ8VV9_ejd;
    _param1.dje_zLPL6EZPA_ejd = this.\u0023\u003DzR8apb4E\u003D(0, 1) * _param2.dje_z3GBAX47U_ejd + this.\u0023\u003DzR8apb4E\u003D(1, 1) * _param2.dje_zLPL6EZPA_ejd + this.\u0023\u003DzR8apb4E\u003D(2, 1) * _param2.dje_zX8HQ8VV9_ejd;
    _param1.dje_zX8HQ8VV9_ejd = this.\u0023\u003DzR8apb4E\u003D(0, 2) * _param2.dje_z3GBAX47U_ejd + this.\u0023\u003DzR8apb4E\u003D(1, 2) * _param2.dje_zLPL6EZPA_ejd + this.\u0023\u003DzR8apb4E\u003D(2, 2) * _param2.dje_zX8HQ8VV9_ejd;
  }

  public void \u0023\u003DzNKmWt8d4scwk(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd[] _param1)
  {
    for (int index = 0; index < _param1.Length; ++index)
      this.\u0023\u003DzNKmWt8d4scwk(ref _param1[index]);
  }

  public uint \u0023\u003DzV\u0024Ct2WAx5lch()
  {
    return this.\u0023\u003DzR8apb4E\u003D(3, 0) == 0.0 && this.\u0023\u003DzR8apb4E\u003D(3, 1) == 0.0 && this.\u0023\u003DzR8apb4E\u003D(3, 2) == 0.0 && this.\u0023\u003DzR8apb4E\u003D(3, 3) == 1.0 ? 1U : 0U;
  }

  public static \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR operator *(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param0,
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param1)
  {
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR nyZdGkxOhAi6zOpwR = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR(_param0);
    nyZdGkxOhAi6zOpwR.\u0023\u003DzQw38IaY\u003D(_param1);
    return nyZdGkxOhAi6zOpwR;
  }

  public void \u0023\u003DzQw38IaY\u003D(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param1)
  {
    this.\u0023\u003DzQw38IaY\u003D(new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR(this), _param1);
  }

  public void \u0023\u003DzQw38IaY\u003D(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param1,
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param2)
  {
    if (this == _param1 || this == _param2)
      throw new FormatException(XXX.SSS(-539321445));
    for (int index1 = 0; index1 < 4; ++index1)
    {
      for (int index2 = 0; index2 < 4; ++index2)
      {
        this.\u0023\u003DzWv0\u0024a14\u003D(index1, index2, 0.0);
        for (int index3 = 0; index3 < 4; ++index3)
          this.\u0023\u003Dz4M_pW8k\u003D(index1, index2, _param1.\u0023\u003DzR8apb4E\u003D(index1, index3) * _param2.\u0023\u003DzR8apb4E\u003D(index3, index2));
      }
    }
  }

  public void \u0023\u003DzFCQKKGtBdBg9(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    _param1.dje_z3GBAX47U_ejd = this.\u0023\u003DzR8apb4E\u003D(0, 0);
    _param1.dje_zLPL6EZPA_ejd = this.\u0023\u003DzR8apb4E\u003D(0, 1);
    _param1.dje_zX8HQ8VV9_ejd = this.\u0023\u003DzR8apb4E\u003D(0, 2);
  }

  public void \u0023\u003DzfFLJEuGeS4wQ(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    _param1.dje_z3GBAX47U_ejd = this.\u0023\u003DzR8apb4E\u003D(1, 0);
    _param1.dje_zLPL6EZPA_ejd = this.\u0023\u003DzR8apb4E\u003D(1, 1);
    _param1.dje_zX8HQ8VV9_ejd = this.\u0023\u003DzR8apb4E\u003D(1, 2);
  }

  public void \u0023\u003DznQWuhS4F7xfT(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    _param1.dje_z3GBAX47U_ejd = this.\u0023\u003DzR8apb4E\u003D(2, 0);
    _param1.dje_zLPL6EZPA_ejd = this.\u0023\u003DzR8apb4E\u003D(2, 1);
    _param1.dje_zX8HQ8VV9_ejd = this.\u0023\u003DzR8apb4E\u003D(2, 2);
  }

  public void \u0023\u003DzOxowIjY\u003D(
    dje_z7MY68MLRV6NNE2US875358L4W5J8L733S6RUSCEASWGYMMMM9MUXFJSLJKQE4A5XXPAXL8ACXPW32QA_ejd _param1)
  {
    _param1.dje_z3GBAX47U_ejd = this.\u0023\u003DzR8apb4E\u003D(3, 0);
    _param1.dje_zLPL6EZPA_ejd = this.\u0023\u003DzR8apb4E\u003D(3, 1);
    _param1.dje_zX8HQ8VV9_ejd = this.\u0023\u003DzR8apb4E\u003D(3, 2);
  }

  public void \u0023\u003DzsNJdwlQ\u003D(
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnC9Cb2dF_LdLpJTi47Qy83cNYZdGkxOhAI6zOPwR _param1)
  {
    this.\u0023\u003DzsNJdwlQ\u003D(_param1.\u0023\u003DzL8atuyQ\u003D());
  }

  public void \u0023\u003DzsNJdwlQ\u003D(double[] _param1)
  {
    for (int index = 0; index < 16 /*0x10*/; ++index)
      this.\u0023\u003DzNnNx0_c\u003D[index] = _param1[index];
  }

  public void \u0023\u003DzsNJdwlQ\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6,
    double _param7,
    double _param8,
    double _param9,
    double _param10,
    double _param11,
    double _param12,
    double _param13,
    double _param14,
    double _param15,
    double _param16)
  {
    int num1 = 0;
    double[] zNnNx0C1 = this.\u0023\u003DzNnNx0_c\u003D;
    int index1 = num1;
    int num2 = index1 + 1;
    double num3 = _param1;
    zNnNx0C1[index1] = num3;
    double[] zNnNx0C2 = this.\u0023\u003DzNnNx0_c\u003D;
    int index2 = num2;
    int num4 = index2 + 1;
    double num5 = _param2;
    zNnNx0C2[index2] = num5;
    double[] zNnNx0C3 = this.\u0023\u003DzNnNx0_c\u003D;
    int index3 = num4;
    int num6 = index3 + 1;
    double num7 = _param3;
    zNnNx0C3[index3] = num7;
    double[] zNnNx0C4 = this.\u0023\u003DzNnNx0_c\u003D;
    int index4 = num6;
    int num8 = index4 + 1;
    double num9 = _param4;
    zNnNx0C4[index4] = num9;
    double[] zNnNx0C5 = this.\u0023\u003DzNnNx0_c\u003D;
    int index5 = num8;
    int num10 = index5 + 1;
    double num11 = _param5;
    zNnNx0C5[index5] = num11;
    double[] zNnNx0C6 = this.\u0023\u003DzNnNx0_c\u003D;
    int index6 = num10;
    int num12 = index6 + 1;
    double num13 = _param6;
    zNnNx0C6[index6] = num13;
    double[] zNnNx0C7 = this.\u0023\u003DzNnNx0_c\u003D;
    int index7 = num12;
    int num14 = index7 + 1;
    double num15 = _param7;
    zNnNx0C7[index7] = num15;
    double[] zNnNx0C8 = this.\u0023\u003DzNnNx0_c\u003D;
    int index8 = num14;
    int num16 = index8 + 1;
    double num17 = _param8;
    zNnNx0C8[index8] = num17;
    double[] zNnNx0C9 = this.\u0023\u003DzNnNx0_c\u003D;
    int index9 = num16;
    int num18 = index9 + 1;
    double num19 = _param9;
    zNnNx0C9[index9] = num19;
    double[] zNnNx0C10 = this.\u0023\u003DzNnNx0_c\u003D;
    int index10 = num18;
    int num20 = index10 + 1;
    double num21 = _param10;
    zNnNx0C10[index10] = num21;
    double[] zNnNx0C11 = this.\u0023\u003DzNnNx0_c\u003D;
    int index11 = num20;
    int num22 = index11 + 1;
    double num23 = _param11;
    zNnNx0C11[index11] = num23;
    double[] zNnNx0C12 = this.\u0023\u003DzNnNx0_c\u003D;
    int index12 = num22;
    int num24 = index12 + 1;
    double num25 = _param12;
    zNnNx0C12[index12] = num25;
    double[] zNnNx0C13 = this.\u0023\u003DzNnNx0_c\u003D;
    int index13 = num24;
    int num26 = index13 + 1;
    double num27 = _param13;
    zNnNx0C13[index13] = num27;
    double[] zNnNx0C14 = this.\u0023\u003DzNnNx0_c\u003D;
    int index14 = num26;
    int num28 = index14 + 1;
    double num29 = _param14;
    zNnNx0C14[index14] = num29;
    double[] zNnNx0C15 = this.\u0023\u003DzNnNx0_c\u003D;
    int index15 = num28;
    int num30 = index15 + 1;
    double num31 = _param15;
    zNnNx0C15[index15] = num31;
    double[] zNnNx0C16 = this.\u0023\u003DzNnNx0_c\u003D;
    int index16 = num30;
    int num32 = index16 + 1;
    double num33 = _param16;
    zNnNx0C16[index16] = num33;
  }

  public void \u0023\u003DzsNJdwlQ\u003D(
    float _param1,
    float _param2,
    float _param3,
    float _param4,
    float _param5,
    float _param6,
    float _param7,
    float _param8,
    float _param9,
    float _param10,
    float _param11,
    float _param12,
    float _param13,
    float _param14,
    float _param15,
    float _param16)
  {
    this.\u0023\u003DzsNJdwlQ\u003D((double) _param1, (double) _param2, (double) _param3, (double) _param4, (double) _param5, (double) _param6, (double) _param7, (double) _param8, (double) _param9, (double) _param10, (double) _param11, (double) _param12, (double) _param13, (double) _param14, (double) _param15, (double) _param16);
  }

  public double[] \u0023\u003DzL8atuyQ\u003D() => this.\u0023\u003DzNnNx0_c\u003D;

  public override string ToString()
  {
    string str = XXX.SSS(-539321775);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      str = str + this.\u0023\u003Dz\u0024CeUvME\u003D(index).ToString() + XXX.SSS(-539427378);
    return str + XXX.SSS(-539432576);
  }
}
