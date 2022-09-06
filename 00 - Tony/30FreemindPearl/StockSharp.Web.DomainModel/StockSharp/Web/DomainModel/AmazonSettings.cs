using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class AmazonSettings : IPersistable
    {
        public string Endpoint { get; set; }

        public string Bucket { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public void Load(SettingsStorage storage)
        {
            Endpoint  = storage.GetValue("Endpoint", (string)null);
            Bucket    = storage.GetValue("Bucket", (string)null);
            AccessKey = storage.GetValue("AccessKey", (string)null);
            SecretKey = storage.GetValue("SecretKey", (string)null);
        }

        public void Save(SettingsStorage storage)
        {
            storage.Set("Endpoint", Endpoint).Set("Bucket", Bucket).Set("AccessKey", AccessKey).Set("SecretKey", SecretKey);
        }
    }
}
