// Decompiled with JetBrains decompiler
// Type: #=z2J8xPQFzEv6$SGdBVtIkvCeM6ZC9qdU8sV7sk3JFAdKN9rpOuxmxHM2c$2gpc4ND3AkP5K8=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal static class \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvCeM6ZC9qdU8sV7sk3JFAdKN9rpOuxmxHM2c\u00242gpc4ND3AkP5K8\u003D
{
  public static int \u0023\u003DziL6rhu76I9knzlgj8g\u003D\u003D(
    int _param0,
    int _param1,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param2)
  {
    return (_param0 > _param2.\u0023\u003Dzp55dtus\u003D ? 1 : 0) | (_param1 > _param2.\u0023\u003DzSzOWcj8\u003D ? 2 : 0) | (_param0 < _param2.\u0023\u003DzP4R7yU0\u003D ? 4 : 0) | (_param1 < _param2.\u0023\u003DzRNV_Dpk\u003D ? 8 : 0);
  }

  public static int \u0023\u003DzcJaEESjpF38GDdk97hFeZJw\u003D(
    int _param0,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1)
  {
    return (_param0 > _param1.\u0023\u003Dzp55dtus\u003D ? 1 : 0) | (_param0 < _param1.\u0023\u003DzP4R7yU0\u003D ? 1 : 0) << 2;
  }

  public static int \u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(
    int _param0,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param1)
  {
    return (_param0 > _param1.\u0023\u003DzSzOWcj8\u003D ? 1 : 0) << 1 | (_param0 < _param1.\u0023\u003DzRNV_Dpk\u003D ? 1 : 0) << 3;
  }

  public static int \u0023\u003Dz2pFhDumE3G5PZhjeM0YEsWQ\u003D(
    int _param0,
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param4,
    int[] _param5,
    int[] _param6)
  {
    int num1 = 0;
    int num2 = 0;
    double num3 = 1E-30;
    double num4 = (double) (_param2 - _param0);
    double num5 = (double) (_param3 - _param1);
    int num6 = 0;
    if (num4 == 0.0)
      num4 = _param0 > _param4.\u0023\u003DzP4R7yU0\u003D ? -num3 : num3;
    if (num5 == 0.0)
      num5 = _param1 > _param4.\u0023\u003DzRNV_Dpk\u003D ? -num3 : num3;
    double num7;
    double num8;
    if (num4 > 0.0)
    {
      num7 = (double) _param4.\u0023\u003DzP4R7yU0\u003D;
      num8 = (double) _param4.\u0023\u003Dzp55dtus\u003D;
    }
    else
    {
      num7 = (double) _param4.\u0023\u003Dzp55dtus\u003D;
      num8 = (double) _param4.\u0023\u003DzP4R7yU0\u003D;
    }
    double num9;
    double num10;
    if (num5 > 0.0)
    {
      num9 = (double) _param4.\u0023\u003DzRNV_Dpk\u003D;
      num10 = (double) _param4.\u0023\u003DzSzOWcj8\u003D;
    }
    else
    {
      num9 = (double) _param4.\u0023\u003DzSzOWcj8\u003D;
      num10 = (double) _param4.\u0023\u003DzRNV_Dpk\u003D;
    }
    double num11 = (num7 - (double) _param0) / num4;
    double num12 = (num9 - (double) _param1) / num5;
    double num13;
    double num14;
    if (num11 < num12)
    {
      num13 = num11;
      num14 = num12;
    }
    else
    {
      num13 = num12;
      num14 = num11;
    }
    if (num13 <= 1.0)
    {
      if (0.0 < num13)
      {
        _param5[num1++] = (int) num7;
        _param6[num2++] = (int) num9;
        ++num6;
      }
      if (num14 <= 1.0)
      {
        double num15 = (num8 - (double) _param0) / num4;
        double num16 = (num10 - (double) _param1) / num5;
        double num17 = num15 < num16 ? num15 : num16;
        int num18;
        int num19;
        if (num14 > 0.0 || num17 > 0.0)
        {
          if (num14 <= num17)
          {
            if (num14 > 0.0)
            {
              if (num11 > num12)
              {
                _param5[num1++] = (int) num7;
                _param6[num2++] = (int) ((double) _param1 + num11 * num5);
              }
              else
              {
                _param5[num1++] = (int) ((double) _param0 + num12 * num4);
                _param6[num2++] = (int) num9;
              }
              ++num6;
            }
            if (num17 < 1.0)
            {
              if (num15 < num16)
              {
                int[] numArray1 = _param5;
                int index1 = num1;
                num18 = index1 + 1;
                int num20 = (int) num8;
                numArray1[index1] = num20;
                int[] numArray2 = _param6;
                int index2 = num2;
                num19 = index2 + 1;
                int num21 = (int) ((double) _param1 + num15 * num5);
                numArray2[index2] = num21;
              }
              else
              {
                int[] numArray3 = _param5;
                int index3 = num1;
                num18 = index3 + 1;
                int num22 = (int) ((double) _param0 + num16 * num4);
                numArray3[index3] = num22;
                int[] numArray4 = _param6;
                int index4 = num2;
                num19 = index4 + 1;
                int num23 = (int) num10;
                numArray4[index4] = num23;
              }
            }
            else
            {
              int[] numArray5 = _param5;
              int index5 = num1;
              num18 = index5 + 1;
              int num24 = _param2;
              numArray5[index5] = num24;
              int[] numArray6 = _param6;
              int index6 = num2;
              num19 = index6 + 1;
              int num25 = _param3;
              numArray6[index6] = num25;
            }
            ++num6;
          }
          else
          {
            if (num11 > num12)
            {
              int[] numArray7 = _param5;
              int index7 = num1;
              num18 = index7 + 1;
              int num26 = (int) num7;
              numArray7[index7] = num26;
              int[] numArray8 = _param6;
              int index8 = num2;
              num19 = index8 + 1;
              int num27 = (int) num10;
              numArray8[index8] = num27;
            }
            else
            {
              int[] numArray9 = _param5;
              int index9 = num1;
              num18 = index9 + 1;
              int num28 = (int) num8;
              numArray9[index9] = num28;
              int[] numArray10 = _param6;
              int index10 = num2;
              num19 = index10 + 1;
              int num29 = (int) num9;
              numArray10[index10] = num29;
            }
            ++num6;
          }
        }
      }
    }
    return num6;
  }

  public static bool \u0023\u003Dz7nfMJBEJUSheryDSnA\u003D\u003D(
    int _param0,
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param4,
    ref int _param5,
    ref int _param6,
    int _param7)
  {
    if ((_param7 & 5) != 0)
    {
      if (_param0 == _param2)
        return false;
      int num = (_param7 & 4) != 0 ? _param4.\u0023\u003DzP4R7yU0\u003D : _param4.\u0023\u003Dzp55dtus\u003D;
      _param6 = (int) ((double) (num - _param0) * (double) (_param3 - _param1) / (double) (_param2 - _param0) + (double) _param1);
      _param5 = num;
    }
    _param7 = \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvCeM6ZC9qdU8sV7sk3JFAdKN9rpOuxmxHM2c\u00242gpc4ND3AkP5K8\u003D.\u0023\u003DzKDrhoWJzds7WPcK7r2Px1yQ\u003D(_param6, _param4);
    if ((_param7 & 10) != 0)
    {
      if (_param1 == _param3)
        return false;
      int num = (_param7 & 4) != 0 ? _param4.\u0023\u003DzRNV_Dpk\u003D : _param4.\u0023\u003DzSzOWcj8\u003D;
      _param5 = (int) ((double) (num - _param1) * (double) (_param2 - _param0) / (double) (_param3 - _param1) + (double) _param0);
      _param6 = num;
    }
    return true;
  }

  public static int \u0023\u003DzNUeUJ1Lg6V1srbl2Gg\u003D\u003D(
    ref int _param0,
    ref int _param1,
    ref int _param2,
    ref int _param3,
    \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSRoXVhBAYRK2CMZOKas _param4)
  {
    int num1 = \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvCeM6ZC9qdU8sV7sk3JFAdKN9rpOuxmxHM2c\u00242gpc4ND3AkP5K8\u003D.\u0023\u003DziL6rhu76I9knzlgj8g\u003D\u003D(_param0, _param1, _param4);
    int num2 = \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvCeM6ZC9qdU8sV7sk3JFAdKN9rpOuxmxHM2c\u00242gpc4ND3AkP5K8\u003D.\u0023\u003DziL6rhu76I9knzlgj8g\u003D\u003D(_param2, _param3, _param4);
    int num3 = 0;
    if ((num2 | num1) == 0)
      return 0;
    if ((num1 & 5) != 0 && (num1 & 5) == (num2 & 5) || (num1 & 10) != 0 && (num1 & 10) == (num2 & 10))
      return 4;
    int num4 = _param0;
    int num5 = _param1;
    int num6 = _param2;
    int num7 = _param3;
    if (num1 != 0)
    {
      if (!\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvCeM6ZC9qdU8sV7sk3JFAdKN9rpOuxmxHM2c\u00242gpc4ND3AkP5K8\u003D.\u0023\u003Dz7nfMJBEJUSheryDSnA\u003D\u003D(num4, num5, num6, num7, _param4, ref _param0, ref _param1, num1) || _param0 == _param2 && _param1 == _param3)
        return 4;
      num3 |= 1;
    }
    if (num2 != 0)
    {
      if (!\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvCeM6ZC9qdU8sV7sk3JFAdKN9rpOuxmxHM2c\u00242gpc4ND3AkP5K8\u003D.\u0023\u003Dz7nfMJBEJUSheryDSnA\u003D\u003D(num4, num5, num6, num7, _param4, ref _param2, ref _param3, num2) || _param0 == _param2 && _param1 == _param3)
        return 4;
      num3 |= 2;
    }
    return num3;
  }

  private enum \u0023\u003Dzi_QXvjMAg9XKbIGr6g7dW\u0024Y\u003D
  {
  }
}
