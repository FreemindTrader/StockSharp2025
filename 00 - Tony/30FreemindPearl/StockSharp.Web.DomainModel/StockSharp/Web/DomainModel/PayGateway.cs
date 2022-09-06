
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PayGateway : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public File Logo { get; set; }

        public string IpAddressList { get; set; }

        public string Email { get; set; }

        public bool IsRepeatSupported { get; set; }

        public BaseEntitySet<PayGatewayDomain> Domains { get; set; }

        public BaseEntitySet<Payment> Payments { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Name              = storage.GetValue("Name", (string)null);
            Code              = storage.GetValue("Code", (string)null);
            Type              = storage.GetValue("Type", (string)null);
            Logo              = storage.GetValue("Logo", (File)null);
            IpAddressList     = storage.GetValue("IpAddressList", (string)null);
            Email             = storage.GetValue("Email", (string)null);
            IsRepeatSupported = storage.GetValue("IsRepeatSupported", false);
            Domains           = storage.GetValue("Domains", (BaseEntitySet<PayGatewayDomain>)null);
            Payments          = storage.GetValue("Payments", (BaseEntitySet<Payment>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Code", Code).Set("Type", Type).Set("Logo", Logo).Set("IpAddressList", IpAddressList).Set("Email", Email).Set("IsRepeatSupported", IsRepeatSupported).Set("Domains", Domains).Set("Payments", Payments);
        }
    }
}
