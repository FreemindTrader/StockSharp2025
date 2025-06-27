// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductDomainBasePrices
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.RenewMonthlyPrice = ( Price ) storage.GetValue<Price>( "RenewMonthlyPrice", null );
            this.RenewAnnualPrice = ( Price ) storage.GetValue<Price>( "RenewAnnualPrice", null );
            this.AnnualMinPrice = ( Price ) storage.GetValue<Price>( "AnnualMinPrice", null );
            this.AnnualMaxPrice = ( Price ) storage.GetValue<Price>( "AnnualMaxPrice", null );
            this.LifetimeMaxPrice = ( Price ) storage.GetValue<Price>( "LifetimeMaxPrice", null );
            this.LifetimeMinPrice = ( Price ) storage.GetValue<Price>( "LifetimeMinPrice", null );
            this.RawMonthlyPrice = ( Price ) storage.GetValue<Price>( "RawMonthlyPrice", null );
            this.RawAnnualPrice = ( Price ) storage.GetValue<Price>( "RawAnnualPrice", null );
            this.RawLifetimePrice = ( Price ) storage.GetValue<Price>( "RawLifetimePrice", null );
            this.RawRenewMonthlyPrice = ( Price ) storage.GetValue<Price>( "RawRenewMonthlyPrice", null );
            this.RawRenewAnnualPrice = ( Price ) storage.GetValue<Price>( "RawRenewAnnualPrice", null );
            this.RawAnnualMinPrice = ( Price ) storage.GetValue<Price>( "RawAnnualMinPrice", null );
            this.RawAnnualMaxPrice = ( Price ) storage.GetValue<Price>( "RawAnnualMaxPrice", null );
            this.RawLifetimeMaxPrice = ( Price ) storage.GetValue<Price>( "RawLifetimeMaxPrice", null );
            this.RawLifetimeMinPrice = ( Price ) storage.GetValue<Price>( "RawLifetimeMinPrice", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Price>( "RenewMonthlyPrice", this.RenewMonthlyPrice ).Set<Price>( "RenewAnnualPrice", this.RenewAnnualPrice ).Set<Price>( "AnnualMinPrice", this.AnnualMinPrice ).Set<Price>( "AnnualMaxPrice", this.AnnualMaxPrice ).Set<Price>( "LifetimeMaxPrice", this.LifetimeMaxPrice ).Set<Price>( "LifetimeMinPrice", this.LifetimeMinPrice ).Set<Price>( "RawMonthlyPrice", this.RawMonthlyPrice ).Set<Price>( "RawAnnualPrice", this.RawAnnualPrice ).Set<Price>( "RawLifetimePrice", this.RawLifetimePrice ).Set<Price>( "RawRenewMonthlyPrice", this.RawRenewMonthlyPrice ).Set<Price>( "RawRenewAnnualPrice", this.RawRenewAnnualPrice ).Set<Price>( "RawAnnualMinPrice", this.RawAnnualMinPrice ).Set<Price>( "RawAnnualMaxPrice", this.RawAnnualMaxPrice ).Set<Price>( "RawLifetimeMaxPrice", this.RawLifetimeMaxPrice ).Set<Price>( "RawLifetimeMinPrice", this.RawLifetimeMinPrice );
        }
    }
}
