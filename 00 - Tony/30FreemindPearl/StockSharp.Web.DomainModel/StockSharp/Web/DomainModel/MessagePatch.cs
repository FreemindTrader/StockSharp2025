
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class MessagePatch : BaseEntity
    {
        public Message Message { get; set; }

        public string PackageId { get; set; }

        public string PackageUrl { get; set; }

        public string Version { get; set; }

        public string Notes { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Message    = storage.GetValue("Message", (Message)null);
            PackageId  = storage.GetValue("PackageId", (string)null);
            PackageUrl = storage.GetValue("PackageUrl", (string)null);
            Version    = storage.GetValue("Version", (string)null);
            Notes      = storage.GetValue("Notes", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Message", Message).Set("PackageId", PackageId).Set("PackageUrl", PackageUrl).Set("Version", Version).Set("Notes", Notes);
        }
    }
}
