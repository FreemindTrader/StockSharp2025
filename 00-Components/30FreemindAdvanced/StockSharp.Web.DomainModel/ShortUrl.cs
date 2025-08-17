// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ShortUrl
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ShortUrl : BaseEntity, IClientEntity, IExpiryEntity
{
    public Client Client { get; set; }

    public string Origin { get; set; }

    public string Short { get; set; }

    public DateTime ExpiryDate { get; set; } = DateTime.MaxValue;

    public BaseEntitySet<ShortUrlVisit> Visits { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Origin = storage.GetValue<string>("Origin", (string)null);
        this.Short = storage.GetValue<string>("Short", (string)null);
        this.ExpiryDate = storage.GetValue<DateTime>("ExpiryDate", new DateTime());
        this.Visits = storage.GetValue<BaseEntitySet<ShortUrlVisit>>("Visits", (BaseEntitySet<ShortUrlVisit>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<string>("Origin", this.Origin).Set<string>("Short", this.Short).Set<DateTime>("ExpiryDate", this.ExpiryDate).Set<BaseEntitySet<ShortUrlVisit>>("Visits", this.Visits);
    }
}
