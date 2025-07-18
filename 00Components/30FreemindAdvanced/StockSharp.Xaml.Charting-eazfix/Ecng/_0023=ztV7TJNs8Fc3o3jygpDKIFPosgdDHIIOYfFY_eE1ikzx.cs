// Decompiled with JetBrains decompiler
// Type: #=ztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_$fe3jeL_uz4nXi9f
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
public sealed class \u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_\u0024fe3jeL_uz4nXi9f : 
  IRasterizer
{
  private \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiZot20XZZLU_SBymsgRVXpM61Ax5QggCbu8 \u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D;
  private VectorClipper \u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D;
  private agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D \u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D;
  private \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpZyApi09lYaLQFtlVvwH2DkCcJeAzrFeePM8kxIf8fDEKw\u003D\u003D \u0023\u003DzqmBOXY7JRTb_;
  private \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_\u0024fe3jeL_uz4nXi9f.\u0023\u003Dzh7UcPPSzOf\u0024r> \u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D;
  private \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<int> \u0023\u003DzAcdr\u0024aGY3FP_;
  private \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<byte> \u0023\u003DzkUtXtdQftHJf;
  private \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb> \u0023\u003DzHkfwL3cpFpGk;
  private \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<byte> \u0023\u003DzVz5XHFr9aa4LdiR5pA\u003D\u003D;
  private \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<int> \u0023\u003DzCtECCbNdmyUP;
  private int \u0023\u003DzG6hzbfumsP2R;
  private int \u0023\u003Dz2kmSOqbKvtak;
  private int \u0023\u003Dz0TSvb7PdghSI;
  private int \u0023\u003Dzm1Oo\u002446Mw4uy;
  private int \u0023\u003DzaytbZH\u00245LIlA;
  private int \u0023\u003Dz\u0024eEiYuO6H7my;
  private int \u0023\u003DzCHZsvpDqpNw9;

  public \u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_\u0024fe3jeL_uz4nXi9f()
  {
    this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiZot20XZZLU_SBymsgRVXpM61Ax5QggCbu8();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D = new VectorClipper();
    this.\u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D = (agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D) 0;
    this.\u0023\u003DzqmBOXY7JRTb_ = (\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpZyApi09lYaLQFtlVvwH2DkCcJeAzrFeePM8kxIf8fDEKw\u003D\u003D) 1;
    this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D = new \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_\u0024fe3jeL_uz4nXi9f.\u0023\u003Dzh7UcPPSzOf\u0024r>();
    this.\u0023\u003DzAcdr\u0024aGY3FP_ = new \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<int>();
    this.\u0023\u003DzkUtXtdQftHJf = new \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<byte>();
    this.\u0023\u003DzHkfwL3cpFpGk = new \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb>();
    this.\u0023\u003DzVz5XHFr9aa4LdiR5pA\u003D\u003D = new \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<byte>();
    this.\u0023\u003DzCtECCbNdmyUP = new \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<int>();
    this.\u0023\u003DzG6hzbfumsP2R = int.MaxValue;
    this.\u0023\u003Dz2kmSOqbKvtak = -2147483647 /*0x80000001*/;
    this.\u0023\u003Dz0TSvb7PdghSI = 0;
    this.\u0023\u003Dzm1Oo\u002446Mw4uy = 0;
    this.\u0023\u003DzaytbZH\u00245LIlA = int.MaxValue;
    this.\u0023\u003Dz\u0024eEiYuO6H7my = 0;
    this.\u0023\u003DzCHZsvpDqpNw9 = 0;
  }

  public void \u0023\u003DzruxDfy35wG0J(
    IGammaFunction _param1)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dzp_DWHgc\u003D()
  {
    this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DzG6hzbfumsP2R = int.MaxValue;
    this.\u0023\u003Dz2kmSOqbKvtak = -2147483647 /*0x80000001*/;
    this.\u0023\u003DzaytbZH\u00245LIlA = int.MaxValue;
    this.\u0023\u003Dz\u0024eEiYuO6H7my = 0;
    this.\u0023\u003DzCHZsvpDqpNw9 = 0;
  }

  private void \u0023\u003Dz9oma1EeDllFlgo5SLQ\u003D\u003D(
    agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D _param1)
  {
    this.\u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D = _param1;
  }

  private void \u0023\u003DzcT6YUp4dmDst(
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpZyApi09lYaLQFtlVvwH2DkCcJeAzrFeePM8kxIf8fDEKw\u003D\u003D _param1)
  {
    this.\u0023\u003DzqmBOXY7JRTb_ = _param1;
  }

  private void \u0023\u003DzbRHAWK1yd7\u00242(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzbRHAWK1yd7\u00242(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param3), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param4));
  }

  private void \u0023\u003DzThyAY8tY1g4ajpiZ6g\u003D\u003D()
  {
    this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzThyAY8tY1g4ajpiZ6g\u003D\u003D();
  }

  public void \u0023\u003Dz5KL\u0024hdjZSRdc(int _param1, int _param2)
  {
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb quLbcdO8tFdwl0Nmb = new \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb();
    quLbcdO8tFdwl0Nmb.\u0023\u003DzIT\u0024DfT8\u003D();
    quLbcdO8tFdwl0Nmb.\u0023\u003DzLw_1RqQ\u003D = _param1;
    quLbcdO8tFdwl0Nmb.\u0023\u003DzTgDnxCg\u003D = _param2;
    this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzgQgmhv4\u003D(quLbcdO8tFdwl0Nmb);
    if (_param1 >= 0 && _param1 < this.\u0023\u003DzG6hzbfumsP2R)
      this.\u0023\u003DzG6hzbfumsP2R = _param1;
    if (_param1 >= 0 && _param1 > this.\u0023\u003Dz2kmSOqbKvtak)
      this.\u0023\u003Dz2kmSOqbKvtak = _param1;
    if (_param2 >= 0 && _param2 < this.\u0023\u003DzG6hzbfumsP2R)
      this.\u0023\u003DzG6hzbfumsP2R = _param2;
    if (_param2 < 0 || _param2 <= this.\u0023\u003Dz2kmSOqbKvtak)
      return;
    this.\u0023\u003Dz2kmSOqbKvtak = _param2;
  }

  public void \u0023\u003Dz_udhSki6JNxe(int _param1, int _param2)
  {
    if (this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003Dz0TSvb7PdghSI = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param1), this.\u0023\u003Dzm1Oo\u002446Mw4uy = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param2));
  }

  public void \u0023\u003DzZi1kLvzptRpH(int _param1, int _param2)
  {
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param2));
  }

  public void \u0023\u003DzAcNdpbnU8gEt(double _param1, double _param2)
  {
    if (this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003Dz0TSvb7PdghSI = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003Dzm1Oo\u002446Mw4uy = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2));
  }

  public void \u0023\u003DzzwT9Ppmp4oNY(double _param1, double _param2)
  {
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2));
  }

  private void \u0023\u003DzNVaj9ergOekrrLbWLQ\u003D\u003D(
    double _param1,
    double _param2,
    Path.\u0023\u003Dz9kUnn38\u003D _param3)
  {
    if (Path.\u0023\u003DzxoYPNFH0kpDd(_param3))
      this.\u0023\u003DzAcNdpbnU8gEt(_param1, _param2);
    else if (Path.\u0023\u003DzepfxPD_ghBSfgm\u0024Sfw\u003D\u003D(_param3))
    {
      this.\u0023\u003DzzwT9Ppmp4oNY(_param1, _param2);
    }
    else
    {
      if (!Path.\u0023\u003DzsnnNWhx8JhO_(_param3))
        return;
      this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D, this.\u0023\u003Dz0TSvb7PdghSI, this.\u0023\u003Dzm1Oo\u002446Mw4uy);
    }
  }

  private void \u0023\u003DzDV2n1hE\u003D(int _param1, int _param2, int _param3, int _param4)
  {
    if (this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param2));
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param3), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param4));
  }

  private void \u0023\u003DztdxBDu6EtVY5(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    if (this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2));
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param3), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param4));
  }

  private void \u0023\u003Dzmt1LDos\u003D()
  {
    this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzTwhqVHorS0SA();
  }

  public bool \u0023\u003DzptZK7icdgElf2pwnqtUfz44glEoM()
  {
    this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzTwhqVHorS0SA();
    if (this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzDfpyjYuVSpvp() == 0 || this.\u0023\u003Dz2kmSOqbKvtak < this.\u0023\u003DzG6hzbfumsP2R)
      return false;
    this.\u0023\u003DzaytbZH\u00245LIlA = this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003Dz1IC3EdsxAbMh();
    this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D.\u0023\u003DzWIEZ7Zw\u003D(this.\u0023\u003Dz2kmSOqbKvtak - this.\u0023\u003DzG6hzbfumsP2R + 2, 128 /*0x80*/);
    this.\u0023\u003DzfKTCFJPIBiBuda2dMgXAhSc\u003D();
    return true;
  }

  public int \u0023\u003DzpcCgt8rWqHN2twu8sbfNfrI\u003D()
  {
    for (; this.\u0023\u003DzaytbZH\u00245LIlA <= this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzF\u0024mRZMMmE83X(); ++this.\u0023\u003DzaytbZH\u00245LIlA)
    {
      int num1 = this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzkhL6vQ3dVWuWmnG7awTRHY0\u003D(this.\u0023\u003DzaytbZH\u00245LIlA);
      int index1 = 0;
      \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb[] quLbcdO8tFdwl0NmbArray;
      this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzM55RxAl19tGHk1q0ZA\u003D\u003D(this.\u0023\u003DzaytbZH\u00245LIlA, out quLbcdO8tFdwl0NmbArray, out index1);
      int num2 = this.\u0023\u003Dz2kmSOqbKvtak - this.\u0023\u003DzG6hzbfumsP2R + 2;
      int index2 = 0;
      this.\u0023\u003DzHkfwL3cpFpGk.\u0023\u003DzWIEZ7Zw\u003D(num1 * 2, 256 /*0x0100*/);
      this.\u0023\u003DzAcdr\u0024aGY3FP_.\u0023\u003Dz0WRhgbw\u003D(num2, 64 /*0x40*/);
      this.\u0023\u003DzkUtXtdQftHJf.\u0023\u003DzWIEZ7Zw\u003D(num2 + 7 >> 3, 8);
      this.\u0023\u003DzkUtXtdQftHJf.\u0023\u003DzSdOfhoo\u003D();
      if (num1 > 0)
      {
        this.\u0023\u003DzkUtXtdQftHJf.\u0023\u003DzvsnCYl4\u003D()[0] |= (byte) 1;
        this.\u0023\u003DzAcdr\u0024aGY3FP_.\u0023\u003DzObQSsmE\u003D(0);
        this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D.\u0023\u003DzvsnCYl4\u003D()[index2].\u0023\u003Dz7D88q7f7iugS = 0;
        this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D.\u0023\u003DzvsnCYl4\u003D()[index2].\u0023\u003DzJ_uAqhdJETQX = 0;
        this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D.\u0023\u003DzvsnCYl4\u003D()[index2].\u0023\u003DzH64ouBYJK6cR = -2147483647 /*0x80000001*/;
        this.\u0023\u003Dz\u0024eEiYuO6H7my = quLbcdO8tFdwl0NmbArray[0].\u0023\u003DzwP120vA\u003D;
        this.\u0023\u003DzCHZsvpDqpNw9 = quLbcdO8tFdwl0NmbArray[num1 - 1].\u0023\u003DzwP120vA\u003D - this.\u0023\u003Dz\u0024eEiYuO6H7my + 1;
        while (num1-- != 0)
        {
          int index3 = index1++;
          this.\u0023\u003Dzo5JHwYKBj5VR(quLbcdO8tFdwl0NmbArray[index3].\u0023\u003DzLw_1RqQ\u003D);
          this.\u0023\u003Dzo5JHwYKBj5VR(quLbcdO8tFdwl0NmbArray[index3].\u0023\u003DzTgDnxCg\u003D);
        }
        int num3 = 0;
        \u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_\u0024fe3jeL_uz4nXi9f.\u0023\u003Dzh7UcPPSzOf\u0024r[] zh7UcPpSzOfRArray = this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D.\u0023\u003DzvsnCYl4\u003D();
        for (int index4 = 0; index4 < this.\u0023\u003DzAcdr\u0024aGY3FP_.\u0023\u003DzG2qqjnQ\u003D(); ++index4)
        {
          int index5 = this.\u0023\u003DzAcdr\u0024aGY3FP_[index4];
          int z7D88q7f7iugS = zh7UcPpSzOfRArray[index5].\u0023\u003Dz7D88q7f7iugS;
          zh7UcPpSzOfRArray[index5].\u0023\u003Dz7D88q7f7iugS = num3;
          num3 += z7D88q7f7iugS;
        }
        int num4 = this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzkhL6vQ3dVWuWmnG7awTRHY0\u003D(this.\u0023\u003DzaytbZH\u00245LIlA);
        this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzM55RxAl19tGHk1q0ZA\u003D\u003D(this.\u0023\u003DzaytbZH\u00245LIlA, out quLbcdO8tFdwl0NmbArray, out index1);
        while (num4-- > 0)
        {
          int index6 = index1++;
          int index7 = quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzLw_1RqQ\u003D < 0 ? 0 : quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzLw_1RqQ\u003D - this.\u0023\u003DzG6hzbfumsP2R + 1;
          if (quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzwP120vA\u003D == zh7UcPpSzOfRArray[index7].\u0023\u003DzH64ouBYJK6cR)
          {
            index1 = zh7UcPpSzOfRArray[index7].\u0023\u003Dz7D88q7f7iugS + zh7UcPpSzOfRArray[index7].\u0023\u003DzJ_uAqhdJETQX - 1;
            quLbcdO8tFdwl0NmbArray[index1]._chartArea_093 += quLbcdO8tFdwl0NmbArray[index6]._chartArea_093;
            quLbcdO8tFdwl0NmbArray[index1].\u0023\u003Dzu6bzeD\u0024Y6N55 += quLbcdO8tFdwl0NmbArray[index6].\u0023\u003Dzu6bzeD\u0024Y6N55;
          }
          else
          {
            index1 = zh7UcPpSzOfRArray[index7].\u0023\u003Dz7D88q7f7iugS + zh7UcPpSzOfRArray[index7].\u0023\u003DzJ_uAqhdJETQX;
            quLbcdO8tFdwl0NmbArray[index1].\u0023\u003DzwP120vA\u003D = quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzwP120vA\u003D;
            quLbcdO8tFdwl0NmbArray[index1]._chartArea_093 = quLbcdO8tFdwl0NmbArray[index6]._chartArea_093;
            quLbcdO8tFdwl0NmbArray[index1].\u0023\u003Dzu6bzeD\u0024Y6N55 = quLbcdO8tFdwl0NmbArray[index6].\u0023\u003Dzu6bzeD\u0024Y6N55;
            zh7UcPpSzOfRArray[index7].\u0023\u003DzH64ouBYJK6cR = quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzwP120vA\u003D;
            ++zh7UcPpSzOfRArray[index7].\u0023\u003DzJ_uAqhdJETQX;
          }
          int index8 = quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzTgDnxCg\u003D < 0 ? 0 : quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzTgDnxCg\u003D - this.\u0023\u003DzG6hzbfumsP2R + 1;
          if (quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzwP120vA\u003D == zh7UcPpSzOfRArray[index8].\u0023\u003DzH64ouBYJK6cR)
          {
            index1 = zh7UcPpSzOfRArray[index8].\u0023\u003Dz7D88q7f7iugS + zh7UcPpSzOfRArray[index8].\u0023\u003DzJ_uAqhdJETQX - 1;
            quLbcdO8tFdwl0NmbArray[index1]._chartArea_093 -= quLbcdO8tFdwl0NmbArray[index6]._chartArea_093;
            quLbcdO8tFdwl0NmbArray[index1].\u0023\u003Dzu6bzeD\u0024Y6N55 -= quLbcdO8tFdwl0NmbArray[index6].\u0023\u003Dzu6bzeD\u0024Y6N55;
          }
          else
          {
            index1 = zh7UcPpSzOfRArray[index8].\u0023\u003Dz7D88q7f7iugS + zh7UcPpSzOfRArray[index8].\u0023\u003DzJ_uAqhdJETQX;
            quLbcdO8tFdwl0NmbArray[index1].\u0023\u003DzwP120vA\u003D = quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzwP120vA\u003D;
            quLbcdO8tFdwl0NmbArray[index1]._chartArea_093 = -quLbcdO8tFdwl0NmbArray[index6]._chartArea_093;
            quLbcdO8tFdwl0NmbArray[index1].\u0023\u003Dzu6bzeD\u0024Y6N55 = -quLbcdO8tFdwl0NmbArray[index6].\u0023\u003Dzu6bzeD\u0024Y6N55;
            zh7UcPpSzOfRArray[index8].\u0023\u003DzH64ouBYJK6cR = quLbcdO8tFdwl0NmbArray[index6].\u0023\u003DzwP120vA\u003D;
            ++zh7UcPpSzOfRArray[index8].\u0023\u003DzJ_uAqhdJETQX;
          }
        }
      }
      if (this.\u0023\u003DzAcdr\u0024aGY3FP_.\u0023\u003DzG2qqjnQ\u003D() > 1)
      {
        ++this.\u0023\u003DzaytbZH\u00245LIlA;
        if (this.\u0023\u003DzqmBOXY7JRTb_ != (\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpZyApi09lYaLQFtlVvwH2DkCcJeAzrFeePM8kxIf8fDEKw\u003D\u003D) 0)
        {
          \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM6z2sXOfdVyBoG70yPzUmVGknO_tSQ\u003D g70yPzUmVgknOTSq = new \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM6z2sXOfdVyBoG70yPzUmVGknO_tSQ\u003D(this.\u0023\u003DzAcdr\u0024aGY3FP_, 1, this.\u0023\u003DzAcdr\u0024aGY3FP_.\u0023\u003DzG2qqjnQ\u003D() - 1);
          if (this.\u0023\u003DzqmBOXY7JRTb_ != (\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpZyApi09lYaLQFtlVvwH2DkCcJeAzrFeePM8kxIf8fDEKw\u003D\u003D) 1)
            throw new NotImplementedException();
          new \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7aLdD0UYentUKC7Ct5IeKJc9rSTNMo\u0024\u0024niOEu7tzTkpfi3UFcQoi7feZ0sMWA\u003D\u003D().\u0023\u003DzSJ0vPIg\u003D(g70yPzUmVgknOTSq);
        }
        return this.\u0023\u003DzAcdr\u0024aGY3FP_.\u0023\u003DzG2qqjnQ\u003D() - 1;
      }
    }
    return 0;
  }

  public int \u0023\u003DzgQgmhv4\u003D(int _param1)
  {
    return this.\u0023\u003DzAcdr\u0024aGY3FP_[_param1 + 1] + this.\u0023\u003DzG6hzbfumsP2R - 1;
  }

  private bool \u0023\u003Dz0YELd17TIkydbHrlQg\u003D\u003D(int _param1)
  {
    this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzTwhqVHorS0SA();
    if (this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzDfpyjYuVSpvp() == 0 || this.\u0023\u003Dz2kmSOqbKvtak < this.\u0023\u003DzG6hzbfumsP2R || _param1 < this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003Dz1IC3EdsxAbMh() || _param1 > this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzF\u0024mRZMMmE83X())
      return false;
    this.\u0023\u003DzaytbZH\u00245LIlA = _param1;
    this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D.\u0023\u003DzWIEZ7Zw\u003D(this.\u0023\u003Dz2kmSOqbKvtak - this.\u0023\u003DzG6hzbfumsP2R + 2, 128 /*0x80*/);
    this.\u0023\u003DzfKTCFJPIBiBuda2dMgXAhSc\u003D();
    return true;
  }

  private bool \u0023\u003Dzc7hJRHEN9Ch4(int _param1, int _param2)
  {
    if (!this.\u0023\u003Dz0YELd17TIkydbHrlQg\u003D\u003D(_param2) || this.\u0023\u003DzpcCgt8rWqHN2twu8sbfNfrI\u003D() <= 0)
      return false;
    \u0023\u003Dz5hVyTN88kBn45NAfOxK7MGk1SFtL0qwPlUnZ4Oy87Hk5rX6EEv1tGN5zMVU1Lks\u0024ZKxvzS_NKEcJ u1LksZkxvzSNkEcJ = new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MGk1SFtL0qwPlUnZ4Oy87Hk5rX6EEv1tGN5zMVU1Lks\u0024ZKxvzS_NKEcJ(_param1);
    this.\u0023\u003DzVDkk5LAF0tc28W2lbQYHPVM\u003D((IScanlineCache) u1LksZkxvzSNkEcJ, -1);
    return u1LksZkxvzSNkEcJ.\u0023\u003DzMR0uGGc\u003D();
  }

  private byte[] \u0023\u003DzifQ\u0024fM3lwTmDl4W__d85PyzvjX9e(int _param1)
  {
    this.\u0023\u003DzVz5XHFr9aa4LdiR5pA\u003D\u003D.\u0023\u003DzWIEZ7Zw\u003D(_param1, 256 /*0x0100*/);
    return this.\u0023\u003DzVz5XHFr9aa4LdiR5pA\u003D\u003D.\u0023\u003DzvsnCYl4\u003D();
  }

  private void \u0023\u003Dzb7Noarrbd7qy(int _param1, double _param2)
  {
    if (_param1 < 0)
      return;
    while (this.\u0023\u003DzCtECCbNdmyUP.\u0023\u003DzG2qqjnQ\u003D() <= _param1)
      this.\u0023\u003DzCtECCbNdmyUP.\u0023\u003DzObQSsmE\u003D((int) byte.MaxValue);
    this.\u0023\u003DzCtECCbNdmyUP.\u0023\u003DzvsnCYl4\u003D()[_param1] = agg_basics.\u0023\u003DzROReRE0C5MV7(_param2 * (double) byte.MaxValue);
  }

  public void \u0023\u003DzR7pES1JXr5Q9(
    IVertexSource _param1)
  {
    this.\u0023\u003DzR7pES1JXr5Q9(_param1, 0);
  }

  public void \u0023\u003DzR7pES1JXr5Q9(
    IVertexSource _param1,
    int _param2)
  {
    _param1.\u0023\u003DzVawdK5C5Lyf_(_param2);
    if (this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    double num1;
    double num2;
    Path.\u0023\u003Dz9kUnn38\u003D z9kUnn38;
    while (!Path.\u0023\u003DzVHztYKNVoUMf(z9kUnn38 = _param1.\u0023\u003DzxfekdAs1X3YM(out num1, out num2)))
      this.\u0023\u003DzNVaj9ergOekrrLbWLQ\u003D\u003D(num1, num2, z9kUnn38);
  }

  public int \u0023\u003DztsdJUYff4ZID()
  {
    return this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DztsdJUYff4ZID();
  }

  public int \u0023\u003Dz1IC3EdsxAbMh()
  {
    return this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003Dz1IC3EdsxAbMh();
  }

  public int \u0023\u003DzZXzraonFIgZ4()
  {
    return this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzZXzraonFIgZ4();
  }

  public int \u0023\u003DzF\u0024mRZMMmE83X()
  {
    return this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzF\u0024mRZMMmE83X();
  }

  public int \u0023\u003Dz59_71Z8FSB4f() => this.\u0023\u003DzG6hzbfumsP2R;

  public int \u0023\u003DzECHHbiHrcwTs() => this.\u0023\u003Dz2kmSOqbKvtak;

  public int \u0023\u003Dzr3uOns6OXBEXnOpoUQ\u003D\u003D() => this.\u0023\u003Dz\u0024eEiYuO6H7my;

  public int \u0023\u003DzpBh7YCjlKzZVGbPm3Q\u003D\u003D() => this.\u0023\u003DzCHZsvpDqpNw9;

  public int \u0023\u003Dz8\u0024atMZyZXD32R_3joHEocOM\u003D(int _param1, int _param2)
  {
    int num = _param1 >> 9;
    if (num < 0)
      num = -num;
    if (this.\u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D == (agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D) 1)
    {
      num &= 511 /*0x01FF*/;
      if (num > 256 /*0x0100*/)
        num = 512 /*0x0200*/ - num;
    }
    if (num > (int) byte.MaxValue)
      num = (int) byte.MaxValue;
    return num * _param2 + (int) byte.MaxValue >> 8;
  }

  public bool \u0023\u003DzVDkk5LAF0tc28W2lbQYHPVM\u003D(
    IScanlineCache _param1)
  {
    throw new NotImplementedException();
  }

  public bool \u0023\u003DzVDkk5LAF0tc28W2lbQYHPVM\u003D(
    IScanlineCache _param1,
    int _param2)
  {
    int num1 = this.\u0023\u003DzaytbZH\u00245LIlA - 1;
    if (num1 > this.\u0023\u003DzScA4JFZguWA\u0024gb6WwSe_Dlw\u003D.\u0023\u003DzF\u0024mRZMMmE83X())
      return false;
    _param1.\u0023\u003Dz_qg8im_\u0024N_mX();
    int maxValue = (int) byte.MaxValue;
    if (_param2 < 0)
    {
      _param2 = 0;
    }
    else
    {
      ++_param2;
      maxValue = this.\u0023\u003DzCtECCbNdmyUP[this.\u0023\u003DzAcdr\u0024aGY3FP_[_param2] + this.\u0023\u003DzG6hzbfumsP2R - 1];
    }
    \u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_\u0024fe3jeL_uz4nXi9f.\u0023\u003Dzh7UcPPSzOf\u0024r zh7UcPpSzOfR = this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D[this.\u0023\u003DzAcdr\u0024aGY3FP_[_param2]];
    int zJUAqhdJetqx = zh7UcPpSzOfR.\u0023\u003DzJ_uAqhdJETQX;
    int z7D88q7f7iugS = zh7UcPpSzOfR.\u0023\u003Dz7D88q7f7iugS;
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb quLbcdO8tFdwl0Nmb = this.\u0023\u003DzHkfwL3cpFpGk[z7D88q7f7iugS];
    int num2 = 0;
    while (zJUAqhdJetqx-- != 0)
    {
      int zwP120vA = quLbcdO8tFdwl0Nmb.\u0023\u003DzwP120vA\u003D;
      int zy5Rews = quLbcdO8tFdwl0Nmb._chartArea_093;
      num2 += quLbcdO8tFdwl0Nmb.\u0023\u003Dzu6bzeD\u0024Y6N55;
      quLbcdO8tFdwl0Nmb = this.\u0023\u003DzHkfwL3cpFpGk[++z7D88q7f7iugS];
      if (zy5Rews != 0)
      {
        int num3 = this.\u0023\u003Dz8\u0024atMZyZXD32R_3joHEocOM\u003D((num2 << 9) - zy5Rews, maxValue);
        _param1.\u0023\u003Dzzb9PhvYi_sP8(zwP120vA, num3);
        ++zwP120vA;
      }
      if (zJUAqhdJetqx != 0 && quLbcdO8tFdwl0Nmb.\u0023\u003DzwP120vA\u003D > zwP120vA)
      {
        int num4 = this.\u0023\u003Dz8\u0024atMZyZXD32R_3joHEocOM\u003D(num2 << 9, maxValue);
        if (num4 != 0)
          _param1.\u0023\u003DzqJOc77USXqlC(zwP120vA, quLbcdO8tFdwl0Nmb.\u0023\u003DzwP120vA\u003D - zwP120vA, num4);
      }
    }
    if (_param1.\u0023\u003DzAK83aWWCVpNB() == 0)
      return false;
    _param1.\u0023\u003DzDoZMoNygQSg8PGaYqw\u003D\u003D(num1);
    return true;
  }

  private void \u0023\u003Dzo5JHwYKBj5VR(int _param1)
  {
    if (_param1 < 0)
      _param1 = 0;
    else
      _param1 -= this.\u0023\u003DzG6hzbfumsP2R - 1;
    int index = _param1 >> 3;
    int num = 1 << (_param1 & 7);
    \u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzx66dZwWE9pxbJ0DUw_\u0024fe3jeL_uz4nXi9f.\u0023\u003Dzh7UcPPSzOf\u0024r[] zh7UcPpSzOfRArray = this.\u0023\u003Dz5ugot49DgF0zyrgrMw\u003D\u003D.\u0023\u003DzvsnCYl4\u003D();
    if (((int) this.\u0023\u003DzkUtXtdQftHJf[index] & num) == 0)
    {
      this.\u0023\u003DzAcdr\u0024aGY3FP_.\u0023\u003DzObQSsmE\u003D(_param1);
      this.\u0023\u003DzkUtXtdQftHJf.\u0023\u003DzvsnCYl4\u003D()[index] |= (byte) num;
      zh7UcPpSzOfRArray[_param1].\u0023\u003Dz7D88q7f7iugS = 0;
      zh7UcPpSzOfRArray[_param1].\u0023\u003DzJ_uAqhdJETQX = 0;
      zh7UcPpSzOfRArray[_param1].\u0023\u003DzH64ouBYJK6cR = -2147483647 /*0x80000001*/;
    }
    ++zh7UcPpSzOfRArray[_param1].\u0023\u003Dz7D88q7f7iugS;
  }

  private void \u0023\u003DzfKTCFJPIBiBuda2dMgXAhSc\u003D()
  {
    while (this.\u0023\u003DzCtECCbNdmyUP.\u0023\u003DzG2qqjnQ\u003D() <= this.\u0023\u003Dz2kmSOqbKvtak)
      this.\u0023\u003DzCtECCbNdmyUP.\u0023\u003DzObQSsmE\u003D((int) byte.MaxValue);
  }

  private struct \u0023\u003Dzh7UcPPSzOf\u0024r
  {
    
    public int \u0023\u003Dz7D88q7f7iugS;
    
    public int \u0023\u003DzJ_uAqhdJETQX;
    
    public int \u0023\u003DzH64ouBYJK6cR;
  }
}
