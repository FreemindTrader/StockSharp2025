
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class MessageHistory : BaseEntity
    {
        public Message Message { get; set; }

        public string PrevBody { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Message  = storage.GetValue("Message", (Message)null);
            PrevBody = storage.GetValue("PrevBody", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Message", Message).Set("PrevBody", PrevBody);
        }
    }
}
