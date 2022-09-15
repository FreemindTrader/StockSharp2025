// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.IconAttribute
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;

namespace Ecng.ComponentModel
{
  public class IconAttribute : Attribute
  {
    public string Icon { get; }

    public bool IsFullPath { get; }

    public IconAttribute(string icon, bool isFullPath = false)
    {
      if (icon.IsEmpty())
        throw new ArgumentNullException(nameof (icon));
      this.Icon = icon;
      this.IsFullPath = isFullPath;
    }
  }
}
