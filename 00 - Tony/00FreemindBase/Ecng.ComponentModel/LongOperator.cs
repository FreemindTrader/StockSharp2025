// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.LongOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class LongOperator : BaseOperator<long>
  {
    public override long Add(long first, long second)
    {
      return first + second;
    }

    public override long Subtract(long first, long second)
    {
      return first - second;
    }

    public override long Multiply(long first, long second)
    {
      return first * second;
    }

    public override long Divide(long first, long second)
    {
      return first / second;
    }

    public override int Compare(long first, long second)
    {
      return first.CompareTo(second);
    }
  }
}
