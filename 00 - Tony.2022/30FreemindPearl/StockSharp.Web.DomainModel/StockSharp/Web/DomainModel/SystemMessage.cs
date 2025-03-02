
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class SystemMessage : Message
    {
        public string Source { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Source = storage.GetValue("Source", (string)null);
            ExpiryDate = storage.GetValue("ExpiryDate", new DateTime?());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Source", Source).Set("ExpiryDate", ExpiryDate);
        }
    }
}
