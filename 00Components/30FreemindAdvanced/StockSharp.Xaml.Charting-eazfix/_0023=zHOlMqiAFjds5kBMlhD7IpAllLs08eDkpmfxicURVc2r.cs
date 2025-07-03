// Decompiled with JetBrains decompiler
// Type: #=zHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A$CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal sealed class \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D : 
  \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024gqLds4g\u0024IDFMMrZlo8\u0024Z\u0024k62_nBRoTA2Qr_Zfexp3BOaM5dc0BCJlb_,
  \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOESA9yl1dafdSqQDeAMKXaBPbCxPlPQgez5bfFbgS\u0024CknPn64g\u003D
{
  private static int[] \u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6 = new int[512 /*0x0200*/];
  private \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D \u0023\u003DzCqxkgh79m36c;

  public \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D(
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D _param1)
  {
    this.\u0023\u003DzCqxkgh79m36c = _param1;
    if (\u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[2] != 0)
      return;
    for (int val1 = 0; val1 < \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6.Length; ++val1)
      \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[val1] = Math.Min(val1, (int) byte.MaxValue);
  }

  public \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D \u0023\u003Dzk1hQXoI0sJ98Ijd3VA\u003D\u003D(
    byte[] _param1,
    int _param2)
  {
    return new \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D((int) _param1[_param2 + 2], (int) _param1[_param2 + 1], (int) _param1[_param2], (int) _param1[_param2 + 3]);
  }

  public void \u0023\u003DzDjUCkxvYu2E5(
    byte[] _param1,
    int _param2,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D _param3,
    int _param4)
  {
    for (int index = 0; index < _param4; ++index)
    {
      _param1[_param2 + 2] = _param3.\u0023\u003Dz4WHdt9g\u003D;
      _param1[_param2 + 1] = _param3.\u0023\u003DzoRsAtmfOFDZe;
      _param1[_param2] = _param3.\u0023\u003DzcdKuX48ZXN_S;
      _param1[_param2 + 3] = _param3.\u0023\u003DzKCqGEcs\u003D;
      _param2 += 4;
    }
  }

  public void \u0023\u003Dz1sAbEWOIYGyA(
    byte[] _param1,
    int _param2,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D _param3)
  {
    int num1 = (int) byte.MaxValue - (int) (byte) \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[this.\u0023\u003DzCqxkgh79m36c.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() * (int) _param3.\u0023\u003DzKCqGEcs\u003D + (int) byte.MaxValue >> 8];
    int num2 = (int) (byte) \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[this.\u0023\u003DzCqxkgh79m36c.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() * (int) _param3.\u0023\u003Dz4WHdt9g\u003D + (int) byte.MaxValue >> 8];
    int num3 = (int) (byte) \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[this.\u0023\u003DzCqxkgh79m36c.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() * (int) _param3.\u0023\u003DzoRsAtmfOFDZe + (int) byte.MaxValue >> 8];
    int num4 = (int) (byte) \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[this.\u0023\u003DzCqxkgh79m36c.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() * (int) _param3.\u0023\u003DzcdKuX48ZXN_S + (int) byte.MaxValue >> 8];
    int num5 = \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 2] * num1 + (int) byte.MaxValue >> 8) + num2];
    int num6 = \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 1] * num1 + (int) byte.MaxValue >> 8) + num3];
    int num7 = \u0023\u003DzHOlMqiAFjds5kBMlhD7IpAllLs08eDkpmfxicURVc2r5hdGvDs_A\u0024CtFTOPaXF_OIHbT9Ni5Dmaj0wA7WLhIFCc\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2] * num1 + (int) byte.MaxValue >> 8) + num4];
    _param1[_param2 + 2] = (byte) num5;
    _param1[_param2 + 1] = (byte) num6;
    _param1[_param2] = (byte) num7;
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
    if (!_param7)
      throw new NotImplementedException("need to consider the polyColor");
    if (_param5[_param6] != byte.MaxValue)
      throw new NotImplementedException("need to consider the polyColor");
    for (int index = 0; index < _param8; ++index)
    {
      this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4]);
      ++_param4;
      _param2 += 4;
    }
  }
}
