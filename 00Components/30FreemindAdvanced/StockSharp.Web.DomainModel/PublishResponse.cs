// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PublishResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class PublishResponse : BaseEntityIdResponse
{
    public PublishResponse()
      : base(SubscriptionTypes.Publish)
    {
    }

    public Guid PublishId { get; set; }

    public PublishActions Action { get; set; }

    public string Value { get; set; }

    public PublishDetails Details { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.PublishId = storage.GetValue<Guid>("PublishId", new Guid());
        this.Action = storage.GetValue<PublishActions>("Action", PublishActions.GetSolutions);
        this.Value = storage.GetValue<string>("Value", (string)null);
        this.Details = storage.GetValue<PublishDetails>("Details", (PublishDetails)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Guid>("PublishId", this.PublishId).Set<PublishActions>("Action", this.Action).Set<string>("Value", this.Value).Set<PublishDetails>("Details", this.Details);
    }
}
