// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.LengthAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using Ecng.Reflection;
using System;

namespace Ecng.ComponentModel
{
  public class LengthAttribute : BaseValidatorAttribute
  {
    public LengthAttribute()
      : this(int.MaxValue)
    {
    }

    public LengthAttribute(int maxLength)
      : this(0, maxLength)
    {
    }

    public LengthAttribute(int minLength, int maxLength)
    {
      this.Length = new Range<int>(minLength, maxLength);
    }

    public Range<int> Length { get; }

    public override BaseValidator CreateValidator(Type validationType)
    {
      return typeof (LengthValidator<,>).Make(validationType, validationType.GetItemType()).CreateInstance<BaseValidator>((object) this.Length);
    }
  }
}
