// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TopicTag
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class TopicTag : BaseEntity, INameEntity
{
    public string Name { get; set; }

    public bool? IsMySubscription { get; set; }

    public BaseEntitySet<Subscription> Subscriptions { get; set; }

    public BaseEntitySet<Topic> Topics { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.IsMySubscription = storage.GetValue<bool?>("IsMySubscription", new bool?());
        this.Subscriptions = storage.GetValue<BaseEntitySet<Subscription>>("Subscriptions", (BaseEntitySet<Subscription>)null);
        this.Topics = storage.GetValue<BaseEntitySet<Topic>>("Topics", (BaseEntitySet<Topic>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<bool?>("IsMySubscription", this.IsMySubscription).Set<BaseEntitySet<Subscription>>("Subscriptions", this.Subscriptions).Set<BaseEntitySet<Topic>>("Topics", this.Topics);
    }
}
