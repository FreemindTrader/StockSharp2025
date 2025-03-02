using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Draft : BaseEntity
    {
        public Client Client { get; set; }

        public string PageId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Tags { get; set; }

        public BaseEntitySet<File> Attachments { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Client      = storage.GetValue("Client", (Client)null);
            PageId      = storage.GetValue("PageId", (string)null);
            Title       = storage.GetValue("Title", (string)null);
            Body        = storage.GetValue("Body", (string)null);
            Tags        = storage.GetValue("Tags", (string)null);
            Attachments = storage.GetValue("Attachments", (BaseEntitySet<File>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("PageId", PageId).Set("Title", Title).Set("Body", Body).Set("Tags", Tags).Set("Attachments", Attachments);
        }
    }
}
