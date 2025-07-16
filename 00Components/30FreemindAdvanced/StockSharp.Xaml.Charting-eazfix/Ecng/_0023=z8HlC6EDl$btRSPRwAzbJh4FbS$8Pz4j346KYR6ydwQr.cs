// Decompiled with JetBrains decompiler
// Type: #=z8HlC6EDl$btRSPRwAzbJh4FbS$8Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D : 
  \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024gqLds4g\u0024IDFMMrZlo8\u0024Z\u0024k62_nBRoTA2Qr_Zfexp3BOaM5dc0BCJlb_,
  \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOESA9yl1dafdSqQDeAMKXaBPbCxPlPQgez5bfFbgS\u0024CknPn64g\u003D
{
  private static int[] \u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6 = new int[512 /*0x0200*/];

  public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D()
  {
    if (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[2] != 0)
      return;
    for (int val1 = 0; val1 < \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6.Length; ++val1)
      \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[val1] = Math.Min(val1, (int) byte.MaxValue);
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
    int num1 = (int) byte.MaxValue - (int) _param3.\u0023\u003DzKCqGEcs\u003D;
    int num2 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 2] * num1 + (int) byte.MaxValue >> 8) + (int) _param3.\u0023\u003Dz4WHdt9g\u003D];
    int num3 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 1] * num1 + (int) byte.MaxValue >> 8) + (int) _param3.\u0023\u003DzoRsAtmfOFDZe];
    int num4 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2] * num1 + (int) byte.MaxValue >> 8) + (int) _param3.\u0023\u003DzcdKuX48ZXN_S];
    int num5 = (int) _param1[_param2 + 3];
    _param1[_param2 + 2] = (byte) num2;
    _param1[_param2 + 1] = (byte) num3;
    _param1[_param2] = (byte) num4;
    _param1[_param2 + 3] = (byte) ((int) byte.MaxValue - \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[num1 * ((int) byte.MaxValue - num5) + (int) byte.MaxValue >> 8]);
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
    if (_param7)
    {
      if (_param5[_param6] == byte.MaxValue)
      {
        for (int index = 0; index < _param8; ++index)
        {
          \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D nwsEwePinXgsJj4Q = _param3[_param4];
          if (nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D == byte.MaxValue)
          {
            _param1[_param2 + 2] = nwsEwePinXgsJj4Q.\u0023\u003Dz4WHdt9g\u003D;
            _param1[_param2 + 1] = nwsEwePinXgsJj4Q.\u0023\u003DzoRsAtmfOFDZe;
            _param1[_param2] = nwsEwePinXgsJj4Q.\u0023\u003DzcdKuX48ZXN_S;
            _param1[_param2 + 3] = byte.MaxValue;
          }
          else
          {
            int num1 = (int) byte.MaxValue - (int) nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D;
            int num2 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 2] * num1 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003Dz4WHdt9g\u003D];
            int num3 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 1] * num1 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003DzoRsAtmfOFDZe];
            int num4 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2] * num1 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003DzcdKuX48ZXN_S];
            int num5 = (int) _param1[_param2 + 3];
            _param1[_param2 + 2] = (byte) num2;
            _param1[_param2 + 1] = (byte) num3;
            _param1[_param2] = (byte) num4;
            _param1[_param2 + 3] = (byte) ((int) byte.MaxValue - \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[num1 * ((int) byte.MaxValue - num5) + (int) byte.MaxValue >> 8]);
          }
          ++_param4;
          _param2 += 4;
        }
      }
      else
      {
        for (int index = 0; index < _param8; ++index)
        {
          \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D nwsEwePinXgsJj4Q = _param3[_param4];
          int num6 = ((int) nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D * (int) _param5[_param6] + (int) byte.MaxValue) / 256 /*0x0100*/;
          switch (num6)
          {
            case 0:
              continue;
            case (int) byte.MaxValue:
              _param1[_param2 + 2] = nwsEwePinXgsJj4Q.\u0023\u003Dz4WHdt9g\u003D;
              _param1[_param2 + 1] = nwsEwePinXgsJj4Q.\u0023\u003DzoRsAtmfOFDZe;
              _param1[_param2] = nwsEwePinXgsJj4Q.\u0023\u003DzcdKuX48ZXN_S;
              _param1[_param2 + 3] = (byte) num6;
              break;
            default:
              int num7 = (int) byte.MaxValue - num6;
              int num8 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 2] * num7 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003Dz4WHdt9g\u003D];
              int num9 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 1] * num7 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003DzoRsAtmfOFDZe];
              int num10 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2] * num7 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003DzcdKuX48ZXN_S];
              int num11 = (int) _param1[_param2 + 3];
              _param1[_param2 + 2] = (byte) num8;
              _param1[_param2 + 1] = (byte) num9;
              _param1[_param2] = (byte) num10;
              _param1[_param2 + 3] = (byte) ((int) byte.MaxValue - \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[num7 * ((int) byte.MaxValue - num11) + (int) byte.MaxValue >> 8]);
              break;
          }
          ++_param4;
          _param2 += 4;
        }
      }
    }
    else
    {
      for (int index = 0; index < _param8; ++index)
      {
        \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q\u003D\u003D nwsEwePinXgsJj4Q = _param3[_param4];
        int num12 = ((int) nwsEwePinXgsJj4Q.\u0023\u003DzKCqGEcs\u003D * (int) _param5[_param6] + (int) byte.MaxValue) / 256 /*0x0100*/;
        if (num12 == (int) byte.MaxValue)
        {
          _param1[_param2 + 2] = nwsEwePinXgsJj4Q.\u0023\u003Dz4WHdt9g\u003D;
          _param1[_param2 + 1] = nwsEwePinXgsJj4Q.\u0023\u003DzoRsAtmfOFDZe;
          _param1[_param2] = nwsEwePinXgsJj4Q.\u0023\u003DzcdKuX48ZXN_S;
          _param1[_param2 + 3] = (byte) num12;
        }
        else if (num12 > 0)
        {
          int num13 = (int) byte.MaxValue - num12;
          int num14 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 2] * num13 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003Dz4WHdt9g\u003D];
          int num15 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2 + 1] * num13 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003DzoRsAtmfOFDZe];
          int num16 = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[((int) _param1[_param2] * num13 + (int) byte.MaxValue >> 8) + (int) nwsEwePinXgsJj4Q.\u0023\u003DzcdKuX48ZXN_S];
          int num17 = (int) _param1[_param2 + 3];
          _param1[_param2 + 2] = (byte) num14;
          _param1[_param2 + 1] = (byte) num15;
          _param1[_param2] = (byte) num16;
          _param1[_param2 + 3] = (byte) ((int) byte.MaxValue - \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D.\u0023\u003Dzj0UOcNgA12GrmdK3Yl_cpR5KTgS6[num13 * ((int) byte.MaxValue - num17) + (int) byte.MaxValue >> 8]);
        }
        ++_param4;
        ++_param6;
        _param2 += 4;
      }
    }
  }
}
