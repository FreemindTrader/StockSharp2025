// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ShortOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class ShortOperator : BaseOperator<short>
  {
    public override short Add(short first, short second)
    {
      return (short) ((int) first + (int) second);
    }

    public override short Subtract(short first, short second)
    {
      return (short) ((int) first - (int) second);
    }

    public override short Multiply(short first, short second)
    {
      return (short) ((int) first * (int) second);
    }

    public override short Divide(short first, short second)
    {
      return (short) ((int) first / (int) second);
    }

    public override int Compare(short first, short second)
    {
      return first.CompareTo(second);
    }
  }
}
