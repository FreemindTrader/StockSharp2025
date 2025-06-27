// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyStatParameter
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;

namespace StockSharp.Web.DomainModel
{
    public class StrategyStatParameter : IPersistable
    {
        public StatisticParameterTypes Type { get; set; }

        public object Value { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Type = ( StatisticParameterTypes ) storage.GetValue<StatisticParameterTypes>( "Type", 0 );
            this.Value = ( object ) storage.GetValue<object>( "Value", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<StatisticParameterTypes>( "Type", this.Type ).Set<object>( "Value", this.Value );
        }
    }
}
