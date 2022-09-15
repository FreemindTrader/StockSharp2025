// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ItemsSourceItem`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  public class ItemsSourceItem<T> : IItemsSourceItem<T>, IItemsSourceItem
  {
    object IItemsSourceItem.Value
    {
      get
      {
        return (object) this.Value;
      }
    }

    public T Value { get; }

    public string DisplayName { get; }

    public string Description { get; }

    public Uri Icon { get; }

    public bool IsObsolete { get; }

    public ItemsSourceItem(
      T value,
      string displayName,
      string description,
      Uri iconUri,
      bool isObsolete)
    {
      this.Value = value;
      this.DisplayName = displayName;
      this.Description = description;
      this.Icon = iconUri;
      this.IsObsolete = isObsolete;
    }

    public override string ToString()
    {
      return this.DisplayName;
    }
  }
}
