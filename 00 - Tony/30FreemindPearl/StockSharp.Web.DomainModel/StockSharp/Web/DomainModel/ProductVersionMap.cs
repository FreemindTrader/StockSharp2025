
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductVersionMap : BaseEntity
    {
        public Product Product { get; set; }

        public string RealVersion { get; set; }

        public string StubVersion { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Product     = storage.GetValue("Product", (Product)null);
            RealVersion = storage.GetValue("RealVersion", (string)null);
            StubVersion = storage.GetValue("StubVersion", (string)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Product", Product).Set("RealVersion", RealVersion).Set("StubVersion", StubVersion);
        }
    }
}
