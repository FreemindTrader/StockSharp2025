// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyCommandData
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyCommandData : IPersistable
    {
        public Strategy Strategy { get; set; }

        public CommandInfo Command { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Strategy = ( Strategy ) storage.GetValue<Strategy>( "Strategy", null );
            this.Command = ( CommandInfo ) storage.GetValue<CommandInfo>( "Command", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<Strategy>( "Strategy", this.Strategy ).Set<CommandInfo>( "Command", this.Command );
        }
    }
}
