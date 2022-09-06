
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class KeySecret : IPersistable
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public void Load(SettingsStorage storage)
        {
            Key = storage.GetValue("Key", (string)null);
            Secret = storage.GetValue("Secret", (string)null);
        }

        public void Save(SettingsStorage storage)
        {
            storage.Set("Key", Key).Set("Secret", Secret);
        }
    }
}
