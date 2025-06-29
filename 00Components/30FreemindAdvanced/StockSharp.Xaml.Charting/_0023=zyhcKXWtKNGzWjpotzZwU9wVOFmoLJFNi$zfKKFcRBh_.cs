// Decompiled with JetBrains decompiler
// Type: #=zyhcKXWtKNGzWjpotzZwU9wVOFmoLJFNi$zfKKFcRBh_0q8pJ0Xe_6I_OqXVkmaDa_ED$euc0efxjW6FoFqlN58U=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
internal struct \u0023\u003DzyhcKXWtKNGzWjpotzZwU9wVOFmoLJFNi\u0024zfKKFcRBh_0q8pJ0Xe_6I_OqXVkmaDa_ED\u0024euc0efxjW6FoFqlN58U\u003D : 
  \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjgvmjBXViFoefH7k8YXhfdQGUaSXGzhBS358MeZf
{
  public int \u0023\u003DzECFlF4okJtqtHXaphw\u003D\u003D() => 1;

  public void \u0023\u003DzeUPFg7rJt9ihcuMFyw\u003D\u003D(
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D[][] _param1,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D[] _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    _param2[_param3] = _param1[_param5][_param4];
  }

  public void \u0023\u003DzGEWK0ZYoSQG3uXAlwg\u003D\u003D(
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D _param1,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D[] _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    int num1;
    int num2 = num1 = 32768 /*0x8000*/;
    int num3 = num1;
    int num4 = num1;
    int num5 = num1;
    int num6 = _param4 >> 8;
    int num7 = _param5 >> 8;
    _param4 &= (int) byte.MaxValue;
    _param5 &= (int) byte.MaxValue;
    int index1;
    byte[] numArray1 = _param1.\u0023\u003DznPLKTp_rfCU9(num6, num7, out index1);
    int num8 = (256 /*0x0100*/ - _param4) * (256 /*0x0100*/ - _param5);
    int num9 = num5 + num8 * (int) numArray1[index1 + 2];
    int num10 = num4 + num8 * (int) numArray1[index1 + 1];
    int num11 = num3 + num8 * (int) numArray1[index1];
    int num12 = num2 + num8 * (int) numArray1[index1 + 3];
    int index2 = index1 + _param1.\u0023\u003DzQB4v2EccUot6eT2VRw\u003D\u003D();
    int num13 = _param4 * (256 /*0x0100*/ - _param5);
    int num14 = num9 + num13 * (int) numArray1[index2 + 2];
    int num15 = num10 + num13 * (int) numArray1[index2 + 1];
    int num16 = num11 + num13 * (int) numArray1[index2];
    int num17 = num12 + num13 * (int) numArray1[index2 + 3];
    byte[] numArray2 = _param1.\u0023\u003DznPLKTp_rfCU9(num6, num7 + 1, out index2);
    int num18 = (256 /*0x0100*/ - _param4) * _param5;
    int num19 = num14 + num18 * (int) numArray2[index2 + 2];
    int num20 = num15 + num18 * (int) numArray2[index2 + 1];
    int num21 = num16 + num18 * (int) numArray2[index2];
    int num22 = num17 + num18 * (int) numArray2[index2 + 3];
    int index3 = index2 + _param1.\u0023\u003DzQB4v2EccUot6eT2VRw\u003D\u003D();
    int num23 = _param4 * _param5;
    int num24 = num19 + num23 * (int) numArray2[index3 + 2];
    int num25 = num20 + num23 * (int) numArray2[index3 + 1];
    int num26 = num21 + num23 * (int) numArray2[index3];
    int num27 = num22 + num23 * (int) numArray2[index3 + 3];
    _param2[_param3].\u0023\u003Dz4WHdt9g\u003D = (byte) (num24 >> 16 /*0x10*/);
    _param2[_param3].\u0023\u003DzoRsAtmfOFDZe = (byte) (num25 >> 16 /*0x10*/);
    _param2[_param3].\u0023\u003DzcdKuX48ZXN_S = (byte) (num26 >> 16 /*0x10*/);
    _param2[_param3].\u0023\u003DzKCqGEcs\u003D = (byte) (num27 >> 16 /*0x10*/);
  }
}
