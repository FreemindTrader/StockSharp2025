// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailBulk
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class EmailBulk : BaseEntity, IDomainEntity, INameEntity
    {
        public string FromAlias { get; set; }

        public string FromEmail { get; set; }

        public Domain Domain { get; set; }

        public string Name { get; set; }

        public File Html { get; set; }

        public File Text { get; set; }

        public bool IsShortLink { get; set; }

        public EmailPreferences EmailPreferences { get; set; }

        public string [ ] ParamKeys { get; set; }

        public string [ ] ParamValues { get; set; }

        public BaseEntitySet<Client> Included { get; set; }

        public BaseEntitySet<Client> Excluded { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.FromAlias = ( string ) storage.GetValue<string>( "FromAlias", null );
            this.FromEmail = ( string ) storage.GetValue<string>( "FromEmail", null );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Html = ( File ) storage.GetValue<File>( "Html", null );
            this.Text = ( File ) storage.GetValue<File>( "Text", null );
            this.IsShortLink = ( bool ) storage.GetValue<bool>( "IsShortLink", false );
            this.EmailPreferences = ( EmailPreferences ) storage.GetValue<EmailPreferences>( "EmailPreferences", 0L );
            this.ParamKeys = ( string [ ] ) storage.GetValue<string [ ]>( "ParamKeys", null );
            this.ParamValues = ( string [ ] ) storage.GetValue<string [ ]>( "ParamValues", null );
            this.Included = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Included", null );
            this.Excluded = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Excluded", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "FromAlias", this.FromAlias ).Set<string>( "FromEmail", this.FromEmail ).Set<Domain>( "Domain", this.Domain ).Set<string>( "Name", this.Name ).Set<File>( "Html", this.Html ).Set<File>( "Text", this.Text ).Set<bool>( "IsShortLink", ( this.IsShortLink  ) ).Set<EmailPreferences>( "EmailPreferences", this.EmailPreferences ).Set<string [ ]>( "ParamKeys", this.ParamKeys ).Set<string [ ]>( "ParamValues", this.ParamValues ).Set<BaseEntitySet<Client>>( "Included", this.Included ).Set<BaseEntitySet<Client>>( "Excluded", this.Excluded );
        }
    }
}
