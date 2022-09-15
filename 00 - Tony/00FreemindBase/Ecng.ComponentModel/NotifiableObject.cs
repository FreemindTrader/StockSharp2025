// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.NotifiableObject
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Ecng.ComponentModel
{
  [DataContract]
  [Serializable]
  public abstract class NotifiableObject : INotifyPropertyChangedEx, INotifyPropertyChanged, INotifyPropertyChanging
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public event PropertyChangingEventHandler PropertyChanging;

    public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged.Invoke((object) this, propertyName);
    }

    protected void NotifyChanged([CallerMemberName] string propertyName = null)
    {
      this.Notify<NotifiableObject>(propertyName);
    }

    protected void NotifyChanging([CallerMemberName] string propertyName = null)
    {
      PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
      if (propertyChanging == null)
        return;
      propertyChanging.Invoke((object) this, propertyName);
    }
  }
}
