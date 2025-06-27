// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductGroupReferral
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductGroupReferral : BaseEntity, IDescriptionEntity, INameEntity, IDomainsEntity<ProductGroupReferralDomain>
    {
        private string _name;

        public ProductGroup Group { get; set; }

        public Client Referral { get; set; }

        public string Description { get; set; }

        public Price Reward { get; set; }

        public BaseEntitySet<ProductGroupReferralDomain> Domains { get; set; }

        string INameEntity.Name
        {
            get
            {
                return StringHelper.IsEmpty( this._name, ( ( INameEntity ) this.Group )?.Name );
            }
            set
            {
                this._name = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Group = ( ProductGroup ) storage.GetValue<ProductGroup>( "Group", null );
            this.Referral = ( Client ) storage.GetValue<Client>( "Referral", null );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.Reward = ( Price ) storage.GetValue<Price>( "Reward", null );
            this.Domains = ( BaseEntitySet<ProductGroupReferralDomain> ) storage.GetValue<BaseEntitySet<ProductGroupReferralDomain>>( "Domains", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<ProductGroup>( "Group", this.Group ).Set<Client>( "Referral", this.Referral ).Set<string>( "Description", this.Description ).Set<Price>( "Reward", this.Reward ).Set<BaseEntitySet<ProductGroupReferralDomain>>( "Domains", this.Domains );
        }
    }
}
