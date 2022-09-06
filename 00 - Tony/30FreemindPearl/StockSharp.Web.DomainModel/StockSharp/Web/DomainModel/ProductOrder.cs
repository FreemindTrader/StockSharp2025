
using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductOrder : BaseEntity
    {
        public Product Product { get; set; }

        public Client Client { get; set; }

        public Decimal Amount { get; set; }

        public string Comment { get; set; }

        public DateTime? SubscriptionStart { get; set; }

        public DateTime? SubscriptionEnd { get; set; }

        public string HardwareId { get; set; }

        public string Account { get; set; }

        public Decimal RepeatAmount { get; set; }

        public string RepeatToken { get; set; }

        public CurrencyTypes Currency { get; set; }

        public Message Message { get; set; }

        public ProductGroupReferral Referral { get; set; }

        public Client ReferralClient { get; set; }

        public ProductPriceTypes PriceType { get; set; }

        public ProductOrderFlags Flags { get; set; }

        public string PayUrl { get; set; }

        public Product OneApp { get; set; }

        public BaseEntitySet<Payment> Payments { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Product           = storage.GetValue("Product", (Product)null);
            Client            = storage.GetValue("Client", (Client)null);
            Amount            = storage.GetValue("Amount", Decimal.Zero);
            Comment           = storage.GetValue("Comment", (string)null);
            SubscriptionStart = storage.GetValue("SubscriptionStart", new DateTime?());
            SubscriptionEnd   = storage.GetValue("SubscriptionEnd", new DateTime?());
            HardwareId        = storage.GetValue("HardwareId", (string)null);
            Account           = storage.GetValue("Account", (string)null);
            RepeatAmount      = storage.GetValue("RepeatAmount", Decimal.Zero);
            RepeatToken       = storage.GetValue("RepeatToken", (string)null);
            Currency          = storage.GetValue("Currency", CurrencyTypes.AFA);
            Message           = storage.GetValue("Message", (Message)null);
            Referral          = storage.GetValue("Referral", (ProductGroupReferral)null);
            ReferralClient    = storage.GetValue("ReferralClient", (Client)null);
            PriceType         = storage.GetValue("PriceType", ProductPriceTypes.Lifetime);
            Flags             = storage.GetValue("Flags", ProductOrderFlags.None);
            PayUrl            = storage.GetValue("PayUrl", (string)null);
            OneApp            = storage.GetValue("OneApp", (Product)null);
            Payments          = storage.GetValue("Payments", (BaseEntitySet<Payment>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Product", Product).Set("Client", Client).Set("Amount", Amount).Set("Comment", Comment).Set("SubscriptionStart", SubscriptionStart).Set("SubscriptionEnd", SubscriptionEnd).Set("HardwareId", HardwareId).Set("Account", Account).Set("RepeatAmount", RepeatAmount).Set("RepeatToken", RepeatToken).Set("Currency", Currency).Set("Message", Message).Set("Referral", Referral).Set("ReferralClient", ReferralClient).Set("PriceType", PriceType).Set("Flags", Flags).Set("PayUrl", PayUrl).Set("OneApp", OneApp).Set("Payments", Payments);
        }
    }
}
