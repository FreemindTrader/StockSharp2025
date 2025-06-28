// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductGroupReferralDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductGroupReferralDomain : BaseEntity, IDomainEntity
{
    public ProductGroupReferral Referral { get; set; }

    public Domain Domain { get; set; }

    public Price MonthlyPrice { get; set; }

    public Price AnnualPrice { get; set; }

    public Price LifetimePrice { get; set; }

    public Price RenewMonthlyPrice { get; set; }

    public Price RenewAnnualPrice { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Referral = storage.GetValue<ProductGroupReferral>("Referral", (ProductGroupReferral)null);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.MonthlyPrice = storage.GetValue<Price>("MonthlyPrice", (Price)null);
        this.AnnualPrice = storage.GetValue<Price>("AnnualPrice", (Price)null);
        this.LifetimePrice = storage.GetValue<Price>("LifetimePrice", (Price)null);
        this.RenewMonthlyPrice = storage.GetValue<Price>("RenewMonthlyPrice", (Price)null);
        this.RenewAnnualPrice = storage.GetValue<Price>("RenewAnnualPrice", (Price)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<ProductGroupReferral>("Referral", this.Referral).Set<Domain>("Domain", this.Domain).Set<Price>("MonthlyPrice", this.MonthlyPrice).Set<Price>("AnnualPrice", this.AnnualPrice).Set<Price>("LifetimePrice", this.LifetimePrice).Set<Price>("RenewMonthlyPrice", this.RenewMonthlyPrice).Set<Price>("RenewAnnualPrice", this.RenewAnnualPrice);
    }
}
