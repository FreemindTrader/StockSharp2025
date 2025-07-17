// Decompiled with JetBrains decompiler
// Type: #=zmKAFQoddoidM6qzDBEHa4ri2MSuQ0yetOTSoJgIvU2aq4D0lUbhHhxGL2gv6t6L7uw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class VectorClipper
{
  public RectangleInt clipBox;
  private int m_x1;
  private int m_y1;
  private int m_f1;
  private bool m_clipping;

  public VectorClipper()
  {
    this.clipBox = new RectangleInt(0, 0, 0, 0);
    this.m_x1 = 0;
    this.m_y1 = 0;
    this.m_f1 = 0;
    this.m_clipping = false;
  }

  private int \u0023\u003DzszKV14r04GDT(double _param1, double _param2, double _param3)
  {
    return agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param1 * _param2 / _param3);
  }

  private int \u0023\u003DzNPTxtnc\u003D(int _param1) => _param1;

  private int \u0023\u003DzmiJPYwQ\u003D(int _param1) => _param1;

  public int \u0023\u003DzE77D4bxULcsn(double _param1)
  {
    return agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param1 * 256.0);
  }

  public int \u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(int _param1) => _param1 / 256 /*0x0100*/;

  public void \u0023\u003DzThyAY8tY1g4ajpiZ6g\u003D\u003D()
  {
    this.m_clipping = false;
  }

  public void \u0023\u003DzbRHAWK1yd7\u00242(int _param1, int _param2, int _param3, int _param4)
  {
    this.clipBox = new RectangleInt(_param1, _param2, _param3, _param4);
    this.clipBox.\u0023\u003DzO3\u0024NMwQ\u003D();
    this.m_clipping = true;
  }

  public void \u0023\u003Dz_udhSki6JNxe(int _param1, int _param2)
  {
    this.m_x1 = _param1;
    this.m_y1 = _param2;
    if (!this.m_clipping)
      return;
    this.m_f1 = ClipLiangBarsky.\u0023\u003DziL6rhu76I9knzlgj8g\u003D\u003D(_param1, _param2, this.clipBox);
  }

  private void \u0023\u003DzTTJvOqaGuxjW(
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiZot20XZZLU_SBymsgRVXpM61Ax5QggCbu8 _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7)
  {
    _param6 &= 10;
    _param7 &= 10;
    if ((_param6 | _param7) == 0)
    {
      _param1.\u0023\u003Dz6WCDXg8\u003D(_param2, _param3, _param4, _param5);
    }
    else
    {
      if (_param6 == _param7)
        return;
      int num1 = _param2;
      int num2 = _param3;
      int num3 = _param4;
      int num4 = _param5;
      if ((_param6 & 8) != 0)
      {
        num1 = _param2 + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzRNV_Dpk\u003D - _param3), (double) (_param4 - _param2), (double) (_param5 - _param3));
        num2 = this.clipBox.\u0023\u003DzRNV_Dpk\u003D;
      }
      if ((_param6 & 2) != 0)
      {
        num1 = _param2 + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzSzOWcj8\u003D - _param3), (double) (_param4 - _param2), (double) (_param5 - _param3));
        num2 = this.clipBox.\u0023\u003DzSzOWcj8\u003D;
      }
      if ((_param7 & 8) != 0)
      {
        num3 = _param2 + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzRNV_Dpk\u003D - _param3), (double) (_param4 - _param2), (double) (_param5 - _param3));
        num4 = this.clipBox.\u0023\u003DzRNV_Dpk\u003D;
      }
      if ((_param7 & 2) != 0)
      {
        num3 = _param2 + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzSzOWcj8\u003D - _param3), (double) (_param4 - _param2), (double) (_param5 - _param3));
        num4 = this.clipBox.\u0023\u003DzSzOWcj8\u003D;
      }
      _param1.\u0023\u003Dz6WCDXg8\u003D(num1, num2, num3, num4);
    }
  }

  public void \u0023\u003DzZi1kLvzptRpH(
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiZot20XZZLU_SBymsgRVXpM61Ax5QggCbu8 _param1,
    int _param2,
    int _param3)
  {
    if (this.m_clipping)
    {
      int num1 = ClipLiangBarsky.\u0023\u003DziL6rhu76I9knzlgj8g\u003D\u003D(_param2, _param3, this.clipBox);
      if ((this.m_f1 & 10) == (num1 & 10) && (this.m_f1 & 10) != 0)
      {
        this.m_x1 = _param2;
        this.m_y1 = _param3;
        this.m_f1 = num1;
        return;
      }
      int zrQge4cfoOivd = this.m_x1;
      int z3TqoxnnGheBc = this.m_y1;
      int z3Isqyoim1Fc9 = this.m_f1;
      switch ((z3Isqyoim1Fc9 & 5) << 1 | num1 & 5)
      {
        case 0:
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, zrQge4cfoOivd, z3TqoxnnGheBc, _param2, _param3, z3Isqyoim1Fc9, num1);
          break;
        case 1:
          int num2 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003Dzp55dtus\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num3 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num2, this.clipBox);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, zrQge4cfoOivd, z3TqoxnnGheBc, this.clipBox.\u0023\u003Dzp55dtus\u003D, num2, z3Isqyoim1Fc9, num3);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003Dzp55dtus\u003D, num2, this.clipBox.\u0023\u003Dzp55dtus\u003D, _param3, num3, num1);
          break;
        case 2:
          int num4 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003Dzp55dtus\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num5 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num4, this.clipBox);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003Dzp55dtus\u003D, z3TqoxnnGheBc, this.clipBox.\u0023\u003Dzp55dtus\u003D, num4, z3Isqyoim1Fc9, num5);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003Dzp55dtus\u003D, num4, _param2, _param3, num5, num1);
          break;
        case 3:
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003Dzp55dtus\u003D, z3TqoxnnGheBc, this.clipBox.\u0023\u003Dzp55dtus\u003D, _param3, z3Isqyoim1Fc9, num1);
          break;
        case 4:
          int num6 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzP4R7yU0\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num7 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num6, this.clipBox);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, zrQge4cfoOivd, z3TqoxnnGheBc, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num6, z3Isqyoim1Fc9, num7);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num6, this.clipBox.\u0023\u003DzP4R7yU0\u003D, _param3, num7, num1);
          break;
        case 6:
          int num8 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003Dzp55dtus\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num9 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzP4R7yU0\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num10 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num8, this.clipBox);
          int num11 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num9, this.clipBox);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003Dzp55dtus\u003D, z3TqoxnnGheBc, this.clipBox.\u0023\u003Dzp55dtus\u003D, num8, z3Isqyoim1Fc9, num10);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003Dzp55dtus\u003D, num8, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num9, num10, num11);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num9, this.clipBox.\u0023\u003DzP4R7yU0\u003D, _param3, num11, num1);
          break;
        case 8:
          int num12 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzP4R7yU0\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num13 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num12, this.clipBox);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003DzP4R7yU0\u003D, z3TqoxnnGheBc, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num12, z3Isqyoim1Fc9, num13);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num12, _param2, _param3, num13, num1);
          break;
        case 9:
          int num14 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003DzP4R7yU0\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num15 = z3TqoxnnGheBc + this.\u0023\u003DzszKV14r04GDT((double) (this.clipBox.\u0023\u003Dzp55dtus\u003D - zrQge4cfoOivd), (double) (_param3 - z3TqoxnnGheBc), (double) (_param2 - zrQge4cfoOivd));
          int num16 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num14, this.clipBox);
          int num17 = ClipLiangBarsky.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(num15, this.clipBox);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003DzP4R7yU0\u003D, z3TqoxnnGheBc, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num14, z3Isqyoim1Fc9, num16);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003DzP4R7yU0\u003D, num14, this.clipBox.\u0023\u003Dzp55dtus\u003D, num15, num16, num17);
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003Dzp55dtus\u003D, num15, this.clipBox.\u0023\u003Dzp55dtus\u003D, _param3, num17, num1);
          break;
        case 12:
          this.\u0023\u003DzTTJvOqaGuxjW(_param1, this.clipBox.\u0023\u003DzP4R7yU0\u003D, z3TqoxnnGheBc, this.clipBox.\u0023\u003DzP4R7yU0\u003D, _param3, z3Isqyoim1Fc9, num1);
          break;
      }
      this.m_f1 = num1;
    }
    else
      _param1.\u0023\u003Dz6WCDXg8\u003D(this.m_x1, this.m_y1, _param2, _param3);
    this.m_x1 = _param2;
    this.m_y1 = _param3;
  }
}
