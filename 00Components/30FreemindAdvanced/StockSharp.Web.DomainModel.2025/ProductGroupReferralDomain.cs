// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductGroupReferralDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductGroupReferralDomain : BaseEntity, IDomainEntity
    {
        public ProductGroupReferral Referral { get; set; }

        public Domain Domain { get; set; }

        public Price MonthlyPrice { get; set; }

        public Price AnnualPrice { get; set; }

        public Price LifetimePrice { get; set; }

        public Price RenewMonthlyPrice { get; set; }

        public Price RenewAnnualPrice { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Referral = ( ProductGroupReferral ) storage.GetValue<ProductGroupReferral>( "Referral", null );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.MonthlyPrice = ( Price ) storage.GetValue<Price>( "MonthlyPrice", null );
            this.AnnualPrice = ( Price ) storage.GetValue<Price>( "AnnualPrice", null );
            this.LifetimePrice = ( Price ) storage.GetValue<Price>( "LifetimePrice", null );
            this.RenewMonthlyPrice = ( Price ) storage.GetValue<Price>( "RenewMonthlyPrice", null );
            this.RenewAnnualPrice = ( Price ) storage.GetValue<Price>( "RenewAnnualPrice", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<ProductGroupReferral>( "Referral", this.Referral ).Set<Domain>( "Domain", this.Domain ).Set<Price>( "MonthlyPrice", this.MonthlyPrice ).Set<Price>( "AnnualPrice", this.AnnualPrice ).Set<Price>( "LifetimePrice", this.LifetimePrice ).Set<Price>( "RenewMonthlyPrice", this.RenewMonthlyPrice ).Set<Price>( "RenewAnnualPrice", this.RenewAnnualPrice );
        }
    }
}
