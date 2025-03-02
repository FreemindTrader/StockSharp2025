
using Ecng.Serialization;
using System.Net;

namespace StockSharp.Web.DomainModel
{
    public class DynamicPage : BaseEntity
    {
        public DynamicPage Parent { get; set; }

        public bool IsEnabled { get; set; }

        public DynamicPageMasters Master { get; set; }

        public bool IsSitemap { get; set; }

        public string SystemPath { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public BaseEntitySet<DynamicPage> Childs { get; set; }

        public BaseEntitySet<DynamicPageDomain> Domains { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Parent            = storage.GetValue("Parent", (DynamicPage)null);
            IsEnabled         = storage.GetValue("IsEnabled", false);
            Master            = storage.GetValue("Master", DynamicPageMasters.Main);
            IsSitemap         = storage.GetValue("IsSitemap", false);
            SystemPath        = storage.GetValue("SystemPath", (string)null);
            StatusCode        = storage.GetValue("StatusCode", (HttpStatusCode)0);
            StatusDescription = storage.GetValue("StatusDescription", (string)null);
            Childs            = storage.GetValue("Childs", (BaseEntitySet<DynamicPage>)null);
            Domains           = storage.GetValue("Domains", (BaseEntitySet<DynamicPageDomain>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Parent", Parent).Set("IsEnabled", IsEnabled).Set("Master", Master).Set("IsSitemap", IsSitemap).Set("SystemPath", SystemPath).Set("StatusCode", StatusCode).Set("StatusDescription", StatusDescription).Set("Childs", Childs).Set("Domains", Domains);
        }
    }
}
