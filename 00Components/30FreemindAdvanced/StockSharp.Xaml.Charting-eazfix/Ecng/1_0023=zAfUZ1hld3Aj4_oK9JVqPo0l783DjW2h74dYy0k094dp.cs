// Decompiled with JetBrains decompiler
// Type: #=zAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D : 
  BlenderBaseBGR,
  IBlenderByte
{
  private static int[] \u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6 = new int[512 /*0x0200*/];

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D()
  {
    if (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[2] != 0)
      return;
    for (int val1 = 0; val1 < \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6.Length; ++val1)
      \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[val1] = Math.Min(val1, (int) byte.MaxValue);
  }

  public RGBA_Bytes \u0023\u003Dzk1hQXoI0sJ98Ijd3VA\u003D\u003D(
    byte[] _param1,
    int _param2)
  {
    return new RGBA_Bytes((int) _param1[_param2 + 2], (int) _param1[_param2 + 1], (int) _param1[_param2], (int) byte.MaxValue);
  }

  public void \u0023\u003DzDjUCkxvYu2E5(
    byte[] _param1,
    int _param2,
    RGBA_Bytes _param3,
    int _param4)
  {
    do
    {
      _param1[_param2 + 2] = _param3.\u0023\u003Dz4WHdt9g\u003D;
      _param1[_param2 + 1] = _param3.\u0023\u003DzoRsAtmfOFDZe;
      _param1[_param2] = _param3.\u0023\u003DzcdKuX48ZXN_S;
      _param2 += 3;
    }
    while (--_param4 != 0);
  }

  public void \u0023\u003Dz1sAbEWOIYGyA(
    byte[] _param1,
    int _param2,
    RGBA_Bytes _param3)
  {
    if (_param3.\u0023\u003DzKCqGEcs\u003D == byte.MaxValue)
    {
      _param1[_param2 + 2] = _param3.\u0023\u003Dz4WHdt9g\u003D;
      _param1[_param2 + 1] = _param3.\u0023\u003DzoRsAtmfOFDZe;
      _param1[_param2] = _param3.\u0023\u003DzcdKuX48ZXN_S;
    }
    else
    {
      int num1 = (int) byte.MaxValue - (int) _param3.\u0023\u003DzKCqGEcs\u003D;
      int num2 = \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 2] * num1 + (int) byte.MaxValue >> 8) + (int) _param3.\u0023\u003Dz4WHdt9g\u003D];
      int num3 = \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 1] * num1 + (int) byte.MaxValue >> 8) + (int) _param3.\u0023\u003DzoRsAtmfOFDZe];
      int num4 = \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dpRXMU43Hi7DG3h71qCFNqPajLZEFedthRQ8q53IA\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2] * num1 + (int) byte.MaxValue >> 8) + (int) _param3.\u0023\u003DzcdKuX48ZXN_S];
      _param1[_param2 + 2] = (byte) num2;
      _param1[_param2 + 1] = (byte) num3;
      _param1[_param2] = (byte) num4;
    }
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
    if (_param7)
    {
      int num = (int) _param5[_param6];
      if (num == (int) byte.MaxValue)
      {
        do
        {
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4++]);
          _param2 += 3;
        }
        while (--_param8 != 0);
      }
      else
      {
        do
        {
          _param3[_param4].\u0023\u003DzKCqGEcs\u003D = (byte) ((int) _param3[_param4].\u0023\u003DzKCqGEcs\u003D * num + (int) byte.MaxValue >> 8);
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4]);
          _param2 += 3;
          ++_param4;
        }
        while (--_param8 != 0);
      }
    }
    else
    {
      do
      {
        int num = (int) _param5[_param6++];
        if (num == (int) byte.MaxValue)
        {
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3[_param4]);
        }
        else
        {
          RGBA_Bytes nwsEwePinXgsJj4Q = _param3[_param4];
          nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D = (byte) ((int) nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D * num + (int) byte.MaxValue >> 8);
          this.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, nwsEwePinXgsJj4Q);
        }
        _param2 += 3;
        ++_param4;
      }
      while (--_param8 != 0);
    }
  }
}
