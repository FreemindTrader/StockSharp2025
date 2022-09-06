
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PayGatewayDomainSettings : IPersistable
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Url { get; set; }

        public string Token { get; set; }

        public File Certificate { get; set; }

        public void Load(SettingsStorage storage)
        {
            Login       = storage.GetValue("Login", (string)null);
            Password    = storage.GetValue("Password", (string)null);
            Url         = storage.GetValue("Url", (string)null);
            Token       = storage.GetValue("Token", (string)null);
            Certificate = storage.GetValue("Certificate", (File)null);
        }

        public void Save(SettingsStorage storage)
        {
            storage.Set("Login", Login).Set("Password", Password).Set("Url", Url).Set("Token", Token).Set("Certificate", Certificate);
        }
    }
}
