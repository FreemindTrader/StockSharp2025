// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.TimeSpanOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  internal class TimeSpanOperator : BaseOperator<TimeSpan>
  {
    public override TimeSpan Add(TimeSpan first, TimeSpan second)
    {
      return first + second;
    }

    public override TimeSpan Subtract(TimeSpan first, TimeSpan second)
    {
      return first - second;
    }

    public override TimeSpan Multiply(TimeSpan first, TimeSpan second)
    {
      return new TimeSpan(first.Ticks * second.Ticks);
    }

    public override TimeSpan Divide(TimeSpan first, TimeSpan second)
    {
      return new TimeSpan(first.Ticks / second.Ticks);
    }

    public override int Compare(TimeSpan first, TimeSpan second)
    {
      return first.CompareTo(second);
    }
  }
}
