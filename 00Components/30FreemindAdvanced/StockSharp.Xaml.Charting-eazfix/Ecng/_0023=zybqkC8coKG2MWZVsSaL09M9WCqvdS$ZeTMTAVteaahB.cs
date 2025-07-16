// Decompiled with JetBrains decompiler
// Type: #=zybqkC8coKG2MWZVsSaL09M9WCqvdS$ZeTMTAVteaahB8y7Bj_0rp3CWiK4CjCy8qEc_EbgGjCoAt
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzybqkC8coKG2MWZVsSaL09M9WCqvdS\u0024ZeTMTAVteaahB8y7Bj_0rp3CWiK4CjCy8qEc_EbgGjCoAt : 
  \u0023\u003DzlbW67NDum9APBLuSxcbgNGlUlH7Xuy4XFsgJRB7XEOO1IBdNFpeHJw\u0024uuZZ\u0024M_etH1o0wicGxNOA,
  \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D
{
  public \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D \u0023\u003DzpXQh1U84EY2nUbOFY6f_7N0\u003D(
    float[] _param1,
    int _param2)
  {
    return new \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D(_param1[_param2 + 2], _param1[_param2 + 1], _param1[_param2], _param1[_param2 + 3]);
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
    byte[] _param5,
    int _param6,
    bool _param7,
    int _param8)
  {
    throw new NotImplementedException();
  }
}
