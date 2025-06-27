// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyIndicatorParam
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyIndicatorParam : IPersistable
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Value = ( object ) storage.GetValue<object>( "Value", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Name", this.Name ).Set<object>( "Value", this.Value );
        }
    }
}
