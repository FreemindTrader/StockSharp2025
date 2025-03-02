
using Ecng.Serialization;
using StockSharp.Messages;

namespace StockSharp.Web.DomainModel
{
    public class ProductDomainBasePrices : ProductDomainPrices
    {
        public Unit RenewMonthlyPrice { get; set; }

        public Unit RenewAnnualPrice { get; set; }

        public Unit AnnualMinPrice { get; set; }

        public Unit AnnualMaxPrice { get; set; }

        public Unit LifetimeMaxPrice { get; set; }

        public Unit LifetimeMinPrice { get; set; }

        public Unit RawMonthlyPrice { get; set; }

        public Unit RawAnnualPrice { get; set; }

        public Unit RawLifetimePrice { get; set; }

        public Unit RawRenewMonthlyPrice { get; set; }

        public Unit RawRenewAnnualPrice { get; set; }

        public Unit RawAnnualMinPrice { get; set; }

        public Unit RawAnnualMaxPrice { get; set; }

        public Unit RawLifetimeMaxPrice { get; set; }

        public Unit RawLifetimeMinPrice { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
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
            storage.Set("RenewMonthlyPrice", RenewMonthlyPrice).Set("RenewAnnualPrice", RenewAnnualPrice).Set("AnnualMinPrice", AnnualMinPrice).Set("AnnualMaxPrice", AnnualMaxPrice).Set("LifetimeMaxPrice", LifetimeMaxPrice).Set("LifetimeMinPrice", LifetimeMinPrice).Set("RawMonthlyPrice", RawMonthlyPrice).Set("RawAnnualPrice", RawAnnualPrice).Set("RawLifetimePrice", RawLifetimePrice).Set("RawRenewMonthlyPrice", RawRenewMonthlyPrice).Set("RawRenewAnnualPrice", RawRenewAnnualPrice).Set("RawAnnualMinPrice", RawAnnualMinPrice).Set("RawAnnualMaxPrice", RawAnnualMaxPrice).Set("RawLifetimeMaxPrice", RawLifetimeMaxPrice).Set("RawLifetimeMinPrice", RawLifetimeMinPrice);
        }
    }
}
