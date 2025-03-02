using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramSocket : BaseEntity
    {
        public string Name { get; set; }

        public bool Direction { get; set; }

        public int MaxLinks { get; set; }

        public DiagramElement Element { get; set; }

        public DiagramSocketType Type { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name      = storage.GetValue("Name", (string)null);
            Direction = storage.GetValue("Direction", false);
            MaxLinks  = storage.GetValue("MaxLinks", 0);
            Element   = storage.GetValue("Element", (DiagramElement)null);
            Type      = storage.GetValue("Type", (DiagramSocketType)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Direction", Direction).Set("MaxLinks", MaxLinks).Set("Element", Element).Set("Type", Type);
        }
    }
}
