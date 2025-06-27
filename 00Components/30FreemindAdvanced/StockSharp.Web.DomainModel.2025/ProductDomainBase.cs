// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductDomainBase
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public abstract class ProductDomainBase : BaseEntity, IDomainEntity, INameEntity
    {
        public string Name { get; set; }

        public Domain Domain { get; set; }

        public ProductDomainBasePrices AllApps { get; set; } = new ProductDomainBasePrices();

        public ProductDomainBasePrices OneApps { get; set; } = new ProductDomainBasePrices();

        [Obsolete]
        public Price MonthlyPrice { get; set; }

        [Obsolete]
        public Price AnnualPrice { get; set; }

        [Obsolete]
        public Price LifetimePrice { get; set; }

        [Obsolete]
        public Price RenewMonthlyPrice { get; set; }

        [Obsolete]
        public Price RenewAnnualPrice { get; set; }

        [Obsolete]
        public Price AnnualMinPrice { get; set; }

        [Obsolete]
        public Price AnnualMaxPrice { get; set; }

        [Obsolete]
        public Price LifetimeMaxPrice { get; set; }

        [Obsolete]
        public Price LifetimeMinPrice { get; set; }

        [Obsolete]
        public Price RawMonthlyPrice { get; set; }

        [Obsolete]
        public Price RawAnnualPrice { get; set; }

        [Obsolete]
        public Price RawLifetimePrice { get; set; }

        [Obsolete]
        public Price RawRenewMonthlyPrice { get; set; }

        [Obsolete]
        public Price RawRenewAnnualPrice { get; set; }

        [Obsolete]
        public Price RawAnnualMinPrice { get; set; }

        [Obsolete]
        public Price RawAnnualMaxPrice { get; set; }

        [Obsolete]
        public Price RawLifetimeMaxPrice { get; set; }

        [Obsolete]
        public Price RawLifetimeMinPrice { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.AllApps = ( ProductDomainBasePrices ) storage.GetValue<ProductDomainBasePrices>( "AllApps", null );
            this.OneApps = ( ProductDomainBasePrices ) storage.GetValue<ProductDomainBasePrices>( "OneApps", null );
            this.MonthlyPrice = ( Price ) storage.GetValue<Price>( "MonthlyPrice", null );
            this.AnnualPrice = ( Price ) storage.GetValue<Price>( "AnnualPrice", null );
            this.LifetimePrice = ( Price ) storage.GetValue<Price>( "LifetimePrice", null );
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
            storage.Set<string>( "Name", this.Name ).Set<Domain>( "Domain", this.Domain ).Set<ProductDomainBasePrices>( "AllApps", this.AllApps ).Set<ProductDomainBasePrices>( "OneApps", this.OneApps ).Set<Price>( "MonthlyPrice", this.MonthlyPrice ).Set<Price>( "AnnualPrice", this.AnnualPrice ).Set<Price>( "LifetimePrice", this.LifetimePrice ).Set<Price>( "RenewMonthlyPrice", this.RenewMonthlyPrice ).Set<Price>( "RenewAnnualPrice", this.RenewAnnualPrice ).Set<Price>( "AnnualMinPrice", this.AnnualMinPrice ).Set<Price>( "AnnualMaxPrice", this.AnnualMaxPrice ).Set<Price>( "LifetimeMaxPrice", this.LifetimeMaxPrice ).Set<Price>( "LifetimeMinPrice", this.LifetimeMinPrice ).Set<Price>( "RawMonthlyPrice", this.RawMonthlyPrice ).Set<Price>( "RawAnnualPrice", this.RawAnnualPrice ).Set<Price>( "RawLifetimePrice", this.RawLifetimePrice ).Set<Price>( "RawRenewMonthlyPrice", this.RawRenewMonthlyPrice ).Set<Price>( "RawRenewAnnualPrice", this.RawRenewAnnualPrice ).Set<Price>( "RawAnnualMinPrice", this.RawAnnualMinPrice ).Set<Price>( "RawAnnualMaxPrice", this.RawAnnualMaxPrice ).Set<Price>( "RawLifetimeMaxPrice", this.RawLifetimeMaxPrice ).Set<Price>( "RawLifetimeMinPrice", this.RawLifetimeMinPrice );
        }
    }
}
