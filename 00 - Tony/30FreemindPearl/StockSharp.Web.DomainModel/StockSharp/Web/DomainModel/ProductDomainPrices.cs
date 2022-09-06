
using Ecng.Serialization;
using StockSharp.Messages;

namespace StockSharp.Web.DomainModel
{
    public class ProductDomainPrices : IPersistable
    {
        public Unit MonthlyPrice { get; set; }

        public Unit AnnualPrice { get; set; }

        public Unit LifetimePrice { get; set; }

        public virtual void Load(SettingsStorage storage)
        {
            MonthlyPrice  = storage.GetValue("MonthlyPrice", (Unit)null);
            AnnualPrice   = storage.GetValue("AnnualPrice", (Unit)null);
            LifetimePrice = storage.GetValue("LifetimePrice", (Unit)null);
        }

        public virtual void Save(SettingsStorage storage)
        {
            storage.Set("MonthlyPrice", MonthlyPrice).Set("AnnualPrice", AnnualPrice).Set("LifetimePrice", LifetimePrice);
        }
    }
}
