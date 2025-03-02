
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class FileShare : BaseEntity
    {
        public Client Client { get; set; }

        public File File { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; } = DateTime.MaxValue;

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Client     = storage.GetValue("Client", (Client)null);
            File       = storage.GetValue("File", (File)null);
            Token      = storage.GetValue("Token", (string)null);
            Expiration = storage.GetValue("Expiration", new DateTime());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("File", File).Set("Token", Token).Set("Expiration", Expiration);
        }
    }
}
