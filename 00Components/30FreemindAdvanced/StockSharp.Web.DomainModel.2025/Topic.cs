// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Topic
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Topic : BaseEntity, IClientEntity, IProductEntity, INameEntity, IDescriptionEntity, IDomainEntity, IPictureEntity
    {
        private string _name;
        private string _description;

        public Domain Domain { get; set; }

        public Client SecondClient { get; set; }

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

        public bool? IsRestricted { get; set; }

        public DateTime? LastMessageTime { get; set; }

        public bool? IsLocked { get; set; }

        public File Picture { get; set; }

        public EmailTemplate EmailTemplate { get; set; }

        public DynamicPage DynamicPage { get; set; }

        public Client PrivateDescription { get; set; }

        public ProductBugReport BugReport { get; set; }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                if ( !StringHelper.IsEmpty( this.Title ) )
                    return;
                this.Title = value;
            }
        }

        [Obsolete( "Use Name property." )]
        public string Title { get; set; }

        public string GetName()
        {
            return StringHelper.IsEmpty( this.Name, this.Title );
        }

        public BaseEntitySet<Favorite> Favorites { get; set; }

        public BaseEntitySet<Message> Messages { get; set; }

        public BaseEntitySet<Client> RolesRead { get; set; }

        public BaseEntitySet<Client> RolesWrite { get; set; }

        public BaseEntitySet<Subscription> Subscriptions { get; set; }

        public BaseEntitySet<TopicTag> Tags { get; set; }

        public BaseEntitySet<TopicVisit> Visits { get; set; }

        public BaseEntitySet<MessageVote> Votes { get; set; }

        string IDescriptionEntity.Description
        {
            get
            {
                return StringHelper.IsEmpty( this._description, this.FirstMessage?.Text );
            }
            set
            {
                this._description = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.SecondClient = ( Client ) storage.GetValue<Client>( "SecondClient", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Type = ( TopicTypes ) storage.GetValue<TopicTypes>( "Type", 0 );
            this.Poll = ( Poll ) storage.GetValue<Poll>( "Poll", null );
            this.Group = ( TopicGroup ) storage.GetValue<TopicGroup>( "Group", null );
            this.UrlRelative = ( string ) storage.GetValue<string>( "UrlRelative", null );
            this.FirstMessage = ( Message ) storage.GetValue<Message>( "FirstMessage", null );
            this.LastMessage = ( Message ) storage.GetValue<Message>( "LastMessage", null );
            this.IsSupport = ( bool? ) storage.GetValue<bool?>( "IsSupport", new bool?() );
            this.IsMySubscription = ( bool? ) storage.GetValue<bool?>( "IsMySubscription", new bool?() );
            this.AllowComment = ( bool? ) storage.GetValue<bool?>( "AllowComment", new bool?() );
            this.Product = ( Product ) storage.GetValue<Product>( "Product", null );
            this.IsPrivate = ( bool? ) storage.GetValue<bool?>( "IsPrivate", new bool?() );
            this.IsMyFavorite = ( bool? ) storage.GetValue<bool?>( "IsMyFavorite", new bool?() );
            this.IsRestricted = ( bool? ) storage.GetValue<bool?>( "IsRestricted", new bool?() );
            this.LastMessageTime = ( DateTime? ) storage.GetValue<DateTime?>( "LastMessageTime", new DateTime?() );
            this.IsLocked = ( bool? ) storage.GetValue<bool?>( "IsLocked", new bool?() );
            this.Picture = ( File ) storage.GetValue<File>( "Picture", null );
            this.EmailTemplate = ( EmailTemplate ) storage.GetValue<EmailTemplate>( "EmailTemplate", null );
            this.DynamicPage = ( DynamicPage ) storage.GetValue<DynamicPage>( "DynamicPage", null );
            this.PrivateDescription = ( Client ) storage.GetValue<Client>( "PrivateDescription", null );
            this.BugReport = ( ProductBugReport ) storage.GetValue<ProductBugReport>( "BugReport", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Title = ( string ) storage.GetValue<string>( "Title", null );
            this.Favorites = ( BaseEntitySet<Favorite> ) storage.GetValue<BaseEntitySet<Favorite>>( "Favorites", null );
            this.Messages = ( BaseEntitySet<Message> ) storage.GetValue<BaseEntitySet<Message>>( "Messages", null );
            this.RolesRead = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "RolesRead", null );
            this.RolesWrite = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "RolesWrite", null );
            this.Subscriptions = ( BaseEntitySet<Subscription> ) storage.GetValue<BaseEntitySet<Subscription>>( "Subscriptions", null );
            this.Tags = ( BaseEntitySet<TopicTag> ) storage.GetValue<BaseEntitySet<TopicTag>>( "Tags", null );
            this.Visits = ( BaseEntitySet<TopicVisit> ) storage.GetValue<BaseEntitySet<TopicVisit>>( "Visits", null );
            this.Votes = ( BaseEntitySet<MessageVote> ) storage.GetValue<BaseEntitySet<MessageVote>>( "Votes", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Domain>( "Domain", this.Domain ).Set<Client>( "SecondClient", this.SecondClient ).Set<Client>( "Client", this.Client ).Set<TopicTypes>( "Type", this.Type ).Set<Poll>( "Poll", this.Poll ).Set<TopicGroup>( "Group", this.Group ).Set<string>( "UrlRelative", this.UrlRelative ).Set<Message>( "FirstMessage", this.FirstMessage ).Set<Message>( "LastMessage", this.LastMessage ).Set<bool?>( "IsSupport", this.IsSupport ).Set<bool?>( "IsMySubscription", this.IsMySubscription ).Set<bool?>( "AllowComment", this.AllowComment ).Set<Product>( "Product", this.Product ).Set<bool?>( "IsPrivate", this.IsPrivate ).Set<bool?>( "IsMyFavorite", this.IsMyFavorite ).Set<bool?>( "IsRestricted", this.IsRestricted ).Set<DateTime?>( "LastMessageTime", this.LastMessageTime ).Set<bool?>( "IsLocked", this.IsLocked ).Set<File>( "Picture", this.Picture ).Set<EmailTemplate>( "EmailTemplate", this.EmailTemplate ).Set<DynamicPage>( "DynamicPage", this.DynamicPage ).Set<Client>( "PrivateDescription", this.PrivateDescription ).Set<ProductBugReport>( "BugReport", this.BugReport ).Set<string>( "Name", this.Name ).Set<string>( "Title", this.Title ).Set<BaseEntitySet<Favorite>>( "Favorites", this.Favorites ).Set<BaseEntitySet<Message>>( "Messages", this.Messages ).Set<BaseEntitySet<Client>>( "RolesRead", this.RolesRead ).Set<BaseEntitySet<Client>>( "RolesWrite", this.RolesWrite ).Set<BaseEntitySet<Subscription>>( "Subscriptions", this.Subscriptions ).Set<BaseEntitySet<TopicTag>>( "Tags", this.Tags ).Set<BaseEntitySet<TopicVisit>>( "Visits", this.Visits ).Set<BaseEntitySet<MessageVote>>( "Votes", this.Votes );
        }
    }
}
