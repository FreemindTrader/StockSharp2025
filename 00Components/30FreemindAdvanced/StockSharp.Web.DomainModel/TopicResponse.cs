// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TopicResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class TopicResponse : BaseEntityIdResponse
{
    public TopicResponse()
      : base(SubscriptionTypes.Topic)
    {
    }

    public long? MessageId { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.MessageId = storage.GetValue<long?>("MessageId", new long?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<long?>("MessageId", this.MessageId);
    }
}
