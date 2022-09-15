// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.DoubleOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class DoubleOperator : BaseOperator<double>
  {
    public override double Add(double first, double second)
    {
      return first + second;
    }

    public override double Subtract(double first, double second)
    {
      return first - second;
    }

    public override double Multiply(double first, double second)
    {
      return first * second;
    }

    public override double Divide(double first, double second)
    {
      return first / second;
    }

    public override int Compare(double first, double second)
    {
      return first.CompareTo(second);
    }
  }
}
