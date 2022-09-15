// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.GreaterThanZeroAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ecng.ComponentModel
{
  public class GreaterThanZeroAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      try
      {
        Decimal? nullable = value.To<Decimal?>();
        Decimal num = new Decimal();
        return nullable.GetValueOrDefault() > num & nullable.HasValue;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
