// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicMenuGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DynamicMenuGroup : BaseEntity, INameEntity
{
    public string Name { get; set; }

    public string Style { get; set; }

    public BaseEntitySet<DynamicMenu> Menus { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Style = storage.GetValue<string>("Style", (string)null);
        this.Menus = storage.GetValue<BaseEntitySet<DynamicMenu>>("Menus", (BaseEntitySet<DynamicMenu>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<string>("Style", this.Style).Set<BaseEntitySet<DynamicMenu>>("Menus", this.Menus);
    }
}
