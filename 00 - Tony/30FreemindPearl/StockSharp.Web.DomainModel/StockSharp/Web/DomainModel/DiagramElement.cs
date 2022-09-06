using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramElement : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TypeId { get; set; }

        public DiagramCategory Category { get; set; }

        public File Icon { get; set; }

        public BaseEntitySet<DiagramSocket> Sockets { get; set; }

        public BaseEntitySet<DiagramParam> Params { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name        = storage.GetValue("Name", (string)null);
            Description = storage.GetValue("Description", (string)null);
            TypeId      = storage.GetValue("TypeId", (string)null);
            Category    = storage.GetValue("Category", (DiagramCategory)null);
            Icon        = storage.GetValue("Icon", (File)null);
            Sockets     = storage.GetValue("Sockets", (BaseEntitySet<DiagramSocket>)null);
            Params      = storage.GetValue("Params", (BaseEntitySet<DiagramParam>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Description", Description).Set("TypeId", TypeId).Set("Category", Category).Set("Icon", Icon).Set("Sockets", Sockets).Set("Params", Params);
        }
    }
}
