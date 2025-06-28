// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientIpAddress
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ClientIpAddress : BaseEntity
{
    public bool IsRegistration { get; set; }

    public Client Client { get; set; }

    public ClientIpAddressEntityTypes EntityType { get; set; }

    public long EntityId { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.IsRegistration = storage.GetValue<bool>("IsRegistration", false);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.EntityType = storage.GetValue<ClientIpAddressEntityTypes>("EntityType", ClientIpAddressEntityTypes.Client);
        this.EntityId = storage.GetValue<long>("EntityId", 0L);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<bool>("IsRegistration", this.IsRegistration).Set<Client>("Client", this.Client).Set<ClientIpAddressEntityTypes>("EntityType", this.EntityType).Set<long>("EntityId", this.EntityId);
    }
}
