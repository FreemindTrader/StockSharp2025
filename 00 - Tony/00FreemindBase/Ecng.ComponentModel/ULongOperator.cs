// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ULongOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class ULongOperator : BaseOperator<ulong>
  {
    public override ulong Add(ulong first, ulong second)
    {
      return first + second;
    }

    public override ulong Subtract(ulong first, ulong second)
    {
      return first - second;
    }

    public override ulong Multiply(ulong first, ulong second)
    {
      return first * second;
    }

    public override ulong Divide(ulong first, ulong second)
    {
      return first / second;
    }

    public override int Compare(ulong first, ulong second)
    {
      return first.CompareTo(second);
    }
  }
}
