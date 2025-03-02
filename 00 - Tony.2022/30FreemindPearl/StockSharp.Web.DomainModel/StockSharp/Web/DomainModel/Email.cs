
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Email : BaseEntity
    {
        public string FromAddress { get; set; }

        public string FromAlias { get; set; }

        public string ToAddress { get; set; }

        public string ToAlias { get; set; }

        public string Subject { get; set; }

        public string BodyPlain { get; set; }

        public string BodyHtml { get; set; }

        public int ErrorCount { get; set; }

        public BaseEntitySet<File> Files { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            FromAddress = storage.GetValue("FromAddress", (string)null);
            FromAlias   = storage.GetValue("FromAlias", (string)null);
            ToAddress   = storage.GetValue("ToAddress", (string)null);
            ToAlias     = storage.GetValue("ToAlias", (string)null);
            Subject     = storage.GetValue("Subject", (string)null);
            BodyPlain   = storage.GetValue("BodyPlain", (string)null);
            BodyHtml    = storage.GetValue("BodyHtml", (string)null);
            ErrorCount  = storage.GetValue("ErrorCount", 0);
            Files       = storage.GetValue("Files", (BaseEntitySet<File>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("FromAddress", FromAddress).Set("FromAlias", FromAlias).Set("ToAddress", ToAddress).Set("ToAlias", ToAlias).Set("Subject", Subject).Set("BodyPlain", BodyPlain).Set("BodyHtml", BodyHtml).Set("ErrorCount", ErrorCount).Set("Files", Files);
        }
    }
}
