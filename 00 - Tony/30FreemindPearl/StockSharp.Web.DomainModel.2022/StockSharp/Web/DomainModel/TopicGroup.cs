
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class TopicGroup : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public BaseEntitySet<Client> RolesRead { get; set; }

        public BaseEntitySet<Client> RolesWrite { get; set; }

        public BaseEntitySet<Topic> Topics { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Name        = storage.GetValue("Name", (string)null);
            Description = storage.GetValue("Description", (string)null);
            RolesRead   = storage.GetValue("RolesRead", (BaseEntitySet<Client>)null);
            RolesWrite  = storage.GetValue("RolesWrite", (BaseEntitySet<Client>)null);
            Topics      = storage.GetValue("Topics", (BaseEntitySet<Topic>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Description", Description).Set("RolesRead", RolesRead).Set("RolesWrite", RolesWrite).Set("Topics", Topics);
        }
    }
}
