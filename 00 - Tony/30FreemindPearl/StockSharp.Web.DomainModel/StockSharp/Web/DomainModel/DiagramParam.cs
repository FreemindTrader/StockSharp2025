using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramParam : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public DiagramCategory Category { get; set; }

        public string DefaultValue { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name         = storage.GetValue("Name", (string)null);
            Description  = storage.GetValue("Description", (string)null);
            Type         = storage.GetValue("Type", (string)null);
            Category     = storage.GetValue("Category", (DiagramCategory)null);
            DefaultValue = storage.GetValue("DefaultValue", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Description", Description).Set("Type", Type).Set("Category", Category).Set("DefaultValue", DefaultValue);
        }
    }
}
