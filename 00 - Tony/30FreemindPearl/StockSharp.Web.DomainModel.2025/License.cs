// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.License
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class License : BaseEntity, IClientEntity, IFileEntity, IDescriptionEntity
    {
        public Client Client { get; set; }

        public File File { get; set; }

        public string Description { get; set; }

        public byte [ ] Body { get; set; }

        public BaseEntitySet<LicenseFeatureEx> Features { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.File = ( File ) storage.GetValue<File>( "File", null );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.Body = ( byte [ ] ) storage.GetValue<byte [ ]>( "Body", null );
            this.Features = ( BaseEntitySet<LicenseFeatureEx> ) storage.GetValue<BaseEntitySet<LicenseFeatureEx>>( "Features", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<File>( "File", this.File ).Set<string>( "Description", this.Description ).Set<byte [ ]>( "Body", this.Body ).Set<BaseEntitySet<LicenseFeatureEx>>( "Features", this.Features );
        }
    }
}
