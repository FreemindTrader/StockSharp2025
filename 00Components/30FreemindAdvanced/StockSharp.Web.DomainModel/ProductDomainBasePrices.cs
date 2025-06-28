// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductDomainBasePrices
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductDomainBasePrices : ProductDomainPrices
{
    public Price RenewMonthlyPrice { get; set; }

    public Price RenewAnnualPrice { get; set; }

    public Price AnnualMinPrice { get; set; }

    public Price AnnualMaxPrice { get; set; }

    public Price LifetimeMaxPrice { get; set; }

    public Price LifetimeMinPrice { get; set; }

    public Price RawMonthlyPrice { get; set; }

    public Price RawAnnualPrice { get; set; }

    public Price RawLifetimePrice { get; set; }

    public Price RawRenewMonthlyPrice { get; set; }

    public Price RawRenewAnnualPrice { get; set; }

    public Price RawAnnualMinPrice { get; set; }

    public Price RawAnnualMaxPrice { get; set; }

    public Price RawLifetimeMaxPrice { get; set; }

    public Price RawLifetimeMinPrice { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.RenewMonthlyPrice = storage.GetValue<Price>("RenewMonthlyPrice", (Price)null);
        this.RenewAnnualPrice = storage.GetValue<Price>("RenewAnnualPrice", (Price)null);
        this.AnnualMinPrice = storage.GetValue<Price>("AnnualMinPrice", (Price)null);
        this.AnnualMaxPrice = storage.GetValue<Price>("AnnualMaxPrice", (Price)null);
        this.LifetimeMaxPrice = storage.GetValue<Price>("LifetimeMaxPrice", (Price)null);
        this.LifetimeMinPrice = storage.GetValue<Price>("LifetimeMinPrice", (Price)null);
        this.RawMonthlyPrice = storage.GetValue<Price>("RawMonthlyPrice", (Price)null);
        this.RawAnnualPrice = storage.GetValue<Price>("RawAnnualPrice", (Price)null);
        this.RawLifetimePrice = storage.GetValue<Price>("RawLifetimePrice", (Price)null);
        this.RawRenewMonthlyPrice = storage.GetValue<Price>("RawRenewMonthlyPrice", (Price)null);
        this.RawRenewAnnualPrice = storage.GetValue<Price>("RawRenewAnnualPrice", (Price)null);
        this.RawAnnualMinPrice = storage.GetValue<Price>("RawAnnualMinPrice", (Price)null);
        this.RawAnnualMaxPrice = storage.GetValue<Price>("RawAnnualMaxPrice", (Price)null);
        this.RawLifetimeMaxPrice = storage.GetValue<Price>("RawLifetimeMaxPrice", (Price)null);
        this.RawLifetimeMinPrice = storage.GetValue<Price>("RawLifetimeMinPrice", (Price)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Price>("RenewMonthlyPrice", this.RenewMonthlyPrice).Set<Price>("RenewAnnualPrice", this.RenewAnnualPrice).Set<Price>("AnnualMinPrice", this.AnnualMinPrice).Set<Price>("AnnualMaxPrice", this.AnnualMaxPrice).Set<Price>("LifetimeMaxPrice", this.LifetimeMaxPrice).Set<Price>("LifetimeMinPrice", this.LifetimeMinPrice).Set<Price>("RawMonthlyPrice", this.RawMonthlyPrice).Set<Price>("RawAnnualPrice", this.RawAnnualPrice).Set<Price>("RawLifetimePrice", this.RawLifetimePrice).Set<Price>("RawRenewMonthlyPrice", this.RawRenewMonthlyPrice).Set<Price>("RawRenewAnnualPrice", this.RawRenewAnnualPrice).Set<Price>("RawAnnualMinPrice", this.RawAnnualMinPrice).Set<Price>("RawAnnualMaxPrice", this.RawAnnualMaxPrice).Set<Price>("RawLifetimeMaxPrice", this.RawLifetimeMaxPrice).Set<Price>("RawLifetimeMinPrice", this.RawLifetimeMinPrice);
    }
}
