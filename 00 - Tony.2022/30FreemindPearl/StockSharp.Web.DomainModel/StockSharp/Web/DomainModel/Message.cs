
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Message : BaseEntity
    {
        public string Body { get; set; }

        public Message Parent { get; set; }

        public Topic Topic { get; set; }

        public Client Client { get; set; }

        public string UrlRelative { get; set; }

        public string PageId { get; set; }

        public bool? AllowHtml { get; set; }

        public bool? AllowEdit { get; set; }

        public bool? AllowSelectExecutor { get; set; }

        public bool? IsSelectedAsExecutor { get; set; }

        public bool? AllowThank { get; set; }

        public string BodyHtml { get; set; }

        public int? PageNum { get; set; }

        public BaseEntitySet<File> Attachments { get; set; }

        public BaseEntitySet<MessageHistory> History { get; set; }

        public BaseEntitySet<MessagePatch> Patches { get; set; }

        public BaseEntitySet<MessageVote> Votes { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Body                 = storage.GetValue("Body", (string)null);
            Parent               = storage.GetValue("Parent", (Message)null);
            Topic                = storage.GetValue("Topic", (Topic)null);
            Client               = storage.GetValue("Client", (Client)null);
            UrlRelative          = storage.GetValue("UrlRelative", (string)null);
            PageId               = storage.GetValue("PageId", (string)null);
            AllowHtml            = storage.GetValue("AllowHtml", new bool?());
            AllowEdit            = storage.GetValue("AllowEdit", new bool?());
            AllowSelectExecutor  = storage.GetValue("AllowSelectExecutor", new bool?());
            IsSelectedAsExecutor = storage.GetValue("IsSelectedAsExecutor", new bool?());
            AllowThank           = storage.GetValue("AllowThank", new bool?());
            BodyHtml             = storage.GetValue("BodyHtml", (string)null);
            PageNum              = storage.GetValue("PageNum", new int?());
            Attachments          = storage.GetValue("Attachments", (BaseEntitySet<File>)null);
            History              = storage.GetValue("History", (BaseEntitySet<MessageHistory>)null);
            Patches              = storage.GetValue("Patches", (BaseEntitySet<MessagePatch>)null);
            Votes                = storage.GetValue("Votes", (BaseEntitySet<MessageVote>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Body", Body).Set("Parent", Parent).Set("Topic", Topic).Set("Client", Client).Set("UrlRelative", UrlRelative).Set("PageId", PageId).Set("AllowHtml", AllowHtml).Set("AllowEdit", AllowEdit).Set("AllowSelectExecutor", AllowSelectExecutor).Set("IsSelectedAsExecutor", IsSelectedAsExecutor).Set("AllowThank", AllowThank).Set("BodyHtml", BodyHtml).Set("PageNum", PageNum).Set("Attachments", Attachments).Set("History", History).Set("Patches", Patches).Set("Votes", Votes);
        }
    }
}
