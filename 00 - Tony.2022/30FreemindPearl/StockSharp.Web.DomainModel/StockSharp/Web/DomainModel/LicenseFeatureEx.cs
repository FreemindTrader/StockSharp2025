
using Ecng.Serialization;
using StockSharp.Licensing;
using System;

namespace StockSharp.Web.DomainModel
{
    public class LicenseFeatureEx : BaseEntity
    {
        public License License { get; set; }

        public LicenseFeature Feature { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Account { get; set; }

        public string HardwareId { get; set; }

        public Product OneApp { get; set; }

        public string Platform { get; set; }

        public LicenseExpireActions ExpireAction { get; set; }

        public int RenewCount { get; set; }

        public int MaxRenewCount { get; set; }

        public Product Broker { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            License        = storage.GetValue("License", (License)null);
            Feature        = storage.GetValue("Feature", (LicenseFeature)null);
            ExpirationDate = storage.GetValue("ExpirationDate", new DateTime());
            Account        = storage.GetValue("Account", (string)null);
            HardwareId     = storage.GetValue("HardwareId", (string)null);
            OneApp         = storage.GetValue("OneApp", (Product)null);
            Platform       = storage.GetValue("Platform", (string)null);
            ExpireAction   = storage.GetValue("ExpireAction", LicenseExpireActions.PreventWork);
            RenewCount     = storage.GetValue("RenewCount", 0);
            MaxRenewCount  = storage.GetValue("MaxRenewCount", 0);
            Broker         = storage.GetValue("Broker", (Product)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("License", License).Set("Feature", Feature).Set("ExpirationDate", ExpirationDate).Set("Account", Account).Set("HardwareId", HardwareId).Set("OneApp", OneApp).Set("Platform", Platform).Set("ExpireAction", ExpireAction).Set("RenewCount", RenewCount).Set("MaxRenewCount", MaxRenewCount).Set("Broker", Broker);
        }
    }
}
