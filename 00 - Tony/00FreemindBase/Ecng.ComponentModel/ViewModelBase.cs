// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ViewModelBase
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Ecng.ComponentModel
{
  public abstract class ViewModelBase : Disposable, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(name));
    }

    protected void OnPropertyChanged<T>(Expression<Func<T>> selectorExpression)
    {
      this.OnPropertyChanged(ViewModelBase.PropertyName<T>(selectorExpression));
    }

    protected bool SetField<T>(ref T field, T value, Expression<Func<T>> selectorExpression)
    {
      return this.SetField<T>(ref field, value, ViewModelBase.PropertyName<T>(selectorExpression));
    }

    protected virtual bool SetField<T>(ref T field, T value, [CallerMemberName] string name = null)
    {
      if (EqualityComparer<T>.Default.Equals(field, value))
        return false;
      field = value;
      this.OnPropertyChanged(name);
      return true;
    }

    public static string PropertyName<T>(Expression<Func<T>> property)
    {
      LambdaExpression lambdaExpression = (LambdaExpression) property;
      UnaryExpression body = lambdaExpression.Body as UnaryExpression;
      return (body == null ? (MemberExpression) lambdaExpression.Body : (MemberExpression) body.Operand).Member.Name;
    }
  }
}
