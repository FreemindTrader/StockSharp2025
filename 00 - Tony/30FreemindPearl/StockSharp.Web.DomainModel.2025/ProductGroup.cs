// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.DomainModel
{
    public class ProductGroup : ProductBase, INameEntity, IDescriptionEntity, IDomainsEntity<ProductGroupDomain>
    {
        private string _name;

        public string Description { get; set; }

        public BaseEntitySet<ProductGroupDomain> Domains { get; set; }

        public BaseEntitySet<ProductGroup> Child { get; set; }

        public BaseEntitySet<ProductGroup> Parent { get; set; }

        public BaseEntitySet<Product> Products { get; set; }

        public BaseEntitySet<ProductGroupReferral> Referral { get; set; }

        string INameEntity.Name
        {
            get
            {
                string name = this._name;
                BaseEntitySet<ProductGroupDomain> domains = this.Domains;
                string str;
                if ( domains == null )
                {
                    str = ( string ) null;
                }
                else
                {
                    ProductGroupDomain[] items = domains.Items;
                    str = items != null ? ( ( IEnumerable<ProductGroupDomain> ) items ).FirstOrDefault<ProductGroupDomain>()?.Name : ( string ) null;
                }
                return StringHelper.IsEmpty( name, str );
            }
            set
            {
                this._name = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.Domains = ( BaseEntitySet<ProductGroupDomain> ) storage.GetValue<BaseEntitySet<ProductGroupDomain>>( "Domains", null );
            this.Child = ( BaseEntitySet<ProductGroup> ) storage.GetValue<BaseEntitySet<ProductGroup>>( "Child", null );
            this.Parent = ( BaseEntitySet<ProductGroup> ) storage.GetValue<BaseEntitySet<ProductGroup>>( "Parent", null );
            this.Products = ( BaseEntitySet<Product> ) storage.GetValue<BaseEntitySet<Product>>( "Products", null );
            this.Referral = ( BaseEntitySet<ProductGroupReferral> ) storage.GetValue<BaseEntitySet<ProductGroupReferral>>( "Referral", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Description", this.Description ).Set<BaseEntitySet<ProductGroupDomain>>( "Domains", this.Domains ).Set<BaseEntitySet<ProductGroup>>( "Child", this.Child ).Set<BaseEntitySet<ProductGroup>>( "Parent", this.Parent ).Set<BaseEntitySet<Product>>( "Products", this.Products ).Set<BaseEntitySet<ProductGroupReferral>>( "Referral", this.Referral );
        }
    }
}
