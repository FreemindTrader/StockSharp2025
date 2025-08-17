// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TopicGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class TopicGroup : BaseEntity, INameEntity, IDescriptionEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public BaseEntitySet<Client> RolesRead { get; set; }

    public BaseEntitySet<Client> RolesWrite { get; set; }

    public BaseEntitySet<Topic> Topics { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.RolesRead = storage.GetValue<BaseEntitySet<Client>>("RolesRead", (BaseEntitySet<Client>)null);
        this.RolesWrite = storage.GetValue<BaseEntitySet<Client>>("RolesWrite", (BaseEntitySet<Client>)null);
        this.Topics = storage.GetValue<BaseEntitySet<Topic>>("Topics", (BaseEntitySet<Topic>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<string>("Description", this.Description).Set<BaseEntitySet<Client>>("RolesRead", this.RolesRead).Set<BaseEntitySet<Client>>("RolesWrite", this.RolesWrite).Set<BaseEntitySet<Topic>>("Topics", this.Topics);
    }
}
