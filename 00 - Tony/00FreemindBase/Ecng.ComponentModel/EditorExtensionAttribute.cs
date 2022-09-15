// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.EditorExtensionAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Property)]
  public class EditorExtensionAttribute : Attribute
  {
    public bool AutoComplete { get; set; }

    public bool Sorted { get; set; }

    public bool IncludeObsolete { get; set; }

    public bool ShowSelectedItemsCount { get; set; }
  }
}
