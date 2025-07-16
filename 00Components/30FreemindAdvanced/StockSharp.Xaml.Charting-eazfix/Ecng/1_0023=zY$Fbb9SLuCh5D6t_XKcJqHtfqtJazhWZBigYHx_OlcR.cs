// Decompiled with JetBrains decompiler
// Type: #=zY$Fbb9SLuCh5D6t_XKcJqHtfqtJazhWZBigYHx_OlcRKpbFiDpT7i1nEEzVZf93Yim6tzmQ=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqHtfqtJazhWZBigYHx_OlcRKpbFiDpT7i1nEEzVZf93Yim6tzmQ\u003D : 
  \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQAaMpPLBW\u0024Zg11zdsYrjLM9h5WMNGGTiHmJKqNU7lBp281ChPextAQHX,
  \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOESA9yl1dafdSqQDeAMKXaBPbCxPlPQgez5bfFbgS\u0024CknPn64g\u003D
{
  public \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D \u0023\u003Dzk1hQXoI0sJ98Ijd3VA\u003D\u003D(
    byte[] _param1,
    int _param2)
  {
    return new \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D((int) _param1[_param2 + 2], (int) _param1[_param2 + 1], (int) _param1[_param2], (int) byte.MaxValue);
  }

  public void \u0023\u003DzDjUCkxvYu2E5(
    byte[] _param1,
    int _param2,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D _param3,
    int _param4)
  {
    do
    {
      _param1[_param2 + 2] = _param3.\u0023\u003Dz4WHdt9g\u003D;
      _param1[_param2 + 1] = _param3.\u0023\u003DzoRsAtmfOFDZe;
      _param1[_param2] = _param3.\u0023\u003DzcdKuX48ZXN_S;
      _param2 += 3;
    }
    while (--_param4 != 0);
  }

  public void \u0023\u003Dz1sAbEWOIYGyA(
    byte[] _param1,
    int _param2,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D _param3)
  {
    int num1 = (int) _param1[_param2 + 2];
    int num2 = (int) _param1[_param2 + 1];
    int num3 = (int) _param1[_param2];
    _param1[_param2 + 2] = (byte) (((int) _param3.\u0023\u003Dz4WHdt9g\u003D - num1) * (int) _param3.\u0023\u003DzKCqGEcs\u003D + (num1 << 8) >> 8);
    _param1[_param2 + 1] = (byte) (((int) _param3.\u0023\u003DzoRsAtmfOFDZe - num2) * (int) _param3.\u0023\u003DzKCqGEcs\u003D + (num2 << 8) >> 8);
    _param1[_param2] = (byte) (((int) _param3.\u0023\u003DzcdKuX48ZXN_S - num3) * (int) _param3.\u0023\u003DzKCqGEcs\u003D + (num3 << 8) >> 8);
  }

  public void \u0023\u003Dz_A3pQLKo8i_c(
    byte[] _param1,
    int _param2,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D[] _param3,
    int _param4,
    byte[] _param5,
    int _param6,
    bool _param7,
    int _param8)
  {
    if (_param7)
    {
      int num = (int) _param5[_param6];
      if (num == (int) byte.MaxValue)
      {
        do
        {
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4++]);
          _param2 += 3;
        }
        while (--_param8 != 0);
      }
      else
      {
        do
        {
          _param3[_param4].\u0023\u003DzKCqGEcs\u003D = (byte) ((int) _param3[_param4].\u0023\u003DzKCqGEcs\u003D * num + (int) byte.MaxValue >> 8);
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4]);
          _param2 += 3;
          ++_param4;
        }
        while (--_param8 != 0);
      }
    }
    else
    {
      do
      {
        int num = (int) _param5[_param6++];
        if (num == (int) byte.MaxValue)
        {
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4]);
        }
        else
        {
          \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D nwsEwePinXgsJj4Q = _param3[_param4];
          nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D = (byte) ((int) nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D * num + (int) byte.MaxValue >> 8);
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, nwsEwePinXgsJj4Q);
        }
        _param2 += 3;
        ++_param4;
      }
      while (--_param8 != 0);
    }
  }
}
