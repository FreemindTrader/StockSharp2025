// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicPageDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DynamicPageDomain : BaseEntity, ITopicEntity, IDomainEntity
    {
        public DynamicPage Page { get; set; }

        public Domain Domain { get; set; }

        public Topic Topic { get; set; }

        public string UrlPart { get; set; }

        public string UrlRelative { get; set; }

        public string RedirectUrl { get; set; }

        public File File { get; set; }

        public string ActionTitle { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Page = ( DynamicPage ) storage.GetValue<DynamicPage>( "Page", null );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.Topic = ( Topic ) storage.GetValue<Topic>( "Topic", null );
            this.UrlPart = ( string ) storage.GetValue<string>( "UrlPart", null );
            this.UrlRelative = ( string ) storage.GetValue<string>( "UrlRelative", null );
            this.RedirectUrl = ( string ) storage.GetValue<string>( "RedirectUrl", null );
            this.File = ( File ) storage.GetValue<File>( "File", null );
            this.ActionTitle = ( string ) storage.GetValue<string>( "ActionTitle", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<DynamicPage>( "Page", this.Page ).Set<Domain>( "Domain", this.Domain ).Set<Topic>( "Topic", this.Topic ).Set<string>( "UrlPart", this.UrlPart ).Set<string>( "UrlRelative", this.UrlRelative ).Set<string>( "RedirectUrl", this.RedirectUrl ).Set<File>( "File", this.File ).Set<string>( "ActionTitle", this.ActionTitle );
        }
    }
}
