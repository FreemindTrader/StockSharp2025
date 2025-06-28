// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Topic
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Topic :
  BaseEntity,
  IClientEntity,
  IProductEntity,
  INameEntity,
  IDescriptionEntity,
  IDomainEntity,
  IPictureEntity
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
        get => this._name;
        set
        {
            this._name = value;
            if (!StringHelper.IsEmpty(this.Title))
                return;
            this.Title = value;
        }
    }

    [Obsolete("Use Name property.")]
    public string Title { get; set; }

    public string GetName() => StringHelper.IsEmpty(this.Name, this.Title);

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
        get => StringHelper.IsEmpty(this._description, this.FirstMessage?.Text);
        set => this._description = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.SecondClient = storage.GetValue<Client>("SecondClient", (Client)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Type = storage.GetValue<TopicTypes>("Type", TopicTypes.Article);
        this.Poll = storage.GetValue<Poll>("Poll", (Poll)null);
        this.Group = storage.GetValue<TopicGroup>("Group", (TopicGroup)null);
        this.UrlRelative = storage.GetValue<string>("UrlRelative", (string)null);
        this.FirstMessage = storage.GetValue<Message>("FirstMessage", (Message)null);
        this.LastMessage = storage.GetValue<Message>("LastMessage", (Message)null);
        this.IsSupport = storage.GetValue<bool?>("IsSupport", new bool?());
        this.IsMySubscription = storage.GetValue<bool?>("IsMySubscription", new bool?());
        this.AllowComment = storage.GetValue<bool?>("AllowComment", new bool?());
        this.Product = storage.GetValue<Product>("Product", (Product)null);
        this.IsPrivate = storage.GetValue<bool?>("IsPrivate", new bool?());
        this.IsMyFavorite = storage.GetValue<bool?>("IsMyFavorite", new bool?());
        this.IsRestricted = storage.GetValue<bool?>("IsRestricted", new bool?());
        this.LastMessageTime = storage.GetValue<DateTime?>("LastMessageTime", new DateTime?());
        this.IsLocked = storage.GetValue<bool?>("IsLocked", new bool?());
        this.Picture = storage.GetValue<File>("Picture", (File)null);
        this.EmailTemplate = storage.GetValue<EmailTemplate>("EmailTemplate", (EmailTemplate)null);
        this.DynamicPage = storage.GetValue<DynamicPage>("DynamicPage", (DynamicPage)null);
        this.PrivateDescription = storage.GetValue<Client>("PrivateDescription", (Client)null);
        this.BugReport = storage.GetValue<ProductBugReport>("BugReport", (ProductBugReport)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Title = storage.GetValue<string>("Title", (string)null);
        this.Favorites = storage.GetValue<BaseEntitySet<Favorite>>("Favorites", (BaseEntitySet<Favorite>)null);
        this.Messages = storage.GetValue<BaseEntitySet<Message>>("Messages", (BaseEntitySet<Message>)null);
        this.RolesRead = storage.GetValue<BaseEntitySet<Client>>("RolesRead", (BaseEntitySet<Client>)null);
        this.RolesWrite = storage.GetValue<BaseEntitySet<Client>>("RolesWrite", (BaseEntitySet<Client>)null);
        this.Subscriptions = storage.GetValue<BaseEntitySet<Subscription>>("Subscriptions", (BaseEntitySet<Subscription>)null);
        this.Tags = storage.GetValue<BaseEntitySet<TopicTag>>("Tags", (BaseEntitySet<TopicTag>)null);
        this.Visits = storage.GetValue<BaseEntitySet<TopicVisit>>("Visits", (BaseEntitySet<TopicVisit>)null);
        this.Votes = storage.GetValue<BaseEntitySet<MessageVote>>("Votes", (BaseEntitySet<MessageVote>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Domain>("Domain", this.Domain).Set<Client>("SecondClient", this.SecondClient).Set<Client>("Client", this.Client).Set<TopicTypes>("Type", this.Type).Set<Poll>("Poll", this.Poll).Set<TopicGroup>("Group", this.Group).Set<string>("UrlRelative", this.UrlRelative).Set<Message>("FirstMessage", this.FirstMessage).Set<Message>("LastMessage", this.LastMessage).Set<bool?>("IsSupport", this.IsSupport).Set<bool?>("IsMySubscription", this.IsMySubscription).Set<bool?>("AllowComment", this.AllowComment).Set<Product>("Product", this.Product).Set<bool?>("IsPrivate", this.IsPrivate).Set<bool?>("IsMyFavorite", this.IsMyFavorite).Set<bool?>("IsRestricted", this.IsRestricted).Set<DateTime?>("LastMessageTime", this.LastMessageTime).Set<bool?>("IsLocked", this.IsLocked).Set<File>("Picture", this.Picture).Set<EmailTemplate>("EmailTemplate", this.EmailTemplate).Set<DynamicPage>("DynamicPage", this.DynamicPage).Set<Client>("PrivateDescription", this.PrivateDescription).Set<ProductBugReport>("BugReport", this.BugReport).Set<string>("Name", this.Name).Set<string>("Title", this.Title).Set<BaseEntitySet<Favorite>>("Favorites", this.Favorites).Set<BaseEntitySet<Message>>("Messages", this.Messages).Set<BaseEntitySet<Client>>("RolesRead", this.RolesRead).Set<BaseEntitySet<Client>>("RolesWrite", this.RolesWrite).Set<BaseEntitySet<Subscription>>("Subscriptions", this.Subscriptions).Set<BaseEntitySet<TopicTag>>("Tags", this.Tags).Set<BaseEntitySet<TopicVisit>>("Visits", this.Visits).Set<BaseEntitySet<MessageVote>>("Votes", this.Votes);
    }
}
