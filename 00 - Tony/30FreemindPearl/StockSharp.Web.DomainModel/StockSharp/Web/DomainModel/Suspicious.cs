
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Suspicious : BaseEntity
    {
        public Client Client { get; set; }

        public Message Message { get; set; }

        public string Reason { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Client = storage.GetValue("Client", (Client)null);
            Message = storage.GetValue("Message", (Message)null);
            Reason = storage.GetValue("Reason", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("Message", Message).Set("Reason", Reason);
        }
    }
}
