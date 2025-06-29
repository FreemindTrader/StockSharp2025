// Decompiled with JetBrains decompiler
// Type: #=zVZAnYWMfoaQCzNrFMqw3u01lCRN7eX_KrOuehBwLhBVnxxjpqw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u01lCRN7eX_KrOuehBwLhBVnxxjpqw\u003D\u003D : 
  \u0023\u003DzdDznHH56iLab0VjufJI3RuJskE2fScparOvjg7O3kpseA96mXw\u003D\u003D
{
  private readonly int \u0023\u003DzEMN\u0024erw\u003D;
  private \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D \u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D;

  internal \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u01lCRN7eX_KrOuehBwLhBVnxxjpqw\u003D\u003D(
    int _param1,
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D _param2)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D((IComparable) _param1, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437192)).\u0023\u003DzIR3Z_Ken7pfcXCwNTw\u003D\u003D((IComparable) 2);
    this.\u0023\u003DzEMN\u0024erw\u003D = _param1;
    this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D = _param2;
  }

  [SpecialName]
  public int \u0023\u003Dz4EvVhZjEuLza() => this.\u0023\u003DzEMN\u0024erw\u003D;

  [SpecialName]
  public \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D \u0023\u003DzCRtaRLiel\u0024uAEyXXM45e2ic\u003D()
  {
    return this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D;
  }

  public bool \u0023\u003DzilOTiYzU6JIQ(
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param1,
    int _param2)
  {
    return this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D != 0 & _param1.Max - _param1.Min + 1 > this.\u0023\u003DzEMN\u0024erw\u003D * _param2;
  }

  public \u0023\u003DzdDznHH56iLab0VjufJI3RuJskE2fScparOvjg7O3kpseA96mXw\u003D\u003D \u0023\u003Dz8WtlTZw\u003D(
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D _param1)
  {
    this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D = _param1;
    return (\u0023\u003DzdDznHH56iLab0VjufJI3RuJskE2fScparOvjg7O3kpseA96mXw\u003D\u003D) this;
  }

  public IList \u0023\u003Dzb0NWmhityJKy(IList _param1, int _param2)
  {
    return this.\u0023\u003Dzb0NWmhityJKy(_param1, new \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D(0, _param1.Count - 1), _param2);
  }

  public IList \u0023\u003Dzb0NWmhityJKy(
    IList _param1,
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param2,
    int _param3)
  {
    if (!this.\u0023\u003DzilOTiYzU6JIQ(_param2, _param3))
    {
      int length = _param2.Max - _param2.Min + 1;
      if (length == _param1.Count)
        return _param1;
      double[] numArray = new double[length];
      int min = _param2.Min;
      for (int index = 0; index < length; ++index)
      {
        numArray[index] = Convert.ToDouble(_param1[min]);
        ++min;
      }
      return (IList) numArray;
    }
    if (this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D.ToString().StartsWith(\u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.MinMax.ToString()))
      return this.\u0023\u003Dz1rpjJBfXHa0JqL21PwrWh4g\u003D(_param1, _param2.Min, _param2.Max, _param3);
    if (this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D == \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Min)
      return this.\u0023\u003DzMtxfw8w31lZlw_y5Aw\u003D\u003D(_param1, _param2.Min, _param2.Max, _param3);
    if (this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D == \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Max)
      return this.\u0023\u003DzHGrIGwQMCG00iPvblg\u003D\u003D(_param1, _param2.Min, _param2.Max, _param3);
    if (this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D == \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Mid)
      return this.\u0023\u003DztccTtmWKXlfE6hK7TQ\u003D\u003D(_param1, _param2.Min, _param2.Max, _param3);
    throw new Exception(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437237), (object) this.\u0023\u003DzZFxstdRmX5NLF0pSzR4qYMQ\u003D));
  }

  private IList \u0023\u003Dz1rpjJBfXHa0JqL21PwrWh4g\u003D(
    IList _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    int num1 = _param3 - _param2 + 1;
    int num2 = this.\u0023\u003DzEMN\u0024erw\u003D * _param4;
    int num3 = num1 / num2;
    double[] numArray1 = new double[2 * num1 / num3];
    double val1_1 = double.MaxValue;
    double val1_2 = double.MinValue;
    int index1 = _param2;
    int num4 = 0;
    int index2 = 0;
    int num5;
    for (num5 = _param3 - num1 % num3; index1 <= num5; ++index1)
    {
      double val2 = ((IComparable) _param1[index1]).\u0023\u003Dzb9UCYbo\u003D();
      val1_1 = Math.Min(val1_1, val2);
      val1_2 = Math.Max(val1_2, val2);
      if (++num4 >= num3)
      {
        double[] numArray2 = numArray1;
        int index3 = index2;
        int num6 = index3 + 1;
        double num7 = val1_1;
        numArray2[index3] = num7;
        double[] numArray3 = numArray1;
        int index4 = num6;
        index2 = index4 + 1;
        double num8 = val1_2;
        numArray3[index4] = num8;
        val1_1 = double.MaxValue;
        val1_2 = double.MinValue;
        num4 = 0;
      }
    }
    if (_param3 != num5 && numArray1.Length > index2)
    {
      for (; index1 <= _param3; ++index1)
      {
        val1_1 = Math.Min(val1_1, (double) _param1[index1]);
        val1_2 = Math.Max(val1_2, (double) _param1[index1]);
      }
      numArray1[index2] = val1_1;
      numArray1[index2] = val1_2;
    }
    return (IList) numArray1;
  }

  private IList \u0023\u003DzHGrIGwQMCG00iPvblg\u003D\u003D(
    IList _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    int num1 = _param3 - _param2 + 1;
    int num2 = num1 / (this.\u0023\u003DzEMN\u0024erw\u003D * _param4);
    double[] numArray = new double[num1 / num2];
    double num3 = double.MinValue;
    int index = _param2;
    int num4 = 0;
    int num5 = 0;
    for (; index <= _param3; ++index)
    {
      double num6 = ((IComparable) _param1[index]).\u0023\u003Dzb9UCYbo\u003D();
      if (num6 > num3)
        num3 = num6;
      if (++num4 >= num2)
      {
        numArray[num5++] = num3;
        num3 = double.MinValue;
        num4 = 0;
      }
    }
    return (IList) numArray;
  }

  private IList \u0023\u003DzMtxfw8w31lZlw_y5Aw\u003D\u003D(
    IList _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    int num1 = _param3 - _param2 + 1;
    int num2 = num1 / (this.\u0023\u003DzEMN\u0024erw\u003D * _param4);
    double[] numArray = new double[num1 / num2];
    double num3 = double.MaxValue;
    int index = _param2;
    int num4 = 0;
    int num5 = 0;
    for (; index <= _param3; ++index)
    {
      double num6 = ((IComparable) _param1[index]).\u0023\u003Dzb9UCYbo\u003D();
      if (num6 < num3)
        num3 = num6;
      if (++num4 >= num2)
      {
        numArray[num5++] = num3;
        num3 = double.MaxValue;
        num4 = 0;
      }
    }
    return (IList) numArray;
  }

  private IList \u0023\u003DztccTtmWKXlfE6hK7TQ\u003D\u003D(
    IList _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    int num1 = _param3 - _param2 + 1;
    int num2 = num1 / (this.\u0023\u003DzEMN\u0024erw\u003D * _param4);
    double[] numArray = new double[num1 / num2];
    double val1_1 = double.MaxValue;
    double val1_2 = double.MinValue;
    int index = _param2;
    int num3 = 0;
    int num4 = 0;
    for (; index <= _param3; ++index)
    {
      double val2 = ((IComparable) _param1[index]).\u0023\u003Dzb9UCYbo\u003D();
      val1_1 = Math.Min(val1_1, val2);
      val1_2 = Math.Max(val1_2, val2);
      if (++num3 >= num2)
      {
        numArray[num4++] = 0.5 * (val1_2 + val1_1);
        val1_1 = double.MaxValue;
        val1_2 = double.MinValue;
        num3 = 0;
      }
    }
    return (IList) numArray;
  }
}
