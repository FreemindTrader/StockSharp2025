// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.NotNullAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;

namespace Ecng.ComponentModel
{
  public class NotNullAttribute : BaseValidatorAttribute
  {
    public override BaseValidator CreateValidator(Type validationType)
    {
      if (validationType.IsByRef)
        validationType = validationType.GetElementType();
      return typeof (NotNullValidator<>).Make(validationType).CreateInstance<BaseValidator>();
    }
  }
}
