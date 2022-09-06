
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class LongUrl : BaseEntity
    {
        public string Text { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Text = storage.GetValue("Text", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Text", Text);
        }
    }
}
