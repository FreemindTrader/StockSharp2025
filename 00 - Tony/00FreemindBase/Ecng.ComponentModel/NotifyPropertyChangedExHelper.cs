// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.NotifyPropertyChangedExHelper
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;
using System.Runtime.CompilerServices;

namespace Ecng.ComponentModel
{
  public static class NotifyPropertyChangedExHelper
  {
    public static Func<object, string, bool> Filter { get; set; }

    public static void Notify<T>(this T entity, [CallerMemberName] string propertyName = null) where T : INotifyPropertyChangedEx
    {
      if (NotifyPropertyChangedExHelper.Filter != null && !NotifyPropertyChangedExHelper.Filter((object) entity, propertyName))
        return;
      entity.NotifyPropertyChanged(propertyName);
    }
  }
}
