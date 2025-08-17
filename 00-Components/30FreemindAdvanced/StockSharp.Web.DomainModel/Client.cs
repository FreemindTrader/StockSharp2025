// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Client
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Client : BaseEntity, INameEntity, IDescriptionEntity, IDomainEntity, IPictureEntity
{
    private string _description;

    public string Email { get; set; }

    public string Phone { get; set; }

    public string City { get; set; }

    public string Skype { get; set; }

    public StockSharp.Web.DomainModel.EmailPreferences? EmailPreferences { get; set; }

    public Domain Domain { get; set; }

    public bool? IsAgreementAccepted { get; set; }

    public bool? IsAllowStatistics { get; set; }

    public string RealName { get; set; }

    public bool? IsPhonePublic { get; set; }

    public string VKontakte { get; set; }

    public string DisplayName { get; set; }

    public int? EmailBounceCount { get; set; }

    public string DeleteReason { get; set; }

    public string UnsubscribeReason { get; set; }

    public File Picture { get; set; }

    public DateTime? Birthday { get; set; }

    public string Facebook { get; set; }

    public int Gender { get; set; }

    public string Homepage { get; set; }

    public bool? IsApproved { get; set; }

    public bool? IsLockedOut { get; set; }

    public DateTime? LastActivityDate { get; set; }

    public DateTime? LastLockOutDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LastPasswordChangedDate { get; set; }

    public Message PublicDescription { get; set; }

    public Message PrivateDescription { get; set; }

    public bool? Gravatar { get; set; }

    public string GravatarToken { get; set; }

    public string Telegram { get; set; }

    public long? TelegramId { get; set; }

    public Client Referral { get; set; }

    public bool? SelfDeleted { get; set; }

    public long? FileUploadMaxBytes { get; set; }

    public bool? IsTrialVerified { get; set; }

    [Obsolete("Use AccessTokens property.")]
    public string AuthToken { get; set; }

    public bool? CanPublish { get; set; }

    public long? UploadLimit { get; set; }

    public string UrlRelative { get; set; }

    public bool? BlockedTo { get; set; }

    public bool? BlockedFrom { get; set; }

    public bool? IsMySubscription { get; set; }

    public DateTime? SupportTill { get; set; }

    public Topic PrivateTopic { get; set; }

    public string SubscriptionToken { get; set; }

    public Social Social { get; set; }

    public string SocialId { get; set; }

    public string SocialLink { get; set; }

    public string NugetPrefix { get; set; }

    public int? SocialMaxLength { get; set; }

    public bool? HasTelegramRobots { get; set; }

    public bool? HasTelegramAlerts { get; set; }

    public bool? HasNewPrivateTopics { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanImpersonate { get; set; }

    public bool? CanManageContent { get; set; }

    public bool? CanTariffFree { get; set; }

    public bool? IsTelegramApproved { get; set; }

    public StrategyBacktestOptions Backtest { get; set; }

    public StrategyOptimizationOptions Optimization { get; set; }

    public StrategyLiveOptions Live { get; set; }

    public TopicTypes[] SubscriptionsByTopicType { get; set; }

    public BaseEntitySet<Client> Awards { get; set; }

    public BaseEntitySet<ClientBalance> Balances { get; set; }

    public BaseEntitySet<AccountRequisites> BankAccounts { get; set; }

    public BaseEntitySet<Client> Blockeds { get; set; }

    public BaseEntitySet<Product> Brokers { get; set; }

    public BaseEntitySet<FileDownload> Downloads { get; set; }

    public BaseEntitySet<Error> Errors { get; set; }

    public BaseEntitySet<Favorite> Favorites { get; set; }

    public BaseEntitySet<LicenseFeature> Features { get; set; }

    public BaseEntitySet<File> Files { get; set; }

    public BaseEntitySet<FileShare> FileShares { get; set; }

    public BaseEntitySet<MessageVote> GivenMessageVotes { get; set; }

    public BaseEntitySet<Client> Inner { get; set; }

    public BaseEntitySet<ClientIpAddress> IpActivity { get; set; }

    public BaseEntitySet<License> Licenses { get; set; }

    public BaseEntitySet<ProductGroup> ManagedGroups { get; set; }

    public BaseEntitySet<Product> ManagedProducts { get; set; }

    public BaseEntitySet<Message> Messages { get; set; }

    public BaseEntitySet<Payment> Payments { get; set; }

    public BaseEntitySet<Poll> Polls { get; set; }

    public BaseEntitySet<PollVote> PollVotes { get; set; }

    public BaseEntitySet<Topic> PrivateTopics { get; set; }

    public BaseEntitySet<ProductBugReport> ProductBugReports { get; set; }

    public BaseEntitySet<ProductFeedback> ProductFeedbacks { get; set; }

    public BaseEntitySet<ProductGroupReferral> ProductGroupReferrals { get; set; }

    public BaseEntitySet<ProductOrder> ProductOrders { get; set; }

    public BaseEntitySet<ProductOrder> ProductOrdersVideo { get; set; }

    public BaseEntitySet<ProductOrder> ProductOrdersDevelopment { get; set; }

    public BaseEntitySet<ProductOrder> ProductOrdersAccount { get; set; }

    public BaseEntitySet<ProductOrder> ProductOrdersTrial { get; set; }

    public BaseEntitySet<ProductOrder> ProductOrdersReferral { get; set; }

    public BaseEntitySet<Product> Products { get; set; }

    public BaseEntitySet<ProfileVisit> ProfileVisits { get; set; }

    public BaseEntitySet<MessageVote> ReceivedMessageVotes { get; set; }

    public BaseEntitySet<Client> ReferralClients { get; set; }

    public BaseEntitySet<ClientRole> Roles2 { get; set; }

    public BaseEntitySet<Session> Sessions { get; set; }

    public BaseEntitySet<ShortUrlVisit> ShortUrlVisits { get; set; }

    public BaseEntitySet<Subscription> Subscriptions { get; set; }

    public BaseEntitySet<StockSharp.Web.DomainModel.Suspicious> Suspicious { get; set; }

    public BaseEntitySet<Topic> Topics { get; set; }

    public BaseEntitySet<Strategy> Strategies { get; set; }

    public BaseEntitySet<StrategyAccount> StrategyAccounts { get; set; }

    public BaseEntitySet<AccessToken> AccessTokens { get; set; }

    public BaseEntitySet<ClientSocial> Socials { get; set; }

    public BaseEntitySet<DataPermission> DataPermissions { get; set; }

    [Obsolete("Use Roles2")]
    public BaseEntitySet<Client> Roles { get; set; }

    string INameEntity.Name
    {
        get => this.DisplayName;
        set => this.DisplayName = value;
    }

    string IDescriptionEntity.Description
    {
        get
        {
            string description = this._description;
            if (!StringHelper.IsEmpty(description))
                return description;
            string email = this.Email;
            Message publicDescription = this.PublicDescription;
            if ((publicDescription != null ? (!StringHelper.IsEmpty(publicDescription.Text) ? 1 : 0) : 0) != 0)
            {
                if (!StringHelper.IsEmpty(email))
                    email += Environment.NewLine;
                email += this.PublicDescription?.Text;
            }
            Message privateDescription = this.PrivateDescription;
            if ((privateDescription != null ? (!StringHelper.IsEmpty(privateDescription.Text) ? 1 : 0) : 0) != 0)
            {
                if (!StringHelper.IsEmpty(email))
                    email += Environment.NewLine;
                email += this.PrivateDescription?.Text;
            }
            return email;
        }
        set => this._description = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Email = storage.GetValue<string>("Email", (string)null);
        this.Phone = storage.GetValue<string>("Phone", (string)null);
        this.City = storage.GetValue<string>("City", (string)null);
        this.Skype = storage.GetValue<string>("Skype", (string)null);
        this.EmailPreferences = storage.GetValue<StockSharp.Web.DomainModel.EmailPreferences?>("EmailPreferences", new StockSharp.Web.DomainModel.EmailPreferences?());
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.IsAgreementAccepted = storage.GetValue<bool?>("IsAgreementAccepted", new bool?());
        this.IsAllowStatistics = storage.GetValue<bool?>("IsAllowStatistics", new bool?());
        this.RealName = storage.GetValue<string>("RealName", (string)null);
        this.IsPhonePublic = storage.GetValue<bool?>("IsPhonePublic", new bool?());
        this.VKontakte = storage.GetValue<string>("VKontakte", (string)null);
        this.DisplayName = storage.GetValue<string>("DisplayName", (string)null);
        this.EmailBounceCount = storage.GetValue<int?>("EmailBounceCount", new int?());
        this.DeleteReason = storage.GetValue<string>("DeleteReason", (string)null);
        this.UnsubscribeReason = storage.GetValue<string>("UnsubscribeReason", (string)null);
        this.Picture = storage.GetValue<File>("Picture", (File)null);
        this.Birthday = storage.GetValue<DateTime?>("Birthday", new DateTime?());
        this.Facebook = storage.GetValue<string>("Facebook", (string)null);
        this.Gender = storage.GetValue<int>("Gender", 0);
        this.Homepage = storage.GetValue<string>("Homepage", (string)null);
        this.IsApproved = storage.GetValue<bool?>("IsApproved", new bool?());
        this.IsLockedOut = storage.GetValue<bool?>("IsLockedOut", new bool?());
        this.LastActivityDate = storage.GetValue<DateTime?>("LastActivityDate", new DateTime?());
        this.LastLockOutDate = storage.GetValue<DateTime?>("LastLockOutDate", new DateTime?());
        this.LastLoginDate = storage.GetValue<DateTime?>("LastLoginDate", new DateTime?());
        this.LastPasswordChangedDate = storage.GetValue<DateTime?>("LastPasswordChangedDate", new DateTime?());
        this.PublicDescription = storage.GetValue<Message>("PublicDescription", (Message)null);
        this.PrivateDescription = storage.GetValue<Message>("PrivateDescription", (Message)null);
        this.Gravatar = storage.GetValue<bool?>("Gravatar", new bool?());
        this.GravatarToken = storage.GetValue<string>("GravatarToken", (string)null);
        this.Telegram = storage.GetValue<string>("Telegram", (string)null);
        this.TelegramId = storage.GetValue<long?>("TelegramId", new long?());
        this.Referral = storage.GetValue<Client>("Referral", (Client)null);
        this.SelfDeleted = storage.GetValue<bool?>("SelfDeleted", new bool?());
        this.FileUploadMaxBytes = storage.GetValue<long?>("FileUploadMaxBytes", new long?());
        this.IsTrialVerified = storage.GetValue<bool?>("IsTrialVerified", new bool?());
        this.AuthToken = storage.GetValue<string>("AuthToken", (string)null);
        this.CanPublish = storage.GetValue<bool?>("CanPublish", new bool?());
        this.UploadLimit = storage.GetValue<long?>("UploadLimit", new long?());
        this.UrlRelative = storage.GetValue<string>("UrlRelative", (string)null);
        this.BlockedTo = storage.GetValue<bool?>("BlockedTo", new bool?());
        this.BlockedFrom = storage.GetValue<bool?>("BlockedFrom", new bool?());
        this.IsMySubscription = storage.GetValue<bool?>("IsMySubscription", new bool?());
        this.SupportTill = storage.GetValue<DateTime?>("SupportTill", new DateTime?());
        this.PrivateTopic = storage.GetValue<Topic>("PrivateTopic", (Topic)null);
        this.SubscriptionToken = storage.GetValue<string>("SubscriptionToken", (string)null);
        this.Social = storage.GetValue<Social>("Social", (Social)null);
        this.SocialId = storage.GetValue<string>("SocialId", (string)null);
        this.SocialLink = storage.GetValue<string>("SocialLink", (string)null);
        this.NugetPrefix = storage.GetValue<string>("NugetPrefix", (string)null);
        this.SocialMaxLength = storage.GetValue<int?>("SocialMaxLength", new int?());
        this.HasTelegramRobots = storage.GetValue<bool?>("HasTelegramRobots", new bool?());
        this.HasTelegramAlerts = storage.GetValue<bool?>("HasTelegramAlerts", new bool?());
        this.HasNewPrivateTopics = storage.GetValue<bool?>("HasNewPrivateTopics", new bool?());
        this.CanEdit = storage.GetValue<bool?>("CanEdit", new bool?());
        this.CanImpersonate = storage.GetValue<bool?>("CanImpersonate", new bool?());
        this.CanManageContent = storage.GetValue<bool?>("CanManageContent", new bool?());
        this.CanTariffFree = storage.GetValue<bool?>("CanTariffFree", new bool?());
        this.IsTelegramApproved = storage.GetValue<bool?>("IsTelegramApproved", new bool?());
        this.Backtest = storage.GetValue<StrategyBacktestOptions>("Backtest", (StrategyBacktestOptions)null);
        this.Optimization = storage.GetValue<StrategyOptimizationOptions>("Optimization", (StrategyOptimizationOptions)null);
        this.Live = storage.GetValue<StrategyLiveOptions>("Live", (StrategyLiveOptions)null);
        this.SubscriptionsByTopicType = storage.GetValue<TopicTypes[]>("SubscriptionsByTopicType", (TopicTypes[])null);
        this.Awards = storage.GetValue<BaseEntitySet<Client>>("Awards", (BaseEntitySet<Client>)null);
        this.Balances = storage.GetValue<BaseEntitySet<ClientBalance>>("Balances", (BaseEntitySet<ClientBalance>)null);
        this.BankAccounts = storage.GetValue<BaseEntitySet<AccountRequisites>>("BankAccounts", (BaseEntitySet<AccountRequisites>)null);
        this.Blockeds = storage.GetValue<BaseEntitySet<Client>>("Blockeds", (BaseEntitySet<Client>)null);
        this.Brokers = storage.GetValue<BaseEntitySet<Product>>("Brokers", (BaseEntitySet<Product>)null);
        this.Downloads = storage.GetValue<BaseEntitySet<FileDownload>>("Downloads", (BaseEntitySet<FileDownload>)null);
        this.Errors = storage.GetValue<BaseEntitySet<Error>>("Errors", (BaseEntitySet<Error>)null);
        this.Favorites = storage.GetValue<BaseEntitySet<Favorite>>("Favorites", (BaseEntitySet<Favorite>)null);
        this.Features = storage.GetValue<BaseEntitySet<LicenseFeature>>("Features", (BaseEntitySet<LicenseFeature>)null);
        this.Files = storage.GetValue<BaseEntitySet<File>>("Files", (BaseEntitySet<File>)null);
        this.FileShares = storage.GetValue<BaseEntitySet<FileShare>>("FileShares", (BaseEntitySet<FileShare>)null);
        this.GivenMessageVotes = storage.GetValue<BaseEntitySet<MessageVote>>("GivenMessageVotes", (BaseEntitySet<MessageVote>)null);
        this.Inner = storage.GetValue<BaseEntitySet<Client>>("Inner", (BaseEntitySet<Client>)null);
        this.IpActivity = storage.GetValue<BaseEntitySet<ClientIpAddress>>("IpActivity", (BaseEntitySet<ClientIpAddress>)null);
        this.Licenses = storage.GetValue<BaseEntitySet<License>>("Licenses", (BaseEntitySet<License>)null);
        this.ManagedGroups = storage.GetValue<BaseEntitySet<ProductGroup>>("ManagedGroups", (BaseEntitySet<ProductGroup>)null);
        this.ManagedProducts = storage.GetValue<BaseEntitySet<Product>>("ManagedProducts", (BaseEntitySet<Product>)null);
        this.Messages = storage.GetValue<BaseEntitySet<Message>>("Messages", (BaseEntitySet<Message>)null);
        this.Payments = storage.GetValue<BaseEntitySet<Payment>>("Payments", (BaseEntitySet<Payment>)null);
        this.Polls = storage.GetValue<BaseEntitySet<Poll>>("Polls", (BaseEntitySet<Poll>)null);
        this.PollVotes = storage.GetValue<BaseEntitySet<PollVote>>("PollVotes", (BaseEntitySet<PollVote>)null);
        this.PrivateTopics = storage.GetValue<BaseEntitySet<Topic>>("PrivateTopics", (BaseEntitySet<Topic>)null);
        this.ProductBugReports = storage.GetValue<BaseEntitySet<ProductBugReport>>("ProductBugReports", (BaseEntitySet<ProductBugReport>)null);
        this.ProductFeedbacks = storage.GetValue<BaseEntitySet<ProductFeedback>>("ProductFeedbacks", (BaseEntitySet<ProductFeedback>)null);
        this.ProductGroupReferrals = storage.GetValue<BaseEntitySet<ProductGroupReferral>>("ProductGroupReferrals", (BaseEntitySet<ProductGroupReferral>)null);
        this.ProductOrders = storage.GetValue<BaseEntitySet<ProductOrder>>("ProductOrders", (BaseEntitySet<ProductOrder>)null);
        this.ProductOrdersVideo = storage.GetValue<BaseEntitySet<ProductOrder>>("ProductOrdersVideo", (BaseEntitySet<ProductOrder>)null);
        this.ProductOrdersDevelopment = storage.GetValue<BaseEntitySet<ProductOrder>>("ProductOrdersDevelopment", (BaseEntitySet<ProductOrder>)null);
        this.ProductOrdersAccount = storage.GetValue<BaseEntitySet<ProductOrder>>("ProductOrdersAccount", (BaseEntitySet<ProductOrder>)null);
        this.ProductOrdersTrial = storage.GetValue<BaseEntitySet<ProductOrder>>("ProductOrdersTrial", (BaseEntitySet<ProductOrder>)null);
        this.ProductOrdersReferral = storage.GetValue<BaseEntitySet<ProductOrder>>("ProductOrdersReferral", (BaseEntitySet<ProductOrder>)null);
        this.Products = storage.GetValue<BaseEntitySet<Product>>("Products", (BaseEntitySet<Product>)null);
        this.ProfileVisits = storage.GetValue<BaseEntitySet<ProfileVisit>>("ProfileVisits", (BaseEntitySet<ProfileVisit>)null);
        this.ReceivedMessageVotes = storage.GetValue<BaseEntitySet<MessageVote>>("ReceivedMessageVotes", (BaseEntitySet<MessageVote>)null);
        this.ReferralClients = storage.GetValue<BaseEntitySet<Client>>("ReferralClients", (BaseEntitySet<Client>)null);
        this.Roles2 = storage.GetValue<BaseEntitySet<ClientRole>>("Roles2", (BaseEntitySet<ClientRole>)null);
        this.Sessions = storage.GetValue<BaseEntitySet<Session>>("Sessions", (BaseEntitySet<Session>)null);
        this.ShortUrlVisits = storage.GetValue<BaseEntitySet<ShortUrlVisit>>("ShortUrlVisits", (BaseEntitySet<ShortUrlVisit>)null);
        this.Subscriptions = storage.GetValue<BaseEntitySet<Subscription>>("Subscriptions", (BaseEntitySet<Subscription>)null);
        this.Suspicious = storage.GetValue<BaseEntitySet<StockSharp.Web.DomainModel.Suspicious>>("Suspicious", (BaseEntitySet<StockSharp.Web.DomainModel.Suspicious>)null);
        this.Topics = storage.GetValue<BaseEntitySet<Topic>>("Topics", (BaseEntitySet<Topic>)null);
        this.Strategies = storage.GetValue<BaseEntitySet<Strategy>>("Strategies", (BaseEntitySet<Strategy>)null);
        this.StrategyAccounts = storage.GetValue<BaseEntitySet<StrategyAccount>>("StrategyAccounts", (BaseEntitySet<StrategyAccount>)null);
        this.AccessTokens = storage.GetValue<BaseEntitySet<AccessToken>>("AccessTokens", (BaseEntitySet<AccessToken>)null);
        this.Socials = storage.GetValue<BaseEntitySet<ClientSocial>>("Socials", (BaseEntitySet<ClientSocial>)null);
        this.DataPermissions = storage.GetValue<BaseEntitySet<DataPermission>>("DataPermissions", (BaseEntitySet<DataPermission>)null);
        this.Roles = storage.GetValue<BaseEntitySet<Client>>("Roles", (BaseEntitySet<Client>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Email", this.Email).Set<string>("Phone", this.Phone).Set<string>("City", this.City).Set<string>("Skype", this.Skype).Set<StockSharp.Web.DomainModel.EmailPreferences?>("EmailPreferences", this.EmailPreferences).Set<Domain>("Domain", this.Domain).Set<bool?>("IsAgreementAccepted", this.IsAgreementAccepted).Set<bool?>("IsAllowStatistics", this.IsAllowStatistics).Set<string>("RealName", this.RealName).Set<bool?>("IsPhonePublic", this.IsPhonePublic).Set<string>("VKontakte", this.VKontakte).Set<string>("DisplayName", this.DisplayName).Set<int?>("EmailBounceCount", this.EmailBounceCount).Set<string>("DeleteReason", this.DeleteReason).Set<string>("UnsubscribeReason", this.UnsubscribeReason).Set<File>("Picture", this.Picture).Set<DateTime?>("Birthday", this.Birthday).Set<string>("Facebook", this.Facebook).Set<int>("Gender", this.Gender).Set<string>("Homepage", this.Homepage).Set<bool?>("IsApproved", this.IsApproved).Set<bool?>("IsLockedOut", this.IsLockedOut).Set<DateTime?>("LastActivityDate", this.LastActivityDate).Set<DateTime?>("LastLockOutDate", this.LastLockOutDate).Set<DateTime?>("LastLoginDate", this.LastLoginDate).Set<DateTime?>("LastPasswordChangedDate", this.LastPasswordChangedDate).Set<Message>("PublicDescription", this.PublicDescription).Set<Message>("PrivateDescription", this.PrivateDescription).Set<bool?>("Gravatar", this.Gravatar).Set<string>("GravatarToken", this.GravatarToken).Set<string>("Telegram", this.Telegram).Set<long?>("TelegramId", this.TelegramId).Set<Client>("Referral", this.Referral).Set<bool?>("SelfDeleted", this.SelfDeleted).Set<long?>("FileUploadMaxBytes", this.FileUploadMaxBytes).Set<bool?>("IsTrialVerified", this.IsTrialVerified).Set<string>("AuthToken", this.AuthToken).Set<bool?>("CanPublish", this.CanPublish).Set<long?>("UploadLimit", this.UploadLimit).Set<string>("UrlRelative", this.UrlRelative).Set<bool?>("BlockedTo", this.BlockedTo).Set<bool?>("BlockedFrom", this.BlockedFrom).Set<bool?>("IsMySubscription", this.IsMySubscription).Set<DateTime?>("SupportTill", this.SupportTill).Set<Topic>("PrivateTopic", this.PrivateTopic).Set<string>("SubscriptionToken", this.SubscriptionToken).Set<Social>("Social", this.Social).Set<string>("SocialId", this.SocialId).Set<string>("SocialLink", this.SocialLink).Set<string>("NugetPrefix", this.NugetPrefix).Set<int?>("SocialMaxLength", this.SocialMaxLength).Set<bool?>("HasTelegramRobots", this.HasTelegramRobots).Set<bool?>("HasTelegramAlerts", this.HasTelegramAlerts).Set<bool?>("HasNewPrivateTopics", this.HasNewPrivateTopics).Set<bool?>("CanEdit", this.CanEdit).Set<bool?>("CanImpersonate", this.CanImpersonate).Set<bool?>("CanManageContent", this.CanManageContent).Set<bool?>("CanTariffFree", this.CanTariffFree).Set<bool?>("IsTelegramApproved", this.IsTelegramApproved).Set<StrategyBacktestOptions>("Backtest", this.Backtest).Set<StrategyOptimizationOptions>("Optimization", this.Optimization).Set<StrategyLiveOptions>("Live", this.Live).Set<TopicTypes[]>("SubscriptionsByTopicType", this.SubscriptionsByTopicType).Set<BaseEntitySet<Client>>("Awards", this.Awards).Set<BaseEntitySet<ClientBalance>>("Balances", this.Balances).Set<BaseEntitySet<AccountRequisites>>("BankAccounts", this.BankAccounts).Set<BaseEntitySet<Client>>("Blockeds", this.Blockeds).Set<BaseEntitySet<Product>>("Brokers", this.Brokers).Set<BaseEntitySet<FileDownload>>("Downloads", this.Downloads).Set<BaseEntitySet<Error>>("Errors", this.Errors).Set<BaseEntitySet<Favorite>>("Favorites", this.Favorites).Set<BaseEntitySet<LicenseFeature>>("Features", this.Features).Set<BaseEntitySet<File>>("Files", this.Files).Set<BaseEntitySet<FileShare>>("FileShares", this.FileShares).Set<BaseEntitySet<MessageVote>>("GivenMessageVotes", this.GivenMessageVotes).Set<BaseEntitySet<Client>>("Inner", this.Inner).Set<BaseEntitySet<ClientIpAddress>>("IpActivity", this.IpActivity).Set<BaseEntitySet<License>>("Licenses", this.Licenses).Set<BaseEntitySet<ProductGroup>>("ManagedGroups", this.ManagedGroups).Set<BaseEntitySet<Product>>("ManagedProducts", this.ManagedProducts).Set<BaseEntitySet<Message>>("Messages", this.Messages).Set<BaseEntitySet<Payment>>("Payments", this.Payments).Set<BaseEntitySet<Poll>>("Polls", this.Polls).Set<BaseEntitySet<PollVote>>("PollVotes", this.PollVotes).Set<BaseEntitySet<Topic>>("PrivateTopics", this.PrivateTopics).Set<BaseEntitySet<ProductBugReport>>("ProductBugReports", this.ProductBugReports).Set<BaseEntitySet<ProductFeedback>>("ProductFeedbacks", this.ProductFeedbacks).Set<BaseEntitySet<ProductGroupReferral>>("ProductGroupReferrals", this.ProductGroupReferrals).Set<BaseEntitySet<ProductOrder>>("ProductOrders", this.ProductOrders).Set<BaseEntitySet<ProductOrder>>("ProductOrdersVideo", this.ProductOrdersVideo).Set<BaseEntitySet<ProductOrder>>("ProductOrdersDevelopment", this.ProductOrdersDevelopment).Set<BaseEntitySet<ProductOrder>>("ProductOrdersAccount", this.ProductOrdersAccount).Set<BaseEntitySet<ProductOrder>>("ProductOrdersTrial", this.ProductOrdersTrial).Set<BaseEntitySet<ProductOrder>>("ProductOrdersReferral", this.ProductOrdersReferral).Set<BaseEntitySet<Product>>("Products", this.Products).Set<BaseEntitySet<ProfileVisit>>("ProfileVisits", this.ProfileVisits).Set<BaseEntitySet<MessageVote>>("ReceivedMessageVotes", this.ReceivedMessageVotes).Set<BaseEntitySet<Client>>("ReferralClients", this.ReferralClients).Set<BaseEntitySet<ClientRole>>("Roles2", this.Roles2).Set<BaseEntitySet<Session>>("Sessions", this.Sessions).Set<BaseEntitySet<ShortUrlVisit>>("ShortUrlVisits", this.ShortUrlVisits).Set<BaseEntitySet<Subscription>>("Subscriptions", this.Subscriptions).Set<BaseEntitySet<StockSharp.Web.DomainModel.Suspicious>>("Suspicious", this.Suspicious).Set<BaseEntitySet<Topic>>("Topics", this.Topics).Set<BaseEntitySet<Strategy>>("Strategies", this.Strategies).Set<BaseEntitySet<StrategyAccount>>("StrategyAccounts", this.StrategyAccounts).Set<BaseEntitySet<AccessToken>>("AccessTokens", this.AccessTokens).Set<BaseEntitySet<ClientSocial>>("Socials", this.Socials).Set<BaseEntitySet<DataPermission>>("DataPermissions", this.DataPermissions).Set<BaseEntitySet<Client>>("Roles", this.Roles);
    }
}
