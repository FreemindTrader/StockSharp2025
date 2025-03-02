// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicMenuDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DynamicMenuDomain : BaseEntity, IDomainEntity, INameEntity, IDescriptionEntity
    {
        public DynamicMenu Menu { get; set; }

        public Domain Domain { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UrlRelative { get; set; }

        public string UrlAbsolute { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Menu = ( DynamicMenu ) storage.GetValue<DynamicMenu>( "Menu", null );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.UrlRelative = ( string ) storage.GetValue<string>( "UrlRelative", null );
            this.UrlAbsolute = ( string ) storage.GetValue<string>( "UrlAbsolute", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<DynamicMenu>( "Menu", this.Menu ).Set<Domain>( "Domain", this.Domain ).Set<string>( "Name", this.Name ).Set<string>( "Description", this.Description ).Set<string>( "UrlRelative", this.UrlRelative ).Set<string>( "UrlAbsolute", this.UrlAbsolute );
        }
    }
}
