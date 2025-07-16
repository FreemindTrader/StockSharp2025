// Decompiled with JetBrains decompiler
// Type: #=zjFV3E4nzZ192p80vvoilfzf1vG0DP$PINEJIKqfnUorAy5Vbf$dLMsbJY$8JxLPZGJ8qsYbxp26SxiO45w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUorAy5Vbf\u0024dLMsbJY\u00248JxLPZGJ8qsYbxp26SxiO45w\u003D\u003D : 
  \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024gqLds4g\u0024IDFMMrZlo8\u0024Z\u0024k62_nBRoTA2Qr_Zfexp3BOaM5dc0BCJlb_,
  IBlenderByte
{
  private \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D \u0023\u003DzY\u0024iy3H6MDQlk;

  public \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUorAy5Vbf\u0024dLMsbJY\u00248JxLPZGJ8qsYbxp26SxiO45w\u003D\u003D()
  {
    this.\u0023\u003DzY\u0024iy3H6MDQlk = new \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D();
  }

  public \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUorAy5Vbf\u0024dLMsbJY\u00248JxLPZGJ8qsYbxp26SxiO45w\u003D\u003D(
    \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D _param1)
  {
    this.\u0023\u003DzY\u0024iy3H6MDQlk = _param1;
  }

  public void \u0023\u003DzruxDfy35wG0J(
    \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D _param1)
  {
    this.\u0023\u003DzY\u0024iy3H6MDQlk = _param1;
  }

  public RGBA_Bytes \u0023\u003Dzk1hQXoI0sJ98Ijd3VA\u003D\u003D(
    byte[] _param1,
    int _param2)
  {
    return new RGBA_Bytes((int) _param1[_param2 + 2], (int) _param1[_param2 + 1], (int) _param1[_param2], (int) _param1[_param2 + 3]);
  }

  public void \u0023\u003DzDjUCkxvYu2E5(
    byte[] _param1,
    int _param2,
    RGBA_Bytes _param3,
    int _param4)
  {
    do
    {
      _param1[_param2 + 2] = this.\u0023\u003DzY\u0024iy3H6MDQlk.\u0023\u003Dz9YtkX8U\u003D((int) _param3.\u0023\u003Dz4WHdt9g\u003D);
      _param1[_param2 + 1] = this.\u0023\u003DzY\u0024iy3H6MDQlk.\u0023\u003Dz9YtkX8U\u003D((int) _param3.\u0023\u003DzoRsAtmfOFDZe);
      _param1[_param2] = this.\u0023\u003DzY\u0024iy3H6MDQlk.\u0023\u003Dz9YtkX8U\u003D((int) _param3.\u0023\u003DzcdKuX48ZXN_S);
      _param1[_param2 + 3] = this.\u0023\u003DzY\u0024iy3H6MDQlk.\u0023\u003Dz9YtkX8U\u003D((int) _param3.\u0023\u003DzKCqGEcs\u003D);
      _param2 += 4;
    }
    while (--_param4 != 0);
  }

  public void \u0023\u003Dz1sAbEWOIYGyA(
    byte[] _param1,
    int _param2,
    RGBA_Bytes _param3)
  {
    int num1 = (int) _param1[_param2 + 2];
    int num2 = (int) _param1[_param2 + 1];
    int num3 = (int) _param1[_param2];
    int num4 = (int) _param1[_param2 + 3];
    _param1[_param2 + 2] = this.\u0023\u003DzY\u0024iy3H6MDQlk.\u0023\u003Dz9YtkX8U\u003D((int) (byte) (((int) _param3.\u0023\u003Dz4WHdt9g\u003D - num1) * (int) _param3.\u0023\u003DzKCqGEcs\u003D + (num1 << 8) >> 8));
    _param1[_param2 + 1] = this.\u0023\u003DzY\u0024iy3H6MDQlk.\u0023\u003Dz9YtkX8U\u003D((int) (byte) (((int) _param3.\u0023\u003DzoRsAtmfOFDZe - num2) * (int) _param3.\u0023\u003DzKCqGEcs\u003D + (num2 << 8) >> 8));
    _param1[_param2] = this.\u0023\u003DzY\u0024iy3H6MDQlk.\u0023\u003Dz9YtkX8U\u003D((int) (byte) (((int) _param3.\u0023\u003DzcdKuX48ZXN_S - num3) * (int) _param3.\u0023\u003DzKCqGEcs\u003D + (num3 << 8) >> 8));
    _param1[3] = (byte) ((int) _param3.\u0023\u003DzKCqGEcs\u003D + num4 - ((int) _param3.\u0023\u003DzKCqGEcs\u003D * num4 + (int) byte.MaxValue >> 8));
  }

  public void \u0023\u003Dz_A3pQLKo8i_c(
    byte[] _param1,
    int _param2,
    RGBA_Bytes[] _param3,
    int _param4,
    byte[] _param5,
    int _param6,
    bool _param7,
    int _param8)
  {
    throw new NotImplementedException();
  }
}
