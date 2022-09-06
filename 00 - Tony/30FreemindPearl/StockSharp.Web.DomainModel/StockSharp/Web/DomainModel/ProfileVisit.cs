
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProfileVisit : BaseEntity
    {
        public Client Profile { get; set; }

        public Client Client { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Profile = storage.GetValue("Profile", (Client)null);
            Client = storage.GetValue("Client", (Client)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Profile", Profile).Set("Client", Client);
        }
    }
}
