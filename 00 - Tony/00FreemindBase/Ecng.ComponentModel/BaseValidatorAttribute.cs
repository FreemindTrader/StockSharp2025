// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.BaseValidatorAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
  public abstract class BaseValidatorAttribute : Attribute
  {
    public abstract BaseValidator CreateValidator(Type validationType);
  }
}
