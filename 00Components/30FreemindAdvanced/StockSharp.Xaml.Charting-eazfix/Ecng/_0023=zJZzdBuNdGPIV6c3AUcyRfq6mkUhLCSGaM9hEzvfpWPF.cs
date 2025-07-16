// Decompiled with JetBrains decompiler
// Type: #=zJZzdBuNdGPIV6c3AUcyRfq6mkUhLCSGaM9hEzvfpWPF8jLd2hBy3qYrBbGeaq3ALH7Dy$K8vYYkrb7YqTQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfq6mkUhLCSGaM9hEzvfpWPF8jLd2hBy3qYrBbGeaq3ALH7Dy\u0024K8vYYkrb7YqTQ\u003D\u003D : 
  \u0023\u003Dz5bjCWzBDiPRPmitpfhC_MmKR1q6GmPzdlRTagZHczYCX8eYbC_HrGI6Ex2\u0024qv61DtjoZcsxDvs3psehDVA\u003D\u003D
{
  private \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwlFaPSU9mAhCXhd850Mu3EmQQSEd_WgxzFpokOE4XAGw8JoIvJCUTKJY \u0023\u003DzSOTpUQsvWXTt;

  public \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfq6mkUhLCSGaM9hEzvfpWPF8jLd2hBy3qYrBbGeaq3ALH7Dy\u0024K8vYYkrb7YqTQ\u003D\u003D(
    \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl22T3I94knCn3cCNsJ0Q _param1,
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param2,
    int _param3,
    int _param4)
    : base(_param1, _param2)
  {
    this.\u0023\u003DzSOTpUQsvWXTt = new \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwlFaPSU9mAhCXhd850Mu3EmQQSEd_WgxzFpokOE4XAGw8JoIvJCUTKJY(_param2.\u0023\u003DzLNbWazE\u003D, _param2.\u0023\u003DzrS5XlCc\u003D, _param2.\u0023\u003DzxpGWEWQ\u003D, _param2.\u0023\u003Dz3MxuAfw\u003D, _param3, _param4, _param2.\u0023\u003DzLNbWazE\u003D & -256, _param2.\u0023\u003DzrS5XlCc\u003D & -256, 0);
    this.\u0023\u003Dzfqm94hc4z54F.\u0023\u003Dz49sA\u0024Ar4vyK1();
    this.\u0023\u003DzWiFSa2\u0024o8viR -= this.\u0023\u003Dz_xUr8LVeoiM9;
  }

  public bool \u0023\u003Dz8dQjZ7dJbvMf()
  {
    int num1 = this.\u0023\u003Dz1o2s1sdZlj2YzZ9Yug\u003D\u003D(this.\u0023\u003DzSOTpUQsvWXTt);
    int index1 = 66;
    int index2 = index1;
    int num2 = this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003DzBQTVifG6hqQA();
    int num3 = 0;
    this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index2] = (byte) 0;
    if (num2 > 0)
    {
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index2] = (byte) this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dzu6bzeD\u0024Y6N55(num1);
      ++num3;
    }
    int index3 = index2 + 1;
    int num4;
    for (int index4 = 1; (num4 = this.\u0023\u003DzwmOOME4syT6R[index4] - num1) <= this.\u0023\u003DzfKsXfd89zAXB; ++index4)
    {
      num2 -= this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003Dz75_FsdQYholb();
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index3] = (byte) 0;
      if (num2 > 0)
      {
        this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index3] = (byte) this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dzu6bzeD\u0024Y6N55(num4);
        ++num3;
      }
      ++index3;
    }
    int index5 = 1;
    int num5 = this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003DzBQTVifG6hqQA();
    int num6;
    for (; (num6 = this.\u0023\u003DzwmOOME4syT6R[index5] + num1) <= this.\u0023\u003DzfKsXfd89zAXB; ++index5)
    {
      num5 += this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003Dz75_FsdQYholb();
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[--index1] = (byte) 0;
      if (num5 > 0)
      {
        this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index1] = (byte) this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dzu6bzeD\u0024Y6N55(num6);
        ++num3;
      }
    }
    this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dz5yPQgEdavuyT81_RwrDy\u002430AFlUD(this.\u0023\u003DzI6P8IpE\u003D, this.\u0023\u003DzFfSb8y0\u003D - index5 + 1, index3 - index1, this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D, index1);
    if (num3 == 0)
      return false;
    return ++this.\u0023\u003DzWiFSa2\u0024o8viR < this.\u0023\u003Dzu2nmf\u0024h1W4Aj;
  }

  public bool \u0023\u003DzVJres4mgxPc\u0024()
  {
    int num1 = this.\u0023\u003DzYPKD4XuCi9J1ac\u0024eBA\u003D\u003D(this.\u0023\u003DzSOTpUQsvWXTt);
    int index1 = 66;
    int index2 = index1;
    int num2 = this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003DzBQTVifG6hqQA();
    int num3 = 0;
    this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index2] = (byte) 0;
    if (num2 > 0)
    {
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index2] = (byte) this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dzu6bzeD\u0024Y6N55(num1);
      ++num3;
    }
    int index3 = index2 + 1;
    int num4;
    for (int index4 = 1; (num4 = this.\u0023\u003DzwmOOME4syT6R[index4] - num1) <= this.\u0023\u003DzfKsXfd89zAXB; ++index4)
    {
      num2 += this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003DzmUsCKr7zf\u0024c\u0024();
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index3] = (byte) 0;
      if (num2 > 0)
      {
        this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index3] = (byte) this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dzu6bzeD\u0024Y6N55(num4);
        ++num3;
      }
      ++index3;
    }
    int index5 = 1;
    int num5 = this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003DzBQTVifG6hqQA();
    int num6;
    for (; (num6 = this.\u0023\u003DzwmOOME4syT6R[index5] + num1) <= this.\u0023\u003DzfKsXfd89zAXB; ++index5)
    {
      num5 -= this.\u0023\u003DzSOTpUQsvWXTt.\u0023\u003DzmUsCKr7zf\u0024c\u0024();
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[--index1] = (byte) 0;
      if (num5 > 0)
      {
        this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[index1] = (byte) this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dzu6bzeD\u0024Y6N55(num6);
        ++num3;
      }
    }
    this.\u0023\u003DzegTLEtYkn1Xk.\u0023\u003Dz\u00244g26jU6qlj5jJbMN_d_Aru2N2xf(this.\u0023\u003DzI6P8IpE\u003D - index5 + 1, this.\u0023\u003DzFfSb8y0\u003D, index3 - index1, this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D, index1);
    if (num3 == 0)
      return false;
    return ++this.\u0023\u003DzWiFSa2\u0024o8viR < this.\u0023\u003Dzu2nmf\u0024h1W4Aj;
  }
}
