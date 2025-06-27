// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SiteSettingsKeys
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class SiteSettingsKeys : BaseEntity
    {
        public SiteSettings Settings { get; set; }

        public string AppName { get; set; }

        public string Keys { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Settings = ( SiteSettings ) storage.GetValue<SiteSettings>( "Settings", null );
            this.AppName = ( string ) storage.GetValue<string>( "AppName", null );
            this.Keys = ( string ) storage.GetValue<string>( "Keys", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<SiteSettings>( "Settings", this.Settings ).Set<string>( "AppName", this.AppName ).Set<string>( "Keys", this.Keys );
        }
    }
}
