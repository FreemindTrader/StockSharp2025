// Decompiled with JetBrains decompiler
// Type: #=zUTCl8jvgS_4weG5YU7g$0UvSdtxx4ULRrY0tRkq5Lb2WIbkHw6NWSEwePinXgsJJ4Q==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
public struct RGBA_Bytes : 
  IColorType
{
  
  public byte \u0023\u003DzcdKuX48ZXN_S;
  
  public byte \u0023\u003DzoRsAtmfOFDZe;
  
  public byte \u0023\u003Dz4WHdt9g\u003D;
  
  public byte \u0023\u003DzKCqGEcs\u003D;
  
  public static readonly RGBA_Bytes \u0023\u003DziT5e12c\u003D = new RGBA_Bytes((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzmQK\u0024Ms_\u0024kEI_ = new RGBA_Bytes(225, 225, 225, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003Dz3jsTr080\u00242a0 = new RGBA_Bytes(125, 125, 125, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzXMsQczM\u003D = new RGBA_Bytes(0, 0, 0, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003Dzr\u0024CAwAU\u003D = new RGBA_Bytes((int) byte.MaxValue, 0, 0, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzQ11P_zRSPdMN = new RGBA_Bytes((int) byte.MaxValue, (int) sbyte.MaxValue, 0, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003Dz8Nx8mZ4\u003D = new RGBA_Bytes(0, (int) byte.MaxValue, 0, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzCfZb6Vg\u003D = new RGBA_Bytes(0, 0, (int) byte.MaxValue, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzeSZjfwP9PT8\u0024 = new RGBA_Bytes(75, 0, 130, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003Dz\u0024wMcc5BFs9G_ = new RGBA_Bytes(143, 0, (int) byte.MaxValue, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzokZEel\u0024u0c1N = new RGBA_Bytes(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzeoCalpffM4n8 = new RGBA_Bytes((int) byte.MaxValue, 0, (int) byte.MaxValue, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003DzD0g4s6O4aQOc = new RGBA_Bytes((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
  
  public static readonly RGBA_Bytes \u0023\u003Dz97mMp8zEecupEsHcfw\u003D\u003D = new RGBA_Bytes(154, 205, 50, (int) byte.MaxValue);

  public RGBA_Bytes(
    int _param1,
    int _param2,
    int _param3)
    : this(_param1, _param2, _param3, (int) byte.MaxValue)
  {
  }

  public RGBA_Bytes(
    int _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = (byte) _param1;
    this.\u0023\u003DzoRsAtmfOFDZe = (byte) _param2;
    this.\u0023\u003DzcdKuX48ZXN_S = (byte) _param3;
    this.\u0023\u003DzKCqGEcs\u003D = (byte) _param4;
  }

  public RGBA_Bytes(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param1 * (double) byte.MaxValue);
    this.\u0023\u003DzoRsAtmfOFDZe = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param2 * (double) byte.MaxValue);
    this.\u0023\u003DzcdKuX48ZXN_S = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param3 * (double) byte.MaxValue);
    this.\u0023\u003DzKCqGEcs\u003D = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param4 * (double) byte.MaxValue);
  }

  public RGBA_Bytes(
    double _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param1 * (double) byte.MaxValue);
    this.\u0023\u003DzoRsAtmfOFDZe = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param2 * (double) byte.MaxValue);
    this.\u0023\u003DzcdKuX48ZXN_S = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param3 * (double) byte.MaxValue);
    this.\u0023\u003DzKCqGEcs\u003D = byte.MaxValue;
  }

  public RGBA_Bytes(
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param1,
    double _param2)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7((double) _param1.\u0023\u003Dz4WHdt9g\u003D * (double) byte.MaxValue);
    this.\u0023\u003DzoRsAtmfOFDZe = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7((double) _param1.\u0023\u003DzoRsAtmfOFDZe * (double) byte.MaxValue);
    this.\u0023\u003DzcdKuX48ZXN_S = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7((double) _param1.\u0023\u003DzcdKuX48ZXN_S * (double) byte.MaxValue);
    this.\u0023\u003DzKCqGEcs\u003D = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7(_param2 * (double) byte.MaxValue);
  }

  public RGBA_Bytes(
    RGBA_Bytes _param1)
    : this(_param1, (int) _param1.\u0023\u003DzKCqGEcs\u003D)
  {
  }

  public RGBA_Bytes(
    RGBA_Bytes _param1,
    int _param2)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = _param1.\u0023\u003Dz4WHdt9g\u003D;
    this.\u0023\u003DzoRsAtmfOFDZe = _param1.\u0023\u003DzoRsAtmfOFDZe;
    this.\u0023\u003DzcdKuX48ZXN_S = _param1.\u0023\u003DzcdKuX48ZXN_S;
    this.\u0023\u003DzKCqGEcs\u003D = (byte) _param2;
  }

  public RGBA_Bytes(
    uint _param1)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = (byte) (_param1 >> 16 /*0x10*/);
    this.\u0023\u003DzoRsAtmfOFDZe = (byte) (_param1 >> 8);
    this.\u0023\u003DzcdKuX48ZXN_S = (byte) _param1;
    this.\u0023\u003DzKCqGEcs\u003D = (byte) (_param1 >> 24);
  }

  public RGBA_Bytes(
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param1)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7((double) _param1.\u0023\u003Dz4WHdt9g\u003D * (double) byte.MaxValue);
    this.\u0023\u003DzoRsAtmfOFDZe = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7((double) _param1.\u0023\u003DzoRsAtmfOFDZe * (double) byte.MaxValue);
    this.\u0023\u003DzcdKuX48ZXN_S = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7((double) _param1.\u0023\u003DzcdKuX48ZXN_S * (double) byte.MaxValue);
    this.\u0023\u003DzKCqGEcs\u003D = (byte) agg_basics.\u0023\u003DzROReRE0C5MV7((double) _param1.\u0023\u003DzKCqGEcs\u003D * (double) byte.MaxValue);
  }

  [SpecialName]
  public int \u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D() => (int) this.\u0023\u003Dz4WHdt9g\u003D;

  [SpecialName]
  public void \u0023\u003DzD9hXShMMhqxP5ayUkQ\u003D\u003D(int _param1)
  {
    this.\u0023\u003Dz4WHdt9g\u003D = (byte) _param1;
  }

  [SpecialName]
  public int \u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D()
  {
    return (int) this.\u0023\u003DzoRsAtmfOFDZe;
  }

  [SpecialName]
  public void \u0023\u003DzSqu7zYTdQL7FVU\u0024pkQ\u003D\u003D(int _param1)
  {
    this.\u0023\u003DzoRsAtmfOFDZe = (byte) _param1;
  }

  [SpecialName]
  public int \u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D() => (int) this.\u0023\u003DzcdKuX48ZXN_S;

  [SpecialName]
  public void \u0023\u003DzeSyb0eNYy90rJiPexQ\u003D\u003D(int _param1)
  {
    this.\u0023\u003DzcdKuX48ZXN_S = (byte) _param1;
  }

  [SpecialName]
  public int \u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() => (int) this.\u0023\u003DzKCqGEcs\u003D;

  [SpecialName]
  public void \u0023\u003Dzhjtfe2Gg3\u0024h2SwOzsg\u003D\u003D(int _param1)
  {
    this.\u0023\u003DzKCqGEcs\u003D = (byte) _param1;
  }

  public static bool operator ==(
    RGBA_Bytes _param0,
    RGBA_Bytes _param1)
  {
    return (int) _param0.\u0023\u003Dz4WHdt9g\u003D == (int) _param1.\u0023\u003Dz4WHdt9g\u003D && (int) _param0.\u0023\u003DzoRsAtmfOFDZe == (int) _param1.\u0023\u003DzoRsAtmfOFDZe && (int) _param0.\u0023\u003DzcdKuX48ZXN_S == (int) _param1.\u0023\u003DzcdKuX48ZXN_S && (int) _param0.\u0023\u003DzKCqGEcs\u003D == (int) _param1.\u0023\u003DzKCqGEcs\u003D;
  }

  public static bool operator !=(
    RGBA_Bytes _param0,
    RGBA_Bytes _param1)
  {
    return (int) _param0.\u0023\u003Dz4WHdt9g\u003D != (int) _param1.\u0023\u003Dz4WHdt9g\u003D || (int) _param0.\u0023\u003DzoRsAtmfOFDZe != (int) _param1.\u0023\u003DzoRsAtmfOFDZe || (int) _param0.\u0023\u003DzcdKuX48ZXN_S != (int) _param1.\u0023\u003DzcdKuX48ZXN_S || (int) _param0.\u0023\u003DzKCqGEcs\u003D != (int) _param1.\u0023\u003DzKCqGEcs\u003D;
  }

  public override bool Equals(object _param1)
  {
    return _param1.GetType() == typeof (RGBA_Bytes) && this == (RGBA_Bytes) _param1;
  }

  public override int GetHashCode()
  {
    return new \u0023\u003Dz7yw\u0024bvWnV1mnwNrOlw\u003D\u003D<byte, byte, byte, byte>(this.\u0023\u003DzcdKuX48ZXN_S, this.\u0023\u003DzoRsAtmfOFDZe, this.\u0023\u003Dz4WHdt9g\u003D, this.\u0023\u003DzKCqGEcs\u003D).GetHashCode();
  }

  public \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D \u0023\u003DzLtGfTF6UBAlmvu08d_2IGiE\u003D()
  {
    return new \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D((float) this.\u0023\u003Dz4WHdt9g\u003D / (float) byte.MaxValue, (float) this.\u0023\u003DzoRsAtmfOFDZe / (float) byte.MaxValue, (float) this.\u0023\u003DzcdKuX48ZXN_S / (float) byte.MaxValue, (float) this.\u0023\u003DzKCqGEcs\u003D / (float) byte.MaxValue);
  }

  public RGBA_Bytes \u0023\u003DzTBzq3CHoFG5sZ9taiA\u003D\u003D()
  {
    return this;
  }

  private void \u0023\u003DzS3VDevc\u003D()
  {
    this.\u0023\u003Dz4WHdt9g\u003D = this.\u0023\u003DzoRsAtmfOFDZe = this.\u0023\u003DzcdKuX48ZXN_S = this.\u0023\u003DzKCqGEcs\u003D = (byte) 0;
  }

  public RGBA_Bytes \u0023\u003Dz04GA0by_Hduv0gSskg\u003D\u003D(
    RGBA_Bytes _param1,
    double _param2)
  {
    RGBA_Bytes nwsEwePinXgsJj4Q = new RGBA_Bytes();
    int num = agg_basics.\u0023\u003DzROReRE0C5MV7(_param2 * 256.0);
    nwsEwePinXgsJj4Q.\u0023\u003DzD9hXShMMhqxP5ayUkQ\u003D\u003D((int) (byte) (this.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D() + ((_param1.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D() - this.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D()) * num >> 8)));
    nwsEwePinXgsJj4Q.\u0023\u003DzSqu7zYTdQL7FVU\u0024pkQ\u003D\u003D((int) (byte) (this.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D() + ((_param1.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D() - this.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D()) * num >> 8)));
    nwsEwePinXgsJj4Q.\u0023\u003DzeSyb0eNYy90rJiPexQ\u003D\u003D((int) (byte) (this.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D() + ((_param1.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D() - this.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D()) * num >> 8)));
    nwsEwePinXgsJj4Q.\u0023\u003Dzhjtfe2Gg3\u0024h2SwOzsg\u003D\u003D((int) (byte) (this.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() + ((_param1.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() - this.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D()) * num >> 8)));
    return nwsEwePinXgsJj4Q;
  }

  public static RGBA_Bytes operator +(
    RGBA_Bytes _param0,
    RGBA_Bytes _param1)
  {
    return new RGBA_Bytes()
    {
      \u0023\u003Dz4WHdt9g\u003D = (int) _param0.\u0023\u003Dz4WHdt9g\u003D + (int) _param1.\u0023\u003Dz4WHdt9g\u003D > (int) byte.MaxValue ? byte.MaxValue : (byte) ((int) _param0.\u0023\u003Dz4WHdt9g\u003D + (int) _param1.\u0023\u003Dz4WHdt9g\u003D),
      \u0023\u003DzoRsAtmfOFDZe = (int) _param0.\u0023\u003DzoRsAtmfOFDZe + (int) _param1.\u0023\u003DzoRsAtmfOFDZe > (int) byte.MaxValue ? byte.MaxValue : (byte) ((int) _param0.\u0023\u003DzoRsAtmfOFDZe + (int) _param1.\u0023\u003DzoRsAtmfOFDZe),
      \u0023\u003DzcdKuX48ZXN_S = (int) _param0.\u0023\u003DzcdKuX48ZXN_S + (int) _param1.\u0023\u003DzcdKuX48ZXN_S > (int) byte.MaxValue ? byte.MaxValue : (byte) ((int) _param0.\u0023\u003DzcdKuX48ZXN_S + (int) _param1.\u0023\u003DzcdKuX48ZXN_S),
      \u0023\u003DzKCqGEcs\u003D = (int) _param0.\u0023\u003DzKCqGEcs\u003D + (int) _param1.\u0023\u003DzKCqGEcs\u003D > (int) byte.MaxValue ? byte.MaxValue : (byte) ((int) _param0.\u0023\u003DzKCqGEcs\u003D + (int) _param1.\u0023\u003DzKCqGEcs\u003D)
    };
  }

  public static RGBA_Bytes operator -(
    RGBA_Bytes _param0,
    RGBA_Bytes _param1)
  {
    return new RGBA_Bytes()
    {
      \u0023\u003Dz4WHdt9g\u003D = (int) _param0.\u0023\u003Dz4WHdt9g\u003D - (int) _param1.\u0023\u003Dz4WHdt9g\u003D < 0 ? (byte) 0 : (byte) ((int) _param0.\u0023\u003Dz4WHdt9g\u003D - (int) _param1.\u0023\u003Dz4WHdt9g\u003D),
      \u0023\u003DzoRsAtmfOFDZe = (int) _param0.\u0023\u003DzoRsAtmfOFDZe - (int) _param1.\u0023\u003DzoRsAtmfOFDZe < 0 ? (byte) 0 : (byte) ((int) _param0.\u0023\u003DzoRsAtmfOFDZe - (int) _param1.\u0023\u003DzoRsAtmfOFDZe),
      \u0023\u003DzcdKuX48ZXN_S = (int) _param0.\u0023\u003DzcdKuX48ZXN_S - (int) _param1.\u0023\u003DzcdKuX48ZXN_S < 0 ? (byte) 0 : (byte) ((int) _param0.\u0023\u003DzcdKuX48ZXN_S - (int) _param1.\u0023\u003DzcdKuX48ZXN_S),
      \u0023\u003DzKCqGEcs\u003D = byte.MaxValue
    };
  }

  public static RGBA_Bytes operator *(
    RGBA_Bytes _param0,
    double _param1)
  {
    float num = (float) _param1;
    return new RGBA_Bytes(new \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D()
    {
      \u0023\u003Dz4WHdt9g\u003D = (float) _param0.\u0023\u003Dz4WHdt9g\u003D / (float) byte.MaxValue * num,
      \u0023\u003DzoRsAtmfOFDZe = (float) _param0.\u0023\u003DzoRsAtmfOFDZe / (float) byte.MaxValue * num,
      \u0023\u003DzcdKuX48ZXN_S = (float) _param0.\u0023\u003DzcdKuX48ZXN_S / (float) byte.MaxValue * num,
      \u0023\u003DzKCqGEcs\u003D = (float) _param0.\u0023\u003DzKCqGEcs\u003D / (float) byte.MaxValue * num
    });
  }

  public void \u0023\u003DzObQSsmE\u003D(
    RGBA_Bytes _param1,
    int _param2)
  {
    if (_param2 == (int) byte.MaxValue)
    {
      if (_param1.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() == (int) byte.MaxValue)
      {
        this = _param1;
      }
      else
      {
        int num1 = this.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D() + _param1.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D();
        this.\u0023\u003DzD9hXShMMhqxP5ayUkQ\u003D\u003D(num1 > (int) byte.MaxValue ? (int) byte.MaxValue : num1);
        int num2 = this.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D() + _param1.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D();
        this.\u0023\u003DzSqu7zYTdQL7FVU\u0024pkQ\u003D\u003D(num2 > (int) byte.MaxValue ? (int) byte.MaxValue : num2);
        int num3 = this.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D() + _param1.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D();
        this.\u0023\u003DzeSyb0eNYy90rJiPexQ\u003D\u003D(num3 > (int) byte.MaxValue ? (int) byte.MaxValue : num3);
        int num4 = this.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() + _param1.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D();
        this.\u0023\u003Dzhjtfe2Gg3\u0024h2SwOzsg\u003D\u003D(num4 > (int) byte.MaxValue ? (int) byte.MaxValue : num4);
      }
    }
    else
    {
      int num5 = this.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D() + (_param1.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D() * _param2 + (int) sbyte.MaxValue >> 8);
      int num6 = this.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D() + (_param1.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D() * _param2 + (int) sbyte.MaxValue >> 8);
      int num7 = this.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D() + (_param1.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D() * _param2 + (int) sbyte.MaxValue >> 8);
      int num8 = this.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() + (_param1.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() * _param2 + (int) sbyte.MaxValue >> 8);
      this.\u0023\u003DzD9hXShMMhqxP5ayUkQ\u003D\u003D(num5 > (int) byte.MaxValue ? (int) byte.MaxValue : num5);
      this.\u0023\u003DzSqu7zYTdQL7FVU\u0024pkQ\u003D\u003D(num6 > (int) byte.MaxValue ? (int) byte.MaxValue : num6);
      this.\u0023\u003DzeSyb0eNYy90rJiPexQ\u003D\u003D(num7 > (int) byte.MaxValue ? (int) byte.MaxValue : num7);
      this.\u0023\u003Dzhjtfe2Gg3\u0024h2SwOzsg\u003D\u003D(num8 > (int) byte.MaxValue ? (int) byte.MaxValue : num8);
    }
  }

  public void \u0023\u003DzcKWKgbYH1sNgQjWIPQ\u003D\u003D(
    \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D _param1)
  {
    this.\u0023\u003DzD9hXShMMhqxP5ayUkQ\u003D\u003D((int) _param1.\u0023\u003DzKiES5cU\u003D((int) (byte) this.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D()));
    this.\u0023\u003DzSqu7zYTdQL7FVU\u0024pkQ\u003D\u003D((int) _param1.\u0023\u003DzKiES5cU\u003D((int) (byte) this.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D()));
    this.\u0023\u003DzeSyb0eNYy90rJiPexQ\u003D\u003D((int) _param1.\u0023\u003DzKiES5cU\u003D((int) (byte) this.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D()));
  }

  public static IColorType \u0023\u003DznZzeZSJZAeyh()
  {
    return (IColorType) new RGBA_Bytes(0, 0, 0, 0);
  }

  public static RGBA_Bytes \u0023\u003DztVdII\u0024xdxiL2(
    int _param0)
  {
    return new RGBA_Bytes(_param0 >> 16 /*0x10*/ & (int) byte.MaxValue, _param0 >> 8 & (int) byte.MaxValue, _param0 & (int) byte.MaxValue);
  }

  public RGBA_Bytes \u0023\u003Dz100Ut9M\u003D(
    RGBA_Bytes _param1,
    double _param2)
  {
    RGBA_Bytes nwsEwePinXgsJj4Q = new RGBA_Bytes(this);
    return this * (1.0 - _param2) + _param1 * _param2;
  }
}
