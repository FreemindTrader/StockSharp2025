// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailTemplateDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class EmailTemplateDomain : BaseEntity, IDomainEntity
    {
        public EmailTemplate Template { get; set; }

        public Domain Domain { get; set; }

        public File Html { get; set; }

        public Topic Text { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Template = ( EmailTemplate ) storage.GetValue<EmailTemplate>( "Template", null );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.Html = ( File ) storage.GetValue<File>( "Html", null );
            this.Text = ( Topic ) storage.GetValue<Topic>( "Text", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<EmailTemplate>( "Template", this.Template ).Set<Domain>( "Domain", this.Domain ).Set<File>( "Html", this.Html ).Set<Topic>( "Text", this.Text );
        }
    }
}
