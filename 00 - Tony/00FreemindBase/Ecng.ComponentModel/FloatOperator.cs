// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.FloatOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

namespace Ecng.ComponentModel
{
  internal class FloatOperator : BaseOperator<float>
  {
    public override float Add(float first, float second)
    {
      return first + second;
    }

    public override float Subtract(float first, float second)
    {
      return first - second;
    }

    public override float Multiply(float first, float second)
    {
      return first * second;
    }

    public override float Divide(float first, float second)
    {
      return first / second;
    }

    public override int Compare(float first, float second)
    {
      return first.CompareTo(second);
    }
  }
}
