// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductPermission
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductPermission : IPersistable
{
    public Client Client { get; set; }

    public bool IsManager { get; set; }

    public DateTime? Till { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.IsManager = storage.GetValue<bool>("IsManager", false);
        this.Till = storage.GetValue<DateTime?>("Till", new DateTime?());
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<Client>("Client", this.Client).Set<bool>("IsManager", this.IsManager).Set<DateTime?>("Till", this.Till);
    }
}
