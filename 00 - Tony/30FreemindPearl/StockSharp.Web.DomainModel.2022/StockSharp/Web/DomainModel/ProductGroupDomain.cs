
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductGroupDomain : ProductDomainBase
    {
        public ProductGroup Group { get; set; }

        public string Name { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Group = storage.GetValue("Group", (ProductGroup)null);
            Name = storage.GetValue("Name", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Group", Group).Set("Name", Name);
        }
    }
}
