using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramStrategy : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public File Body { get; set; }

        public BaseEntitySet<DiagramStrategyParam> Params { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name        = storage.GetValue("Name", (string)null);
            Description = storage.GetValue("Description", (string)null);
            Body        = storage.GetValue("Body", (File)null);
            Params      = storage.GetValue("Params", (BaseEntitySet<DiagramStrategyParam>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Description", Description).Set("Body", Body).Set("Params", Params);
        }
    }
}
