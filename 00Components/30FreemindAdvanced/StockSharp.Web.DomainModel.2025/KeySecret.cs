// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.KeySecret
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class KeySecret : IPersistable
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Key = ( string ) storage.GetValue<string>( "Key", null );
            this.Secret = ( string ) storage.GetValue<string>( "Secret", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Key", this.Key ).Set<string>( "Secret", this.Secret );
        }
    }
}
