using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ClientBalanceHistory : BaseEntity
    {
        public Payment Payment { get; set; }

        public ClientBalance Balance { get; set; }

        public Decimal Diff { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Payment = storage.GetValue("Payment", (Payment)null);
            Balance = storage.GetValue("Balance", (ClientBalance)null);
            Diff    = storage.GetValue("Diff", Decimal.Zero);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Payment", Payment).Set("Balance", Balance).Set("Diff", Diff);
        }
    }
}
