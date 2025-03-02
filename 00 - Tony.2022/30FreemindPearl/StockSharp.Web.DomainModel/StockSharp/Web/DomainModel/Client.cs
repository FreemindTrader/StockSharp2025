using Ecng.Serialization;
using System;

#pragma warning disable CS0618

namespace StockSharp.Web.DomainModel
{
    public class Client : BaseEntity
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string Skype { get; set; }

        public bool? IsSubscriptionEnabled { get; set; }

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

        public File Avatar { get; set; }

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

        public ClientTrustLevels? TrustLevel { get; set; }

        public Client Referral { get; set; }

        public bool? SelfDeleted { get; set; }

        public long? FileUploadMaxBytes { get; set; }

        public bool? IsTrialVerified { get; set; }

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

        public bool? HasNewPrivateTopics { get; set; }

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

        public BaseEntitySet<ProductOrder> ProductOrdersSupport { get; set; }

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

        public BaseEntitySet<Suspicious> Suspicious { get; set; }

        public BaseEntitySet<Client> Tags { get; set; }

        public BaseEntitySet<Topic> Topics { get; set; }

        [Obsolete("Use Roles2")]
        public BaseEntitySet<Client> Roles { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Email                    = storage.GetValue("Email", (string)null);
            Phone                    = storage.GetValue("Phone", (string)null);
            City                     = storage.GetValue("City", (string)null);
            Skype                    = storage.GetValue("Skype", (string)null);
            IsSubscriptionEnabled    = storage.GetValue("IsSubscriptionEnabled", new bool?());
            Domain                   = storage.GetValue("Domain", (Domain)null);
            IsAgreementAccepted      = storage.GetValue("IsAgreementAccepted", new bool?());
            IsAllowStatistics        = storage.GetValue("IsAllowStatistics", new bool?());
            RealName                 = storage.GetValue("RealName", (string)null);
            IsPhonePublic            = storage.GetValue("IsPhonePublic", new bool?());
            VKontakte                = storage.GetValue("VKontakte", (string)null);
            DisplayName              = storage.GetValue("DisplayName", (string)null);
            EmailBounceCount         = storage.GetValue("EmailBounceCount", new int?());
            DeleteReason             = storage.GetValue("DeleteReason", (string)null);
            UnsubscribeReason        = storage.GetValue("UnsubscribeReason", (string)null);
            Avatar                   = storage.GetValue("Avatar", (File)null);
            Birthday                 = storage.GetValue("Birthday", new DateTime?());
            Facebook                 = storage.GetValue("Facebook", (string)null);
            Gender                   = storage.GetValue("Gender", 0);
            Homepage                 = storage.GetValue("Homepage", (string)null);
            IsApproved               = storage.GetValue("IsApproved", new bool?());
            IsLockedOut              = storage.GetValue("IsLockedOut", new bool?());
            LastActivityDate         = storage.GetValue("LastActivityDate", new DateTime?());
            LastLockOutDate          = storage.GetValue("LastLockOutDate", new DateTime?());
            LastLoginDate            = storage.GetValue("LastLoginDate", new DateTime?());
            LastPasswordChangedDate  = storage.GetValue("LastPasswordChangedDate", new DateTime?());
            PublicDescription        = storage.GetValue("PublicDescription", (Message)null);
            PrivateDescription       = storage.GetValue("PrivateDescription", (Message)null);
            Gravatar                 = storage.GetValue("Gravatar", new bool?());
            GravatarToken            = storage.GetValue("GravatarToken", (string)null);
            Telegram                 = storage.GetValue("Telegram", (string)null);
            TrustLevel               = storage.GetValue("TrustLevel", new ClientTrustLevels?());
            Referral                 = storage.GetValue("Referral", (Client)null);
            SelfDeleted              = storage.GetValue("SelfDeleted", new bool?());
            FileUploadMaxBytes       = storage.GetValue("FileUploadMaxBytes", new long?());
            IsTrialVerified          = storage.GetValue("IsTrialVerified", new bool?());
            AuthToken                = storage.GetValue("AuthToken", (string)null);
            CanPublish               = storage.GetValue("CanPublish", new bool?());
            UploadLimit              = storage.GetValue("UploadLimit", new long?());
            UrlRelative              = storage.GetValue("UrlRelative", (string)null);
            BlockedTo                = storage.GetValue("BlockedTo", new bool?());
            BlockedFrom              = storage.GetValue("BlockedFrom", new bool?());
            IsMySubscription         = storage.GetValue("IsMySubscription", new bool?());
            SupportTill              = storage.GetValue("SupportTill", new DateTime?());
            PrivateTopic             = storage.GetValue("PrivateTopic", (Topic)null);
            SubscriptionToken        = storage.GetValue("SubscriptionToken", (string)null);
            HasNewPrivateTopics      = storage.GetValue("HasNewPrivateTopics", new bool?());
            SubscriptionsByTopicType = storage.GetValue("SubscriptionsByTopicType", (TopicTypes[])null);
            Awards                   = storage.GetValue("Awards", (BaseEntitySet<Client>)null);
            Balances                 = storage.GetValue("Balances", (BaseEntitySet<ClientBalance>)null);
            BankAccounts             = storage.GetValue("BankAccounts", (BaseEntitySet<AccountRequisites>)null);
            Blockeds                 = storage.GetValue("Blockeds", (BaseEntitySet<Client>)null);
            Brokers                  = storage.GetValue("Brokers", (BaseEntitySet<Product>)null);
            Downloads                = storage.GetValue("Downloads", (BaseEntitySet<FileDownload>)null);
            Errors                   = storage.GetValue("Errors", (BaseEntitySet<Error>)null);
            Favorites                = storage.GetValue("Favorites", (BaseEntitySet<Favorite>)null);
            Features                 = storage.GetValue("Features", (BaseEntitySet<LicenseFeature>)null);
            Files                    = storage.GetValue("Files", (BaseEntitySet<File>)null);
            FileShares               = storage.GetValue("FileShares", (BaseEntitySet<FileShare>)null);
            GivenMessageVotes        = storage.GetValue("GivenMessageVotes", (BaseEntitySet<MessageVote>)null);
            Inner                    = storage.GetValue("Inner", (BaseEntitySet<Client>)null);
            IpActivity               = storage.GetValue("IpActivity", (BaseEntitySet<ClientIpAddress>)null);
            Licenses                 = storage.GetValue("Licenses", (BaseEntitySet<License>)null);
            ManagedGroups            = storage.GetValue("ManagedGroups", (BaseEntitySet<ProductGroup>)null);
            ManagedProducts          = storage.GetValue("ManagedProducts", (BaseEntitySet<Product>)null);
            Messages                 = storage.GetValue("Messages", (BaseEntitySet<Message>)null);
            Payments                 = storage.GetValue("Payments", (BaseEntitySet<Payment>)null);
            Polls                    = storage.GetValue("Polls", (BaseEntitySet<Poll>)null);
            PollVotes                = storage.GetValue("PollVotes", (BaseEntitySet<PollVote>)null);
            PrivateTopics            = storage.GetValue("PrivateTopics", (BaseEntitySet<Topic>)null);
            ProductBugReports        = storage.GetValue("ProductBugReports", (BaseEntitySet<ProductBugReport>)null);
            ProductFeedbacks         = storage.GetValue("ProductFeedbacks", (BaseEntitySet<ProductFeedback>)null);
            ProductGroupReferrals    = storage.GetValue("ProductGroupReferrals", (BaseEntitySet<ProductGroupReferral>)null);
            ProductOrders            = storage.GetValue("ProductOrders", (BaseEntitySet<ProductOrder>)null);
            ProductOrdersVideo       = storage.GetValue("ProductOrdersVideo", (BaseEntitySet<ProductOrder>)null);
            ProductOrdersDevelopment = storage.GetValue("ProductOrdersDevelopment", (BaseEntitySet<ProductOrder>)null);
            ProductOrdersSupport     = storage.GetValue("ProductOrdersSupport", (BaseEntitySet<ProductOrder>)null);
            ProductOrdersAccount     = storage.GetValue("ProductOrdersAccount", (BaseEntitySet<ProductOrder>)null);
            ProductOrdersTrial       = storage.GetValue("ProductOrdersTrial", (BaseEntitySet<ProductOrder>)null);
            ProductOrdersReferral    = storage.GetValue("ProductOrdersReferral", (BaseEntitySet<ProductOrder>)null);
            Products                 = storage.GetValue("Products", (BaseEntitySet<Product>)null);
            ProfileVisits            = storage.GetValue("ProfileVisits", (BaseEntitySet<ProfileVisit>)null);
            ReceivedMessageVotes     = storage.GetValue("ReceivedMessageVotes", (BaseEntitySet<MessageVote>)null);
            ReferralClients          = storage.GetValue("ReferralClients", (BaseEntitySet<Client>)null);
            Roles2                   = storage.GetValue("Roles2", (BaseEntitySet<ClientRole>)null);
            Sessions                 = storage.GetValue("Sessions", (BaseEntitySet<Session>)null);
            ShortUrlVisits           = storage.GetValue("ShortUrlVisits", (BaseEntitySet<ShortUrlVisit>)null);
            Subscriptions            = storage.GetValue("Subscriptions", (BaseEntitySet<Subscription>)null);
            Suspicious               = storage.GetValue("Suspicious", (BaseEntitySet<Suspicious>)null);
            Tags                     = storage.GetValue("Tags", (BaseEntitySet<Client>)null);
            Topics                   = storage.GetValue("Topics", (BaseEntitySet<Topic>)null);
            Roles                    = storage.GetValue("Roles", (BaseEntitySet<Client>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Email", Email).Set("Phone", Phone).Set("City", City).Set("Skype", Skype).Set("IsSubscriptionEnabled", IsSubscriptionEnabled).Set("Domain", Domain).Set("IsAgreementAccepted", IsAgreementAccepted).Set("IsAllowStatistics", IsAllowStatistics).Set("RealName", RealName).Set("IsPhonePublic", IsPhonePublic).Set("VKontakte", VKontakte).Set("DisplayName", DisplayName).Set("EmailBounceCount", EmailBounceCount).Set("DeleteReason", DeleteReason).Set("UnsubscribeReason", UnsubscribeReason).Set("Avatar", Avatar).Set("Birthday", Birthday).Set("Facebook", Facebook).Set("Gender", Gender).Set("Homepage", Homepage).Set("IsApproved", IsApproved).Set("IsLockedOut", IsLockedOut).Set("LastActivityDate", LastActivityDate).Set("LastLockOutDate", LastLockOutDate).Set("LastLoginDate", LastLoginDate).Set("LastPasswordChangedDate", LastPasswordChangedDate).Set("PublicDescription", PublicDescription).Set("PrivateDescription", PrivateDescription).Set("Gravatar", Gravatar).Set("GravatarToken", GravatarToken).Set("Telegram", Telegram).Set("TrustLevel", TrustLevel).Set("Referral", Referral).Set("SelfDeleted", SelfDeleted).Set("FileUploadMaxBytes", FileUploadMaxBytes).Set("IsTrialVerified", IsTrialVerified).Set("AuthToken", AuthToken).Set("CanPublish", CanPublish).Set("UploadLimit", UploadLimit).Set("UrlRelative", UrlRelative).Set("BlockedTo", BlockedTo).Set("BlockedFrom", BlockedFrom).Set("IsMySubscription", IsMySubscription).Set("SupportTill", SupportTill).Set("PrivateTopic", PrivateTopic).Set("SubscriptionToken", SubscriptionToken).Set("HasNewPrivateTopics", HasNewPrivateTopics).Set("SubscriptionsByTopicType", SubscriptionsByTopicType).Set("Awards", Awards).Set("Balances", Balances).Set("BankAccounts", BankAccounts).Set("Blockeds", Blockeds).Set("Brokers", Brokers).Set("Downloads", Downloads).Set("Errors", Errors).Set("Favorites", Favorites).Set("Features", Features).Set("Files", Files).Set("FileShares", FileShares).Set("GivenMessageVotes", GivenMessageVotes).Set("Inner", Inner).Set("IpActivity", IpActivity).Set("Licenses", Licenses).Set("ManagedGroups", ManagedGroups).Set("ManagedProducts", ManagedProducts).Set("Messages", Messages).Set("Payments", Payments).Set("Polls", Polls).Set("PollVotes", PollVotes).Set("PrivateTopics", PrivateTopics).Set("ProductBugReports", ProductBugReports).Set("ProductFeedbacks", ProductFeedbacks).Set("ProductGroupReferrals", ProductGroupReferrals).Set("ProductOrders", ProductOrders).Set("ProductOrdersVideo", ProductOrdersVideo).Set("ProductOrdersDevelopment", ProductOrdersDevelopment).Set("ProductOrdersSupport", ProductOrdersSupport).Set("ProductOrdersAccount", ProductOrdersAccount).Set("ProductOrdersTrial", ProductOrdersTrial).Set("ProductOrdersReferral", ProductOrdersReferral).Set("Products", Products).Set("ProfileVisits", ProfileVisits).Set("ReceivedMessageVotes", ReceivedMessageVotes).Set("ReferralClients", ReferralClients).Set("Roles2", Roles2).Set("Sessions", Sessions).Set("ShortUrlVisits", ShortUrlVisits).Set("Subscriptions", Subscriptions).Set("Suspicious", Suspicious).Set("Tags", Tags).Set("Topics", Topics).Set("Roles", Roles);
        }
    }
}
