// Decompiled with JetBrains decompiler
// Type: #=zWwDYSe$z_nPO4KEsBbkyAbG5yCERTVlAHXU$91pTc_3vt77FIPjRFCJar$JpvVhWLGEsZV6XDakaihyBrQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAbG5yCERTVlAHXU\u002491pTc_3vt77FIPjRFCJar\u0024JpvVhWLGEsZV6XDakaihyBrQ\u003D\u003D : 
  \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOESA9yl1dafdSqQDeAMKXaBPbCxPlPQgez5bfFbgS\u0024CknPn64g\u003D
{
  private static int[] \u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6 = new int[512 /*0x0200*/];
  private int \u0023\u003Dzcz7RxakSRn3uq70hWw\u003D\u003D;

  public \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAbG5yCERTVlAHXU\u002491pTc_3vt77FIPjRFCJar\u0024JpvVhWLGEsZV6XDakaihyBrQ\u003D\u003D(
    int _param1)
  {
    this.\u0023\u003Dzcz7RxakSRn3uq70hWw\u003D\u003D = _param1;
    if (\u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAbG5yCERTVlAHXU\u002491pTc_3vt77FIPjRFCJar\u0024JpvVhWLGEsZV6XDakaihyBrQ\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[2] != 0)
      return;
    for (int val1 = 0; val1 < \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAbG5yCERTVlAHXU\u002491pTc_3vt77FIPjRFCJar\u0024JpvVhWLGEsZV6XDakaihyBrQ\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6.Length; ++val1)
      \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAbG5yCERTVlAHXU\u002491pTc_3vt77FIPjRFCJar\u0024JpvVhWLGEsZV6XDakaihyBrQ\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[val1] = Math.Min(val1, (int) byte.MaxValue);
  }

  [SpecialName]
  public int \u0023\u003DzVAoYC\u0024MJfjKU3EUhiA\u003D\u003D() => 8;

  public \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D \u0023\u003Dzk1hQXoI0sJ98Ijd3VA\u003D\u003D(
    byte[] _param1,
    int _param2)
  {
    int num = (int) _param1[_param2];
    return new \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D(num, num, num, (int) byte.MaxValue);
  }

  public void \u0023\u003DzDjUCkxvYu2E5(
    byte[] _param1,
    int _param2,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D _param3,
    int _param4)
  {
    do
    {
      byte num = Math.Min(Math.Max(_param3.\u0023\u003Dz4WHdt9g\u003D, Math.Max(_param3.\u0023\u003DzoRsAtmfOFDZe, _param3.\u0023\u003DzcdKuX48ZXN_S)), byte.MaxValue);
      _param1[_param2] = num;
      _param2 += this.\u0023\u003Dzcz7RxakSRn3uq70hWw\u003D\u003D;
    }
    while (--_param4 != 0);
  }

  public void \u0023\u003Dz1sAbEWOIYGyA(
    byte[] _param1,
    int _param2,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D _param3)
  {
    byte num = (byte) (((int) Math.Min(Math.Max(_param3.\u0023\u003Dz4WHdt9g\u003D, Math.Max(_param3.\u0023\u003DzoRsAtmfOFDZe, _param3.\u0023\u003DzcdKuX48ZXN_S)), byte.MaxValue) - (int) _param1[_param2]) * (int) _param3.\u0023\u003DzKCqGEcs\u003D + ((int) _param1[_param2] << 8) >> 8);
    _param1[_param2] = num;
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
          _param2 += this.\u0023\u003Dzcz7RxakSRn3uq70hWw\u003D\u003D;
        }
        while (--_param8 != 0);
      }
      else
      {
        do
        {
          _param3[_param4].\u0023\u003DzKCqGEcs\u003D = (byte) ((int) _param3[_param4].\u0023\u003DzKCqGEcs\u003D * num + (int) byte.MaxValue >> 8);
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4]);
          _param2 += this.\u0023\u003Dzcz7RxakSRn3uq70hWw\u003D\u003D;
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
        _param2 += this.\u0023\u003Dzcz7RxakSRn3uq70hWw\u003D\u003D;
        ++_param4;
      }
      while (--_param8 != 0);
    }
  }
}
