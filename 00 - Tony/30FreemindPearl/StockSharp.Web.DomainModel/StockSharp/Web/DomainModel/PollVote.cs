
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PollVote : BaseEntity
    {
        public PollChoice Choice { get; set; }

        public Client Client { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Choice = storage.GetValue("Choice", (PollChoice)null);
            Client = storage.GetValue("Client", (Client)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Choice", Choice).Set("Client", Client);
        }
    }
}
