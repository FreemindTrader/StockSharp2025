
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ShortUrl : BaseEntity
    {
        public Client Client { get; set; }

        public string Origin { get; set; }

        public string Short { get; set; }

        public DateTime Expiration { get; set; } = DateTime.MaxValue;

        public BaseEntitySet<ShortUrlVisit> Visits { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Client     = storage.GetValue("Client", (Client)null);
            Origin     = storage.GetValue("Origin", (string)null);
            Short      = storage.GetValue("Short", (string)null);
            Expiration = storage.GetValue("Expiration", new DateTime());
            Visits     = storage.GetValue("Visits", (BaseEntitySet<ShortUrlVisit>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("Origin", Origin).Set("Short", Short).Set("Expiration", Expiration).Set("Visits", Visits);
        }
    }
}
