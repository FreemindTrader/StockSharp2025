// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.IItemsSource`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System.Collections.Generic;

namespace Ecng.ComponentModel
{
  public interface IItemsSource<TValue> : IItemsSource
  {
    IEnumerable<IItemsSourceItem<TValue>> Values { get; }

    IItemsSourceItem<TValue> CreateNewItem(TValue value);
  }
}
