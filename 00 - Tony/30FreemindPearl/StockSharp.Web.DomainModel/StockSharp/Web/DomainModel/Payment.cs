using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Payment : BaseEntity
    {
        public string Details { get; set; }

        public Decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Client From { get; set; }

        public Client To { get; set; }

        public string PaySystemId { get; set; }

        public PaymentStates State { get; set; }

        public CurrencyTypes Currency { get; set; }

        public AccountRequisites BankAccount { get; set; }

        public ProductOrder ProductOrder { get; set; }

        public bool IsTest { get; set; }

        public PayGatewayDomain GatewayDomain { get; set; }

        public bool IsRecurrent { get; set; }

        public bool IsConfirmRequired { get; set; }

        public string StatusUrlPart { get; set; }

        public bool? IsApproveAllow { get; set; }

        public bool? IsCancelAllow { get; set; }

        public bool? IsAutoActionsAllow { get; set; }

        public FileGroup Files { get; set; }

        public BaseEntitySet<Error> Errors { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Details            = storage.GetValue("Details", (string)null);
            Amount             = storage.GetValue("Amount", Decimal.Zero);
            Date               = storage.GetValue("Date", new DateTime());
            From               = storage.GetValue("From", (Client)null);
            To                 = storage.GetValue("To", (Client)null);
            PaySystemId        = storage.GetValue("PaySystemId", (string)null);
            State              = storage.GetValue("State", PaymentStates.Done);
            Currency           = storage.GetValue("Currency", CurrencyTypes.AFA);
            BankAccount        = storage.GetValue("BankAccount", (AccountRequisites)null);
            ProductOrder       = storage.GetValue("ProductOrder", (ProductOrder)null);
            IsTest             = storage.GetValue("IsTest", false);
            GatewayDomain      = storage.GetValue("GatewayDomain", (PayGatewayDomain)null);
            IsRecurrent        = storage.GetValue("IsRecurrent", false);
            IsConfirmRequired  = storage.GetValue("IsConfirmRequired", false);
            StatusUrlPart      = storage.GetValue("StatusUrlPart", (string)null);
            IsApproveAllow     = storage.GetValue("IsApproveAllow", new bool?());
            IsCancelAllow      = storage.GetValue("IsCancelAllow", new bool?());
            IsAutoActionsAllow = storage.GetValue("IsAutoActionsAllow", new bool?());
            Files              = storage.GetValue("Files", (FileGroup)null);
            Errors             = storage.GetValue("Errors", (BaseEntitySet<Error>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Details", Details).Set("Amount", Amount).Set("Date", Date).Set("From", From).Set("To", To).Set("PaySystemId", PaySystemId).Set("State", State).Set("Currency", Currency).Set("BankAccount", BankAccount).Set("ProductOrder", ProductOrder).Set("IsTest", IsTest).Set("GatewayDomain", GatewayDomain).Set("IsRecurrent", IsRecurrent).Set("IsConfirmRequired", IsConfirmRequired).Set("StatusUrlPart", StatusUrlPart).Set("IsApproveAllow", IsApproveAllow).Set("IsCancelAllow", IsCancelAllow).Set("IsAutoActionsAllow", IsAutoActionsAllow).Set("Files", Files).Set("Errors", Errors);
        }
    }
}
