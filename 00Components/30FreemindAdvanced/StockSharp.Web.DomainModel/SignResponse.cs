// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SignResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class SignResponse : BaseResponse
{
    public SignResponse()
      : base(SubscriptionTypes.Sign)
    {
    }

    public Guid SignId { get; set; }

    public long ClientId { get; set; }

    public long FileId { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.SignId = storage.GetValue<Guid>("SignId", new Guid());
        this.ClientId = storage.GetValue<long>("ClientId", 0L);
        this.FileId = storage.GetValue<long>("FileId", 0L);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Guid>("SignId", this.SignId).Set<long>("ClientId", this.ClientId).Set<long>("FileId", this.FileId);
    }
}
