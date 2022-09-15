// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.IOperator`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System.Collections;
using System.Collections.Generic;

namespace Ecng.ComponentModel
{
  public interface IOperator<T> : IComparer<T>, IOperator, IComparer
  {
    T Add(T first, T second);

    T Subtract(T first, T second);

    T Multiply(T first, T second);

    T Divide(T first, T second);
  }
}
