// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.UShortOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class UShortOperator : BaseOperator<ushort>
  {
    public override ushort Add(ushort first, ushort second)
    {
      return (ushort) ((uint) first + (uint) second);
    }

    public override ushort Subtract(ushort first, ushort second)
    {
      return (ushort) ((uint) first - (uint) second);
    }

    public override ushort Multiply(ushort first, ushort second)
    {
      return (ushort) ((uint) first * (uint) second);
    }

    public override ushort Divide(ushort first, ushort second)
    {
      return (ushort) ((uint) first / (uint) second);
    }

    public override int Compare(ushort first, ushort second)
    {
      return first.CompareTo(second);
    }
  }
}
