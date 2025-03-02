
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Favorite : BaseEntity
    {
        public Client Client { get; set; }

        public Topic Topic { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Client = storage.GetValue("Client", (Client)null);
            Topic  = storage.GetValue("Topic", (Topic)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("Topic", Topic);
        }
    }
}
