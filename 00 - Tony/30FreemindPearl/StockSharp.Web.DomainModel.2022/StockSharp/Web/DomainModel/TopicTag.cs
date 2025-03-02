
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class TopicTag : BaseEntity
    {
        public string Name { get; set; }

        public bool? IsMySubscription { get; set; }

        public BaseEntitySet<Subscription> Subscriptions { get; set; }

        public BaseEntitySet<Topic> Topics { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name             = storage.GetValue("Name", (string)null);
            IsMySubscription = storage.GetValue("IsMySubscription", new bool?());
            Subscriptions    = storage.GetValue("Subscriptions", (BaseEntitySet<Subscription>)null);
            Topics           = storage.GetValue("Topics", (BaseEntitySet<Topic>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("IsMySubscription", IsMySubscription).Set("Subscriptions", Subscriptions).Set("Topics", Topics);
        }
    }
}
