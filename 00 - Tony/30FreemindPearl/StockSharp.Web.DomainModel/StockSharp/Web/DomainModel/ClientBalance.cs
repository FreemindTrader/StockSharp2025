using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ClientBalance : BaseEntity
    {
        public Client Client { get; set; }

        public CurrencyTypes Currency { get; set; }

        public Decimal Amount { get; set; }

        public BaseEntitySet<ClientBalanceHistory> History { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Client   = storage.GetValue("Client", (Client)null);
            Currency = storage.GetValue("Currency", CurrencyTypes.AFA);
            Amount   = storage.GetValue("Amount", Decimal.Zero);
            History  = storage.GetValue("History", (BaseEntitySet<ClientBalanceHistory>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("Currency", Currency).Set("Amount", Amount).Set("History", History);
        }
    }
}
