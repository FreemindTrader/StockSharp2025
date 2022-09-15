// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.DecimalOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  internal class DecimalOperator : BaseOperator<Decimal>
  {
    public override Decimal Add(Decimal first, Decimal second)
    {
      return first + second;
    }

    public override Decimal Subtract(Decimal first, Decimal second)
    {
      return first - second;
    }

    public override Decimal Multiply(Decimal first, Decimal second)
    {
      return first * second;
    }

    public override Decimal Divide(Decimal first, Decimal second)
    {
      return first / second;
    }

    public override int Compare(Decimal first, Decimal second)
    {
      return first.CompareTo(second);
    }
  }
}
