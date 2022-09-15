// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.DateTimeOperator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  internal class DateTimeOperator : BaseOperator<DateTime>
  {
    public override DateTime Add(DateTime first, DateTime second)
    {
      return new DateTime(first.Ticks + second.Ticks);
    }

    public override DateTime Subtract(DateTime first, DateTime second)
    {
      return new DateTime(first.Ticks - second.Ticks);
    }

    public override DateTime Multiply(DateTime first, DateTime second)
    {
      return new DateTime(first.Ticks * second.Ticks);
    }

    public override DateTime Divide(DateTime first, DateTime second)
    {
      return new DateTime(first.Ticks / second.Ticks);
    }

    public override int Compare(DateTime first, DateTime second)
    {
      return first.CompareTo(second);
    }
  }
}
