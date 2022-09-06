using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramCategory : BaseEntity
    {
        public string Name { get; set; }

        public DiagramCategory Parent { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name = storage.GetValue("Name", (string)null);
            Parent = storage.GetValue("Parent", (DiagramCategory)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Parent", Parent);
        }
    }
}
