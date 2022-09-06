
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductGroup : ProductBase
    {
        public string Description { get; set; }

        public BaseEntitySet<ProductGroupDomain> Domains { get; set; }

        public BaseEntitySet<ProductGroup> Child { get; set; }

        public BaseEntitySet<ProductGroup> Parent { get; set; }

        public BaseEntitySet<Product> Products { get; set; }

        public BaseEntitySet<ProductGroupReferral> Referral { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Description = storage.GetValue("Description", (string)null);
            Domains     = storage.GetValue("Domains", (BaseEntitySet<ProductGroupDomain>)null);
            Child       = storage.GetValue("Child", (BaseEntitySet<ProductGroup>)null);
            Parent      = storage.GetValue("Parent", (BaseEntitySet<ProductGroup>)null);
            Products    = storage.GetValue("Products", (BaseEntitySet<Product>)null);
            Referral    = storage.GetValue("Referral", (BaseEntitySet<ProductGroupReferral>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Description", Description).Set("Domains", Domains).Set("Child", Child).Set("Parent", Parent).Set("Products", Products).Set("Referral", Referral);
        }
    }
}
