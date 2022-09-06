
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class License : BaseEntity
    {
        public Client Client { get; set; }

        public File File { get; set; }

        public string Comment { get; set; }

        public byte[] Body { get; set; }

        public BaseEntitySet<LicenseFeatureEx> Features { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Client   = storage.GetValue("Client", (Client)null);
            File     = storage.GetValue("File", (File)null);
            Comment  = storage.GetValue("Comment", (string)null);
            Body     = storage.GetValue("Body", (byte[])null);
            Features = storage.GetValue("Features", (BaseEntitySet<LicenseFeatureEx>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("File", File).Set("Comment", Comment).Set("Body", Body).Set("Features", Features);
        }
    }
}
