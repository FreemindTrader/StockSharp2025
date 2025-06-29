// Decompiled with JetBrains decompiler
// Type: #=zQN2Zes8h9tElvYmX48o49LUqCmhVH6715qLutxVwykbdZt0kUd30ELKgdFuVQrPMLcIuZEfMDn_AgnK_Dw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal sealed class \u0023\u003DzQN2Zes8h9tElvYmX48o49LUqCmhVH6715qLutxVwykbdZt0kUd30ELKgdFuVQrPMLcIuZEfMDn_AgnK_Dw\u003D\u003D : 
  \u0023\u003DzuxHg1RShgoNoz91lIJvsfNpaIZHYCAi8nIZQBxHeA1YPjtvfCM4Cf\u0024SW20w4MlG5kJm\u0024WkUzOQfp
{
  public \u0023\u003DzQN2Zes8h9tElvYmX48o49LUqCmhVH6715qLutxVwykbdZt0kUd30ELKgdFuVQrPMLcIuZEfMDn_AgnK_Dw\u003D\u003D(
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0LKrMWRP8HpfFNPgYY55tLjfw\u003D\u003D _param1,
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT _param2,
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQ6PRW443CXjnpjY_jRLnxeTxLdSjL9CR9M_QGcrFkI\u0024EwQ\u003D\u003D _param3)
    : base(_param1, _param2, _param3)
  {
    if (_param1.\u0023\u003Dz8hY71usSYcKH().\u0023\u003DztvwmoyKu8oUlLP4_gA\u003D\u003D().\u0023\u003DzVAoYC\u0024MJfjKU3EUhiA\u003D\u003D() != 32 /*0x20*/)
      throw new FormatException(XXX.SSS(-539324097));
  }

  public override void \u0023\u003DzvJVSzbY\u003D(
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT nbVwaPinzNveauDt = this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D();
    nbVwaPinzNveauDt.\u0023\u003DzoLjFgpI\u003D((double) _param3 + this.\u0023\u003Dz5c6JtFGOLiHGd1cR5w\u003D\u003D(), (double) _param4 + this.\u0023\u003DzD8L\u0024XwNa3mshC_L9ug\u003D\u003D(), _param5);
    int[] numArray1 = new int[4];
    int[] numArray2 = this.\u0023\u003DzM42BFj8\u003D().\u0023\u003Dztrjmxx0aHjVH();
    int num1 = this.\u0023\u003DzM42BFj8\u003D().\u0023\u003DzsG\u0024bkFt924ExZZI2LQ\u003D\u003D();
    int num2 = num1 << 8;
    int[] numArray3 = numArray2;
    do
    {
      nbVwaPinzNveauDt.\u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out _param3, out _param4);
      int num3;
      int num4;
      nbVwaPinzNveauDt.\u0023\u003DzsdUKR\u0024YKO9R\u0024(out num3, out num4);
      this.\u0023\u003Dzkol1j7VmSj1\u0024(ref num3, ref num4);
      int num5 = 65536 /*0x010000*/ / num3;
      int num6 = 65536 /*0x010000*/ / num4;
      int num7 = num1 * num3 >> 1;
      int num8 = num1 * num4 >> 1;
      int num9 = num1 * num3 + (int) byte.MaxValue >> 8;
      _param3 += this.\u0023\u003DzxioLP9oyjKFBIHWWVA\u003D\u003D() - num7;
      _param4 += this.\u0023\u003Dz2YwJt7tn\u0024hxMRoRU\u0024A\u003D\u003D() - num8;
      numArray1[0] = numArray1[1] = numArray1[2] = numArray1[3] = 8192 /*0x2000*/;
      int num10 = _param4 >> 8;
      int index1 = ((int) byte.MaxValue - (_param4 & (int) byte.MaxValue)) * num6 >> 8;
      int num11 = 0;
      int num12 = _param3 >> 8;
      int num13 = ((int) byte.MaxValue - (_param3 & (int) byte.MaxValue)) * num5 >> 8;
      int index2;
      byte[] numArray4 = this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003DzmiTEKiA\u003D(num12, num10, num9, out index2);
      while (true)
      {
        int num14 = numArray3[index1];
        int index3 = num13;
        while (true)
        {
          int num15 = num14 * numArray3[index3] + 8192 /*0x2000*/ >> 14;
          numArray1[0] += (int) numArray4[index2 + 2] * num15;
          numArray1[1] += (int) numArray4[index2 + 1] * num15;
          numArray1[2] += (int) numArray4[index2] * num15;
          numArray1[3] += (int) numArray4[index2 + 3] * num15;
          num11 += num15;
          index3 += num5;
          if (index3 < num2)
            numArray4 = this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003DziwTQ98wsEeu3(out index2);
          else
            break;
        }
        index1 += num6;
        if (index1 < num2)
          numArray4 = this.\u0023\u003DzXfsXM99CTPMy().\u0023\u003DzXgTbLkAgh1Pd(out index2);
        else
          break;
      }
      numArray1[0] /= num11;
      numArray1[1] /= num11;
      numArray1[2] /= num11;
      numArray1[3] /= num11;
      if (numArray1[0] < 0)
        numArray1[0] = 0;
      if (numArray1[1] < 0)
        numArray1[1] = 0;
      if (numArray1[2] < 0)
        numArray1[2] = 0;
      if (numArray1[3] < 0)
        numArray1[3] = 0;
      if (numArray1[0] > (int) byte.MaxValue)
        numArray1[0] = (int) byte.MaxValue;
      if (numArray1[1] > (int) byte.MaxValue)
        numArray1[1] = (int) byte.MaxValue;
      if (numArray1[2] > (int) byte.MaxValue)
        numArray1[2] = (int) byte.MaxValue;
      if (numArray1[3] > (int) byte.MaxValue)
        numArray1[3] = (int) byte.MaxValue;
      _param1[_param2].\u0023\u003Dz4WHdt9g\u003D = (byte) numArray1[0];
      _param1[_param2].\u0023\u003DzoRsAtmfOFDZe = (byte) numArray1[1];
      _param1[_param2].\u0023\u003DzcdKuX48ZXN_S = (byte) numArray1[2];
      _param1[_param2].\u0023\u003DzKCqGEcs\u003D = (byte) numArray1[3];
      ++_param2;
      this.\u0023\u003DzGNzgb4HQGBekYN3Uq0M_3pY\u003D().\u0023\u003DzXiQrjbw\u003D();
    }
    while (--_param5 != 0);
  }
}
