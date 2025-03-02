// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Client
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
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

        [Obsolete( "Use AccessTokens property." )]
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

        public TopicTypes [ ] SubscriptionsByTopicType { get; set; }

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

        [Obsolete( "Use Roles2" )]
        public BaseEntitySet<Client> Roles { get; set; }

        string INameEntity.Name
        {
            get
            {
                return this.DisplayName;
            }
            set
            {
                this.DisplayName = value;
            }
        }

        string IDescriptionEntity.Description
        {
            get
            {
                string description = this._description;
                if ( !StringHelper.IsEmpty( description ) )
                    return description;
                string email = this.Email;
                Message publicDescription = this.PublicDescription;
                if ( ( publicDescription != null ? ( !StringHelper.IsEmpty( publicDescription.Text ) ? 1 : 0 ) : 0 ) != 0 )
                {
                    if ( !StringHelper.IsEmpty( email ) )
                        email += Environment.NewLine;
                    email += this.PublicDescription?.Text;
                }
                Message privateDescription = this.PrivateDescription;
                if ( ( privateDescription != null ? ( !StringHelper.IsEmpty( privateDescription.Text ) ? 1 : 0 ) : 0 ) != 0 )
                {
                    if ( !StringHelper.IsEmpty( email ) )
                        email += Environment.NewLine;
                    email += this.PrivateDescription?.Text;
                }
                return email;
            }
            set
            {
                this._description = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Email = ( string ) storage.GetValue<string>( "Email", null );
            this.Phone = ( string ) storage.GetValue<string>( "Phone", null );
            this.City = ( string ) storage.GetValue<string>( "City", null );
            this.Skype = ( string ) storage.GetValue<string>( "Skype", null );
            this.EmailPreferences = ( StockSharp.Web.DomainModel.EmailPreferences? ) storage.GetValue<StockSharp.Web.DomainModel.EmailPreferences?>( "EmailPreferences", new StockSharp.Web.DomainModel.EmailPreferences?() );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.IsAgreementAccepted = ( bool? ) storage.GetValue<bool?>( "IsAgreementAccepted", new bool?() );
            this.IsAllowStatistics = ( bool? ) storage.GetValue<bool?>( "IsAllowStatistics", new bool?() );
            this.RealName = ( string ) storage.GetValue<string>( "RealName", null );
            this.IsPhonePublic = ( bool? ) storage.GetValue<bool?>( "IsPhonePublic", new bool?() );
            this.VKontakte = ( string ) storage.GetValue<string>( "VKontakte", null );
            this.DisplayName = ( string ) storage.GetValue<string>( "DisplayName", null );
            this.EmailBounceCount = ( int? ) storage.GetValue<int?>( "EmailBounceCount", new int?() );
            this.DeleteReason = ( string ) storage.GetValue<string>( "DeleteReason", null );
            this.UnsubscribeReason = ( string ) storage.GetValue<string>( "UnsubscribeReason", null );
            this.Picture = ( File ) storage.GetValue<File>( "Picture", null );
            this.Birthday = ( DateTime? ) storage.GetValue<DateTime?>( "Birthday", new DateTime?() );
            this.Facebook = ( string ) storage.GetValue<string>( "Facebook", null );
            this.Gender = ( int ) storage.GetValue<int>( "Gender", 0 );
            this.Homepage = ( string ) storage.GetValue<string>( "Homepage", null );
            this.IsApproved = ( bool? ) storage.GetValue<bool?>( "IsApproved", new bool?() );
            this.IsLockedOut = ( bool? ) storage.GetValue<bool?>( "IsLockedOut", new bool?() );
            this.LastActivityDate = ( DateTime? ) storage.GetValue<DateTime?>( "LastActivityDate", new DateTime?() );
            this.LastLockOutDate = ( DateTime? ) storage.GetValue<DateTime?>( "LastLockOutDate", new DateTime?() );
            this.LastLoginDate = ( DateTime? ) storage.GetValue<DateTime?>( "LastLoginDate", new DateTime?() );
            this.LastPasswordChangedDate = ( DateTime? ) storage.GetValue<DateTime?>( "LastPasswordChangedDate", new DateTime?() );
            this.PublicDescription = ( Message ) storage.GetValue<Message>( "PublicDescription", null );
            this.PrivateDescription = ( Message ) storage.GetValue<Message>( "PrivateDescription", null );
            this.Gravatar = ( bool? ) storage.GetValue<bool?>( "Gravatar", new bool?() );
            this.GravatarToken = ( string ) storage.GetValue<string>( "GravatarToken", null );
            this.Telegram = ( string ) storage.GetValue<string>( "Telegram", null );
            this.TelegramId = ( long? ) storage.GetValue<long?>( "TelegramId", new long?() );
            this.Referral = ( Client ) storage.GetValue<Client>( "Referral", null );
            this.SelfDeleted = ( bool? ) storage.GetValue<bool?>( "SelfDeleted", new bool?() );
            this.FileUploadMaxBytes = ( long? ) storage.GetValue<long?>( "FileUploadMaxBytes", new long?() );
            this.IsTrialVerified = ( bool? ) storage.GetValue<bool?>( "IsTrialVerified", new bool?() );
            this.AuthToken = ( string ) storage.GetValue<string>( "AuthToken", null );
            this.CanPublish = ( bool? ) storage.GetValue<bool?>( "CanPublish", new bool?() );
            this.UploadLimit = ( long? ) storage.GetValue<long?>( "UploadLimit", new long?() );
            this.UrlRelative = ( string ) storage.GetValue<string>( "UrlRelative", null );
            this.BlockedTo = ( bool? ) storage.GetValue<bool?>( "BlockedTo", new bool?() );
            this.BlockedFrom = ( bool? ) storage.GetValue<bool?>( "BlockedFrom", new bool?() );
            this.IsMySubscription = ( bool? ) storage.GetValue<bool?>( "IsMySubscription", new bool?() );
            this.SupportTill = ( DateTime? ) storage.GetValue<DateTime?>( "SupportTill", new DateTime?() );
            this.PrivateTopic = ( Topic ) storage.GetValue<Topic>( "PrivateTopic", null );
            this.SubscriptionToken = ( string ) storage.GetValue<string>( "SubscriptionToken", null );
            this.Social = ( Social ) storage.GetValue<Social>( "Social", null );
            this.SocialId = ( string ) storage.GetValue<string>( "SocialId", null );
            this.SocialLink = ( string ) storage.GetValue<string>( "SocialLink", null );
            this.NugetPrefix = ( string ) storage.GetValue<string>( "NugetPrefix", null );
            this.SocialMaxLength = ( int? ) storage.GetValue<int?>( "SocialMaxLength", new int?() );
            this.HasTelegramRobots = ( bool? ) storage.GetValue<bool?>( "HasTelegramRobots", new bool?() );
            this.HasTelegramAlerts = ( bool? ) storage.GetValue<bool?>( "HasTelegramAlerts", new bool?() );
            this.HasNewPrivateTopics = ( bool? ) storage.GetValue<bool?>( "HasNewPrivateTopics", new bool?() );
            this.CanEdit = ( bool? ) storage.GetValue<bool?>( "CanEdit", new bool?() );
            this.CanImpersonate = ( bool? ) storage.GetValue<bool?>( "CanImpersonate", new bool?() );
            this.CanManageContent = ( bool? ) storage.GetValue<bool?>( "CanManageContent", new bool?() );
            this.CanTariffFree = ( bool? ) storage.GetValue<bool?>( "CanTariffFree", new bool?() );
            this.IsTelegramApproved = ( bool? ) storage.GetValue<bool?>( "IsTelegramApproved", new bool?() );
            this.Backtest = ( StrategyBacktestOptions ) storage.GetValue<StrategyBacktestOptions>( "Backtest", null );
            this.Optimization = ( StrategyOptimizationOptions ) storage.GetValue<StrategyOptimizationOptions>( "Optimization", null );
            this.Live = ( StrategyLiveOptions ) storage.GetValue<StrategyLiveOptions>( "Live", null );
            this.SubscriptionsByTopicType = ( TopicTypes [ ] ) storage.GetValue<TopicTypes [ ]>( "SubscriptionsByTopicType", null );
            this.Awards = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Awards", null );
            this.Balances = ( BaseEntitySet<ClientBalance> ) storage.GetValue<BaseEntitySet<ClientBalance>>( "Balances", null );
            this.BankAccounts = ( BaseEntitySet<AccountRequisites> ) storage.GetValue<BaseEntitySet<AccountRequisites>>( "BankAccounts", null );
            this.Blockeds = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Blockeds", null );
            this.Brokers = ( BaseEntitySet<Product> ) storage.GetValue<BaseEntitySet<Product>>( "Brokers", null );
            this.Downloads = ( BaseEntitySet<FileDownload> ) storage.GetValue<BaseEntitySet<FileDownload>>( "Downloads", null );
            this.Errors = ( BaseEntitySet<Error> ) storage.GetValue<BaseEntitySet<Error>>( "Errors", null );
            this.Favorites = ( BaseEntitySet<Favorite> ) storage.GetValue<BaseEntitySet<Favorite>>( "Favorites", null );
            this.Features = ( BaseEntitySet<LicenseFeature> ) storage.GetValue<BaseEntitySet<LicenseFeature>>( "Features", null );
            this.Files = ( BaseEntitySet<File> ) storage.GetValue<BaseEntitySet<File>>( "Files", null );
            this.FileShares = ( BaseEntitySet<FileShare> ) storage.GetValue<BaseEntitySet<FileShare>>( "FileShares", null );
            this.GivenMessageVotes = ( BaseEntitySet<MessageVote> ) storage.GetValue<BaseEntitySet<MessageVote>>( "GivenMessageVotes", null );
            this.Inner = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Inner", null );
            this.IpActivity = ( BaseEntitySet<ClientIpAddress> ) storage.GetValue<BaseEntitySet<ClientIpAddress>>( "IpActivity", null );
            this.Licenses = ( BaseEntitySet<License> ) storage.GetValue<BaseEntitySet<License>>( "Licenses", null );
            this.ManagedGroups = ( BaseEntitySet<ProductGroup> ) storage.GetValue<BaseEntitySet<ProductGroup>>( "ManagedGroups", null );
            this.ManagedProducts = ( BaseEntitySet<Product> ) storage.GetValue<BaseEntitySet<Product>>( "ManagedProducts", null );
            this.Messages = ( BaseEntitySet<Message> ) storage.GetValue<BaseEntitySet<Message>>( "Messages", null );
            this.Payments = ( BaseEntitySet<Payment> ) storage.GetValue<BaseEntitySet<Payment>>( "Payments", null );
            this.Polls = ( BaseEntitySet<Poll> ) storage.GetValue<BaseEntitySet<Poll>>( "Polls", null );
            this.PollVotes = ( BaseEntitySet<PollVote> ) storage.GetValue<BaseEntitySet<PollVote>>( "PollVotes", null );
            this.PrivateTopics = ( BaseEntitySet<Topic> ) storage.GetValue<BaseEntitySet<Topic>>( "PrivateTopics", null );
            this.ProductBugReports = ( BaseEntitySet<ProductBugReport> ) storage.GetValue<BaseEntitySet<ProductBugReport>>( "ProductBugReports", null );
            this.ProductFeedbacks = ( BaseEntitySet<ProductFeedback> ) storage.GetValue<BaseEntitySet<ProductFeedback>>( "ProductFeedbacks", null );
            this.ProductGroupReferrals = ( BaseEntitySet<ProductGroupReferral> ) storage.GetValue<BaseEntitySet<ProductGroupReferral>>( "ProductGroupReferrals", null );
            this.ProductOrders = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "ProductOrders", null );
            this.ProductOrdersVideo = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "ProductOrdersVideo", null );
            this.ProductOrdersDevelopment = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "ProductOrdersDevelopment", null );
            this.ProductOrdersAccount = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "ProductOrdersAccount", null );
            this.ProductOrdersTrial = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "ProductOrdersTrial", null );
            this.ProductOrdersReferral = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "ProductOrdersReferral", null );
            this.Products = ( BaseEntitySet<Product> ) storage.GetValue<BaseEntitySet<Product>>( "Products", null );
            this.ProfileVisits = ( BaseEntitySet<ProfileVisit> ) storage.GetValue<BaseEntitySet<ProfileVisit>>( "ProfileVisits", null );
            this.ReceivedMessageVotes = ( BaseEntitySet<MessageVote> ) storage.GetValue<BaseEntitySet<MessageVote>>( "ReceivedMessageVotes", null );
            this.ReferralClients = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "ReferralClients", null );
            this.Roles2 = ( BaseEntitySet<ClientRole> ) storage.GetValue<BaseEntitySet<ClientRole>>( "Roles2", null );
            this.Sessions = ( BaseEntitySet<Session> ) storage.GetValue<BaseEntitySet<Session>>( "Sessions", null );
            this.ShortUrlVisits = ( BaseEntitySet<ShortUrlVisit> ) storage.GetValue<BaseEntitySet<ShortUrlVisit>>( "ShortUrlVisits", null );
            this.Subscriptions = ( BaseEntitySet<Subscription> ) storage.GetValue<BaseEntitySet<Subscription>>( "Subscriptions", null );
            this.Suspicious = ( BaseEntitySet<StockSharp.Web.DomainModel.Suspicious> ) storage.GetValue<BaseEntitySet<StockSharp.Web.DomainModel.Suspicious>>( "Suspicious", null );
            this.Topics = ( BaseEntitySet<Topic> ) storage.GetValue<BaseEntitySet<Topic>>( "Topics", null );
            this.Strategies = ( BaseEntitySet<Strategy> ) storage.GetValue<BaseEntitySet<Strategy>>( "Strategies", null );
            this.StrategyAccounts = ( BaseEntitySet<StrategyAccount> ) storage.GetValue<BaseEntitySet<StrategyAccount>>( "StrategyAccounts", null );
            this.AccessTokens = ( BaseEntitySet<AccessToken> ) storage.GetValue<BaseEntitySet<AccessToken>>( "AccessTokens", null );
            this.Socials = ( BaseEntitySet<ClientSocial> ) storage.GetValue<BaseEntitySet<ClientSocial>>( "Socials", null );
            this.DataPermissions = ( BaseEntitySet<DataPermission> ) storage.GetValue<BaseEntitySet<DataPermission>>( "DataPermissions", null );
            this.Roles = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Roles", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Email", this.Email ).Set<string>( "Phone", this.Phone ).Set<string>( "City", this.City ).Set<string>( "Skype", this.Skype ).Set<StockSharp.Web.DomainModel.EmailPreferences?>( "EmailPreferences", this.EmailPreferences ).Set<Domain>( "Domain", this.Domain ).Set<bool?>( "IsAgreementAccepted", this.IsAgreementAccepted ).Set<bool?>( "IsAllowStatistics", this.IsAllowStatistics ).Set<string>( "RealName", this.RealName ).Set<bool?>( "IsPhonePublic", this.IsPhonePublic ).Set<string>( "VKontakte", this.VKontakte ).Set<string>( "DisplayName", this.DisplayName ).Set<int?>( "EmailBounceCount", this.EmailBounceCount ).Set<string>( "DeleteReason", this.DeleteReason ).Set<string>( "UnsubscribeReason", this.UnsubscribeReason ).Set<File>( "Picture", this.Picture ).Set<DateTime?>( "Birthday", this.Birthday ).Set<string>( "Facebook", this.Facebook ).Set<int>( "Gender", this.Gender ).Set<string>( "Homepage", this.Homepage ).Set<bool?>( "IsApproved", this.IsApproved ).Set<bool?>( "IsLockedOut", this.IsLockedOut ).Set<DateTime?>( "LastActivityDate", this.LastActivityDate ).Set<DateTime?>( "LastLockOutDate", this.LastLockOutDate ).Set<DateTime?>( "LastLoginDate", this.LastLoginDate ).Set<DateTime?>( "LastPasswordChangedDate", this.LastPasswordChangedDate ).Set<Message>( "PublicDescription", this.PublicDescription ).Set<Message>( "PrivateDescription", this.PrivateDescription ).Set<bool?>( "Gravatar", this.Gravatar ).Set<string>( "GravatarToken", this.GravatarToken ).Set<string>( "Telegram", this.Telegram ).Set<long?>( "TelegramId", this.TelegramId ).Set<Client>( "Referral", this.Referral ).Set<bool?>( "SelfDeleted", this.SelfDeleted ).Set<long?>( "FileUploadMaxBytes", this.FileUploadMaxBytes ).Set<bool?>( "IsTrialVerified", this.IsTrialVerified ).Set<string>( "AuthToken", this.AuthToken ).Set<bool?>( "CanPublish", this.CanPublish ).Set<long?>( "UploadLimit", this.UploadLimit ).Set<string>( "UrlRelative", this.UrlRelative ).Set<bool?>( "BlockedTo", this.BlockedTo ).Set<bool?>( "BlockedFrom", this.BlockedFrom ).Set<bool?>( "IsMySubscription", this.IsMySubscription ).Set<DateTime?>( "SupportTill", this.SupportTill ).Set<Topic>( "PrivateTopic", this.PrivateTopic ).Set<string>( "SubscriptionToken", this.SubscriptionToken ).Set<Social>( "Social", this.Social ).Set<string>( "SocialId", this.SocialId ).Set<string>( "SocialLink", this.SocialLink ).Set<string>( "NugetPrefix", this.NugetPrefix ).Set<int?>( "SocialMaxLength", this.SocialMaxLength ).Set<bool?>( "HasTelegramRobots", this.HasTelegramRobots ).Set<bool?>( "HasTelegramAlerts", this.HasTelegramAlerts ).Set<bool?>( "HasNewPrivateTopics", this.HasNewPrivateTopics ).Set<bool?>( "CanEdit", this.CanEdit ).Set<bool?>( "CanImpersonate", this.CanImpersonate ).Set<bool?>( "CanManageContent", this.CanManageContent ).Set<bool?>( "CanTariffFree", this.CanTariffFree ).Set<bool?>( "IsTelegramApproved", this.IsTelegramApproved ).Set<StrategyBacktestOptions>( "Backtest", this.Backtest ).Set<StrategyOptimizationOptions>( "Optimization", this.Optimization ).Set<StrategyLiveOptions>( "Live", this.Live ).Set<TopicTypes [ ]>( "SubscriptionsByTopicType", this.SubscriptionsByTopicType ).Set<BaseEntitySet<Client>>( "Awards", this.Awards ).Set<BaseEntitySet<ClientBalance>>( "Balances", this.Balances ).Set<BaseEntitySet<AccountRequisites>>( "BankAccounts", this.BankAccounts ).Set<BaseEntitySet<Client>>( "Blockeds", this.Blockeds ).Set<BaseEntitySet<Product>>( "Brokers", this.Brokers ).Set<BaseEntitySet<FileDownload>>( "Downloads", this.Downloads ).Set<BaseEntitySet<Error>>( "Errors", this.Errors ).Set<BaseEntitySet<Favorite>>( "Favorites", this.Favorites ).Set<BaseEntitySet<LicenseFeature>>( "Features", this.Features ).Set<BaseEntitySet<File>>( "Files", this.Files ).Set<BaseEntitySet<FileShare>>( "FileShares", this.FileShares ).Set<BaseEntitySet<MessageVote>>( "GivenMessageVotes", this.GivenMessageVotes ).Set<BaseEntitySet<Client>>( "Inner", this.Inner ).Set<BaseEntitySet<ClientIpAddress>>( "IpActivity", this.IpActivity ).Set<BaseEntitySet<License>>( "Licenses", this.Licenses ).Set<BaseEntitySet<ProductGroup>>( "ManagedGroups", this.ManagedGroups ).Set<BaseEntitySet<Product>>( "ManagedProducts", this.ManagedProducts ).Set<BaseEntitySet<Message>>( "Messages", this.Messages ).Set<BaseEntitySet<Payment>>( "Payments", this.Payments ).Set<BaseEntitySet<Poll>>( "Polls", this.Polls ).Set<BaseEntitySet<PollVote>>( "PollVotes", this.PollVotes ).Set<BaseEntitySet<Topic>>( "PrivateTopics", this.PrivateTopics ).Set<BaseEntitySet<ProductBugReport>>( "ProductBugReports", this.ProductBugReports ).Set<BaseEntitySet<ProductFeedback>>( "ProductFeedbacks", this.ProductFeedbacks ).Set<BaseEntitySet<ProductGroupReferral>>( "ProductGroupReferrals", this.ProductGroupReferrals ).Set<BaseEntitySet<ProductOrder>>( "ProductOrders", this.ProductOrders ).Set<BaseEntitySet<ProductOrder>>( "ProductOrdersVideo", this.ProductOrdersVideo ).Set<BaseEntitySet<ProductOrder>>( "ProductOrdersDevelopment", this.ProductOrdersDevelopment ).Set<BaseEntitySet<ProductOrder>>( "ProductOrdersAccount", this.ProductOrdersAccount ).Set<BaseEntitySet<ProductOrder>>( "ProductOrdersTrial", this.ProductOrdersTrial ).Set<BaseEntitySet<ProductOrder>>( "ProductOrdersReferral", this.ProductOrdersReferral ).Set<BaseEntitySet<Product>>( "Products", this.Products ).Set<BaseEntitySet<ProfileVisit>>( "ProfileVisits", this.ProfileVisits ).Set<BaseEntitySet<MessageVote>>( "ReceivedMessageVotes", this.ReceivedMessageVotes ).Set<BaseEntitySet<Client>>( "ReferralClients", this.ReferralClients ).Set<BaseEntitySet<ClientRole>>( "Roles2", this.Roles2 ).Set<BaseEntitySet<Session>>( "Sessions", this.Sessions ).Set<BaseEntitySet<ShortUrlVisit>>( "ShortUrlVisits", this.ShortUrlVisits ).Set<BaseEntitySet<Subscription>>( "Subscriptions", this.Subscriptions ).Set<BaseEntitySet<StockSharp.Web.DomainModel.Suspicious>>( "Suspicious", this.Suspicious ).Set<BaseEntitySet<Topic>>( "Topics", this.Topics ).Set<BaseEntitySet<Strategy>>( "Strategies", this.Strategies ).Set<BaseEntitySet<StrategyAccount>>( "StrategyAccounts", this.StrategyAccounts ).Set<BaseEntitySet<AccessToken>>( "AccessTokens", this.AccessTokens ).Set<BaseEntitySet<ClientSocial>>( "Socials", this.Socials ).Set<BaseEntitySet<DataPermission>>( "DataPermissions", this.DataPermissions ).Set<BaseEntitySet<Client>>( "Roles", this.Roles );
        }
    }
}
