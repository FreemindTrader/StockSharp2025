using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramSocketType : BaseEntity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Color { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name  = storage.GetValue("Name", (string)null);
            Type  = storage.GetValue("Type", (string)null);
            Color = storage.GetValue("Color", 0);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Type", Type).Set("Color", Color);
        }
    }
}
