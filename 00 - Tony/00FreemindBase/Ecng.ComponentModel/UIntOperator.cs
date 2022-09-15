// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.UIntOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class UIntOperator : BaseOperator<uint>
  {
    public override uint Add(uint first, uint second)
    {
      return first + second;
    }

    public override uint Subtract(uint first, uint second)
    {
      return first - second;
    }

    public override uint Multiply(uint first, uint second)
    {
      return first * second;
    }

    public override uint Divide(uint first, uint second)
    {
      return first / second;
    }

    public override int Compare(uint first, uint second)
    {
      return first.CompareTo(second);
    }
  }
}
