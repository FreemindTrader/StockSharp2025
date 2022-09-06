
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductFeedback : ProductBugReport
    {
        public int Rate { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Rate = storage.GetValue("Rate", 0);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Rate", Rate);
        }
    }
}
