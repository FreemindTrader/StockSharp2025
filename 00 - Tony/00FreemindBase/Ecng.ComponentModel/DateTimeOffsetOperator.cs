// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.DateTimeOffsetOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  internal class DateTimeOffsetOperator : BaseOperator<DateTimeOffset>
  {
    public override DateTimeOffset Add(DateTimeOffset first, DateTimeOffset second)
    {
      return new DateTimeOffset(first.UtcTicks + second.UtcTicks, first.Offset);
    }

    public override DateTimeOffset Subtract(
      DateTimeOffset first,
      DateTimeOffset second)
    {
      return new DateTimeOffset(first.UtcTicks - second.UtcTicks, first.Offset);
    }

    public override DateTimeOffset Multiply(
      DateTimeOffset first,
      DateTimeOffset second)
    {
      return new DateTimeOffset(first.UtcTicks * second.UtcTicks, first.Offset);
    }

    public override DateTimeOffset Divide(DateTimeOffset first, DateTimeOffset second)
    {
      return new DateTimeOffset(first.UtcTicks / second.UtcTicks, first.Offset);
    }

    public override int Compare(DateTimeOffset first, DateTimeOffset second)
    {
      return first.CompareTo(second);
    }
  }
}
