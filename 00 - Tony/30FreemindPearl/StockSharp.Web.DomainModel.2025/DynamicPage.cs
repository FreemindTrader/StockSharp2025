// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicPage
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace StockSharp.Web.DomainModel
{
    public class DynamicPage : BaseEntity, IDomainsEntity<DynamicPageDomain>, INameEntity, IDescriptionEntity
    {
        private string _name;
        private string _description;

        public DynamicPage Parent { get; set; }

        public bool IsEnabled { get; set; }

        public DynamicMenuGroup MenuGroup { get; set; }

        public bool IsSitemap { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public DynamicPageHandlers Handler { get; set; }

        public bool UseQueryString { get; set; }

        public BaseEntitySet<DynamicPage> Childs { get; set; }

        public BaseEntitySet<DynamicPageDomain> Domains { get; set; }

        string INameEntity.Name
        {
            get
            {
                string name = this._name;
                BaseEntitySet<DynamicPageDomain> domains = this.Domains;
                string str1;
                if ( domains == null )
                {
                    str1 = ( string ) null;
                }
                else
                {
                    DynamicPageDomain[] items = domains.Items;
                    str1 = items != null ? ( ( IEnumerable<DynamicPageDomain> ) items ).FirstOrDefault<DynamicPageDomain>()?.UrlPart : ( string ) null;
                }
                var m0 = Converter.To<string>( this.Id);
                string str2 = StringHelper.IsEmpty(str1, (string) m0);
                return StringHelper.IsEmpty( name, str2 );
            }
            set
            {
                this._name = value;
            }
        }

        string IDescriptionEntity.Description
        {
            get
            {
                string description = this._description;
                BaseEntitySet<DynamicPageDomain> domains = this.Domains;
                string str;
                if ( domains == null )
                {
                    str = ( string ) null;
                }
                else
                {
                    DynamicPageDomain[] items = domains.Items;
                    str = items != null ? ( ( IEnumerable<DynamicPageDomain> ) items ).FirstOrDefault<DynamicPageDomain>()?.Topic?.Name : ( string ) null;
                }
                return StringHelper.IsEmpty( description, str );
            }
            set
            {
                this._description = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Parent = ( DynamicPage ) storage.GetValue<DynamicPage>( "Parent", null );
            this.IsEnabled = ( bool ) storage.GetValue<bool>( "IsEnabled", false );
            this.MenuGroup = ( DynamicMenuGroup ) storage.GetValue<DynamicMenuGroup>( "MenuGroup", null );
            this.IsSitemap = ( bool ) storage.GetValue<bool>( "IsSitemap", false );
            this.StatusCode = ( HttpStatusCode ) storage.GetValue<HttpStatusCode>( "StatusCode", 0 );
            this.StatusDescription = ( string ) storage.GetValue<string>( "StatusDescription", null );
            this.Handler = ( DynamicPageHandlers ) storage.GetValue<DynamicPageHandlers>( "Handler", 0 );
            this.UseQueryString = ( bool ) storage.GetValue<bool>( "UseQueryString", false);
            this.Childs = ( BaseEntitySet<DynamicPage> ) storage.GetValue<BaseEntitySet<DynamicPage>>( "Childs", null );
            this.Domains = ( BaseEntitySet<DynamicPageDomain> ) storage.GetValue<BaseEntitySet<DynamicPageDomain>>( "Domains", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<DynamicPage>( "Parent", this.Parent ).Set<bool>( "IsEnabled", ( this.IsEnabled  ) ).Set<DynamicMenuGroup>( "MenuGroup", this.MenuGroup ).Set<bool>( "IsSitemap", ( this.IsSitemap ) ).Set<HttpStatusCode>( "StatusCode", this.StatusCode ).Set<string>( "StatusDescription", this.StatusDescription ).Set<DynamicPageHandlers>( "Handler", this.Handler ).Set<bool>( "UseQueryString", ( this.UseQueryString ) ).Set<BaseEntitySet<DynamicPage>>( "Childs", this.Childs ).Set<BaseEntitySet<DynamicPageDomain>>( "Domains", this.Domains );
        }
    }
}
