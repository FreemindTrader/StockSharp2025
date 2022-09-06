
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DynamicMenuDomain : BaseEntity
    {
        public DynamicMenu Menu { get; set; }

        public Domain Domain { get; set; }

        public string Text { get; set; }

        public string UrlRelative { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Menu        = storage.GetValue("Menu", (DynamicMenu)null);
            Domain      = storage.GetValue("Domain", (Domain)null);
            Text        = storage.GetValue("Text", (string)null);
            UrlRelative = storage.GetValue("UrlRelative", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Menu", Menu).Set("Domain", Domain).Set("Text", Text).Set("UrlRelative", UrlRelative);
        }
    }
}
