// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SocialUrl
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class SocialUrl : IPersistable
    {
        public string Base { get; set; }

        public string Mask { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Base = ( string ) storage.GetValue<string>( "Base", null );
            this.Mask = ( string ) storage.GetValue<string>( "Mask", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Base", this.Base ).Set<string>( "Mask", this.Mask );
        }
    }
}
