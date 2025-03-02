using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class AccountRequisites : BaseEntity
    {
        public string BankId { get; set; }

        public string BankName { get; set; }

        public string AccountHolder { get; set; }

        public string AccountNo { get; set; }

        public Client Client { get; set; }

        public BaseEntitySet<Payment> Payments { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            BankId        = storage.GetValue("BankId", (string)null);
            BankName      = storage.GetValue("BankName", (string)null);
            AccountHolder = storage.GetValue("AccountHolder", (string)null);
            AccountNo     = storage.GetValue("AccountNo", (string)null);
            Client        = storage.GetValue("Client", (Client)null);
            Payments      = storage.GetValue("Payments", (BaseEntitySet<Payment>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("BankId", BankId).Set("BankName", BankName).Set("AccountHolder", AccountHolder).Set("AccountNo", AccountNo).Set("Client", Client).Set("Payments", Payments);
        }
    }
}
