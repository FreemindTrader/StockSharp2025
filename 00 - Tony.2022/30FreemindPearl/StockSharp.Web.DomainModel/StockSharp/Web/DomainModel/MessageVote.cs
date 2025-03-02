
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class MessageVote : BaseEntity
    {
        public Message Message { get; set; }

        public int Value { get; set; }

        public Client Client { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Message = storage.GetValue("Message", (Message)null);
            Value   = storage.GetValue("Value", 0);
            Client  = storage.GetValue("Client", (Client)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Message", Message).Set("Value", Value).Set("Client", Client);
        }
    }
}
