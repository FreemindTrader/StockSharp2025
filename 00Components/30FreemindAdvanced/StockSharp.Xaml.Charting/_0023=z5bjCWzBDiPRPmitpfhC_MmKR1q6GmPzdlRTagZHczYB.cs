// Decompiled with JetBrains decompiler
// Type: #=z5bjCWzBDiPRPmitpfhC_MmKR1q6GmPzdlRTagZHczYB76YwlGmfqCi_gQw$1sSkPXPHUgRedwEiVNzkID5K$tdM=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal sealed class \u0023\u003Dz5bjCWzBDiPRPmitpfhC_MmKR1q6GmPzdlRTagZHczYB76YwlGmfqCi_gQw\u00241sSkPXPHUgRedwEiVNzkID5K\u0024tdM\u003D(
  \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0LKrMWRP8HpfFNPgYY55tLjfw\u003D\u003D _param1,
  \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT _param2) : 
  \u0023\u003DzumuFC1VOOoBDs2QpPto5VkbLjIthccEiDcgR\u0024vJQ7K_Hrp4E7vWJo36zKSrEqSwNKA\u003D\u003D(_param1, _param2, (\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQ6PRW443CXjnpjY_jRLnxeTxLdSjL9CR9M_QGcrFkI\u0024EwQ\u003D\u003D) null)
{
  public override unsafe void \u0023\u003DzvJVSzbY\u003D(
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D ppsbKthY7Nkewpng = (\u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D) this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003Dz8hY71usSYcKH();
    int num1 = ppsbKthY7Nkewpng.\u0023\u003DzQB4v2EccUot6eT2VRw\u003D\u003D();
    if (ppsbKthY7Nkewpng.\u0023\u003DzHstjD51XfGa0() != 8)
      throw new NotSupportedException("");
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT nbVwaPinzNveauDt = this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D();
    nbVwaPinzNveauDt.\u0023\u003DzoLjFgpI\u003D((double) _param3 + this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D(), (double) _param4 + this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D(), _param5);
    int num2;
    int num3;
    nbVwaPinzNveauDt.\u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out num2, out num3);
    int index = ppsbKthY7Nkewpng.\u0023\u003DzHlHGfKJZNJsq(num2 >> 8, num3 >> 8);
    fixed (byte* numPtr = ppsbKthY7Nkewpng.\u0023\u003Dz9b1_JhA\u003D())
    {
      do
      {
        _param1[_param2].\u0023\u003Dz4WHdt9g\u003D = numPtr[index];
        _param1[_param2].\u0023\u003DzoRsAtmfOFDZe = numPtr[index];
        _param1[_param2].\u0023\u003DzcdKuX48ZXN_S = numPtr[index];
        _param1[_param2].\u0023\u003DzKCqGEcs\u003D = byte.MaxValue;
        ++_param2;
        index += num1;
      }
      while (--_param5 != 0);
    }
  }
}
