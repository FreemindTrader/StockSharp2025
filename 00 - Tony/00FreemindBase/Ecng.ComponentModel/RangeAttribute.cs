// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.RangeAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using Ecng.Reflection;
using System;
using System.Reflection;

namespace Ecng.ComponentModel
{
  public class RangeAttribute : BaseValidatorAttribute
  {
    public virtual object MinValue { get; set; }

    public virtual object MaxValue { get; set; }

    public override BaseValidator CreateValidator(Type validationType)
    {
      if ((object) validationType == null)
        throw new ArgumentNullException(nameof (validationType));
      if (validationType.IsByRef)
        validationType = validationType.GetElementType();
      return (BaseValidator) typeof (RangeAttribute).GetMember<MethodInfo>(nameof (CreateValidator)).Make(validationType).Invoke((object) this, (object[]) null);
    }

    private BaseValidator<T> CreateValidator<T>() where T : IComparable<T>
    {
      RangeValidator<T> rangeValidator = new RangeValidator<T>();
      if (this.MinValue != null)
        rangeValidator.Range.Min = this.MinValue.To<T>();
      if (this.MaxValue != null)
        rangeValidator.Range.Max = this.MaxValue.To<T>();
      return (BaseValidator<T>) rangeValidator;
    }
  }
}
