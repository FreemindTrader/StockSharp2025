
using Ecng.Common;
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Domain : BaseEntity
    {
        public string Code { get; set; }

        public string Culture { get; set; }

        public CurrencyTypes Currency { get; set; }

        public int YandexCounter { get; set; }

        public string GoogleCounter { get; set; }

        public int VKCounter { get; set; }

        public string Phone { get; set; }

        public string Messenger { get; set; }

        public File SiteMapDoc { get; set; }

        public File SiteMapMain { get; set; }

        public File SiteMapTopics { get; set; }

        public File SiteMapUsers { get; set; }

        public string Pluso { get; set; }

        public Topic Error403 { get; set; }

        public Topic Error404 { get; set; }

        public bool YandexChat { get; set; }

        public File Yml { get; set; }

        public Domain PriceSource { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Code          = storage.GetValue("Code", (string)null);
            Culture       = storage.GetValue("Culture", (string)null);
            Currency      = storage.GetValue("Currency", CurrencyTypes.AFA);
            YandexCounter = storage.GetValue("YandexCounter", 0);
            GoogleCounter = storage.GetValue("GoogleCounter", (string)null);
            VKCounter     = storage.GetValue("VKCounter", 0);
            Phone         = storage.GetValue("Phone", (string)null);
            Messenger     = storage.GetValue("Messenger", (string)null);
            SiteMapDoc    = storage.GetValue("SiteMapDoc", (File)null);
            SiteMapMain   = storage.GetValue("SiteMapMain", (File)null);
            SiteMapTopics = storage.GetValue("SiteMapTopics", (File)null);
            SiteMapUsers  = storage.GetValue("SiteMapUsers", (File)null);
            Pluso         = storage.GetValue("Pluso", (string)null);
            Error403      = storage.GetValue("Error403", (Topic)null);
            Error404      = storage.GetValue("Error404", (Topic)null);
            YandexChat    = storage.GetValue("YandexChat", false);
            Yml           = storage.GetValue("Yml", (File)null);
            PriceSource   = storage.GetValue("PriceSource", (Domain)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Code", Code).Set("Culture", Culture).Set("Currency", Currency).Set("YandexCounter", YandexCounter).Set("GoogleCounter", GoogleCounter).Set("VKCounter", VKCounter).Set("Phone", Phone).Set("Messenger", Messenger).Set("SiteMapDoc", SiteMapDoc).Set("SiteMapMain", SiteMapMain).Set("SiteMapTopics", SiteMapTopics).Set("SiteMapUsers", SiteMapUsers).Set("Pluso", Pluso).Set("Error403", Error403).Set("Error404", Error404).Set("YandexChat", YandexChat).Set("Yml", Yml).Set("PriceSource", PriceSource);
        }
    }
}
