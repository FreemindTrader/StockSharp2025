
using Ecng.Serialization;
using StockSharp.Messages;

namespace StockSharp.Web.DomainModel
{
    public class ProductGroupReferralDomain : BaseEntity
    {
        public ProductGroupReferral Referral { get; set; }

        public Domain Domain { get; set; }

        public Unit MonthlyPrice { get; set; }

        public Unit AnnualPrice { get; set; }

        public Unit LifetimePrice { get; set; }

        public Unit RenewMonthlyPrice { get; set; }

        public Unit RenewAnnualPrice { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Referral          = storage.GetValue("Referral", (ProductGroupReferral)null);
            Domain            = storage.GetValue("Domain", (Domain)null);
            MonthlyPrice      = storage.GetValue("MonthlyPrice", (Unit)null);
            AnnualPrice       = storage.GetValue("AnnualPrice", (Unit)null);
            LifetimePrice     = storage.GetValue("LifetimePrice", (Unit)null);
            RenewMonthlyPrice = storage.GetValue("RenewMonthlyPrice", (Unit)null);
            RenewAnnualPrice  = storage.GetValue("RenewAnnualPrice", (Unit)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Referral", Referral).Set("Domain", Domain).Set("MonthlyPrice", MonthlyPrice).Set("AnnualPrice", AnnualPrice).Set("LifetimePrice", LifetimePrice).Set("RenewMonthlyPrice", RenewMonthlyPrice).Set("RenewAnnualPrice", RenewAnnualPrice);
        }
    }
}
