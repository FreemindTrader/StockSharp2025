// Decompiled with JetBrains decompiler
// Type: #=z7oKBks6ccXdMBOl$qXdcQAaMpPLBW$Zg11zdsYrjLM9nJb1ADJMhUVI0gywA65$G8nuhFUmGTW3S
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQAaMpPLBW\u0024Zg11zdsYrjLM9nJb1ADJMhUVI0gywA65\u0024G8nuhFUmGTW3S : 
  \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nhmhGtNdzmSduAAUU4XWAa3E1XIWWar5UU1A8kbYiZgM0qg75I\u003D
{
  public \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQAaMpPLBW\u0024Zg11zdsYrjLM9nJb1ADJMhUVI0gywA65\u0024G8nuhFUmGTW3S(
    \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBHrPPgTSsTv2U_6gZ4ERd6w55yk5zndcPe_vl2HLhMqz\u0024A\u003D\u003D _param1,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb0jFkvOD3SGxLkYwl94q52RF8Lm58bmuASR _param2,
    IImageFilterFunction _param3)
    : base(_param1, _param2, _param3)
  {
    if (_param1.\u0023\u003Dz8hY71usSYcKH().\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D() != 4)
      throw new NotSupportedException("span_image_filter_rgba must have a 32 bit DestImage");
  }

  public override void \u0023\u003DzvJVSzbY\u003D(
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D().\u0023\u003DzoLjFgpI\u003D((double) _param3 + (double) this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D(), (double) _param4 + (double) this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D(), _param5);
    int num1 = (int) this.\u0023\u003Dzyo0OJMP0VzUq.radius() * 2;
    int num2 = -(num1 / 2 - 1);
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb0jFkvOD3SGxLkYwl94q52RF8Lm58bmuASR ywl94q52Rf8Lm58bmuAsr = this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D();
    \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBHrPPgTSsTv2U_6gZ4ERd6w55yk5zndcPe_vl2HLhMqz\u0024A\u003D\u003D erd6w55yk5zndcPeVl2HlhMqzA = this.\u0023\u003DzL2OrHlw\u003D();
    do
    {
      float num3 = (float) _param3;
      float num4 = (float) _param4;
      ywl94q52Rf8Lm58bmuAsr.\u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out num3, out num4);
      int num5 = (int) num3;
      int num6 = (int) num4;
      Vector2 jnelpsqX4Q78W2Ejd1 = new Vector2((double) num3, (double) num4);
      Vector2 jnelpsqX4Q78W2Ejd2 = new Vector2((double) (num5 + num2), (double) (num6 + num2));
      double num7;
      float num8 = (float) (num7 = 0.0);
      float num9 = (float) num7;
      float num10 = (float) num7;
      float num11 = (float) num7;
      int num12 = num1;
      int index;
      float[] numArray = erd6w55yk5zndcPeVl2HlhMqzA.\u0023\u003DzmiTEKiA\u003D(num5 + num2, num6 + num2, num1, out index);
      float num13 = 0.0f;
      while (true)
      {
        float num14 = (float) this.\u0023\u003Dzyo0OJMP0VzUq.calc_weight(Math.Sqrt((jnelpsqX4Q78W2Ejd2.dje_zLPL6EZPA_ejd - jnelpsqX4Q78W2Ejd1.dje_zLPL6EZPA_ejd) * (jnelpsqX4Q78W2Ejd2.dje_zLPL6EZPA_ejd - jnelpsqX4Q78W2Ejd1.dje_zLPL6EZPA_ejd)));
        int num15 = num1;
        while (true)
        {
          float num16 = (float) this.\u0023\u003Dzyo0OJMP0VzUq.calc_weight(Math.Sqrt((jnelpsqX4Q78W2Ejd2.dje_z3GBAX47U_ejd - jnelpsqX4Q78W2Ejd1.dje_z3GBAX47U_ejd) * (jnelpsqX4Q78W2Ejd2.dje_z3GBAX47U_ejd - jnelpsqX4Q78W2Ejd1.dje_z3GBAX47U_ejd))) * num14;
          num9 += num16 * numArray[index + 2];
          num10 += num16 * numArray[index + 1];
          num11 += num16 * numArray[index];
          num8 += num16 * numArray[index + 3];
          num13 += num16;
          ++jnelpsqX4Q78W2Ejd2.dje_z3GBAX47U_ejd;
          if (--num15 != 0)
            erd6w55yk5zndcPeVl2HlhMqzA.\u0023\u003DziwTQ98wsEeu3(out index);
          else
            break;
        }
        jnelpsqX4Q78W2Ejd2.dje_z3GBAX47U_ejd -= (double) num1;
        if (--num12 != 0)
        {
          ++jnelpsqX4Q78W2Ejd2.dje_zLPL6EZPA_ejd;
          numArray = erd6w55yk5zndcPeVl2HlhMqzA.\u0023\u003DzXgTbLkAgh1Pd(out index);
        }
        else
          break;
      }
      if ((double) num11 < 0.0)
        num11 = 0.0f;
      if ((double) num11 > 1.0)
        num11 = 1f;
      if ((double) num9 < 0.0)
        num9 = 0.0f;
      if ((double) num9 > 1.0)
        num9 = 1f;
      if ((double) num10 < 0.0)
        num10 = 0.0f;
      if ((double) num10 > 1.0)
        num10 = 1f;
      _param1[_param2].\u0023\u003Dz4WHdt9g\u003D = num9;
      _param1[_param2].\u0023\u003DzoRsAtmfOFDZe = num10;
      _param1[_param2].\u0023\u003DzcdKuX48ZXN_S = num11;
      _param1[_param2].\u0023\u003DzKCqGEcs\u003D = 1f;
      ++_param2;
      ywl94q52Rf8Lm58bmuAsr.\u0023\u003DzXiQrjbw\u003D();
    }
    while (--_param5 != 0);
  }
}
