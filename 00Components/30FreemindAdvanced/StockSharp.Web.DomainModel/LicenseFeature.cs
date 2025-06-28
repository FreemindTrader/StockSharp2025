// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.LicenseFeature
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class LicenseFeature : BaseEntity, INameEntity
{
    public string Name { get; set; }

    public BaseEntitySet<License> Licenses { get; set; }

    public BaseEntitySet<Client> Roles { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Licenses = storage.GetValue<BaseEntitySet<License>>("Licenses", (BaseEntitySet<License>)null);
        this.Roles = storage.GetValue<BaseEntitySet<Client>>("Roles", (BaseEntitySet<Client>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<BaseEntitySet<License>>("Licenses", this.Licenses).Set<BaseEntitySet<Client>>("Roles", this.Roles);
    }
}
