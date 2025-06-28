// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Session
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Session : BaseEntity, IClientEntity, IProductEntity
{
    public Product Product { get; set; }

    public Client Client { get; set; }

    public bool Logout { get; set; }

    public string Version { get; set; }

    public int? Count { get; set; }

    public int? AverageUpTimeMinutes { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Product = storage.GetValue<Product>("Product", (Product)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Logout = storage.GetValue<bool>("Logout", false);
        this.Version = storage.GetValue<string>("Version", (string)null);
        this.Count = storage.GetValue<int?>("Count", new int?());
        this.AverageUpTimeMinutes = storage.GetValue<int?>("AverageUpTimeMinutes", new int?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Product>("Product", this.Product).Set<Client>("Client", this.Client).Set<bool>("Logout", this.Logout).Set<string>("Version", this.Version).Set<int?>("Count", this.Count).Set<int?>("AverageUpTimeMinutes", this.AverageUpTimeMinutes);
    }
}
