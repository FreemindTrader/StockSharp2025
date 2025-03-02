using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ClientRole : BaseEntity
    {
        public Client Role { get; set; }

        public Product OneApp { get; set; }

        public DateTime? Till { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Role   = storage.GetValue("Role", (Client)null);
            OneApp = storage.GetValue("OneApp", (Product)null);
            Till   = storage.GetValue("Till", new DateTime?());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Role", Role).Set("OneApp", OneApp).Set("Till", Till);
        }
    }
}
