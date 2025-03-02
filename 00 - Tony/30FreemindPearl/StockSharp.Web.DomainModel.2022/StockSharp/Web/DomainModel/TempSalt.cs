
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class TempSalt : BaseEntity
    {
        public byte[] Salt { get; set; }

        public byte[] IV { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Salt = storage.GetValue("Salt", (byte[])null);
            IV = storage.GetValue("IV", (byte[])null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Salt", Salt).Set("IV", IV);
        }
    }
}
