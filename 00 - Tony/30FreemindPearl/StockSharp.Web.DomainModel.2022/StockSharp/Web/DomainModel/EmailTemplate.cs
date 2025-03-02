
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class EmailTemplate : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public bool IsEnabled { get; set; }

        public BaseEntitySet<EmailTemplateDomain> Domains { get; set; }

        public BaseEntitySet<ProductGroup> ProductGroups { get; set; }

        public BaseEntitySet<Product> Products { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name          = storage.GetValue("Name", (string)null);
            Address       = storage.GetValue("Address", (string)null);
            IsEnabled     = storage.GetValue("IsEnabled", false);
            Domains       = storage.GetValue("Domains", (BaseEntitySet<EmailTemplateDomain>)null);
            ProductGroups = storage.GetValue("ProductGroups", (BaseEntitySet<ProductGroup>)null);
            Products      = storage.GetValue("Products", (BaseEntitySet<Product>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Address", Address).Set("IsEnabled", IsEnabled).Set("Domains", Domains).Set("ProductGroups", ProductGroups).Set("Products", Products);
        }
    }
}
