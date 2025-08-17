// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientSocial
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ClientSocial : BaseEntity, IClientEntity, INameEntity, ITelegramChannel
{
    public Client Client { get; set; }

    public Social Social { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public Domain DomainOnly { get; set; }

    public string Url { get; set; }

    public long? MessageCount { get; set; }

    public bool? IsApproved { get; set; }

    public string AI { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Social = storage.GetValue<Social>("Social", (Social)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Code = storage.GetValue<string>("Code", (string)null);
        this.DomainOnly = storage.GetValue<Domain>("DomainOnly", (Domain)null);
        this.Url = storage.GetValue<string>("Url", (string)null);
        this.MessageCount = storage.GetValue<long?>("MessageCount", new long?());
        this.IsApproved = storage.GetValue<bool?>("IsApproved", new bool?());
        this.AI = storage.GetValue<string>("AI", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<Social>("Social", this.Social).Set<string>("Name", this.Name).Set<string>("Code", this.Code).Set<Domain>("DomainOnly", this.DomainOnly).Set<string>("Url", this.Url).Set<long?>("MessageCount", this.MessageCount).Set<bool?>("IsApproved", this.IsApproved).Set<string>("AI", this.AI);
    }
}
