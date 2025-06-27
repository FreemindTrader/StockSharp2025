// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyIndicatorValue
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyIndicatorValue : IPersistable
    {
        public int Index { get; set; }

        public object [ ] Values { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Index = ( int ) storage.GetValue<int>( "Index", 0 );
            this.Values = ( object [ ] ) storage.GetValue<object [ ]>( "Values", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<int>( "Index", this.Index ).Set<object [ ]>( "Values", this.Values );
        }
    }
}
