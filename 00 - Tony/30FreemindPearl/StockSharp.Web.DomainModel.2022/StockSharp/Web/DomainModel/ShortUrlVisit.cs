using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ShortUrlVisit : BaseEntity
    {
        public ShortUrl ShortUrl { get; set; }

        public Client Client { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            ShortUrl = storage.GetValue("ShortUrl", (ShortUrl)null);
            Client = storage.GetValue("Client", (Client)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("ShortUrl", ShortUrl).Set("Client", Client);
        }
    }
}
