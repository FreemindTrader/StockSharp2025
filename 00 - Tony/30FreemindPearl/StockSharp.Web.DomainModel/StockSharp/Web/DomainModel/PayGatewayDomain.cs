
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PayGatewayDomain : BaseEntity
    {
        public Domain Domain { get; set; }

        public PayGateway Gateway { get; set; }

        public bool IsEnabled { get; set; }

        public string SupportedCurrencies { get; set; }

        public PayGatewayDomainSettings Demo { get; set; }

        public PayGatewayDomainSettings Real { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Domain              = storage.GetValue("Domain", (Domain)null);
            Gateway             = storage.GetValue("Gateway", (PayGateway)null);
            IsEnabled           = storage.GetValue("IsEnabled", false);
            SupportedCurrencies = storage.GetValue("SupportedCurrencies", (string)null);
            Demo                = storage.GetValue("Demo", (PayGatewayDomainSettings)null);
            Real                = storage.GetValue("Real", (PayGatewayDomainSettings)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Domain", Domain).Set("Gateway", Gateway).Set("IsEnabled", IsEnabled).Set("SupportedCurrencies", SupportedCurrencies).Set("Demo", Demo).Set("Real", Real);
        }
    }
}
