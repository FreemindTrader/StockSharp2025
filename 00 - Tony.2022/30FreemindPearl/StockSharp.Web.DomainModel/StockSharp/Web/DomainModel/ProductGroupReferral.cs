
using Ecng.Serialization;
using StockSharp.Messages;

namespace StockSharp.Web.DomainModel
{
    public class ProductGroupReferral : BaseEntity
    {
        public ProductGroup Group { get; set; }

        public Client Referral { get; set; }

        public string Comment { get; set; }

        public Unit Reward { get; set; }

        public BaseEntitySet<ProductGroupReferralDomain> Domains { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Group    = storage.GetValue("Group", (ProductGroup)null);
            Referral = storage.GetValue("Referral", (Client)null);
            Comment  = storage.GetValue("Comment", (string)null);
            Reward   = storage.GetValue("Reward", (Unit)null);
            Domains  = storage.GetValue("Domains", (BaseEntitySet<ProductGroupReferralDomain>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Group", Group).Set("Referral", Referral).Set("Comment", Comment).Set("Reward", Reward).Set("Domains", Domains);
        }
    }
}
