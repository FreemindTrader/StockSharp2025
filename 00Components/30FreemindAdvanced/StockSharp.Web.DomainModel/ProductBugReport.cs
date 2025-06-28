// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductBugReport
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductBugReport : BaseEntity, IClientEntity, IProductEntity, IMessageEntity
{
    public Product Product { get; set; }

    public string Lang { get; set; }

    public string Framework { get; set; }

    public string Version { get; set; }

    public string SystemInfo { get; set; }

    public Client Client { get; set; }

    public Message Message { get; set; }

    public Error Error { get; set; }

    public int? Priority { get; set; }

    public int? Count { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Product = storage.GetValue<Product>("Product", (Product)null);
        this.Lang = storage.GetValue<string>("Lang", (string)null);
        this.Framework = storage.GetValue<string>("Framework", (string)null);
        this.Version = storage.GetValue<string>("Version", (string)null);
        this.SystemInfo = storage.GetValue<string>("SystemInfo", (string)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Message = storage.GetValue<Message>("Message", (Message)null);
        this.Error = storage.GetValue<Error>("Error", (Error)null);
        this.Priority = storage.GetValue<int?>("Priority", new int?());
        this.Count = storage.GetValue<int?>("Count", new int?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Product>("Product", this.Product).Set<string>("Lang", this.Lang).Set<string>("Framework", this.Framework).Set<string>("Version", this.Version).Set<string>("SystemInfo", this.SystemInfo).Set<Client>("Client", this.Client).Set<Message>("Message", this.Message).Set<Error>("Error", this.Error).Set<int?>("Priority", this.Priority).Set<int?>("Count", this.Count);
    }
}
