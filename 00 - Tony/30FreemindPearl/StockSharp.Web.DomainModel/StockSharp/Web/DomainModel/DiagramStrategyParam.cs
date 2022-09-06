using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramStrategyParam : BaseEntity
    {
        public DiagramStrategy Strategy { get; set; }

        public DiagramParam Param { get; set; }

        public string Value { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Strategy = storage.GetValue("Strategy", (DiagramStrategy)null);
            Param    = storage.GetValue("Param", (DiagramParam)null);
            Value    = storage.GetValue("Value", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Strategy", Strategy).Set("Param", Param).Set("Value", Value);
        }
    }
}
