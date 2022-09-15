// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.BaseOperator`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System.Collections;
using System.Collections.Generic;

namespace Ecng.ComponentModel
{
  public abstract class BaseOperator<T> : IOperator<T>, IComparer<T>, IOperator, IComparer
  {
    public abstract int Compare(T x, T y);

    int IComparer.Compare(object x, object y)
    {
      return this.Compare((T) x, (T) y);
    }

    object IOperator.Add(object first, object second)
    {
      return (object) this.Add((T) first, (T) second);
    }

    object IOperator.Subtract(object first, object second)
    {
      return (object) this.Subtract((T) first, (T) second);
    }

    object IOperator.Multiply(object first, object second)
    {
      return (object) this.Multiply((T) first, (T) second);
    }

    object IOperator.Divide(object first, object second)
    {
      return (object) this.Divide((T) first, (T) second);
    }

    public abstract T Add(T first, T second);

    public abstract T Subtract(T first, T second);

    public abstract T Multiply(T first, T second);

    public abstract T Divide(T first, T second);
  }
}
