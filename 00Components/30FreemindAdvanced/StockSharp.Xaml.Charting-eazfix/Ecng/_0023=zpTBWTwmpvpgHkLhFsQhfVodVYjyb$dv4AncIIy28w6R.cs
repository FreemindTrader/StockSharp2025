// Decompiled with JetBrains decompiler
// Type: #=zpTBWTwmpvpgHkLhFsQhfVodVYjyb$dv4AncIIy28w6RuBZ8F_Cy1vjuc4if_iGlCxpTsEkB5$_OYIL7q0g==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzpTBWTwmpvpgHkLhFsQhfVodVYjyb\u0024dv4AncIIy28w6RuBZ8F_Cy1vjuc4if_iGlCxpTsEkB5\u0024_OYIL7q0g\u003D\u003D : 
  \u0023\u003DzumuFC1VOOoBDs2QpPto5VkbLjIthccEiDcgR\u0024vJQ7K_Hrp4E7vWJo36zKSrEqSwNKA\u003D\u003D
{
  public \u0023\u003DzpTBWTwmpvpgHkLhFsQhfVodVYjyb\u0024dv4AncIIy28w6RuBZ8F_Cy1vjuc4if_iGlCxpTsEkB5\u0024_OYIL7q0g\u003D\u003D(
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0LKrMWRP8HpfFNPgYY55tLjfw\u003D\u003D _param1,
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT _param2)
    : base(_param1, _param2, (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQ6PRW443CXjnpjY_jRLnxeTxLdSjL9CR9M_QGcrFkI\u0024EwQ\u003D\u003D) null)
  {
    if (_param1.\u0023\u003Dz8hY71usSYcKH().\u0023\u003DzQB4v2EccUot6eT2VRw\u003D\u003D() != 3)
      throw new NotSupportedException("span_image_filter_rgb must have a 24 bit DestImage");
  }

  public override void \u0023\u003DzvJVSzbY\u003D(
    RGBA_Bytes[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D().\u0023\u003DzoLjFgpI\u003D((double) _param3 + this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D(), (double) _param4 + this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D(), _param5);
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D ppsbKthY7Nkewpng = (\u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D) this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003Dz8hY71usSYcKH();
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT nbVwaPinzNveauDt = this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D();
    byte[] numArray = ppsbKthY7Nkewpng.\u0023\u003Dz9b1_JhA\u003D(out int _);
    do
    {
      int num1;
      int num2;
      nbVwaPinzNveauDt.\u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out num1, out num2);
      num1 -= this.\u0023\u003DzxioLP9oyjKFBIHWWVA\u003D\u003D();
      num2 -= this.\u0023\u003Dz2YwJt7tn\u0024hxMRoRU\u0024A\u003D\u003D();
      int num3 = num1 >> 8;
      int num4 = num2 >> 8;
      int num5;
      int num6 = num5 = 32768 /*0x8000*/;
      int num7 = num5;
      int num8 = num5;
      num1 &= (int) byte.MaxValue;
      num2 &= (int) byte.MaxValue;
      int index1 = ppsbKthY7Nkewpng.\u0023\u003DzHlHGfKJZNJsq(num3, num4);
      int num9 = (256 /*0x0100*/ - num1) * (256 /*0x0100*/ - num2);
      int num10 = num8 + num9 * (int) numArray[index1 + 2];
      int num11 = num7 + num9 * (int) numArray[index1 + 1];
      int num12 = num6 + num9 * (int) numArray[index1];
      int index2 = index1 + 3;
      int num13 = num1 * (256 /*0x0100*/ - num2);
      int num14 = num10 + num13 * (int) numArray[index2 + 2];
      int num15 = num11 + num13 * (int) numArray[index2 + 1];
      int num16 = num12 + num13 * (int) numArray[index2];
      int num17 = num4 + 1;
      int index3 = ppsbKthY7Nkewpng.\u0023\u003DzHlHGfKJZNJsq(num3, num17);
      int num18 = (256 /*0x0100*/ - num1) * num2;
      int num19 = num14 + num18 * (int) numArray[index3 + 2];
      int num20 = num15 + num18 * (int) numArray[index3 + 1];
      int num21 = num16 + num18 * (int) numArray[index3];
      int index4 = index3 + 3;
      int num22 = num1 * num2;
      int num23 = num19 + num22 * (int) numArray[index4 + 2];
      int num24 = num20 + num22 * (int) numArray[index4 + 1];
      int num25 = num21 + num22 * (int) numArray[index4];
      int num26 = num23 >> 16 /*0x10*/;
      int num27 = num24 >> 16 /*0x10*/;
      int num28 = num25 >> 16 /*0x10*/;
      RGBA_Bytes nwsEwePinXgsJj4Q;
      nwsEwePinXgsJj4Q.\u0023\u003Dz4WHdt9g\u003D = (byte) num26;
      nwsEwePinXgsJj4Q.\u0023\u003DzoRsAtmfOFDZe = (byte) num27;
      nwsEwePinXgsJj4Q.\u0023\u003DzcdKuX48ZXN_S = (byte) num28;
      nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D = byte.MaxValue;
      _param1[_param2] = nwsEwePinXgsJj4Q;
      ++_param2;
      nbVwaPinzNveauDt.\u0023\u003DzXiQrjbw\u003D();
    }
    while (--_param5 != 0);
  }

  private void \u0023\u003DzvarNhRFp_fcR(
    int[] _param1,
    ref int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D _param7,
    int _param8,
    int _param9,
    int _param10,
    int _param11,
    int _param12)
  {
    throw new NotImplementedException();
  }
}
