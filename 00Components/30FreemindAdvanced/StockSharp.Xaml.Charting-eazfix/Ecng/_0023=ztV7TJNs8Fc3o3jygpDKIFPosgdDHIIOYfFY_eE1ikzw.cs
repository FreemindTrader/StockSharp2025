// Decompiled with JetBrains decompiler
// Type: #=ztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzwjYTtd2Y4XJ9$LmrFYUarlJnNjSRG3t4w69ang3iq0Klw=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DztV7TJNs8Fc3o3jygpDKIFPosgdDHIIOYfFY_eE1ikzwjYTtd2Y4XJ9\u0024LmrFYUarlJnNjSRG3t4w69ang3iq0Klw\u003D(
  \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBHrPPgTSsTv2U_6gZ4ERd6w55yk5zndcPe_vl2HLhMqz\u0024A\u003D\u003D _param1,
  \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb0jFkvOD3SGxLkYwl94q52RF8Lm58bmuASR _param2) : 
  \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nhmhGtNdzmSduAAUU4XWAa3E1XIWWar5UU1A8kbYiZgM0qg75I\u003D(_param1, _param2, (\u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7\u00247IVjNcUWYRVrjRbV\u0024QDTRFg\u003D\u003D) null)
{
  public override void \u0023\u003DzvJVSzbY\u003D(
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D().\u0023\u003DzoLjFgpI\u003D((double) _param3 + (double) this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D(), (double) _param4 + (double) this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D(), _param5);
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D hjj4mAclInlkopAshpw = (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D) this.\u0023\u003DzL2OrHlw\u003D().\u0023\u003Dz8hY71usSYcKH();
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb0jFkvOD3SGxLkYwl94q52RF8Lm58bmuASR ywl94q52Rf8Lm58bmuAsr = this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D();
    float[] numArray = hjj4mAclInlkopAshpw.\u0023\u003Dz9b1_JhA\u003D(out int _);
    do
    {
      float num1;
      float num2;
      ywl94q52Rf8Lm58bmuAsr.\u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out num1, out num2);
      num1 -= this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D();
      num2 -= this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D();
      int num3 = (int) num1;
      int num4 = (int) num2;
      double num5;
      float num6 = (float) (num5 = 0.0);
      float num7 = (float) num5;
      float num8 = (float) num5;
      float num9 = (float) num5;
      num1 -= (float) num3;
      num2 -= (float) num4;
      int index1 = hjj4mAclInlkopAshpw.\u0023\u003DzHlHGfKJZNJsq(num3, num4);
      float num10 = (float) ((1.0 - (double) num1) * (1.0 - (double) num2));
      float num11 = num9 + num10 * numArray[index1 + 2];
      float num12 = num8 + num10 * numArray[index1 + 1];
      float num13 = num7 + num10 * numArray[index1];
      float num14 = num6 + num10 * numArray[index1 + 3];
      int index2 = index1 + 4;
      float num15 = num1 * (1f - num2);
      float num16 = num11 + num15 * numArray[index2 + 2];
      float num17 = num12 + num15 * numArray[index2 + 1];
      float num18 = num13 + num15 * numArray[index2];
      float num19 = num14 + num15 * numArray[index2 + 3];
      int num20 = num4 + 1;
      int index3 = hjj4mAclInlkopAshpw.\u0023\u003DzHlHGfKJZNJsq(num3, num20);
      float num21 = (1f - num1) * num2;
      float num22 = num16 + num21 * numArray[index3 + 2];
      float num23 = num17 + num21 * numArray[index3 + 1];
      float num24 = num18 + num21 * numArray[index3];
      float num25 = num19 + num21 * numArray[index3 + 3];
      int index4 = index3 + 4;
      float num26 = num1 * num2;
      float num27 = num22 + num26 * numArray[index4 + 2];
      float num28 = num23 + num26 * numArray[index4 + 1];
      float num29 = num24 + num26 * numArray[index4];
      float num30 = num25 + num26 * numArray[index4 + 3];
      \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D af0lRdvTglyLevjU0;
      af0lRdvTglyLevjU0.\u0023\u003Dz4WHdt9g\u003D = num27;
      af0lRdvTglyLevjU0.\u0023\u003DzoRsAtmfOFDZe = num28;
      af0lRdvTglyLevjU0.\u0023\u003DzcdKuX48ZXN_S = num29;
      af0lRdvTglyLevjU0.\u0023\u003DzKCqGEcs\u003D = num30;
      _param1[_param2] = af0lRdvTglyLevjU0;
      ++_param2;
      ywl94q52Rf8Lm58bmuAsr.\u0023\u003DzXiQrjbw\u003D();
    }
    while (--_param5 != 0);
  }
}
