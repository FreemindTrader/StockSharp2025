// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientRole
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ClientRole : BaseEntity
{
    public Client Role { get; set; }

    public Product OneApp { get; set; }

    public DateTime? Till { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Role = storage.GetValue<Client>("Role", (Client)null);
        this.OneApp = storage.GetValue<Product>("OneApp", (Product)null);
        this.Till = storage.GetValue<DateTime?>("Till", new DateTime?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Role", this.Role).Set<Product>("OneApp", this.OneApp).Set<DateTime?>("Till", this.Till);
    }
}
