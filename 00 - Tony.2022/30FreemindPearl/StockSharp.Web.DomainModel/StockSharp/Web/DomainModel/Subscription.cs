
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Subscription : BaseEntity
    {
        public Client Client { get; set; }

        public Topic Topic { get; set; }

        public TopicTag TopicTag { get; set; }

        public TopicTypes? TopicType { get; set; }

        public bool Active { get; set; }

        public Client Author { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Client    = storage.GetValue("Client", (Client)null);
            Topic     = storage.GetValue("Topic", (Topic)null);
            TopicTag  = storage.GetValue("TopicTag", (TopicTag)null);
            TopicType = storage.GetValue("TopicType", new TopicTypes?());
            Active    = storage.GetValue("Active", false);
            Author    = storage.GetValue("Author", (Client)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("Topic", Topic).Set("TopicTag", TopicTag).Set("TopicType", TopicType).Set("Active", Active).Set("Author", Author);
        }
    }
}
