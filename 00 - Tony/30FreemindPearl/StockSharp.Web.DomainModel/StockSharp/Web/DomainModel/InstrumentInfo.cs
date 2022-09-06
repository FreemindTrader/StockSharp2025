using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class InstrumentInfo : BaseEntity
    {
        public string Code { get; set; }

        public string Board { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Isin { get; set; }

        public string Asset { get; set; }

        public SecurityTypes? Type { get; set; }

        public DateTime? IssueDate { get; set; }

        public Decimal? IssueSize { get; set; }

        public DateTime? LastDate { get; set; }

        public int? Decimals { get; set; }

        public Decimal? Multiplier { get; set; }

        public Decimal? PriceStep { get; set; }

        public CurrencyTypes? Currency { get; set; }

        public DateTime? SettleDate { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Code       = storage.GetValue("Code", (string)null);
            Board      = storage.GetValue("Board", (string)null);
            Name       = storage.GetValue("Name", (string)null);
            ShortName  = storage.GetValue("ShortName", (string)null);
            Isin       = storage.GetValue("Isin", (string)null);
            Asset      = storage.GetValue("Asset", (string)null);
            Type       = storage.GetValue("Type", new SecurityTypes?());
            IssueDate  = storage.GetValue("IssueDate", new DateTime?());
            IssueSize  = storage.GetValue("IssueSize", new Decimal?());
            LastDate   = storage.GetValue("LastDate", new DateTime?());
            Decimals   = storage.GetValue("Decimals", new int?());
            Multiplier = storage.GetValue("Multiplier", new Decimal?());
            PriceStep  = storage.GetValue("PriceStep", new Decimal?());
            Currency   = storage.GetValue("Currency", new CurrencyTypes?());
            SettleDate = storage.GetValue("SettleDate", new DateTime?());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Code", Code).Set("Board", Board).Set("Name", Name).Set("ShortName", ShortName).Set("Isin", Isin).Set("Asset", Asset).Set("Type", Type).Set("IssueDate", IssueDate).Set("IssueSize", IssueSize).Set("LastDate", LastDate).Set("Decimals", Decimals).Set("Multiplier", Multiplier).Set("PriceStep", PriceStep).Set("Currency", Currency).Set("SettleDate", SettleDate);
        }
    }
}
