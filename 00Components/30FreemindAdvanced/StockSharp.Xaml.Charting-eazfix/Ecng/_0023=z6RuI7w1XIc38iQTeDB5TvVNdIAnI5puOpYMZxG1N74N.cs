// Decompiled with JetBrains decompiler
// Type: #=z6RuI7w1XIc38iQTeDB5TvVNdIAnI5puOpYMZxG1N74NKDg_amLDV7KqxGsLju8sgS0tDjuo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvVNdIAnI5puOpYMZxG1N74NKDg_amLDV7KqxGsLju8sgS0tDjuo\u003D : 
  \u0023\u003DzumuFC1VOOoBDs2QpPto5VkbLjIthccEiDcgR\u0024vJQ7K_Hrp4E7vWJo36zKSrEqSwNKA\u003D\u003D
{
  public \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvVNdIAnI5puOpYMZxG1N74NKDg_amLDV7KqxGsLju8sgS0tDjuo\u003D(
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0LKrMWRP8HpfFNPgYY55tLjfw\u003D\u003D _param1,
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT _param2,
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQ6PRW443CXjnpjY_jRLnxeTxLdSjL9CR9M_QGcrFkI\u0024EwQ\u003D\u003D _param3)
    : base(_param1, _param2, _param3)
  {
    if (_param1.\u0023\u003Dz8hY71usSYcKH().\u0023\u003DzQB4v2EccUot6eT2VRw\u003D\u003D() != 3)
      throw new NotSupportedException("span_image_filter_rgb must have a 24 bit DestImage");
  }

  public override void \u0023\u003DzvJVSzbY\u003D(
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D().\u0023\u003DzoLjFgpI\u003D((double) _param3 + this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D(), (double) _param4 + this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D(), _param5);
    int num1 = this.\u0023\u003DzPAwmFd_s7qOr.\u0023\u003DzsG\u0024bkFt924ExZZI2LQ\u003D\u003D();
    int num2 = this.\u0023\u003DzPAwmFd_s7qOr.\u0023\u003DzCVPVJaY\u003D();
    int[] numArray1 = this.\u0023\u003DzPAwmFd_s7qOr.\u0023\u003Dztrjmxx0aHjVH();
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT nbVwaPinzNveauDt = this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D();
    do
    {
      nbVwaPinzNveauDt.\u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out _param3, out _param4);
      _param3 -= this.\u0023\u003DzxioLP9oyjKFBIHWWVA\u003D\u003D();
      _param4 -= this.\u0023\u003Dz2YwJt7tn\u0024hxMRoRU\u0024A\u003D\u003D();
      int num3 = _param3;
      int num4 = _param4;
      int num5 = num3 >> 8;
      int num6 = num4 >> 8;
      int num7;
      int num8 = num7 = 8192 /*0x2000*/;
      int num9 = num7;
      int num10 = num7;
      int num11 = num3 & (int) byte.MaxValue;
      int num12 = num1;
      int index1 = (int) byte.MaxValue - (num4 & (int) byte.MaxValue);
      int index2;
      byte[] numArray2 = this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003DzmiTEKiA\u003D(num5 + num2, num6 + num2, num1, out index2);
      while (true)
      {
        int num13 = num1;
        int num14 = numArray1[index1];
        int index3 = (int) byte.MaxValue - num11;
        while (true)
        {
          int num15 = num14 * numArray1[index3] + 8192 /*0x2000*/ >> 14;
          num10 += num15 * (int) numArray2[index2 + 2];
          num9 += num15 * (int) numArray2[index2 + 1];
          num8 += num15 * (int) numArray2[index2];
          if (--num13 != 0)
          {
            index3 += 256 /*0x0100*/;
            this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003DziwTQ98wsEeu3(out index2);
          }
          else
            break;
        }
        if (--num12 != 0)
        {
          index1 += 256 /*0x0100*/;
          numArray2 = this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003DzXgTbLkAgh1Pd(out index2);
        }
        else
          break;
      }
      int num16 = num10 >> 14;
      int num17 = num9 >> 14;
      int num18 = num8 >> 14;
      if ((uint) num16 > (uint) byte.MaxValue)
      {
        if (num16 < 0)
          num16 = 0;
        if (num16 > (int) byte.MaxValue)
          num16 = (int) byte.MaxValue;
      }
      if ((uint) num17 > (uint) byte.MaxValue)
      {
        if (num17 < 0)
          num17 = 0;
        if (num17 > (int) byte.MaxValue)
          num17 = (int) byte.MaxValue;
      }
      if ((uint) num18 > (uint) byte.MaxValue)
      {
        if (num18 < 0)
          num18 = 0;
        if (num18 > (int) byte.MaxValue)
          num18 = (int) byte.MaxValue;
      }
      _param1[_param2].\u0023\u003DzKCqGEcs\u003D = byte.MaxValue;
      _param1[_param2].\u0023\u003Dz4WHdt9g\u003D = (byte) num16;
      _param1[_param2].\u0023\u003DzoRsAtmfOFDZe = (byte) num17;
      _param1[_param2].\u0023\u003DzcdKuX48ZXN_S = (byte) num18;
      ++_param2;
      nbVwaPinzNveauDt.\u0023\u003DzXiQrjbw\u003D();
    }
    while (--_param5 != 0);
  }
}
