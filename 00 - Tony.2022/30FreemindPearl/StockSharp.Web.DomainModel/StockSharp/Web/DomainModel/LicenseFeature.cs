
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class LicenseFeature : BaseEntity
    {
        public string Name { get; set; }

        public BaseEntitySet<License> Licenses { get; set; }

        public BaseEntitySet<Client> Roles { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name     = storage.GetValue("Name", (string)null);
            Licenses = storage.GetValue("Licenses", (BaseEntitySet<License>)null);
            Roles    = storage.GetValue("Roles", (BaseEntitySet<Client>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Licenses", Licenses).Set("Roles", Roles);
        }
    }
}
