// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.EntityProperty
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System.Collections.Generic;

namespace Ecng.ComponentModel
{
  public class EntityProperty
  {
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }

    public EntityProperty Parent { get; set; }

    public IEnumerable<EntityProperty> Properties { get; set; }

    public string FullDisplayName
    {
      get
      {
        if (this.Parent != null)
          return this.Parent.FullDisplayName + " -> " + this.DisplayName;
        return this.DisplayName;
      }
    }

    public string ParentName
    {
      get
      {
        if (this.Parent != null)
          return this.Parent.Name;
        return string.Empty;
      }
    }

    public override string ToString()
    {
      return this.Name + " (" + this.FullDisplayName + ")";
    }
  }
}
