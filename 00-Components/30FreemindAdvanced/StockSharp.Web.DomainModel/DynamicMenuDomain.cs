// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicMenuDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DynamicMenuDomain : BaseEntity, IDomainEntity, INameEntity, IDescriptionEntity
{
    public DynamicMenu Menu { get; set; }

    public Domain Domain { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string UrlRelative { get; set; }

    public string UrlAbsolute { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Menu = storage.GetValue<DynamicMenu>("Menu", (DynamicMenu)null);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.UrlRelative = storage.GetValue<string>("UrlRelative", (string)null);
        this.UrlAbsolute = storage.GetValue<string>("UrlAbsolute", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<DynamicMenu>("Menu", this.Menu).Set<Domain>("Domain", this.Domain).Set<string>("Name", this.Name).Set<string>("Description", this.Description).Set<string>("UrlRelative", this.UrlRelative).Set<string>("UrlAbsolute", this.UrlAbsolute);
    }
}
