
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Error : BaseEntity
    {
        public Client Client { get; set; }

        public Payment Payment { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string Referer { get; set; }

        public string IpAddress { get; set; }

        public int Priority { get; set; }

        public BaseEntitySet<ProductBugReport> ProductBugReports { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Client            = storage.GetValue("Client", (Client)null);
            Payment           = storage.GetValue("Payment", (Payment)null);
            Text              = storage.GetValue("Text", (string)null);
            Url               = storage.GetValue("Url", (string)null);
            Referer           = storage.GetValue("Referer", (string)null);
            IpAddress         = storage.GetValue("IpAddress", (string)null);
            Priority          = storage.GetValue("Priority", 0);
            ProductBugReports = storage.GetValue("ProductBugReports", (BaseEntitySet<ProductBugReport>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("Payment", Payment).Set("Text", Text).Set("Url", Url).Set("Referer", Referer).Set("IpAddress", IpAddress).Set("Priority", Priority).Set("ProductBugReports", ProductBugReports);
        }
    }
}
