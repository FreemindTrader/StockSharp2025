
using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductDomain : ProductDomainBase
    {
        public Product Product { get; set; }

        public Topic Topic { get; set; }

        public Topic Instruction { get; set; }

        public Topic Extra { get; set; }

        public string Name { get; set; }

        public string Homepage { get; set; }

        public string UrlAlias { get; set; }

        public string UrlRelative { get; set; }

        public Unit ReceivedAmount { get; set; }

        public ProductDomainPrices DiscountAllApps { get; set; } = new ProductDomainPrices();

        public ProductDomainPrices DiscountOneApps { get; set; } = new ProductDomainPrices();

        [Obsolete]
        public Unit DiscountMonthlyPrice { get; set; }

        [Obsolete]
        public Unit DiscountAnnualPrice { get; set; }

        [Obsolete]
        public Unit DiscountLifetimePrice { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Product               = storage.GetValue("Product", (Product)null);
            Topic                 = storage.GetValue("Topic", (Topic)null);
            Instruction           = storage.GetValue("Instruction", (Topic)null);
            Extra                 = storage.GetValue("Extra", (Topic)null);
            Name                  = storage.GetValue("Name", (string)null);
            Homepage              = storage.GetValue("Homepage", (string)null);
            UrlAlias              = storage.GetValue("UrlAlias", (string)null);
            UrlRelative           = storage.GetValue("UrlRelative", (string)null);
            ReceivedAmount        = storage.GetValue("ReceivedAmount", (Unit)null);
            DiscountAllApps       = storage.GetValue("DiscountAllApps", (ProductDomainPrices)null);
            DiscountOneApps       = storage.GetValue("DiscountOneApps", (ProductDomainPrices)null);
            DiscountMonthlyPrice  = storage.GetValue("DiscountMonthlyPrice", (Unit)null);
            DiscountAnnualPrice   = storage.GetValue("DiscountAnnualPrice", (Unit)null);
            DiscountLifetimePrice = storage.GetValue("DiscountLifetimePrice", (Unit)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Product", Product).Set("Topic", Topic).Set("Instruction", Instruction).Set("Extra", Extra).Set("Name", Name).Set("Homepage", Homepage).Set("UrlAlias", UrlAlias).Set("UrlRelative", UrlRelative).Set("ReceivedAmount", ReceivedAmount).Set("DiscountAllApps", DiscountAllApps).Set("DiscountOneApps", DiscountOneApps).Set("DiscountMonthlyPrice", DiscountMonthlyPrice).Set("DiscountAnnualPrice", DiscountAnnualPrice).Set("DiscountLifetimePrice", DiscountLifetimePrice);
        }
    }
}
