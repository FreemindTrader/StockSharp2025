// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.SByteOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class SByteOperator : BaseOperator<sbyte>
  {
    public override sbyte Add(sbyte first, sbyte second)
    {
      return (sbyte) ((int) first + (int) second);
    }

    public override sbyte Subtract(sbyte first, sbyte second)
    {
      return (sbyte) ((int) first - (int) second);
    }

    public override sbyte Multiply(sbyte first, sbyte second)
    {
      return (sbyte) ((int) first * (int) second);
    }

    public override sbyte Divide(sbyte first, sbyte second)
    {
      return (sbyte) ((int) first / (int) second);
    }

    public override int Compare(sbyte first, sbyte second)
    {
      return first.CompareTo(second);
    }
  }
}
