// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PayGatewayDomainSettings
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PayGatewayDomainSettings : IPersistable
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Url { get; set; }

        public string Token { get; set; }

        public File Certificate { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Login = ( string ) storage.GetValue<string>( "Login", null );
            this.Password = ( string ) storage.GetValue<string>( "Password", null );
            this.Url = ( string ) storage.GetValue<string>( "Url", null );
            this.Token = ( string ) storage.GetValue<string>( "Token", null );
            this.Certificate = ( File ) storage.GetValue<File>( "Certificate", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Login", this.Login ).Set<string>( "Password", this.Password ).Set<string>( "Url", this.Url ).Set<string>( "Token", this.Token ).Set<File>( "Certificate", this.Certificate );
        }
    }
}
