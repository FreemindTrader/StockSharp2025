// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ByteOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class ByteOperator : BaseOperator<byte>
  {
    public override byte Add(byte first, byte second)
    {
      return (byte) ((uint) first + (uint) second);
    }

    public override byte Subtract(byte first, byte second)
    {
      return (byte) ((uint) first - (uint) second);
    }

    public override byte Multiply(byte first, byte second)
    {
      return (byte) ((uint) first * (uint) second);
    }

    public override byte Divide(byte first, byte second)
    {
      return (byte) ((uint) first / (uint) second);
    }

    public override int Compare(byte first, byte second)
    {
      return first.CompareTo(second);
    }
  }
}
