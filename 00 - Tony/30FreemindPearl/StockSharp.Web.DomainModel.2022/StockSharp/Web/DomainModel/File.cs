using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class File : BaseEntity
    {
        public string Name { get; set; }

        public Client Client { get; set; }

        public bool IsCloud { get; set; }

        public string Hash { get; set; }

        public File Root { get; set; }

        public long BodyLength { get; set; }

        public DateTime Till { get; set; }

        public string UrlRelative { get; set; }

        public bool AsAvatar { get; set; }

        public string PageId { get; set; }

        public string Source { get; set; }

        public Message Message { get; set; }

        public BaseEntitySet<FileDownload> Downloads { get; set; }

        public BaseEntitySet<FileGroup> Groups { get; set; }

        public BaseEntitySet<File> History { get; set; }

        public BaseEntitySet<FileShare> Shares { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Name        = storage.GetValue("Name", (string)null);
            Client      = storage.GetValue("Client", (Client)null);
            IsCloud     = storage.GetValue("IsCloud", false);
            Hash        = storage.GetValue("Hash", (string)null);
            Root        = storage.GetValue("Root", (File)null);
            BodyLength  = storage.GetValue("BodyLength", 0L);
            Till        = storage.GetValue("Till", new DateTime());
            UrlRelative = storage.GetValue("UrlRelative", (string)null);
            AsAvatar    = storage.GetValue("AsAvatar", false);
            PageId      = storage.GetValue("PageId", (string)null);
            Source      = storage.GetValue("Source", (string)null);
            Message     = storage.GetValue("Message", (Message)null);
            Downloads   = storage.GetValue("Downloads", (BaseEntitySet<FileDownload>)null);
            Groups      = storage.GetValue("Groups", (BaseEntitySet<FileGroup>)null);
            History     = storage.GetValue("History", (BaseEntitySet<File>)null);
            Shares      = storage.GetValue("Shares", (BaseEntitySet<FileShare>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Client", Client).Set("IsCloud", IsCloud).Set("Hash", Hash).Set("Root", Root).Set("BodyLength", BodyLength).Set("Till", Till).Set("UrlRelative", UrlRelative).Set("AsAvatar", AsAvatar).Set("PageId", PageId).Set("Source", Source).Set("Message", Message).Set("Downloads", Downloads).Set("Groups", Groups).Set("History", History).Set("Shares", Shares);
        }
    }
}
