// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicMenu
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.DomainModel
{
    public class DynamicMenu : BaseEntity, IProductEntity, INameEntity, IDomainsEntity<DynamicMenuDomain>
    {
        private string _name;

        public bool IsBlank { get; set; }

        public DynamicMenuGroup Group { get; set; }

        public int SortOrder { get; set; }

        public string Style { get; set; }

        public DynamicPage Page { get; set; }

        public Product Product { get; set; }

        public BaseEntitySet<DynamicMenuDomain> Domains { get; set; }

        public BaseEntitySet<Client> Roles { get; set; }

        string INameEntity.Name
        {
            get
            {
                string name = this._name;
                BaseEntitySet<DynamicMenuDomain> domains1 = this.Domains;
                string str1;
                if ( domains1 == null )
                {
                    str1 = ( string ) null;
                }
                else
                {
                    DynamicMenuDomain[] items = domains1.Items;
                    str1 = items != null ? ( ( IEnumerable<DynamicMenuDomain> ) items ).FirstOrDefault<DynamicMenuDomain>()?.UrlAbsolute : ( string ) null;
                }
                string str2 = StringHelper.IsEmpty(name, str1);
                BaseEntitySet<DynamicMenuDomain> domains2 = this.Domains;
                string str3;
                if ( domains2 == null )
                {
                    str3 = ( string ) null;
                }
                else
                {
                    DynamicMenuDomain[] items = domains2.Items;
                    str3 = items != null ? ( ( IEnumerable<DynamicMenuDomain> ) items ).FirstOrDefault<DynamicMenuDomain>()?.UrlRelative : ( string ) null;
                }
                return StringHelper.IsEmpty( StringHelper.IsEmpty( str2, str3 ), ( string ) Converter.To<string>( ( object ) this.Id ) );
            }
            set
            {
                this._name = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.IsBlank = ( bool ) storage.GetValue<bool>( "IsBlank", false );
            this.Group = ( DynamicMenuGroup ) storage.GetValue<DynamicMenuGroup>( "Group", null );
            this.SortOrder = ( int ) storage.GetValue<int>( "SortOrder", 0 );
            this.Style = ( string ) storage.GetValue<string>( "Style", null );
            this.Page = ( DynamicPage ) storage.GetValue<DynamicPage>( "Page", null );
            this.Product = ( Product ) storage.GetValue<Product>( "Product", null );
            this.Domains = ( BaseEntitySet<DynamicMenuDomain> ) storage.GetValue<BaseEntitySet<DynamicMenuDomain>>( "Domains", null );
            this.Roles = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Roles", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<bool>( "IsBlank", ( this.IsBlank ) ).Set<DynamicMenuGroup>( "Group", this.Group ).Set<int>( "SortOrder", this.SortOrder ).Set<string>( "Style", this.Style ).Set<DynamicPage>( "Page", this.Page ).Set<Product>( "Product", this.Product ).Set<BaseEntitySet<DynamicMenuDomain>>( "Domains", this.Domains ).Set<BaseEntitySet<Client>>( "Roles", this.Roles );
        }
    }
}
