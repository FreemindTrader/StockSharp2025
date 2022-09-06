
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductBaseEmailTemplate : IPersistable
    {
        public EmailTemplate ToClient { get; set; }

        public EmailTemplate ToManager { get; set; }

        public EmailTemplate ToSite { get; set; }

        public void Load(SettingsStorage storage)
        {
            ToClient  = storage.GetValue("ToClient", (EmailTemplate)null);
            ToManager = storage.GetValue("ToManager", (EmailTemplate)null);
            ToSite    = storage.GetValue("ToSite", (EmailTemplate)null);
        }

        public void Save(SettingsStorage storage)
        {
            storage.Set("ToClient", ToClient).Set("ToManager", ToManager).Set("ToSite", ToSite);
        }
    }
}
