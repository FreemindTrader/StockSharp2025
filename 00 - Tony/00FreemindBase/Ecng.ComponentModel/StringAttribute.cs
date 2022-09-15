// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.StringAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;

namespace Ecng.ComponentModel
{
  public class StringAttribute : LengthAttribute
  {
    public StringAttribute()
    {
    }

    public StringAttribute(int maxLength)
      : base(maxLength)
    {
    }

    public StringAttribute(int minLength, int maxLength)
      : base(minLength, maxLength)
    {
    }

    public string Regex { get; set; }

    public override BaseValidator CreateValidator(Type validationType)
    {
      StringValidator stringValidator = new StringValidator(this.Length);
      if (!this.Regex.IsEmpty())
        stringValidator.Regex = this.Regex;
      return (BaseValidator) stringValidator;
    }
  }
}
