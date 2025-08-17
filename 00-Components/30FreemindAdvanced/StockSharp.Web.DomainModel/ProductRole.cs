// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductRole
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductRole : BaseEntity, IProductEntity
{
    public Product Product { get; set; }

    public Client Role { get; set; }

    public TimeSpan Till { get; set; }

    public ProductPriceTypes? PriceType { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Product = storage.GetValue<Product>("Product", (Product)null);
        this.Role = storage.GetValue<Client>("Role", (Client)null);
        this.Till = storage.GetValue<TimeSpan>("Till", new TimeSpan());
        this.PriceType = storage.GetValue<ProductPriceTypes?>("PriceType", new ProductPriceTypes?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Product>("Product", this.Product).Set<Client>("Role", this.Role).Set<TimeSpan>("Till", this.Till).Set<ProductPriceTypes?>("PriceType", this.PriceType);
    }
}
