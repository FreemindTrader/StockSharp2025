// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.LengthValidator`2
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.Collections.Generic;

namespace Ecng.ComponentModel
{
  public class LengthValidator<TCollectiom, TItem> : BaseValidator<TCollectiom> where TCollectiom : ICollection<TItem>
  {
    public LengthValidator()
      : this(new Range<int>(0, int.MaxValue))
    {
    }

    public LengthValidator(Range<int> length)
    {
      this.Length = length;
    }

    public Range<int> Length { get; }

    public override void Validate(TCollectiom value)
    {
      if (value.IsNull<TCollectiom>())
        throw new ArgumentNullException(nameof (value));
      if (!this.Length.Contains(value.Count))
        throw new ArgumentOutOfRangeException(nameof (value));
    }
  }
}
