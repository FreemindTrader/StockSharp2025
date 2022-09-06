
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Session : BaseEntity
    {
        public Product Product { get; set; }

        public Client Client { get; set; }

        public bool Logout { get; set; }

        public string Version { get; set; }

        public string IpAddress { get; set; }

        public int? Count { get; set; }

        public int? AverageUpTimeMinutes { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Product   = storage.GetValue("Product", (Product)null);
            Client    = storage.GetValue("Client", (Client)null);
            Logout    = storage.GetValue("Logout", false);
            Version   = storage.GetValue("Version", (string)null);
            IpAddress = storage.GetValue("IpAddress", (string)null);
            Count     = storage.GetValue("Count", new int?());
            AverageUpTimeMinutes = storage.GetValue("AverageUpTimeMinutes", new int?());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Product", Product).Set("Client", Client).Set("Logout", Logout).Set("Version", Version).Set("IpAddress", IpAddress).Set("Count", Count).Set("AverageUpTimeMinutes", AverageUpTimeMinutes);
        }
    }
}
