// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductBaseEmailTemplate
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductBaseEmailTemplate : IPersistable
    {
        public EmailTemplate ToClient { get; set; }

        public EmailTemplate ToManager { get; set; }

        public EmailTemplate ToSite { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.ToClient = ( EmailTemplate ) storage.GetValue<EmailTemplate>( "ToClient", null );
            this.ToManager = ( EmailTemplate ) storage.GetValue<EmailTemplate>( "ToManager", null );
            this.ToSite = ( EmailTemplate ) storage.GetValue<EmailTemplate>( "ToSite", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<EmailTemplate>( "ToClient", this.ToClient ).Set<EmailTemplate>( "ToManager", this.ToManager ).Set<EmailTemplate>( "ToSite", this.ToSite );
        }
    }
}
