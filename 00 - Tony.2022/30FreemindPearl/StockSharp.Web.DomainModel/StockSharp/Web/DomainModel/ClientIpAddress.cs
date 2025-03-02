using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ClientIpAddress : BaseEntity
    {
        public string IpAddress { get; set; }

        public bool IsRegistration { get; set; }

        public Client Client { get; set; }

        public License License { get; set; }

        public Payment Payment { get; set; }

        public ProductOrder ProductOrder { get; set; }

        public ProductGroup ProductGroup { get; set; }

        public ShortUrlVisit ShortUrlVisit { get; set; }

        public Message Message { get; set; }

        public PollVote PollVote { get; set; }

        public File File { get; set; }

        public FileDownload FileDownload { get; set; }

        public MessageVote MessageVote { get; set; }

        public TopicVisit TopicVisit { get; set; }

        public ProfileVisit ProfileVisit { get; set; }

        public ProductFeedback ProductFeedback { get; set; }

        public Session Session { get; set; }

        public FileGroup FileGroup { get; set; }

        public Product Product { get; set; }

        public EmailTemplate EmailTemplate { get; set; }

        public TopicGroup TopicGroup { get; set; }

        public FileShare FileShare { get; set; }

        public ClientBalanceHistory ClientBalanceHistory { get; set; }

        public Favorite Favorite { get; set; }

        public AccountRequisites AccountRequisites { get; set; }

        public MessageHistory MessageHistory { get; set; }

        public ShortUrl ShortUrl { get; set; }

        public MessagePatch MessagePatch { get; set; }

        public FileShareVisit FileShareVisit { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            IpAddress            = storage.GetValue("IpAddress", (string)null);
            IsRegistration       = storage.GetValue("IsRegistration", false);
            Client               = storage.GetValue("Client", (Client)null);
            License              = storage.GetValue("License", (License)null);
            Payment              = storage.GetValue("Payment", (Payment)null);
            ProductOrder         = storage.GetValue("ProductOrder", (ProductOrder)null);
            ProductGroup         = storage.GetValue("ProductGroup", (ProductGroup)null);
            ShortUrlVisit        = storage.GetValue("ShortUrlVisit", (ShortUrlVisit)null);
            Message              = storage.GetValue("Message", (Message)null);
            PollVote             = storage.GetValue("PollVote", (PollVote)null);
            File                 = storage.GetValue("File", (File)null);
            FileDownload         = storage.GetValue("FileDownload", (FileDownload)null);
            MessageVote          = storage.GetValue("MessageVote", (MessageVote)null);
            TopicVisit           = storage.GetValue("TopicVisit", (TopicVisit)null);
            ProfileVisit         = storage.GetValue("ProfileVisit", (ProfileVisit)null);
            ProductFeedback      = storage.GetValue("ProductFeedback", (ProductFeedback)null);
            Session              = storage.GetValue("Session", (Session)null);
            FileGroup            = storage.GetValue("FileGroup", (FileGroup)null);
            Product              = storage.GetValue("Product", (Product)null);
            EmailTemplate        = storage.GetValue("EmailTemplate", (EmailTemplate)null);
            TopicGroup           = storage.GetValue("TopicGroup", (TopicGroup)null);
            FileShare            = storage.GetValue("FileShare", (FileShare)null);
            ClientBalanceHistory = storage.GetValue("ClientBalanceHistory", (ClientBalanceHistory)null);
            Favorite             = storage.GetValue("Favorite", (Favorite)null);
            AccountRequisites    = storage.GetValue("AccountRequisites", (AccountRequisites)null);
            MessageHistory       = storage.GetValue("MessageHistory", (MessageHistory)null);
            ShortUrl             = storage.GetValue("ShortUrl", (ShortUrl)null);
            MessagePatch         = storage.GetValue("MessagePatch", (MessagePatch)null);
            FileShareVisit       = storage.GetValue("FileShareVisit", (FileShareVisit)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("IpAddress", IpAddress).Set("IsRegistration", IsRegistration).Set("Client", Client).Set("License", License).Set("Payment", Payment).Set("ProductOrder", ProductOrder).Set("ProductGroup", ProductGroup).Set("ShortUrlVisit", ShortUrlVisit).Set("Message", Message).Set("PollVote", PollVote).Set("File", File).Set("FileDownload", FileDownload).Set("MessageVote", MessageVote).Set("TopicVisit", TopicVisit).Set("ProfileVisit", ProfileVisit).Set("ProductFeedback", ProductFeedback).Set("Session", Session).Set("FileGroup", FileGroup).Set("Product", Product).Set("EmailTemplate", EmailTemplate).Set("TopicGroup", TopicGroup).Set("FileShare", FileShare).Set("ClientBalanceHistory", ClientBalanceHistory).Set("Favorite", Favorite).Set("AccountRequisites", AccountRequisites).Set("MessageHistory", MessageHistory).Set("ShortUrl", ShortUrl).Set("MessagePatch", MessagePatch).Set("FileShareVisit", FileShareVisit);
        }
    }
}
