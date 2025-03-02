
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class EmailTemplateDomain : BaseEntity
    {
        public EmailTemplate Template { get; set; }

        public Domain Domain { get; set; }

        public Topic Content { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Template = storage.GetValue("Template", (EmailTemplate)null);
            Domain   = storage.GetValue("Domain", (Domain)null);
            Content  = storage.GetValue("Content", (Topic)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Template", Template).Set("Domain", Domain).Set("Content", Content);
        }
    }
}
