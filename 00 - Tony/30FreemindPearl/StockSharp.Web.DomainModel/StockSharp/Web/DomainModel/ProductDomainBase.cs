
using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public abstract class ProductDomainBase : BaseEntity
    {
        public Domain Domain { get; set; }

        public ProductDomainBasePrices AllApps { get; set; } = new ProductDomainBasePrices();

        public ProductDomainBasePrices OneApps { get; set; } = new ProductDomainBasePrices();

        [Obsolete]
        public Unit MonthlyPrice { get; set; }

        [Obsolete]
        public Unit AnnualPrice { get; set; }

        [Obsolete]
        public Unit LifetimePrice { get; set; }

        [Obsolete]
        public Unit RenewMonthlyPrice { get; set; }

        [Obsolete]
        public Unit RenewAnnualPrice { get; set; }

        [Obsolete]
        public Unit AnnualMinPrice { get; set; }

        [Obsolete]
        public Unit AnnualMaxPrice { get; set; }

        [Obsolete]
        public Unit LifetimeMaxPrice { get; set; }

        [Obsolete]
        public Unit LifetimeMinPrice { get; set; }

        [Obsolete]
        public Unit RawMonthlyPrice { get; set; }

        [Obsolete]
        public Unit RawAnnualPrice { get; set; }

        [Obsolete]
        public Unit RawLifetimePrice { get; set; }

        [Obsolete]
        public Unit RawRenewMonthlyPrice { get; set; }

        [Obsolete]
        public Unit RawRenewAnnualPrice { get; set; }

        [Obsolete]
        public Unit RawAnnualMinPrice { get; set; }

        [Obsolete]
        public Unit RawAnnualMaxPrice { get; set; }

        [Obsolete]
        public Unit RawLifetimeMaxPrice { get; set; }

        [Obsolete]
        public Unit RawLifetimeMinPrice { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Domain               = storage.GetValue("Domain", (Domain)null);
            AllApps              = storage.GetValue("AllApps", (ProductDomainBasePrices)null);
            OneApps              = storage.GetValue("OneApps", (ProductDomainBasePrices)null);
            MonthlyPrice         = storage.GetValue("MonthlyPrice", (Unit)null);
            AnnualPrice          = storage.GetValue("AnnualPrice", (Unit)null);
            LifetimePrice        = storage.GetValue("LifetimePrice", (Unit)null);
            RenewMonthlyPrice    = storage.GetValue("RenewMonthlyPrice", (Unit)null);
            RenewAnnualPrice     = storage.GetValue("RenewAnnualPrice", (Unit)null);
            AnnualMinPrice       = storage.GetValue("AnnualMinPrice", (Unit)null);
            AnnualMaxPrice       = storage.GetValue("AnnualMaxPrice", (Unit)null);
            LifetimeMaxPrice     = storage.GetValue("LifetimeMaxPrice", (Unit)null);
            LifetimeMinPrice     = storage.GetValue("LifetimeMinPrice", (Unit)null);
            RawMonthlyPrice      = storage.GetValue("RawMonthlyPrice", (Unit)null);
            RawAnnualPrice       = storage.GetValue("RawAnnualPrice", (Unit)null);
            RawLifetimePrice     = storage.GetValue("RawLifetimePrice", (Unit)null);
            RawRenewMonthlyPrice = storage.GetValue("RawRenewMonthlyPrice", (Unit)null);
            RawRenewAnnualPrice  = storage.GetValue("RawRenewAnnualPrice", (Unit)null);
            RawAnnualMinPrice    = storage.GetValue("RawAnnualMinPrice", (Unit)null);
            RawAnnualMaxPrice    = storage.GetValue("RawAnnualMaxPrice", (Unit)null);
            RawLifetimeMaxPrice  = storage.GetValue("RawLifetimeMaxPrice", (Unit)null);
            RawLifetimeMinPrice  = storage.GetValue("RawLifetimeMinPrice", (Unit)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Domain", Domain).Set("AllApps", AllApps).Set("OneApps", OneApps).Set("MonthlyPrice", MonthlyPrice).Set("AnnualPrice", AnnualPrice).Set("LifetimePrice", LifetimePrice).Set("RenewMonthlyPrice", RenewMonthlyPrice).Set("RenewAnnualPrice", RenewAnnualPrice).Set("AnnualMinPrice", AnnualMinPrice).Set("AnnualMaxPrice", AnnualMaxPrice).Set("LifetimeMaxPrice", LifetimeMaxPrice).Set("LifetimeMinPrice", LifetimeMinPrice).Set("RawMonthlyPrice", RawMonthlyPrice).Set("RawAnnualPrice", RawAnnualPrice).Set("RawLifetimePrice", RawLifetimePrice).Set("RawRenewMonthlyPrice", RawRenewMonthlyPrice).Set("RawRenewAnnualPrice", RawRenewAnnualPrice).Set("RawAnnualMinPrice", RawAnnualMinPrice).Set("RawAnnualMaxPrice", RawAnnualMaxPrice).Set("RawLifetimeMaxPrice", RawLifetimeMaxPrice).Set("RawLifetimeMinPrice", RawLifetimeMinPrice);
        }
    }
}
