// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.ComponentModel;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductDomain : ProductDomainBase, ITopicEntity, IProductEntity
{
    public Product Product { get; set; }

    public Topic Topic { get; set; }

    public Topic Instruction { get; set; }

    public Topic Extra { get; set; }

    public string Homepage { get; set; }

    public string UrlAlias { get; set; }

    public string UrlRelative { get; set; }

    public Price ReceivedAmount { get; set; }

    public ProductDomainPrices DiscountAllApps { get; set; } = new ProductDomainPrices();

    public ProductDomainPrices DiscountOneApps { get; set; } = new ProductDomainPrices();

    [Obsolete]
    public Price DiscountMonthlyPrice { get; set; }

    [Obsolete]
    public Price DiscountAnnualPrice { get; set; }

    [Obsolete]
    public Price DiscountLifetimePrice { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Product = storage.GetValue<Product>("Product", (Product)null);
        this.Topic = storage.GetValue<Topic>("Topic", (Topic)null);
        this.Instruction = storage.GetValue<Topic>("Instruction", (Topic)null);
        this.Extra = storage.GetValue<Topic>("Extra", (Topic)null);
        this.Homepage = storage.GetValue<string>("Homepage", (string)null);
        this.UrlAlias = storage.GetValue<string>("UrlAlias", (string)null);
        this.UrlRelative = storage.GetValue<string>("UrlRelative", (string)null);
        this.ReceivedAmount = storage.GetValue<Price>("ReceivedAmount", (Price)null);
        this.DiscountAllApps = storage.GetValue<ProductDomainPrices>("DiscountAllApps", (ProductDomainPrices)null);
        this.DiscountOneApps = storage.GetValue<ProductDomainPrices>("DiscountOneApps", (ProductDomainPrices)null);
        this.DiscountMonthlyPrice = storage.GetValue<Price>("DiscountMonthlyPrice", (Price)null);
        this.DiscountAnnualPrice = storage.GetValue<Price>("DiscountAnnualPrice", (Price)null);
        this.DiscountLifetimePrice = storage.GetValue<Price>("DiscountLifetimePrice", (Price)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Product>("Product", this.Product).Set<Topic>("Topic", this.Topic).Set<Topic>("Instruction", this.Instruction).Set<Topic>("Extra", this.Extra).Set<string>("Homepage", this.Homepage).Set<string>("UrlAlias", this.UrlAlias).Set<string>("UrlRelative", this.UrlRelative).Set<Price>("ReceivedAmount", this.ReceivedAmount).Set<ProductDomainPrices>("DiscountAllApps", this.DiscountAllApps).Set<ProductDomainPrices>("DiscountOneApps", this.DiscountOneApps).Set<Price>("DiscountMonthlyPrice", this.DiscountMonthlyPrice).Set<Price>("DiscountAnnualPrice", this.DiscountAnnualPrice).Set<Price>("DiscountLifetimePrice", this.DiscountLifetimePrice);
    }
}
