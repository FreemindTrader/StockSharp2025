
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class IpBan : BaseEntity
    {
        public string IpAddress { get; set; }

        public string Reason { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            IpAddress = storage.GetValue("IpAddress", (string)null);
            Reason = storage.GetValue("Reason", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("IpAddress", IpAddress).Set("Reason", Reason);
        }
    }
}
