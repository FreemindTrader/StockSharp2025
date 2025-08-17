// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Subscription
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Subscription : BaseEntity, IClientEntity, ITopicEntity
{
    public Client Client { get; set; }

    public Topic Topic { get; set; }

    public TopicTag TopicTag { get; set; }

    public TopicTypes? TopicType { get; set; }

    public Client Author { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Topic = storage.GetValue<Topic>("Topic", (Topic)null);
        this.TopicTag = storage.GetValue<TopicTag>("TopicTag", (TopicTag)null);
        this.TopicType = storage.GetValue<TopicTypes?>("TopicType", new TopicTypes?());
        this.Author = storage.GetValue<Client>("Author", (Client)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<Topic>("Topic", this.Topic).Set<TopicTag>("TopicTag", this.TopicTag).Set<TopicTypes?>("TopicType", this.TopicType).Set<Client>("Author", this.Author);
    }
}
