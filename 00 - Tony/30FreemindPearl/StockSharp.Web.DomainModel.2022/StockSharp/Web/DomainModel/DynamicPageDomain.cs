
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DynamicPageDomain : BaseEntity
    {
        public DynamicPage Page { get; set; }

        public Domain Domain { get; set; }

        public Topic Topic { get; set; }

        public string UrlPart { get; set; }

        public string UrlRelative { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Page        = storage.GetValue("Page", (DynamicPage)null);
            Domain      = storage.GetValue("Domain", (Domain)null);
            Topic       = storage.GetValue("Topic", (Topic)null);
            UrlPart     = storage.GetValue("UrlPart", (string)null);
            UrlRelative = storage.GetValue("UrlRelative", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Page", Page).Set("Domain", Domain).Set("Topic", Topic).Set("UrlPart", UrlPart).Set("UrlRelative", UrlRelative);
        }
    }
}
