
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductBugReport : BaseEntity
    {
        public Product Product { get; set; }

        public string Version { get; set; }

        public string SystemInfo { get; set; }

        public Client Client { get; set; }

        public Message Message { get; set; }

        public Error Error { get; set; }

        public int? Priority { get; set; }

        public int? Count { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Product    = storage.GetValue("Product", (Product)null);
            Version    = storage.GetValue("Version", (string)null);
            SystemInfo = storage.GetValue("SystemInfo", (string)null);
            Client     = storage.GetValue("Client", (Client)null);
            Message    = storage.GetValue("Message", (Message)null);
            Error      = storage.GetValue("Error", (Error)null);
            Priority   = storage.GetValue("Priority", new int?());
            Count      = storage.GetValue("Count", new int?());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Product", Product).Set("Version", Version).Set("SystemInfo", SystemInfo).Set("Client", Client).Set("Message", Message).Set("Error", Error).Set("Priority", Priority).Set("Count", Count);
        }
    }
}
