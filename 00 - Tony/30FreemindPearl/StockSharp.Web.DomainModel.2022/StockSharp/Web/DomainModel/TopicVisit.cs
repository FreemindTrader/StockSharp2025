
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class TopicVisit : BaseEntity
    {
        public Topic Topic { get; set; }

        public Client Client { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Topic = storage.GetValue("Topic", (Topic)null);
            Client = storage.GetValue("Client", (Client)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Topic", Topic).Set("Client", Client);
        }
    }
}
