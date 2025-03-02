using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductRole : BaseEntity
    {
        public Product Product { get; set; }

        public Client Role { get; set; }

        public TimeSpan Till { get; set; }

        public ProductPriceTypes? PriceType { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Product   = storage.GetValue("Product", (Product)null);
            Role      = storage.GetValue("Role", (Client)null);
            Till      = storage.GetValue("Till", new TimeSpan());
            PriceType = storage.GetValue("PriceType", new ProductPriceTypes?());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Product", Product).Set("Role", Role).Set("Till", Till).Set("PriceType", PriceType);
        }
    }
}
