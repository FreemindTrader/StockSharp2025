// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.LicenseFeature
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class LicenseFeature : BaseEntity, INameEntity
    {
        public string Name { get; set; }

        public BaseEntitySet<License> Licenses { get; set; }

        public BaseEntitySet<Client> Roles { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Licenses = ( BaseEntitySet<License> ) storage.GetValue<BaseEntitySet<License>>( "Licenses", null );
            this.Roles = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Roles", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<BaseEntitySet<License>>( "Licenses", this.Licenses ).Set<BaseEntitySet<Client>>( "Roles", this.Roles );
        }
    }
}
