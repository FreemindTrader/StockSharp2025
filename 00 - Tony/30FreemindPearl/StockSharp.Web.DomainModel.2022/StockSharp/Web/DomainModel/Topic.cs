
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Topic : BaseEntity
    {
        public Domain Domain { get; set; }

        public TopicFlags Flags { get; set; }

        public Client SecondClient { get; set; }

        public string Title { get; set; }

        public Client Client { get; set; }

        public TopicTypes Type { get; set; }

        public Poll Poll { get; set; }

        public TopicGroup Group { get; set; }

        public string UrlRelative { get; set; }

        public Message FirstMessage { get; set; }

        public Message LastMessage { get; set; }

        public bool? IsSupport { get; set; }

        public bool? IsMySubscription { get; set; }

        public bool? AllowComment { get; set; }

        public Product Product { get; set; }

        public bool? IsPrivate { get; set; }

        public bool? IsMyFavorite { get; set; }

        public BaseEntitySet<Favorite> Favorites { get; set; }

        public BaseEntitySet<Message> Messages { get; set; }

        public BaseEntitySet<Client> RolesRead { get; set; }

        public BaseEntitySet<Client> RolesWrite { get; set; }

        public BaseEntitySet<Subscription> Subscriptions { get; set; }

        public BaseEntitySet<TopicTag> Tags { get; set; }

        public BaseEntitySet<TopicVisit> Visits { get; set; }

        public BaseEntitySet<MessageVote> Votes { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Domain = storage.GetValue("Domain", (Domain)null);
            Flags = storage.GetValue("Flags", TopicFlags.None);
            SecondClient = storage.GetValue("SecondClient", (Client)null);
            Title = storage.GetValue("Title", (string)null);
            Client = storage.GetValue("Client", (Client)null);
            Type = storage.GetValue("Type", TopicTypes.Article);
            Poll = storage.GetValue("Poll", (Poll)null);
            Group = storage.GetValue("Group", (TopicGroup)null);
            UrlRelative = storage.GetValue("UrlRelative", (string)null);
            FirstMessage = storage.GetValue("FirstMessage", (Message)null);
            LastMessage = storage.GetValue("LastMessage", (Message)null);
            IsSupport = storage.GetValue("IsSupport", new bool?());
            IsMySubscription = storage.GetValue("IsMySubscription", new bool?());
            AllowComment = storage.GetValue("AllowComment", new bool?());
            Product = storage.GetValue("Product", (Product)null);
            IsPrivate = storage.GetValue("IsPrivate", new bool?());
            IsMyFavorite = storage.GetValue("IsMyFavorite", new bool?());
            Favorites = storage.GetValue("Favorites", (BaseEntitySet<Favorite>)null);
            Messages = storage.GetValue("Messages", (BaseEntitySet<Message>)null);
            RolesRead = storage.GetValue("RolesRead", (BaseEntitySet<Client>)null);
            RolesWrite = storage.GetValue("RolesWrite", (BaseEntitySet<Client>)null);
            Subscriptions = storage.GetValue("Subscriptions", (BaseEntitySet<Subscription>)null);
            Tags = storage.GetValue("Tags", (BaseEntitySet<TopicTag>)null);
            Visits = storage.GetValue("Visits", (BaseEntitySet<TopicVisit>)null);
            Votes = storage.GetValue("Votes", (BaseEntitySet<MessageVote>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Domain", Domain).Set("Flags", Flags).Set("SecondClient", SecondClient).Set("Title", Title).Set("Client", Client).Set("Type", Type).Set("Poll", Poll).Set("Group", Group).Set("UrlRelative", UrlRelative).Set("FirstMessage", FirstMessage).Set("LastMessage", LastMessage).Set("IsSupport", IsSupport).Set("IsMySubscription", IsMySubscription).Set("AllowComment", AllowComment).Set("Product", Product).Set("IsPrivate", IsPrivate).Set("IsMyFavorite", IsMyFavorite).Set("Favorites", Favorites).Set("Messages", Messages).Set("RolesRead", RolesRead).Set("RolesWrite", RolesWrite).Set("Subscriptions", Subscriptions).Set("Tags", Tags).Set("Visits", Visits).Set("Votes", Votes);
        }
    }
}
