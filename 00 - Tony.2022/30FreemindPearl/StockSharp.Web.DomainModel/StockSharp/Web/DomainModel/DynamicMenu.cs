
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DynamicMenu : BaseEntity
    {
        public string Url { get; set; }

        public bool IsBlank { get; set; }

        public DynamicMenuLocations Location { get; set; }

        public int SortOrder { get; set; }

        public string Color { get; set; }

        public DynamicPage Page { get; set; }

        public Product Product { get; set; }

        public BaseEntitySet<DynamicMenuDomain> Domains { get; set; }

        public BaseEntitySet<Client> Roles { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Url       = storage.GetValue("Url", (string)null);
            IsBlank   = storage.GetValue("IsBlank", false);
            Location  = storage.GetValue("Location", DynamicMenuLocations.Main);
            SortOrder = storage.GetValue("SortOrder", 0);
            Color     = storage.GetValue("Color", (string)null);
            Page      = storage.GetValue("Page", (DynamicPage)null);
            Product   = storage.GetValue("Product", (Product)null);
            Domains   = storage.GetValue("Domains", (BaseEntitySet<DynamicMenuDomain>)null);
            Roles     = storage.GetValue("Roles", (BaseEntitySet<Client>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Url", Url).Set("IsBlank", IsBlank).Set("Location", Location).Set("SortOrder", SortOrder).Set("Color", Color).Set("Page", Page).Set("Product", Product).Set("Domains", Domains).Set("Roles", Roles);
        }
    }
}
