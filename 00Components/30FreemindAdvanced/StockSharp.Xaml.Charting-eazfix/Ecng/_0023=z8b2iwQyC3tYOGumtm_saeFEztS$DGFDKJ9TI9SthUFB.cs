// Decompiled with JetBrains decompiler
// Type: #=z8b2iwQyC3tYOGumtm_saeFEztS$DGFDKJ9TI9SthUFBGwEgWF1yTFOI1W1C2hhjL2Ij7$5nV82_r
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dz8b2iwQyC3tYOGumtm_saeFEztS\u0024DGFDKJ9TI9SthUFBGwEgWF1yTFOI1W1C2hhjL2Ij7\u00245nV82_r
{
  private int \u0023\u003DzdfIXiUfrx8sS;
  private int \u0023\u003DzWiFSa2\u0024o8viR;
  private double \u0023\u003Dz9IbNApzCydlB;
  private double \u0023\u003Dz0TSvb7PdghSI;
  private double \u0023\u003Dzm1Oo\u002446Mw4uy;
  private double \u0023\u003DzaDVeTRUY6dqI;
  private double \u0023\u003DzuhVMXs0eA6iJ;
  private double \u0023\u003DzWYndTuds7\u0024Yf;
  private double \u0023\u003DzTPVp5n0ryn49;
  private double \u0023\u003DzPOAUtcXGvFAR;
  private double \u0023\u003DzczoUR1_9JEKr;
  private double \u0023\u003DzkqtguUFWWs_L;
  private double \u0023\u003Dze3q8J0KWxsyH;
  private double \u0023\u003Dz7LhOT4D6sIso;
  private double \u0023\u003DzKbkBDegHtdf4;
  private double \u0023\u003DzEKZJ_DwwOvm8T0cffw\u003D\u003D;
  private double \u0023\u003Dz0QjY6kX8Pw3VuJyofw\u003D\u003D;

  public \u0023\u003Dz8b2iwQyC3tYOGumtm_saeFEztS\u0024DGFDKJ9TI9SthUFBGwEgWF1yTFOI1W1C2hhjL2Ij7\u00245nV82_r()
  {
    this.\u0023\u003DzdfIXiUfrx8sS = 0;
    this.\u0023\u003DzWiFSa2\u0024o8viR = 0;
    this.\u0023\u003Dz9IbNApzCydlB = 1.0;
  }

  public \u0023\u003Dz8b2iwQyC3tYOGumtm_saeFEztS\u0024DGFDKJ9TI9SthUFBGwEgWF1yTFOI1W1C2hhjL2Ij7\u00245nV82_r(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6)
  {
    this.\u0023\u003DzdfIXiUfrx8sS = 0;
    this.\u0023\u003DzWiFSa2\u0024o8viR = 0;
    this.\u0023\u003Dz9IbNApzCydlB = 1.0;
    this.\u0023\u003Dz0gbwL\u00244\u003D(_param1, _param2, _param3, _param4, _param5, _param6);
  }

  public void \u0023\u003Dzp_DWHgc\u003D()
  {
    this.\u0023\u003DzdfIXiUfrx8sS = 0;
    this.\u0023\u003DzWiFSa2\u0024o8viR = -1;
  }

  public void \u0023\u003Dz0gbwL\u00244\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6)
  {
    this.\u0023\u003Dz0TSvb7PdghSI = _param1;
    this.\u0023\u003Dzm1Oo\u002446Mw4uy = _param2;
    this.\u0023\u003DzaDVeTRUY6dqI = _param5;
    this.\u0023\u003DzuhVMXs0eA6iJ = _param6;
    double num1 = _param3 - _param1;
    double num2 = _param4 - _param2;
    double num3 = _param5 - _param3;
    double num4 = _param6 - _param4;
    this.\u0023\u003DzdfIXiUfrx8sS = agg_basics.\u0023\u003DzROReRE0C5MV7((Math.Sqrt(num1 * num1 + num2 * num2) + Math.Sqrt(num3 * num3 + num4 * num4)) * 0.25 * this.\u0023\u003Dz9IbNApzCydlB);
    if (this.\u0023\u003DzdfIXiUfrx8sS < 4)
      this.\u0023\u003DzdfIXiUfrx8sS = 4;
    double num5 = 1.0 / (double) this.\u0023\u003DzdfIXiUfrx8sS;
    double num6 = num5 * num5;
    double num7 = (_param1 - _param3 * 2.0 + _param5) * num6;
    double num8 = (_param2 - _param4 * 2.0 + _param6) * num6;
    this.\u0023\u003Dz7LhOT4D6sIso = this.\u0023\u003DzWYndTuds7\u0024Yf = _param1;
    this.\u0023\u003DzKbkBDegHtdf4 = this.\u0023\u003DzTPVp5n0ryn49 = _param2;
    this.\u0023\u003DzEKZJ_DwwOvm8T0cffw\u003D\u003D = this.\u0023\u003DzPOAUtcXGvFAR = num7 + (_param3 - _param1) * (2.0 * num5);
    this.\u0023\u003Dz0QjY6kX8Pw3VuJyofw\u003D\u003D = this.\u0023\u003DzczoUR1_9JEKr = num8 + (_param4 - _param2) * (2.0 * num5);
    this.\u0023\u003DzkqtguUFWWs_L = num7 * 2.0;
    this.\u0023\u003Dze3q8J0KWxsyH = num8 * 2.0;
    this.\u0023\u003DzWiFSa2\u0024o8viR = this.\u0023\u003DzdfIXiUfrx8sS;
  }

  public void \u0023\u003Dz1aTkV_Yyam0BDiGr8lyfRBRPwV8A(
    \u0023\u003DzumuFC1VOOoBDs2QpPto5VkbLjIthccEiDcgR\u0024vJQ7K\u00249r5Yplixrjoazbf7S2zmaASXxlwc\u003D.\u0023\u003Dzt2UYNjKgfqDoOHqnDZkjC93619wu _param1)
  {
  }

  public \u0023\u003DzumuFC1VOOoBDs2QpPto5VkbLjIthccEiDcgR\u0024vJQ7K\u00249r5Yplixrjoazbf7S2zmaASXxlwc\u003D.\u0023\u003Dzt2UYNjKgfqDoOHqnDZkjC93619wu \u0023\u003Dz1aTkV_Yyam0BDiGr8lyfRBRPwV8A()
  {
    return (\u0023\u003DzumuFC1VOOoBDs2QpPto5VkbLjIthccEiDcgR\u0024vJQ7K\u00249r5Yplixrjoazbf7S2zmaASXxlwc\u003D.\u0023\u003Dzt2UYNjKgfqDoOHqnDZkjC93619wu) 0;
  }

  public void \u0023\u003Dz5TglSMBX9ZRLd_7HBMzyncJUn7cT(double _param1)
  {
    this.\u0023\u003Dz9IbNApzCydlB = _param1;
  }

  public double \u0023\u003Dz5TglSMBX9ZRLd_7HBMzyncJUn7cT() => this.\u0023\u003Dz9IbNApzCydlB;

  public void \u0023\u003DzYkA1W0WFF\u0024EQ69Kzo8kKCaE\u003D(double _param1)
  {
  }

  public double \u0023\u003DzYkA1W0WFF\u0024EQ69Kzo8kKCaE\u003D() => 0.0;

  public void \u0023\u003DzPOtSiP0bYOdT(double _param1)
  {
  }

  public double \u0023\u003DzPOtSiP0bYOdT() => 0.0;

  public void \u0023\u003DzVawdK5C5Lyf_(int _param1)
  {
    if (this.\u0023\u003DzdfIXiUfrx8sS == 0)
    {
      this.\u0023\u003DzWiFSa2\u0024o8viR = -1;
    }
    else
    {
      this.\u0023\u003DzWiFSa2\u0024o8viR = this.\u0023\u003DzdfIXiUfrx8sS;
      this.\u0023\u003DzWYndTuds7\u0024Yf = this.\u0023\u003Dz7LhOT4D6sIso;
      this.\u0023\u003DzTPVp5n0ryn49 = this.\u0023\u003DzKbkBDegHtdf4;
      this.\u0023\u003DzPOAUtcXGvFAR = this.\u0023\u003DzEKZJ_DwwOvm8T0cffw\u003D\u003D;
      this.\u0023\u003DzczoUR1_9JEKr = this.\u0023\u003Dz0QjY6kX8Pw3VuJyofw\u003D\u003D;
    }
  }

  public Path.\u0023\u003Dz9kUnn38\u003D \u0023\u003DzxfekdAs1X3YM(
    out double _param1,
    out double _param2)
  {
    if (this.\u0023\u003DzWiFSa2\u0024o8viR < 0)
    {
      _param1 = 0.0;
      _param2 = 0.0;
      return (Path.\u0023\u003Dz9kUnn38\u003D) 0;
    }
    if (this.\u0023\u003DzWiFSa2\u0024o8viR == this.\u0023\u003DzdfIXiUfrx8sS)
    {
      _param1 = this.\u0023\u003Dz0TSvb7PdghSI;
      _param2 = this.\u0023\u003Dzm1Oo\u002446Mw4uy;
      --this.\u0023\u003DzWiFSa2\u0024o8viR;
      return (Path.\u0023\u003Dz9kUnn38\u003D) 1;
    }
    if (this.\u0023\u003DzWiFSa2\u0024o8viR == 0)
    {
      _param1 = this.\u0023\u003DzaDVeTRUY6dqI;
      _param2 = this.\u0023\u003DzuhVMXs0eA6iJ;
      --this.\u0023\u003DzWiFSa2\u0024o8viR;
      return (Path.\u0023\u003Dz9kUnn38\u003D) 2;
    }
    this.\u0023\u003DzWYndTuds7\u0024Yf += this.\u0023\u003DzPOAUtcXGvFAR;
    this.\u0023\u003DzTPVp5n0ryn49 += this.\u0023\u003DzczoUR1_9JEKr;
    this.\u0023\u003DzPOAUtcXGvFAR += this.\u0023\u003DzkqtguUFWWs_L;
    this.\u0023\u003DzczoUR1_9JEKr += this.\u0023\u003Dze3q8J0KWxsyH;
    _param1 = this.\u0023\u003DzWYndTuds7\u0024Yf;
    _param2 = this.\u0023\u003DzTPVp5n0ryn49;
    --this.\u0023\u003DzWiFSa2\u0024o8viR;
    return (Path.\u0023\u003Dz9kUnn38\u003D) 2;
  }
}
