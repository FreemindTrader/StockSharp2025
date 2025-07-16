// Decompiled with JetBrains decompiler
// Type: #=z5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB$RrBBDXLSStj0zJjwtU37JKMgle_YNK92CiuXJz1UoZWkP20bYIUyc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSStj0zJjwtU37JKMgle_YNK92CiuXJz1UoZWkP20bYIUyc\u003D : 
  \u0023\u003DzlbW67NDum9APBLuSxcbgNGlUlH7Xuy4XFsgJRB7XEOO1IBdNFpeHJw\u0024uuZZ\u0024M_etH1o0wicGxNOA,
  \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D
{
  public \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D \u0023\u003DzpXQh1U84EY2nUbOFY6f_7N0\u003D(
    float[] _param1,
    int _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz2hKEBlg\u003D(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param3,
    int _param4)
  {
    do
    {
      _param1[_param2 + 2] = _param3.\u0023\u003Dz4WHdt9g\u003D;
      _param1[_param2 + 1] = _param3.\u0023\u003DzoRsAtmfOFDZe;
      _param1[_param2] = _param3.\u0023\u003DzcdKuX48ZXN_S;
      _param1[_param2 + 3] = _param3.\u0023\u003DzKCqGEcs\u003D;
      _param2 += 4;
    }
    while (--_param4 != 0);
  }

  public void \u0023\u003DzDjUCkxvYu2E5(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param3,
    int _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzDjUCkxvYu2E5(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param3,
    int _param4)
  {
    do
    {
      _param1[_param2 + 2] = _param3.\u0023\u003Dz4WHdt9g\u003D;
      _param1[_param2 + 1] = _param3.\u0023\u003DzoRsAtmfOFDZe;
      _param1[_param2] = _param3.\u0023\u003DzcdKuX48ZXN_S;
      _param1[_param2 + 3] = _param3.\u0023\u003DzKCqGEcs\u003D;
      _param2 += 4;
    }
    while (--_param4 != 0);
  }

  public void \u0023\u003Dz1sAbEWOIYGyA(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param3)
  {
    if ((double) _param3.\u0023\u003DzKCqGEcs\u003D == 1.0)
    {
      _param1[_param2 + 2] = (float) (byte) _param3.\u0023\u003Dz4WHdt9g\u003D;
      _param1[_param2 + 1] = (float) (byte) _param3.\u0023\u003DzoRsAtmfOFDZe;
      _param1[_param2] = (float) (byte) _param3.\u0023\u003DzcdKuX48ZXN_S;
      _param1[_param2 + 3] = (float) (byte) _param3.\u0023\u003DzKCqGEcs\u003D;
    }
    else
    {
      float num1 = _param1[_param2 + 2];
      float num2 = _param1[_param2 + 1];
      float num3 = _param1[_param2];
      float num4 = _param1[_param2 + 3];
      _param1[_param2 + 2] = (_param3.\u0023\u003Dz4WHdt9g\u003D - num1) * _param3.\u0023\u003DzKCqGEcs\u003D + num1;
      _param1[_param2 + 1] = (_param3.\u0023\u003DzoRsAtmfOFDZe - num2) * _param3.\u0023\u003DzKCqGEcs\u003D + num2;
      _param1[_param2] = (_param3.\u0023\u003DzcdKuX48ZXN_S - num3) * _param3.\u0023\u003DzKCqGEcs\u003D + num3;
      _param1[_param2 + 3] = (float) ((double) _param3.\u0023\u003DzKCqGEcs\u003D + (double) num4 - (double) _param3.\u0023\u003DzKCqGEcs\u003D * (double) num4);
    }
  }

  public void \u0023\u003Dz_A3pQLKo8i_c(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param3,
    int _param4,
    int _param5)
  {
  }

  public void \u0023\u003Dz_A3pQLKo8i_c(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param3,
    int _param4,
    byte _param5,
    int _param6)
  {
  }

  public void \u0023\u003Dz_A3pQLKo8i_c(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param3,
    int _param4,
    byte[] _param5,
    int _param6,
    int _param7)
  {
  }

  public void \u0023\u003Dz_A3pQLKo8i_c(
    float[] _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param3,
    int _param4,
    byte[] _param5,
    int _param6,
    bool _param7,
    int _param8)
  {
    if (_param7)
    {
      if (_param5[_param6] == byte.MaxValue)
      {
        for (int index = 0; index < _param8; ++index)
        {
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4]);
          ++_param4;
          _param2 += 4;
        }
      }
      else
      {
        for (int index = 0; index < _param8; ++index)
        {
          \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D af0lRdvTglyLevjU0 = _param3[_param4];
          float num1 = (float) (((double) af0lRdvTglyLevjU0.\u0023\u003DzKCqGEcs\u003D * (double) _param5[_param6] + (double) byte.MaxValue) / 256.0);
          if ((double) num1 != 0.0)
          {
            if ((double) num1 == (double) byte.MaxValue)
            {
              _param1[_param2 + 2] = (float) (byte) af0lRdvTglyLevjU0.\u0023\u003Dz4WHdt9g\u003D;
              _param1[_param2 + 1] = (float) (byte) af0lRdvTglyLevjU0.\u0023\u003DzoRsAtmfOFDZe;
              _param1[_param2] = (float) (byte) af0lRdvTglyLevjU0.\u0023\u003DzcdKuX48ZXN_S;
              _param1[_param2 + 3] = (float) (byte) num1;
            }
            else
            {
              float num2 = (float) byte.MaxValue - num1;
              float num3 = _param1[_param2 + 2] * num2 + af0lRdvTglyLevjU0.\u0023\u003Dz4WHdt9g\u003D;
              float num4 = _param1[_param2 + 1] * num2 + af0lRdvTglyLevjU0.\u0023\u003DzoRsAtmfOFDZe;
              float num5 = _param1[_param2] * num2 + af0lRdvTglyLevjU0.\u0023\u003DzcdKuX48ZXN_S;
              float num6 = _param1[_param2 + 3];
              _param1[_param2 + 2] = num3;
              _param1[_param2 + 1] = num4;
              _param1[_param2] = num5;
              _param1[_param2 + 3] = (float) (1.0 - (double) num2 * (1.0 - (double) num6));
            }
            ++_param4;
            _param2 += 4;
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < _param8; ++index)
      {
        \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D af0lRdvTglyLevjU0 = _param3[_param4];
        if ((double) af0lRdvTglyLevjU0.\u0023\u003DzKCqGEcs\u003D == 1.0 && _param5[_param6] == byte.MaxValue)
        {
          _param1[_param2 + 2] = af0lRdvTglyLevjU0.\u0023\u003Dz4WHdt9g\u003D;
          _param1[_param2 + 1] = af0lRdvTglyLevjU0.\u0023\u003DzoRsAtmfOFDZe;
          _param1[_param2] = af0lRdvTglyLevjU0.\u0023\u003DzcdKuX48ZXN_S;
          _param1[_param2 + 3] = 1f;
        }
        else
        {
          float num7 = (float) _param5[_param6] * 0.003921569f;
          float num8 = af0lRdvTglyLevjU0.\u0023\u003DzKCqGEcs\u003D * num7;
          if ((double) num7 > 0.0 && (double) num8 > 0.0)
          {
            float num9 = 1f - num8;
            float num10 = (float) ((double) _param1[_param2 + 2] * (double) num9 + (double) af0lRdvTglyLevjU0.\u0023\u003Dz4WHdt9g\u003D * (double) num7);
            float num11 = (float) ((double) _param1[_param2 + 1] * (double) num9 + (double) af0lRdvTglyLevjU0.\u0023\u003DzoRsAtmfOFDZe * (double) num7);
            float num12 = (float) ((double) _param1[_param2] * (double) num9 + (double) af0lRdvTglyLevjU0.\u0023\u003DzcdKuX48ZXN_S * (double) num7);
            float num13 = _param1[_param2 + 3];
            float num14 = num13 + (1f - num13) * af0lRdvTglyLevjU0.\u0023\u003DzKCqGEcs\u003D * num7;
            _param1[_param2 + 2] = num10;
            _param1[_param2 + 1] = num11;
            _param1[_param2] = num12;
            _param1[_param2 + 3] = num14;
          }
        }
        ++_param4;
        ++_param6;
        _param2 += 4;
      }
    }
  }
}
