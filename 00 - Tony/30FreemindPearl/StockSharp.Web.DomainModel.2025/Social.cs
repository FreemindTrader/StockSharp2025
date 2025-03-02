// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Social
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Social : BaseEntity, INameEntity, IPictureEntity
    {
        public string Url { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string LoginScope { get; set; }

        public string PostScope { get; set; }

        public SocialUrl AuthReal { get; set; }

        public SocialUrl AuthDemo { get; set; }

        public string AuthHandler { get; set; }

        public int CharsLimit { get; set; }

        public File Picture { get; set; }

        public Domain AuthDomainOnly { get; set; }

        public KeySecret Auth { get; set; }

        public KeySecret Blog { get; set; }

        public KeySecret AccessToken { get; set; }

        public string BlogHandler { get; set; }

        public SocialFlags Flags { get; set; }

        public bool Logging { get; set; }

        public BaseEntitySet<Client> Clients { get; set; }

        public BaseEntitySet<ClientSocial> ClientSocials { get; set; }

        public BaseEntitySet<Client> Tokens { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Url = ( string ) storage.GetValue<string>( "Url", null );
            this.Code = ( string ) storage.GetValue<string>( "Code", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.LoginScope = ( string ) storage.GetValue<string>( "LoginScope", null );
            this.PostScope = ( string ) storage.GetValue<string>( "PostScope", null );
            this.AuthReal = ( SocialUrl ) storage.GetValue<SocialUrl>( "AuthReal", null );
            this.AuthDemo = ( SocialUrl ) storage.GetValue<SocialUrl>( "AuthDemo", null );
            this.AuthHandler = ( string ) storage.GetValue<string>( "AuthHandler", null );
            this.CharsLimit = ( int ) storage.GetValue<int>( "CharsLimit", 0 );
            this.Picture = ( File ) storage.GetValue<File>( "Picture", null );
            this.AuthDomainOnly = ( Domain ) storage.GetValue<Domain>( "AuthDomainOnly", null );
            this.Auth = ( KeySecret ) storage.GetValue<KeySecret>( "Auth", null );
            this.Blog = ( KeySecret ) storage.GetValue<KeySecret>( "Blog", null );
            this.AccessToken = ( KeySecret ) storage.GetValue<KeySecret>( "AccessToken", null );
            this.BlogHandler = ( string ) storage.GetValue<string>( "BlogHandler", null );
            this.Flags = ( SocialFlags ) storage.GetValue<SocialFlags>( "Flags", 0L );
            this.Logging = ( bool ) storage.GetValue<bool>( "Logging", false );
            this.Clients = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Clients", null );
            this.ClientSocials = ( BaseEntitySet<ClientSocial> ) storage.GetValue<BaseEntitySet<ClientSocial>>( "ClientSocials", null );
            this.Tokens = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Tokens", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Url", this.Url ).Set<string>( "Code", this.Code ).Set<string>( "Name", this.Name ).Set<string>( "LoginScope", this.LoginScope ).Set<string>( "PostScope", this.PostScope ).Set<SocialUrl>( "AuthReal", this.AuthReal ).Set<SocialUrl>( "AuthDemo", this.AuthDemo ).Set<string>( "AuthHandler", this.AuthHandler ).Set<int>( "CharsLimit", this.CharsLimit ).Set<File>( "Picture", this.Picture ).Set<Domain>( "AuthDomainOnly", this.AuthDomainOnly ).Set<KeySecret>( "Auth", this.Auth ).Set<KeySecret>( "Blog", this.Blog ).Set<KeySecret>( "AccessToken", this.AccessToken ).Set<string>( "BlogHandler", this.BlogHandler ).Set<SocialFlags>( "Flags", this.Flags ).Set<bool>( "Logging", ( this.Logging  ) ).Set<BaseEntitySet<Client>>( "Clients", this.Clients ).Set<BaseEntitySet<ClientSocial>>( "ClientSocials", this.ClientSocials ).Set<BaseEntitySet<Client>>( "Tokens", this.Tokens );
        }
    }
}
