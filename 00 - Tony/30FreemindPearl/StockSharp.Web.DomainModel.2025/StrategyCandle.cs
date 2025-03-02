// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyCandle
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyCandle : INameEntity, IPersistable
    {
        public string Name { get; set; }

        public string Security { get; set; }

        public string TypeName { get; set; }

        public string Arg { get; set; }

        public string Style { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Security = ( string ) storage.GetValue<string>( "Security", null );
            this.TypeName = ( string ) storage.GetValue<string>( "TypeName", null );
            this.Arg = ( string ) storage.GetValue<string>( "Arg", null );
            this.Style = ( string ) storage.GetValue<string>( "Style", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Name", this.Name ).Set<string>( "Security", this.Security ).Set<string>( "TypeName", this.TypeName ).Set<string>( "Arg", this.Arg ).Set<string>( "Style", this.Style );
        }
    }
}
