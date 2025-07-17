// Decompiled with JetBrains decompiler
// Type: #=zP1B35udtO7gPuZCGGUXpBEt89ZoCjzieaKFuxaIhniqhhASkz8arwDCYKtXcOKZXA$LMlBTvrgSB
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzP1B35udtO7gPuZCGGUXpBEt89ZoCjzieaKFuxaIhniqhhASkz8arwDCYKtXcOKZXA\u0024LMlBTvrgSB(
  IImageBufferAccessor _param1,
  ISpanInterpolator _param2) : 
  span_image_filter(_param1, _param2, (ImageFilterLookUpTable) null)
{
  public override void \u0023\u003DzvJVSzbY\u003D(
    RGBA_Bytes[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D ppsbKthY7Nkewpng = (\u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D) this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003Dz8hY71usSYcKH();
    if (ppsbKthY7Nkewpng.\u0023\u003DzHstjD51XfGa0() != 24)
      throw new NotSupportedException("The source is expected to be 32 bit.");
    ISpanInterpolator nbVwaPinzNveauDt = this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D();
    nbVwaPinzNveauDt.\u0023\u003DzoLjFgpI\u003D((double) _param3 + this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D(), (double) _param4 + this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D(), _param5);
    byte[] numArray1 = ppsbKthY7Nkewpng.\u0023\u003Dz9b1_JhA\u003D(out int _);
    do
    {
      int num1;
      int num2;
      nbVwaPinzNveauDt.\u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out num1, out num2);
      int num3 = num1 >> 8;
      int num4 = num2 >> 8;
      int num5 = ppsbKthY7Nkewpng.\u0023\u003DzHlHGfKJZNJsq(num3, num4);
      RGBA_Bytes nwsEwePinXgsJj4Q;
      ref RGBA_Bytes local1 = ref nwsEwePinXgsJj4Q;
      byte[] numArray2 = numArray1;
      int index1 = num5;
      int num6 = index1 + 1;
      int num7 = (int) numArray2[index1];
      local1.\u0023\u003DzcdKuX48ZXN_S = (byte) num7;
      ref RGBA_Bytes local2 = ref nwsEwePinXgsJj4Q;
      byte[] numArray3 = numArray1;
      int index2 = num6;
      int num8 = index2 + 1;
      int num9 = (int) numArray3[index2];
      local2.\u0023\u003DzoRsAtmfOFDZe = (byte) num9;
      ref RGBA_Bytes local3 = ref nwsEwePinXgsJj4Q;
      byte[] numArray4 = numArray1;
      int index3 = num8;
      int num10 = index3 + 1;
      int num11 = (int) numArray4[index3];
      local3.\u0023\u003Dz4WHdt9g\u003D = (byte) num11;
      nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D = byte.MaxValue;
      _param1[_param2] = nwsEwePinXgsJj4Q;
      ++_param2;
      nbVwaPinzNveauDt.\u0023\u003DzXiQrjbw\u003D();
    }
    while (--_param5 != 0);
  }
}
