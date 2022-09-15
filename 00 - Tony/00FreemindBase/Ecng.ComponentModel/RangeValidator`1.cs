// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.RangeValidator`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  public class RangeValidator<T> : BaseValidator<T> where T : IComparable<T>
  {
    public RangeValidator()
      : this(new Ecng.ComponentModel.Range<T>())
    {
    }

    public RangeValidator(T minValue, T maxValue)
      : this(new Ecng.ComponentModel.Range<T>(minValue, maxValue))
    {
    }

    public RangeValidator(Ecng.ComponentModel.Range<T> range)
    {
      this.Range = range;
    }

    public Ecng.ComponentModel.Range<T> Range { get; }

    public override void Validate(T value)
    {
      if (!this.Range.Contains(value))
        throw new ArgumentOutOfRangeException(nameof (value));
    }
  }
}
